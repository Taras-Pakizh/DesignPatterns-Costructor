using DesignPatterns.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Client.View
{
    public class ComboBoxQuestionItem
    {
        public int Number { get; set; }

        public QuestionAnswersView View { get; set; }

        public ComboBoxQuestionItem(QuestionAnswersView view, int id)
        {
            Number = id;

            View = view;
        }
    }
}
