using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VivaTask.Application
{
    public static class Enums
    {
        public enum ResponseStatus
        {
            Success,
            Failed
        }
        public enum ListValidationStatus
        {
            None,
            [Description("List cannot be empty!")]
            EmptyList,
            [Description("List must have at least two integers")]
            LessThanTwoIntegers,
            [Description("List must have at least two different integers")]
            ExactlyTwoSameIntegers  
        }
    }
}
