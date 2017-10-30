import { Component } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { Http, Response } from "@angular/http";
import { Router } from "@angular/router";
import { Observable } from "rxjs/Observable";
import weaponPresets from "../../presets/weaponPresets";
import { IFormNames as FormNames } from "../interfaces";

@Component({
  selector: "import",
  templateUrl: "./import.component.html",
})
export class ImportComponent {
  primaryWeapon: FormNames[] = [
    { name: "Blade", displayName: "Blade" },
    { name: "Hammer", displayName: "Hammer" },
    { name: "Fist", displayName: "Fist" },
    { name: "Chaos", displayName: "Chaos" },
    { name: "Blood", displayName: "Blood" },
    { name: "Elemental", displayName: "Elemental" },
    { name: "Pistol", displayName: "Pistols" },
    { name: "Shotgun", displayName: "Shotgun" },
    { name: "Rifle", displayName: "Rifle" },
  ];

  primaryAffix: FormNames[] = [
    { name: "none", displayName: "None" },
    { name: "Havoc", displayName: "Havoc" },
    { name: "Destruction", displayName: "Destruction" },
    { name: "Efficiency", displayName: "Efficiency" },
    { name: "Energy", displayName: "Energy" },
  ];

  secondaryWeapon: FormNames[] = [
    { name: "Blade", displayName: "Blade" },
    { name: "Hammer", displayName: "Hammer" },
    { name: "Fist", displayName: "Fist" },
    { name: "Chaos", displayName: "Chaos" },
    { name: "Blood", displayName: "Blood" },
    { name: "Elemental", displayName: "Elemental" },
    { name: "Pistol", displayName: "Pistols" },
    { name: "Shotgun", displayName: "Shotgun" },
    { name: "Rifle", displayName: "Rifle" },
  ];

  secondaryAffix: FormNames[] = [
    { name: "none", displayName: "None" },
    { name: "Havoc", displayName: "Havoc" },
    { name: "Destruction", displayName: "Destruction" },
    { name: "Efficiency", displayName: "Efficiency" },
    { name: "Energy", displayName: "Energy" },
  ];

  primaryProc: string[];
  secondaryProc: string[];

  hammerProc: FormNames[] = [
    { name: "none", displayName: "None" },
    { name: "AnimaTouched", displayName: "Anima Touched" },
    { name: "PlasmaForged", displayName: "Plasma Forged" },
    { name: "Shadowbound", displayName: "Shadowbound" },
    { name: "PneumaticMaul", displayName: "Pneumatic Maul" },
    { name: "FumingDespoiler", displayName: "Fuming Despoiler" },
  ];

  fistProc: FormNames[] = [
    { name: "none", displayName: "None" },
    { name: "AnimaTouched", displayName: "Anima Touched" },
    { name: "PlasmaForged", displayName: "Plasma Forged" },
    { name: "Shadowbound", displayName: "Shadowbound" },
    { name: "BladedGauntlets", displayName: "Bladed Gauntlets" },
    { name: "TreshingClaws", displayName: "Treshing Claws" },
    { name: "BloodDrinkers", displayName: "Blood Drinkers" },
  ];

  bladeProc: FormNames[] = [
    { name: "none", displayName: "None" },
    { name: "AnimaTouched", displayName: "Anima Touched" },
    { name: "PlasmaForged", displayName: "Plasma Forged" },
    { name: "Shadowbound", displayName: "Shadowbound" },
    { name: "Apocalypse", displayName: "Apocalypse" },
    { name: "RazorsEdge", displayName: "Razors Edge" },
    { name: "BladeOfTheSeventhSon", displayName: "Blade Of The Seventh Son" },
    { name: "Soulblade", displayName: "Soulblade" },
  ];

  elementalProc: FormNames[] = [
    { name: "none", displayName: "None" },
    { name: "AnimaTouched", displayName: "Anima Touched" },
    { name: "PlasmaForged", displayName: "Plasma Forged" },
    { name: "Shadowbound", displayName: "Shadowbound" },
    { name: "UnstableElectronCore", displayName: "UnstableElectronCore" },
    { name: "FrozenFigurine", displayName: "Frozen Figurine" },
    { name: "CryoChargedConduit", displayName: "Cryo Charged Conduit" },
  ];

  bloodProc: FormNames[] = [
    { name: "none", displayName: "None" },
    { name: "AnimaTouched", displayName: "Anima Touched" },
    { name: "PlasmaForged", displayName: "Plasma Forged" },
    { name: "Shadowbound", displayName: "Shadowbound" },
    { name: "EldritchTome", displayName: "Eldritch Tome" },
  ];

  pistolProc: FormNames[] = [
    { name: "none", displayName: "None" },
    { name: "AnimaTouched", displayName: "Anima Touched" },
    { name: "PlasmaForged", displayName: "Plasma Forged" },
    { name: "Shadowbound", displayName: "Shadowbound" },
    { name: "MiseryAndMalice", displayName: "Misery & Malice" },
    { name: "SixShooters", displayName: "Six Shooters" },
    { name: "SovTechHarmonisers", displayName: "Sov-Tech Harmonisers" },
    { name: "Cb3Annihilators", displayName: "CB-3Annihilators" },
    { name: "HeavyCaliberPistols", displayName: "Heavy Caliber Pistols" },
  ];

  shotgunProc: FormNames[] = [
    { name: "none", displayName: "None" },
    { name: "AnimaTouched", displayName: "Anima Touched" },
    { name: "PlasmaForged", displayName: "Plasma Forged" },
    { name: "Shadowbound", displayName: "Shadowbound" },
    { name: "IfritanDespoiler", displayName: "Ifritan Despoiler" },
    {
      name: "SovTechParadoxGenerator",
      displayName: "Sov-Tech Paradox Generator",
    },
    { name: "SpesC221", displayName: "Spes-C221" },
  ];

  rifleProc: FormNames[] = [
    { name: "none", displayName: "None" },
    { name: "AnimaTouched", displayName: "Anima Touched" },
    { name: "PlasmaForged", displayName: "Plasma Forged" },
    { name: "Shadowbound", displayName: "Shadowbound" },
    { name: "InfernalLoader", displayName: "Infernal Loader" },
    {
      name: "SovTechParadoxGenerator",
      displayName: "Sov-Tech Paradox Generator",
    },
    { name: "Ksr43", displayName: "KSR-43" },
  ];

  chaosProc: FormNames[] = [
    { name: "none", displayName: "None" },
    { name: "AnimaTouched", displayName: "Anima Touched" },
    { name: "PlasmaForged", displayName: "Plasma Forged" },
    { name: "Shadowbound", displayName: "Shadowbound" },
    { name: "OtherwordlyArtifact", displayName: "Otherwordly Artifact" },
    {
      name: "SovTechParadoxGenerator",
      displayName: "Sov-Tech Paradox Generator",
    },
    { name: "WarpedVisage", displayName: "Warped Visage" },
  ];

  passives: string[] = [
    "Obliterate",
    "FastandFurious",
    "Outrage",
    "Berserker",
    "UnbridledWrath",
    "Flay",
    "Contaminate",
    "Desolate",
    "Defilement",
    "CrimsonPulse",
    "LethalAim",
    "Jackpot",
    "BeginnersLuck",
    "FatalShot",
    "FocusedFire",
  ];

  head: FormNames[] = [
    { name: "none", displayName: "None" },
    { name: "Ashes", displayName: "Ashes" },
  ];

  neck: FormNames[] = [
    { name: "none", displayName: "None" },
    { name: "SeedOfAgression", displayName: "Seed Of Aggression" },
    { name: "ChokerOfSheedBlood", displayName: "Choker Of Shed Blood" },
    { name: "EgonPendant", displayName: "Egon Pendant" },
  ];

  luck: FormNames[] = [
    { name: "none", displayName: "None" },
    { name: "ColdSilverDice", displayName: "Cold Silver Dice" },
    { name: "GamblersSoul", displayName: "Gambler's Soul" },
  ];

  gadget: FormNames[] = [
    { name: "none", displayName: "None" },
    {
      name: "ElectrograviticAttractor",
      displayName: "Electrogravitic Attractor",
    },
    { name: "ShardOfSesshoSeki", displayName: "Shard Of Sessho Seki" },
    { name: "ValiMetabolic", displayName: "Vali Metabolic Accelerator" },
    { name: "MnemonicWerewolf", displayName: "Mnemonic Guardian Werewolf" },
  ];

  apl: string;
  passive1: string;
  passive2: string;
  passive3: string;
  passive4: string;
  passive5: string;

  exposed: true;
  openingShot: true;
  headCdr: false;
  waistCdr: true;
  iterations = 10;
  fightLength = 240;
  target: "Regional";

  myform: FormGroup;

  constructor(private readonly http: Http, private readonly router: Router) {}

  userSaveOne(): void {
    console.log("Saved!");
  }

  userLoadOne(): void {
    console.log("Loaded!");
  }

  onSubmit(): void {
    const formInput: any = this.myform.value;
    this.http
      .post("/api/values", formInput)
      .subscribe(
        response => this.extractData(response, this.router),
        this.handleError,
      );
  }

  onChange(newValue) {
    this.setPreset(weaponPresets[newValue]);
  }

  ngOnInit(): void {
    this.myform = new FormGroup({
      primaryWeapon: new FormControl(Validators.required),
      primaryAffix: new FormControl(),
      primaryProc: new FormControl(),
      secondaryWeapon: new FormControl(Validators.required),
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
      gadget: new FormControl(),
      apl: new FormControl(Validators.required),
      exposed: new FormControl(),
      openingShot: new FormControl(),
      headCdr: new FormControl(),
      waistCdr: new FormControl(),
      passive1: new FormControl(),
      passive2: new FormControl(),
      passive3: new FormControl(),
      passive4: new FormControl(),
      passive5: new FormControl(),
    });
    this.setPreset(weaponPresets.Hammer);
  }

  private setPreset(preset) {
    Object.keys(preset).forEach(key => {
      const value = {};
      value[key] = preset[key];
      this.myform.patchValue(value);
    });
  }

  private extractData(res: Response, router: Router) {
    const body = res.text();
    localStorage.setItem("Results", body);
    this.router.navigate(["/result"]);
  }

  private handleError(error: Response | any) {
    console.log(error);
    return Observable.throw(error);
  }
}
