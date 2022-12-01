import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
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
    HttpClientModule,
    RouterModule.forRoot([
      {
        path: 'persondetails/:id', component: PersonDetailsComponent,
        canActivate: [PersonDetailsGuard]
      },
      { path: 'createperson', component: PersonCreateComponent },
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
