import { CommonModule } from '@angular/common';
import { Component, NgModule, VERSION } from '@angular/core'
import { BrowserModule } from '@angular/platform-browser'
import { HttpModule, Http } from '@angular/http';
import { ReactiveFormsModule, FormGroup, FormArray, FormBuilder, Validators } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu.component';
import { HomeComponent } from './components/home.component';
import { ImportComponent } from './components/import.component';
import { SpellqueryComponent } from './components/spellquery.component';
import { ResultComponent } from './components/result.component';
import { Configuration } from './components/app/app.constants'
import { FormsModule } from '@angular/forms';


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
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'import', component: ImportComponent },
            { path: 'result', component: ResultComponent },
            { path: 'spellquery', component: SpellqueryComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ],
    exports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule
    ],
    providers: [
        Configuration,
    ],
})
export class AppModuleShared {
}
