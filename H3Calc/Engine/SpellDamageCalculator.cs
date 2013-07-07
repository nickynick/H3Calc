using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace H3Calc.Engine
{
    public class SpellDamageCalculator
    {
    }

    public class SpellDamageCalculatorData
    {
        // TODO
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
        void ApplySpell(DamageSpell spell, Unit unit, SpellDamageModifier damageModifier);
    }  
}
