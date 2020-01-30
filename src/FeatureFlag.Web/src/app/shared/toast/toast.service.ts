import { Injectable, Output, EventEmitter } from "@angular/core";

@Injectable({
  providedIn: "root"
})
export class ToastService {
  @Output() show: EventEmitter<string> = new EventEmitter();

  public alert(message: string) {
    this.show.emit(message);
  }

  constructor() {}
}
