import {Component, OnInit} from "@angular/core";
import {ActivatedRoute} from "@angular/router";
import {ObservableService} from "./observable.service";
import {Observable} from "rxjs/Observable";
import {
    AbilityBuff,
    DistinctBuff,
    DistinctSpellCast,
    SpellBreakdownList,
    BuffBreakdownList,
    EnergyList,
    RootObject
} from "./resultsinterface";
import {ChartsModule} from "ng2-charts";
import {NgFor} from "@angular/common";
import { DecimalPipe } from '@angular/common';

@Component({
    selector: "result",
    templateUrl: "./result.component.html"
})
export class ResultComponent implements OnInit {
    breakdownlist: any;
    buffbreakdownlist: any;
    AvarageCrit: string;
    TotalCount: number;
    Raw: any;
    HighestDps: string;
    LowestDps: string;
    Dps: string;
    TotalDps: string;

    public primary: Array<any> = [];
    public secondary: Array<any> = [];
    public pgimmick: Array<any> = [];
    public sgimmick: Array<any> = [];
    public breakdownLabels: string[] = [];
    public breakdownData: number[] = [];
    public pieChartType: string = "pie";

    public chartClicked(e: any): void {}
    public chartHovered(e: any): void {}

    public energyChartData: Array<any> = [
        {data: this.primary, label: "Primary", borderColor: "#3e95cd"},
        {data: this.secondary, label: "Secondary", borderColor: "#c45850"}
    ];
    public gimmickChartData: Array<any> = [
        {data: this.pgimmick, label: "Primary", borderColor: "#3e95cd"},
        {data: this.sgimmick, label: "Secondary", borderColor: "#c45850"}
    ];

    public bgcolors: Array<any> = [
        "#c45850",
        "#3e95cd",
        "#c45850",
        "#3e95cd",
        "#c45850",
        "#90A4AE",
        "#B0BEC5",
        "#CFD8DC",
        "#ECEFF1"
    ];
    public lineChartLabels: Array<any> = [];
    public lineChartOptions: any = {
        responsive: true
    };

    public lineChartLegend: boolean = true;
    public lineChartType: string = "line";

    ngOnInit() {
        var results: any = localStorage.getItem("Results");
        let jsonObj: any = JSON.parse(results); // string to generic object first
        let root: RootObject = <RootObject>jsonObj;
        this.Dps = root.totalDps.toFixed(0);
        this.LowestDps = root.lowestDps.toFixed(0);
        this.HighestDps = root.highestDps.toFixed(0);
        this.TotalDps = root.totalDamage.toFixed(0);
        this.TotalCount = root.totalHits - root.totalCrits;
        this.AvarageCrit = (100 * (root.totalCrits / root.totalHits)).toFixed(2);

        this.Raw = root._oneBuilder.m_StringValue;
        this.breakdownlist = root.spellBreakdownList;
        this.buffbreakdownlist = root.buffBreakdownList;
        this.breakdownData = root.spellBreakdownList
            .filter(spell => spell.dpsPercentage > 0)
            .map(spell => spell.dpsPercentage);

        this.breakdownLabels = root.spellBreakdownList
            .filter(spell => spell.dpsPercentage > 0)
            .map(spell => spell.name);

        this.lineChartLabels = root.energyList.map(time => time.time);

        for (let pgimmick of root.energyList) {
            this.pgimmick.push(pgimmick.pgimmick);
        }
        for (let sgimmick of root.energyList) {
            this.sgimmick.push(sgimmick.sgimmick);
        }
        for (let primary of root.energyList) {
            this.primary.push(primary.primary);
        }
        for (let secondary of root.energyList) {
            this.secondary.push(secondary.secondary);
        }
    }
}
