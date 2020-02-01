import { Component, OnInit, Input } from "@angular/core";
import { faTimes } from "@fortawesome/free-solid-svg-icons";
import Environment from "../../models/Environment";
import { NgForm } from "@angular/forms";
import { FeatureService } from "../../feature.service";
import { ToastService } from "src/app/shared/toast/toast.service";
import User from "../../models/User";

@Component({
  selector: "app-environment-edit-form",
  templateUrl: "./environment-edit-form.component.html",
  styleUrls: ["./environment-edit-form.component.css"]
})
export class EnvironmentEditFormComponent implements OnInit {
  @Input() featureId: number;
  @Input() private environment: Environment;
  @Input() private closeEditForm: () => void;
  @Input() private editEnvironment: (environment: Environment) => void;
  public updatingEnvironment = new Environment();
  public faTimes = faTimes;
  public userNames = "";

  constructor(
    private service: FeatureService,
    private toastService: ToastService
  ) {}

  ngOnInit() {
    this.updatingEnvironment = { ...this.environment };
    this.userNames = this.environment.usersEnabled.map(u => u.name).join(", ");
  }

  onSubmit(form: NgForm) {
    if (!form.valid) {
      this.toastService.alert("Environment is not valid");
      return;
    }

    this.updatingEnvironment.usersEnabled = this.userNames
      .split(",")
      .map(u => {
        var user = new User();
        user.name = u.trim();

        return user;
      })
      .filter(u => u.name !== "");

    this.service
      .editEnvironment(this.featureId, this.updatingEnvironment)
      .then(() => {
        this.editEnvironment(this.updatingEnvironment);
        this.closeEditForm();
      })
      .catch(() => this.toastService.alert("Error while adding environment"));
  }
}
