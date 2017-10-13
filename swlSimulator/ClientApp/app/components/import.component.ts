import { Component, OnInit } from '@angular/core';
import { FormGroup, FormArray, FormBuilder, Validators } from '@angular/forms';
import { Settings } from './settings.interface';

@Component({
    moduleId: "import",
    selector: 'import',
    templateUrl: 'import.component.html',
})
export class ImportComponent implements OnInit {

    orderForm: FormGroup;
    items: any[] = [];
    model: Settings;
    fb = FormBuilder;

    ngOnInit() {
        this.orderForm = this.fb.apply({
            item: "",
        });
    }
}