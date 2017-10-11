using swlsimNET.ServerApp.Combat;
using swlsimNET.ServerApp.Models;
using swlsimNET.ServerApp.Spells;

namespace swlsimNET.ServerApp.Weapons
{
    public class Elemental : Weapon
    {
        private decimal LastElementalSpellTimeStamp { get; set; }
        private ISpell LastElementalSpell { get; set; }
        private decimal TimeSinceLastElementalSpell { get; set; }

        public Elemental(WeaponType wtype, WeaponAffix waffix) : base(wtype, waffix)
        {
            _maxGimickResource = 100;
        }
        public override void PreAttack(IPlayer player, RoundResult rr)
        {

        }

        public override double GetBonusBaseDamageMultiplier(IPlayer player, ISpell spell, decimal heatBeforeCast)
        {
            var hasFigurine = player.Settings.PrimaryWeaponProc == WeaponProc.FrozenFigurine && spell.ElementalType == "Cold";
            LastElementalSpell = spell;

            TimeSinceLastElementalSpell = player.CurrentTimeSec - LastElementalSpellTimeStamp;

            // Set new time stamp for new cast
            LastElementalSpellTimeStamp = player.CurrentTimeSec + spell.CastTime;

            if (player.Settings.PrimaryWeaponProc != WeaponProc.FrozenFigurine)
            {
                Decay(heatBeforeCast);
            }

            //HeatStop();

            // TODO: Add all constants to own file?
            if (heatBeforeCast >= 25 && heatBeforeCast <= 50)
            {
                // GimmickBonusDamage = 1.087; // 8.7%
                return hasFigurine ? 0.797 : 0.087;
            }
            if (heatBeforeCast >= 50 && heatBeforeCast <= 75)
            {
                // GimmickBonusDamage = 1.174; // 17.4%
                return hasFigurine ? 0.884 : 0.174;
            }
            if (heatBeforeCast >= 75 && heatBeforeCast <= 100)
            {
                // GimmickBonusDamage = 1.348 // 34.8%
                return hasFigurine ? 1.058 : 0.348;
            }

            // heatBeforeCast >= 0 && heatBeforeCast <= 25
            // Normal damage
            return 0;
        }

        public override void AfterAttack(IPlayer player, ISpell spell, RoundResult rr)
        {
            if (player.Settings.PrimaryWeaponProc == WeaponProc.UnstableElectronCore && GimmickResource > 50 && (spell.ElementalType == "Fire" || spell.ElementalType == "Lightning"))
            {
                player.AddBonusAttack(rr, new UnstableElectronCore());
            }
            if (player.Settings.PrimaryWeaponProc == WeaponProc.CryoChargedConduit && spell.ElementalType == "Cold")
            {
                GimmickResource -= 15;
                //TODO: and cause any targets hit to become frostbitten for 6 seconds. Critically hitting a frostbitten enemy with an Elemental attack deals an additional (3.45*Combat Power) magical damage.
                //What does it even mean?
            }
        }

        private void Decay(decimal heatBeforeCast)
        {
            // Corruption = -4 for each second.
            // Only reduce per second, so for example 1.5s = 1s
            var time = TimeSinceLastElementalSpell;
            int reduce = 0;

            if (heatBeforeCast <= 25)
            {
                // Heat = -1 per second.
                reduce = (int)(time * 1);
            }
            if (heatBeforeCast >= 26 && heatBeforeCast <= 50)
            {
                // Heat = -2 per second.
                reduce = (int)(time * 2);
            }
            if (heatBeforeCast >= 51 && heatBeforeCast <= 75)
            {
                // Heat = -3 per second.
                reduce = (int)(time * 3);
            }
            if (heatBeforeCast >= 76)
            {
                // Heat = -4 per second.
                reduce = (int)(time * 4);
            }

            GimmickResource -= reduce;

            if (GimmickResource < 0) GimmickResource = 0;
        }

        // TODO: Fix HeatStop
        //private void HeatStop()
        //{
        //    if (Player.Heat == 100)
        //    {
        //        // Not able to cast any abilities that generates Heat.
        //        // Casting a cooling ability at 100 heat will immideiatly reduce heat and allow heat generating casts again.
        //    }
        //}

        private class UnstableElectronCore : Spell
        {
            public UnstableElectronCore()
            {
                WeaponType = WeaponType.Elemental;
                SpellType = SpellType.Procc;
                BaseDamage = 0.17;
            }
        }
    }
}
