import { Component, OnInit, Input } from "@angular/core";
import { NgForm } from '@angular/forms';
import { faTimes } from '@fortawesome/free-solid-svg-icons';
import Environment from '../../models/Environment';
import User from '../../models/User';
import { FeatureService } from '../../feature.service';
import { ToastService } from 'src/app/shared/toast/toast.service';

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

  constructor(private service: FeatureService, private toastService: ToastService) { }

  ngOnInit() {
    this.environment.featureId = this.featureId;
  }

  onSubmit(form: NgForm) {
    if (!form.valid) {
      this.toastService.alert("Environment is not valid");
      return;
    }

    var userNames = this.userNames.split(",")
      .filter(u => u !== "")
      .map(u => {
        var user = new User();
        user.name = u.trim();

        return user;
      });

    this.environment.usersEnabled = userNames;

    this.service.addEnvironment(this.environment).subscribe(response => {
      this.addEnvironment(response.body);
    }, error => this.toastService.alert("Error while adding environment"));
  }

  closeForm() {
    this.closeAddEnvironmentForm();
  }
}
