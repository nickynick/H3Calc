using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.ComponentModel;

namespace H3Calc
{
    public class Unit
    {        
        public int Id { get; set; }
        public string Name { get; set; }

        public int Attack { get; set; }
        public int Defense { get; set; }

        public int MinDamage { get; set; }
        public int MaxDamage { get; set; }

        public int Health { get; set; }

        public int NativeTerrainId { get; set; }

        [DefaultValue(false)]
        public bool IsRanged { get; set; }

        [DefaultValue(1)]
        public int NumberOfHits { get; set; }

        public Unit()
        {
            IsRanged = false;
            NumberOfHits = 1;
        }
    }

    [XmlRoot("Units")]
    public class UnitsList : List<Unit>
    {
    }

    public class Terrain
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    [XmlRoot("Terrains")]
    public class TerrainsList : List<Terrain>
    {
    }

    public class Hero
    {
        public int PrimarySkill { get; set; }
        public int SecondarySkill { get; set; }

        public bool IsSecondarySkillSpecialist { get; set; }

        public int Level { get; set; }
    }

    public class SecondarySkillLevel
    {
        public int Level { get; set; }
        public string Title { get; set; }
    }

    public class DamageCalculator
    {
        // http://mightandmagic.wikia.com/wiki/Damage_(Heroes)

        public void CalculateDamage(Unit attacker, int attackerCount, Unit defender, Hero attackerHero, Hero defenderHero, Terrain terrain, out int minDamage, out int maxDamage, out string notes)
        {
            var damageBonuses = new List<double>();
            var damageReductions = new List<double>();

            int terrainId = (terrain != null) ? terrain.Id : -1;

            int totalAttack = attacker.Attack 
                + ((attackerHero != null) ? attackerHero.PrimarySkill : 0) 
                + ((attacker.NativeTerrainId == terrainId) ? 1 : 0);
            int totalDefense = defender.Defense
                + ((defenderHero != null) ? defenderHero.PrimarySkill : 0) 
                + ((defender.NativeTerrainId == terrainId) ? 1 : 0);

            if (totalAttack > totalDefense)
            {
                double primarySkillBonus = Math.Min((totalAttack - totalDefense) * 0.05, 3.0);
                damageBonuses.Add(primarySkillBonus);
            } 
            else if (totalAttack < totalDefense)
            {
                double primarySkillReduction = Math.Min((totalDefense - totalAttack) * 0.025, 0.7);
                damageReductions.Add(primarySkillReduction);
            }

            if ((attackerHero != null) && (attackerHero.SecondarySkill > 0))
            {                
                double secondarySkillBonus;

                if (attacker.IsRanged)
                {
                    // Archery
                    switch (attackerHero.SecondarySkill)
                    {
                        case 1:
                            secondarySkillBonus = 0.1;
                            break;
                        case 2:
                            secondarySkillBonus = 0.25;
                            break;
                        case 3:
                        default:
                            secondarySkillBonus = 0.5;
                            break;
                    }
                }
                else
                {                    
                    // Offense
                    secondarySkillBonus = attackerHero.SecondarySkill * 0.1;
                }

                if (attackerHero.IsSecondarySkillSpecialist)
                {
                    secondarySkillBonus *= (1.0 + 0.05 * attackerHero.Level);
                }

                damageBonuses.Add(secondarySkillBonus);
            }

            if ((defenderHero != null) && (defenderHero.SecondarySkill > 0))
            {
                // Armorer
                double secondarySkillReduction = defenderHero.SecondarySkill * 0.05;

                if (defenderHero.IsSecondarySkillSpecialist)
                {
                    secondarySkillReduction *= (1.0 + 0.05 * defenderHero.Level);
                }

                damageReductions.Add(secondarySkillReduction);
            }
            
            // TODO: special units (behemoths, double hits, etc.)

            int minBaseDamage = attacker.MinDamage * attackerCount;
            int maxBaseDamage = attacker.MaxDamage * attackerCount;

            minDamage = PerformCalculation(minBaseDamage, damageBonuses, damageReductions);
            maxDamage = PerformCalculation(maxBaseDamage, damageBonuses, damageReductions);
            notes = GenerateNotes(attacker, defender);            
        }

        private int PerformCalculation(int baseDamage, List<double> damageBonuses, List<double> damageReductions)
        {
            double result = baseDamage;

            double totalBonus = 1;
            foreach (double damageBonus in damageBonuses)
            {
                totalBonus += damageBonus;
            }

            result *= totalBonus;
            result = Math.Floor(result);

            foreach (double damageReduction in damageReductions)
            {
                result *= (1 - damageReduction);
                result = Math.Floor(result);
            }

            int intResult = (int)result;
            return (intResult > 0) ? intResult : 1;
        }

        private string GenerateNotes(Unit attacker, Unit defender)
        {
            if (attacker.NumberOfHits > 1)
            {
                return "x" + attacker.NumberOfHits.ToString();
            }

            // Cavaliers / Champions
            if ((attacker.Id == 10) || (attacker.Id == 11))
            {
                return "+ jousting bonus damage";
            }

            return null;
        }
    }
}
