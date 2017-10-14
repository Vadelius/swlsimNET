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

    public class Pistol : Weapon
    {
        public Chamber LeftChamber { get; private set; }
        public Chamber RightChamber { get; private set; }

        public decimal ChamberLockTimeStamp { get; private set; }
        public decimal LastPistolSpellTimeStamp { get; private set; }

        private bool _init;
        private bool _jackpotBonus;
        private bool _annihilators;
        private bool _harmonisers;

        private Passive _jackpot;
        private Passive _fixedGame;
        private Passive _fullyLoaded;
        private Passive _winStreak;
        private Passive _flechetteRounds;
        private Passive _holdout;
        private Passive _focusedFire;

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

                // If no matching chambers gain Double White, BaseDamage of Kill Blind increased to 3,47CP
                _focusedFire = player.GetPassive(nameof(FocusedFire));

                // Whenever your Pistol Energy reaches 15 you automatically gain Double White set if you do not already have a set
                _fullyLoaded = player.GetPassive(nameof(FullyLoaded));

                // 0.06CP-0.335CP on matching chamber hit
                _winStreak = player.GetPassive(nameof(WinStreak));

                // If matching set of chambers Pistol attacks TAoE 0,09CP BaseDamage
                _flechetteRounds = player.GetPassive(nameof(FlechetteRounds));

                // Unload: 33% Chance to not spin chambers if matching set
                _holdout = player.GetPassive(nameof(Holdout));

                _annihilators = player.Settings.PrimaryWeaponProc == WeaponProc.Cb3Annihilators;
                _harmonisers = player.Settings.PrimaryWeaponProc == WeaponProc.SovTechHarmonisers;
            }

            var timeSinceLastPistolSpell = player.CurrentTimeSec - LastPistolSpellTimeStamp;

            if (_fullyLoaded != null && Energy == 15 && LeftChamber != RightChamber)
            {
                RightChamber = Chamber.White;
                LeftChamber = Chamber.White;
                ChamberLockTimeStamp = player.CurrentTimeSec;
            }

            if (_fixedGame != null && timeSinceLastPistolSpell >= 4 && LeftChamber != RightChamber)
            {
                RightChamber = LeftChamber;
                ChamberLockTimeStamp = player.CurrentTimeSec;
            }
        }

        public override double GetBonusBaseDamage(IPlayer player, ISpell spell, decimal gimmickBeforeCast)
        {
            double bonusBaseDamage = 0;

            // Jackpot passive
            if (_jackpot != null && _jackpotBonus)
            {
                bonusBaseDamage += _jackpot.BaseDamage;
            }

            return bonusBaseDamage;
        }

        public override double GetBonusBaseDamageMultiplier(IPlayer player, ISpell spell, decimal gimmickBeforeCast)
        {
            double bonusBaseDamageMultiplier = 0;
            if (LeftChamber == RightChamber)
            {
                if (player.Settings.PrimaryWeaponProc == WeaponProc.MiseryAndMalice)
                {
                    bonusBaseDamageMultiplier = 0.06;
                }
            }

            return bonusBaseDamageMultiplier;
        }

        // Using a pistol ability triggers Chamber Roulette if it hits
        // Matching Chambers lasts for 3 seconds
        public override void AfterAttack(IPlayer player, ISpell spell, RoundResult rr)
        {
            var timeSinceLocked = player.CurrentTimeSec - ChamberLockTimeStamp;
            LastPistolSpellTimeStamp = player.CurrentTimeSec + spell.CastTime;            

            if (_focusedFire != null && LeftChamber != RightChamber && spell.GetType() == typeof(KillBlind))
            {
                LeftChamber = Chamber.White;
                RightChamber = Chamber.White;
            }

            if (timeSinceLocked > 3)
            {
                if (_holdout != null && LeftChamber == RightChamber)
                {
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
                if (_jackpot != null)
                {
                    _jackpotBonus = timeSinceLocked <= 3;
                }

                if (_winStreak != null)
                {
                    player.AddBonusAttack(rr, new WinStreak(GetRandomNumber(0.06, 0.335)));
                }

                if (_flechetteRounds != null)
                {
                    player.AddBonusAttack(rr, new FlechetteRounds());
                }

                switch (LeftChamber)
                {
                    case Chamber.White:
                        if (_annihilators)
                        {
                            player.AddBonusAttack(rr, new Cb3Annihilators(player));
                        }
                        if (_harmonisers)
                        {
                            if (Rnd.Next(1, 5) == 4)
                            {
                                LeftChamber = Chamber.Blue;
                                RightChamber = Chamber.Blue;
                                player.AddBonusAttack(rr, new BlueChambers(player));
                            }
                            else
                            {
                                player.AddBonusAttack(rr, new WhiteChambers(player));
                            }
                        }
                        else
                        {
                            player.AddBonusAttack(rr, new WhiteChambers(player));
                        }
                        break;

                    case Chamber.Blue:
                        if (_annihilators)
                        {
                            player.AddBonusAttack(rr, new Cb3Annihilators(player));
                        }
                        if (_harmonisers)
                        {
                            if (Rnd.Next(1, 101) <= 15)
                            {
                                LeftChamber = Chamber.Red;
                                RightChamber = Chamber.Red;
                                player.AddBonusAttack(rr, new RedChambers(player));
                            }
                            else
                            {
                                player.AddBonusAttack(rr, new BlueChambers(player));
                            }
                        }
                        else
                        {
                            player.AddBonusAttack(rr, new BlueChambers(player));
                        }
                        break;

                    case Chamber.Red:
                        if (_annihilators)
                        {
                            player.AddBonusAttack(rr, new Cb3Annihilators(player));
                        }
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
            LeftChamber = Dice(player);
            RightChamber = Dice(player);
            ChamberLockTimeStamp = player.Settings.PrimaryWeaponProc == WeaponProc.SixShooters ? player.CurrentTimeSec - 0.5m : player.CurrentTimeSec;
        }

        private Chamber Dice(IPlayer player)
        {
            var roll = Rnd.Next(1, 7);

            if (player.Settings.PrimaryWeaponProc == WeaponProc.HeavyCaliberPistols)
            {
                // "You are more likely to roll a Double Red set of chambers, but less likely to roll a Double White or Double Blue set of chambers." Thanks Funcom.
                // Purely speculation below. TODO: Confirm..
                if (roll >= 2 && roll <= 3)
                    return Chamber.White;
                if (roll == 4)
                    return Chamber.Blue;
                if (roll >= 5)
                    return Chamber.Red;
            } 

            if (roll >= 1 && roll <= 3)
                return Chamber.White;
            if (roll >= 4 && roll <= 5)
                return Chamber.Blue;

            return Chamber.Red;
        }
    }
}