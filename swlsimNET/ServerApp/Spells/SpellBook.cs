using swlsimNET.ServerApp.Models;
using swlsimNET.ServerApp.Weapons;

namespace swlsimNET.ServerApp.Spells
{
    #region General


    #endregion

    #region Buffs & Debuffs

    public class OpeningShot : Buff
    {
        public OpeningShot()
        {
            MaxDuration = 8;
            MaxCooldown = 20;
            MaxBonusCritMultiplier = 0.3;
        }
    }

    public class Savagery : Buff
    {
        public Savagery()
        {
            MaxDuration = 6;
            MaxCooldown = 20;
            MaxBonusBaseDamageMultiplier = 0.15;
        }
    }

    public class Exposed : Debuff
    {
        public Exposed()
        {
            MaxDuration = 0; // uptime 100%
            MaxCooldown = 0;
            MaxBonusDamageMultiplier = 0.1;
        }
    }

// TODO: Add Glaciate 

    #endregion

    #region Items

    public class Ashes : Spell
    {
        public Ashes(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.None;
            SpellType = SpellType.Procc;
            BaseDamage = 0.15;
        }
    }

    public class SeedOfAggression : Spell
    {
        public SeedOfAggression(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.None;
            SpellType = SpellType.Procc;
        }
    }

    public class ColdSilver : Spell
    {
        public ColdSilver(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.None;
            SpellType = SpellType.Procc;
        }
    }

    public class GamblersSoul : Spell
    {
        public GamblersSoul(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.None;
            SpellType = SpellType.Procc;
            BaseDamage = 0.10;
        }
    }
    public class ChokerOfShedBlood : Spell
    {
        public ChokerOfShedBlood(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.None;
            SpellType = SpellType.Procc;
            BaseDamage = 0.20;
        }
    }
    public class EgonPendant : Spell
    {
        public EgonPendant(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.None;
            SpellType = SpellType.Dot;
            BaseDamage = 0.06;
            DotDuration = 5;
            //TODO: "Your damage is increased by 1.75% for each enemy affected by this effect." <- For Dot Duration.
        }
    }

    #endregion
}