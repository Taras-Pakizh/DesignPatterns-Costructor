using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Views
{
    public class TestResult:AbstractResult
    {
        public TestResult()
        {

        }

        public TestResult(string message)
        {
            Message = message;

            IsModelValid = false;
        }
        
        public TestResult(IEnumerable<AnswerView> result)
        {
            double correctAnswers = 0;

            foreach(var item in result)
            {
                if (item.IsTrue)
                {
                    correctAnswers++;
                }
            }

            Percentage = (int)(correctAnswers / result.Count());

            Mark = Percentage % 20;

            IsModelValid = true;

            Message = "Ok";
        }

    }
}
