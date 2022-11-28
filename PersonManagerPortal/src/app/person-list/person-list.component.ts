import { Component, OnDestroy, OnInit } from "@angular/core";
import { Subscription } from "rxjs";
import { IPerson } from "../model/person";
import { PersonService } from "../services/person.service";

@Component({
    templateUrl: './person-list.component.html',
    styleUrls: ["./person-list.component.css"]
})
export class PersonListComponent implements OnInit, OnDestroy
{
    persons: IPerson[] = [];
    filteredPersons: IPerson[] = [];
    pageTitle: string = 'Person list'
    errorMsg : string = "";
    sub! : Subscription;
    private _listFilter = "";

    constructor(private _personService : PersonService) {
    }

    ngOnInit(): void {
        this.sub = this._personService.getPersons().subscribe({
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

    performFilteringBy(condition : string) : IPerson[]
    {
      return this.persons.filter((prod : IPerson) => 
        prod.fullName.toLocaleLowerCase().includes(condition.toLocaleLowerCase())
      );
    }
}
