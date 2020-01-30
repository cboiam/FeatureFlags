import { Component, OnInit, Input } from "@angular/core";
import { NgForm } from '@angular/forms';
import { faTimes } from '@fortawesome/free-solid-svg-icons';
import Environment from '../../models/Environment';

@Component({
  selector: "app-environment-add-form",
  templateUrl: "./environment-add-form.component.html",
  styleUrls: ["./environment-add-form.component.css"]
})
export class EnvironmentAddFormComponent implements OnInit {
  environment = new Environment();
  userNames: string;
  faTimes = faTimes;

  constructor() {}

  ngOnInit() {}

  onSubmit(form: NgForm){
    console.log(form.value);
  }

  closeForm(){
    console.log("close");
  }
}
