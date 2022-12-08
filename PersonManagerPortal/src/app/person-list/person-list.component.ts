import { Component, OnDestroy, OnInit } from "@angular/core";
import { Subscription } from "rxjs";
import { PersonResponse } from "../model/read/person";
import { PersonManagerService } from "../services/person.service";

@Component({
    templateUrl: './person-list.component.html',
    styleUrls: ["./person-list.component.css"]
})
export class PersonListComponent implements OnInit, OnDestroy
{
    persons: PersonResponse[] = [];
    filteredPersons: PersonResponse[] = [];
    pageTitle: string = 'Person list'
    errorMsg : string = "";
    sub! : Subscription;
    private _listFilter = "";

    constructor(private _personManagerService : PersonManagerService) {
    }

    ngOnInit(): void {
        this.sub = this._personManagerService.getPersons().subscribe({
            next: prs => 
            {
              this.persons = prs;
              this.filteredPersons = this.persons;
            },
            error: err => this.errorMsg = err
          });
    }

    ngOnDestroy(): void {
        this.sub.unsubscribe();
    }
    
    get filter() : string
    {
        return this._listFilter;
    }

    set filter(value : string)
    {
      this._listFilter = value;
      this.filteredPersons = this.performFilteringBy(this._listFilter);
    }

    performFilteringBy(condition : string) : PersonResponse[]
    {
      return this.persons.filter((prod : PersonResponse) => 
        prod.fullName.toLocaleLowerCase().includes(condition.toLocaleLowerCase())
      );
    }
}
