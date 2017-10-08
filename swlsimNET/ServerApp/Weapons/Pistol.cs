using System.Linq;
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
        public Chamber LeftChamber { get; set; }
        public Chamber RightChamber { get; set; }
        private int MiseryAndMaliceTimeStamp { get; set; }
        private double ChamberLockTimeStamp { get; set; }
        private double LastPistolSpellTimeStamp { get; set; }

        private bool _init;
        private bool _jackpotBonus;
        private double _miseryAndMaliceBonus;
        private bool _harmonisers = false;
        private bool _sixShooters = false;
        private bool _miseryAndMalice = false;
        private bool _annihilators = false;
        private bool _heavyCaliberPistols = false;

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
            }

            var timeSinceLastPistolSpell = player.CurrentTimeSec - LastPistolSpellTimeStamp;

            // Fully loaded passive
            if (_fullyLoaded != null && Energy == 15 && LeftChamber != RightChamber)
            {
                RightChamber = Chamber.White;
                LeftChamber = Chamber.White;
                ChamberLockTimeStamp = player.CurrentTimeSec;
            }

            // Fixed Game passive
            if (_fixedGame != null && timeSinceLastPistolSpell >= 4 && LeftChamber != RightChamber)
            {
                RightChamber = LeftChamber;
                ChamberLockTimeStamp = player.CurrentTimeSec;
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

            // Misery & Malice Weapon
            if (_miseryAndMalice)
            {
                bonusBaseDamage += _miseryAndMaliceBonus;
            }

            return bonusBaseDamage;
        }

        // Using a pistol ability triggers Chamber Roulette if it hits
        // Matching Chambers lasts for 3 seconds
        public override void AfterAttack(IPlayer player, ISpell spell, RoundResult rr)
        {
            var timeSinceLocked = player.CurrentTimeSec - ChamberLockTimeStamp;
            LastPistolSpellTimeStamp = player.CurrentTimeSec + spell.CastTime;

            // KillBlind Active + FocusedFire Passive.
            if (_focusedFire != null && LeftChamber != RightChamber && spell.Name == "Kill Blind")
            {
                LeftChamber = Chamber.White;
                RightChamber = Chamber.White;
            }

            // Chambers locked as White during combat start
            if ((timeSinceLocked > 3 && spell.Name != "KillBlind") || (timeSinceLocked > 4.5 && spell.Name == "KillBlind"))
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
                    _jackpotBonus = timeSinceLocked <= 3;
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

                // Misery & Malice Weapon
                if (_miseryAndMalice)
                {
                    _miseryAndMaliceBonus = 0.06; // TODO: 6% with all pistol abilities
                }

                switch (LeftChamber)
                {

                    case Chamber.White:
                        if (_annihilators) { player.AddBonusAttack(rr, new Cb3Annihilators(player)); }
                        if (_harmonisers)
                        {
                            var roll = Rnd.Next(1, 5);
                            if (roll == 4)
                            {
                                LeftChamber = Chamber.Blue; RightChamber = Chamber.Blue;
                                player.AddBonusAttack(rr, new BlueChambers(player));
                            }
                            else player.AddBonusAttack(rr, new WhiteChambers(player));
                        }
                        else player.AddBonusAttack(rr, new WhiteChambers(player));
                        break;
                    case Chamber.Blue:
                        if (_annihilators) { player.AddBonusAttack(rr, new Cb3Annihilators(player)); }
                        if (_harmonisers)
                        {
                            var roll = Rnd.Next(1, 101);
                            if (roll <= 15)
                            {
                                LeftChamber = Chamber.Red; RightChamber = Chamber.Red;
                                player.AddBonusAttack(rr, new RedChambers(player));
                            }
                            else player.AddBonusAttack(rr, new BlueChambers(player));
                        }
                        else player.AddBonusAttack(rr, new BlueChambers(player));
                        break;
                    case Chamber.Red:
                        if (_annihilators) { player.AddBonusAttack(rr, new Cb3Annihilators(player)); }
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
            if (_sixShooters)
            { ChamberLockTimeStamp = player.CurrentTimeSec - 0.5; }
            else ChamberLockTimeStamp = player.CurrentTimeSec;
        }

        private Chamber Dice()
        {
            // 6 chambers. 3 white, 2 blue, 1 red.

            // TODO: var maxroll = 7 + PASSIVEBONUSES;

            if (_heavyCaliberPistols)
            {
                var caliberRoll = Rnd.Next(1, 7);
                // "You are more likely to roll a Double Red set of chambers, but less likely to roll a Double White or Double Blue set of chambers." Thanks Funcom.
                // Purely speculation below.
                if (caliberRoll >= 2 && caliberRoll <= 3)
                    return Chamber.White;
                if (caliberRoll == 4)
                    return Chamber.Blue;
                if (caliberRoll >= 5)
                    return Chamber.Red;
            }

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

        public class Cb3Annihilators : Spell
        {
            public Cb3Annihilators(IPlayer player)
            {
                WeaponType = WeaponType.Pistol;
                SpellType = SpellType.Gimmick;
                BaseDamage = 0.86;
            }
        }

        #endregion
    }
}