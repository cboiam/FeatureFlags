import { Component, OnInit, Input } from "@angular/core";
import { NgForm } from "@angular/forms";
import { faTimes } from "@fortawesome/free-solid-svg-icons";
import Environment from "../../models/Environment";
import User from "../../models/User";
import { FeatureService } from "../../feature.service";
import { ToastService } from "src/app/shared/toast/toast.service";

@Component({
  selector: "app-environment-add-form",
  templateUrl: "./environment-add-form.component.html",
  styleUrls: ["./environment-add-form.component.css"]
})
export class EnvironmentAddFormComponent implements OnInit {
  @Input() featureId: number;
  @Input() closeAddEnvironmentForm: () => void;
  @Input() addEnvironment: (environment: Environment) => void;
  environment = new Environment();
  userNames = "";
  faTimes = faTimes;

  constructor(
    private service: FeatureService,
    private toastService: ToastService
  ) {}

  ngOnInit() {}

  onSubmit(form: NgForm) {
    if (!form.valid) {
      this.toastService.alert("Environment is not valid");
      return;
    }

    this.environment.usersEnabled = this.userNames
      .split(",")
      .map(u => {
        var user = new User();
        user.name = u.trim();

        return user;
      })
      .filter(u => u.name !== "");

    this.service.addEnvironment(this.featureId, this.environment).subscribe(
      response => {
        this.addEnvironment(response.body);
        this.closeAddEnvironmentForm();
      },
      error => this.toastService.alert("Error while adding environment")
    );
  }
}
