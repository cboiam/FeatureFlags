import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HttpClientModule } from '@angular/common/http';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

import { AppComponent } from './app.component';
import { FeatureListComponent } from './feature-list/feature-list.component';
import { FeatureComponent } from './feature-list/feature/feature.component';
import { EnvironmentComponent } from './feature-list/feature/environment/environment.component';
import { FeatureFormComponent } from './feature-list/feature-form/feature-form.component';
import { FormsModule } from '@angular/forms';
import { EnvironmentFormComponent } from './feature-list/environment-form/environment-form.component';

@NgModule({
  declarations: [
    AppComponent,
    FeatureListComponent,
    FeatureComponent,
    EnvironmentComponent,
    FeatureFormComponent,
    EnvironmentFormComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    NgbModule,
    FontAwesomeModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
