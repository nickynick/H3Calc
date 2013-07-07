using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace H3Calc.Engine
{
    public class SpellDamageCalculator
    {
        public void CalculateDamage(SpellDamageCalculatorData data, out int damage)
        {
            int baseDamage = data.Spell.BaseDamage(data.Unit);

            SpellDamageModifier damageModifier = data.Spell.BaseModifier(data.Unit);

            UnitUniqueTraitManager.Instance.ApplySpell(data, damageModifier);
            data.Spell.CasterStats.ApplySpell(data, damageModifier);
            // TODO: protection spells
            // TODO: orbs
            // TODO: sorcery (UI)

            damage = PerformCalculation(baseDamage, damageModifier);
        }

        private int PerformCalculation(int baseDamage, SpellDamageModifier damageModifier)
        {
            double result = baseDamage;

            foreach (double damageMultiplier in damageModifier.DamageMultipliers)
            {
                result *= damageMultiplier;
            }

            result = Math.Floor(result);

            int intResult = (int)result;
            return intResult;            
        }
    }

    public class SpellDamageCalculatorData
    {
        public Unit Unit { get; set; }
        public DamageSpell Spell { get; set; }        
    }

    public class SpellDamageModifier
    {
        public List<double> DamageMultipliers { get; set; }

        public SpellDamageModifier()
        {
            DamageMultipliers = new List<double>();            
        }
    }

    public interface ISpellDamageModifierProvider
    {
        void ApplySpell(SpellDamageCalculatorData data, SpellDamageModifier damageModifier);
    }  
}
