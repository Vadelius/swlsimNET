using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using swlsimNET.ServerApp.Combat;
using swlsimNET.ServerApp.Spells;

namespace swlsimNET.Models
{
    public class Report
    {
        private List<Attack> _allSpellCast = new List<Attack>();
        private List<ISpell> _distinctSpellCast = new List<ISpell>();
        private StringBuilder _oneBuilder = new StringBuilder();
        private StringBuilder _twoBuilder = new StringBuilder();
        private NumberFormatInfo nfi;
        private Settings _settings;

        public int TotalCrits { get; private set; }
        public int TotalHits { get; private set; }
        public double TotalDamage { get; private set; }
        public string FightDebug { get; private set; }
        public string SpellBreakdown { get; private set; }

        private double lowestDps = double.MaxValue;
        private double highestDps;

        public bool GenerateReportData(List<FightResult> iterationFightResults, Settings settings)
        {
            _settings = settings;
            InitReportData(iterationFightResults);

            GenerateSpellReportData();

            FightDebug = _oneBuilder.ToString();
            SpellBreakdown = _twoBuilder.ToString();

            // TODO: set any variables that are needed here, e.g. TotalDPS

            return true;
        }

        private void GenerateSpellReportData()
        {
            // Set order here for spell reports
            SpellTypeReport(SpellType.Cast);
            SpellTypeReport(SpellType.Channel);
            SpellTypeReport(SpellType.Dot);
            SpellTypeReport(SpellType.Instant);
            SpellTypeReport(SpellType.Buff);
            SpellTypeReport(SpellType.Procc);
            SpellTypeReport(SpellType.Gimmick);
            SpellTypeReport(SpellType.Passive);
        }
        private List<TablePopulator> SpellTypeReport(SpellType spellType, Settings settings)
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings));
            var dSpells = _distinctSpellCast.Where(s => s.SpellType == spellType).ToList();
            var list = new List<TablePopulator>();

            foreach (var dSpell in dSpells)
            {
                var allOfSameSpellDatas = _allSpellCast.Where(s => s.Spell.Name == dSpell.Name).ToList();
                var alldmg = allOfSameSpellDatas.Sum(s => s.Damage);
                var avgDmg = allOfSameSpellDatas.Average(s => s.Damage);
                var crits = allOfSameSpellDatas.Count(s => s.IsCrit);
                var hits = allOfSameSpellDatas.Count(s => s.IsHit);
                // Can't divide int with int, 4.9 will result in 4 etc, either print with decimal or do a correct rounding
                var avghits = hits / (double)settings.Iterations;
                var avgcrits = crits / (double)settings.Iterations;
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
                    CritChance = cc
                });
            }
            return list;
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
                        if (a.IsHit && a.IsCrit)
                        {
                            _oneBuilder.AppendLine($"[{rr.TimeMs.ToString("#,##0,.0s", nfi)}] " +
                                                   $"{a.Spell.Name} *{a.Damage.ToString("#,##0,.0K", nfi)}* " +
                                                   $"E({rr.PrimaryEnergyEnd}/{rr.SecondaryEnergyEnd}) " +
                                                   $"R({rr.PrimaryGimmickEnd}/{rr.SecondaryGimmickEnd})");
                        }
                        else if (a.IsHit && a.Spell.SpellType != SpellType.Procc)
                        {
                            _oneBuilder.AppendLine($"[{rr.TimeMs.ToString("#,##0,.0s", nfi)}] " +
                                                   $"{a.Spell.Name} {a.Damage.ToString("#,##0,.0K", nfi)} " +
                                                   $"E({rr.PrimaryEnergyEnd}/{rr.SecondaryEnergyEnd}) " +
                                                   $"R({rr.PrimaryGimmickEnd}/{rr.SecondaryGimmickEnd})");
                        }
                        else if (a.IsHit && a.Spell.SpellType == SpellType.Procc)
                        {
                            _oneBuilder.AppendLine($"[{rr.TimeMs.ToString("#,##0,.0s", nfi)}] " +
                                                   $"[{a.Spell.Name}] proc!");
                        }

                        _allSpellCast.Add(a);

                        if (_distinctSpellCast.All(s => s.Name != a.Spell.Name))
                        {
                            _distinctSpellCast.Add(a.Spell);
                        }
                    }
                }
            }

            _oneBuilder.AppendLine("\r\nOutput format:" +
                                   "\r\n#1 Elapsed time in Seconds" +
                                   "\r\n#2 Spellname + Damage" +
                                   "\r\n#3 Primary/Secondary Energy/Resource(Weapon)");
        }

        private void SpellTypeReport(SpellType spellType, string nameOverride = "")
        {
            var dSpells = _distinctSpellCast.Where(s => s.SpellType == spellType).ToList();

            if (!dSpells.Any()) return;

            _twoBuilder.AppendLine(string.IsNullOrEmpty(nameOverride)
                ? $"\r\n----- {spellType} summary normalized per fight -----"
                : $"\r\n----- {nameOverride} summary normalized per fight -----");

            foreach (var dSpell in dSpells)
            {
                var allOfSameSpellDatas = _allSpellCast.Where(s => s.Spell.Name == dSpell.Name).ToList();
                var alldmg = allOfSameSpellDatas.Sum(s => s.Damage);
                var avgDmg = allOfSameSpellDatas.Average(s => s.Damage);
                var crits = allOfSameSpellDatas.Count(s => s.IsCrit);
                var hits = allOfSameSpellDatas.Count(s => s.IsHit);

                // Can't divide int with int, 4.9 will result in 4 etc, either print with decimal or do a correct rounding
                var avghits = hits / (double) _settings.Iterations;
                var avgcrits = crits / (double) _settings.Iterations;
                var cc = decimal.Divide(crits, hits) * 100;
                var hdmg = allOfSameSpellDatas.Max(s => s.Damage);
                var ldmg = allOfSameSpellDatas.Where(s => s.IsHit).Min(s => s.Damage);

                var ofTotal = alldmg / TotalDamage * 100;

                if (dSpell.BaseDamage == 0)
                {
                    _twoBuilder.AppendLine($"{ofTotal:0.0}% | {dSpell.Name}, hits: {avghits.ToString("#,0.0", nfi)}");
                }
                else
                {
                    _twoBuilder.AppendLine(
                        $"{ofTotal:0.0}% | {dSpell.Name}, dmg: {avgDmg.ToString("#,##0,.0K", nfi)}, " +
                        $"high: {hdmg.ToString("#,##0,.0K", nfi)}, low: {ldmg.ToString("#,##0,.0K", nfi)}," +
                        $" hits: {avghits.ToString("#,0.0", nfi)}, crits: {avgcrits.ToString("#,0.0", nfi)} ({cc:0.0}%)");
                }
            }
        }

        private class TablePopulator

        {
            // [spellName, DPS, DPS%, Executes, DPE, SpellType, Count, Avarage, Crit%]

            public string Name { get; set; }
            public int DamagePerSecond { get; set; }
            public double DpsPercentage { get; set; }
            public int Executes { get; set; }
            public int DamagePerExecution { get; set; }
            public string SpellType { get; set; }
            public int Amount { get; set; }
            public int Avarage { get; set; }
            public decimal CritChance { get; set; }

        }
    }
}
    
