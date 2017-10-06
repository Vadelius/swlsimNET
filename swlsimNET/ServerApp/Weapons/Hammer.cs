using swlsimNET.ServerApp.Combat;
using swlsimNET.ServerApp.Models;
using swlsimNET.ServerApp.Spells;
using swlsimNET.ServerApp.Spells.Hammer;

namespace swlsimNET.ServerApp.Weapons
{
    public class Hammer : Weapon
    {
        private Passive _fastAndFurious;

        private bool _hasLetLoose;
        private bool _hasAnnihilate;
        private bool _enraged50;
        private bool _enraged100;

        private bool _init;

        private double _enragedLockTimeStamp;
        private double _timeSinceEnraged;        

        public bool FastAndFuriousBonus { get; private set; }
        public bool LetLooseBonus { get; private set; }

        public Hammer(WeaponType wtype, WeaponAffix waffix) : base(wtype, waffix)
        {
            _maxGimickResource = 100;
        }

        public override void AfterAttack(IPlayer player, ISpell spell, RoundResult rr)
        {
            // Only on first activation
            if (!_init)
            {
                _init = true;

                _fastAndFurious = player.GetPassive(nameof(FastAndFurious));
                _hasLetLoose = player.HasPassive(nameof(LetLoose));
                _hasAnnihilate = player.HasPassive(nameof(Annihilate));
            }

            var enraged50 = GimmickResource >= 50 && GimmickResource < 100;
            var enraged100 = GimmickResource >= 100;

            // Has any enraged treshold passed since last time
            if (!_enraged50 && enraged50)
            {
                // Passed 50 treshold
                _enragedLockTimeStamp = player.CurrentTimeSec;
            }
            else if(!_enraged100 && _enraged50 && enraged100)
            {
                // Reached 100 treshold
                _enragedLockTimeStamp = player.CurrentTimeSec;
            }

            _enraged50 = enraged50;
            _enraged100 = enraged100;

            if(_fastAndFurious == null) return;

            _timeSinceEnraged = player.CurrentTimeSec - _enragedLockTimeStamp;
            FastAndFuriousBonus = _timeSinceEnraged < 3.5;
        }

        public override double GetBonusBaseDamageMultiplier(IPlayer player, ISpell spell, double rageBeforeCast)
        {
            double bonusBaseDamageMultiplier = 0;

            if (LetLooseBonus)
            {
                // if using "Rampage" causes you to become Enraged increase base damage,

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
            }

            if (FastAndFuriousBonus)
            {
                bonusBaseDamageMultiplier += _fastAndFurious.BaseDamageModifier;
            }

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
                {
                    // Passed 50 treshold
                    rampageMadeUsEnraged = true;
                }
                else if (!enraged100ba && enraged50ba && enraged100aa)
                {
                    // Reached 100 treshold
                    rampageMadeUsEnraged = true;
                }

                // If Rampage attack made us enraged
                if (rampageMadeUsEnraged)
                {
                    // Consumable buff to Hammer that are used on next Demolish or Eruption
                    LetLooseBonus = true;
                }
            }
        }
    }
}

