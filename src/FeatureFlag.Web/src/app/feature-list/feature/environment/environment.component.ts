import { Component, OnInit, Input } from "@angular/core";
import Environment from "../../models/Environment";
import { faUsers, faTrashAlt, faEdit } from "@fortawesome/free-solid-svg-icons";
import { FeatureService } from "../../feature.service";
import { ToastService } from "src/app/shared/toast/toast.service";

@Component({
  selector: "app-environment",
  templateUrl: "./environment.component.html",
  styleUrls: ["./environment.component.css"]
})
export class EnvironmentComponent implements OnInit {
  @Input() public environment: Environment;
  @Input() public removeEnvironmentFromList: (environmentId: number) => void;
  public faUsers = faUsers;
  public faTrashAlt = faTrashAlt;
  public faEdit = faEdit;

  public users = (): string =>
    this.environment &&
    this.environment.usersEnabled.map(u => u.name).join(", ");
  public hasUsers = (): boolean =>
    this.environment &&
    this.environment.usersEnabled &&
    this.environment.usersEnabled.length > 0;
  public isUsersVisible = true;

  toggle = () => {
    this.service
      .toggle(this.environment.id)
      .then(() => {
        this.environment.enabled = !this.environment.enabled;
      })
      .catch(error => {
        this.toastService.alert(
          `Error on environment ${this.environment.name} toggle`
        );
      });
  };

  removeEnvironment() {
    this.service
      .removeEnvironment(this.environment.id)
      .then(() => {
        this.removeEnvironmentFromList(this.environment.id);
      })
      .catch(() => {
        this.toastService.alert(
          `Error while removing environment ${this.environment.name}`
        );
      });
  }

  constructor(
    private service: FeatureService,
    private toastService: ToastService
  ) {}

  ngOnInit() {}
}
