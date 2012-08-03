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
        public int Level { get; set; }

        public UnitStats InitialStats { get; set; }

        [DefaultValue(-1)]
        public int NativeTerrainId { get; set; }

        [DefaultValue(false)]
        public bool IsRanged { get; set; }

        [DefaultValue(1)]
        public int NumberOfHits { get; set; }

        public Unit()
        {
            NativeTerrainId = -1;
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

    public class UnitUniqueTraitManager : IUnitStatsModifier, IDamageModifierProvider
    {
        public void ApplyPermanently(Unit unit, UnitStats modifiedStats)
        {
        }

        public void ApplyOnAttack(AttackData attackData, UnitStats modifiedStats)
        {
        }

        public void ApplyOnDefense(AttackData attackData, UnitStats modifiedStats)
        {
            if (attackData.Attacker.Id == 110) // Behemoth
            {
                modifiedStats.Defense = (int)(modifiedStats.Defense * 0.6);
            }

            if (attackData.Attacker.Id == 111) // Ancient Behemoth
            {
                modifiedStats.Defense = (int)(modifiedStats.Defense * 0.2);
            }
        }

        public void ApplyOnAttack(AttackData attackData, DamageModifier damageModifier)
        {
            int attackerId = attackData.Attacker.Id;
            int defenderId = attackData.Defender.Id;

            int minUnitId = Math.Min(attackerId, defenderId);
            int maxUnitId = Math.Max(attackerId, defenderId);

            // Opposite elementals

            bool isOppositeElementalPair = false;

            isOppositeElementalPair |= (minUnitId == 114 || minUnitId == 115) && (maxUnitId == 120 || maxUnitId == 121); // air / earth
            isOppositeElementalPair |= (minUnitId == 116 || minUnitId == 117) && (maxUnitId == 118 || maxUnitId == 119); // water / fire

            if (isOppositeElementalPair)
            {
                damageModifier.DamageBonuses.Add(1.0);
            }

            // Hate pairs

            bool isHatePair = false;
            isHatePair |= (minUnitId == 12 || minUnitId == 13) && (maxUnitId == 82 || maxUnitId == 83); // angels / devils
            isHatePair |= (minUnitId == 36 || minUnitId == 37) && (maxUnitId == 80 || maxUnitId == 81); // genies / efreets
            isHatePair |= (minUnitId == 41) && (maxUnitId == 69); // titans / black dragons

            if (isHatePair)
            {
                damageModifier.DamageReductions.Add(0.5);
            }

            // Attacker is Psychic Elemental, defender is immune to Mind spells 

            if ((attackerId == 122) &&
                (
                 (defenderId >= 42 && defenderId <= 55) || // Necropolis creatures
                 (defenderId == 131) || // Mummy
                 (defenderId == 32 || defenderId == 33 || defenderId == 133 || defenderId == 135) || // Golems
                 (defenderId == 40 || defenderId == 41) || // Giant / Titan
                 (defenderId == 69) || // Black Dragon
                 (defenderId >= 114 && defenderId <= 123) // Elementals
                ))
            {
                damageModifier.DamageReductions.Add(0.5);
            }

            // Magic Elemental vs Magic Elemental or Black Dragon

            if ((attackerId == 123) && (defenderId == 123 || defenderId == 69))
            {
                damageModifier.DamageReductions.Add(0.5);
            }
        }

        public void ApplyOnDefense(AttackData attackData, DamageModifier damageModifier)
        {
        }
    }
}
