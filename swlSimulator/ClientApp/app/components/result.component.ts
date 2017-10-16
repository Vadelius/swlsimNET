import {Component} from "@angular/core";
import {Data} from "./data";
import {ResultService} from "./resultservice";

@Component({
    selector: "result",
    templateUrl: "./result.component.html"
})
export class ResultComponent {
    data:any;

    constructor(dataService:ResultService) {
      dataService.getData();

    }
  }

