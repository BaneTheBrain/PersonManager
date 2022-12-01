import { Component, OnDestroy, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { ISocialMediaAccount } from '../model/read/socialmediaaccount';
import { IPersonSocialMediaAccount } from '../model/shared/personsocialmediaaccount';
import { CreatePerson } from '../model/write/person';
import { PersonManagerService } from '../services/person.service';

@Component({
  templateUrl: './person-create.component.html',
  styleUrls: ['./person-create.component.css']
})
export class PersonCreateComponent implements OnInit, OnDestroy {

  firstName: string = '';
  lastName: string = '';

  socialSkill: string = '';
  socialSkills: Array<string> = [];

  socialMediaAccounts: ISocialMediaAccount[] = [];
  selectedSocialMediaAccountId: string ='';
  personSocialMediaAccountAddress: string = '';
  personSocialMediaAccounts: IPersonSocialMediaAccount[] = [];

  sub!: Subscription;
  errorMsg: string = '';

  constructor(private _personManagerService: PersonManagerService, private _router: Router) {
  }

  ngOnInit(): void {
    this.sub = this._personManagerService.getSocialMediaAccounts().subscribe({
      next: smas => {
        this.socialMediaAccounts = smas;
      },
      error: err => this.errorMsg = err
    });
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }

  onAddSocialSkill(socialSkill: string) {
    this.socialSkills.push(socialSkill);
    this.socialSkill = ''
  }

  onDeleteSocialSkill(socialSkill: string) {
    var index = this.socialSkills.indexOf(socialSkill);
    if (index !== -1) {
      this.socialSkills.splice(index, 1);
    }
  }

  onAddPersonSocialMediaAccount() {

    const sma: IPersonSocialMediaAccount =
    {
      accountId: this.selectedSocialMediaAccountId,
      type: this.socialMediaAccounts.find(obj => 
        {
          return obj.socialMediaAccountId == this.selectedSocialMediaAccountId
        })?.type,
      address: this.personSocialMediaAccountAddress,
    };

    this.personSocialMediaAccounts.push(sma);
    this.personSocialMediaAccountAddress = '';
  }

  onDeletePersonSocialMediaAccount(personSocialMediaAccount: IPersonSocialMediaAccount) {
    var index = this.personSocialMediaAccounts.indexOf(personSocialMediaAccount);
    if (index !== -1) {
      this.personSocialMediaAccounts.splice(index, 1);
    }
  }

  onSelectionChanged(selectedSocialMediaAccountId: string) {
  }
  
  onSubmit() {
       const person = new CreatePerson(this.firstName, this.lastName, this.socialSkills, this.personSocialMediaAccounts);
       
       this._personManagerService.addPerson(person)
       .subscribe(response => 
        {
           this._router.navigate(['/persondetails/', response]);
       });

  }
}
