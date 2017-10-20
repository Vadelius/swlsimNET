using swlSimulator.api.Combat;
using swlSimulator.api.Models;
using swlSimulator.api.Spells;
using swlSimulator.api.Spells.Shotgun;

namespace swlSimulator.api.Weapons
{
    public class Shotgun : Weapon
    {
        private int _ifritanDespoilerCounter;
        private decimal _shellstamp;

        public Shotgun(WeaponType wtype, WeaponAffix waffix) : base(wtype, waffix)
        {
            _maxGimickResource = 6;
            _gimickResource = 6;
        }

        public override void PreAttack(IPlayer player, RoundResult rr)
        {
            _shellstamp = GimmickResource;
        }

        public override void AfterAttack(IPlayer player, ISpell spell, RoundResult rr)
        {
            var roll = Rnd.Next(1, 6);

            if (player.Settings.PrimaryWeaponProc == WeaponProc.IfritanDespoiler)
            {
                if (spell.GetType() == typeof(DragonBreath))
                {
                    _ifritanDespoilerCounter += 2;
                }
                if (spell.GetType() == typeof(DepletedUranium))
                {
                    _ifritanDespoilerCounter++;
                }
                if (_ifritanDespoilerCounter >= 12)
                {
                    player.AddBonusAttack(rr, new IfritanDespoiler(player, ""));
                    _ifritanDespoilerCounter = 0;
                }
            }

            if (player.Settings.PrimaryWeaponProc == WeaponProc.Spesc221 && GimmickResource < _shellstamp && roll == 5)
            {
                player.AddBonusAttack(rr, new SpesC221());
            }

            // Not all spells should procc gimmick
            if (spell.GetType() == typeof(Reload) || spell.GetType() == typeof(ShellSalvage))
            {
                return;
            }

            if (Rnd.Next(1, 3) == 1)
            {
                // TODO: Check & FIX DOT duration/stacks and assume perfect play by default 
                // in weapon-model so APL does not have to worry about it at all.
                player.AddBonusAttack(rr, new DragonBreath());
            }
            else
            {
                player.AddBonusAttack(rr, new DepletedUranium());
            }
        }

        // We assume we will be alternating Dragon's Breath and Depleted Uranium Shells every reload
        private class DragonBreath : Spell
        {
            public DragonBreath()
            {
                WeaponType = WeaponType.Shotgun;
                SpellType = SpellType.Gimmick;
                PrimaryGimmickCost = 1;
                BaseDamage = 0.147;
                // TODO: Stacking dot
                // Dragons Breath rounds do 0.147CP per stack with a maximum of 6 stacks	
            }
        }

        private class DepletedUranium : Spell
        {
            public DepletedUranium()
            {
                WeaponType = WeaponType.Shotgun;
                SpellType = SpellType.Gimmick;
                PrimaryGimmickCost = 1;
                BaseDamage = 0.97;
                // Depleted Uranium rounds do 0.97CP, and will be used with 50% uptime	
            }
        }

        private class ArmorPiercing : Spell
        {
            public ArmorPiercing()
            {
                WeaponType = WeaponType.Shotgun;
                SpellType = SpellType.Gimmick;
                PrimaryGimmickCost = 1;
                BaseDamage = 0; // Adds exposed for 10s
            }
        }

        private class AnimaInfused : Spell
        {
            public AnimaInfused()
            {
                WeaponType = WeaponType.Shotgun;
                SpellType = SpellType.Gimmick;
                PrimaryGimmickCost = 1;
                BaseDamage = 0; // 3% HP heal
            }
        }

        public class IfritanDespoiler : Spell
        {
            public IfritanDespoiler(IPlayer player, string args = null)
            {
                WeaponType = WeaponType.Shotgun;
                SpellType = SpellType.Instant;
                AbilityBuff = player.GetAbilityBuffFromName(Name) as AbilityBuff;
            }
        }

        private class SpesC221 : Spell
        {
            public SpesC221()
            {
                WeaponType = WeaponType.Shotgun;
                SpellType = SpellType.Procc;
                BaseDamage = 1.5;
            }
        }
    }
}