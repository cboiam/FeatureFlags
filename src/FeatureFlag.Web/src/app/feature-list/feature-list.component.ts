import { Component, OnInit } from "@angular/core";
import { FeatureService } from "./feature.service";
import Feature from "./models/Feature";
import { faPlus } from "@fortawesome/free-solid-svg-icons";

@Component({
  selector: "app-feature-list",
  templateUrl: "./feature-list.component.html",
  styleUrls: ["./feature-list.component.css"]
})
export class FeatureListComponent implements OnInit {
  public faPlus = faPlus;
  public isFeatureFormVisible = false;

  public features: Array<Feature>;

  constructor(private service: FeatureService) {}

  addFeature = (feature: Feature) => {
    this.features.push(feature);
  };

  removeFeature = (featureId: number) => {
    const featureIndex = this.features.findIndex(f => f.id === featureId);
    this.features.splice(featureIndex, 1);
  };

  showFeatureForm = () => {
    this.isFeatureFormVisible = true;
  };

  closeFeatureForm = () => {
    this.isFeatureFormVisible = false;
  };

  ngOnInit() {
    this.service.getFeatures().subscribe(response => {
      this.features = response;
    });
  }
}
