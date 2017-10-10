using swlsimNET.ServerApp.Models;
using swlsimNET.ServerApp.Weapons;

namespace swlsimNET.ServerApp.Spells.Elemental
{
    public class Fireball : Spell
    {
        public Fireball(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Elemental;
            AbilityType = AbilityType.Basic;
            BaseDamage = 1.175;
            Args = args;
            PrimaryGimmickGain = 6;
            ElementalType = "Fire";
        }


    }
    public class ChainLightning : Spell
    {
        public ChainLightning(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Elemental;
            AbilityType = AbilityType.Power;
            BaseDamage = 1.26;
            PrimaryCost = 3;
            Args = args;
            PrimaryGimmickGain = 20;
            ElementalType = "Lightning";
            // AOE
        }
    }
    public class FlashFreeze : Spell
    {
        public FlashFreeze(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Elemental;
            AbilityType = AbilityType.Special;
            BaseDamage = 1.45;
            PrimaryCost = 2;
            Args = args;
            PrimaryGimmickCost = 30; //Can be used while overheated
            ElementalType = "Cold";
        }
    }

    public class Mjolnir : Spell
    {
        public Mjolnir(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Elemental;
            AbilityType = AbilityType.Power;
            PrimaryCost = 5;
            PrimaryGimmickGain = 30;
            BaseDamage = 3.45;
            Args = args;
            ElementalType = "Lightning";
            // If passive is slotted and it crits 32% basedamage more.
        }
    }
    public class IceBeam : Spell
    {
        public IceBeam(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Elemental;
            AbilityType = AbilityType.Elite;
            PrimaryCost = 4;
            PrimaryGimmickCost = 30;
            SpellType = SpellType.Cast;
            CastTime = 2;
            BaseDamage = 7;
            Args = args;
            ElementalType = "Cold";
            // If passive is slotted and it crits 32% basedamage more.
        }
    }
    public class CrystallizedFrost : Spell
    {
        public CrystallizedFrost(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Elemental;
            AbilityType = AbilityType.Special;
            SpellType = SpellType.Dot;
            PrimaryCost = 2;
            PrimaryGimmickCost = 30;
            BaseDamage = 0.36;
            DotDuration = 3; // Actually 9 but damage per 3 sec.
            PrimaryGimmickCost = 8; // 8 per sec for 3 sec
            Args = args;
            ElementalType = "Cold";
            //AOE
        }
    }
    public class CrystallizedFlame : Spell
    {
        public CrystallizedFlame(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Elemental;
            AbilityType = AbilityType.Special;
            SpellType = SpellType.Dot;
            PrimaryCost = 2;
            PrimaryGimmickGain = 25;
            BaseDamage = 0.58;
            DotDuration = 4; // Actually 10 but for every 2.5s
            Args = args;
            ElementalType = "Fire";
            //AOE
        }
    }
    public class Blizzard : Spell
    {
        public Blizzard(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Elemental;
            AbilityType = AbilityType.Elite;
            SpellType = SpellType.Dot;
            PrimaryCost = 4;
            PrimaryGimmickCost = 30;
            BaseDamage = 0.68;
            DotDuration = 8;
            Args = args;
            ElementalType = "Cold";
            //AOE
        }
    }
    public class FireBolt : Spell
    {
        public FireBolt(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Elemental;
            AbilityType = AbilityType.Power;
            PrimaryCost = 3;
            PrimaryGimmickGain = 20;
            BaseDamage = 2.52;
            Args = args;
            ElementalType = "Fire";
        }
    }
    public class Inferno : Spell
    {
        public Inferno(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Elemental;
            AbilityType = AbilityType.Power;
            PrimaryCost = 5;
            PrimaryGimmickGain = 30;
            BaseDamage = 1.71;
            Args = args;
            ElementalType = "Fire";
        }
    }
    public class Flashpoint : Spell
    {
        public Flashpoint(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Elemental;
            AbilityType = AbilityType.Special;
            PrimaryCost = 5;
            ElementalType = "Lightning";
            // For 4 seconds all elemetnal abilities deal damage equial to as if our heat was MAX.
        }
    }
    public class Overload : Spell
    {
        public Overload(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Elemental;
            AbilityType = AbilityType.Elite;
            PrimaryCost = 4;
            SpellType = SpellType.Channel;
            ChannelTicks = 3;
            CastTime = 3;
            BaseDamage = 2.71;
            PrimaryGimmickCost = 10; // -30 first sec. +10 second sec. +10 third sic.
            ElementalType = "Lightning";
        }
    }
}
