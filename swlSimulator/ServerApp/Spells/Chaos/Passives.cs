﻿using swlsimNET.ServerApp.Models;
using swlsimNET.ServerApp.Weapons;

namespace swlsimNET.ServerApp.Spells.Chaos
{
    public class ParadoxControl : Passive
    {
        public ParadoxControl(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Chaos;
            SpellTypes.Add(typeof(Deconstruct));
            // +1 Paradox for each Paradox generated by Schism.
        }
    }

    public class Dissolution : Passive
    {
        public Dissolution(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Chaos;
            SpellTypes.Add(typeof(Entropy));
            // If you generate a paradox via %8 damage Entropy bonus gets set to 24% for the remainder of the duration..
        }
    }

    public class Disintegrate : Passive
    {
        public Disintegrate(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Chaos;
            SpellTypes.Add(typeof(Breakdown));
            BaseDamage = 0.1475;
        }
    }

    public class FracturedExistence : Passive
    {
        public FracturedExistence(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Chaos;
            SpellTypes.Add(typeof(Pandemonium));
            BaseDamage = 2.75;
        }
    }

    public class Duality : Passive
    {
        public Duality(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Chaos;
            SpellTypes.Add(typeof(TumultousWhisper));
            // If TumultousWHisper causes us to exceed 8 paradoxes... We get a Doppleganger();
            // See chaos weapon modeling. (Weapons.Chaos.Doppleganger)
        }
    }

    public class ResonantCascade : Passive
    {
        public ResonantCascade(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Chaos;
            // (18.5% chance to generate 1 paradox) per second.
        }
    }

    public class BodyDouble : Passive
    {
        public BodyDouble(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Chaos;
            SpellTypes.Add(typeof(Weapons.Chaos.Doppleganger));
            BaseDamageModifier = 1.25; // 125% damage.
        }
    }

    public class EyeOfTheRuinStorm : Passive
    {
        public EyeOfTheRuinStorm(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Chaos;
            SpellTypes.Add(typeof(Weapons.Chaos.Singularity));
            BaseDamage = 1.68;
            // Singularity Base Damage increased to 4.35
        }
    }

    public class ButterflyEffect : Passive
    {
        public ButterflyEffect(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Chaos;
            // Whenever you deal damage with chaos abilities -
            // we have 10% to deal 0.58 * CP and +1 Paradox
        }
    }

    public class BlessingOfOcted : Passive
    {
        public BlessingOfOcted(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Chaos;
            // The chance of dealing damage divislbe by 8 increases by 6.25% ......
        }
    }
}