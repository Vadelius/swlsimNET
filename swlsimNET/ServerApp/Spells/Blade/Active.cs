using swlsim.Spells;
using swlsimNET.ServerApp.Models;
using swlsimNET.ServerApp.Weapons;

namespace swlsimNET.ServerApp.Spells.Blade
{
    public class FlowingStrike : Spell
    {
        public FlowingStrike(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Blade;
            AbilityType = AbilityType.Basic;
            BaseDamage = 1.175;
            Args = args;
        }
    }

    public class Hurricane : Spell
    { 
        public Hurricane(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Blade;
            AbilityType = AbilityType.Power;
            BaseDamage = 1.26;
            PrimaryCost = 3;
            Args = args;

            // TODO: SpellType?
            // PBAOE
        }
    }

    public class Hone : Spell
    {
        public Hone(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Blade;
            AbilityType = AbilityType.Special;
            SpellType = SpellType.Instant;
            PrimaryGain = 2;
            BaseDamage = 0;
            // Attacks have 20% extra chance to generate CHI for 8 seconds.
            Args = args;
        }
    }

    public class Tsunami : Spell
    {
        public Tsunami(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Blade;
            AbilityType = AbilityType.Power;
            SpellType = SpellType.Channel;
            ChannelTicks = 5;
            BaseDamage = 1.04;
            PrimaryCost = 5;
            Args = args;
        }
    }

    public class DancingBlade : Spell
    {
        public DancingBlade(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Blade;
            AbilityType = AbilityType.Elite;
            SpellType = SpellType.Channel;
            ChannelTicks = 5;
            BaseDamage = 1.52;
            PrimaryCost = 4;
            Args = args;
        }
    }

    public class SpiritBlade : Spell // TODO: Two of same spell, exists in blade also?
    {
        public SpiritBlade(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Blade;
            AbilityType = AbilityType.Special;
            PrimaryGimmickCost = 5;
            // Requires & Consumes 5 Chi, The next 10 blade attacks deal additional 1.04 CP per attack (SpiritBlade)
            // If used when already forged (Requires 1 chi and will consume all chi if we have more and will generate more SpiritBlade() attacks.) 
            // All of this is in blade weapon Model.
            Args = args;
        }
    }

    public class SnakesBite : Spell
    {
        public SnakesBite(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Blade;
            AbilityType = AbilityType.Special;
            PrimaryCost = 2;
            BaseDamage = 2.9;
            // 3 second stun.
            Args = args;
        }
    }

    public class SupremeHarmony : Spell
    {
        public SupremeHarmony(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Blade;
            AbilityType = AbilityType.Elite;
            SpellType = SpellType.Instant;
            PrimaryCost = 4;
            Args = args;
            AbilityBuff = player.GetAbilityBuffFromName(Name) as AbilityBuff;
            // 8 second buff for 23% blade damage.
        }
    }

    public class SwallowCut : Spell
    {
        public SwallowCut(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Blade;
            AbilityType = AbilityType.Power;
            PrimaryCost = 3;
            BaseDamage = 2.52;
            Args = args;
        }
    }

    public class Typhoon : Spell
    {
        public Typhoon(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Blade;
            AbilityType = AbilityType.Elite;
            PrimaryCost = 4;
            BaseDamage = 1.28;
            Args = args;
            // Gives you a 6 second buff that increases blade damage by 6% per target hit..
        }
    }
}

