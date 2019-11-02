using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Views
{
    public class QuestionAnswersView : IViewBase
    {
        public QuestionView Question { get; set; }

        public IEnumerable<AnswerView> Variants { get; set; }

        public object GetId()
        {
            return Question.Id;
        }
    }
}
