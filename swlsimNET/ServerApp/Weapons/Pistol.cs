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
        public Chamber LeftChamber { get; set; }
        public Chamber RightChamber { get; set; }
        private int ChamberLockTimeStamp { get; set; }
        private int LastPistolSpellTimeStamp { get; set; }

        private bool _init;
        private bool _jackpotBonus;

        private Passive _jackpot;
        private Passive _fixedGame;
        private Passive _fullyLoaded;
        private Passive _winStreak;
        private Passive _flechetteRounds;
        private Passive _holdout;

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

                // 0.32CP damage on matching chambers for 3s
                _jackpot = player.GetPassive(nameof(Jackpot));

                // If you haven't used a Pistol Ability in the last 4s your right chamber is set to match your left chamber
                _fixedGame = player.GetPassive(nameof(FixedGame));

                // Whenever your Pistol Energy reaches 15 you automatically gain Double White set if you do not already have a set
                _fullyLoaded = player.GetPassive(nameof(FullyLoaded));

                // 0.06CP-0.335CP on matching chamber hit
                _winStreak = player.GetPassive(nameof(WinStreak));

                // If matching set of chambers Pistol attacks TAoE 0,09CP BaseDamage
                _flechetteRounds = player.GetPassive(nameof(FlechetteRounds));

                // Unload: 33% Chance to not spin chambers if matching set
                _holdout = player.GetPassive(nameof(Holdout));
            }

            var timeSinceLastPistolSpell = player.CurrentTimeMs - LastPistolSpellTimeStamp;

            // Fully loaded passive
            if (_fullyLoaded != null && Energy == 15 && LeftChamber != RightChamber)
            {
                RightChamber = Chamber.White;
                LeftChamber = Chamber.White;
                ChamberLockTimeStamp = player.CurrentTimeMs;
            }

            // Fixed Game passive
            if (_fixedGame != null && timeSinceLastPistolSpell >= 4000 && LeftChamber != RightChamber)
            {
                RightChamber = LeftChamber;
                ChamberLockTimeStamp = player.CurrentTimeMs;
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
            LastPistolSpellTimeStamp = player.CurrentTimeMs + spell.CastTimeMs;

            // Chambers locked as White during combat start
            if (timeSinceLocked > 3000)
            {
                if (_holdout != null && LeftChamber == RightChamber)
                {
                    // 33% chance not to reroll chambers
                    if (Rnd.Next(1, 4) < 3 && spell.GetType() == typeof(Unload))
                    {
                        ChamberRoulette(player);
                    }
                }
                else
                {
                    ChamberRoulette(player);
                }
            }

            if (LeftChamber == RightChamber)
            {
                // Jackpot passive
                if (_jackpot != null)
                {
                    _jackpotBonus = timeSinceLocked <= 3000;
                }

                // Winstreak passives
                if (_winStreak != null)
                {
                    player.AddBonusAttack(rr, new WinStreak(GetRandomNumber(0.06, 0.335)));
                }

                // Flechette Rounds passives
                if (_flechetteRounds != null)
                {
                    player.AddBonusAttack(rr, new FlechetteRounds());
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

        private void ChamberRoulette(IPlayer player)
        {
            LeftChamber = Dice();
            RightChamber = Dice();
            ChamberLockTimeStamp = player.CurrentTimeMs;
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