using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Views
{
    public class MarkView:IViewBase
    {
        public int Id { get; set; }

        public int mark { get; set; }

        public int percent { get; set; }

        public int pattern_Id { get; set; }

        public Difficulty difficulty { get; set; }

        public string User_Id { get; set; }

        public object GetId()
        {
            return Id;
        }
    }
}
