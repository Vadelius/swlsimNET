import { Component } from '@angular/core';

@Component({
    selector: 'import',
    templateUrl: './import.component.html'
})
export class ImportComponent {
    primaryWeapon = "";
    primaryProc = "";  
    primaryAffix = "";
    secondaryWeapon = "";
    secondaryyProc = "";
    secondaryAffix = "";
    head = "";
    neck = "";
    luck = "";
    gadget = "";
    basicSignet = 101;
    powerSignet = 18;
    eliteSignet = 31;
    luckSignet = 12;
    waistSignet = 52;
    combatPower = 1300;
    criticalChance = 31;
    criticalPower = 110;
}

