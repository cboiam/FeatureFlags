import { Component, OnInit } from "@angular/core";
import { isNullOrUndefined } from "util";
import { ToastService } from "./toast.service";

const alertMessageTimeout = 2500;

@Component({
  selector: "app-toast",
  templateUrl: "./toast.component.html",
  styleUrls: ["./toast.component.css"]
})
export class ToastComponent implements OnInit {
  public messages = new Array<string>();

  public showAlert = (message: string) => {
    if (isNullOrUndefined(message) && message === "") {
      return;
    }

    this.messages.push(message);

    setTimeout(() => this.removeMessage(message), alertMessageTimeout);
  };

  private removeMessage(message: string) {
    const messageIndex = this.messages.findIndex(m => m == message);
    this.messages.splice(messageIndex, 1);
  }

  public hasMessages(): boolean {
    return !isNullOrUndefined(this.messages) && this.messages.length > 0;
  }

  constructor(private service: ToastService) {}

  ngOnInit() {
    this.service.show.subscribe((message: string) => this.showAlert(message));
  }
}
