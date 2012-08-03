﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;

namespace H3Calc.Engine
{
    public class Hero : IUnitStatsModifier, IDamageModifierProvider
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

        public void ApplyPermanently(Unit unit, UnitStats modifiedStats)
        {
            modifiedStats.Attack += Stats.Attack;
            modifiedStats.Defense += Stats.Defense;

            if (SpecializedUnitId >= 0)
            {
                ApplyUnitSpecialization(unit, modifiedStats);
            }
        }

        private void ApplyUnitSpecialization(Unit unit, UnitStats modifiedStats)
        {
            // TODO: this is dirty, should handle upgraded units in a better way.
            if ((unit.Id != SpecializedUnitId) && (unit.Id != SpecializedUnitId + 1))
            {
                return;
            }

            // Special formulae first.

            // Water/Ice Elementals
            if ((unit.Id == 116) || (unit.Id == 117))
            {
                modifiedStats.Attack += 2;
                return;
            }

            // Fire/Energy Elementals
            if ((unit.Id == 116) || (unit.Id == 117))
            {
                modifiedStats.Attack += 1;
                modifiedStats.Defense += 2;
                modifiedStats.MinDamage += 2;
                modifiedStats.MaxDamage += 2;
                return;
            }

            // Earth/Magma Elementals
            if ((unit.Id == 120) || (unit.Id == 121))
            {
                modifiedStats.Attack += 2;
                modifiedStats.Defense += 1;
                modifiedStats.MinDamage += 5;
                modifiedStats.MaxDamage += 5;
                return;
            }

            // Psychic/Magic Elementals
            if ((unit.Id == 122) || (unit.Id == 123))
            {
                modifiedStats.Attack += 3;
                modifiedStats.Defense += 3;                
                return;
            }

            // Default formula
            int levelCoefficient = Stats.Level / unit.Level;

            double attackBonus = unit.InitialStats.Attack * (0.05 * levelCoefficient);
            double defenseBonus = unit.InitialStats.Defense * (0.05 * levelCoefficient);

            modifiedStats.Attack += (int)Math.Ceiling(attackBonus);
            modifiedStats.Defense += (int)Math.Ceiling(defenseBonus);
        }

        public void ApplyOnAttack(AttackData attackData, UnitStats modifiedStats) { }
        public void ApplyOnDefense(AttackData attackData, UnitStats modifiedStats) { }

        public void ApplyOnAttack(AttackData attackData, DamageModifier damageModifier)
        {
            foreach (SecondarySkill skill in Stats.SecondarySkills)
            {
                skill.ApplyOnAttack(attackData, damageModifier);
            }
        }

        public void ApplyOnDefense(AttackData attackData, DamageModifier damageModifier)
        {
            foreach (SecondarySkill skill in Stats.SecondarySkills)
            {
                skill.ApplyOnDefense(attackData, damageModifier);
            }
        }
    }

    [XmlRoot("Heroes")]
    public class HeroesList : List<Hero>
    {
    }

    public class HeroStats
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
    }
}