import { Component, OnInit, Input } from "@angular/core";
import Feature from "../../models/Feature";
import { NgForm } from "@angular/forms";
import { faPlus, faTimes } from "@fortawesome/free-solid-svg-icons";
import Environment from "../../models/Environment";
import { FeatureService } from "../../feature.service";
import { ToastService } from "src/app/shared/toast/toast.service";

@Component({
  selector: "app-feature-add-form",
  templateUrl: "./feature-add-form.component.html",
  styleUrls: ["./feature-add-form.component.css"]
})
export class FeatureAddFormComponent implements OnInit {
  public faPlus = faPlus;
  public faTimes = faTimes;
  public feature = new Feature();
  @Input() closeFeatureForm: () => void;
  @Input() addFeature: (feature: Feature) => void;

  constructor(
    private service: FeatureService,
    private toastService: ToastService
  ) {}

  ngOnInit() {}

  onSubmit = (form: NgForm) => {
    if (!form.valid) {
      this.toastService.alert("Feature is not valid!");
      return;
    }

    this.service.addFeature(this.feature).subscribe(
      response => {
        this.addFeature(response.body);
        this.closeForm();
      },
      error => {
        this.toastService.alert("Failed to add new feature!");
      }
    );
  };

  addEnvironment() {
    this.feature.environments.push(new Environment());
  }

  removeEnvironment(index: number) {
    this.feature.environments.splice(index, 1);
  }

  closeForm() {
    this.feature = new Feature();
    this.closeFeatureForm();
  }
}
