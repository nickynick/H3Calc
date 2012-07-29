using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.ComponentModel;

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
                SpecializedSecondarySkillString = value.Name;
            }
        }

        [DefaultValue(-1)]
        public int SpecializedSpellId { get; set; }

        [DefaultValue(-1)]
        public int SpecializedUnitId { get; set; }

        public HeroStats Stats { get; set; }

        public Hero()
        {
            SpecializedSecondarySkillString = null;
            SpecializedSpellId = -1;
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

    public class Terrain : IUnitStatsModifier
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public void ApplyPermanently(Unit unit, UnitStats modifiedStats)
        {
            if (unit.NativeTerrainId == Id)
            {
                modifiedStats.Attack += 1;
                modifiedStats.Defense += 1;
            }
        }

        public void ApplyOnAttack(AttackData attackData, UnitStats modifiedStats) { }
        public void ApplyOnDefense(AttackData attackData, UnitStats modifiedStats) { }
    }

    [XmlRoot("Terrains")]
    public class TerrainsList : List<Terrain>
    {
    }

    public class SecondarySkillLevel
    {
        public static readonly SecondarySkillLevel None = new SecondarySkillLevel(0, "None");
        public static readonly SecondarySkillLevel Basic = new SecondarySkillLevel(1, "Basic");
        public static readonly SecondarySkillLevel Advanced = new SecondarySkillLevel(2, "Advanced");
        public static readonly SecondarySkillLevel Expert = new SecondarySkillLevel(3, "Expert");

        public readonly int Value;
        public readonly string Name;

        private SecondarySkillLevel() { }
        private SecondarySkillLevel(int value, string name) 
        {
            Value = value;
            Name = name;
        }

        public override bool Equals(object obj)
        {
            SecondarySkillLevel otherObj = (SecondarySkillLevel)obj;
            return (Value == otherObj.Value);
        }

        public override int GetHashCode()
        {
            return 31 * Value;
        }

        public static bool operator <(SecondarySkillLevel left, SecondarySkillLevel right)
        {
            return (left.Value < right.Value);
        }

        public static bool operator <=(SecondarySkillLevel left, SecondarySkillLevel right)
        {
            return (left.Value <= right.Value);
        }

        public static bool operator >(SecondarySkillLevel left, SecondarySkillLevel right)
        {
            return (left.Value > right.Value);
        }

        public static bool operator >=(SecondarySkillLevel left, SecondarySkillLevel right)
        {
            return (left.Value >= right.Value);
        }
    }
}
