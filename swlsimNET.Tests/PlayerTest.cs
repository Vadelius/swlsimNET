using Microsoft.VisualStudio.TestTools.UnitTesting;
using swlSimulator.api.Models;
using swlSimulator.api.Spells;
using swlSimulator.api.Spells.Hammer;
using swlSimulator.api.Weapons;
using swlSimulator.Models;

namespace swlsimNET.Tests
{
    [TestClass]
    public class PlayerTest
    {
        [TestMethod]
        public void AplReaderTest()
        {
            var setting = TestSettingsHammerFist();
            setting.Apl = "Hammer.Smash, Rage < 50";

            var player = new Player(setting);
            var spell = player.Spells.Find(s => s.GetType() == typeof(Smash));

            var spellArgs = spell.Args == "Rage < 50";

            Assert.IsTrue(spell != null && spellArgs);
        }

        [TestMethod]
        public void WeaponTypes()
        {
            var setting = TestSettingsHammerFist();
            setting.Apl = "Hammer.Smash";
            var player = new Player(setting);

            Assert.IsInstanceOfType(player.PrimaryWeapon, typeof(Hammer));
            Assert.IsInstanceOfType(player.SecondaryWeapon, typeof(Fist));
        }

        private Settings TestSettingsHammerFist()
        {
            return new Settings
            {
                PrimaryWeapon = WeaponType.Hammer,
                SecondaryWeapon = WeaponType.Fist,
            };
        }

        private sealed class CastSpell : Spell
        {
            public CastSpell()
            {
                WeaponType = WeaponType.Elemental;
                SpellType = SpellType.Cast;
                CastTime = 2.5m;
                BaseDamage = 1;
            }
        }

        private sealed class ChannelSpell : Spell
        {
            public ChannelSpell()
            {
                WeaponType = WeaponType.Blood;
                SpellType = SpellType.Channel;
                CastTime = 2.5m;
                ChannelTicks = 5;
                BaseDamage = 1;
            }
        }

        private sealed class DotSpell : Spell
        {
            public DotSpell()
            {
                WeaponType = WeaponType.Blood;
                SpellType = SpellType.Dot;
                CastTime = 0;
                DotDuration = 2.5m;
                DotTicks = 5;
                BaseDamage = 1;

                // TODO: Add ability debuff
            }
        }

        private sealed class GadgetSpell : Spell
        {
            public GadgetSpell()
            {
                WeaponType = WeaponType.None;
                AbilityType = AbilityType.Gadget;
                SpellType = SpellType.Instant;
                MaxCooldown = 2.5m;
            }
        }

        private sealed class RifleLoadGrenadeSpell : Spell
        {
            public RifleLoadGrenadeSpell()
            {
                WeaponType = WeaponType.Rifle;
                SpellType = SpellType.Cast;
                CastTime = 1.0m;
                BaseDamage = 1;
            }
        }

        private sealed class RifleGrenadeSpell : Spell
        {
            public RifleGrenadeSpell()
            {
                PrimaryGimmickCost = 1;
                WeaponType = WeaponType.Rifle;
                SpellType = SpellType.Cast;
                CastTime = 0;
                BaseDamage = 1;
            }
        }
    }
}
