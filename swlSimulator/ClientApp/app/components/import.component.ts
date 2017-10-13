import { Component } from '@angular/core';
import { FormGroup, FormBuilder, FormArray, FormControl, Validators } from '@angular/forms';
import { Hero } from './settings.interface';

@Component({
    selector: 'import',
    templateUrl: 'import.component.html',
})
export class ImportComponent {

    powers = ['Shitforms',
        'kms', 'plzstop'];

    model = new Hero(18, 'Dr Angular', this.powers[0], 'Testskit');

    submitted = false;

    onSubmit() { this.submitted = true; }

    newHero() {
        this.model = new Hero(42, '', '');
    }
}