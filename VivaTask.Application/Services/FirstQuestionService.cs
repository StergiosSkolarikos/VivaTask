using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VivaTask.Application.Services
{
    public interface IFirstQuestionService
    {
        int GetSecondLargestNumber(IEnumerable<int> numbers);
    }
    public class FirstQuestionService : IFirstQuestionService
    {
        public int GetSecondLargestNumber(IEnumerable<int> numbers)
        {
            var distinctSorted = numbers.Distinct().OrderByDescending(x => x);
            return distinctSorted.Skip(1).First();
        }
    }
}
