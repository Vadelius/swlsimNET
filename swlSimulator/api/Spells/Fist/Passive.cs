using swlSimulator.api.Models;
using swlSimulator.api.Weapons;

namespace swlSimulator.api.Spells.Fist
{
    public class Rip : Spell
    {
        public Rip(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Fist;
            SpellType = SpellType.Passive;
            BaseDamage = 0;
            // Savage Sweep: PBAoE 0,077CP Dot every 1s for 5s
        }
    }

    public class Gore : Spell
    {
        public Gore(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Fist;
            SpellType = SpellType.Passive;
            BaseDamage = 0;
            // New ability in Frenzied Wrath, Charge: 15m must be atleast 1m away,
            // TAoE 1.21CP, 0,38CP Dot every 0.5s for 5s
        }
    }

    public class Maul : Spell
    {
        public Maul(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Fist;
            SpellType = SpellType.Passive;
            BaseDamage = 0;
            // Mangle: 0,11CP Dot every 1s for 5s
        }
    }

    public class Rampage : Spell
    {
        public Rampage(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Fist;
            SpellType = SpellType.Passive;
            BaseDamage = 0;
            // Berserk: 5s Stun, If fist dot active increase damage to 0.95CP 
        }
    }

    public class Bloodbath : Spell
    {
        public Bloodbath(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Fist;
            SpellType = SpellType.Passive;
            BaseDamage = 0;
            // Eviscerate: If successfull interrupt 0,38CP Dot every 1s for 5s
        }
    }

    public class Brutality : Spell
    {
        public Brutality(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Fist;
            SpellType = SpellType.Passive;
            BaseDamage = 0;
            // Ferocity: If used in conjunction with fist dot 0,52CP Dot every 1s for 5s
        }
    }

    public class KillerInstinct : Spell
    {
        public KillerInstinct(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Fist;
            SpellType = SpellType.Passive;
            BaseDamage = 0;
            // 50% Dot Crit BaseDamage during Primal Instinct
        }
    }

    public class Furor : Spell
    {
        public Furor(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Fist;
            SpellType = SpellType.Passive;
            BaseDamage = 0;
            // Whenever you use 3 Fist power in succession gain 3 Fury
        }
    }

    public class SecondWind : Spell
    {
        public SecondWind(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Fist;
            SpellType = SpellType.Passive;
            BaseDamage = 0;
            // Whenever Frenzied Wrath ends gain 4 Fist Energy
        }
    }

    public class SmellFear : Spell
    {
        public SmellFear(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Fist;
            SpellType = SpellType.Passive;
            BaseDamage = 0;
            // Whenever you enter combat and every 8s afterwards sense fear in a nearby enemy,
            // Fist Power abilites generates 8 Fury
        }
    }

    public class BloodyMist : Spell
    {
        public BloodyMist(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Fist;
            SpellType = SpellType.Passive;
            BaseDamage = 0;
            // Whenever you Crit with Fist abilities deal an additional 0.26CP BaseDamage
        }
    }

    public class PotentWrath : Spell
    {
        public PotentWrath(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Fist;
            SpellType = SpellType.Passive;
            BaseDamage = 0;
            // Whenever Frenzied Wrath ends PBAoE 0,19Cp Dot every 1s for 5s
        }
    }

    public class KeenSenses : Spell
    {
        public KeenSenses(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Fist;
            SpellType = SpellType.Passive;
            BaseDamage = 0;
            // Enemy health below 35% Deal additional 0,26CP BaseDamage
        }
    }

    public class WildNature : Passive
    {
        public WildNature(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Fist;
            BaseDamage = 0;
            PrimaryGain = 1;
            
            // TODO: 1 Fury per sec in combat
        }
    }
}