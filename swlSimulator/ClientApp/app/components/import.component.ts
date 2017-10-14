import { Component } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { Settings } from './settings.interface';

@Component({
    selector: 'import',
    templateUrl: 'import.component.html',
})
export class ImportComponent {


    settings = Settings;
    myform: FormGroup;

    ngOnInit() {
        this.myform = new FormGroup({
            primaryWeapon: new FormControl(),
            primaryAffix: new FormControl(),
            primaryProc: new FormControl(),
            secondaryWeapon: new FormControl(),
            secondaryAffix: new FormControl(),
            secondaryProc: new FormControl(),
            combatPower: new FormControl( [
                    Validators.required
            ]),
            criticalChance: new FormControl([
                Validators.required
            ]),
            criticalPower: new FormControl([
                Validators.required
            ]),
        });
    }
}