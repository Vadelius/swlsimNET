using swlsimNET.ServerApp.Combat;
using swlsimNET.ServerApp.Models;
using swlsimNET.ServerApp.Spells;
using swlsimNET.ServerApp.Spells.Shotgun;

namespace swlsimNET.ServerApp.Weapons
{
    internal class Shotgun : Weapon
    {
        public Shotgun(WeaponType wtype, WeaponAffix waffix) : base(wtype, waffix)
        {
            _maxGimickResource = 6;
            _gimickResource = 6;
        }

        public override void AfterAttack(IPlayer player, ISpell spell, RoundResult rr)
        {
            // Not all spells should procc gimmick
            if(spell.GetType() == typeof(Reload) || spell.GetType() == typeof(ShellSalvage)) return;

            if (Rnd.Next(1, 3) == 1)
            {
                // TODO: Check & FIX DOT duration/stacks and assume perfect play by default 
                // in weapon-model so APL does not have to worry about it at all.
                player.AddBonusAttack(rr, new DragonBreath()); 
            } 
            else player.AddBonusAttack(rr, new DepletedUranium());   
        }

        #region Gimmickspells

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

        #endregion
    }
}
