import {Component} from "@angular/core";
import {ChartsModule} from "ng2-charts";

@Component({
    selector: "home",
    templateUrl: "./home.component.html"
})

export class HomeComponent  {

    public barChartOptions:any = {
        scaleShowVerticalLines: false,
        responsive: true,
        scales: {
            yAxes: [{
                ticks: {
                    beginAtZero: true
                }
            }]
        }
    }      
      public barChartLabels:string[] = [
        "Rifle",
        "Blade",
        "Blood",
        "Chaos",
        "Elemental",
        "Fist",
        "Hammer",
        "Pistol",
        "Shotgun"
    ];
      public barChartType:string = 'bar';
      public barChartLegend:boolean = true;
     
      public barChartData:any[] = [
        {data: [12231, 10844, 14779, 11984, 12334, 9932, 14811, 13432, 12901], label: '~ 1000 IP over 240 seconds'}]

      // events
      public chartClicked(e:any):void {
        console.log(e);
      }
     
      public chartHovered(e:any):void {
        console.log(e);
      }
}
