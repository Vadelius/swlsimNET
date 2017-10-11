using swlsimNET.ServerApp.Models;
using swlsimNET.ServerApp.Weapons;

namespace swlsimNET.ServerApp.Spells.Shotgun
{
    public class BlastBarrels : Passive
    {
        public BlastBarrels(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Shotgun;
            SpellTypes.Add(typeof(BothBarrels));
            // Additional hit dealing 0.38CP to each target.
        }
    }

    public class FireAtWill : Passive
    {
        public FireAtWill(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Shotgun;
            SpellTypes.Add(typeof(OpeningShot));
            // Shotgun attacks deal additional 0.21CP until enemy deals daamge to you. Crit hits with Shotgun reloads 1 Shell.
        }
    }

    public class PointBlankShot : Passive
    {
        public PointBlankShot()
        {
            WeaponType = WeaponType.Shotgun;
            SpellTypes.Add(typeof(RagingShot));
            BaseDamage = 0.58;
            // TODO: Test and implement range???
            // Increase damage to 4.00 to targets within 3m
        }
    }

    public class WitheringSalvo : Passive
    {
        public WitheringSalvo(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Shotgun;
            SpellTypes.Add(typeof(FullSalvo));
            // 40% more base damage on each successive hit.
        }
    }

    public class SalvageExpert : Passive
    {
        public SalvageExpert()
        {
            WeaponType = WeaponType.Shotgun;
            SpellTypes.Add(typeof(ShellSalvage));
            SecondaryGain = 5;
            // TODO: offer to reload the same type of shell
            // Always offer to reload the same type of shell.
        }
    }

    public class ClusterBombs : Passive
    {
        public ClusterBombs(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Shotgun;
            SpellTypes.Add(typeof(Bombardment));
            // On hit break into 3 smaller explosives dealing 0.31CP
        }
    }

    public class OddsAndEvens : Passive
    {
        public OddsAndEvens(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Shotgun;
            // Odd number of shells: Shotgun basic deal 70% more damage, Even number of shells: Shotgun power deal 15% more damage.
        }
    }

    public class WhitePhosphorusShells : Passive
    {
        public WhitePhosphorusShells(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Shotgun;
            // Dragon's Breath Shells damage is improved by 12%
        }
    }

    public class Enrich : Passive
    {
        public Enrich(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Shotgun;
            // Whenever you load Depleted Uranium Shells feed in 2 extra shells
        }
    }

    public class MunitionsExpert : Passive
    {
        public MunitionsExpert(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Shotgun;
            // More opportunities to load Dragon's Breath Shells, Depleted Uranium Shells and Armor-Piercing Shells
        }
    }

    public class CombatReload : Passive
    {
        public CombatReload(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Shotgun;
            // In combat with shells still loaded reload 1 shell every 5s
        }
    }
}