import { Component } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';

@Component({
    selector: 'import',
    templateUrl: 'import.component.html',
})
export class ImportComponent {

    primaryWeapon: string[] = ['Rifle' , "Blade", "Blood", "Chaos", "Elemental", "Fist", "Hammer", "Pistol", "Shotgun"];
    primaryAffix: string[] = ["Havoc", "Destruction", "Effieciency", "Energy", "None"];
    secondaryWeapon: string[] = ["Rifle", "Blade", "Blood", "Chaos", "Elemental", "Fist", "Hammer", "Pistol", "Shotgun"];
    secondaryAffix: string[] = ["Havoc", "Destruction", "Effieciency", "Energy", "None"];

    
    hammerProc: string[] = ["None", "Anima Touched", "Plasma Forged", "Shadowbound", "Pneumatic Maul", "Fuming Despoiler"]
    fistProc: string[] = ["None", "Anima Touched", "Plasma Forged", "Shadowbound", "Bladed Gauntlets", "Treshing Claws", "Blood Drinkers"]
    bladeProc: string[] = [""]
    elementalProc: string[] = [""]
    bloodProc: string[] = [""]
    pistolProc: string[] = [""]
    shotgunProc: string[] = [""]
    rifleProc: string[] = [""]
    chaosProc: string[] = [""]

    // None, AnimaTouched, FlameWreathed, PlasmaForged, Shadowbound,
    // EldritchTome,
    // MiseryAndMalice, SixShooters, SovTechHarmonisers, Cb3Annihilators, HeavyCaliberPistols,
    // InfernalLoader, Ksr43,
    // Apocalypse, RazorsEdge, BladeOfTheSeventhSon, Soulblade,
    // IfritanDespoiler, Spesc221,
    // UnstableElectronCore, FrozenFigurine, CryoChargedConduit,
    // WarpedVisage, OtherworldlyArtifact, SovTechParadoxGenerator

    combatPower: 1300;
    criticalChance: 31;
    critaicalPower: 110;

    basicSignet: 110;
    powerSignet: 21;
    waistSignet: 52;
    luckSignet: 12;
    eliteSignet: 51;

    head: string [] = ["None", "Ashes"]
    neck: string[] = ["None", "SeedOfAgression", "ChokerOfSheedBlood", "EgonPendant"]
    luck: string [] = ["None", "ColdSilver", "GamblersSoul"]
    gadget: string [] = ["None", "ElectrograviticAttractor", "ShardOfSesshoSeki", "ValiMetabolic", "MnemonicGuardianWerewolf"]

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