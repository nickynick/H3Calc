using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace H3Calc.Engine
{
    public abstract class Spell : IUnitStatsModifier, IDamageModifierProvider
    {
        public readonly string Name;
        public readonly Type SecondarySkillType;

        public Hero Caster { get; set; }        

        protected Spell(string name, Type secondarySkillType)
        {
            Name = name;
            SecondarySkillType = secondarySkillType;
        }

        public SecondarySkillLevel SkillLevel
        {
            get
            {
                if ((Caster == null) || (Caster.Stats == null))
                {
                    return SecondarySkillLevel.None;                    
                }

                foreach (SecondarySkill skill in Caster.Stats.SecondarySkills)
                {
                    if (skill.GetType() == SecondarySkillType)
                    {
                        return skill.SkillLevel;
                    }
                }

                return SecondarySkillLevel.None;
            }
        }

        public bool IsSpecialized         
        {
            get
            {
                if (Caster == null)
                {
                    return false;
                }

                return (Caster.SpecializedSpell == GetType());
            }
        }

        public virtual void ApplyPermanently(Unit unit, UnitStats modifiedStats) { }
        public virtual void ApplyOnAttack(AttackData attackData, UnitStats modifiedStats) { }
        public virtual void ApplyOnDefense(AttackData attackData, UnitStats modifiedStats) { }

        public virtual void ApplyOnAttack(AttackData attackData, DamageModifier damageModifier) { }
        public virtual void ApplyOnDefense(AttackData attackData, DamageModifier damageModifier) { }
    }

    public class Bloodlust : Spell
    {
        public Bloodlust() : base("Bloodlust", typeof(FireMagic)) { }

        public override void ApplyPermanently(Unit unit, UnitStats modifiedStats) 
        {
            int bonus = (SkillLevel <= SecondarySkillLevel.Basic) ? 3 : 6;
            
            if (IsSpecialized)
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

    public class Curse : Spell
    {
        public Curse() : base("Curse", typeof(FireMagic)) { }

        public override void ApplyPermanently(Unit unit, UnitStats modifiedStats)
        {
            if (SkillLevel <= SecondarySkillLevel.Basic)
            {
                modifiedStats.MinDamage = modifiedStats.MaxDamage = unit.InitialStats.MinDamage;
            }
            else
            {
                modifiedStats.MinDamage = modifiedStats.MaxDamage = unit.InitialStats.MinDamage - 1;         
            }
        }
    }

    public class Frenzy : Spell
    {
        public Frenzy() : base("Frenzy", typeof(FireMagic)) { }

        public override void ApplyPermanently(Unit unit, UnitStats modifiedStats)
        {
            if (SkillLevel <= SecondarySkillLevel.Basic)
            {
                modifiedStats.Attack += modifiedStats.Defense;
            }
            else if (SkillLevel <= SecondarySkillLevel.Advanced) 
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

    public class Slayer : Spell
    {
        public Slayer() : base("Slayer", typeof(FireMagic)) { }

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

            if (SkillLevel >= SecondarySkillLevel.Advanced)
            {
                affectedUnitIds.Add(12); // Angel
                affectedUnitIds.Add(13); // Archangel
                affectedUnitIds.Add(82); // Devil
                affectedUnitIds.Add(83); // Archdevil
            }

            if (SkillLevel == SecondarySkillLevel.Expert)
            {
                affectedUnitIds.Add(40); // Giant
                affectedUnitIds.Add(41); // Titan
            }

            if (affectedUnitIds.Contains(attackData.Defender.Id))
            {
                int bonus = 8;

                if (IsSpecialized)
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

    public class Bless : Spell
    {
        public Bless() : base("Bless", typeof(WaterMagic)) { }

        public override void ApplyPermanently(Unit unit, UnitStats modifiedStats)
        {
            if (SkillLevel <= SecondarySkillLevel.Basic)
            {
                modifiedStats.MinDamage = modifiedStats.MaxDamage = unit.InitialStats.MaxDamage;
            }
            else
            {
                modifiedStats.MinDamage = modifiedStats.MaxDamage = unit.InitialStats.MaxDamage + 1;
            }

            if (IsSpecialized)
            {
                // TODO
            }
        }
    }

    public class Weakness : Spell
    {
        public Weakness() : base("Weakness", typeof(WaterMagic)) { }

        public override void ApplyPermanently(Unit unit, UnitStats modifiedStats)
        {
            int reduction = (SkillLevel <= SecondarySkillLevel.Basic) ? 3 : 6;
            
            if (IsSpecialized)
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

    public class Prayer : Spell
    {
        public Prayer() : base("Prayer", typeof(WaterMagic)) { }

        public override void ApplyPermanently(Unit unit, UnitStats modifiedStats)
        {
            int bonus = (SkillLevel <= SecondarySkillLevel.Basic) ? 2 : 4;

            if (IsSpecialized)
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

    public class Shield : Spell
    {
        public Shield() : base("Shield", typeof(EarthMagic)) { }

        public override void ApplyOnDefense(AttackData attackData, DamageModifier damageModifier)
        {
            if (attackData.Attacker.IsRanged)
            {
                return;
            }

            if (SkillLevel <= SecondarySkillLevel.Basic)
            {
                damageModifier.DamageReductions.Add(0.15);
            }
            else
            {
                damageModifier.DamageReductions.Add(0.3);
            }
        }
    }

    public class StoneSkin : Spell
    {
        public StoneSkin() : base("Stone Skin", typeof(EarthMagic)) { }

        public override void ApplyPermanently(Unit unit, UnitStats modifiedStats)
        {
            int bonus = (SkillLevel <= SecondarySkillLevel.Basic) ? 3 : 6;

            if (IsSpecialized)
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

    public class DisruptingRay : Spell
    {
        public DisruptingRay() : base("Disrupting Ray", typeof(AirMagic)) { }

        public override void ApplyPermanently(Unit unit, UnitStats modifiedStats)
        {
            int reduction;

            if (SkillLevel <= SecondarySkillLevel.Basic)
            {
                reduction = 3;
            }
            else if (SkillLevel <= SecondarySkillLevel.Advanced)
            {
                reduction = 4;
            }
            else
            {
                reduction = 5;
            }

            if (IsSpecialized)
            {
                reduction += 2;
            }

            modifiedStats.Defense -= reduction;
            modifiedStats.Defense = Math.Max(modifiedStats.Defense, 0);
        }
    }

    public class Precision : Spell
    {
        public Precision() : base("Precision", typeof(AirMagic)) { }

        public override void ApplyPermanently(Unit unit, UnitStats modifiedStats)
        {
            if (!unit.IsRanged)
            {
                return;
            }

            int bonus = (SkillLevel <= SecondarySkillLevel.Basic) ? 3 : 6;

            if (IsSpecialized)
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

    public class AirShield : Spell
    {
        public AirShield() : base("Air Shield", typeof(AirMagic)) { }

        public override void ApplyOnDefense(AttackData attackData, DamageModifier damageModifier)
        {
            if (!attackData.Attacker.IsRanged)
            {
                return;
            }

            if (SkillLevel <= SecondarySkillLevel.Basic)
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


