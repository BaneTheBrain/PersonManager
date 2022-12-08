import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, catchError, tap, throwError, map } from "rxjs";
import { environment } from "src/environments/environment";
import { PersonResponse } from "../model/read/person";
import { SocialMediaAccountResponse } from "../model/read/socialmediaaccount";
import { PersonRequest } from "../model/write/person";

@Injectable(
    {
        providedIn: 'root'
    }
)
export class PersonManagerService {
    private _personEndpoint: string = "persons";
    private _socialMediaAccountEndpoint: string = "socialmediaaccounts";

    constructor(private httpClient: HttpClient) { }

    getPersons(): Observable<PersonResponse[]> {
        return this.httpClient.get<PersonResponse[]>(environment.personManagerServiceUrl + this._personEndpoint)
            .pipe(
                tap(data => console.log('all', JSON.stringify(data))),
                catchError(this.handleError)
            );
    }

    getPerson(id: string): Observable<PersonResponse> {
        return this.httpClient.get<PersonResponse>(environment.personManagerServiceUrl + this._personEndpoint + '/' + id)
            .pipe(
                tap(data => console.log('single', JSON.stringify(data))),
                catchError(this.handleError)
            );
    }

    addPerson(person: PersonRequest): Observable<string> {
        return this.httpClient.post<string>(environment.personManagerServiceUrl + this._personEndpoint, person)
            .pipe(catchError(this.handleError));
    }

    updatePerson(person: PersonRequest): Observable<string> {
        return this.httpClient.put<string>(environment.personManagerServiceUrl + this._personEndpoint, person)
            .pipe(catchError(this.handleError));
    }

    getSocialMediaAccounts(): Observable<SocialMediaAccountResponse[]> {
        return this.httpClient.get<SocialMediaAccountResponse[]>(environment.personManagerServiceUrl + this._socialMediaAccountEndpoint)
            .pipe(
                tap(data => console.log('all', JSON.stringify(data))),
                catchError(this.handleError)
            );
    }

    private handleError(err: HttpErrorResponse) {
        let errorMessage = '';
        if (err.error instanceof ErrorEvent) {
            errorMessage = 'An error occured: ' + err.error.message;
        }
        else {
            errorMessage = 'Server returned code: ' + err.status + ', message is: ' + err.message;
        }
        console.error(errorMessage);

        return throwError(() => errorMessage);
    }
}