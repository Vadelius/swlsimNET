import { Component } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';

@Component({
    selector: 'import',
    templateUrl: 'import.component.html',
})
export class ImportComponent {

    primaryWeapon: string[] = ['Rifle', "Blade", "Blood", "Chaos", "Elemental", "Fist", "Hammer", "Pistol", "Shotgun"];
    primaryAffix: string[] = ["Havoc", "Destruction", "Effieciency", "Energy", "None"];
    secondaryWeapon: string[] = ["Rifle", "Blade", "Blood", "Chaos", "Elemental", "Fist", "Hammer", "Pistol", "Shotgun"];
    secondaryAffix: string[] = ["Havoc", "Destruction", "Effieciency", "Energy", "None"];
    primaryProc: string[]
    secondaryProc: string[]
    hammerProc: string[] = ["None", "Anima Touched", "Plasma Forged", "Shadowbound", "Pneumatic Maul", "Fuming Despoiler"]
    fistProc: string[] = ["None", "Anima Touched", "Plasma Forged", "Shadowbound", "Bladed Gauntlets", "Treshing Claws", "Blood Drinkers"]
    bladeProc: string[] = ["None", "Anima Touched", "Plasma Forged", "Shadowbound", "Apocalypse", "Razors Edge", "Blade Of The eventh Son", "Soulblade"]
    elementalProc: string[] = ["None", "Anima Touched", "Plasma Forged", "Shadowbound", "Unstable Electron Core", "Frozen Figurine", "Cryo Charged Conduit"]
    bloodProc: string[] = ["None", "Anima Touched", "Plasma Forged", "Shadowbound", "EldritchTome"]
    pistolProc: string[] = ["None", "Anima Touched", "Plasma Forged", "Shadowbound", "Misery & Malice", "Six Shooters", "Sov-Tech Harmonisers", "CB-3 Annihilators", "Heavy Caliber Pistols"]
    shotgunProc: string[] = ["None", "Anima Touched", "Plasma Forged", "Shadowbound", "Ifritan Despoiler", "SpesC221"]
    rifleProc: string[] = ["None", "Anima Touched", "Plasma Forged", "Shadowbound", "Infernal Loader", "KSR43"]
    chaosProc: string[] = ["None", "Anima Touched", "Plasma Forged", "Shadowbound", "Warped Visage", "Otherwordly Artifact", "Sov Tech Paradox Generator"]

    combatPower: 1300;
    criticalChance: 31;
    critaicalPower: 110;

    basicSignet: 110;
    powerSignet: 21;
    waistSignet: 52;
    luckSignet: 12;
    eliteSignet: 51;

    head: string[] = ["None", "Ashes"]
    neck: string[] = ["None", "SeedOfAgression", "ChokerOfSheedBlood", "EgonPendant"]
    luck: string[] = ["None", "ColdSilver", "GamblersSoul"]
    gadget: string[] = ["None", "ElectrograviticAttractor", "ShardOfSesshoSeki", "ValiMetabolic", "MnemonicGuardianWerewolf"]

    apl: string;
    passive1: string;
    passive2: string;
    passive3: string;
    passive4: string;
    passive5: string;

    exposed: true;
    openingShot: true;
    headCd: false;
    waistCdr: true;
    iterations: 10;
    fightLength: 240;
    target: "Regional";

    myform: FormGroup;

    ngOnInit() {
        this.myform = new FormGroup({
            primaryWeapon: new FormControl(),
            primaryAffix: new FormControl(),
            primaryProc: new FormControl(),
            secondaryWeapon: new FormControl(),
            secondaryAffix: new FormControl(),
            secondaryProc: new FormControl(),
            head: new FormControl(),
            neck: new FormControl(),
            luck: new FormControl(),
            combatPower: new FormControl(),
            criticalChance: new FormControl(),
            criticalPower: new FormControl(),
            basicSignet: new FormControl(),
            powerSignet: new FormControl(),
            eliteSignet: new FormControl(),
            luckSignet: new FormControl(),
            wristSignet: new FormControl(),
            gadget: new FormControl(),
            apl: new FormControl(),

        });

    }
}