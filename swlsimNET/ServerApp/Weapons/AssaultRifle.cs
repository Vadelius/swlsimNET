using System;
using System.Collections.Generic;
using System.Linq;
using swlsimNET.ServerApp.Combat;
using swlsimNET.ServerApp.Models;
using swlsimNET.ServerApp.Spells;

namespace swlsimNET.ServerApp.Weapons
{
    public class AssaultRifle : Weapon
    {
        private readonly List<string> _grenadeGenerators = new List<string>
        {
            "FullAuto", "UnveilEssence", "BurstFire"
        };

        public AssaultRifle(WeaponType wtype, WeaponAffix waffix) : base(wtype, waffix)
        {
            _maxGimickResource = 1;
        }

        public override void AfterAttack(IPlayer player, ISpell spell, RoundResult rr)
        {
            var spellName = spell.Name;
            if (spellName != null && !_grenadeGenerators.Contains(spellName, StringComparer.CurrentCultureIgnoreCase)) return;

            if (Rnd.Next(1, 101) <= 65) GimmickResource++;
        } 
    }
}