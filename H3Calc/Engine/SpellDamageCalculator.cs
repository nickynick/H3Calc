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
            int baseDamage = data.Spell.BaseDamage(data.Target);

            SpellDamageModifier damageModifier = data.Spell.BaseModifier(data.Target);

            UnitUniqueTraitManager.Instance.ApplySpell(data, damageModifier);
            data.CasterHeroStats.ApplySpell(data, damageModifier);

            if (data.TargetProtectionSpells != null) 
            {
                foreach (ProtectionSpell protectionSpell in data.TargetProtectionSpells) 
                {
                    protectionSpell.ApplySpell(data, damageModifier);                        
                }
            }

            if (data.CasterMagicOrbs != null)
            {
                foreach (MagicOrb magicOrb in data.CasterMagicOrbs)
                {
                    magicOrb.ApplySpell(data, damageModifier);
                }
            }
                                    
            // TODO: orb of vulnerability

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
        public HeroStats CasterHeroStats { get; set; }
        public DamageSpell Spell { get; set; }        
        public Unit Target { get; set; }
        public List<ProtectionSpell> TargetProtectionSpells { get; set; }
        public List<MagicOrb> CasterMagicOrbs { get; set; }
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
