using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace H3Calc.Engine
{
    public abstract class Spell
    {        
        public string Name { get; private set; }                
        public int Level { get; private set; }

        protected readonly Type SecondarySkillType;

        public SpellCasterStats CasterStats { get; set; }        

        protected Spell(string name, Type secondarySkillType, int level)
        {
            Name = name;
            SecondarySkillType = secondarySkillType;
            Level = level;

            CasterStats = new SpellCasterStats();
        }        

        public virtual bool IsAffectedBySecondarySkillType(Type skillType)
        {
            return (skillType == SecondarySkillType);
        }
    }

    public class SpellCasterStats
    {
        public SecondarySkillLevel SkillLevel { get; set; }

        public int SpellPower { get; set; }

        public bool IsSpecialized { get; set; }
        public int SpecializationLevel { get; set; }

        public SpellCasterStats()
        {
            SkillLevel = SecondarySkillLevel.None;

            IsSpecialized = false;
            SpecializationLevel = 0;
        }
    }
}


