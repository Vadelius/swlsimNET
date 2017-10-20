import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { HttpModule } from "@angular/http";
import { RouterModule } from "@angular/router";
import { ChartsModule } from "ng2-charts";
import { AppComponent } from "./components/app/app.component";
import { HomeComponent } from "./components/home/home.component";
import { ImportComponent } from "./components/import/import.component";
import { NavMenuComponent } from "./components/navmenu/navmenu.component";
import { ResultComponent } from "./components/result/result.component";
import { SpellqueryComponent } from "./components/spellquery/spellquery.component";

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    ImportComponent,
    ResultComponent,
    SpellqueryComponent,
    HomeComponent,
  ],
  imports: [
    CommonModule,
    HttpModule,
    FormsModule,
    ChartsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: "", redirectTo: "home", pathMatch: "full" },
      { path: "home", component: HomeComponent },
      { path: "import", component: ImportComponent },
      { path: "result", component: ResultComponent },
      { path: "spellquery", component: SpellqueryComponent },
      { path: "**", redirectTo: "home" },
    ]),
  ],
  exports: [CommonModule, FormsModule, ReactiveFormsModule],
})
export class AppModuleShared {}
