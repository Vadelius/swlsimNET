using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using swlsimNET.ServerApp.Combat;
using swlsimNET.ServerApp.Spells;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace swlsimNET.Models
{
    public class Results
    {
        public double TheDps => _theDps;
        private List<Attack> _allSpellCast = new List<Attack>();
        private List<ISpell> _distinctSpellCast = new List<ISpell>();
        private NumberFormatInfo nfi;

        public int TotalCrits { get; private set; }
        public int TotalHits { get; private set; }
        public double TotalDamage { get; private set; }

        private double lowestDps = double.MaxValue;
        private double highestDps;
        private double _theDps;

        public void GenerateReportData(List<FightResult> iterationFightResults)
        {
            InitReportData(iterationFightResults);
            GenerateSpellReportData();    
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

    private void InitReportData(List<FightResult> iterationFightResults)
    {
        var settings = new Settings();
            nfi = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
        nfi.NumberGroupSeparator = " ";
        _theDps = TotalDamage /settings.FightLength / settings.Iterations;
        foreach (var iteration in iterationFightResults)
        {

            TotalDamage += iteration.TotalDamage;
            TotalHits += iteration.TotalHits;
            TotalCrits += iteration.TotalCrits;
            if (iteration.Dps > highestDps) highestDps = iteration.Dps;
            if (iteration.Dps < lowestDps) lowestDps = iteration.Dps;

            //foreach (var rr in iteration.RoundResults)
                //{

                //}


            }



        }

        private void SpellTypeReport(SpellType spellType, string nameOverride = "")
    {
        var dSpells = _distinctSpellCast.Where(s => s.SpellType == spellType).ToList();

        if (!dSpells.Any()) return;

        foreach (var dSpell in dSpells)
        {

        }
    }
}
}