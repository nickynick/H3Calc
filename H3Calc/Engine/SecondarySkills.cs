using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace H3Calc.Engine
{
    public abstract class SecondarySkill : IDamageModifierProvider
    {
        public SecondarySkillLevel SkillLevel { get; set; }
        public HeroStats HeroStats { get; set; }

        public bool IsSpecialized
        {
            get
            {
                if (HeroStats == null)
                {
                    return false;
                }

                return (HeroStats.Hero.SpecializedSecondarySkill == GetType());
            }
        }

        public virtual void ApplyOnAttack(AttackData attackData, DamageModifier damageModifier) { }
        public virtual void ApplyOnDefense(AttackData attackData, DamageModifier damageModifier) { }
    }

    public class Offense : SecondarySkill
    {
        public override void ApplyOnAttack(AttackData attackData, DamageModifier damageModifier)
        {
            if (attackData.Attacker.IsRanged || (SkillLevel == SecondarySkillLevel.None))
            {
                return;
            }

            double bonus = 0;

            if (SkillLevel == SecondarySkillLevel.Basic)
            {
                bonus = 0.1;
            }
            else if (SkillLevel == SecondarySkillLevel.Advanced)
            {
                bonus = 0.2;
            }
            else
            {
                bonus = 0.3;
            }

            if (IsSpecialized)
            {
                bonus *= (1.0 + 0.05 * HeroStats.Level);
            }

            damageModifier.DamageBonuses.Add(bonus);
        }
    }

    public class Archery : SecondarySkill
    {
        public override void ApplyOnAttack(AttackData attackData, DamageModifier damageModifier)
        {
            if (!attackData.Attacker.IsRanged || (SkillLevel == SecondarySkillLevel.None))
            {
                return;
            }

            double bonus = 0;

            if (SkillLevel == SecondarySkillLevel.Basic)
            {
                bonus = 0.1;
            }
            else if (SkillLevel == SecondarySkillLevel.Advanced)
            {
                bonus = 0.25;
            }
            else
            {
                bonus = 0.5;
            }

            if (IsSpecialized)
            {
                bonus *= (1.0 + 0.05 * HeroStats.Level);
            }

            damageModifier.DamageBonuses.Add(bonus);
        }
    }

    public class Armorer : SecondarySkill
    {
        public override void ApplyOnDefense(AttackData attackData, DamageModifier damageModifier)
        {
            if (SkillLevel == SecondarySkillLevel.None)
            {
                return;
            }

            double reduction = 0;

            if (SkillLevel == SecondarySkillLevel.Basic)
            {
                reduction = 0.05;
            }
            else if (SkillLevel == SecondarySkillLevel.Advanced)
            {
                reduction = 0.1;
            }
            else
            {
                reduction = 0.15;
            }

            if (IsSpecialized)
            {
                reduction *= (1.0 + 0.05 * HeroStats.Level);
            }

            damageModifier.DamageReductions.Add(reduction);
        }
    }

    public class FireMagic : SecondarySkill
    {
    }

    public class WaterMagic : SecondarySkill
    {
    }

    public class EarthMagic : SecondarySkill
    {
    }

    public class AirMagic : SecondarySkill
    {
    }
}
