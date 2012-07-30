using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace H3Calc.Engine
{
    public class Terrain : IUnitStatsModifier
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public void ApplyPermanently(Unit unit, UnitStats modifiedStats)
        {
            if (unit.NativeTerrainId == Id)
            {
                modifiedStats.Attack += 1;
                modifiedStats.Defense += 1;
            }
        }

        public void ApplyOnAttack(AttackData attackData, UnitStats modifiedStats) { }
        public void ApplyOnDefense(AttackData attackData, UnitStats modifiedStats) { }
    }

    [XmlRoot("Terrains")]
    public class TerrainsList : List<Terrain>
    {
    }
}
