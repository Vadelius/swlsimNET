export interface IFormNames {
  name: string;
  displayName: string;
}

export interface ISpellBreakdownList {
  name: string;
  dps: number;
  dpsPercent: number;
  executes: number;
  dpe: number;
  ticks: number;
  critChance: number;
}

export interface IBuffBreakdownList {
  name: string;
  executes: number;
  refresh: number;
  interval: number;
  uptime: number;
}

export interface IEnergyList {
  time: number;
  primaryEnergy: number;
  secondaryEnergy: number;
}

export interface IGimmickList {
  time: number;
  primaryGimmick: number;
  secondaryGimmick: number;
}

export interface IRootObject {
  totalCrits: number;
  totalHits: number;
  totalDamage: number;
  totalDps: number;
  fightDebug: string;
  lowestDps: number;
  highestDps: number;
  totalSpellExecutes: number;
  spellBreakdownList: ISpellBreakdownList[];
  buffBreakdownList: IBuffBreakdownList[];
  energyList: IEnergyList[];
  gimmickList: IGimmickList[];
}

export interface IWeaponPreset {
  primaryWeapon: string;
  primaryAffix: string;
  primaryProc: string;
  secondaryWeapon: string;
  secondaryAffix: string;
  secondaryProc: string;
  combatPower: number;
  criticalChance: number;
  criticalPower: number;
  basicSignet: number;
  powerSignet: number;
  eliteSignet: number;
  waistSignet: number;
  luckSignet: number;
  head: string;
  neck: string;
  luck: string;
  gadget: string;
  exposed: boolean;
  openingShot: boolean;
  headCdr: boolean;
  waistCdr: boolean;
  passive1: string;
  passive2: string;
  passive3: string;
  passive4: string;
  passive5: string;
  apl: string;
}
