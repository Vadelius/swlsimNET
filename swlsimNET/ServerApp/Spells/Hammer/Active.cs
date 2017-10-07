using swlsimNET.ServerApp.Models;
using swlsimNET.ServerApp.Weapons;

namespace swlsimNET.ServerApp.Spells.Hammer
{
    public class Smash : Spell
    {
        public Smash(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Hammer;
            AbilityType = AbilityType.Basic;
            BaseDamage = 1.175;
            Args = args;
            PrimaryGimmickGain = 5;
        }
    }
    public class RazorShards : Spell
    {
        public RazorShards(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Hammer;
            Args = args;
            PrimaryCost = 3;
            PrimaryGimmickGain = 6; // Per Target
            BaseDamage = 1.26;
            // TODO: 5s Dot 0,89CP
        }
    }
    public class RazorShardsRage : Spell
    {
        public RazorShardsRage(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Hammer;
            SpellType = SpellType.Dot; // TODO: No initial dmg only dot?
            Args = args;
            PrimaryCost = 3;
            PrimaryGimmickCost = 50;
            BaseDamage = 1;
            // TODO: 5s Dot 0,89CP
        }
    }


    public class Seethe : Spell
    {
        public Seethe(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Hammer;
            SpellType = SpellType.Instant;
            Args = args;
            PrimaryGain = 4;
            MaxCooldown = 20;
        }
    }

    public class Demolish : Spell
    {
        public Demolish(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Hammer;
            AbilityType = AbilityType.Power;
            Args = args;
            PrimaryCost = 5;
            PrimaryGimmickGain = 25;
            BaseDamage = 3.43;
        }
    }

    public class DemolishRage : Spell
    {
        public DemolishRage(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Hammer;
            AbilityType = AbilityType.Power;
            Args = args;
            PrimaryCost = 5;
            PrimaryGimmickCost = 50;
            BaseDamage = 8.38;
        }
    }

    public class Eruption : Spell
    {
        public Eruption(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Hammer;
            Args = args;
            PrimaryCost = 4;
            PrimaryGimmickGain = 7;
            MaxCooldown = 20;
            BaseDamage = 5.6;
            // TODO: Stun, Purge, Exposed, +7Rage per target, Column 6 Enemies
        }
    }

    public class EruptionRage : Spell
    {
        public EruptionRage(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Hammer;
            Args = args;
            PrimaryCost = 4;
            PrimaryGimmickGain = 7;
            MaxCooldown = 20;
            BaseDamage = 5.6;
            // TODO: Stun, Purge, Exposed, +7 Rage per target, Column 6 Enemies
        }
    }

    public class Rampage : Spell
    {
        public Rampage(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Hammer;
            Args = args;
            PrimaryCost = 2;
            PrimaryGimmickGain = 9;
            MaxCooldown = 20;
            BaseDamage = 0.62;
            //Exposed, PBAoE 6 Enemies        
        }
    }
    
    public class UnstoppableForce : Spell
    {
        public UnstoppableForce(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Hammer;
            AbilityType = AbilityType.Elite;
            SpellType = SpellType.Instant;
            Args = args;
            PrimaryCost = 4;
            MaxCooldown = 20;
            AbilityBuff = player.GetAbilityBuffFromName(Name) as AbilityBuff;
        }
    }

    public class Blindside : Spell
    {
        public Blindside(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Hammer;
            Args = args;
            PrimaryCost = 2;
            PrimaryGimmickGain = 10;
            MaxCooldown = 20;
            BaseDamage = 2.57;
            //Purge, Interrupt
        }
    }
}
