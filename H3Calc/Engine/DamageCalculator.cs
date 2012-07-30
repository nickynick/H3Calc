using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace H3Calc.Engine
{
    // http://mightandmagic.wikia.com/wiki/Damage_(Heroes)

    public class DamageCalculator
    {
        private UnitUniqueTraitManager unitManager;

        public DamageCalculator()
        {
            unitManager = new UnitUniqueTraitManager();
        }

        public void CalculateDamage(DamageCalculatorInputData data, out int minDamage, out int maxDamage, out string notes)
        {
            DamageModifier damageModifier = new DamageModifier();
            AttackData attackData = new AttackData { Attacker = data.Attacker, Defender = data.Defender };

            List<IUnitStatsModifier> attackerStatsModifiers = new List<IUnitStatsModifier>();
            List<IUnitStatsModifier> defenderStatsModifiers = new List<IUnitStatsModifier>();
            List<IDamageModifierProvider> attackerDamageModifierProviders = new List<IDamageModifierProvider>();
            List<IDamageModifierProvider> defenderDamageModifierProviders = new List<IDamageModifierProvider>();

            if (data.Terrain != null)
            {
                attackerStatsModifiers.Add(data.Terrain);
                defenderStatsModifiers.Add(data.Terrain);
            }

            if (data.AttackerHero != null)
            {
                attackerStatsModifiers.Add(data.AttackerHero.Stats);
                attackerDamageModifierProviders.Add(data.AttackerHero.Stats);
            }

            if (data.DefenderHero != null)
            {
                defenderStatsModifiers.Add(data.DefenderHero.Stats);
                defenderDamageModifierProviders.Add(data.DefenderHero.Stats);
            }

            attackerStatsModifiers.AddRange(data.AttackerSpells);
            attackerDamageModifierProviders.AddRange(data.AttackerSpells);
            defenderStatsModifiers.AddRange(data.DefenderSpells);
            defenderDamageModifierProviders.AddRange(data.DefenderSpells);

            attackerStatsModifiers.Add(unitManager);
            defenderStatsModifiers.Add(unitManager);
            attackerDamageModifierProviders.Add(unitManager);
            defenderDamageModifierProviders.Add(unitManager);

            /////////////////////////

            UnitStats modifiedAttackerStats = data.Attacker.InitialStats.Copy();
            UnitStats modifiedDefenderStats = data.Defender.InitialStats.Copy();

            foreach (IUnitStatsModifier modifier in attackerStatsModifiers)
            {
                modifier.ApplyPermanently(data.Attacker, modifiedAttackerStats);
                modifier.ApplyOnAttack(attackData, modifiedAttackerStats);
            }

            foreach (IUnitStatsModifier modifier in defenderStatsModifiers)
            {
                modifier.ApplyPermanently(data.Defender, modifiedDefenderStats);
                modifier.ApplyOnDefense(attackData, modifiedDefenderStats);
            }

            int totalAttack = modifiedAttackerStats.Attack;
            int totalDefense = modifiedDefenderStats.Defense;

            if (totalAttack > totalDefense)
            {
                double primarySkillBonus = Math.Min((totalAttack - totalDefense) * 0.05, 3.0);
                damageModifier.DamageBonuses.Add(primarySkillBonus);
            } 
            else if (totalAttack < totalDefense)
            {
                double primarySkillReduction = Math.Min((totalDefense - totalAttack) * 0.025, 0.7);
                damageModifier.DamageReductions.Add(primarySkillReduction);
            }

            /////////////////////////

            foreach (IDamageModifierProvider provider in attackerDamageModifierProviders)
            {
                provider.ApplyOnAttack(attackData, damageModifier);
            }

            foreach (IDamageModifierProvider provider in defenderDamageModifierProviders)
            {
                provider.ApplyOnDefense(attackData, damageModifier);
            }
            
            // TODO: special units (behemoths, double hits, etc.)

            int minBaseDamage = data.AttackerCount * modifiedAttackerStats.MinDamage;
            int maxBaseDamage = data.AttackerCount * modifiedAttackerStats.MaxDamage;

            minDamage = PerformCalculation(minBaseDamage, damageModifier);
            maxDamage = PerformCalculation(maxBaseDamage, damageModifier);
            notes = GenerateNotes(data);            
        }

        private int PerformCalculation(int baseDamage, DamageModifier damageModifier)
        {
            double result = baseDamage;

            double totalBonus = 1;
            foreach (double damageBonus in damageModifier.DamageBonuses)
            {
                totalBonus += damageBonus;
            }

            result *= totalBonus;
            result = Math.Floor(result);

            foreach (double damageReduction in damageModifier.DamageReductions)
            {
                result *= (1 - damageReduction);
                result = Math.Floor(result);
            }

            int intResult = (int)result;
            return (intResult > 0) ? intResult : 1;
        }

        private string GenerateNotes(DamageCalculatorInputData data)
        {
            if (data.Attacker.NumberOfHits > 1)
            {
                return "x" + data.Attacker.NumberOfHits.ToString();
            }

            // Cavaliers / Champions
            if ((data.Attacker.Id == 10) || (data.Attacker.Id == 11))
            {
                return "+ jousting bonus damage";
            }

            return null;
        }
    }    

    public class DamageCalculatorInputData
    {
        public Unit Attacker { get; set; }
        public Unit Defender { get; set; }

        public int AttackerCount { get; set; }

        public Hero AttackerHero { get; set; }
        public Hero DefenderHero { get; set; }        

        public List<Spell> AttackerSpells { get; set; }
        public List<Spell> DefenderSpells { get; set; }

        public Terrain Terrain { get; set; }

        public DamageCalculatorInputData()
        {
            AttackerSpells = new List<Spell>();
            DefenderSpells = new List<Spell>();
        }
    }

    public class DamageModifier
    {        
        public List<double> DamageBonuses { get; set; }
        public List<double> DamageReductions { get; set; }

        public DamageModifier()
        {
            DamageBonuses = new List<double>();
            DamageReductions = new List<double>();
        }
    }

    public interface IDamageModifierProvider
    {
        void ApplyOnAttack(AttackData attackData, DamageModifier damageModifier);
        void ApplyOnDefense(AttackData attackData, DamageModifier damageModifier);
    }    
}
