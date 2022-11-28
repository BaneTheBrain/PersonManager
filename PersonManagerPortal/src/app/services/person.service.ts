import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, catchError, tap, throwError, map } from "rxjs";
import { environment } from "src/environments/environment";
import { IPerson } from "../model/person";

@Injectable(
    {
        providedIn: 'root'
    }
)
export class PersonService
{
    private _personEndpoint: string = "persons";

    constructor(private httpClient : HttpClient) {}
  
    getPersons() : Observable<IPerson[]>
    {
        return this.httpClient.get<IPerson[]>(environment.personServiceUrl + this._personEndpoint)
        .pipe(
            tap(data => console.log('all', JSON.stringify(data))),
            catchError(this.handleError)
        );
    }

    getPerson(id: string): Observable<IPerson | undefined> {
        return this.httpClient.get<IPerson>(environment.personServiceUrl + this._personEndpoint + '/' + id)
          .pipe(
            tap(data => console.log('single', JSON.stringify(data))),
            catchError(this.handleError)
          );
      }

    addPerson(person: IPerson): Observable<string> {
        return this.httpClient.post<string>(environment.personServiceUrl + this._personEndpoint, person)
            .pipe(catchError(this.handleError));
      }

    private handleError(err : HttpErrorResponse)
    {
        let errorMessage = '';
        if(err.error instanceof ErrorEvent)
        {
            errorMessage = 'An error occured: ' + err.error.message;
        }
        else
        {
            errorMessage = 'Server returned code: ' + err.status + ', message is: ' + err.message;
        }
        console.error(errorMessage);

        return throwError(()=> errorMessage);
    }
}