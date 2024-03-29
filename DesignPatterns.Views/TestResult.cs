﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Views
{
    public class TestResult:AbstractResult
    {
        public TestResult() { }

        public TestResult(string message)
        {
            Message = message;

            IsModelValid = false;
        }
        
        public TestResult(PatternView pattern, IEnumerable<AnswerView> result, IList<Answer> trueAnswer)
        {
            Pattern = pattern;

            double correctAnswers = 0;

            var ids = trueAnswer.Select(x => x.Id).ToList();

            foreach(var item in result)
            {
                if (ids.Contains(item.Id))
                {
                    correctAnswers++;
                }
            }

            Percentage = (int)(100 * (correctAnswers / trueAnswer.Count()));

            Mark = Percentage / 20;

            IsModelValid = true;

            Message = "Ok";
        }

        public override string ToString()
        {
            var result = new StringBuilder();

            result.Append("------------------------\nTests result: \nPattern: " + Pattern.Name + "\n");

            result.Append("Message: " + Message + "\nIsModelValid: " + IsModelValid + "\n");

            result.Append("Percentage: " + Percentage + "\nMark: " + Mark + "\n");

            return result.ToString();
        }

    }
}
