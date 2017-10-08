using swlsimNET.ServerApp.Combat;
using swlsimNET.ServerApp.Models;
using swlsimNET.ServerApp.Weapons;

namespace swlsimNET.ServerApp.Spells.Blood
{
    public class Torment : Spell
    {
        public Torment(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Blood;
            AbilityType = AbilityType.Basic;
            BaseDamage = 1.175;
            Args = args;
            PrimaryGimmickGain = 2;
        }
    }

    public class DreadSigil : Spell
    {
        public DreadSigil(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Blood;
            AbilityType = AbilityType.Power;
            SpellType = SpellType.Instant;
            PrimaryCost = 3;
            PrimaryGimmickGain = 8;
            BaseDamage = 1.26;
            Args = args;
        } // TAoE 6 Enemies 5m radius
    }

    public class Reap : Spell
    {
        public Reap(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Blood;
            SpellType = SpellType.Dot;
            PrimaryCost = 2;
            PrimaryGimmickRequirement = 10;
            PrimaryGimmickReduce = 50;
            MaxCooldown = 20;
            BaseDamage = 0.25;
            DotDuration = 7;
            Args = args;
        }
        // Dot 0.25CP per second for 7s. Restores 35% Health and cleanse 1 effect
        // Corruption level must be higher than 10 to use.
    }

    public class Maleficium : Spell
    {
        public Maleficium(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Blood;
            AbilityType = AbilityType.Power;
            SpellType = SpellType.Channel;
            CastTime = 2.5m;
            ChannelTicks = 5;
            PrimaryCost = 5;
            PrimaryGimmickGain = 3;
            BaseDamage = 1.04;
            Args = args;
        }
    }

    public class SanguineCoalescence : Spell
    {
        public SanguineCoalescence(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Blood;
            AbilityType = AbilityType.Elite;
            SpellType = SpellType.Instant;
            PrimaryCost = 4;
            PrimaryGimmickGain = 20;
            MaxCooldown = 20;
            BaseDamage = 5.7;
            Args = args;
        }   
        // PBAoE 5m when the barrier expires or is destroyed, Increases Corruption by 20 when cast, 
        // TODO: Reduces Corruption by 20 when barrier expires or is destroyed, 10s Duration Barrier
    }

    public class Rupture : Spell
    {
        public Rupture(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Blood;
            SpellType = SpellType.Instant;
            PrimaryCost = 2;
            PrimaryGimmickCost = 50;
            MaxCooldown = 20;
            BaseDamage = 2.9;
            Args = args;
        }
        // Stun 3s
        // TODO: Damage dealt is based on CP if over 10 Corruption and HP if over 10 Martyrdom
    }

    public class Desecrate : Spell
    {
        public Desecrate(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Blood;
            SpellType = SpellType.Dot; 
            DotDuration = 6;
            CastTime = 1.0m;
            PrimaryGimmickRequirement = 10;
            PrimaryCost = 2;
            MaxCooldown = 20;
            BaseDamage = 0.37;
            Args = args;
        }   
        // Dot 0,37CP every second for 6s, During this time you do not take damage from Corruption
        // Corruption must be higher than 10 to use.
    }

    public class RunicHex : Spell
    {
        public RunicHex(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Blood;
            SpellType = SpellType.Dot;
            CastTime = 1.5m;
            PrimaryCost = 2;
            PrimaryGimmickCost = 50;
            MaxCooldown = 20;
            BaseDamage = 0.24;
            DotDuration = 6;
            DotExpirationBaseDamage = 0.87;
            Args = args;
        }
        // TODO: Test, especially DotExpirationBaseDamage
        // Dot 0,24CP every second for 6s, 
        // 0,87CP Damage when it expires to nearby enemies, Slow, Debilitated
    }

    public class EldritchScourge : Spell
    {
        public EldritchScourge(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Blood;
            AbilityType = AbilityType.Elite;
            SpellType = SpellType.Dot;
            CastTime = 1.5m;
            DotDuration = 6;
            PrimaryCost = 4;
            PrimaryGimmickGain = 15;
            MaxCooldown = 20;
            BaseDamage = 0.68;
            Args = args;
        }

        public override Attack Execute(Player player)
        {
            // TODO: Redo this if something can edit PrimaryGimmickGain
            PrimaryGimmickGain = player.Corruption > 0 ? 15 : 0;
            return base.Execute(player);
        }

        // TODO: Test
        // Dot 0,68CP every second for 6s, Dot spreads to nearby enemies 0,53Cp every second for 3s, Debilitated
        // Corruption increase only occurs if Corruption is above 0
    }

}
