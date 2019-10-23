using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Views
{
    public class AnswerView:IViewBase
    {
        public int Id { get; set; }

        public string answer { get; set; }

        public bool IsTrue { get; set; }

        public int question_Id { get; set; }

        public object GetId()
        {
            return Id;
        }
    }
}
