import { IWeaponPreset } from "../interfaces";

const Hammer: IWeaponPreset = {
  primaryWeapon: "Hammer",
  primaryAffix: "Havoc",
  primaryProc: "PneumaticMaul",
  secondaryWeapon: "Fist",
  secondaryAffix: "Destruction",
  secondaryProc: "None",
  combatPower: 1350,
  criticalChance: 30,
  criticalPower: 120,
  basicSignet: 73,
  powerSignet: 19,
  eliteSignet: 41,
  waistSignet: 49,
  luckSignet: 12,
  head: "Ashes",
  neck: "SeedOfAgression",
  luck: "ColdSilverDice",
  gadget: "ValiMetabolic",
  exposed: true,
  openingShot: true,
  headCdr: false,
  waistCdr: true,
  passive1: "Obliterate",
  passive2: "FastandFurious",
  passive3: "Outrage",
  passive4: "Berserker",
  passive5: "UnbridledWrath",
  apl:
    "Fist.Savagery, Buff.UnstoppableForce.Active\r\n" +
    "Hammer.Seethe, Buff.UnstoppableForce.Active\r\n" +
    "Hammer.UnstoppableForce, Rage > 50 || Hammer.Energy > 8\r\n" +
    "Hammer.Demolish, Buff.UnstoppableForce.Active\r\n" +
    "Hammer.Demolish, Rage > 60 || Hammer.Energy > 13\r\n" +
    "Hammer.Smash",
};

const Chaos: IWeaponPreset = {
  primaryWeapon: "Chaos",
  primaryAffix: "Havoc",
  primaryProc: "WarpedVisage",
  secondaryWeapon: "Shotgun",
  secondaryAffix: "Destruction",
  secondaryProc: "None",
  combatPower: 1350,
  criticalChance: 30,
  criticalPower: 120,
  basicSignet: 73,
  powerSignet: 19,
  eliteSignet: 41,
  waistSignet: 49,
  luckSignet: 12,
  head: "Ashes",
  neck: "SeedOfAgression",
  luck: "ColdSilverDice",
  gadget: "ValiMetabolic",
  exposed: true,
  openingShot: true,
  headCdr: false,
  waistCdr: true,
  passive1: "Disintegrate",
  passive2: "FracturedExistence",
  passive3: "BodyDouble",
  passive4: "ButterflyEffect",
  passive5: "BlessingofOcted",
  apl:
    "Chaos.Pandemonium\r\n" +
    "Chaos.Breakdown\r\n" +
    "Shotgun.ShellSalvage\r\n" +
    "Shotgun.RagingShot\r\n" +
    "Chaos.Deconstruct",
};

const Fist: IWeaponPreset = {
  primaryWeapon: "Fist",
  primaryAffix: "Destruction",
  primaryProc: "Bladed Gauntlets",
  secondaryWeapon: "Shotgun",
  secondaryAffix: "Destruction",
  secondaryProc: "None",
  combatPower: 1350,
  criticalChance: 30,
  criticalPower: 120,
  basicSignet: 73,
  powerSignet: 19,
  eliteSignet: 41,
  waistSignet: 49,
  luckSignet: 12,
  head: "Ashes",
  neck: "SeedOfAgression",
  luck: "ColdSilverDice",
  gadget: "ValiMetabolic",
  exposed: true,
  openingShot: true,
  headCdr: false,
  waistCdr: true,
  passive1: "SmellFear",
  passive2: "WildNature",
  passive3: "Brutality",
  passive4: "KillerInstinct",
  passive5: "Gore",
  apl:
    "Fist.Savagery\r\n" +
    "Fist.PrimalInstinct\r\n" +
    "Shotgun.ShelLSalvage\r\n" +
    "Fist.Mangle\r\n" +
    "Shotgun.RagingShot\r\n" +
    "Fist.Trash",
};

const Pistol: IWeaponPreset = {
  primaryWeapon: "Pistol",
  primaryAffix: "Energy",
  primaryProc: "SovTechHarmonisers",
  secondaryWeapon: "Fist",
  secondaryAffix: "Destruction",
  secondaryProc: "None",
  combatPower: 1350,
  criticalChance: 30,
  criticalPower: 120,
  basicSignet: 73,
  powerSignet: 19,
  eliteSignet: 41,
  waistSignet: 49,
  luckSignet: 12,
  head: "Ashes",
  neck: "SeedOfAgression",
  luck: "ColdSilverDice",
  gadget: "ValiMetabolic",
  exposed: true,
  openingShot: true,
  headCdr: false,
  waistCdr: true,
  passive1: "LethalAim",
  passive2: "Jackpot",
  passive3: "BeginnersLuck",
  passive4: "FatalShot",
  passive5: "FocusedFire",
  apl:
    "Pistol.KillBlind, Buff.RedChambers || Buff.BlueChambers || Buff.WhiteChambers\r\n" +
    "Fist.Savagery\r\n" +
    "Pistol.Flourish, Pistol.Energy < 11\r\n" +
    "Pistol.TrickShot, Buff.Savagery.Active\r\n" +
    "Pistol.Dualshot, Pistol.Energy > 12 || Buff.RedChambers || Buff.BlueChambers || Buff.WhiteChambers\r\n" +
    "Pistol.HairTrigger",
};

const Blood: IWeaponPreset = {
  primaryWeapon: "Blood",
  primaryAffix: "Energy",
  primaryProc: "EldritchTome",
  secondaryWeapon: "Fist",
  secondaryAffix: "Destruction",
  secondaryProc: "None",
  combatPower: 1350,
  criticalChance: 30,
  criticalPower: 120,
  basicSignet: 73,
  powerSignet: 19,
  eliteSignet: 41,
  waistSignet: 49,
  luckSignet: 12,
  head: "Ashes",
  neck: "SeedOfAgression",
  luck: "ColdSilverDice",
  gadget: "ValiMetabolic",
  exposed: true,
  openingShot: true,
  headCdr: false,
  waistCdr: true,
  passive1: "Flay",
  passive2: "Contaminate",
  passive3: "Desolate",
  passive4: "Defilement",
  passive5: "CrimsonPulse",
  apl:
    "Fist.Savagery, Corruption >= 10 && EldritchScourge.Cooldown <= 0 && Desecrate.Cooldown <= 0\r\n" +
    "Blood.EldritchScourge, Corruption > 0 && Buff.Savagery.Duration > 2\r\n" +
    "Blood.Desecrate, Buff.Savagery.Active\r\n" +
    "Blood.Maleficium, Blood.Energy > 10\r\n" +
    "Blood.Torment\r\n",
};

const Blade: IWeaponPreset = {
  primaryWeapon: "Blade",
  primaryAffix: "Energy",
  primaryProc: "PlasmaForged",
  secondaryWeapon: "Fist",
  secondaryAffix: "Destruction",
  secondaryProc: "None",
  combatPower: 1350,
  criticalChance: 30,
  criticalPower: 120,
  basicSignet: 73,
  powerSignet: 19,
  eliteSignet: 41,
  waistSignet: 49,
  luckSignet: 12,
  head: "Ashes",
  neck: "SeedOfAgression",
  luck: "ColdSilverDice",
  gadget: "ValiMetabolic",
  exposed: true,
  openingShot: true,
  headCdr: false,
  waistCdr: true,
  passive1: "KeenEdge",
  passive2: "StormSurge",
  passive3: "Masterpiece",
  passive4: "MastersFocus",
  passive5: "WarriorsSpirit",
  apl:
    "Fist.Savagery\r\n" +
    "Blade.Hone\r\n" +
    "Blade.SupremeHarmony\r\n" +
    "Blade.SpiritBlade\r\n" +
    "Blade.Tsunami\r\n" +
    "Blade.FlowingStrike\r\n",
};

const Rifle: IWeaponPreset = {
  primaryWeapon: "Rifle",
  primaryAffix: "Energy",
  primaryProc: "InfernalLoader",
  secondaryWeapon: "Fist",
  secondaryAffix: "Destruction",
  secondaryProc: "BladedGauntlets",
  combatPower: 1350,
  criticalChance: 30,
  criticalPower: 120,
  basicSignet: 73,
  powerSignet: 19,
  eliteSignet: 41,
  waistSignet: 49,
  luckSignet: 12,
  head: "Ashes",
  neck: "SeedOfAgression",
  luck: "ColdSilverDice",
  gadget: "ValiMetabolic",
  exposed: true,
  openingShot: true,
  headCdr: false,
  waistCdr: true,
  passive1: "Stability",
  passive2: "BackupPlan",
  passive3: "SecondaryExplosion",
  passive4: "SlowBurn",
  passive5: "ExplosivesExpert",
  apl:
    "Fist.Savagery, HighExplosiveGrenade.Cooldown <= 0\r\n" +
    "Rifle.HighExplosiveGrenade, Buff.Savagery" +
    "Rifle.IncendiaryGrenade, HighExplosiveGrenade.Cooldown >6\r\n" +
    "Rifle.BurstFire\r\n" +
    "Rifle.PlacedShot",
};

const Elemental: IWeaponPreset = {
  primaryWeapon: "Elemental",
  primaryAffix: "Havoc",
  primaryProc: "UnstableElectronCore",
  secondaryWeapon: "Fist",
  secondaryAffix: "Destruction",
  secondaryProc: "None",
  combatPower: 1350,
  criticalChance: 30,
  criticalPower: 120,
  basicSignet: 73,
  powerSignet: 19,
  eliteSignet: 41,
  waistSignet: 49,
  luckSignet: 12,
  head: "Ashes",
  neck: "SeedOfAgression",
  luck: "ColdSilverDice",
  gadget: "ValiMetabolic",
  exposed: true,
  openingShot: true,
  headCdr: false,
  waistCdr: true,
  passive1: "Glaciate",
  passive2: "Superconductor",
  passive3: "CrystallisedBlaze",
  passive4: "Combustion",
  passive5: "Thermodynamo",
  apl:
    "Fist.Savagery\r\n" +
    "Elementalism.IceBeam\r\n" +
    "Elementalism.CrystallisedFlame\r\n" +
    "Elementalism.Mjolnir\r\n" +
    "Elementalism.Fireball",
};

const Shotgun: IWeaponPreset = {
  primaryWeapon: "Shotgun",
  primaryAffix: "Energy",
  primaryProc: "IfritanDespoiler",
  secondaryWeapon: "Fist",
  secondaryAffix: "Destruction",
  secondaryProc: "None",
  combatPower: 1350,
  criticalChance: 30,
  criticalPower: 120,
  basicSignet: 73,
  powerSignet: 19,
  eliteSignet: 41,
  waistSignet: 49,
  luckSignet: 12,
  head: "Ashes",
  neck: "SeedOfAgression",
  luck: "ColdSilverDice",
  gadget: "ValiMetabolic",
  exposed: true,
  openingShot: true,
  headCdr: false,
  waistCdr: true,
  passive1: "PointBlankShot",
  passive2: "WitheringSalvo",
  passive3: "SalvageExpert",
  passive4: "OddsandEvens",
  passive5: "MunitionsExpert",
  apl:
    "Fist.Savagery\r\n" +
    "Shotgun.FullSalvo\r\n" +
    "Shotgun.ShellSalvage\r\n" +
    "Shotgun.RagingShot\r\n" +
    "Shotgun.PumpAction\r\n",
};

export default {
  Hammer,
  Chaos,
  Fist,
  Pistol,
  Blood,
  Blade,
  Rifle,
  Elemental,
  Shotgun,
};
