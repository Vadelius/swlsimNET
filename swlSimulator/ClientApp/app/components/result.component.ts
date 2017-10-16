import {Component, OnInit} from "@angular/core";
import {ActivatedRoute} from "@angular/router";

@Component({
    selector: "result",
    templateUrl: "./result.component.html"
})
export class ResultComponent implements OnInit {
    data: any;
    constructor(private route:ActivatedRoute) {}
        ngOnInit(): any {
            this.route.data.subscribe((data: any ) => {
                this.data = data;
            });
        }
}

