using swlSimulator.api.Models;
using swlSimulator.api.Weapons;

namespace swlSimulator.api.Spells.Rifle
{
    public class ExtendedMagazine : Passive
    {
        public ExtendedMagazine()
        {
            WeaponType = WeaponType.Rifle;
            SpellTypes.Add(typeof(FullAuto));
            ChannelTicks = 1;
            // TODO: Test
            // Increase number of hits to 5 from 4
        }
    }

    public class HeavyPayload : Passive
    {
        public HeavyPayload(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Rifle;
            SpellTypes.Add(typeof(LockAndLoad));
            // 41% chance to load a grenade on use
        }
    }

    public class Stability : Passive
    {
        public Stability(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Rifle;
            SpellTypes.Add(typeof(BurstFire));
            // Each successive hit to deal 25% more base damage
        }
    }

    public class UnerringAccuracy : Passive
    {
        public UnerringAccuracy()
        {
            WeaponType = WeaponType.Rifle;
            SpellTypes.Add(typeof(RedMist));
            BaseDamageCritModifier = 0.68;
            // Critical hits deal 68 % more damage
            // TODO: Test and add: Can no longer glance or evade
        }
    }

    public class SlowBurn : Passive
    {
        public SlowBurn(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Rifle;
            SpellTypes.Add(typeof(IncendiaryGrenade));
            DotDuration = 3;
            // TODO: Test
            // Increasing duration to 11s from 8s
        }
    }

    public class AutoLoader : Passive
    {
        public AutoLoader(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Rifle;
            SpellTypes.Add(typeof(HighExplosiveGrenade));
            // After using High Explosive Grenade next grenade gen ability gets 100% chance to load a grenade
        }
    }

    public class ExplosivesExpert : Passive
    {
        public ExplosivesExpert(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Rifle;
            // Grenade fuse time now 9s and fully cooked with 6s remaining
            // If a grenade detonates before launch no selfharm and nearby enemies take 0.44CP
        }
    }

    public class SecondaryExplosion : Passive
    {
        public SecondaryExplosion(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Rifle;
            // Extra explosives 1.5s after every grenade ability dealing 0.44Cp within 2m of original target
        }
    }

    public class BackupPlan : Passive
    {
        public BackupPlan(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Rifle;
            // If you load a grenade while one is already cooking gain 10% AR damage for 4s
        }
    }

    public class EmergencyLoader : Passive
    {
        public EmergencyLoader(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Rifle;
            // Whenever you are below 85% health Chance to load grenades is incresed to 45% 
            // when using Full Auto and 80% when using Burst Fire
        }
    }

    public class JungleStyle : Passive
    {
        public JungleStyle(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Rifle;
            // Whenever you run out of AR energy you gain 1 AR energy
        }
    }
}
