using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace H3Calc.Engine
{
    public abstract class Spell
    {
        public readonly string Name;
        protected readonly Type SecondarySkillType;
        public readonly int Level;

        public HeroStats CasterStats { get; set; }        

        protected Spell(string name, Type secondarySkillType, int level)
        {
            Name = name;
            SecondarySkillType = secondarySkillType;
            Level = level;
        }

        public SecondarySkillLevel SkillLevel
        {
            get
            {
                if (CasterStats == null)
                {
                    return SecondarySkillLevel.None;                    
                }

                SecondarySkillLevel level = SecondarySkillLevel.None;

                foreach (SecondarySkill skill in CasterStats.SecondarySkills)
                {
                    if (IsAffectedBySecondarySkillType(skill.GetType()))
                    {
                        if (level < skill.SkillLevel)
                        {
                            level = skill.SkillLevel;
                        }                        
                    }
                }

                return level;
            }
        }

        public bool IsSpecialized         
        {
            get
            {
                if (CasterStats == null)
                {
                    return false;
                }

                return (CasterStats.Hero.SpecializedSpell == GetType());
            }
        }

        public virtual bool IsAffectedBySecondarySkillType(Type skillType)
        {
            return (skillType == SecondarySkillType);
        }
    }
}


