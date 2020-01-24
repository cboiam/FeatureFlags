import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { FeatureListComponent } from './feature-list/feature-list.component';
import { FeatureComponent } from './feature-list/feature/feature.component';
import { EnvironmentComponent } from './feature-list/feature/environment/environment.component';

@NgModule({
  declarations: [
    AppComponent,
    FeatureListComponent,
    FeatureComponent,
    EnvironmentComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    NgbModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
