using System;
using System.Collections.Generic;
using System.Linq;
using swlsimNET.ServerApp.Combat;
using swlsimNET.ServerApp.Models;
using swlsimNET.ServerApp.Spells;

namespace swlsimNET.Models
{
    public class PopulatorModel
    {
        private List<Attack> _allSpellCast = new List<Attack>();
        private List<ISpell> _distinctSpellCast = new List<ISpell>();
        public double TotalDamage { get; private set; }
        public List<TablePopulator> SpellData(SpellType spellType, Settings settings, List<TablePopulator> list)
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings));
            if (list == null) throw new ArgumentNullException(nameof(list));
            var dSpells = _distinctSpellCast.Where(s => s.SpellType == spellType).ToList();
            list = new List<TablePopulator>();

            foreach (var dSpell in dSpells)
            {
                var allOfSameSpellDatas = _allSpellCast.Where(s => s.Spell.Name == dSpell.Name).ToList();
                var alldmg = allOfSameSpellDatas.Sum(s => s.Damage);
                var avgDmg = allOfSameSpellDatas.Average(s => s.Damage);
                var crits = allOfSameSpellDatas.Count(s => s.IsCrit);
                var hits = allOfSameSpellDatas.Count(s => s.IsHit);
                // Can't divide int with int, 4.9 will result in 4 etc, either print with decimal or do a correct rounding
                var avghits = hits / (double) settings.Iterations;
                var avgcrits = crits / (double) settings.Iterations;
                var cc = decimal.Divide(crits, hits) * 100;
                var hdmg = allOfSameSpellDatas.Max(s => s.Damage);
                var ldmg = allOfSameSpellDatas.Where(s => s.IsHit).Min(s => s.Damage);
                var ofTotal = alldmg / TotalDamage * 100;
                var dmgPerSecond = alldmg / settings.FightLength / settings.Iterations;
                var avgdmgAvarage = avgDmg / settings.FightLength / settings.Iterations;
                var executes = avghits + avgcrits;
                // [spellName, DPS, DPS%, Executes, DPE, SpellType, Count, Avarage, Crit%]

                list.Add(new TablePopulator()
                {
                    Name = dSpell.Name,
                    DamagePerSecond = (int) dmgPerSecond,
                    DpsPercentage = ofTotal,
                    Executes = (int) executes,
                    DamagePerExecution = (int) avgdmgAvarage,
                    SpellType = dSpell.SpellType.ToString(),
                    Count = (int) executes,
                    Avarage = (int) avgdmgAvarage,
                    CritChance = cc
                });

            }
            return list;
        }

        private string ToHtml(List<TablePopulator> xs) // Keep this. Might use in future.
        {
            var name = xs[0];
            var dps = xs[1];
            var dpsPercent = xs[2];
            var executes = xs[3];
            var dpe = xs[4];
            var type = xs[5];
            var count = xs[6];
            var avarage = xs[7];
            var crit = xs[8];

            return "";
        }
    }
}
