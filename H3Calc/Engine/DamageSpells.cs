using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace H3Calc.Engine
{
    public abstract class DamageSpell : Spell
    {
        protected DamageSpell(string name, Type secondarySkillType) : base(name, secondarySkillType) { }

        public int BaseDamage()
        {
            if (SkillLevel <= SecondarySkillLevel.Basic)
            {
                return BasicBaseDamage();
            }
            else if (SkillLevel == SecondarySkillLevel.Advanced)
            {
                return AdvancedBaseDamage();
            }
            else
            {
                return ExpertBaseDamage();
            }
        }

        protected abstract int BasicBaseDamage();
        protected abstract int AdvancedBaseDamage();
        protected abstract int ExpertBaseDamage();
    }

    public class MagicArrow : DamageSpell
    {
        public MagicArrow() : base("Magic Arrow", null) { }

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
    }

    public class FireWall : DamageSpell
    {
        public FireWall() : base("Fire Wall", typeof(FireMagic)) { }

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

    public class Fireball : DamageSpell
    {
        public Fireball() : base("Fireball", typeof(FireMagic)) { }

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
        public LandMine() : base("Land Mine", typeof(FireMagic)) { }

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
        public Armageddon() : base("Armageddon", typeof(FireMagic)) { }

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
        public Inferno() : base("Inferno", typeof(FireMagic)) { }

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
        public IceBolt() : base("Ice Bolt", typeof(WaterMagic)) { }

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
        public FrostRing() : base("Frost Ring", typeof(WaterMagic)) { }

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
        public DeathRipple() : base("Death Ripple", typeof(EarthMagic)) { }

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
        public MeteorShower() : base("Meteor Shower", typeof(EarthMagic)) { }

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
        public Implosion() : base("Implosion", typeof(EarthMagic)) { }

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
        public LightningBolt() : base("Lightning Bolt", typeof(AirMagic)) { }

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
        public DestroyUndead() : base("Destroy Undead", typeof(AirMagic)) { }

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
        public ChainLightning() : base("Chain Lightning", typeof(AirMagic)) { }

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
        public TitansBolt() : base("Titan's Bolt", typeof(AirMagic)) { }

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
