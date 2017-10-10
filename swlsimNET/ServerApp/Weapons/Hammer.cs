using System;
using System.Collections.Generic;
using System.Linq;
using swlsimNET.ServerApp.Combat;
using swlsimNET.ServerApp.Models;
using swlsimNET.ServerApp.Spells;
using swlsimNET.ServerApp.Spells.Hammer;

namespace swlsimNET.ServerApp.Weapons
{
    public class Hammer : Weapon
    {
        private readonly List<string> _hammerConsumers = new List<string>
        {
            "DemolishRage",
            "Eruption"
        };

        private int _demolishOriginalCost;
        private bool _enraged100;
        private bool _enraged50;

        private decimal _enragedLockTimeStamp;
        private int _eruptionOriginalCost;
        private Passive _fastAndFurious;
        private bool _hasAnnihilate;

        private bool _hasLetLoose;
        private bool _hasPneumaticMaul;

        private bool _init;

        private decimal _pneumaticStamp = -1;
        private decimal _timeSinceEnraged;

        public Hammer(WeaponType wtype, WeaponAffix waffix) : base(wtype, waffix)
        {
            _maxGimickResource = 100;
        }

        public bool FastAndFuriousBonus { get; private set; }
        public bool LetLooseBonus { get; private set; }
        public bool PneumaticAvailable { get; private set; }

        public override void PreAttack(IPlayer player, RoundResult rr)
        {
            // Only on first activation
            if (!_init)
            {
                _init = true;

                _fastAndFurious = player.GetPassive(nameof(FastAndFurious));
                _hasLetLoose = player.HasPassive(nameof(LetLoose));
                _hasAnnihilate = player.HasPassive(nameof(Annihilate));
                _hasPneumaticMaul = player.Settings.PrimaryWeaponProc == WeaponProc.PneumaticMaul;

                if (_hasPneumaticMaul)
                {
                    var demolishRage = player.Spells.Find(s => s.GetType() == typeof(DemolishRage));
                    var eruptionRage = player.Spells.Find(s => s.GetType() == typeof(EruptionRage));

                    _demolishOriginalCost = demolishRage?.PrimaryGimmickCost ?? 50;
                    _eruptionOriginalCost = eruptionRage?.PrimaryGimmickCost ?? 50;
                }
            }

            if (_hasPneumaticMaul)
                PneumaticMaulActive(player, _pneumaticStamp >= player.CurrentTimeSec && PneumaticAvailable);
        }

        public override void AfterAttack(IPlayer player, ISpell spell, RoundResult rr)
        {
            var enraged50 = GimmickResource >= 50 && GimmickResource < 100;
            var enraged100 = GimmickResource >= 100;

            // Has any enraged treshold passed since last time
            if (!_enraged50 && enraged50)
                _enragedLockTimeStamp = player.CurrentTimeSec;
            else if (!_enraged100 && _enraged50 && enraged100)
                _enragedLockTimeStamp = player.CurrentTimeSec;

            _enraged50 = enraged50;
            _enraged100 = enraged100;

            if (_fastAndFurious != null)
            {
                _timeSinceEnraged = player.CurrentTimeSec - _enragedLockTimeStamp;
                FastAndFuriousBonus = _timeSinceEnraged < 3.5m;
            }

            if (player.Settings.PrimaryWeaponProc == WeaponProc.PneumaticMaul)
                PneumaticMaul(player, rr, spell);

            var spellName = spell.Name;
            if (player.Settings.PrimaryWeaponProc == WeaponProc.FumingDespoiler && spellName != null &&
                _hammerConsumers.Contains(spellName, StringComparer.CurrentCultureIgnoreCase))
                player.AddBonusAttack(rr, new FumingDespoiler(player));
        }

        public override double GetBonusBaseDamageMultiplier(IPlayer player, ISpell spell, double rageBeforeCast)
        {
            double bonusBaseDamageMultiplier = 0;

            if (LetLooseBonus)
                if (spell.GetType() == typeof(Demolish) || spell.GetType() == typeof(DemolishRage))
                {
                    // Demolish: 30 %
                    bonusBaseDamageMultiplier += 0.3;
                    LetLooseBonus = false; // Buff consumed
                }
                else if (_hasAnnihilate &&
                         (spell.GetType() == typeof(Eruption) || spell.GetType() == typeof(EruptionRage)))
                {
                    // Eruption with Annihilate Passive: 20%
                    bonusBaseDamageMultiplier += 0.2;
                    LetLooseBonus = false; // Buff consumed
                }

            if (FastAndFuriousBonus)
                bonusBaseDamageMultiplier += _fastAndFurious.BaseDamageModifier;

            return bonusBaseDamageMultiplier;
        }

        public override void OnHit(IPlayer player, ISpell spell, double rageBeforeCast)
        {
            // TODO: Save enrage states between rounds instead

            if (_hasLetLoose && spell.GetType() == typeof(Rampage))
            {
                // Enraged status before attack
                var enraged50ba = rageBeforeCast >= 50 && rageBeforeCast < 100;
                var enraged100ba = rageBeforeCast >= 100 && rageBeforeCast > 50;

                // Enraged status after attack
                var enraged50aa = GimmickResource >= 50 && GimmickResource < 100;
                var enraged100aa = GimmickResource >= 100;

                var rampageMadeUsEnraged = false;

                // Has any enraged treshold passed since since last time
                if (!enraged50ba && enraged50aa)
                    rampageMadeUsEnraged = true;
                else if (!enraged100ba && enraged50ba && enraged100aa)
                    rampageMadeUsEnraged = true;

                // If Rampage attack made us enraged
                if (rampageMadeUsEnraged)
                    LetLooseBonus = true;
            }
        }

        private void PneumaticMaul(IPlayer player, RoundResult rr, ISpell spell)
        {
            // On critical hit with a Hammer ability 
            // gain the benefits of the Enrage bonus effects on your abilities without spending any Rage and without being Enraged.
            // This effect can only occur once every 9 seconds.

            var pneumaticBuffActive = _pneumaticStamp >= player.CurrentTimeSec;

            // Buff is up but used
            if (pneumaticBuffActive && !PneumaticAvailable)
                return;

            // Buff is up and not used
            if (pneumaticBuffActive && PneumaticAvailable)
                if (spell.GetType() == typeof(DemolishRage) || spell.GetType() == typeof(EruptionRage))
                {
                    // Buff consumed
                    PneumaticAvailable = false;
                    return;
                }

            // Buff is not up and not on cooldown
            var attack = rr.Attacks.FirstOrDefault();
            if (attack != null && attack.IsCrit)
            {
                // Activate buff and set to available
                PneumaticAvailable = true;
                _pneumaticStamp = player.CurrentTimeSec + 9;
            }
        }

        private void PneumaticMaulActive(IPlayer player, bool active)
        {
            var demolish = player.Spells.Where(s => s.GetType() == typeof(DemolishRage));
            var eruption = player.Spells.Where(s => s.GetType() == typeof(EruptionRage));

            if (active)
            {
                foreach (var d in demolish) d.PrimaryGimmickCost = 0;
                foreach (var e in eruption) e.PrimaryGimmickCost = 0;
                PneumaticAvailable = true;
            }
            else
            {
                foreach (var d in demolish) d.PrimaryGimmickCost = _demolishOriginalCost;
                foreach (var e in eruption) e.PrimaryGimmickCost = _eruptionOriginalCost;
                PneumaticAvailable = false;
            }
        }

        public class FumingDespoiler : Spell
        {
            public FumingDespoiler(IPlayer player)
            {
                WeaponType = WeaponType.Hammer;
                SpellType = SpellType.Gimmick;
                BaseDamage = 0.825;
            }
        }
    }
}