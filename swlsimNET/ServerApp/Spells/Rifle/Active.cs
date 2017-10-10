using swlsimNET.ServerApp.Models;
using swlsimNET.ServerApp.Weapons;

namespace swlsimNET.ServerApp.Spells.Rifle
{
    public class PlacedShot : Spell
    {
        public PlacedShot(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Rifle;
            BaseDamage = 1.17;
            Args = args;
        }
    }

    public class FullAuto : Spell
    {
        public FullAuto(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Rifle;
            AbilityType = AbilityType.Power;
            SpellType = SpellType.Channel;
            CastTime = 1;
            ChannelTicks = 4;
            PrimaryCost = 3;
            BaseDamage = 0.31; //Multihit 4times 0.31CP     
            Args = args;
        } // 37.5% Chance to load grenade.
    }

    public class LockAndLoad : Spell
    {
        public LockAndLoad(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Rifle;
            AbilityType = AbilityType.Special;
            SpellType = SpellType.Instant;
            PrimaryGain = 4;
            MaxCooldown = 20;
            Args = args;
        }
    }

    public class BurstFire : Spell
    {
        public BurstFire(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Rifle;
            AbilityType = AbilityType.Power;
            SpellType = SpellType.Channel;
            CastTime = 1;
            ChannelTicks = 3;
            PrimaryCost = 5;
            BaseDamage = 1.14; //Multihit 3times 1.14CP       
            Args = args;
        } //65% Chance to load grenade. 
    }

    public class RedMist : Spell
    {
        public RedMist(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Rifle;
            AbilityType = AbilityType.Elite;
            SpellType = SpellType.Cast;
            CastTime = 2.0m;
            PrimaryCost = 4;
            MaxCooldown = 20;
            BaseDamage = 6.97;
            Args = args;
        }
    }

    public class IncendiaryGrenade : Spell
    {
        public IncendiaryGrenade(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Rifle;
            AbilityType = AbilityType.Special;
            SpellType = SpellType.Dot;
            PrimaryCost = 2;
            PrimaryGimmickCost = 1;
            MaxCooldown = 4;
            BaseDamage = 0.51;
            DotDuration = 8;
            Args = args;
        } // Uncooked Damage: 0.13CP, Requires a grenade to activate. GTAoE
    }

    public class TacticalRetreat : Spell
    {
        public TacticalRetreat(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Rifle;
            AbilityType = AbilityType.Special;
            PrimaryCost = 2;
            MaxCooldown = 20;
            BaseDamage = 4.09;
            Args = args;
        } // 5m Dash backwards, If used with grenade active distance increased to 10m and CD reduced to 4s(can only happen every 15s)

        // Uncooked Damage: 1.03CP, 70% slow 4s
    }

    public class HighExplosiveGrenade : Spell
    {
        public HighExplosiveGrenade(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Rifle;
            AbilityType = AbilityType.Elite;
            SpellType = SpellType.Dot;
            PrimaryCost = 4;
            PrimaryGimmickCost = 1;
            MaxCooldown = 20;
            BaseDamage = 1.48;
            DotDuration = 8;
            Args = args;
        } // Uncooked damage: 0.72CP, Requires a grenade to activate, TAoE
    }
}