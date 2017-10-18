import { Component, OnInit } from "@angular/core";
import { IRootObject as RootObject } from "./interfaces";

@Component({
  selector: "result",
  templateUrl: "./result.component.html",
})
export class ResultComponent implements OnInit {
  spellBreakdownList: any;
  buffBreakdownList: any;
  averageCrit: string;
  totalSpellExecutes: string;
  raw: any;
  highestDps: string;
  lowestDps: string;
  dps: string;
  totalDps: string;

  primaryEnergy: any[] = [];
  secondaryEnergy: any[] = [];
  primaryGimmick: any[] = [];
  secondaryGimmick: any[] = [];
  pieBreakdownLabels: string[] = [];
  pieBreakdownData: number[] = [];
  pieChartType: string = "pie";

  pieBgcolors: any[] = [
    {
      backgroundColor: [
        "#c44224",
        "#3e95cd",
        "#c45850",
        "#3e95cd",
        "#c45850",
        "#90A4AE",
        "#B0BEC5",
        "#CFD8DC",
        "#ECEFF1",
      ],
    },
  ];

  energyChartData: any[] = [
    { data: this.primaryEnergy, label: "Primary", borderColor: "#3e95cd" },
    { data: this.secondaryEnergy, label: "Secondary", borderColor: "#c45850" },
  ];

  gimmickChartData: any[] = [
    { data: this.primaryGimmick, label: "Primary", borderColor: "#3e95cd" },
    { data: this.secondaryGimmick, label: "Secondary", borderColor: "#c45850" },
  ];

  lineChartLabels: any[] = [];
  lineChartOptions: any = {
    responsive: true,
  };

  lineChartLegend: boolean = true;
  lineChartType: string = "line";

  chartClicked(e: any): void {}
  chartHovered(e: any): void {}

  ngOnInit() {
    const results: any = localStorage.getItem("Results");
    const jsonObj: any = JSON.parse(results); // string to generic object first
    const root = jsonObj as RootObject;
    this.dps = root.totalDps.toFixed(2);
    this.lowestDps = root.lowestDps.toFixed(0);
    this.highestDps = root.highestDps.toFixed(0);
    this.totalDps = root.totalDamage.toFixed(0);
    this.averageCrit = (100 * (root.totalCrits / root.totalHits)).toFixed(2);
    this.totalSpellExecutes = root.totalSpellExecutes.toFixed(2);

    this.raw = root.fightDebug;
    this.spellBreakdownList = root.spellBreakdownList;
    this.buffBreakdownList = root.buffBreakdownList;

    this.pieBreakdownData = root.spellBreakdownList
      .filter(spell => spell.dpsPercent > 0)
      .map(spell => spell.dpsPercent);

    this.pieBreakdownLabels = root.spellBreakdownList
      .filter(spell => spell.dpsPercent > 0)
      .map(spell => spell.name);

    this.lineChartLabels = root.energyList.map(time => time.time);

    for (const pEnergy of root.energyList) {
      this.primaryEnergy.push(pEnergy.primaryEnergy);
    }
    for (const sEnergy of root.energyList) {
      this.secondaryEnergy.push(sEnergy.secondaryEnergy);
    }

    for (const pGimmick of root.gimmickList) {
      this.primaryGimmick.push(pGimmick.primaryGimmick);
    }
    for (const sGimmick of root.gimmickList) {
      this.secondaryGimmick.push(sGimmick.secondaryGimmick);
    }
  }
}
