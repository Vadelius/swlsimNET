using System;
using System.Collections.Generic;
using swlsimNET.ServerApp.Models;
using swlsimNET.ServerApp.Spells;
using swlsimNET.ServerApp.Spells.Pistol;
using swlsimNET.ServerApp.Weapons;


namespace swlsim.Spells.Pistol
{
    public class HighRoller : Spell
    {
        public HighRoller(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Pistol;
            SpellType = SpellType.Passive;
            // Controlled Shooting:  Increase base damage 10% on each successive hit
        }
    }

    public class LethalAim : Spell
    {
        public LethalAim(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Pistol;
            SpellType = SpellType.Passive;
            // Flourish: 6s 15% damage and cannot be evaded or glance
        }
    }

    public class Holdout : Spell
    {
        public Holdout(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Pistol;
            SpellType = SpellType.Passive;
            //  Unload: 33% Chance to not spin chambers if matching set
        }
    }

    public class RoyalFlush : Spell
    {
        public RoyalFlush(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Pistol;
            SpellType = SpellType.Passive;
            // All In: Additional effects if matching chamber, Double White: TAoE additional 0,34CP BaseDamage Exposed Debilitated, Double Blue: TAoE additional 0,43CP BaseDamage, Double Red: TAoE additional 0,87CP BaseDamage
        }
    }

    public class StackedDeck : Spell
    {
        public StackedDeck(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Pistol;
            SpellType = SpellType.Passive;
            // Increase duration of Full House to 5s
        }
    }

    public class Mulligan : Spell
    {
        public Mulligan(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Pistol;
            SpellType = SpellType.Passive;
            // If you dont get a Double Red set during Six Line reset cooldown
        }
    }

    public class FocusedFire : Spell
    {
        public FocusedFire(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Pistol;
            SpellType = SpellType.Passive;
            // If no matching chambers gain Double White, BaseDamage of Kill Blind increased to 3,47CP
        }
    }

    public class Bamboozle : Spell
    {
        public Bamboozle(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Pistol;
            SpellType = SpellType.Passive;
            // Trick Shot:  Stun Root or Snare, Stunned enemies additional 0,34CP damage from you or your allies, Rooted Enemies take 2,61CP BaseDamage when root ends, Snared enemies and other enemies near take 0,32CP every second
        }
    }

    public class FatalShot : Passive
    {
        public FatalShot()
        {
            WeaponType = WeaponType.Pistol;
            SpellTypes.Add(typeof(DualShot));
            BaseDamage = 0.81;
            BonusSpellOnlyOnCrit = true;
            PassiveBonusSpell = this;
            // Critical Hits with Dual Shot deal an additional 0,81CP BaseDamage
        }
    }

    public class BlastCrater : Spell
    {
        public BlastCrater(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Pistol;
            SpellType = SpellType.Passive;
            BaseDamage = 0;
            // Charged Blast:  Enemies still in the blast radius take 0,12CP BaseDamage every second they remain in the area, Slow
        }
    }

    public class BulletEcho : Spell
    {
        public BulletEcho(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Pistol;
            SpellType = SpellType.Passive;
            BaseDamage = 0;
            // When Ricochet expires Pistol attacks chain to nearby enemies 0,32CP damage 4s duration
        }
    }

    public class DeadlyDance : Passive
    {
        public DeadlyDance()
        {
            WeaponType = WeaponType.Pistol;
            SpellTypes.Add(typeof(BulletBallet));
            BaseDamage = 0.32;
            PassiveBonusSpell = this; //TODO: Check this.
            // Bullet Ballet: Additional 0,32 BaseDamage
        }
    }

    public class BeginnersLuck : Passive
    {
        public BeginnersLuck()
        {
            WeaponType = WeaponType.Pistol;
            SpecificWeaponTypeBonus = true;
            BaseDamageModifier = 0.14; // TODO: Check this value
            // Pistol abilites deal additional 0,04-0,24CP BaseDamage, 
            // BaseDamage dealt max out when the target is at maximum health
        }
    }

    public class FlechetteRounds : Spell
    {
        public FlechetteRounds(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Pistol;
            SpellType = SpellType.Passive;
            // If matching set of chambers Pistol attacks TAoE 0,09CP BaseDamage
        }
    }

    public class HeavyCaliberRounds : Passive
    {
        public HeavyCaliberRounds()
        {
            WeaponType = WeaponType.Pistol;
            SpecificSpellTypes.Add(new Passive
            {
                SpellTypes = new List<Type> { typeof(swlsimNET.ServerApp.Weapons.Pistol.WhiteChambers) },
                BaseDamageModifier = 0.28
            });

            SpecificSpellTypes.Add(new Passive
            {
                SpellTypes = new List<Type> { typeof(swlsimNET.ServerApp.Weapons.Pistol.BlueChambers) },
                BaseDamageModifier = 0.1415
            });
            SpecificSpellTypes.Add(new Passive
            {
                SpellTypes = new List<Type> { typeof(swlsimNET.ServerApp.Weapons.Pistol.RedChambers) },
                BaseDamageModifier = 0.09
            });
            // Increased bonus damage from matching set, Double White: 28%, Double Blue: 14.15%, Double Red: 9%
        }
    }

    public class DoubleDown : Spell
    {
        public DoubleDown(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Pistol;
            SpellType = SpellType.Passive;
            // 0.03CP-0.285CP damage based on length of 8s debuff
        }
    }

    public class Jackpot : Passive
    {
        public Jackpot()
        {
            // TODO: Test
            WeaponType = WeaponType.Pistol;
            ModelledInWeapon = true;
            BaseDamage = 0.32;
            // 0.32CP damage on matching chambers for 3s
        }
    }

    public class WinStreak : Spell
    {
        public WinStreak(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Pistol;
            SpellType = SpellType.Passive;
            // 0.06CP-0.335CP on matching chamber hit
        }
    }

    public class FullyLoaded : Spell
    {
        public FullyLoaded(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Pistol;
            SpellType = SpellType.Passive;
            // Whenever your Pistol Energy reaches 15 you automatically gain Double White set if you do not already have a set
        }
    }

    public class FixedGame : Passive
    {
        public FixedGame()
        {
            // TODO: Test
            WeaponType = WeaponType.Pistol;
            ModelledInWeapon = true;
            // If you haven't used a Pistol Ability in the last 4s your right chamber is set to match your left chamber
        }
    }
}