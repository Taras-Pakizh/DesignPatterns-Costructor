using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Views
{
    public class QuestionView:IViewBase
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public string question { get; set; }

        public int Pattern_id { get; set; }

        public ICollection<int> Answers { get; set; }

        public object GetId()
        {
            return Id;
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public override bool Equals(object obj)
        {
            var questionView = obj as QuestionView;

            if (questionView == null)
                return false;

            return Id.Equals(questionView.Id);
        }
    }
}
