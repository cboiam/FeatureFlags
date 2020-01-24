import { Component, OnInit, Input } from '@angular/core';
import Environment from '../../models/Environment';
import { environment } from '../../../../environments/environment';

@Component({
  selector: 'app-environment',
  templateUrl: './environment.component.html',
  styleUrls: ['./environment.component.css']
})
export class EnvironmentComponent implements OnInit {

  @Input() public environment: Environment;
  public isCollapsed = true;
  public users = (): string => this.environment && this.environment.usersEnabled.map(u => u.name).join(", ");

  constructor() { }

  ngOnInit() { }

}
