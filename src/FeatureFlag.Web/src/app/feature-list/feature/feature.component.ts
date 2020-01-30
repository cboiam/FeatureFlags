import { Component, OnInit, Input } from "@angular/core";
import Feature from "../models/Feature";
import { faTrashAlt, faEdit, faPlus } from "@fortawesome/free-solid-svg-icons";
import { FeatureService } from "../feature.service";
import { ToastService } from "src/app/shared/toast/toast.service";
import { isNullOrUndefined } from "util";
import Environment from '../models/Environment';

@Component({
  selector: "app-feature",
  templateUrl: "./feature.component.html",
  styleUrls: ["./feature.component.css"]
})
export class FeatureComponent implements OnInit {
  @Input() public feature: Feature;
  @Input() public removeFeatureFromList: (featureId: number) => void;
  public isEnvironmentsVisible = true;
  isEditFormVisible = false;
  isAddEnvironmentFormVisible = false;

  public faTrashAlt = faTrashAlt;
  public faEdit = faEdit;
  public faPlus = faPlus;

  public enabled = (): boolean => {
    return (
      !isNullOrUndefined(this.feature.environments) &&
      this.feature.environments.length > 0 &&
      !this.feature.environments.some(e => !e.enabled)
    );
  };

  removeFeature() {
    this.service
      .removeFeature(this.feature.id)
      .then(() => {
        this.removeFeatureFromList(this.feature.id);
      })
      .catch(() => {
        this.toastService.alert(
          `Error while removing feature ${this.feature.name}`
        );
      });
  }

  public editFeature = (feature: Feature) => {
    this.feature = feature;
  };

  showEditForm() {
    this.isEditFormVisible = true;
  }

  public closeEditForm = () => {
    this.isEditFormVisible = false;
  };

  showAddEnvironmentForm() {
    this.isAddEnvironmentFormVisible = true;
  }

  public closeAddEnvironmentForm = () => {
    this.isAddEnvironmentFormVisible = false;
  };

  public addEnvironment = (environment: Environment) => {
    this.feature.environments.push(environment);
  }

  removeEnvironment = (environmentId: number) => {
    const environmentIndex = this.feature.environments.findIndex(
      e => e.id === environmentId
    );
    this.feature.environments.splice(environmentIndex, 1);
  };

  constructor(
    private service: FeatureService,
    private toastService: ToastService
  ) { }

  ngOnInit() { }
}
