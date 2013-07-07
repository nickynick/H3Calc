using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace H3Calc.Engine
{
    // http://mightandmagic.wikia.com/wiki/Damage_(Heroes)

    public class CombatDamageCalculator
    {
        private UnitUniqueTraitManager unitManager;

        public CombatDamageCalculator()
        {
            unitManager = new UnitUniqueTraitManager();
        }

        public void CalculateDamage(CombatDamageCalculatorInputData data, out int minDamage, out int maxDamage, out string notes)
        {
            if (data.AttackerCount == 0)
            {
                minDamage = 0;
                maxDamage = 0;
                notes = null;
                return;
            }

            CombatDamageModifier damageModifier = new CombatDamageModifier();
            AttackData attackData = new AttackData { Attacker = data.Attacker, Defender = data.Defender };

            List<ICombatUnitStatsModifier> attackerStatsModifiers = new List<ICombatUnitStatsModifier>();
            List<ICombatUnitStatsModifier> defenderStatsModifiers = new List<ICombatUnitStatsModifier>();
            List<ICombatDamageModifierProvider> attackerDamageModifierProviders = new List<ICombatDamageModifierProvider>();
            List<ICombatDamageModifierProvider> defenderDamageModifierProviders = new List<ICombatDamageModifierProvider>();

            if (data.Terrain != null)
            {
                attackerStatsModifiers.Add(data.Terrain);
                defenderStatsModifiers.Add(data.Terrain);
            }

            if (data.AttackerHeroStats != null)
            {
                attackerStatsModifiers.Add(data.AttackerHeroStats);
                attackerDamageModifierProviders.Add(data.AttackerHeroStats);
            }

            if (data.DefenderHeroStats != null)
            {
                defenderStatsModifiers.Add(data.DefenderHeroStats);
                defenderDamageModifierProviders.Add(data.DefenderHeroStats);
            }

            if (data.AttackerSpells != null)
            {
                attackerStatsModifiers.AddRange(data.AttackerSpells);
                attackerDamageModifierProviders.AddRange(data.AttackerSpells);
            }

            if (data.DefenderSpells != null)
            {
                defenderStatsModifiers.AddRange(data.DefenderSpells);
                defenderDamageModifierProviders.AddRange(data.DefenderSpells);
            }

            attackerStatsModifiers.Add(unitManager);
            defenderStatsModifiers.Add(unitManager);
            attackerDamageModifierProviders.Add(unitManager);
            defenderDamageModifierProviders.Add(unitManager);

            /////////////////////////

            UnitStats modifiedAttackerStats = data.Attacker.InitialStats.Copy();
            UnitStats modifiedDefenderStats = data.Defender.InitialStats.Copy();

            foreach (ICombatUnitStatsModifier modifier in attackerStatsModifiers)
            {
                modifier.ApplyPermanently(data.Attacker, modifiedAttackerStats);
                modifier.ApplyOnAttack(attackData, modifiedAttackerStats);
            }

            foreach (ICombatUnitStatsModifier modifier in defenderStatsModifiers)
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

            foreach (ICombatDamageModifierProvider provider in attackerDamageModifierProviders)
            {
                provider.ApplyOnAttack(attackData, damageModifier);
            }

            foreach (ICombatDamageModifierProvider provider in defenderDamageModifierProviders)
            {
                provider.ApplyOnDefense(attackData, damageModifier);
            }            

            int minBaseDamage = data.AttackerCount * modifiedAttackerStats.MinDamage;
            int maxBaseDamage = data.AttackerCount * modifiedAttackerStats.MaxDamage;

            minDamage = PerformCalculation(minBaseDamage, damageModifier);
            maxDamage = PerformCalculation(maxBaseDamage, damageModifier);
            notes = GenerateNotes(data);            
        }

        private int PerformCalculation(int baseDamage, CombatDamageModifier damageModifier)
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

        private string GenerateNotes(CombatDamageCalculatorInputData data)
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

    public class CombatDamageCalculatorInputData
    {
        public Unit Attacker { get; set; }
        public Unit Defender { get; set; }

        public int AttackerCount { get; set; }        

        public HeroStats AttackerHeroStats { get; set; }
        public HeroStats DefenderHeroStats { get; set; }        

        public List<ModifierSpell> AttackerSpells { get; set; }
        public List<ModifierSpell> DefenderSpells { get; set; }

        public Terrain Terrain { get; set; }

        public CombatDamageCalculatorInputData()
        {
            AttackerSpells = new List<ModifierSpell>();
            DefenderSpells = new List<ModifierSpell>();
        }        
    }

    public class CombatDamageModifier
    {        
        public List<double> DamageBonuses { get; set; }
        public List<double> DamageReductions { get; set; }

        public CombatDamageModifier()
        {
            DamageBonuses = new List<double>();
            DamageReductions = new List<double>();
        }
    }

    public interface ICombatUnitStatsModifier
    {
        void ApplyPermanently(Unit unit, UnitStats modifiedStats);
        void ApplyOnAttack(AttackData attackData, UnitStats modifiedStats);
        void ApplyOnDefense(AttackData attackData, UnitStats modifiedStats);
    }

    public interface ICombatDamageModifierProvider
    {
        void ApplyOnAttack(AttackData attackData, CombatDamageModifier damageModifier);
        void ApplyOnDefense(AttackData attackData, CombatDamageModifier damageModifier);
    }    
}
