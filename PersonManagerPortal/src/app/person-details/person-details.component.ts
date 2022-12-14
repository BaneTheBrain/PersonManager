import { Component, OnDestroy, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { PersonResponse } from "../model/read/person";
import { PersonManagerService } from "../services/person.service";

@Component({
    templateUrl: './person-details.component.html'
})
export class PersonDetailsComponent implements OnInit
{
    pageTitle: string = 'Person details'
    person!: PersonResponse;
    errorMessage : string = '';
    private _id : string ='';

    constructor(
        private _personManagerService : PersonManagerService,
        private route: ActivatedRoute, 
        private router: Router) {
    }

    ngOnInit(): void {
        
        //when paging is implemented, you need to subscribe to rout.paramMap.Subscribe(..) to get last changed route param
        //because you don't change details view and you can't use route.snapshot.paramMap

        this._id = String(this.route.snapshot.paramMap.get('id'));

        this._personManagerService.getPerson(this._id).subscribe(
            {
            next: person => this.person = person,
            error: err => this.errorMessage = err
          });
    }

    onBack(): void
    {
        this.router.navigate(['/persons']);
    }
}