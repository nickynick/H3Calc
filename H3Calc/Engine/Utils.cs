using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace H3Calc.Engine
{
    class Utils
    {
        public static Type TypeFromString(string typeName)
        {
            if (typeName == null)
            {
                return null;
            }
            else
            {
                string fullName = typeof(SecondarySkillLevel).Namespace + "." + typeName;
                return Type.GetType(fullName);
            }
        }

        public static string StringFromType(Type type)
        {
            if (type == null)
            {
                return null;
            }
            else
            {
                return type.Name;
            }
        }
    }
}
