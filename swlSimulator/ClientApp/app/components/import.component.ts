import {Component} from "@angular/core";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {Http, Response} from "@angular/http";
import {Router} from "@angular/router";
import {Observable} from "rxjs/Observable";
import {FormNames} from "./interfaces";

@Component({
    selector: "import",
    templateUrl: "import.component.html"
})
export class ImportComponent {
    constructor(private http: Http, private router: Router) {}

    primaryWeapon: FormNames[] = [
        {name:"Blade", displayName:"Blade"},
        {name:"Hammer", displayName:"Hammer"},
        {name:"Fist", displayName:"Fist"},
        {name:"Chaos", displayName: "Chaos"},
        {name:"Blood", displayName:"Blood"},
        {name:"Elemental", displayName:"Elemental"},
        {name:"Pistol", displayName:"Pistols"},
        {name:"Shotgun", displayName: "Shotgun"},
        {name:"Rifle", displayName:"Rifle"}
    ];
    primaryAffix: FormNames[] = [
        {name:"none", displayName:"None"},
        {name:"Havoc", displayName:"Havoc"},
        {name:"Destruction", displayName:"Destruction"},
        {name:"Efficiency", displayName: "Efficiency"},
        {name:"Energy", displayName:"Energy"}
    ];
    secondaryWeapon: FormNames[] = [
        {name:"Blade", displayName:"Blade"},
        {name:"Hammer", displayName:"Hammer"},
        {name:"Fist", displayName:"Fist"},
        {name:"Chaos", displayName: "Chaos"},
        {name:"Blood", displayName:"Blood"},
        {name:"Elemental", displayName:"Elemental"},
        {name:"Pistol", displayName:"Pistols"},
        {name:"Shotgun", displayName: "Shotgun"},
        {name:"Rifle", displayName:"Rifle"}
    ];

    secondaryAffix: FormNames[] = [
        {name:"none", displayName:"None"},
        {name:"Havoc", displayName:"Havoc"},
        {name:"Destruction", displayName:"Destruction"},
        {name:"Efficiency", displayName: "Efficiency"},
        {name:"Energy", displayName:"Energy"}
    ];
    
    primaryProc: string[];
    secondaryProc: string[];
    
    hammerProc: FormNames[] = [
        {name:"none", displayName:"None"},
        {name:"AnimaTouched", displayName:"Anima Touched"},
        {name:"PlasmaForged", displayName:"Plasma Forged"},
        {name:"Shadowbound", displayName: "Shadowbound"},
        {name:"PneumaticMaul", displayName:"Pneumatic Maul"},
        {name:"FumingDespoiler", displayName:"Fuming Despoiler"}
    ];
    
    fistProc: FormNames[] = [
        {name:"none", displayName:"None"},
        {name:"AnimaTouched", displayName:"Anima Touched"},
        {name:"PlasmaForged", displayName:"Plasma Forged"},
        {name:"Shadowbound", displayName: "Shadowbound"},
        {name:"BladedGauntlets", displayName:"Bladed Gauntlets"},
        {name:"TreshingClaws", displayName:"Treshing Claws"},
        {name:"BloodDrinkers", displayName:"Blood Drinkers"}
    ];
    
    bladeProc: FormNames[] = [
        {name:"none", displayName:"None"},
        {name:"AnimaTouched", displayName:"Anima Touched"},
        {name:"PlasmaForged", displayName:"Plasma Forged"},
        {name:"Shadowbound", displayName: "Shadowbound"},
        {name:"Apocalypse", displayName:"Apocalypse"},
        {name:"RazorsEdge", displayName:"Razors Edge"},
        {name:"BladeOfTheSeventhSon", displayName:"Blade Of The Seventh Son"},
        {name:"Soulblade", displayName:"Soulblade"}
    ];
    
    elementalProc: FormNames[] = [
        {name:"none", displayName:"None"},
        {name:"AnimaTouched", displayName:"Anima Touched"},
        {name:"PlasmaForged", displayName:"Plasma Forged"},
        {name:"Shadowbound", displayName: "Shadowbound"},
        {name:"UnstableElectronCore", displayName:"UnstableElectronCore"},
        {name:"FrozenFigurine", displayName:"Frozen Figurine"},
        {name:"CryoChargedConduit", displayName:"Cryo Charged Conduit"}
    ];

    bloodProc: FormNames[] = [
        {name:"none", displayName:"None"},
        {name:"AnimaTouched", displayName:"Anima Touched"},
        {name:"PlasmaForged", displayName:"Plasma Forged"},
        {name:"Shadowbound", displayName: "Shadowbound"},
        {name:"EldritchTome", displayName:"Eldritch Tome"},
    ];
    
    pistolProc: FormNames[] = [
        {name:"none", displayName:"None"},
        {name:"AnimaTouched", displayName:"Anima Touched"},
        {name:"PlasmaForged", displayName:"Plasma Forged"},
        {name:"Shadowbound", displayName: "Shadowbound"},
        {name:"MiseryAndMalice", displayName:"Misery & Malice"},
        {name:"SixShooters", displayName:"Six Shooters"},
        {name:"SovTechHarmonisers", displayName: "Sov-Tech Harmonisers"},
        {name:"Cb3Annihilators", displayName:"CB-3Annihilators"},
        {name:"HeavyCaliberPistols", displayName: "Heavy Caliber Pistols"}
    ];

    shotgunProc: FormNames[] = [
        {name:"none", displayName:"None"},
        {name:"AnimaTouched", displayName:"Anima Touched"},
        {name:"PlasmaForged", displayName:"Plasma Forged"},
        {name:"Shadowbound", displayName: "Shadowbound"},
        {name:"IfritanDespoiler", displayName:"Ifritan Despoiler"},
        {name:"SovTechParadoxGenerator", displayName:"Sov-Tech Paradox Generator"},
        {name:"SpesC221", displayName: "Spes-C221"}
    ];
    
    rifleProc: FormNames[] = [
        {name:"none", displayName:"None"},
        {name:"AnimaTouched", displayName:"Anima Touched"},
        {name:"PlasmaForged", displayName:"Plasma Forged"},
        {name:"Shadowbound", displayName: "Shadowbound"},
        {name:"InfernalLoader", displayName:"Infernal Loader"},
        {name:"SovTechParadoxGenerator", displayName:"Sov-Tech Paradox Generator"},
        {name:"Ksr43", displayName: "KSR-43"}
    ];

    chaosProc: FormNames[] = [
        {name:"none", displayName:"None"},
        {name:"AnimaTouched", displayName:"Anima Touched"},
        {name:"PlasmaForged", displayName:"Plasma Forged"},
        {name:"Shadowbound", displayName: "Shadowbound"},
        {name:"OtherwordlyArtifact", displayName:"Otherwordly Artifact"},
        {name:"SovTechParadoxGenerator", displayName:"Sov-Tech Paradox Generator"},
        {name:"WarpedVisage", displayName: "Warped Visage"}
        
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
        "FocusedFire"
    ];

    head: FormNames[] = [
        {name:"none", displayName:"None"},
        {name:"Ashes", displayName:"Ashes"}
    ];
    
    neck: FormNames[] = [
        {name:"none", displayName:"None"},
        {name:"SeedOfAgression", displayName:"Seed Of Aggression"},
        {name:"ChokerOfSheedBlood", displayName:"Choker Of Shed Blood"},
        {name:"EgonPendant", displayName: "Egon Pendant"}
    ];
    
    luck: FormNames[] = [
        {name:"none", displayName:"None"},
        {name:"ColdSilverDice", displayName:"Cold Silver Dice"},
        {name:"GamblersSoul", displayName:"Gambler's Soul"}
    ];
    
    gadget: FormNames[] = [
        {name:"none", displayName:"None"},
        {name:"ElectrograviticAttractor", displayName:"Electrogravitic Attractor"},
        {name:"ShardOfSesshoSeki", displayName:"Shard Of Sessho Seki"},
        {name:"ValiMetabolic", displayName:"Vali Metabolic Accelerator"},
        {name:"MnemonicWerewolf", displayName:"Mnemonic Guardian Werewolf"}
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
        this.myform.patchValue({primaryProc: "WarpedVisage"});
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
        this.myform.patchValue({neck: "SeedOfAgression"});
        this.myform.patchValue({luck: "ColdSilverDice"});
        this.myform.patchValue({gadget: "ValiMetabolic"});
        this.myform.patchValue({exposed: true});
        this.myform.patchValue({openingShot: true});
        this.myform.patchValue({headCdr: false});
        this.myform.patchValue({waistCdr: true});
        this.myform.patchValue({passive1: "Disintegrate"});
        this.myform.patchValue({passive2: "FracturedExistence"});
        this.myform.patchValue({passive3: "BodyDouble"});
        this.myform.patchValue({passive4: "ButterflyEffect"});
        this.myform.patchValue({passive5: "BlessingofOcted"});
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
        this.myform.patchValue({neck: "SeedOfAgression"});
        this.myform.patchValue({luck: "ColdSilverDice"});
        this.myform.patchValue({gadget: "ValiMetabolic"});
        this.myform.patchValue({exposed: true});
        this.myform.patchValue({openingShot: true});
        this.myform.patchValue({headCdr: false});
        this.myform.patchValue({waistCdr: true});
        this.myform.patchValue({passive1: "SmellFear"});
        this.myform.patchValue({passive2: "WildNature"});
        this.myform.patchValue({passive3: "Brutality"});
        this.myform.patchValue({passive4: "KillerInstinct"});
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
        this.myform.patchValue({primaryProc: "SovTechHarmonisers"});
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
        this.myform.patchValue({neck: "SeedOfAgression"});
        this.myform.patchValue({luck: "ColdSilverDice"});
        this.myform.patchValue({gadget: "ValiMetabolic"});
        this.myform.patchValue({exposed: true});
        this.myform.patchValue({openingShot: true});
        this.myform.patchValue({headCdr: false});
        this.myform.patchValue({waistCdr: true});
        this.myform.patchValue({passive1: "LethalAim"});
        this.myform.patchValue({passive2: "Jackpot"});
        this.myform.patchValue({passive3: "BeginnersLuck"});
        this.myform.patchValue({passive4: "FatalShot"});
        this.myform.patchValue({passive5: "FocusedFire"});
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
        this.myform.patchValue({primaryProc: "EldritchTome"});
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
        this.myform.patchValue({neck: "SeedOfAgression"});
        this.myform.patchValue({luck: "ColdSilverDice"});
        this.myform.patchValue({gadget: "ValiMetabolic"});
        this.myform.patchValue({exposed: true});
        this.myform.patchValue({openingShot: true});
        this.myform.patchValue({headCdr: false});
        this.myform.patchValue({waistCdr: true});
        this.myform.patchValue({passive1: "Flay"});
        this.myform.patchValue({passive2: "Contaminate"});
        this.myform.patchValue({passive3: "Desolate"});
        this.myform.patchValue({passive4: "Defilement"});
        this.myform.patchValue({passive5: "CrimsonPulse"});
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
        this.myform.patchValue({primaryProc: "PlasmaForged"});
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
        this.myform.patchValue({neck: "SeedOfAgression"});
        this.myform.patchValue({luck: "ColdSilverDice"});
        this.myform.patchValue({gadget: "ValiMetabolic"});
        this.myform.patchValue({exposed: true});
        this.myform.patchValue({openingShot: true});
        this.myform.patchValue({headCdr: false});
        this.myform.patchValue({waistCdr: true});
        this.myform.patchValue({passive1: "KeenEdge"});
        this.myform.patchValue({passive2: "StormSurge"});
        this.myform.patchValue({passive3: "Masterpiece"});
        this.myform.patchValue({passive4: "MastersFocus"});
        this.myform.patchValue({passive5: "WarriorsSpirit"});
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
        this.myform.patchValue({primaryProc: "InfernalLoader"});
        this.myform.patchValue({secondaryWeapon: "Fist"});
        this.myform.patchValue({secondaryAffix: "Destruction"});
        this.myform.patchValue({secondaryProc: "BladedGauntlets"});
        this.myform.patchValue({combatPower: 1350});
        this.myform.patchValue({criticalChance: 30});
        this.myform.patchValue({criticalPower: 120});
        this.myform.patchValue({basicSignet: 73});
        this.myform.patchValue({powerSignet: 19});
        this.myform.patchValue({eliteSignet: 41});
        this.myform.patchValue({waistSignet: 49});
        this.myform.patchValue({head: "Ashes"});
        this.myform.patchValue({neck: "SeedOfAgression"});
        this.myform.patchValue({luck: "ColdSilverDice"});
        this.myform.patchValue({gadget: "ValiMetabolic"});
        this.myform.patchValue({exposed: true});
        this.myform.patchValue({openingShot: true});
        this.myform.patchValue({headCdr: false});
        this.myform.patchValue({waistCdr: true});
        this.myform.patchValue({passive1: "Stability"});
        this.myform.patchValue({passive2: "BackupPlan"});
        this.myform.patchValue({passive3: "SecondaryExplosion"});
        this.myform.patchValue({passive4: "SlowBurn"});
        this.myform.patchValue({passive5: "ExplosivesExpert"});
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
        this.myform.patchValue({primaryProc: "UnstableElectronCore"});
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
        this.myform.patchValue({neck: "SeedOfAgression"});
        this.myform.patchValue({luck: "ColdSilverDice"});
        this.myform.patchValue({gadget: "ValiMetabolic"});
        this.myform.patchValue({exposed: true});
        this.myform.patchValue({openingShot: true});
        this.myform.patchValue({headCdr: false});
        this.myform.patchValue({waistCdr: true});
        this.myform.patchValue({passive1: "Glaciate"});
        this.myform.patchValue({passive2: "Superconductor"});
        this.myform.patchValue({passive3: "CrystallisedBlaze"});
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
        this.myform.patchValue({primaryProc: "IfritanDespoiler"});
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
        this.myform.patchValue({neck: "SeedOfAgression"});
        this.myform.patchValue({luck: "ColdSilverDice"});
        this.myform.patchValue({gadget: "ValiMetabolic"});
        this.myform.patchValue({exposed: true});
        this.myform.patchValue({openingShot: true});
        this.myform.patchValue({headCdr: false});
        this.myform.patchValue({waistCdr: true});
        this.myform.patchValue({passive1: "PointBlankShot"});
        this.myform.patchValue({passive2: "WitheringSalvo"});
        this.myform.patchValue({passive3: "SalvageExpert"});
        this.myform.patchValue({passive4: "OddsandEvens"});
        this.myform.patchValue({passive5: "MunitionsExpert"});
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
            passive5: new FormControl()
        });
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
}
