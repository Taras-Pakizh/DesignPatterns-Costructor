using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Views
{
    public class TestView : IViewBase
    {
        public PatternView Pattern { get; set; }
        
        public IEnumerable<QuestionAnswersView> Questions { get; set; }

        public IEnumerable<AnswerView> Answers { get; set; } = new List<AnswerView>();

        public object GetId()
        {
            return Pattern.Id;
        }

        public TestView() { }

        public TestView(PatternView pattern, IEnumerable<QuestionView> questions, IEnumerable<AnswerView> answers)
        {
            Pattern = pattern;

            var questionAnswers = new List<QuestionAnswersView>();

            foreach(var question in questions)
            {
                var variants = answers.Where(x => x.question_Id == question.Id).ToList();

                questionAnswers.Add(new QuestionAnswersView()
                {
                    Question = question,
                    Variants = variants
                });
            }

            Questions = questionAnswers;
        }
    }
}
