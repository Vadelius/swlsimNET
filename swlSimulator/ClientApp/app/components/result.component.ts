import {Component, OnInit} from "@angular/core";
import {ActivatedRoute} from "@angular/router";
import {ObservableService} from "./observable.service"
import {Observable} from "rxjs/Observable";
import {RootObject} from "./resultsinterface";

@Component({
    selector: "result",
    templateUrl: "./result.component.html"
})
export class ResultComponent implements OnInit {

    
    public results = localStorage.getItem("Results");
    ngOnInit() {
    
       
     
}
}
