import { Component, OnInit } from '@angular/core';
import { FeatureListService } from './feature-list.service';
import Feature from './models/Feature';
import { faPlus } from '@fortawesome/free-solid-svg-icons';
const alertMessageTimeout = 4000;


@Component({
  selector: 'app-feature-list',
  templateUrl: './feature-list.component.html',
  styleUrls: ['./feature-list.component.css']
})

export class FeatureListComponent implements OnInit {
  public faPlus = faPlus;
  public isFeatureFormVisible = false;

  public isAlertVisible = false;
  public alertMessage = "";

  public features: Array<Feature>;

  constructor(private service: FeatureListService) {
  }

  showAlert = (message: string) => {
    this.alertMessage = message;
    this.isAlertVisible = this.alertMessage !== null && this.alertMessage !== undefined && this.alertMessage !== "";

    setTimeout(() => this.isAlertVisible = false, alertMessageTimeout);
  }

  addFeature = (feature: Feature) => {
    this.features.push(feature);
  }

  showFeatureForm = () => {
    this.isFeatureFormVisible = true;
  }

  closeFeatureForm = () => {
    this.isFeatureFormVisible = false;
  }

  ngOnInit() {
    this.service.getFeatures().subscribe((response) => {
      this.features = response;
    });
  }

}
