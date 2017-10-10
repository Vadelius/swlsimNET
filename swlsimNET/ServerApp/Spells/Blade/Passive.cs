using swlsimNET.ServerApp.Models;
using swlsimNET.ServerApp.Weapons;

namespace swlsimNET.ServerApp.Spells.Blade
{
    public class EyeOfTheStorm : Passive
    {
        public EyeOfTheStorm()
        {
            WeaponType = WeaponType.Blade;
            SpellTypes.Add(typeof(Hurricane));
            BaseDamage = 0.47; // per channel hit.
        }
    }

    public class KeenEdge : Passive
    {
        public KeenEdge()
        {
            WeaponType = WeaponType.Blade;
            SpellTypes.Add(typeof(Hone));
            BaseDamageCritModifier = 0.4;
            // Adds 40% critpower buff during hone
        }
    }

    public class StormSurge : Passive
    {
        public StormSurge()
        {
            WeaponType = WeaponType.Blade;
            SpellTypes.Add(typeof(Tsunami));
            BaseDamage = 0.47; // per channel hit.
        }
    }

    public class SoulForgedBlade : Passive
    {
        public SoulForgedBlade()
        {
            WeaponType = WeaponType.Blade;
            SpellTypes.Add(typeof(DancingBlade));
            // Generates a 3 second Spirit Blade OR 3 spirit blade attacks if spirit blade is up.
        }
    }

    public class Masterpiece : Passive
    {
        public Masterpiece()
        {
            WeaponType = WeaponType.Blade;
            SpellTypes.Add(typeof(SpiritBlade));
            // Every 4th blade attack with Spirit Blade up deals an extra 0.78 CP per attack
        }
    }

    public class MastersFocus : Passive
    {
        public MastersFocus()
        {
            WeaponType = WeaponType.Blade;
            SpellTypes.Add(typeof(SupremeHarmony));
            // Whenever Chi is gained during Supreme Harmony reduce its CURRENT cooldown by 5% (Like efficiency)
        }
    }

    public class HardenedBlade : Passive
    {
        public HardenedBlade()
        {
            WeaponType = WeaponType.Blade;
            SpellTypes.Add(typeof(SwallowCut));
            ModelledInWeapon = true;
            // Has 30% chance to not consume a spirit blade attackcharge when used.
        }
    }

    public class Torrent : Passive
    {
        public Torrent()
        {
            WeaponType = WeaponType.Blade;
            SpellTypes.Add(typeof(Typhoon));
            // Blade damage bonus for each target hit increased to 9.3% per target (46.5 maximum) ( 5 targets.. )
        }
    }

    public class Deluge : Passive
    {
        public Deluge()
        {
            WeaponType = WeaponType.Blade;
            // Every 6th hit with spirit blade unleashes an AoE of 0.38CP
        }
    }

    public class DrawSwordDrawBlood : Passive
    {
        public DrawSwordDrawBlood()
        {
            WeaponType = WeaponType.Blade;
            // When combat starts the next 3 blade attacks does 20% more damage.
            // This bonus is also applied once every 10 seconds. (ONE CHARGE, not 3 ?)
        }
    }

    public class MeasureTwiceCutOnce : Passive
    {
        public MeasureTwiceCutOnce()
        {
            WeaponType = WeaponType.Blade;
            // If Chi has not been generated in the past 4.5 seconds, the next blade attack will generate 1 chi
            // in addition to the normal chi gain chance.
        }
    }

    public class FocusedBreathing : Passive
    {
        public FocusedBreathing()
        {
            WeaponType = WeaponType.Blade;
            //1 chi gained every 9 seconds.
        }
    }

    public class Resonance : Passive
    {
        public Resonance()
        {
            WeaponType = WeaponType.Blade;
            // Whenever chi is gained do an attack for 0.38 CP
        }
    }

    public class WarriorsSpirit : Passive
    {
        public WarriorsSpirit()
        {
            WeaponType = WeaponType.Blade;
            // When the spirit blade is consumed (shattered... RP) gain 1 chi and do 0.67CP in an AoE
        }
    }
}