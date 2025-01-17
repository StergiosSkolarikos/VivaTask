using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using VivaTask.Application.Commands.Requests;
using VivaTask.Application.Commands.Responses;
using static VivaTask.Application.Enums;

namespace VivaTask.Application.Extensions
{
    public static class EnumerableExtensions
    {
        public static ListValidationStatus ValidateList(this IEnumerable<int> numbers)
        {
            if (!numbers.Any())
            {
                return ListValidationStatus.EmptyList;
            }
            if (numbers.Count() < 2)
            {
                return ListValidationStatus.LessThanTwoIntegers;
            }
            if (numbers.Count() == 2 && numbers.Distinct().Count() == 1)
            {
                return ListValidationStatus.ExactlyTwoSameIntegers;
            }

            return ListValidationStatus.None;

        }
    }
}
