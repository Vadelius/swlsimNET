export interface AbilityBuff {
    active: boolean;
    name: string;
    maxDuration: number;
    duration: number;
    maxCooldown: number;
    cooldown: number;
    activationRounds: number[];
    deactivationRounds: any[];
    maxBonusCritChance: number;
    maxBonusCritMultiplier: number;
    maxBonusDamageMultiplier: number;
    maxBonusBaseDamageMultiplier: number;
    weaponType: number;
    specificWeaponTypeBonus: boolean;
    bonusDamageMultiplier: number;
    bonusBaseDamageMultiplier: number;
    bonusCritChance: number;
    bonusCritMultiplier: number;
    gimmickGainPerSec: number;
    energyGainPerSec: number;
}

export interface DistinctSpellCast {
    primaryGimmickCost: number;
    primaryGimmickReduce: number;
    secondaryGimmickCost: number;
    secondaryGimmickReduce: number;
    primaryGimmickGain: number;
    secondaryGimmickGain: number;
    primaryGimmickRequirement: number;
    primaryGimmickGainOnCrit: number;
    name: string;
    baseDamage: number;
    dotExpirationBaseDamage: number;
    baseDamageCrit: number;
    weaponType: number;
    dotDuration: number;
    spellType: number;
    abilityType: number;
    primaryCost: number;
    secondaryCost: number;
    primaryGain: number;
    secondaryGain: number;
    maxCooldown: number;
    cooldown: number;
    castTime: number;
    channelTicks: number;
    dotTicks: number;
    bonusCritChance: number;
    bonusCritPower: number;
    args: string;
    elementalType?: any;
    passiveBonusSpell?: any;
    abilityBuff: AbilityBuff;
}

export interface DistinctBuff {
    name: string;
    maxDuration: number;
    duration: number;
    maxCooldown: number;
    cooldown: number;
    active: boolean;
    activationRounds: number[];
    deactivationRounds: any[];
    maxBonusCritChance: number;
    maxBonusCritMultiplier: number;
    maxBonusDamageMultiplier: number;
    maxBonusBaseDamageMultiplier: number;
    weaponType: number;
    specificWeaponTypeBonus: boolean;
    bonusDamageMultiplier: number;
    bonusBaseDamageMultiplier: number;
    bonusCritChance: number;
    bonusCritMultiplier: number;
    gimmickGainPerSec: number;
    energyGainPerSec: number;
}

export interface OneBuilder {
    m_MaxCapacity: number;
    Capacity: number;
    m_StringValue: string;
    m_currentThread: number;
}

export interface SpellBreakdownList {
    name: string;
    damagePerSecond: number;
    dpsPercentage: number;
    executes: number;
    damagePerExecution: number;
    spellType: string;
    amount: number;
    average: number;
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
    _distinctSpellCast: DistinctSpellCast[];
    _distinctBuffs: DistinctBuff[];
    _oneBuilder: OneBuilder;
    totalCrits: number;
    totalHits: number;
    totalDamage: number;
    totalDps: number;
    fightDebug: string;
    lowestDps: number;
    highestDps: number;
    spellBreakdown: string;
    spellBreakdownList: SpellBreakdownList[];
    buffBreakdownList: BuffBreakdownList[];
    energyList: EnergyList[];
    pieStuff?: any;
}



