import { Component, OnInit } from '@angular/core';
import {
    FormGroup, FormBuilder } from '@angular/forms';

@Component({
    selector: 'import',
    templateUrl: './import.component.html',

})
   
export class ImportComponent implements OnInit {
    myform: FormGroup;

    
    constructor(fb: FormBuilder) {
        this.myform = fb.group({
            "val": [""]
            //"primaryAffix": [""],
            //"primaryProc": [""],
            //"secondaryWeapon": [""],
            //"secondaryAffix": [""],
            //"secondaryProc": [""],
            //"combatPower": [""],
            //"criticalChance": [""],
            //"criticalPower": [""],


        });
    }

    ngOnInit() {
    }

    onSubmit(value: string): void {
        console.log('you submitted value: ', value);
    }

}
