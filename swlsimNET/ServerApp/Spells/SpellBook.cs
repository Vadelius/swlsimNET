using swlsimNET.ServerApp.Models;
using swlsimNET.ServerApp.Spells;
using swlsimNET.ServerApp.Weapons;

namespace swlsimNET.ServerApp.Spells.Buffs
{
    #region General


    #endregion
}

namespace swlsimNET.ServerApp.Spells.Buffs
{
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
}

namespace swlsimNET.ServerApp.Spells.Items
{
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

    #endregion
}