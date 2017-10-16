import {Component, OnInit} from "@angular/core";
import {ActivatedRoute} from "@angular/router";
import {ObservableService} from "./observable.service"
import {Observable} from "rxjs/Observable";
@Component({
    selector: "result",
    templateUrl: "./result.component.html"
})
export class ResultComponent implements OnInit {
    private data: ObservableService;

    public activities$: Observable<any>;
    
    constructor(private activityService: ObservableService) { }
    
    ngOnInit() {
        let results = localStorage.getItem("results");
        console.log(results);
        // this.activities$ = this.activityService.loadData();
        // var data = this.activities$
        // console.log(data);

        
}
}
