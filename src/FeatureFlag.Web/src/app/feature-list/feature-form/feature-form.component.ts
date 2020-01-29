import { Component, OnInit, Input } from '@angular/core';
import Feature from '../models/Feature';
import { NgForm } from '@angular/forms';
import { faPlus, faTimes } from '@fortawesome/free-solid-svg-icons';
import Environment from '../models/Environment';
import { FeatureListService } from '../feature-list.service';

@Component({
  selector: 'app-feature-form',
  templateUrl: './feature-form.component.html',
  styleUrls: ['./feature-form.component.css']
})

export class FeatureFormComponent implements OnInit {
  public faPlus = faPlus;
  public faTimes = faTimes;
  public feature = new Feature();
  @Input() closeFeatureForm: () => void;
  @Input() showAlert: (message: string) => void;
  @Input() addFeature: (feature: Feature) => void;

  constructor(private service: FeatureListService) {
  }

  ngOnInit() {
  }

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

  onSubmit = (form: NgForm) => {
    if (form.valid) {
      this.service.addFeature(this.feature).subscribe((response) => {
        this.addFeature(response.body);
        this.closeForm();
      }, (error) => {
        this.showAlert("Failed to add new feature!");
      });
      return;
    }
    this.showAlert("Feature is invalid!");
  }
}
