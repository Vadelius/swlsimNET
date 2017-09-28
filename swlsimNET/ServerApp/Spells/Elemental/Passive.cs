using swlsim.Spells;
using swlsimNET.ServerApp.Models;
using swlsimNET.ServerApp.Weapons;

namespace swlsimNET.ServerApp.Spells.Elemental
{
    public class StaticCharge : Passive
    {
        public StaticCharge(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Elemental;
            SpellTypes.Add(typeof(ChainLightning));
            // Adds a 0.39 * CP dot for 3 seconds.
        }
    }

    public class Cryoblast : Passive
    {
        public Cryoblast(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Elemental;
            SpellTypes.Add(typeof(FlashFreeze));
            // IF Flash freezed is used when heat is <30 you get 20% elemental damage for 5 seconds
        }
    }

    public class Superconductor : Passive
    {
        public Superconductor(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Elemental;
            SpellTypes.Add(typeof(Mjolnir));
            //Mjolnir can't miss.
            BaseDamageCritModifier = 0.32;
        }
    }

    public class Glaciate : Passive
    {
        public Glaciate(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Elemental;
            SpellTypes.Add(typeof(IceBeam));
            //Attacks against the target with Ice Beam debuff ( 5seconds)
            //Deal 0.5 * CP extra.
        }
    }

    public class IcySteps : Passive
    {
        public IcySteps(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Elemental;
            SpellTypes.Add(typeof(CrystallizedFrost));
            PrimaryGimmickCost = 7; //7 extra per sec upto a total of 15 per second (for 3 sec)
        }
    }

    public class CrystallizedBlaze : Passive
    {
        public CrystallizedBlaze(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Elemental;
            SpellTypes.Add(typeof(CrystallizedFlame));
            BaseDamage = 0.7; //0.7 * CP for DotDuration (4 seconds)
        }
    }

    public class FridgidTempest : Passive
    {
        public FridgidTempest(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Elemental;
            SpellTypes.Add(typeof(Blizzard));
            PrimaryCost = 4; // 4 per second (8 seconds)
        }
    }

    public class BlazingBolts : Passive
    {
        public BlazingBolts(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Elemental;
            SpellTypes.Add(typeof(FireBolt));
            //For each succussive FireBolt use the next FireBolt generates +1 more heat and does 5% more damage (stacks to 4 times)
        }
    }

    public class ScorchedEarth : Passive
    {
        public ScorchedEarth(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Elemental;
            SpellTypes.Add(typeof(Inferno));
            //If heat > 30 Inferno drops a 0.06 * CP for 10 seconds DOT.
        }
    }

    public class ElementalForce : Passive
    {
        public ElementalForce(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Elemental;
            SpellTypes.Add(typeof(Overload));
            //Heat reduction increased by 10 (so total -40)
            //Creates 0.42 * CP attack
            //Creates 0.14 * CP dot for 6 seconds
        }
    }

    public class ElementalControl : Passive
    {
        public ElementalControl(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Elemental;
            //TODO: Make an Elemental school (Fire/Cold/Electrical) attribute on Spells since passives have some interactions.
            //If dealing fire damage create 0.05 * CP dot for 5 sec.
            //If dealing electrical damage two times in 3 seconds (after first hit lands) deal CP * 0.125...
        }
    }

    public class Calorimetry : Passive
    {
        public Calorimetry(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Elemental;
            // Start combat with 15 heat instead of 0.
        }
    }

    public class Thermodynamo : Passive
    {
        public Thermodynamo(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Elemental;
            // When Heat >= 100: GimmickMaxBonusDamageTier =+ 4.5%
        }
    }

    public class Combustion : Passive
    {
        public Combustion(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Elemental;
            // When Fire or Electrical spell Crits, deal 0.25 * CP bonusattack && Heat +5 UNLESS Heat is already >= 50
        }
    }

    public class LingeringFrost : Passive
    {
        public LingeringFrost(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Elemental;
            // When using any Cold ability we lose 2 heat per sec for 5 sec.
        }
    }

    public class Thermocoupler : Passive
    {
        public Thermocoupler(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Elemental;
            //Using a Fire ability means next Electrical (if such) will reduce heat by 3.
            //Using a Electrical ability means next Fire (if such) will reduce heat by 3.
        }
    }

    public class Supercooler : Passive
    {
        public Supercooler(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Elemental;
            //Gain 1 "Supercooler" when entering combat and one every 5 seconds after that.
            //Every stack of Supercooler reduces your heat by 2*Supercoolerstack. Stacks upto a max of 5
        }
    }

    public class MaxwellsDemon : Passive
    {
        public MaxwellsDemon(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Elemental;
            //Whenever you crit with an Elemental ability you "store" your heat level for 4 seconds.
            //After 4 seconds you reduce your heat to the level it was 4 seconds earlier IF-
            //the stored level is lower then the current heat level.
            //This can only occur once every 5 seconds AND IF heat > 50
        }
    }
}