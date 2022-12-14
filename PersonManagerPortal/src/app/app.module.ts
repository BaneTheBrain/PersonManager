import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { PersonListComponent } from './person-list/person-list.component';
import { WelcomeComponent } from './home/welcome/welcome.component';
import { PersonDetailsComponent } from './person-details/person-details.component';
import { PersonCreateComponent } from './person-create/person-create.component';
import { PersonDetailsGuard } from './services/person-details.guard';

@NgModule({
  declarations: [
    AppComponent,
    WelcomeComponent,
    PersonListComponent,
    PersonDetailsComponent,
    PersonCreateComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    RouterModule.forRoot([
      {
        path: 'persons/:id', component: PersonDetailsComponent,
        canActivate: [PersonDetailsGuard]
      },
      { path: 'createperson', component: PersonCreateComponent },
      { path: 'editperson/:id', component: PersonCreateComponent },
      { path: 'persons', component: PersonListComponent },
      { path: 'welcome', component: WelcomeComponent },
      { path: '', redirectTo: 'welcome', pathMatch: 'full' },
      { path: '**', redirectTo: 'welcome', pathMatch: 'full' }
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
