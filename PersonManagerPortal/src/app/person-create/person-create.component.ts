import { Component, OnDestroy, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { debounceTime, Subscription } from 'rxjs';
import { ISocialMediaAccount } from '../model/read/socialmediaaccount';
import { IPersonSocialMediaAccount } from '../model/shared/personsocialmediaaccount';
import { CreatePerson } from '../model/write/person';
import { PersonManagerService } from '../services/person.service';

@Component({
  templateUrl: './person-create.component.html',
  styleUrls: ['./person-create.component.css']
})
export class PersonCreateComponent implements OnInit, OnDestroy {

  socialSkills: Array<string> = [];
  socialMediaAccounts: ISocialMediaAccount[] = [];
  personSocialMediaAccounts: IPersonSocialMediaAccount[] = [];

  sub!: Subscription;
  errorMsg: string = '';
  addPersonForm!: FormGroup;

  validationMessages: { [key: string]: { [key: string]: string } };
  firstNameValidationMessage: string = '';
  lastNameValidationMessage: string = '';
  socialSkillValidationMessage: string = '';

  constructor(private formBuilder: FormBuilder, private _personManagerService: PersonManagerService, private _router: Router) {

    this.validationMessages = {
      firstName: {
        required: 'First name is required',
        minlength: 'First name must be minimal lenght of 3',
        pattern: 'First name must be all letters'
      },
      lastName: {
        required: 'Last name is required',
        minlength: 'Last name must be minimal lenght of 3',
        pattern: 'Last name must be all letters'
      },
      socialSkill:
      {
        minlength: 'Social skill must be minimal lenght of 3',
        pattern: 'Social skill must be all letters'
      }
    };
  }

  ngOnInit(): void {

    this.sub = this._personManagerService.getSocialMediaAccounts().subscribe({
      next: smas => {
        this.socialMediaAccounts = smas;
      },
      error: err => this.errorMsg = err
    });

    this.addPersonForm = this.formBuilder.group(
      {
        firstName: ['', [Validators.required, Validators.minLength(3), Validators.pattern('^[a-zA-Z ]*$')]],
        lastName: ['', [Validators.required, Validators.minLength(3), Validators.pattern('^[a-zA-Z ]*$')]],
        socialSkill: ['', [Validators.minLength(3), Validators.pattern('^[a-zA-Z ]*$')]],
        personSocialMediaAccountAddress: '',
        selectedSocialMediaAccountId: ''
      }
    );

    this.addPersonForm.get('firstName')?.valueChanges.pipe(debounceTime(1000)).subscribe(
      value => this.setValidationMsgForFirstName(this.addPersonForm.controls['firstName'])
    );

    this.addPersonForm.get('lastName')?.valueChanges.pipe(debounceTime(1000)).subscribe(
      value => this.setValidationMsgForLastName(this.addPersonForm.controls['lastName'])
    );

    this.addPersonForm.get('socialSkill')?.valueChanges.pipe(debounceTime(1000)).subscribe(
      value => this.setValidationMsgForSocialSkill(this.addPersonForm.controls['socialSkill'])
    );
  }

  setValidationMsgForFirstName(c: AbstractControl): void {
    this.firstNameValidationMessage = '';

    if (c.errors && (c.touched || c.dirty)) {
      this.firstNameValidationMessage = Object.keys(c.errors).map(key => this.validationMessages['firstName'][key]).join(' ');
    }
  }

  setValidationMsgForLastName(c: AbstractControl): void {
    this.lastNameValidationMessage = '';

    if (c.errors && (c.touched || c.dirty)) {
      this.lastNameValidationMessage = Object.keys(c.errors).map(key => this.validationMessages['lastName'][key]).join(' ');
    }
  }

  setValidationMsgForSocialSkill(c: AbstractControl): void {
    this.socialSkillValidationMessage = '';

    if (this.socialSkills.length === 0 && c.touched) {
      this.socialSkillValidationMessage = 'At least one social skill must be added.';
    }
    if (c.errors && c.dirty) {
      this.socialSkillValidationMessage += Object.keys(c.errors).map(key => this.validationMessages['socialSkill'][key]).join(' ');
    }
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }

  onAddSocialSkill() {
    const socialSkill = this.addPersonForm.get('socialSkill')?.value;
    if (socialSkill) {
      this.socialSkills.push(socialSkill);
    }
    this.addPersonForm.controls['socialSkill'].setValue('');
  }

  onDeleteSocialSkill(socialSkill: string) {
    var index = this.socialSkills.indexOf(socialSkill);
    if (index !== -1) {
      this.socialSkills.splice(index, 1);
    }
    this.setValidationMsgForSocialSkill(this.addPersonForm.controls['socialSkill'])
  }

  onAddPersonSocialMediaAccount() {

    const selectedSocialMediaAccountId = this.addPersonForm.get('selectedSocialMediaAccountId')?.value;
    const personSocialMediaAccountAddress = this.addPersonForm.get('personSocialMediaAccountAddress')?.value;

    if (selectedSocialMediaAccountId && personSocialMediaAccountAddress) {
      const sma: IPersonSocialMediaAccount =
      {
        accountId: selectedSocialMediaAccountId,
        type: this.socialMediaAccounts.find(obj => {
          return obj.socialMediaAccountId == selectedSocialMediaAccountId
        })?.type,
        address: personSocialMediaAccountAddress
      };

      this.personSocialMediaAccounts.push(sma);
      this.addPersonForm.controls['selectedSocialMediaAccountId'].setValue('');
      this.addPersonForm.controls['personSocialMediaAccountAddress'].setValue('');
    }
  }

  onDeletePersonSocialMediaAccount(personSocialMediaAccount: IPersonSocialMediaAccount) {
    var index = this.personSocialMediaAccounts.indexOf(personSocialMediaAccount);
    if (index !== -1) {
      this.personSocialMediaAccounts.splice(index, 1);
    }
  }

  formValid(): boolean {
    return this.addPersonForm.valid && this.socialSkills.length > 0 && this.personSocialMediaAccounts.length > 0;
  }

  onSubmit() {
    if (this.formValid()) {
      const person = new CreatePerson(
        this.addPersonForm.get('firstName')?.value,
        this.addPersonForm.get('lastName')?.value,
        this.socialSkills,
        this.personSocialMediaAccounts);

      this._personManagerService.addPerson(person)
        .subscribe(response => {
          this.addPersonForm.reset();
          this._router.navigate(['/persondetails/', response]);
        });
    }
  }
}
