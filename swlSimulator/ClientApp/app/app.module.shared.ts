import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { CounterComponent } from './components/counter/counter.component';
import { ImportComponent } from './components/import/import.component';
import { SpellqueryComponent } from './components/spellquery/spellquery.component';
import { ResultComponent } from './components/result/result.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        CounterComponent,
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
            { path: 'counter', component: CounterComponent },
            { path: 'import', component: ImportComponent },
            { path: 'result', component: ResultComponent },
            { path: 'spellquery', component: SpellqueryComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ]
})
export class AppModuleShared {
}
