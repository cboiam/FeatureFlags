import { Component, OnInit } from '@angular/core';
import { FeatureListService } from './feature-list.service';
import Feature from './models/Feature';

@Component({
  selector: 'app-feature-list',
  templateUrl: './feature-list.component.html',
  styleUrls: ['./feature-list.component.css']
})
export class FeatureListComponent implements OnInit {

  private service: FeatureListService;
  public features: Array<Feature>;

  constructor(service: FeatureListService) {
    this.service = service;
  }

  ngOnInit() {
    this.service.getFeatures().subscribe((response) => {
      this.features = response;
    });
  }

}
