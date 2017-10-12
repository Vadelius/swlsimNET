import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { NgModule } from '@angular/core';

@Component({
    selector: 'import',
    templateUrl: './import.component.html',

})

@NgModule({
    imports: [FormBuilder, FormGroup],
})
export class ImportComponent implements OnInit {
    public initialState: any;
    public areasForm: FormGroup;

    constructor(private fb: FormBuilder) { }

    area(): any {
        return this.fb.group({
            primaryWeapon: [""],
            primaryProc: [""],
            primaryAffix: [""],
            secondaryWeapon: [""],
            secondaryProc: [""],
            secondaryAffix: [""]
        });
    }

    ngOnInit(): void {
        this.areasForm = this.fb.group({
            primaryWeapon: [""],
            primaryAffix: [""],
            primaryProc: [""],
            secondaryWeapon: [""],
            secondaryProc: [""],
            secondaryAffix: [""],
            areas: this.fb.array([this.area()])
        });

        this.areasForm.valueChanges.subscribe(data => {
            console.log(this.areasForm.controls.primaryWeapon);
        });

    }
}



