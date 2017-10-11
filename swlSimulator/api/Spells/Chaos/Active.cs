using swlSimulator.api.Models;
using swlSimulator.api.Weapons;

namespace swlSimulator.api.Spells.Chaos
{
    public class Deconstruct : Spell
    {
        public Deconstruct(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Chaos;
            AbilityType = AbilityType.Basic;
            BaseDamage = 1.175;
            SpellType = SpellType.Channel;
            ChannelTicks = 3;
            CastTime = 1;
            Args = args;
        }
    }
    public class Chism : Spell
    {
        public Chism(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Chaos;
            AbilityType = AbilityType.Power;
            PrimaryCost = 3;
            Args = args;
            BaseDamage = 1.26;

            // PBAoE
        }
    }
    public class Entropy : Spell
    {
        public Entropy(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Chaos;
            AbilityType = AbilityType.Special;
            PrimaryCost = 2;
            Args = args;
            // 10% Chaos Damage & 8.6% Critpower for 8 seconds.

            // PBAoE
        }
    }

    public class Breakdown : Spell
    {
        public Breakdown(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Chaos;
            AbilityType = AbilityType.Power;
            SpellType = SpellType.Channel;
            ChannelTicks = 4;
            CastTime = 1;
            BaseDamage = 0.8575; // 3.43 (4 hits)
            PrimaryCost = 5;
            Args = args;
        }
    }
    public class Pandemonium : Spell
    {
        public Pandemonium(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Chaos;
            AbilityType = AbilityType.Elite;
            BaseDamage = 5.8;
            PrimaryCost = 4;
            Args = args;
            // PBAoE
        }
    }
    public class TumultousWhisper : Spell
    {
        public TumultousWhisper(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Chaos;
            AbilityType = AbilityType.Special;
            BaseDamage = 0.45;
            PrimaryCost = 2;
            Args = args;
            // AoE Purge 2x10m column And damage dealt by this WIKLL ALWAYS BE DIVISIBLE BY EIGHT...
            // Resulting in 2-4 paradoxes. (?) WHY?
        }
    }
}