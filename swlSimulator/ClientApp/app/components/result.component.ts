import {Component, OnInit} from "@angular/core";
import {ActivatedRoute} from "@angular/router";
import {ObservableService} from "./observable.service"
import {Observable} from "rxjs/Observable";
import {AbilityBuff, DistinctBuff, DistinctSpellCast, SpellBreakdownList, BuffBreakdownList, EnergyList, RootObject} from "./resultsinterface";

@Component({
    selector: "result",
    templateUrl: "./result.component.html"
})
export class ResultComponent implements OnInit {
    HighestDps: string;
    LowestDps: string;
    Dps: string;

    ngOnInit() { 
        
        var results: any = localStorage.getItem("Results");
        let jsonObj: any = JSON.parse(results); // string to generic object first
        let root: RootObject = <RootObject>jsonObj;
        this.Dps = root.totalDps.toFixed(0);
        this.LowestDps = root.lowestDps.toFixed(0);
        this.HighestDps = root.highestDps.toFixed(0);
}
}
