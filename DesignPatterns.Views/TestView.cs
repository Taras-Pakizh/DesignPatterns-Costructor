using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Views
{
    public class TestView : IViewBase
    {
        public Pattern Pattern { get; set; }

        public Dictionary<QuestionView, IEnumerable<AnswerView>> Questions { get; set; } = 
            new Dictionary<QuestionView, IEnumerable<AnswerView>>();

        public IEnumerable<AnswerView> Answers { get; set; }

        public object GetId()
        {
            return Pattern.Id;
        }

        public TestView() { }

        public TestView(IEnumerable<QuestionView> questions, IEnumerable<AnswerView> answers)
        {
            foreach(var question in questions)
            {
                var variants = answers.Where(x => x.question_Id == question.Id).ToList();

                Questions.Add(question, variants);
            }
        }
    }
}
