export class Settings {
    primaryWeapon: ["Rifle", "Blade", "Blood", "Chaos", "Elemental", "Fist", "Hammer", "Pistol", "Shotgun"];
    primaryAffix: ["Havoc", "Destruction", "Effieciency", "Energy", "None"];
    primaryProc: "Pneumatic";
    secondaryWeapon: ["Rifle", "Blade", "Blood", "Chaos", "Elemental", "Fist", "Hammer", "Pistol", "Shotgun"];
    secondaryAffix: ["Havoc", "Destruction", "Effieciency", "Energy", "None"];
    secondarProc: "Bladed Gauntlets";

    combatPower: 1300;
    criticalChance: 31;
    critaicalPower: 110;

    basicSignet: 110;
    powerSignet: 21;
    waistSignet: 52;
    luckSignet: 12;
    eliteSignet: 51;

    head: "Ashes";
    neck: "Seed of Agression";
    luck: "Cold Silver Dice";
    gadget: "Vali Metabolic";

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
}

export enum WeaponProc {
    None, AnimaTouched, FlameWreathed, PlasmaForged, Shadowbound,
    PneumaticMaul, FumingDespoiler,
    EldritchTome,
    MiseryAndMalice, SixShooters, SovTechHarmonisers, Cb3Annihilators, HeavyCaliberPistols,
    InfernalLoader, Ksr43,
    BladedGauntlets, TreshingClaws, BloodDrinkers,
    Apocalypse, RazorsEdge, BladeOfTheSeventhSon, Soulblade,
    IfritanDespoiler, Spesc221,
    UnstableElectronCore, FrozenFigurine, CryoChargedConduit,
    WarpedVisage, OtherworldlyArtifact, SovTechParadoxGenerator
}