using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace H3Calc.Engine
{
    public abstract class ProtectionSpell : Spell, ISpellDamageModifierProvider
    {
        protected ProtectionSpell(String name, Type secondarySkillType, int level) : base(name, secondarySkillType, level) { }

        public void ApplySpell(SpellDamageCalculatorData data, SpellDamageModifier damageModifier) 
        {
            if (data.Spell.IsAffectedBySecondarySkillType(SecondarySkillType))
            {
                if (SkillLevel <= SecondarySkillLevel.Basic)
                {
                    damageModifier.DamageMultipliers.Add(0.7);
                }
                else
                {
                    damageModifier.DamageMultipliers.Add(0.5);
                }
            }
        }
    }

    public class ProtectionFromFire : ProtectionSpell
    {
        public ProtectionFromFire() : base("Protection from Fire", typeof(FireMagic), 2) { }
    }

    public class ProtectionFromWater : ProtectionSpell
    {
        public ProtectionFromWater() : base("Protection from Water", typeof(WaterMagic), 2) { }
    }

    public class ProtectionFromEarth : ProtectionSpell
    {
        public ProtectionFromEarth() : base("Protection from Earth", typeof(EarthMagic), 2) { }
    }

    public class ProtectionFromAir : ProtectionSpell
    {
        public ProtectionFromAir() : base("Protection from Air", typeof(AirMagic), 2) { }
    }
}
