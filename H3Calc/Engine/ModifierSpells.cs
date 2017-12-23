using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace H3Calc.Engine
{
    public abstract class ModifierSpell : Spell, ICombatUnitStatsModifier, ICombatDamageModifierProvider
    {
        protected ModifierSpell(String name, Type secondarySkillType, int level) : base(name, secondarySkillType, level) { }

        public virtual void ApplyPermanently(Unit unit, UnitStats modifiedStats) { }
        public virtual void ApplyOnAttack(AttackData attackData, UnitStats modifiedStats) { }
        public virtual void ApplyOnDefense(AttackData attackData, UnitStats modifiedStats) { }

        public virtual void ApplyOnAttack(AttackData attackData, CombatDamageModifier damageModifier) { }
        public virtual void ApplyOnDefense(AttackData attackData, CombatDamageModifier damageModifier) { }
    }

    public class Bloodlust : ModifierSpell
    {
        public Bloodlust() : base("Bloodlust", typeof(FireMagic), 1) { }

        public override void ApplyPermanently(Unit unit, UnitStats modifiedStats)
        {
            if (unit.IsRanged)
            {
                return;
            }

            int bonus = (CasterStats.SkillLevel <= SecondarySkillLevel.Basic) ? 3 : 6;

            if (CasterStats.IsSpecialized)
            {
                switch (unit.Level)
                {
                    case 1:
                    case 2:
                        bonus += 3;
                        break;
                    case 3:
                    case 4:
                        bonus += 2;
                        break;
                    case 5:
                    case 6:
                        bonus += 1;
                        break;
                }
            }

            modifiedStats.Attack += bonus;
        }
    }

    public class Curse : ModifierSpell
    {
        public Curse() : base("Curse", typeof(FireMagic), 1) { }

        public override void ApplyPermanently(Unit unit, UnitStats modifiedStats)
        {
            if (CasterStats.SkillLevel <= SecondarySkillLevel.Basic)
            {
                modifiedStats.MinDamage = modifiedStats.MaxDamage = modifiedStats.MinDamage;
            }
            else
            {
                modifiedStats.MinDamage = modifiedStats.MaxDamage = (modifiedStats.MinDamage - 1 < 1) ? 1 : modifiedStats.MinDamage - 1; //min damage can't be less than 1
            }
        }
    }

    public class Frenzy : ModifierSpell
    {
        public Frenzy() : base("Frenzy", typeof(FireMagic), 4) { }

        public override void ApplyPermanently(Unit unit, UnitStats modifiedStats)
        {
            if (CasterStats.SkillLevel <= SecondarySkillLevel.Basic)
            {
                modifiedStats.Attack += modifiedStats.Defense;
            }
            else if (CasterStats.SkillLevel <= SecondarySkillLevel.Advanced)
            {
                modifiedStats.Attack += modifiedStats.Defense * 3 / 2;
            }
            else
            {
                modifiedStats.Attack += modifiedStats.Defense * 2;
            }

            modifiedStats.Defense = 0;
        }
    }

    public class Slayer : ModifierSpell
    {
        public Slayer() : base("Slayer", typeof(FireMagic), 4) { }

        public override void ApplyOnAttack(AttackData attackData, UnitStats modifiedStats)
        {
            List<int> affectedUnitIds = new List<int>();

            affectedUnitIds.Add(26); // Green Dragon
            affectedUnitIds.Add(27); // Gold Dragon
            affectedUnitIds.Add(54); // Bone Dragon
            affectedUnitIds.Add(55); // Ghost Dragon
            affectedUnitIds.Add(68); // Red Dragon
            affectedUnitIds.Add(69); // Black Dragon
            affectedUnitIds.Add(96); // Hydra
            affectedUnitIds.Add(97); // Chaos Hydra
            affectedUnitIds.Add(110); // Behemoth
            affectedUnitIds.Add(111); // Ancient Behemoth
            affectedUnitIds.Add(124); // Firebird
            affectedUnitIds.Add(125); // Phoenix
            affectedUnitIds.Add(137); // Fairy Dragon
            affectedUnitIds.Add(138); // Rust Dragon
            affectedUnitIds.Add(139); // Crystal Dragon
            affectedUnitIds.Add(140); // Azure Dragon

            if (CasterStats.SkillLevel >= SecondarySkillLevel.Advanced)
            {
                affectedUnitIds.Add(12); // Angel
                affectedUnitIds.Add(13); // Archangel
                affectedUnitIds.Add(82); // Devil
                affectedUnitIds.Add(83); // Archdevil
            }

            if (CasterStats.SkillLevel == SecondarySkillLevel.Expert)
            {
                affectedUnitIds.Add(40); // Giant
                affectedUnitIds.Add(41); // Titan
            }

            if (affectedUnitIds.Contains(attackData.Defender.Id))
            {
                int bonus = 8;

                if (CasterStats.IsSpecialized)
                {
                    switch (attackData.Attacker.Level)
                    {
                        case 1:
                            bonus += 4;
                            break;
                        case 2:
                            bonus += 3;
                            break;
                        case 3:
                            bonus += 2;
                            break;
                        case 4:
                            bonus += 1;
                            break;
                    }
                }

                modifiedStats.Attack += bonus;
            }
        }
    }

    public class Bless : ModifierSpell
    {
        public Bless() : base("Bless", typeof(WaterMagic), 1) { }

        public override void ApplyPermanently(Unit unit, UnitStats modifiedStats)
        {
            if (CasterStats.SkillLevel <= SecondarySkillLevel.Basic)
            {
                modifiedStats.MinDamage = modifiedStats.MaxDamage = modifiedStats.MaxDamage;
            }
            else
            {
                modifiedStats.MinDamage = modifiedStats.MaxDamage = modifiedStats.MaxDamage + 1;
            }
        }

        public override void ApplyOnAttack(AttackData attackData, CombatDamageModifier damageModifier)
        {
            if (CasterStats.IsSpecialized)
            {
                double bonus = 0.03 * (CasterStats.SpecializationLevel / attackData.Attacker.Level);
                damageModifier.DamageBonuses.Add(bonus);
            }
        }
    }

    public class Weakness : ModifierSpell
    {
        public Weakness() : base("Weakness", typeof(WaterMagic), 2) { }

        public override void ApplyPermanently(Unit unit, UnitStats modifiedStats)
        {
            int reduction = (CasterStats.SkillLevel <= SecondarySkillLevel.Basic) ? 3 : 6;

            if (CasterStats.IsSpecialized)
            {
                switch (unit.Level)
                {
                    case 1:
                    case 2:
                        reduction += 3;
                        break;
                    case 3:
                    case 4:
                        reduction += 2;
                        break;
                    case 5:
                    case 6:
                        reduction += 1;
                        break;
                }
            }

            modifiedStats.Attack -= reduction;
        }
    }

    public class Prayer : ModifierSpell
    {
        public Prayer() : base("Prayer", typeof(WaterMagic), 4) { }

        public override void ApplyPermanently(Unit unit, UnitStats modifiedStats)
        {
            int bonus = (CasterStats.SkillLevel <= SecondarySkillLevel.Basic) ? 2 : 4;

            if (CasterStats.IsSpecialized)
            {
                switch (unit.Level)
                {
                    case 1:
                    case 2:
                        bonus += 3;
                        break;
                    case 3:
                    case 4:
                        bonus += 2;
                        break;
                    case 5:
                    case 6:
                        bonus += 1;
                        break;
                }
            }

            modifiedStats.Attack += bonus;
            modifiedStats.Defense += bonus;
        }
    }

    public class Shield : ModifierSpell
    {
        public Shield() : base("Shield", typeof(EarthMagic), 1) { }

        public override void ApplyOnDefense(AttackData attackData, CombatDamageModifier damageModifier)
        {
            if (attackData.Attacker.IsRanged)
            {
                return;
            }

            if (CasterStats.SkillLevel <= SecondarySkillLevel.Basic)
            {
                damageModifier.DamageReductions.Add(0.15);
            }
            else
            {
                damageModifier.DamageReductions.Add(0.3);
            }
        }
    }

    public class StoneSkin : ModifierSpell
    {
        public StoneSkin() : base("Stone Skin", typeof(EarthMagic), 1) { }

        public override void ApplyPermanently(Unit unit, UnitStats modifiedStats)
        {
            int bonus = (CasterStats.SkillLevel <= SecondarySkillLevel.Basic) ? 3 : 6;

            if (CasterStats.IsSpecialized)
            {
                switch (unit.Level)
                {
                    case 1:
                    case 2:
                        bonus += 3;
                        break;
                    case 3:
                    case 4:
                        bonus += 2;
                        break;
                    case 5:
                    case 6:
                        bonus += 1;
                        break;
                }
            }

            modifiedStats.Defense += bonus;
        }
    }

    public class DisruptingRay : ModifierSpell
    {
        public DisruptingRay() : base("Disrupting Ray", typeof(AirMagic), 2) { }

        public override void ApplyPermanently(Unit unit, UnitStats modifiedStats)
        {
            int reduction;

            if (CasterStats.SkillLevel <= SecondarySkillLevel.Basic)
            {
                reduction = 3;
            }
            else if (CasterStats.SkillLevel <= SecondarySkillLevel.Advanced)
            {
                reduction = 4;
            }
            else
            {
                reduction = 5;
            }

            if (CasterStats.IsSpecialized)
            {
                reduction += 2;
            }

            modifiedStats.Defense -= reduction;
            modifiedStats.Defense = Math.Max(modifiedStats.Defense, 0);
        }
    }

    public class Precision : ModifierSpell
    {
        public Precision() : base("Precision", typeof(AirMagic), 2) { }

        public override void ApplyPermanently(Unit unit, UnitStats modifiedStats)
        {
            if (!unit.IsRanged)
            {
                return;
            }

            int bonus = (CasterStats.SkillLevel <= SecondarySkillLevel.Basic) ? 3 : 6;

            if (CasterStats.IsSpecialized)
            {
                switch (unit.Level)
                {
                    case 1:
                    case 2:
                        bonus += 3;
                        break;
                    case 3:
                    case 4:
                        bonus += 2;
                        break;
                    case 5:
                    case 6:
                        bonus += 1;
                        break;
                }
            }

            modifiedStats.Attack += bonus;
        }
    }

    public class AirShield : ModifierSpell
    {
        public AirShield() : base("Air Shield", typeof(AirMagic), 3) { }

        public override void ApplyOnDefense(AttackData attackData, CombatDamageModifier damageModifier)
        {
            if (!attackData.Attacker.IsRanged)
            {
                return;
            }

            if (CasterStats.SkillLevel <= SecondarySkillLevel.Basic)
            {
                damageModifier.DamageReductions.Add(0.25);
            }
            else
            {
                damageModifier.DamageReductions.Add(0.5);
            }
        }
    }
}
