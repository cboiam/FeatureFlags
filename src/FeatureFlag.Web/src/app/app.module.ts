import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";
import { HttpClientModule } from "@angular/common/http";
import { FontAwesomeModule } from "@fortawesome/angular-fontawesome";

import { AppComponent } from "./app.component";
import { FeatureListComponent } from "./feature-list/feature-list.component";
import { FeatureComponent } from "./feature-list/feature/feature.component";
import { EnvironmentComponent } from "./feature-list/feature/environment/environment.component";
import { FeatureAddFormComponent } from "./feature-list/forms/feature-add-form/feature-add-form.component";
import { FormsModule } from "@angular/forms";
import { EnvironmentAddFormComponent } from "./feature-list/forms/environment-add-form/environment-add-form.component";
import { SharedModule } from "./shared/shared.module";
import { FeatureEditFormComponent } from './feature-list/forms/feature-edit-form/feature-edit-form.component';
import { EnvironmentEditFormComponent } from './feature-list/forms/environment-edit-form/environment-edit-form.component';

@NgModule({
  declarations: [
    AppComponent,
    FeatureListComponent,
    FeatureComponent,
    EnvironmentComponent,
    FeatureAddFormComponent,
    EnvironmentAddFormComponent,
    FeatureEditFormComponent,
    EnvironmentEditFormComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    NgbModule,
    FontAwesomeModule,
    SharedModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {}
