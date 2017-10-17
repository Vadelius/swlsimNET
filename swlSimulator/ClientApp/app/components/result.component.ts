import {Component, OnInit} from "@angular/core";
import {ActivatedRoute} from "@angular/router";
import {ObservableService} from "./observable.service"
import {Observable} from "rxjs/Observable";
import {AbilityBuff, DistinctBuff, DistinctSpellCast, SpellBreakdownList, BuffBreakdownList, EnergyList, RootObject} from "./resultsinterface";
import { ChartsModule } from 'ng2-charts';

@Component({
    selector: "result",
    templateUrl: "./result.component.html"
})
export class ResultComponent implements OnInit {
    HighestDps: string;
    LowestDps: string;
    Dps: string;

    public pieChartLabels:string[] = ['Download Sales', 'In-Store Sales', 'Mail Sales'];
    public pieChartData:number[] = [300, 500, 100];
    public pieChartType:string = 'pie';
      // events
  public chartClicked(e:any):void {
    console.log(e);
    }
 
  public chartHovered(e:any):void {
    console.log(e);
    }
    // line chart

      // lineChart
  public lineChartData:Array<any> = [
    {data: [65, 59, 80, 81, 56, 55, 40], label: 'Series A'},
    {data: [28, 48, 40, 19, 86, 27, 90], label: 'Series B'},
    {data: [18, 48, 77, 9, 100, 27, 40], label: 'Series C'}
  ];
  public lineChartLabels:Array<any> = ['January', 'February', 'March', 'April', 'May', 'June', 'July'];
  public lineChartOptions:any = {
    responsive: true
  };
  public lineChartColors:Array<any> = [
    { // grey
      backgroundColor: 'rgba(148,159,177,0.2)',
      borderColor: 'rgba(148,159,177,1)',
      pointBackgroundColor: 'rgba(148,159,177,1)',
      pointBorderColor: '#fff',
      pointHoverBackgroundColor: '#fff',
      pointHoverBorderColor: 'rgba(148,159,177,0.8)'
    },
    { // dark grey
      backgroundColor: 'rgba(77,83,96,0.2)',
      borderColor: 'rgba(77,83,96,1)',
      pointBackgroundColor: 'rgba(77,83,96,1)',
      pointBorderColor: '#fff',
      pointHoverBackgroundColor: '#fff',
      pointHoverBorderColor: 'rgba(77,83,96,1)'
    },
    { // grey
      backgroundColor: 'rgba(148,159,177,0.2)',
      borderColor: 'rgba(148,159,177,1)',
      pointBackgroundColor: 'rgba(148,159,177,1)',
      pointBorderColor: '#fff',
      pointHoverBackgroundColor: '#fff',
      pointHoverBorderColor: 'rgba(148,159,177,0.8)'
    }
  ];
  public lineChartLegend:boolean = true;
  public lineChartType:string = 'line';
 
  public randomize():void {
    let _lineChartData:Array<any> = new Array(this.lineChartData.length);
    for (let i = 0; i < this.lineChartData.length; i++) {
      _lineChartData[i] = {data: new Array(this.lineChartData[i].data.length), label: this.lineChartData[i].label};
      for (let j = 0; j < this.lineChartData[i].data.length; j++) {
        _lineChartData[i].data[j] = Math.floor((Math.random() * 100) + 1);
      }
    }
    this.lineChartData = _lineChartData;
  }
 
  // events


    ngOnInit() { 
        
        var results: any = localStorage.getItem("Results");
        let jsonObj: any = JSON.parse(results); // string to generic object first
        let root: RootObject = <RootObject>jsonObj;
        this.Dps = root.totalDps.toFixed(0);
        this.LowestDps = root.lowestDps.toFixed(0);
        this.HighestDps = root.highestDps.toFixed(0);



           
}
}
