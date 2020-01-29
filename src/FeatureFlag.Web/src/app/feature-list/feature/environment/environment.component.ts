import { Component, OnInit, Input } from '@angular/core';
import Environment from '../../models/Environment';
import { faUsers } from '@fortawesome/free-solid-svg-icons';
import { FeatureListService } from '../../feature-list.service';

@Component({
  selector: 'app-environment',
  templateUrl: './environment.component.html',
  styleUrls: ['./environment.component.css']
})
export class EnvironmentComponent implements OnInit {

  @Input() public environment: Environment;
  public faUsers = faUsers;
  public users = (): string => this.environment && this.environment.usersEnabled.map(u => u.name).join(", ");
  public hasUsers = (): boolean => this.environment && this.environment.usersEnabled && this.environment.usersEnabled.length > 0;
  public isUsersVisible = true;

  toggle = () => {
    this.service.toggle(this.environment.id).subscribe((response) => {
      this.environment.enabled = !this.environment.enabled;
    }, (error) => {
      // alertar
    });
  }

  constructor(private service: FeatureListService) {
  }

  ngOnInit() { }

}
