using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using swlsimNET.Models;
using swlsimNET.ServerApp.Combat;
using swlsimNET.ServerApp.Models;
using swlsimNET.ServerApp.Spells;

namespace swlsimNET.ServerApp.Models
{
    public class Report
    {
        private List<Attack> _allSpellCast = new List<Attack>();
        private List<ISpell> _distinctSpellCast = new List<ISpell>();
        private NumberFormatInfo nfi;

        public int TotalCrits { get; private set; }
        public int TotalHits { get; private set; }
        public double TotalDamage { get; private set; }
        public List<TablePopulator> list { get; private set; }

        private double lowestDps = double.MaxValue;
        private double highestDps;

        public void GenerateReportData(List<FightResult> iterationFightResults, Settings settings)
        {
            InitReportData(iterationFightResults);

            var critPercent = decimal.Divide(TotalCrits, TotalHits) * 100;
            var dps = TotalDamage / settings.FightLength / settings.Iterations;
            var avgDamage = TotalDamage / settings.Iterations;

            GenerateSpellReportData(settings);
        }

        private void GenerateSpellReportData(Settings settings)
        {
            // Set order here for spell reports
            SpellTypeReport(SpellType.Cast, settings);
            SpellTypeReport(SpellType.Channel, settings);
            SpellTypeReport(SpellType.Dot, settings);
            SpellTypeReport(SpellType.Instant, settings);
            SpellTypeReport(SpellType.Buff, settings);
            SpellTypeReport(SpellType.Procc, settings);
            SpellTypeReport(SpellType.Gimmick, settings);
            SpellTypeReport(SpellType.Passive, settings);
        }

        private void InitReportData(List<FightResult> iterationFightResults)
        {
            nfi = (NumberFormatInfo) CultureInfo.InvariantCulture.NumberFormat.Clone();
            nfi.NumberGroupSeparator = " ";

            // Displays 1,234,567,890   
            // value.ToString("#,#", CultureInfo.InvariantCulture));

            // Displays 1,234,568K
            // value.ToString("#,##0,K", CultureInfo.InvariantCulture));

            // Displays 1,235M
            // value.ToString("#,##0,,M", CultureInfo.InvariantCulture));

            // Displays 1B
            // value.ToString("#,##0,,,B", CultureInfo.InvariantCulture));

            foreach (var iteration in iterationFightResults)
            {

                TotalDamage += iteration.TotalDamage;
                TotalHits += iteration.TotalHits;
                TotalCrits += iteration.TotalCrits;
                if (iteration.Dps > highestDps) highestDps = iteration.Dps;
                if (iteration.Dps < lowestDps) lowestDps = iteration.Dps;


                foreach (var rr in iteration.RoundResults)
                {
                    foreach (var a in rr.Attacks)
                    {
                        //if (a.IsHit && a.IsCrit)
                        //{
                        //    _oneBuilder.AppendLine($"[{rr.TimeMs.ToString("#,##0,.0s", nfi)}] " +
                        //                           $"{a.Spell.Name} *{a.Damage.ToString("#,##0,.0K", nfi)}* " +
                        //                           $"E({rr.PrimaryEnergyEnd}/{rr.SecondaryEnergyEnd}) " +
                        //                           $"R({rr.PrimaryGimmickEnd}/{rr.SecondaryGimmickEnd})");
                        //}
                        //else if (a.IsHit && a.Spell.SpellType != SpellType.Procc)
                        //{
                        //    _oneBuilder.AppendLine($"[{rr.TimeMs.ToString("#,##0,.0s", nfi)}] " +
                        //                           $"{a.Spell.Name} {a.Damage.ToString("#,##0,.0K", nfi)} " +
                        //                           $"E({rr.PrimaryEnergyEnd}/{rr.SecondaryEnergyEnd}) " +
                        //                           $"R({rr.PrimaryGimmickEnd}/{rr.SecondaryGimmickEnd})");
                        //}
                        //else if (a.IsHit && a.Spell.SpellType == SpellType.Procc)
                        //{
                        //    _oneBuilder.AppendLine($"[{rr.TimeMs.ToString("#,##0,.0s", nfi)}] " +
                        //                           $"[{a.Spell.Name}] proc!");
                        //}

                        _allSpellCast.Add(a);

                        if (_distinctSpellCast.All(s => s.Name != a.Spell.Name))
                        {
                            _distinctSpellCast.Add(a.Spell);
                        }
                    }
                }
            }

            //_oneBuilder.AppendLine("\r\nOutput format:" +
            //                       "\r\n#1 Elapsed time in Seconds" +
            //                       "\r\n#2 Spellname + Damage" +
            //                       "\r\n#3 Primary/Secondary Energy/Resource(Weapon)");
        }

        private List<TablePopulator> SpellTypeReport(SpellType spellType, Settings settings)
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings));
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

                list.Add(new TablePopulator
                {
                    Name = dSpell.Name,
                    DamagePerSecond = (int)dmgPerSecond,
                    DpsPercentage = ofTotal,
                    Executes = (int)executes,
                    DamagePerExecution = (int)avgdmgAvarage,
                    SpellType = dSpell.SpellType.ToString(),
                    Amount = (int)executes,
                    Avarage = (int)avgdmgAvarage,
                    CritChance = cc});   
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

