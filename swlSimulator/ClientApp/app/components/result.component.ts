import {Component} from "@angular/core";
import {FormControl, FormGroup} from "@angular/forms";
import {Http, Response} from "@angular/http";
import {RouterModule, Routes, Router} from "@angular/router";
import {Observable} from "rxjs/Observable";

@Component({
    selector: "import",
    templateUrl: "import.component.html"
})
export class ImportComponent {
    constructor(private http: Http, private router: Router) {}

    primaryWeapon: string[] = [
        "Rifle",
        "Blade",
        "Blood",
        "Chaos",
        "Elemental",
        "Fist",
        "Hammer",
        "Pistol",
        "Shotgun"
    ];
    primaryAffix: string[] = [
        "Havoc",
        "Destruction",
        "Effieciency",
        "Energy",
        "None"
    ];
    secondaryWeapon: string[] = [
        "Rifle",
        "Blade",
        "Blood",
        "Chaos",
        "Elemental",
        "Fist",
        "Hammer",
        "Pistol",
        "Shotgun"
    ];
    secondaryAffix: string[] = [
        "Havoc",
        "Destruction",
        "Effieciency",
        "Energy",
        "None"
    ];
    primaryProc: string[];
    secondaryProc: string[];
    hammerProc: string[] = [
        "None",
        "AnimaTouched",
        "PlasmaForged",
        "Shadowbound",
        "Pneumatic Maul",
        "Fuming Despoiler"
    ];
    fistProc: string[] = [
        "None",
        "AnimaTouched",
        "PlasmaForged",
        "Shadowbound",
        "Bladed Gauntlets",
        "Treshing Claws",
        "Blood Drinkers"
    ];
    bladeProc: string[] = [
        "None",
        "AnimaTouched",
        "PlasmaForged",
        "Shadowbound",
        "Apocalypse",
        "RazorsEdge",
        "Blade Of The Seventh Son",
        "Soulblade"
    ];
    elementalProc: string[] = [
        "None",
        "AnimaTouched",
        "Plasma Forged",
        "Shadowbound",
        "UnstableElectronCore",
        "Frozen Figurine",
        "Cryo Charged Conduit"
    ];

    bloodProc: string[] = [
        "None",
        "Anima Touched",
        "Plasma Forged",
        "Shadowbound",
        "EldritchTome"
    ];
    pistolProc: string[] = [
        "None",
        "Anima Touched",
        "Plasma Forged",
        "Shadowbound",
        "Misery & Malice",
        "Six Shooters",
        "Sov-Tech Harmonisers",
        "CB-3Annihilators",
        "HeavyCaliberPistols"
    ];

    shotgunProc: string[] = [
        "None",
        "Anima Touched",
        "Plasma Forged",
        "Shadowbound",
        "Ifritan Despoiler",
        "SpesC221"
    ];
    rifleProc: string[] = [
        "None",
        "Anima Touched",
        "Plasma Forged",
        "Shadowbound",
        "Infernal Loader",
        "KSR43"
    ];
    chaosProc: string[] = [
        "None",
        "Anima Touched",
        "Plasma Forged",
        "Shadowbound",
        "Warped Visage",
        "Otherwordly Artifact",
        "Sov Tech Paradox Generator"
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
        "Beginners Luck",
        "Fatal Shot",
        "Focused Fire"
    ];

    head: string[] = ["None", "Ashes"];
    neck: string[] = [
        "None",
        "SeedOfAgression",
        "Choker Of Sheed Blood",
        "Egon Pendant"
    ];
    luck: string[] = ["None", "ColdSilverDice", "GamblersSoul"];
    gadget: string[] = [
        "None",
        "Electrogravitic Attractor",
        "Shard Of SesshoSeki",
        "ValiMetabolic",
        "Mnemonic Guardian Werewolf"
    ];

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

    hammerPreset(): void {
        this.myform.patchValue({primaryWeapon: "Hammer"});
        this.myform.patchValue({primaryAffix: "Havoc"});
        this.myform.patchValue({primaryProc: "PneumaticMaul"});
        this.myform.patchValue({secondaryWeapon: "Fist"});
        this.myform.patchValue({secondaryAffix: "Destruction"});
        this.myform.patchValue({secondaryProc: "None"});
        this.myform.patchValue({combatPower: 1350});
        this.myform.patchValue({criticalChance: 30});
        this.myform.patchValue({criticalPower: 120});
        this.myform.patchValue({basicSignet: 73});
        this.myform.patchValue({powerSignet: 19});
        this.myform.patchValue({eliteSignet: 41});
        this.myform.patchValue({waistSignet: 49});
        this.myform.patchValue({luckSignet: 12});
        this.myform.patchValue({head: "Ashes"});
        this.myform.patchValue({neck: "SeedOfAgression"});
        this.myform.patchValue({luck: "ColdSilverDice"});
        this.myform.patchValue({gadget: "ValiMetabolic"});
        this.myform.patchValue({exposed: true});
        this.myform.patchValue({openingShot: true});
        this.myform.patchValue({headCdr: false});
        this.myform.patchValue({waistCdr: true});
        this.myform.patchValue({passive1: "Obliterate"});
        this.myform.patchValue({passive2: "FastandFurious"});
        this.myform.patchValue({passive3: "Outrage"});
        this.myform.patchValue({passive4: "Berserker"});
        this.myform.patchValue({passive5: "UnbridledWrath"});
        this.myform.patchValue({
            apl:
                "Fist.Savagery, Buff.UnstoppableForce.Active\r\n" +
                "Hammer.Seethe, Buff.UnstoppableForce.Active\r\n" +
                "Hammer.UnstoppableForce, Rage > 50 || Hammer.Energy > 8\r\n" +
                "Hammer.Demolish, Buff.UnstoppableForce.Active\r\n" +
                "Hammer.Demolish, Rage > 60 || Hammer.Energy > 13\r\n" +
                "Hammer.Smash"
        });
    }

    chaosPreset(): void {
        this.myform.patchValue({primaryWeapon: "Chaos"});
        this.myform.patchValue({primaryAffix: "Havoc"});
        this.myform.patchValue({primaryProc: "Warped Visage"});
        this.myform.patchValue({secondaryWeapon: "Shotgun"});
        this.myform.patchValue({secondaryAffix: "Destruction"});
        this.myform.patchValue({secondaryProc: "None"});
        this.myform.patchValue({combatPower: 1350});
        this.myform.patchValue({criticalChance: 30});
        this.myform.patchValue({criticalPower: 120});
        this.myform.patchValue({basicSignet: 73});
        this.myform.patchValue({powerSignet: 19});
        this.myform.patchValue({eliteSignet: 41});
        this.myform.patchValue({waistSignet: 49});
        this.myform.patchValue({head: "Ashes"});
        this.myform.patchValue({neck: "Seed Of Agression"});
        this.myform.patchValue({luck: "Cold Silver Dice"});
        this.myform.patchValue({gadget: "Vali Metabolic"});
        this.myform.patchValue({exposed: true});
        this.myform.patchValue({openingShot: true});
        this.myform.patchValue({headCdr: false});
        this.myform.patchValue({waistCdr: true});
        this.myform.patchValue({passive1: "Disintegrate"});
        this.myform.patchValue({passive2: "Fractured Existence"});
        this.myform.patchValue({passive3: "Body Double"});
        this.myform.patchValue({passive4: "Butterfly Effect"});
        this.myform.patchValue({passive5: "Blessing of Octed"});
        this.myform.patchValue({
            apl:
                "Chaos.Pandemonium\r\n" +
                "Chaos.Breakdown\r\n" +
                "Shotgun.ShellSalvage\r\n" +
                "Shotgun.RagingShot\r\n" +
                "Chaos.Deconstruct"
        });
    }

    fistPreset(): void {
        this.myform.patchValue({primaryWeapon: "Fist"});
        this.myform.patchValue({primaryAffix: "Destruction"});
        this.myform.patchValue({primaryProc: "Bladed Gauntlets"});
        this.myform.patchValue({secondaryWeapon: "Shotgun"});
        this.myform.patchValue({secondaryAffix: "Destruction"});
        this.myform.patchValue({secondaryProc: "None"});
        this.myform.patchValue({combatPower: 1350});
        this.myform.patchValue({criticalChance: 30});
        this.myform.patchValue({criticalPower: 120});
        this.myform.patchValue({basicSignet: 73});
        this.myform.patchValue({powerSignet: 19});
        this.myform.patchValue({eliteSignet: 41});
        this.myform.patchValue({waistSignet: 49});
        this.myform.patchValue({head: "Ashes"});
        this.myform.patchValue({neck: "Seed Of Agression"});
        this.myform.patchValue({luck: "Cold Silver Dice"});
        this.myform.patchValue({gadget: "Vali Metabolic"});
        this.myform.patchValue({exposed: true});
        this.myform.patchValue({openingShot: true});
        this.myform.patchValue({headCdr: false});
        this.myform.patchValue({waistCdr: true});
        this.myform.patchValue({passive1: "Smell Fear"});
        this.myform.patchValue({passive2: "Wild Nature"});
        this.myform.patchValue({passive3: "Brutality"});
        this.myform.patchValue({passive4: "Killer Instinct"});
        this.myform.patchValue({passive5: "Gore"});
        this.myform.patchValue({
            apl:
                "Fist.Savagery\r\n" +
                "Fist.PrimalInstinct\r\n" +
                "Shotgun.ShelLSalvage\r\n" +
                "Fist.Mangle\r\n" +
                "Shotgun.RagingShot\r\n" +
                "Fist.Trash"
        });
    }

    pistolPreset(): void {
        this.myform.patchValue({primaryWeapon: "Pistol"});
        this.myform.patchValue({primaryAffix: "Energy"});
        this.myform.patchValue({primaryProc: "Sov-Tech Harmonisers"});
        this.myform.patchValue({secondaryWeapon: "Fist"});
        this.myform.patchValue({secondaryAffix: "Destruction"});
        this.myform.patchValue({secondaryProc: "None"});
        this.myform.patchValue({combatPower: 1350});
        this.myform.patchValue({criticalChance: 30});
        this.myform.patchValue({criticalPower: 120});
        this.myform.patchValue({basicSignet: 73});
        this.myform.patchValue({powerSignet: 19});
        this.myform.patchValue({eliteSignet: 41});
        this.myform.patchValue({waistSignet: 49});
        this.myform.patchValue({head: "Ashes"});
        this.myform.patchValue({neck: "Seed Of Agression"});
        this.myform.patchValue({luck: "Cold Silver Dice"});
        this.myform.patchValue({gadget: "Vali Metabolic"});
        this.myform.patchValue({exposed: true});
        this.myform.patchValue({openingShot: true});
        this.myform.patchValue({headCdr: false});
        this.myform.patchValue({waistCdr: true});
        this.myform.patchValue({passive1: "Lethal Aim"});
        this.myform.patchValue({passive2: "Jackpot"});
        this.myform.patchValue({passive3: "Beginners Luck"});
        this.myform.patchValue({passive4: "Fatal Shot"});
        this.myform.patchValue({passive5: "Focused Fire"});
        this.myform.patchValue({
            apl:
                "Pistol.KillBlind, Buff.RedChambers || Buff.BlueChambers || Buff.WhiteChambers\r\n" +
                "Fist.Savagery\r\n" +
                "Pistol.Flourish, Pistol.Energy < 11\r\n" +
                "Pistol.TrickShot, Buff.Savagery.Active\r\n" +
                "Pistol.Dualshot, Pistol.Energy > 12 || Buff.RedChambers || Buff.BlueChambers || Buff.WhiteChambers\r\n" +
                "Pistol.HairTrigger"
        });
    }

    bloodPreset(): void {
        this.myform.patchValue({primaryWeapon: "Blood"});
        this.myform.patchValue({primaryAffix: "Energy"});
        this.myform.patchValue({primaryProc: "Eldritch Tome"});
        this.myform.patchValue({secondaryWeapon: "Fist"});
        this.myform.patchValue({secondaryAffix: "Destruction"});
        this.myform.patchValue({secondaryProc: "None"});
        this.myform.patchValue({combatPower: 1350});
        this.myform.patchValue({criticalChance: 30});
        this.myform.patchValue({criticalPower: 120});
        this.myform.patchValue({basicSignet: 73});
        this.myform.patchValue({powerSignet: 19});
        this.myform.patchValue({eliteSignet: 41});
        this.myform.patchValue({waistSignet: 49});
        this.myform.patchValue({head: "Ashes"});
        this.myform.patchValue({neck: "Seed Of Agression"});
        this.myform.patchValue({luck: "Cold Silver Dice"});
        this.myform.patchValue({gadget: "Vali Metabolic"});
        this.myform.patchValue({exposed: true});
        this.myform.patchValue({openingShot: true});
        this.myform.patchValue({headCdr: false});
        this.myform.patchValue({waistCdr: true});
        this.myform.patchValue({passive1: "Flay"});
        this.myform.patchValue({passive2: "Contaminate"});
        this.myform.patchValue({passive3: "Desolate"});
        this.myform.patchValue({passive4: "Defilement"});
        this.myform.patchValue({passive5: "Crimson Pulse"});
        this.myform.patchValue({
            apl:
                "Fist.Savagery, Corruption >= 10 && EldritchScourge.Cooldown <= 0 && Desecrate.Cooldown <= 0\r\n" +
                "Blood.EldritchScourge, Corruption > 0 && Buff.Savagery.Duration > 2\r\n" +
                "Blood.Desecrate, Buff.Savagery.Active\r\n" +
                "Blood.Maleficium, Blood.Energy > 10\r\n" +
                "Blood.Torment\r\n"
        });
    }

    bladePreset(): void {
        this.myform.patchValue({primaryWeapon: "Blade"});
        this.myform.patchValue({primaryAffix: "Energy"});
        this.myform.patchValue({primaryProc: "Plasma Forged"});
        this.myform.patchValue({secondaryWeapon: "Fist"});
        this.myform.patchValue({secondaryAffix: "Destruction"});
        this.myform.patchValue({secondaryProc: "None"});
        this.myform.patchValue({combatPower: 1350});
        this.myform.patchValue({criticalChance: 30});
        this.myform.patchValue({criticalPower: 120});
        this.myform.patchValue({basicSignet: 73});
        this.myform.patchValue({powerSignet: 19});
        this.myform.patchValue({eliteSignet: 41});
        this.myform.patchValue({waistSignet: 49});
        this.myform.patchValue({head: "Ashes"});
        this.myform.patchValue({neck: "Seed Of Agression"});
        this.myform.patchValue({luck: "Cold Silver Dice"});
        this.myform.patchValue({gadget: "Vali Metabolic"});
        this.myform.patchValue({exposed: true});
        this.myform.patchValue({openingShot: true});
        this.myform.patchValue({headCdr: false});
        this.myform.patchValue({waistCdr: true});
        this.myform.patchValue({passive1: "Keen Edge"});
        this.myform.patchValue({passive2: "Storm Surge"});
        this.myform.patchValue({passive3: "Masterpiece"});
        this.myform.patchValue({passive4: "Masters Focus"});
        this.myform.patchValue({passive5: "Warriors Spirit"});
        this.myform.patchValue({
            apl:
                "Fist.Savagery\r\n" +
                "Blade.Hone\r\n" +
                "Blade.SupremeHarmony\r\n" +
                "Blade.SpiritBlade\r\n" +
                "Blade.Tsunami\r\n" +
                "Blade.FlowingStrike\r\n"
        });
    }

    riflePreset(): void {
        this.myform.patchValue({primaryWeapon: "Rifle"});
        this.myform.patchValue({primaryAffix: "Energy"});
        this.myform.patchValue({primaryProc: "Infernal Loader"});
        this.myform.patchValue({secondaryWeapon: "Fist"});
        this.myform.patchValue({secondaryAffix: "Destruction"});
        this.myform.patchValue({secondaryProc: "Bladed Gauntlets"});
        this.myform.patchValue({combatPower: 1350});
        this.myform.patchValue({criticalChance: 30});
        this.myform.patchValue({criticalPower: 120});
        this.myform.patchValue({basicSignet: 73});
        this.myform.patchValue({powerSignet: 19});
        this.myform.patchValue({eliteSignet: 41});
        this.myform.patchValue({waistSignet: 49});
        this.myform.patchValue({head: "Ashes"});
        this.myform.patchValue({neck: "Seed Of Agression"});
        this.myform.patchValue({luck: "Cold Silver Dice"});
        this.myform.patchValue({gadget: "Vali Metabolic"});
        this.myform.patchValue({exposed: true});
        this.myform.patchValue({openingShot: true});
        this.myform.patchValue({headCdr: false});
        this.myform.patchValue({waistCdr: true});
        this.myform.patchValue({passive1: "Stability"});
        this.myform.patchValue({passive2: "Backup Plan"});
        this.myform.patchValue({passive3: "Secondary Explosion"});
        this.myform.patchValue({passive4: "Slow Burn"});
        this.myform.patchValue({passive5: "Explosives Expert"});
        this.myform.patchValue({
            apl:
                "Fist.Savagery, HighExplosiveGrenade.Cooldown <= 0\r\n" +
                "Rifle.HighExplosiveGrenade, Buff.Savagery" +
                "Rifle.IncendiaryGrenade, HighExplosiveGrenade.Cooldown >6\r\n" +
                "Rifle.BurstFire\r\n" +
                "Rifle.PlacedShot"
        });
    }

    elementalismPreset(): void {
        this.myform.patchValue({primaryWeapon: "Elemental"});
        this.myform.patchValue({primaryAffix: "Havoc"});
        this.myform.patchValue({primaryProc: "Unstable Electron Core"});
        this.myform.patchValue({secondaryWeapon: "Fist"});
        this.myform.patchValue({secondaryAffix: "Destruction"});
        this.myform.patchValue({secondaryProc: "None"});
        this.myform.patchValue({combatPower: 1350});
        this.myform.patchValue({criticalChance: 30});
        this.myform.patchValue({criticalPower: 120});
        this.myform.patchValue({basicSignet: 73});
        this.myform.patchValue({powerSignet: 19});
        this.myform.patchValue({eliteSignet: 41});
        this.myform.patchValue({waistSignet: 49});
        this.myform.patchValue({head: "Ashes"});
        this.myform.patchValue({neck: "Seed Of Agression"});
        this.myform.patchValue({luck: "Cold Silver Dice"});
        this.myform.patchValue({gadget: "Vali Metabolic"});
        this.myform.patchValue({exposed: true});
        this.myform.patchValue({openingShot: true});
        this.myform.patchValue({headCdr: false});
        this.myform.patchValue({waistCdr: true});
        this.myform.patchValue({passive1: "Glaciate"});
        this.myform.patchValue({passive2: "Superconductor"});
        this.myform.patchValue({passive3: "Crystallised Blaze"});
        this.myform.patchValue({passive4: "Combustion"});
        this.myform.patchValue({passive5: "Thermodynamo"});
        this.myform.patchValue({
            apl:
                "Fist.Savagery\r\n" +
                "Elementalism.IceBeam\r\n" +
                "Elementalism.CrystallisedFlame\r\n" +
                "Elementalism.Mjolnir\r\n" +
                "Elementalism.Fireball"
        });
    }

    shotgunPreset(): void {
        this.myform.patchValue({primaryWeapon: "Shotgun"});
        this.myform.patchValue({primaryAffix: "Energy"});
        this.myform.patchValue({primaryProc: "Ifritan Despoiler"});
        this.myform.patchValue({secondaryWeapon: "Fist"});
        this.myform.patchValue({secondaryAffix: "Destruction"});
        this.myform.patchValue({secondaryProc: "None"});
        this.myform.patchValue({combatPower: 1350});
        this.myform.patchValue({criticalChance: 30});
        this.myform.patchValue({criticalPower: 120});
        this.myform.patchValue({basicSignet: 73});
        this.myform.patchValue({powerSignet: 19});
        this.myform.patchValue({eliteSignet: 41});
        this.myform.patchValue({waistSignet: 49});
        this.myform.patchValue({head: "Ashes"});
        this.myform.patchValue({neck: "Seed Of Agression"});
        this.myform.patchValue({luck: "Cold Silver Dice"});
        this.myform.patchValue({gadget: "Vali Metabolic"});
        this.myform.patchValue({exposed: true});
        this.myform.patchValue({openingShot: true});
        this.myform.patchValue({headCdr: false});
        this.myform.patchValue({waistCdr: true});
        this.myform.patchValue({passive1: "Point Blank Shot"});
        this.myform.patchValue({passive2: "Withering Salvo"});
        this.myform.patchValue({passive3: "Salvage Expert"});
        this.myform.patchValue({passive4: "Odds and Evens"});
        this.myform.patchValue({passive5: "Munitions Expert"});
        this.myform.patchValue({
            apl:
                "Fist.Savagery\r\n" +
                "Shotgun.FullSalvo\r\n" +
                "Shotgun.ShellSalvage\r\n" +
                "Shotgun.RagingShot\r\n" +
                "Shotgun.PumpAction\r\n"
        });
    }

    userSaveOne(): void {
        console.log("Saved!");
    }

    userLoadOne(): void {
        console.log("Loaded!");
    }

    onSubmit(): void {
        let formInput: any = this.myform.value;
        this.http
            .post("/api/values", formInput)
            .subscribe(
                response => this.extractData(response, this.router),
                this.handleError
            );
    }

    private extractData(res: Response, router: Router) {
        let body = res.text();
        localStorage.setItem("Results", body);
        this.router.navigate(["/result"]);
    }

    private handleError(error: Response | any) {
        console.log(error);
        return Observable.throw(error);
    }

    ngOnInit(): void {
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
            gadget: new FormControl(),
            apl: new FormControl(),
            exposed: new FormControl(),
            openingShot: new FormControl(),
            headCdr: new FormControl(),
            waistCdr: new FormControl(),
            passive1: new FormControl(),
            passive2: new FormControl(),
            passive3: new FormControl(),
            passive4: new FormControl(),
            passive5: new FormControl()
        });
    }
}
