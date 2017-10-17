export interface FormNames {
    name: string;
    displayName: string;
}

export interface SpellBreakdownList {
    name: string;
    dps: number;
    dpsPercent: number;
    executes: number;
    dpe: number;
    ticks: number;
    critChance: number;
}

export interface BuffBreakdownList {
    name: string;
    executes: number;
    refresh: number;
    interval: number;
    uptime: number;
}

export interface EnergyList {
    time: number;
    primary: number;
    secondary: number;
    pgimmick: number;
    sgimmick: number;
}

export interface RootObject {
    totalCrits: number;
    totalHits: number;
    totalDamage: number;
    totalDps: number;
    fightDebug: string;
    lowestDps: number;
    highestDps: number;
    spellBreakdownList: SpellBreakdownList[];
    buffBreakdownList: BuffBreakdownList[];
    energyList: EnergyList[];
}