using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace H3Calc.Engine
{
    public abstract class DamageSpell : Spell
    {        
        protected DamageSpell(string name, Type secondarySkillType, int level) : base(name, secondarySkillType, level) { }

        public virtual int BaseDamage(Unit unit)
        {
            if (CasterStats.SkillLevel <= SecondarySkillLevel.Basic)
            {
                return BasicBaseDamage();
            }
            else if (CasterStats.SkillLevel == SecondarySkillLevel.Advanced)
            {
                return AdvancedBaseDamage();
            }
            else
            {
                return ExpertBaseDamage();
            }
        }

        public virtual SpellDamageModifier BaseModifier(Unit unit)
        {
            SpellDamageModifier damageModifier = new SpellDamageModifier();

            if (CasterStats.IsSpecialized)
            {
                damageModifier.DamageMultipliers.Add(SpecializationMultiplier(unit));
            }

            return damageModifier;
        }

        protected abstract int BasicBaseDamage();
        protected abstract int AdvancedBaseDamage();
        protected abstract int ExpertBaseDamage();
        protected virtual double SpecializationMultiplier(Unit unit)
        {
            return 1 + 0.03 * (CasterStats.SpecializationLevel / unit.Level);
        }
    }    

    public class MagicArrow : DamageSpell
    {
        public MagicArrow() : base("Magic Arrow", null, 1) { }

        public override bool IsAffectedBySecondarySkillType(Type skillType)
        {
            return (skillType == typeof(AirMagic) ||
                    skillType == typeof(FireMagic) ||
                    skillType == typeof(WaterMagic) ||
                    skillType == typeof(EarthMagic));
        }       

        protected override int BasicBaseDamage()
        {
            return CasterStats.SpellPower * 10 + 10;
        }

        protected override int AdvancedBaseDamage()
        {
            return CasterStats.SpellPower * 10 + 20;
        }

        protected override int ExpertBaseDamage()
        {
            return CasterStats.SpellPower * 10 + 30;
        }

        protected override double SpecializationMultiplier(Unit unit)
        {
            return 1.5;
        }
    }

    public class FireWall : DamageSpell
    {
        public FireWall() : base("Fire Wall", typeof(FireMagic), 2) { }

        protected override int BasicBaseDamage()
        {
            return CasterStats.SpellPower * 10 + 10;
        }

        protected override int AdvancedBaseDamage()
        {
            return CasterStats.SpellPower * 10 + 20;
        }

        protected override int ExpertBaseDamage()
        {
            return CasterStats.SpellPower * 10 + 50;
        }

        protected override double SpecializationMultiplier(Unit unit)
        {
            return 2;
        }
    }

    public class Fireball : DamageSpell
    {
        public Fireball() : base("Fireball", typeof(FireMagic), 3) { }

        protected override int BasicBaseDamage()
        {
            return CasterStats.SpellPower * 10 + 15;
        }

        protected override int AdvancedBaseDamage()
        {
            return CasterStats.SpellPower * 10 + 30;
        }

        protected override int ExpertBaseDamage()
        {
            return CasterStats.SpellPower * 10 + 60;
        }
    }

    public class LandMine : DamageSpell
    {
        public LandMine() : base("Land Mine", typeof(FireMagic), 3) { }        

        protected override int BasicBaseDamage()
        {
            return CasterStats.SpellPower * 10 + 25;
        }

        protected override int AdvancedBaseDamage()
        {
            return CasterStats.SpellPower * 10 + 50;
        }

        protected override int ExpertBaseDamage()
        {
            return CasterStats.SpellPower * 10 + 100;
        }
    }

    public class Armageddon : DamageSpell
    {
        public Armageddon() : base("Armageddon", typeof(FireMagic), 4) { }         

        protected override int BasicBaseDamage()
        {
            return CasterStats.SpellPower * 50 + 30;
        }

        protected override int AdvancedBaseDamage()
        {
            return CasterStats.SpellPower * 50 + 60;
        }

        protected override int ExpertBaseDamage()
        {
            return CasterStats.SpellPower * 50 + 120;
        }
    }

    public class Inferno : DamageSpell
    {
        public Inferno() : base("Inferno", typeof(FireMagic), 4) { }        

        protected override int BasicBaseDamage()
        {
            return CasterStats.SpellPower * 10 + 20;
        }

        protected override int AdvancedBaseDamage()
        {
            return CasterStats.SpellPower * 10 + 40;
        }

        protected override int ExpertBaseDamage()
        {
            return CasterStats.SpellPower * 10 + 80;
        }
    }

    public class IceBolt : DamageSpell
    {
        public IceBolt() : base("Ice Bolt", typeof(WaterMagic), 2) { }        

        protected override int BasicBaseDamage()
        {
            return CasterStats.SpellPower * 20 + 10;
        }

        protected override int AdvancedBaseDamage()
        {
            return CasterStats.SpellPower * 20 + 20;
        }

        protected override int ExpertBaseDamage()
        {
            return CasterStats.SpellPower * 20 + 50;
        }
    }

    public class FrostRing : DamageSpell
    {
        public FrostRing() : base("Frost Ring", typeof(WaterMagic), 3) { } 
        
        protected override int BasicBaseDamage()
        {
            return CasterStats.SpellPower * 10 + 15;
        }

        protected override int AdvancedBaseDamage()
        {
            return CasterStats.SpellPower * 10 + 30;
        }

        protected override int ExpertBaseDamage()
        {
            return CasterStats.SpellPower * 10 + 60;
        }
    }

    public class DeathRipple : DamageSpell
    {
        public DeathRipple() : base("Death Ripple", typeof(EarthMagic), 2) { }

        public override int BaseDamage(Unit unit)
        {
            if (unit.IsUndead)
            {
                return 0;
            }

            return base.BaseDamage(unit);
        }

        protected override int BasicBaseDamage()
        {
            return CasterStats.SpellPower * 5 + 10;
        }

        protected override int AdvancedBaseDamage()
        {
            return CasterStats.SpellPower * 5 + 20;
        }

        protected override int ExpertBaseDamage()
        {
            return CasterStats.SpellPower * 5 + 30;
        }
    }

    public class MeteorShower : DamageSpell
    {
        public MeteorShower() : base("Meteor Shower", typeof(EarthMagic), 4) { }         

        protected override int BasicBaseDamage()
        {
            return CasterStats.SpellPower * 25 + 25;
        }

        protected override int AdvancedBaseDamage()
        {
            return CasterStats.SpellPower * 25 + 50;
        }

        protected override int ExpertBaseDamage()
        {
            return CasterStats.SpellPower * 25 + 100;
        }
    }

    public class Implosion : DamageSpell
    {
        public Implosion() : base("Implosion", typeof(EarthMagic), 5) { }

        protected override int BasicBaseDamage()
        {
            return CasterStats.SpellPower * 75 + 100;
        }

        protected override int AdvancedBaseDamage()
        {
            return CasterStats.SpellPower * 75 + 200;
        }

        protected override int ExpertBaseDamage()
        {
            return CasterStats.SpellPower * 75 + 300;
        }
    }

    public class LightningBolt : DamageSpell
    {
        public LightningBolt() : base("Lightning Bolt", typeof(AirMagic), 2) { }         

        protected override int BasicBaseDamage()
        {
            return CasterStats.SpellPower * 25 + 10;
        }

        protected override int AdvancedBaseDamage()
        {
            return CasterStats.SpellPower * 25 + 20;
        }

        protected override int ExpertBaseDamage()
        {
            return CasterStats.SpellPower * 25 + 50;
        }
    }

    public class DestroyUndead : DamageSpell
    {
        public DestroyUndead() : base("Destroy Undead", typeof(AirMagic), 3) { }

        public override int BaseDamage(Unit unit)
        {
            if (!unit.IsUndead)
            {
                return 0;
            }

            return base.BaseDamage(unit);
        }

        protected override int BasicBaseDamage()
        {
            return CasterStats.SpellPower * 10 + 10;
        }

        protected override int AdvancedBaseDamage()
        {
            return CasterStats.SpellPower * 10 + 20;
        }

        protected override int ExpertBaseDamage()
        {
            return CasterStats.SpellPower * 10 + 50;
        }
    }

    public class ChainLightning : DamageSpell
    {
        public ChainLightning() : base("Chain Lightning", typeof(AirMagic), 4) { }         

        protected override int BasicBaseDamage()
        {
            return CasterStats.SpellPower * 40 + 25;
        }

        protected override int AdvancedBaseDamage()
        {
            return CasterStats.SpellPower * 40 + 50;
        }

        protected override int ExpertBaseDamage()
        {
            return CasterStats.SpellPower * 40 + 100;
        }
    }

    public class TitansBolt : DamageSpell
    {
        public TitansBolt() : base("Titan's Bolt", typeof(AirMagic), 4) { }

        protected override int BasicBaseDamage()
        {
            return 600;
        }

        protected override int AdvancedBaseDamage()
        {
            return BasicBaseDamage();
        }

        protected override int ExpertBaseDamage()
        {
            return BasicBaseDamage();
        }
    }
}
