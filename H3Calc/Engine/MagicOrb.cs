using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace H3Calc.Engine
{
    public class MagicOrb : ISpellDamageModifierProvider
    {
        public Type MagicType { get; set; }

        public MagicOrb(Type magicType)
        {
            MagicType = magicType;
        }

        public void ApplySpell(SpellDamageCalculatorData data, SpellDamageModifier damageModifier)
        {
            if (data.Spell.IsAffectedBySecondarySkillType(MagicType))
            {
                damageModifier.DamageMultipliers.Add(1.5);
            }
        }
    }
}
