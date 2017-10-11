using System.Collections.Generic;
using System.Linq;
using Expressions;
using swlSimulator.api.Combat;
using swlSimulator.api.Spells;
using swlSimulator.api.Weapons;
using swlSimulator.Models;

namespace swlSimulator.api.Models
{
    public class Player : IPlayer, ICombat
    {
        private bool _passivesInitiated;
        public ExpressionContext Context;

        public Item Item { get; }
        public Settings Settings { get; set; }
        public Spell CurrentSpell { get; set; }
        public BuffWrapper Buff { get; }
        public List<ISpell> Spells { get; set; }
        public List<IBuff> Buffs { get; }
        public List<IBuff> AbilityBuffs { get; }
        public List<Passive> Passives { get; set; }
        public Weapon PrimaryWeapon { get; set; }
        public Weapon SecondaryWeapon { get; set; }

        public double CombatPower { get; protected set; } = 1200;
        public double GlanceReduction { get; protected set; } = 0.3;
        public double CriticalChance { get; protected set; } = 0.1;
        public double CritPower { get; set; } = 2.3;
        public double BasicSignetBoost { get; protected set; } = 1.74;
        public double PowerSignetBoost { get; protected set; } = 1.18;
        public double EliteSignetBoost { get; protected set; } = 1.43;
        public decimal EliteSignetCooldownReduction { get; protected set; } = 0;
        public double WaistSignetBoost { get; protected set; } = 1.30;

        public decimal Interval { get; set; }
        public decimal CastTime { get; set; }
        public decimal CurrentTimeSec { get; private set; }
        public decimal GCD { get; set; }    

        // Weapon gimmick resource (for APL)
        public decimal Chi => GetWeaponResourceFromType(WeaponType.Blade);
        public decimal Corruption => GetWeaponResourceFromType(WeaponType.Blood);
        public decimal Fury => GetWeaponResourceFromType(WeaponType.Fist);
        public decimal Heat => GetWeaponResourceFromType(WeaponType.Elemental);
        public decimal Paradox => GetWeaponResourceFromType(WeaponType.Chaos);
        public decimal Rage => GetWeaponResourceFromType(WeaponType.Hammer);
        public decimal Shells => GetWeaponResourceFromType(WeaponType.Shotgun);
        public bool Grenade => GetWeaponResourceFromType(WeaponType.Rifle) > 0;

        // Weapon wrappers (for APL)
        public Weapon Blade => GetWeaponFromType(WeaponType.Blade);
        public Weapon Blood => GetWeaponFromType(WeaponType.Blood);
        public Weapon Chaos => GetWeaponFromType(WeaponType.Chaos);
        public Weapon Elemental => GetWeaponFromType(WeaponType.Elemental);
        public Weapon Fist => GetWeaponFromType(WeaponType.Fist);
        public Weapon Hammer => GetWeaponFromType(WeaponType.Hammer);
        public Weapon Pistol => GetWeaponFromType(WeaponType.Pistol);
        public Weapon Rifle => GetWeaponFromType(WeaponType.Rifle);
        public Weapon Shotgun => GetWeaponFromType(WeaponType.Shotgun);

        public Player(Settings settings)
        {
            Settings = settings;
            PrimaryWeapon = GetWeaponFromType(settings.PrimaryWeapon, settings.PrimaryWeaponAffix);
            SecondaryWeapon = GetWeaponFromType(settings.SecondaryWeapon, settings.SecondaryWeaponAffix);
            Passives = GetSelectedPassives();      

            Buffs = new List<IBuff>();
            {
                if (settings.Exposed) Buffs.Add(new Exposed());
                if (settings.OpeningShot) Buffs.Add(new OpeningShot());
                if (settings.Savagery) Buffs.Add(new Savagery());
            }

            AbilityBuffs = new List<IBuff>();
            InitAbilityBuffs();

            CombatPower = settings.CombatPower;
            GlanceReduction = settings.GlanceReduction / 100;
            CriticalChance = settings.CriticalChance / 100 + 0.075;
            CritPower = settings.CritPower / 100 + 1.3;
            BasicSignetBoost = settings.BasicSignet / 100 + 1;
            PowerSignetBoost = settings.PowerSignet / 100 + 1;
            EliteSignetBoost = settings.HeadSignetIsCdr ? 1 : settings.EliteSignet / 100 + 1;
            EliteSignetCooldownReduction = (decimal) (settings.HeadSignetIsCdr ? settings.EliteSignet / 100 : 0);
            WaistSignetBoost = settings.WaistSignet / 100;

            var apl = new AplReader(this, settings.Apl);
            Spells = apl.GetApl();

            this.Buff = new BuffWrapper(this);
            this.Item = new Item(this);
        }

        private Weapon GetWeaponFromType(WeaponType? wtypenullable, WeaponAffix waffix)
        {
            var wtype = (WeaponType) wtypenullable;

            switch (wtype)
            {
                case WeaponType.Blade:
                    return new Blade(wtype, waffix);
                case WeaponType.Blood:
                    return new Blood(wtype, waffix);
                case WeaponType.Chaos:
                    return new Chaos(wtype, waffix);
                case WeaponType.Elemental:
                    return new Elemental(wtype, waffix);
                case WeaponType.Fist:
                    return new Fist(wtype, waffix);
                case WeaponType.Hammer:
                    return new Hammer(wtype, waffix);
                case WeaponType.Pistol:
                    return new Pistol(wtype, waffix);
                case WeaponType.Rifle:
                    return new Rifle(wtype, waffix);
                case WeaponType.Shotgun:
                    return new Shotgun(wtype, waffix);
            }

            return null;
        }

        private void InitAbilityBuffs()
        {
            AbilityBuffs.Add(new Spells.Blade.Buffs.SupremeHarmony());
            AbilityBuffs.Add(new Spells.Fist.Buffs.Savagery());
            AbilityBuffs.Add(new Spells.Hammer.Buffs.UnstoppableForce());
            AbilityBuffs.Add(new Spells.Pistol.Buffs.Flourish());
        }

        // TODO: Simplify
        private List<Passive> GetSelectedPassives()
        {
            var selectedPassives = new List<Passive>();

            var passive = Settings.AllPassives.Find(p => p.Name == Settings.Passive1);
            if (passive != null) selectedPassives.Add(passive);

            passive = Settings.AllPassives.Find(p => p.Name == Settings.Passive2);
            if (passive != null) selectedPassives.Add(passive);

            passive = Settings.AllPassives.Find(p => p.Name == Settings.Passive3);
            if (passive != null) selectedPassives.Add(passive);

            passive = Settings.AllPassives.Find(p => p.Name == Settings.Passive4);
            if (passive != null) selectedPassives.Add(passive);

            passive = Settings.AllPassives.Find(p => p.Name == Settings.Passive5);
            if (passive != null) selectedPassives.Add(passive);

            return selectedPassives;
        }

        private void InitPassives()
        {
            // Go through all passives
            foreach (var passive in Passives)
            {
                passive.Init(this);

                if (passive.ModelledInWeapon) continue; ;

                passive.LoopSpellsFromPassive(this);

                // Any sub passives
                foreach (var subPassive in passive.SpecificSpellTypes)
                {
                    subPassive.LoopSpellsFromPassive(this);
                }

                if (!passive.SpecificWeaponTypeBonus) continue;

                // The passive modifies all spells of the same weapon type
                var allSpells = Spells.Where(s => s.WeaponType == passive.WeaponType);
                foreach (var s in allSpells)
                {
                    passive.ModifySpellWithPassive(s);
                }
            }

            _passivesInitiated = true;
        }

        public RoundResult NewRound(decimal currentSec, decimal interval)
        {
            if (!_passivesInitiated) InitPassives();

            Interval = interval;
            CurrentTimeSec = currentSec;

            var rr = new RoundResult
            {
                TimeSec = currentSec,
                Interval = interval
            };

            // TODO: Check order, e.g. should passive bonus spell get bonus from buff cast same round?
            // Order of a round
            PreRound(rr);
            StartRound(rr);
            StartRoundBuffs(rr);
            Item.PreAttack(rr);
            WeaponPreAttack(rr);
            var spell = ExecuteAction(rr);
            ExecuteBuff(rr, spell);
            Item.AfterAttack(rr, spell);
            WeaponAfterAttack(rr, spell);
            PassiveBonusSpells(rr, spell);
            EndRound(rr, spell);
            //EndRoundBuffs(rr);
            PostRound(rr, spell);

            return rr;
        }

        private bool ContinueRound(RoundResult rr, ISpell prevSpell)
        {
            // TODO: Check if all this should be done in continue round
            // TODO: Fix timestamtp and cooldown checks in all methods below etc
            // Order of a round

            var actions = rr.Attacks.Count;

            Item.PreAttack(rr);
            WeaponPreAttack(rr);
            var spell = ExecuteAction(rr, prevSpell);
            ExecuteBuff(rr, spell);
            Item.AfterAttack(rr, spell);
            WeaponAfterAttack(rr, spell);
            PassiveBonusSpells(rr, spell);
            EndRound(rr, spell);

            // Did any change occur?
            return actions != rr.Attacks.Count;
        }

        private void PreRound(RoundResult rr)
        {
            rr.PrimaryEnergyStart = PrimaryWeapon.Energy;
            rr.SecondaryEnergyStart = SecondaryWeapon.Energy;
            rr.PrimaryGimmickStart = PrimaryWeapon.GimmickResource;
            rr.SecondaryGimmickStart = SecondaryWeapon.GimmickResource;
        }

        private void StartRound(RoundResult rr)
        {
            // Not on first round
            if(rr.TimeSec == 0) return;

            // +1 resource per sec primary weapon
            if (rr.TimeSec != 0 && rr.TimeSec % 1 == 0)
            {
                PrimaryWeapon.Energy++;
            }

            // +1 resource every other second secondary weapon
            if (rr.TimeSec != 0 && rr.TimeSec % 2 == 0)
            {
                SecondaryWeapon.Energy++;
            }

            // Lower cooldown of all spells except the one used this round
            foreach (var spell in Spells.Where(s => s.Cooldown > 0))
            {
                spell.Cooldown -= rr.Interval;
            }

            // Lower cooldown of all item spells except the one used this round
            foreach (var spell in Item.Spells.Where(s => s.Cooldown > 0))
            {
                spell.Cooldown -= rr.Interval;
            }

            // Lower GCD
            if (GCD > 0) GCD -= rr.Interval;

            // Lower CastTime
            if (CastTime > 0) CastTime -= rr.Interval;

            // Lower Buff cooldowns
            foreach (var buff in Buffs)
            {
                if (buff.Duration >= 0) buff.Duration -= rr.Interval;
                if (buff.Cooldown > 0) buff.Cooldown -= rr.Interval;

                if (buff is AbilityBuff ab)
                {
                    var weapon = GetWeaponFromType(ab.WeaponType);
                    if (weapon == null) continue;

                    if (ab.Active && ab.Duration % 1 == 0)
                    {
                        weapon.GimmickResource += ab.GimmickGainPerSec;
                        weapon.Energy += ab.EnergyGainPerSec;
                    }
                }
            }
        }

        private void StartRoundBuffs(RoundResult rr)
        {
            var availableBuffs = Buffs.Where(b => b.CanActivate());

            foreach (var buff in availableBuffs)
            {
                buff.Activate(rr.TimeSec);
            }
        }

        private void WeaponPreAttack(RoundResult rr)
        {
            PrimaryWeapon.PreAttack(this, rr);
            SecondaryWeapon.PreAttack(this, rr);
        }

        private ISpell ExecuteAction(RoundResult rr, ISpell prevSpell = null)
        {
            Attack attack;
            var spell = prevSpell;
            
            if (CurrentSpell == null)
            {
                spell = GetSpellFromApl();
                attack = spell?.Execute(this);

                // If attack is null we have started a cast
                if (attack != null)
                {
                    rr.Attacks.Add(attack);
                    return spell;
                }
            }
            else
            {
                // We have already continued on this cast this turn
                if (spell != null)
                {
                    return null;
                }

                // We are already casting
                attack = CurrentSpell.Continue(this);

                // If attack is null cast is not complete
                if (attack != null)
                {
                    rr.Attacks.Add(attack);
                    return attack.Spell;
                }
            }

            return null;
        }

        private ISpell GetSpellFromApl()
        {
            // Get first spell from top of priority list we can execute
            var spell = Spells.FirstOrDefault(s => s.CanExecute(this));

            // If spell is null we cant cast anything
            if (spell == null) return null;

            // Specific Hammer stuff, if enraged get rage spell
            if (Buff.Enraged)
            {
                var rageSpell = Spells.Find(s => s.Name == spell.Name + "Rage");
                if (rageSpell != null)
                {
                    return rageSpell;
                }
            }

            return spell;
        }

        private void ExecuteBuff(RoundResult rr, ISpell spell)
        {
            if (spell?.AbilityBuff == null) return;

            var buff = Buffs.Find(b => b == spell.AbilityBuff);
            buff.Activate(rr.TimeSec);
        }

        private void WeaponAfterAttack(RoundResult rr, ISpell spell)
        {
            var attack = rr.Attacks.FirstOrDefault(a => a.Spell == spell);
            if (attack == null || !attack.IsHit) return;

            var weapon = GetWeaponFromSpell(attack.Spell);
            weapon?.AfterAttack(this, attack.Spell, rr);
            weapon?.WeaponAffixes(this, attack.Spell, rr);
        }

        private void PassiveBonusSpells(RoundResult rr, ISpell spell)
        {
            var attack = rr.Attacks.FirstOrDefault(a => a.Spell == spell);
            if (attack == null || !attack.IsHit) return;

            if (spell.PassiveBonusSpell != null)
            {
                if (spell.PassiveBonusSpell.BonusSpellOnlyOnCrit)
                {
                    // Passive bonus spell only on crit
                    if (attack.IsCrit)
                    {
                        AddBonusAttack(rr, spell.PassiveBonusSpell);
                    }
                }
                else
                {
                    // Passive bonus spell on hit
                    AddBonusAttack(rr, spell.PassiveBonusSpell);
                }
            }
        }

        private void EndRound(RoundResult rr, ISpell spell)
        {
            var attack = rr.Attacks.FirstOrDefault(a => a.Spell == spell);
            if (attack != null && attack.IsHit)
            {
                var weapon = GetWeaponFromSpell(attack.Spell);

                if (attack.IsCrit)
                {
                    // TODO: Make sure this can never happen more than 1/s
                    // Energy on crit 1s IDC
                    weapon?.EnergyOnCrit(this);
                }
            }
        }

        private void EndRoundBuffs(RoundResult rr)
        {
            //var attack = rr.Attacks.FirstOrDefault();
            //var abilityBuff = attack?.Spell.AbilityBuff;

            //foreach (var buff in Buffs)
            //{
            //    if (buff == abilityBuff) continue;

            //    if (buff.Duration >= 0)
            //        buff.Duration -= rr.Interval;
            //    if (buff.Cooldown > 0)
            //        buff.Cooldown -= rr.Interval;

            //    if (buff is AbilityBuff ab)
            //    {
            //        var weapon = GetWeaponFromType(ab.WeaponType);
            //        if (weapon == null) continue;

            //        if (ab.Active && ab.Duration % 1 == 0)
            //        {
            //            weapon.GimmickResource += ab.GimmickGainPerSec;
            //            weapon.Energy += ab.EnergyGainPerSec;
            //        }
            //    }
            //}
        }

        private void PostRound(RoundResult rr, ISpell spell)
        {
            // Make sure we cant do anything more this round
            if (rr.Attacks.Any())
            {
                var changes = true;
                while (changes)
                {
                    changes = ContinueRound(rr, spell);
                }
            }           

            // Add relevant info to round result
            rr.PrimaryEnergyEnd = PrimaryWeapon.Energy;
            rr.SecondaryEnergyEnd = SecondaryWeapon.Energy;
            rr.PrimaryGimmickEnd = PrimaryWeapon.GimmickResource;
            rr.SecondaryGimmickEnd = SecondaryWeapon.GimmickResource;

            var activeBuffs = Buffs.Where(b => b.Active).ToList();
            rr.Buffs = activeBuffs;
        }

        private Attack ExecuteNoGcd(ISpell spell)
        {
            return spell.Execute(this);
        }

        public void AddBonusAttack(RoundResult rr, ISpell spell)
        {
            if (spell.CanExecute(this))
            {
                rr.Attacks.Add(ExecuteNoGcd(spell));
            }
        }

        public Weapon GetWeaponFromSpell(ISpell spell)
        {
            if (PrimaryWeapon.WeaponType == spell.WeaponType)
                return PrimaryWeapon;
            return SecondaryWeapon.WeaponType == spell.WeaponType ? SecondaryWeapon : null;
        }

        public Weapon GetOtherWeaponFromSpell(ISpell spell)
        {
            // Can go really wrong if APL contains 3 weapon types
            return PrimaryWeapon.WeaponType != spell.WeaponType ? PrimaryWeapon : SecondaryWeapon;
        }

        public Weapon GetWeaponFromType(WeaponType wtype)
        {
            if (PrimaryWeapon.WeaponType == wtype)
                return PrimaryWeapon;
            return SecondaryWeapon.WeaponType == wtype ? SecondaryWeapon : null;
        }

        public decimal GetWeaponResourceFromType(WeaponType wtype)
        {
            if (PrimaryWeapon.WeaponType == wtype)
                return PrimaryWeapon.GimmickResource;
            return SecondaryWeapon.WeaponType == wtype ? SecondaryWeapon.GimmickResource : 0;
        }

        public IBuff GetBuffFromName(string name)
        {
            var buffs = Buffs.Where(b => b.Name == name).ToList();
            if (buffs.Count == 1) return buffs.FirstOrDefault();

            // If there is several whith same name get abilitybuff
            var abilitybuff = buffs.Find(b => b is AbilityBuff);
            return abilitybuff;
        }

        public IBuff GetAbilityBuffFromName(string name)
        {
            var abilitybuff = AbilityBuffs.Find(b => b.Name == name && b is AbilityBuff);

            // Shameful add to Buffs list to keep other stuff working for now
            Buffs.Add(abilitybuff);
            return abilitybuff;
        }

        public bool HasPassive(string name)
        {
            return Passives.Any(p => p.Name == name);
        }

        public Passive GetPassive(string name)
        {
            return Passives.Find(p => p.Name == name);
        }
    }
}