using swlsimNET.ServerApp.Models;
using swlsimNET.ServerApp.Spells;

namespace swlsimNET.ServerApp.Weapons
{
    public class Elemental : Weapon
    {
        private double LastElementalSpellTimeStamp { get; set; }
        private ISpell LastElementalSpell { get; set; }
        private double TimeSinceLastElementalSpell { get; set; }

        public Elemental(WeaponType wtype, WeaponAffix waffix) : base(wtype, waffix)
        {
            _maxGimickResource = 100;
        }

        public override double GetBonusBaseDamageMultiplier(IPlayer player, ISpell spell, double heatBeforeCast)
        {
            LastElementalSpell = spell;

            TimeSinceLastElementalSpell = player.CurrentTimeSec - LastElementalSpellTimeStamp;

            // Set new time stamp for new cast
            LastElementalSpellTimeStamp = player.CurrentTimeSec + spell.CastTime;

            Decay(heatBeforeCast);
            //HeatStop();

            if (heatBeforeCast >= 25 && heatBeforeCast <= 50)
            {
                // GimmickBonusDamage = 1.087; // 8.7%
                return 0.087;
            }
            if (heatBeforeCast >= 50 && heatBeforeCast <= 75)
            {
                // GimmickBonusDamage = 1.174; // 17.4%
                return 0.174;
            }
            if (heatBeforeCast >= 75 && heatBeforeCast <= 100)
            {
                // GimmickBonusDamage = 1.348 // 34.8%
                return 0.348;
            }

            // heatBeforeCast >= 0 && heatBeforeCast <= 25
            // Normal damage
            return 0;
        }

        private void Decay(double heatBeforeCast)
        {
            // Corruption = -4 for each second.
            // Only reduce per second, so for example 1.5s = 1s
            var time = TimeSinceLastElementalSpell;
            int reduce = 0;

            if (heatBeforeCast <= 25)
            {
                // Heat = -1 per second.
                reduce = (int) (time * 1);
            }
            if (heatBeforeCast >= 26 && heatBeforeCast <= 50)
            {
                // Heat = -2 per second.
                reduce = (int) (time * 2);
            }
            if (heatBeforeCast >= 51 && heatBeforeCast <= 75)
            {
                // Heat = -3 per second.
                reduce = (int) (time * 3);
            }
            if (heatBeforeCast >= 76)
            {
                // Heat = -4 per second.
                reduce = (int) (time * 4);
            }

            GimmickResource -= reduce;

            if (GimmickResource < 0) GimmickResource = 0;
        }

        //private void HeatStop()
        //{
        //    if (Player.Heat == 100)
        //    {
        //        // Not able to cast any abilities that generates Heat.
        //        // Casting a cooling ability at 100 heat will immideiatly reduce heat and allow heat generating casts again.
        //    }
        //}
    }
}
