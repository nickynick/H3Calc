using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace H3Calc.Engine
{
    public class Hero
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [DefaultValue(null)]        
        [JsonProperty("SpecializedSecondarySkill")]
        public string SpecializedSecondarySkillString;
        
        [JsonIgnore]
        public Type SpecializedSecondarySkill
        {
            get
            {
                return Utils.TypeFromString(SpecializedSecondarySkillString);                
            }
            set
            {
                SpecializedSecondarySkillString = Utils.StringFromType(value);
            }
        }

        [DefaultValue(null)]        
        [JsonProperty("SpecializedSpell")]
        public string SpecializedSpellString;
        
        [JsonIgnore]
        public Type SpecializedSpell
        {
            get
            {
                return Utils.TypeFromString(SpecializedSpellString);
            }
            set
            {
                SpecializedSpellString = Utils.StringFromType(value);
            }
        }

        [DefaultValue(-1)]
        public int SpecializedUnitId { get; set; }        

        public Hero()
        {
            SpecializedSecondarySkill = null;
            SpecializedSpell = null;
            SpecializedUnitId = -1;
        }
    }    

    public class HeroStats : ICombatUnitStatsModifier, ICombatDamageModifierProvider, ISpellDamageModifierProvider
    {
        public Hero Hero { get; set; }

        public int Level { get; set; }

        public int Attack { get; set; }
        public int Defense { get; set; }
        public int SpellPower { get; set; }

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

            if (Hero != null && Hero.SpecializedUnitId >= 0)
            {
                ApplyUnitSpecialization(unit, modifiedStats);
            }
        }

        private void ApplyUnitSpecialization(Unit unit, UnitStats modifiedStats)
        {
            // TODO: this is dirty, should handle upgraded units in a better way.
            if ((unit.Id != Hero.SpecializedUnitId) && (unit.Id != Hero.SpecializedUnitId + 1))
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
            int levelCoefficient = Level / unit.Level;

            double attackBonus = unit.InitialStats.Attack * (0.05 * levelCoefficient);
            double defenseBonus = unit.InitialStats.Defense * (0.05 * levelCoefficient);

            modifiedStats.Attack += (int)Math.Ceiling(attackBonus);
            modifiedStats.Defense += (int)Math.Ceiling(defenseBonus);
        }

        public void ApplyOnAttack(AttackData attackData, UnitStats modifiedStats) { }
        public void ApplyOnDefense(AttackData attackData, UnitStats modifiedStats) { }

        public void ApplyOnAttack(AttackData attackData, CombatDamageModifier damageModifier)
        {
            foreach (SecondarySkill skill in SecondarySkills)
            {
                skill.ApplyOnAttack(attackData, damageModifier);
            }
        }

        public void ApplyOnDefense(AttackData attackData, CombatDamageModifier damageModifier)
        {
            foreach (SecondarySkill skill in SecondarySkills)
            {
                skill.ApplyOnDefense(attackData, damageModifier);
            }
        }

        public void ApplySpell(SpellDamageCalculatorData data, SpellDamageModifier damageModifier)
        {
            foreach (SecondarySkill skill in SecondarySkills)
            {
                skill.ApplySpell(data, damageModifier);
            }
        }
    }
}
