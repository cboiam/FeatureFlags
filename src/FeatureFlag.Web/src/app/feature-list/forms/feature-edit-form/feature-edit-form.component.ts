import { Component, OnInit, Input } from "@angular/core";
import { faTimes } from "@fortawesome/free-solid-svg-icons";
import Feature from "../../models/Feature";
import { FeatureService } from "../../feature.service";
import { ToastService } from "src/app/shared/toast/toast.service";
import { NgForm } from "@angular/forms";

@Component({
  selector: "app-feature-edit-form",
  templateUrl: "./feature-edit-form.component.html",
  styleUrls: ["./feature-edit-form.component.css"]
})
export class FeatureEditFormComponent implements OnInit {
  @Input() public feature: Feature;
  @Input() closeEditForm: () => void;
  @Input() editFeature: (feature: Feature) => void;
  updatingFeature: Feature;

  public faTimes = faTimes;

  constructor(
    private service: FeatureService,
    private toastService: ToastService
  ) {}

  onSubmit(form: NgForm) {
    if (!form.valid || this.updatingFeature.name === this.feature.name) {
      this.toastService.alert("Feature is not valid!");
      return;
    }

    this.service
      .editFeature(this.updatingFeature)
      .then(() => {
        this.editFeature(this.updatingFeature);
        this.closeEditForm();
      })
      .catch(() =>
        this.toastService.alert(
          `Error while updating feature ${this.feature.name}`
        )
      );
  }

  ngOnInit() {
    this.updatingFeature = { ...this.feature };
  }
}
