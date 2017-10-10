using swlsimNET.ServerApp.Combat;
using swlsimNET.ServerApp.Models;
using swlsimNET.ServerApp.Weapons;

namespace swlsimNET.ServerApp.Spells.Shotgun
{
    public class PumpAction : Spell
    {
        public PumpAction(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Shotgun;
            AbilityType = AbilityType.Basic;
            PrimaryGimmickCost = 1;
            BaseDamage = 1.17;
            Args = args;
        }
    }

    public class BothBarrels : Spell
    {
        public BothBarrels(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Shotgun;
            AbilityType = AbilityType.Power;
            PrimaryCost = 3;
            PrimaryGimmickCost = 1;
            BaseDamage = 1.26;
            Args = args;
        }
    }

    public class OpeningShot : Spell
    {
        public OpeningShot(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Shotgun;
            AbilityType = AbilityType.Special;
            SpellType = SpellType.Instant;
            PrimaryCost = 2;
            PrimaryGimmickCost = 1;
            MaxCooldown = 20;
            Args = args;
        } // TODO: 30% Crit Power 8s Entire Group.
    }

    public class RagingShot : Spell
    {
        public RagingShot(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Shotgun;
            AbilityType = AbilityType.Power;
            PrimaryCost = 5;
            PrimaryGimmickCost = 1;
            BaseDamage = 3.42;
            Args = args;
        }
    }

    public class FullSalvo : Spell
    {
        public FullSalvo(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Shotgun;
            AbilityType = AbilityType.Elite;
            SpellType = SpellType.Channel;
            CastTime = 2.5m;
            ChannelTicks = 5;
            PrimaryCost = 4;
            PrimaryGimmickCost = 5;
            MaxCooldown = 20;
            BaseDamage = 1.51; // Channel 5 hits 1.51 per hit.         
            Args = args;
        }
    }

    public class ShellSalvage : Spell
    {
        public ShellSalvage(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Shotgun;
            AbilityType = AbilityType.Special;
            SpellType = SpellType.Instant;
            // Gain 3-8 Shotgun Energy based on number of Shells Salvaged.
            PrimaryGimmickRequirement = 1;
            MaxCooldown = 20;
            Args = args;
        }

        public override Attack Execute(Player player)
        {
            var spellWeapon = player.GetWeaponFromSpell(this);

            switch (spellWeapon.GimmickResource)
            {
                case 1:
                    PrimaryGimmickReduce = 1;
                    PrimaryGain = 3;
                    break;
                case 2:
                    PrimaryGimmickReduce = 2;
                    PrimaryGain = 4;
                    break;
                case 3:
                    PrimaryGimmickReduce = 3;
                    PrimaryGain = 5;
                    break;
                case 4:
                    PrimaryGimmickReduce = 4;
                    PrimaryGain = 6;
                    break;
                case 5:
                    PrimaryGimmickReduce = 5;
                    PrimaryGain = 7;
                    break;
                case 6:
                    PrimaryGimmickReduce = 6;
                    PrimaryGain = 8;
                    break;
            }

            return base.Execute(player);
        }
    }

    public class Bombardment : Spell
    {
        public Bombardment(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Shotgun;
            AbilityType = AbilityType.Elite;
            PrimaryCost = 4;
            PrimaryGimmickCost = 6;
            MaxCooldown = 20;
            BaseDamage = 0.70; // 10s Ground AoE 0.70CP Every 1.25s for 10s TAoE 3m  0.36CP       
            Args = args;
        } // Exposed
    }

    public class Reload : Spell
    {
        public Reload(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Shotgun;
            Args = args;
            PrimaryGimmickGain = 6;
        }
    }
}