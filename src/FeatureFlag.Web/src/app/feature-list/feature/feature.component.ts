import { Component, OnInit, Input } from '@angular/core';
import Feature from '../models/Feature';

@Component({
  selector: 'app-feature',
  templateUrl: './feature.component.html',
  styleUrls: ['./feature.component.css']
})
export class FeatureComponent implements OnInit {

  @Input() public feature: Feature;
  public isCollapsed = true;

  public enabled = (): boolean => this.feature.environments &&
    !this.feature.environments.some((e) => !e.enabled);

  constructor() { }

  ngOnInit() {
  }

}
