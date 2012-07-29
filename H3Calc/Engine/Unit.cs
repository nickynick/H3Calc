using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Schema;

namespace H3Calc.Engine
{
    public class Unit
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public UnitStats InitialStats { get; set; }

        public int NativeTerrainId { get; set; }

        [DefaultValue(false)]
        public bool IsRanged { get; set; }

        [DefaultValue(1)]
        public int NumberOfHits { get; set; }

        public Unit()
        {
            IsRanged = false;
            NumberOfHits = 1;
        }
    }

    public class UnitStats
    {
        public int Attack { get; set; }
        public int Defense { get; set; }

        public int MinDamage { get; set; }
        public int MaxDamage { get; set; }

        public int Health { get; set; }

        public UnitStats Copy()
        {
            return (UnitStats)MemberwiseClone();
        }
    }

    [XmlRoot("Units")]
    public class UnitsList : List<Unit>
    {
    }

    public struct AttackData
    {
        public Unit Attacker { get; set; }
        public Unit Defender { get; set; }
    }

    public interface IUnitStatsModifier
    {
        void ApplyPermanently(Unit unit, UnitStats modifiedStats);
        void ApplyOnAttack(AttackData attackData, UnitStats modifiedStats);
        void ApplyOnDefense(AttackData attackData, UnitStats modifiedStats);
    }
}
