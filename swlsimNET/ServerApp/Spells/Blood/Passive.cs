using swlsim.Spells;
using swlsimNET.ServerApp.Models;
using swlsimNET.ServerApp.Weapons;

namespace swlsimNET.ServerApp.Spells.Blood
{
    public class LingeringCurse : Passive
    {
        public LingeringCurse()
        {
            WeaponType = WeaponType.Blood;
            SpellTypes.Add(typeof(DreadSigil));
            SpellType = SpellType.Dot;
            BaseDamage = 0.78;
            DotDuration = 5;
            PassiveBonusSpell = this;

            // TODO: Test
            // 0.78 CP per sec for 5 seconds DoT whenever DreadSigil is cast.
        }
    }

    public class Contaminate : Passive
    {
        public Contaminate()
        {
            WeaponType = WeaponType.Blood;
            SpellTypes.Add(typeof(Reap));
            BaseDamage = 0.31; // + 0.31 CP per sec to a total of 0.56 CP
        }
    }

    public class Flay : Passive
    {
        public Flay()
        {
            WeaponType = WeaponType.Blood;
            SpellTypes.Add(typeof(Maleficium));
            BaseDamage = 0.11; // TODO: Make sure it's 0.11 BaseDamage and not 0.11 BaseDamageModifier...
            ModelledInWeapon = true;
        }
    }

    public class Convergence : Passive
    {
        public Convergence(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Blood;
            SpellTypes.Add(typeof(SanguineCoalescence));
            // Causes next 4 blood attacks to deal additional 1.93 * CP extra.
        }
    }

    public class BloodFlow : Passive
    {
        public BloodFlow(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Blood;
            SpellTypes.Add(typeof(Rupture));
            // If Corruption(Gimmick) is > 60 we do a dot, dealing 0.36*CP/sec for 6 seconds.
            // Causes next 4 blood attacks to deal additional 1.93 * CP extra.
        }
    }

    public class Defilement : Passive
    {
        public Defilement()
        {
            WeaponType = WeaponType.Blood;
            SpellTypes.Add(typeof(Desecrate));
            ModelledInWeapon = true;
            // Corrution gain during Desecrate is increased by 20%
            // If at max corruption we gain 16.5% blood ability damage.
        }
    }

    public class Paroxysm : Passive
    {
        public Paroxysm(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Blood;
            SpellTypes.Add(typeof(RunicHex));
            // When Runic Hex expires we do a shittier Runic Hex for 0.31 * CP for 4 (3+1) seconds
        }
    }

    public class Desolate : Passive
    {
        public Desolate()
        {
            WeaponType = WeaponType.Blood;
            SpellTypes.Add(typeof(EldritchScourge));
            BaseDamageModifier = 0.52;
        }
    }

    public class CurseOfDestruction : Passive
    {
        public CurseOfDestruction(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Blood;
            // If Coprrution > 10 && We do a Blood Crit we throw a Curse on %target for 5 sec.
            // Curse causes %target to take 0.28 * CP for each crit we do on them.
        }
    }

    public class CrimsonPulse : Passive
    {
        public CrimsonPulse()
        {
            WeaponType = WeaponType.Blood;
            BonusSpellOnlyOnCrit = true;
            PassiveBonusSpell = this;
            SpecificWeaponTypeBonus = true;
            BaseDamage = 0.255; // 0.085 for 3 seconds. // TODO: Make dot
            // When we crit with blood abilities target suffers 0.085 * CP  for 3 seconds.
        }
    }

    public class Absolution : Passive
    {
        public Absolution(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Blood;
            // Whenever Corruption > 60 we set Corruption -= 50;
        }
    }

    public class SanguineCharm : Passive
    {
        public SanguineCharm(IPlayer player, string args = null)
        {
            WeaponType = WeaponType.Blood;
            // Whenever we lose and go under a CorruptionGimmick% treshhold
            // We gain 9% blood ability damage for 4 seconds.
        }
    }
}