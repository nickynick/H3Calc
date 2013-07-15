using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace H3Calc.Engine
{
    public class SecondarySkillLevel
    {
        public static readonly SecondarySkillLevel None = new SecondarySkillLevel(0, "None");
        public static readonly SecondarySkillLevel Basic = new SecondarySkillLevel(1, "Basic");
        public static readonly SecondarySkillLevel Advanced = new SecondarySkillLevel(2, "Advanced");
        public static readonly SecondarySkillLevel Expert = new SecondarySkillLevel(3, "Expert");

        public static SecondarySkillLevel[] Levels()
        {
            return new SecondarySkillLevel[] { None, Basic, Advanced, Expert};
        }

        public int Value { get; private set; }
        public string Name { get; private set; }

        private SecondarySkillLevel() { }
        private SecondarySkillLevel(int value, string name)
        {
            Value = value;
            Name = name;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            SecondarySkillLevel otherObj = (SecondarySkillLevel)obj;
            return (Value == otherObj.Value);
        }

        public override int GetHashCode()
        {
            return 31 * Value;
        }

        public static bool operator <(SecondarySkillLevel left, SecondarySkillLevel right)
        {
            return (left.Value < right.Value);
        }

        public static bool operator <=(SecondarySkillLevel left, SecondarySkillLevel right)
        {
            return (left.Value <= right.Value);
        }

        public static bool operator >(SecondarySkillLevel left, SecondarySkillLevel right)
        {
            return (left.Value > right.Value);
        }

        public static bool operator >=(SecondarySkillLevel left, SecondarySkillLevel right)
        {
            return (left.Value >= right.Value);
        }
    }
}
