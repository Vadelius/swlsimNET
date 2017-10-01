using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
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
        private StringBuilder _oneBuilder = new StringBuilder();

        private StringBuilder _twoBuilder = new StringBuilder();
        private NumberFormatInfo nfi;

        public int TotalCrits { get; private set; }
        public int TotalHits { get; private set; }
        public double TotalDamage { get; private set; }

        private double lowestDps = double.MaxValue;
        private double highestDps;

        public Tuple<string, string> GenerateReportData(List<FightResult> iterationFightResults, Settings settings)
        {
            InitReportData(iterationFightResults);

            // Export everything to JSON
            var serializer = new JsonSerializer
            {
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.None // Formatting.Indented for testing
            };

            // Write to local appdata folder since we always can access this
            var path = Environment.GetEnvironmentVariable("LocalAppData")
                       + $"\\{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}";

            Directory.CreateDirectory(path);
            var file = Path.Combine(path, "result.json");

            using (var sw = new StreamWriter(file))
            using (var writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, iterationFightResults);
            }

            var critPercent = decimal.Divide(TotalCrits, TotalHits) * 100;
            var dps = TotalDamage / settings.FightLength / settings.Iterations;

            var avgDamage = TotalDamage / settings.Iterations;

            GenerateSpellReportData(settings);
            //JsonExport(spellType: SpellType.Procc);
            return new Tuple<string, string>(_oneBuilder.ToString(), _twoBuilder.ToString());
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

        private static string[] _spellnames = new[] {""};

        public IList<TablePopulator> SpellTypeReport(SpellType spellType, Settings settings, List<TablePopulator> list,
            string nameOverride = "")
        {
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
                var executes = hits + crits;
                // [spellName, DPS, DPS%, Executes, DPE, SpellType, Count, Avarage, Crit%]

                list.Add(new TablePopulator() { Name = dSpell.Name, DamagePerSecond = (int)dmgPerSecond, DpsPercentage = ofTotal, Executes = executes, DamagePerExecution = (int)avgdmgAvarage, SpellType = dSpell.SpellType.ToString(), Count = executes, Avarage = (int)avgdmgAvarage, CritChance = cc});
    

            }
            return list;
        }
    }
}

