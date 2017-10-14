using swlsimNET.ServerApp.Models;
using swlsimNET.ServerApp.Weapons;

namespace swlsimNET.ServerApp.Spells.Fist
{
    public class Basic : Spell
    {
        public Basic(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Fist;
            AbilityType = AbilityType.Basic;
            BaseDamage = 1.175;
            Args = args;
        }
    }

    public class Trash : Spell
    {
        public Trash(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Fist;
            AbilityType = AbilityType.Basic;
            SpellType = SpellType.Channel;
            CastTime = 1;
            ChannelTicks = 2;
            Args = args;
            PrimaryGimmickGain = 4;
            BaseDamage = 1.175;
            // Striking 2 Times
        }
    }

    public class SavageSweep : Spell
    {
        public SavageSweep(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Fist;
            Args = args;
            PrimaryCost = 3;
            PrimaryGimmickGain = 3;
            BaseDamage = 1.26;
        }
    }

    public class FrenziedWrath : Spell
    {
        public FrenziedWrath(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Fist;
            Args = args;
            PrimaryGimmickCost = 60;
            MaxCooldown = 5;
            // TODO: Is there a spell with this name u can activate???

            // TODO: Activate FrenziedWrath buff
            // 2 Self Cleanse
        }
    }

    public class Ravage : Spell
    {
        public Ravage(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Fist;
            Args = args;
            BaseDamage = 1.26;
            // TODO: 5s Dot 0,59CP
        }
    }

    public class Shred : Spell
    {
        public Shred(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Fist;
            Args = args;
            BaseDamage = 1.05;
            // TODO: 5s Dot 0,125CP, If maim active 0,42CP PBAoE
        }
    }

    public class Maim : Spell
    {
        public Maim(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Fist;
            Args = args;
            PrimaryCost = 4;
            MaxCooldown = 15;
            BaseDamage = 0.24;
            // TODO: 15s Dot 0,386CP
        }
    }

    public class Mangle : Spell
    {
        public Mangle(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Fist;
            Args = args;
            PrimaryCost = 5;
            PrimaryGimmickGain = 12;
            BaseDamage = 3.43;
        }
    }

    public class Berserk : Spell
    {
        public Berserk(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Fist;
            Args = args;
            PrimaryCost = 4;
            PrimaryGimmickGain = 22;
            BaseDamage = 7.10;
            // TODO: Exposed, Root, +2 Fury per hit, 2.5s Channel
        }
    }

    public class Eviscerate : Spell
    {
        public Eviscerate(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Fist;
            Args = args;
            PrimaryCost = 2;
            PrimaryGimmickGain = 3;
            MaxCooldown = 20;
            BaseDamage = 2.7;
            //Exposed, 3s Stun
        }
    }

    public class Ferocity : Spell
    {
        public Ferocity(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Fist;
            Args = args;
            PrimaryCost = 2;
            MaxCooldown = 20;
            BaseDamage = 2.9;
            // TODO: Remove all fist dots and cause them to deal their remaining damage
        }
    }

    public class Savagery : Spell
    {
        public Savagery(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Fist;
            SpellType = SpellType.Instant;
            Args = args;
            PrimaryCost = 2;
            PrimaryGimmickGain = 8;
            MaxCooldown = 20;
            AbilityBuff = player.GetAbilityBuffFromName(Name) as AbilityBuff;
        }
    }

    public class PrimalInstinct : Spell
    {
        public PrimalInstinct(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Fist;
            Args = args;
            PrimaryCost = 4;
            PrimaryGimmickGain = 18;
            MaxCooldown = 20;
            // TODO: 6s 215% Dot damage
        }
    }
}
