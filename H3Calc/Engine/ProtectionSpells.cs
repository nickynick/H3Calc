using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace H3Calc.Engine
{
    public abstract class ProtectionSpell : Spell, ISpellDamageModifierProvider
    {
        protected ProtectionSpell(String name, Type secondarySkillType) : base(name, secondarySkillType) { }

        public void ApplySpell(DamageSpell spell, Unit unit, SpellDamageModifier damageModifier) 
        {
            if (spell.IsAffectedBySecondarySkillType(SecondarySkillType))
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
        public ProtectionFromFire() : base("Protection from Fire", typeof(FireMagic)) { }
    }

    public class ProtectionFromWater : ProtectionSpell
    {
        public ProtectionFromWater() : base("Protection from Water", typeof(WaterMagic)) { }
    }

    public class ProtectionFromEarth : ProtectionSpell
    {
        public ProtectionFromEarth() : base("Protection from Earth", typeof(EarthMagic)) { }
    }

    public class ProtectionFromAir : ProtectionSpell
    {
        public ProtectionFromAir() : base("Protection from Air", typeof(AirMagic)) { }
    }
}
