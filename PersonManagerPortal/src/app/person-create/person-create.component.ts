import { Component, OnDestroy, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { debounceTime, Subscription } from 'rxjs';
import { PersonResponse } from '../model/read/person';
import { SocialMediaAccountResponse } from '../model/read/socialmediaaccount';
import { PersonRequest, PersonSocialMediaAccountRequest } from '../model/write/person';
import { PersonManagerService } from '../services/person.service';

@Component({
  templateUrl: './person-create.component.html',
  styleUrls: ['./person-create.component.css']
})
export class PersonCreateComponent implements OnInit, OnDestroy {

  //#region fields
  availableAccounts: SocialMediaAccountResponse[] = [];
  personSocialSkills: Array<string> = [];
  personSocialMediaAccounts: PersonSocialMediaAccountRequest[] = [];
  
  pageTitle: string = '';
  sub!: Subscription;
  errorMsg: string = '';
  addPersonForm!: FormGroup;

  validationMessages: { [key: string]: { [key: string]: string } };
  firstNameValidationMessage: string = '';
  lastNameValidationMessage: string = '';
  socialSkillValidationMessage: string = '';
  psmaValidationMessage: string = '';
  personId: string ='';
  //#endregion

  constructor(
    private formBuilder: FormBuilder, 
    private _personManagerService: PersonManagerService, 
    private _router: Router,
    private _activatedRoute: ActivatedRoute) {

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
      },
      psma:
      {
        minlength: 'Person social media account address must be minimal lenght of 3',
      }
    };
  }

  ngOnInit(): void {
    
    this.sub = this.loadSocialMediaAccounts();
    this.addPersonForm = this.createForm();
    this.setValidationMessages();
    
    this.pageTitle = "New person";
    this.personId = '';

    const personId = this._activatedRoute.snapshot.paramMap.get('id');
    if(personId !== null)
    {
      this.pageTitle = "Edit person";
      this._personManagerService.getPerson(personId).subscribe(
        {
        next: person => this.displayEditingPerson(person),
        error: err => this.errorMsg = err
      });
    }
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }

  //#region validation
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

    if (c.errors && (c.touched || c.dirty)) {
        this.socialSkillValidationMessage = Object.keys(c.errors).map(key => this.validationMessages['socialSkill'][key]).join(' ');
    }
    if (this.personSocialSkills.length === 0) {
      this.socialSkillValidationMessage += 'At least one social skill must be added.';
    }
  }

  setValidationMsgForPSMA(c: AbstractControl): void {
    this.psmaValidationMessage = '';

    if (c.errors && (c.touched || c.dirty)) {
      this.psmaValidationMessage = Object.keys(c.errors).map(key => this.validationMessages['psma'][key]).join(' ');
    }
    if (this.personSocialMediaAccounts.length === 0) {
      this.psmaValidationMessage += 'At least one person social media account must be added.';
    }
  }
  //#endregion

  //#region add/remove array elements
  onAddSocialSkill() {
    const socialSkill = this.addPersonForm.get('socialSkill')?.value;
    if (socialSkill) {
      this.personSocialSkills.push(socialSkill);
    }
    this.addPersonForm.controls['socialSkill'].setValue('');
  }

  onDeleteSocialSkill(socialSkill: string) {
    var index = this.personSocialSkills.indexOf(socialSkill);
    if (index !== -1) {
      this.personSocialSkills.splice(index, 1);
    }
    this.addPersonForm.controls['socialSkill'].setValue('');
  }

  onAddPersonSocialMediaAccount() {

    const selectedAccountId = this.addPersonForm.get('selectedAccountId')?.value;
    const accountAddress = this.addPersonForm.get('accountAddress')?.value;

    if (selectedAccountId && accountAddress) {
      
      const type = this.availableAccounts.find(obj => {
        return obj.socialMediaAccountId == selectedAccountId
      })?.type;

      const psma = new PersonSocialMediaAccountRequest(selectedAccountId, accountAddress, type);

      this.personSocialMediaAccounts.push(psma);
      this.addPersonForm.controls['selectedAccountId'].setValue('');
      this.addPersonForm.controls['accountAddress'].setValue('');
    }
  }

  onDeletePersonSocialMediaAccount(personSocialMediaAccount: PersonSocialMediaAccountRequest) {
    var index = this.personSocialMediaAccounts.indexOf(personSocialMediaAccount);
    if (index !== -1) {
      this.personSocialMediaAccounts.splice(index, 1);
    }
    this.addPersonForm.controls['selectedAccountId'].setValue('');
    this.addPersonForm.controls['accountAddress'].setValue('');
  }
  //#endregion

  //#region methods

  private displayEditingPerson(personResponse: PersonResponse): void {
    
    this.personId = personResponse.personId;

    this.addPersonForm.patchValue({
      firstName: personResponse.firstName,
      lastName: personResponse.lastName,
    });

    this.personSocialSkills.push(...personResponse.personSkills);

    for(let psma of personResponse.personSocialMediaAccounts)
    {
      const psmaRequest = new PersonSocialMediaAccountRequest(psma.socialMediaAccountId, psma.address, psma.type);
      this.personSocialMediaAccounts.push(psmaRequest);
    }
  }

  private setValidationMessages() {
    this.addPersonForm.get('firstName')?.valueChanges.pipe(debounceTime(1000)).subscribe(
      value => this.setValidationMsgForFirstName(this.addPersonForm.controls['firstName'])
    );

    this.addPersonForm.get('lastName')?.valueChanges.pipe(debounceTime(1000)).subscribe(
      value => this.setValidationMsgForLastName(this.addPersonForm.controls['lastName'])
    );

    this.addPersonForm.get('socialSkill')?.valueChanges.pipe(debounceTime(1000)).subscribe(
      value => this.setValidationMsgForSocialSkill(this.addPersonForm.controls['socialSkill'])
    );

    this.addPersonForm.get('accountAddress')?.valueChanges.pipe(debounceTime(1000)).subscribe(
      value => this.setValidationMsgForPSMA(this.addPersonForm.controls['accountAddress'])
    );
  }

  private loadSocialMediaAccounts(): Subscription {
    return this._personManagerService.getSocialMediaAccounts().subscribe({
      next: smas => {
        this.availableAccounts = smas;
      },
      error: err => this.errorMsg = err
    });
  }

  private createForm(): FormGroup {
    return this.formBuilder.group(
      {
        firstName: ['', [Validators.required, Validators.minLength(3), Validators.pattern('^[a-zA-Z ]*$')]],
        lastName: ['', [Validators.required, Validators.minLength(3), Validators.pattern('^[a-zA-Z ]*$')]],
        socialSkill: ['', [Validators.minLength(3), Validators.pattern('^[a-zA-Z ]*$')]],
        accountAddress: ['', [Validators.minLength(3)]],
        selectedAccountId: ''
      });
  }

  formValid(): boolean {
    return this.addPersonForm.valid && this.personSocialSkills.length > 0 && this.personSocialMediaAccounts.length > 0;
  }

  onSubmit() {

    if (this.formValid()) {

      const person = new PersonRequest(
        this.addPersonForm.get('firstName')?.value,
        this.addPersonForm.get('lastName')?.value,
        this.personSocialSkills,
        this.personSocialMediaAccounts);

      if (this.personId !== '') {
        //editing
        person.personId = this.personId;
        this._personManagerService.updatePerson(person)
          .subscribe(response => {
            this._router.navigate(['/persons']);
          });
      }
      else {
        //adding
        this._personManagerService.addPerson(person)
          .subscribe(response => {
            this._router.navigate(['/persons/', response]);
          });
      }
    }
  }
  //#endregion
}
