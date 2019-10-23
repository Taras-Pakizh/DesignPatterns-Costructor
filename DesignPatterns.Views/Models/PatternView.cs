using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Views
{
    public class PatternView:IViewBase
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string description { get; set; }

        public ICollection<int> subjects { get; set; }

        public object GetId()
        {
            return Id;
        }
    }
}
