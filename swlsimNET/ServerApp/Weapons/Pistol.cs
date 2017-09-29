using swlsimNET.ServerApp.Combat;
using swlsimNET.ServerApp.Models;
using swlsimNET.ServerApp.Spells;
using swlsimNET.ServerApp.Spells.Pistol;

namespace swlsimNET.ServerApp.Weapons
{
    public enum Chamber
    {
        White, Blue, Red
    }

    internal class Pistol : Weapon
    {
        private Chamber LeftChamber { get; set; }
        private Chamber RightChamber { get; set; }
        private int ChamberLockTimeStamp { get; set; }
        private int LastPistolSpellTimeStamp { get; set; }

        private bool _init;
        private bool _jackpotBonus;

        private Passive _jackpot;
        private Passive _fixedGame;

        public Pistol(WeaponType wtype, WeaponAffix waffix) : base(wtype, waffix)
        {
            LeftChamber = Chamber.White;
            RightChamber = Chamber.White;
            ChamberLockTimeStamp = 0;
        }

        public override void PreAttack(IPlayer player, RoundResult rr)
        {
            // Only on first activation
            if (!_init)
            {
                _init = true;

                _jackpot = player.GetPassive(nameof(Jackpot));

                // If you haven't used a Pistol Ability in the last 4s your right chamber is set to match your left chamber
                _fixedGame = player.GetPassive(nameof(FixedGame));
            }
        }

        public override double GetBonusBaseDamage(IPlayer player, ISpell spell, double gimmickBeforeCast)
        {
            double bonusBaseDamage = 0;

            // Jackpot passive
            if (_jackpot != null && _jackpotBonus)
            {
                bonusBaseDamage += _jackpot.BaseDamage;
            }

            return bonusBaseDamage;
        }

        // Using a pistol ability triggers Chamber Roulette if it hits
        // Matching Chambers lasts for 3 seconds
        public override void AfterAttack(IPlayer player, ISpell spell, RoundResult rr)
        {
            var timeSinceLocked = player.CurrentTimeMs - ChamberLockTimeStamp;

            var timeSinceLastPistolSpell = player.CurrentTimeMs - LastPistolSpellTimeStamp;
            LastPistolSpellTimeStamp = player.CurrentTimeMs + spell.CastTimeMs;

            // Chambers locked as White during combat start
            if (timeSinceLocked > 3000)
            {
                ChamberRoulette();
                ChamberLockTimeStamp = player.CurrentTimeMs;
            }

            // Fixed Game passive
            if (timeSinceLastPistolSpell >= 4000 && LeftChamber != RightChamber)
            {
                RightChamber = LeftChamber;
            }

            if (LeftChamber == RightChamber)
            {
                if (_jackpot != null)
                {
                    _jackpotBonus = timeSinceLocked <= 3000;
                }

                switch (LeftChamber) 
                {
                    case Chamber.White:
                        player.AddBonusAttack(rr, new WhiteChambers(player));
                        break;
                    case Chamber.Blue:
                        player.AddBonusAttack(rr, new BlueChambers(player));
                        break;
                    case Chamber.Red:
                        player.AddBonusAttack(rr, new RedChambers(player));
                        break;
                }
            }
            else
            {
                _jackpotBonus = false;
            }
        }

        private void ChamberRoulette()
        {
            LeftChamber = Dice();
            RightChamber = Dice();
        }

        private Chamber Dice()
        {
            // 6 chambers. 3 white, 2 blue, 1 red.

            // TODO: var maxroll = 7 + PASSIVEBONUSES;
            var roll = Rnd.Next(1, 7);

            if (roll >= 1 && roll <= 3)
                return Chamber.White;
            if (roll >= 4 && roll <= 5)
                return Chamber.Blue;

            return Chamber.Red;
        }

        #region ChamberProcs

        public class WhiteChambers : Spell
        {
            public WhiteChambers(IPlayer player)
            {
                WeaponType = WeaponType.Pistol;
                SpellType = SpellType.Gimmick;
                BaseDamage = 0.65;
            }
        }

        public class BlueChambers : Spell
        {
            public BlueChambers(IPlayer player)
            {
                WeaponType = WeaponType.Pistol;
                SpellType = SpellType.Gimmick;
                BaseDamage = 1.25;
            }
        }

        public class RedChambers : Spell
        {
            public RedChambers(IPlayer player)
            {
                WeaponType = WeaponType.Pistol;
                SpellType = SpellType.Gimmick;
                BaseDamage = 2;
            }
        }

        #endregion
    }
}
