using swlsimNET.ServerApp.Models;
using swlsimNET.ServerApp.Weapons;

namespace swlsimNET.ServerApp.Spells.Pistol
{
    public class HairTrigger : Spell
    {
        public HairTrigger(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Pistol;
            AbilityType = AbilityType.Basic;
            BaseDamage = 1.175;
            Args = args;
        }
    }

    public class DualShot : Spell
    {
        public DualShot(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Pistol;
            AbilityType = AbilityType.Power;
            Args = args;
            PrimaryCost = 3;
            BaseDamage = 2.52;
        }
    }

    public class ControlledShooting : Spell
    {
        public ControlledShooting(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Pistol;
            AbilityType = AbilityType.Power;
            SpellType = SpellType.Cast;
            CastTime = 0.5;
            PrimaryCost = 3;
            BaseDamage = 1.26;
            Args = args;
        } // Chain Jumps 5 times hitting up to 6 enemies
    }

    public class Flourish : Spell
    {
        public Flourish(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Pistol;
            SpellType = SpellType.Instant;
            MaxCooldown = 20;
            PrimaryGain = 4;
            Args = args;
        } // Cleanse 1 effect.
    }

    public class Unload : Spell
    {
        public Unload(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Pistol;
            AbilityType = AbilityType.Power;
            SpellType = SpellType.Channel;
            CastTime = 2.5;
            ChannelTicks = 5;
            PrimaryCost = 5;
            BaseDamage = 1.03;
            Args = args;
        }
    }

    public class AllInn : Spell
    {
        public AllInn(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Pistol;
            AbilityType = AbilityType.Elite;
            SpellType = SpellType.Channel;
            CastTime = 3.0;
            ChannelTicks = 10;
            MaxCooldown = 20;
            PrimaryCost = 4;
            BaseDamage = 1337;
            Args = args;
        } // TODO: First 3 hits: 0.54CP, Next 3 Hits 0.68CP, Next 3 hits: 0.81CP in a 5m area, Final Hit: 2.03CP.
    }

    public class FullHouse : Spell
    {
        public FullHouse(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Pistol;
            SpellType = SpellType.Instant;
            MaxCooldown = 20;
            PrimaryCost = 2;
            Args = args;
        } // TODO: Force chambers to Double Blue for 3s.
    }

    public class SixLine : Spell
    {
        public SixLine(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Pistol;
            SpellType = SpellType.Instant;
            MaxCooldown = 20;
            PrimaryCost = 2;
            Args = args;
        } // TODO: Set left chamber to Red for 6s, During this time all pistols attacks deal additional 0.37CP Damage. 
    }

    public class KillBlind : Spell
    {
        public KillBlind(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Pistol;
            MaxCooldown = 20;
            PrimaryCost = 2;
            BaseDamage = 0.81;
            Args = args;
        } // TODO: Extend chamber damage bonus by 1.5s, Exposed, Debilitated, Stun.
    }

    public class TrickShot : Spell
    {
        public TrickShot(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Pistol;
            AbilityType = AbilityType.Elite;
            MaxCooldown = 20;
            PrimaryCost = 4;
            BaseDamage = 5.6;
            Args = args;
        } // TAoE 6E 5M, Exposed, Debilitated.
    }

    public class SeekingBullet : Spell
    {
        public SeekingBullet(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Pistol;
            AbilityType = AbilityType.Basic;
            SpellType = SpellType.Cast;
            CastTime = 0.5;
            BaseDamage = 0.58;
            Args = args;
        } // Chain: Jumps 8m 5 times hitting 6E
    }

    public class ChargedBlast : Spell
    {
        public ChargedBlast(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Pistol;
            AbilityType = AbilityType.Power;
            PrimaryCost = 5;
            BaseDamage = 1.71;
            Args = args;
        } // TAoE 5E 6M.
    }

    public class Ricochet : Spell
    {
        public Ricochet(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Pistol;
            SpellType = SpellType.Instant;
            MaxCooldown = 20;
            PrimaryCost = 2;
            Args = args;
        } // TODO: Your target and nearby enemies are dealt additional 0.24CP damage with Pistol abilities.
    }

    public class BulletBallet : Spell
    {
        public BulletBallet(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Pistol;
            AbilityType = AbilityType.Elite;
            SpellType = SpellType.Channel;
            CastTime = 2.0;
            ChannelTicks = 10;
            MaxCooldown = 20;
            PrimaryCost = 4;
            BaseDamage = 0.69;
            Args = args;
        } // PBAoE 6E 10M, Rooted during channel, Slow.
    }
}
