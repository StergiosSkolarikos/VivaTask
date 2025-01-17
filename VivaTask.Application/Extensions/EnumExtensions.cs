using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace VivaTask.Application.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());

            if (field != null)
            {
                DescriptionAttribute? attribute =
                    field.GetCustomAttribute<DescriptionAttribute>();

                if (attribute != null)
                {
                    return attribute.Description;
                }
            }

            // Return the enum name if no Description is found
            return value.ToString();
        }
    }
}
