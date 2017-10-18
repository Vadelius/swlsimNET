using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using swlSimulator.api.Combat;
using swlSimulator.api.Spells;
using swlSimulator.Models;

namespace swlSimulator.api
{
    public class Report
    {
        private readonly List<Attack> _allSpellCast = new List<Attack>();
        private readonly List<IBuff> _distinctBuffs = new List<IBuff>();
        private readonly List<ISpell> _distinctSpellCast = new List<ISpell>();
        private readonly StringBuilder _oneBuilder = new StringBuilder();
        private Settings _settings;
        private NumberFormatInfo nfi;

        public int TotalCrits { get; private set; }
        public int TotalHits { get; private set; }
        public double TotalDamage { get; private set; }
        public double TotalDps { get; private set; }
        public string FightDebug { get; private set; }
        public double LowestDps { get; private set; } = double.MaxValue;
        public double HighestDps { get; private set; }
        public double TotalSpellExecutes { get; private set; }

        public List<SpellResult> SpellBreakdownList { get; private set; }
        public List<BuffResult> BuffBreakdownList { get; private set; }
        public List<EnergySnap> EnergyList { get; private set; }
        public List<GimmickSnap> GimmickList { get; private set; }

        public bool GenerateReportData(List<FightResult> iterationFightResults, Settings settings)
        {
            _settings = settings;
            SpellBreakdownList = new List<SpellResult>();
            BuffBreakdownList = new List<BuffResult>();
            EnergyList = new List<EnergySnap>();
            GimmickList = new List<GimmickSnap>();
            InitReportData(iterationFightResults);
            GenerateSpellReportData();
            GenerateBuffReportData();

            FightDebug = _oneBuilder.ToString();
            TotalDps = TotalDamage / _settings.FightLength / _settings.Iterations;
            TotalSpellExecutes = Math.Round(TotalSpellExecutes, 2);

            return true;
        }

        private void GenerateSpellReportData()
        {
            // Set order here for spell reports
            SpellTypeReport(SpellType.Cast);
            SpellTypeReport(SpellType.Channel);
            SpellTypeReport(SpellType.Dot);
            SpellTypeReport(SpellType.Instant);
            //SpellTypeReport(SpellType.Buff);
            SpellTypeReport(SpellType.Procc);
            SpellTypeReport(SpellType.Gimmick);
            SpellTypeReport(SpellType.Passive);
        }

        private void GenerateBuffReportData()
        {
            foreach (var buffspell in _distinctBuffs)
            {
                BuffBreakdownList.Add(new BuffResult
                {
                    // TODO: Calculate values
                    Executes = buffspell.ActivationRounds.Count,
                    Interval = 20,
                    Name = buffspell.Name,
                    Refresh = 20,
                    Uptime = 20
                });
            }
        }

        private void InitReportData(List<FightResult> iterationFightResults)
        {
            nfi = (NumberFormatInfo) CultureInfo.InvariantCulture.NumberFormat.Clone();
            nfi.NumberGroupSeparator = " ";

            decimal lastEnergyChangeTimeStamp = 0;
            decimal lastGimmickChangeTimeStamp = 0;
            const int interval = 2; // Can't go higher due to inconsistent line-values.

            foreach (var iteration in iterationFightResults)
            {
                _oneBuilder.Clear();

                TotalDamage += iteration.TotalDamage;
                TotalHits += iteration.TotalHits;
                TotalCrits += iteration.TotalCrits;

                if (iteration.Dps > HighestDps)
                {
                    HighestDps = iteration.Dps;
                }

                if (iteration.Dps < LowestDps)
                {
                    LowestDps = iteration.Dps;
                }

                foreach (var rr in iteration.RoundResults)
                {
                    if (lastEnergyChangeTimeStamp == 0 || lastEnergyChangeTimeStamp + interval < rr.TimeSec)
                    {
                        EnergyList.Add(new EnergySnap
                        {
                            Time = rr.TimeSec,
                            PrimaryEnergy = rr.PrimaryEnergyEnd,
                            SecondaryEnergy = rr.SecondaryEnergyEnd,
                        });

                        lastEnergyChangeTimeStamp = rr.TimeSec;
                    }

                    if (lastGimmickChangeTimeStamp == 0 || lastGimmickChangeTimeStamp + interval < rr.TimeSec)
                    {
                        GimmickList.Add(new GimmickSnap
                        {
                            Time = rr.TimeSec,
                            PrimaryGimmick = rr.PrimaryGimmickEnd,
                            SecondaryGimmick = rr.SecondaryGimmickEnd
                        });

                        lastGimmickChangeTimeStamp = rr.TimeSec;
                    }

                    foreach (var a in rr.Attacks)
                    {
                        if (a.IsHit && a.IsCrit)
                        {
                            _oneBuilder.AppendLine($"<div>[{rr.TimeSec.ToString("#,0.0s", nfi)}] " +
                                                   $"{a.Spell.Name} *{a.Damage.ToString("#,##0,.0K", nfi)}* " +
                                                   $"E({rr.PrimaryEnergyEnd}/{rr.SecondaryEnergyEnd}) " +
                                                   $"R({rr.PrimaryGimmickEnd}/{rr.SecondaryGimmickEnd})</div>");
                        }
                        else if (a.IsHit && a.Spell.SpellType != SpellType.Procc)
                        {
                            _oneBuilder.AppendLine($"<div>[{rr.TimeSec.ToString("#,0.0s", nfi)}] " +
                                                   $"{a.Spell.Name} {a.Damage.ToString("#,##0,.0K", nfi)} " +
                                                   $"E({rr.PrimaryEnergyEnd}/{rr.SecondaryEnergyEnd}) " +
                                                   $"R({rr.PrimaryGimmickEnd}/{rr.SecondaryGimmickEnd})</div>");
                        }
                        else if (a.IsHit && a.Spell.SpellType == SpellType.Procc)
                        {
                            _oneBuilder.AppendLine($"<div>[{rr.TimeSec.ToString("#,0.0s", nfi)}] " +
                                                   $"[{a.Spell.Name}] proc!</div>");
                        }

                        _allSpellCast.Add(a);

                        if (_distinctSpellCast.All(s => s.Name != a.Spell.Name))
                        {
                            _distinctSpellCast.Add(a.Spell);
                        }
                    }

                    foreach (var buff in rr.Buffs)
                    {
                        if (_distinctBuffs.All(b => b.Name != buff.Name))
                        {
                            _distinctBuffs.Add(buff);
                        }
                    }
                }
            }

            _oneBuilder.AppendLine("\r\nOutput format:" +
                                   "\r\n#1 Elapsed time in Seconds" +
                                   "\r\n#2 Spellname + Damage" +
                                   "\r\n#3 Primary/Secondary Energy/Resource(Weapon)");
        }

        private void SpellTypeReport(SpellType spellType)
        {
            var dSpells = _distinctSpellCast.Where(s => s.SpellType == spellType).ToList();

            if (!dSpells.Any())
            {
                return;
            }

            foreach (var dSpell in dSpells)
            {
                var allOfSameSpellDatas = _allSpellCast.Where(s => s.Spell.Name == dSpell.Name).ToList();
                var alldmg = allOfSameSpellDatas.Sum(s => s.Damage);
                var avgDmg = allOfSameSpellDatas.Average(s => s.Damage);
                var crits = allOfSameSpellDatas.Count(s => s.IsCrit);
                var hits = allOfSameSpellDatas.Count(s => s.IsHit);

                TotalSpellExecutes += hits / (double)_settings.Iterations;

                SpellBreakdownList.Add(new SpellResult
                {
                    Name = dSpell.Name,
                    Dps = Math.Round(alldmg / _settings.FightLength / _settings.Iterations, 2),
                    DpsPercent = Math.Round(alldmg / TotalDamage * 100, 2),
                    Dpe = Math.Round(avgDmg, 2),
                    Executes = Math.Round(hits / (double) _settings.Iterations, 2),
                    Ticks = 0, // TODO: when implementing dot ticks / for channel ticks also?
                    CritChance = Math.Round(decimal.Divide(crits, hits) * 100, 2)
                });
            }
        }

        public class EnergySnap
        {
            public decimal Time { get; set; }
            public int PrimaryEnergy { get; set; }
            public int SecondaryEnergy { get; set; }
        }

        public class GimmickSnap
        {
            public decimal Time { get; set; }
            public decimal PrimaryGimmick { get; set; }
            public decimal SecondaryGimmick { get; set; }
        }

        public class BuffResult
        {
            public string Name { get; set; }
            public double Executes { get; set; }
            public double Refresh { get; set; }
            public double Interval { get; set; }
            public double Uptime { get; set; }
        }

        public class SpellResult
        {
            public string Name { get; set; }
            public double Dps { get; set; }
            public double DpsPercent { get; set; }
            public double Executes { get; set; }
            public double Dpe { get; set; }
            public double Ticks { get; set; }
            public decimal CritChance { get; set; }
        }
    }
}