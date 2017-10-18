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
