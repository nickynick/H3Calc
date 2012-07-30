using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;

namespace H3Calc.Engine
{
    public class Hero
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [DefaultValue(null)]
        [XmlElement("SpecializedSecondarySkill")]
        public string SpecializedSecondarySkillString;

        [XmlIgnore]
        public Type SpecializedSecondarySkill
        {
            get
            {
                if (SpecializedSecondarySkillString == null)
                {
                    return null;
                }
                else
                {
                    string fullName = typeof(SecondarySkillLevel).Namespace + "." + SpecializedSecondarySkillString;
                    return Type.GetType(fullName);
                }
            }
            set
            {
                SpecializedSecondarySkillString = (value != null) ? value.Name : null;
            }
        }

        [DefaultValue(null)]
        [XmlElement("SpecializedSpell")]
        public string SpecializedSpellString;

        [XmlIgnore]
        public Type SpecializedSpell
        {
            get
            {
                if (SpecializedSpellString == null)
                {
                    return null;
                }
                else
                {
                    string fullName = typeof(Spell).Namespace + "." + SpecializedSpellString;
                    return Type.GetType(fullName);
                }
            }
            set
            {
                SpecializedSpellString = (value != null) ? value.Name : null;
            }
        }

        [DefaultValue(-1)]
        public int SpecializedUnitId { get; set; }

        public HeroStats Stats { get; set; }

        public Hero()
        {
            SpecializedSecondarySkill = null;
            SpecializedSpell = null;
            SpecializedUnitId = -1;
        }
    }

    [XmlRoot("Heroes")]
    public class HeroesList : List<Hero>
    {
    }

    public class HeroStats : IUnitStatsModifier, IDamageModifierProvider
    {
        public int Level { get; set; }

        public int Attack { get; set; }
        public int Defense { get; set; }

        public List<SecondarySkill> SecondarySkills { get; set; }

        public HeroStats()
        {
            Level = 1;
            SecondarySkills = new List<SecondarySkill>();
        }

        public void ApplyPermanently(Unit unit, UnitStats modifiedStats)
        {
            modifiedStats.Attack += Attack;
            modifiedStats.Defense += Defense;
        }

        public void ApplyOnAttack(AttackData attackData, UnitStats modifiedStats) { }
        public void ApplyOnDefense(AttackData attackData, UnitStats modifiedStats) { }

        public void ApplyOnAttack(AttackData attackData, DamageModifier damageModifier)
        {
            foreach (SecondarySkill skill in SecondarySkills)
            {
                skill.ApplyOnAttack(attackData, damageModifier);
            }
        }

        public void ApplyOnDefense(AttackData attackData, DamageModifier damageModifier)
        {
            foreach (SecondarySkill skill in SecondarySkills)
            {
                skill.ApplyOnDefense(attackData, damageModifier);
            }
        }
    }
}
