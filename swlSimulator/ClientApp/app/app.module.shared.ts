import {CommonModule} from "@angular/common";
import {NgModule} from "@angular/core";
import {HttpModule} from "@angular/http";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {RouterModule} from "@angular/router";
import {AppComponent} from "./components/app/app.component";
import {NavMenuComponent} from "./components/navmenu.component";
import {HomeComponent} from "./components/home.component";
import {ImportComponent} from "./components/import.component";
import {SpellqueryComponent} from "./components/spellquery.component";
import {ResultComponent} from "./components/result.component";
import {Configuration} from "./components/app/app.constants";
import {ResultResolver} from "./components/result.resolver";
import {ObservableService} from "./components/observable.service"

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        ImportComponent,
        ResultComponent,
        SpellqueryComponent,
        HomeComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule.forRoot([
            { path: "", redirectTo: "home", pathMatch: "full" },
            { path: "home", component: HomeComponent },
            { path: "import", component: ImportComponent },
            { path: "result", component: ResultComponent },
            //resolve: { data:  ResultResolver }},
            { path: "spellquery", component: SpellqueryComponent },
            { path: "**", redirectTo: "home" }
        ])
    ],
    exports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule
    ],
    providers: [
        ResultResolver,
        ObservableService,
        Configuration,
    ],
})
export class AppModuleShared {
}
