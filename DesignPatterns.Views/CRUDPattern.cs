using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Views
{
    public class CRUDPattern :IViewBase
    {
        public PatternView Pattern { get; set; }

        public Diagram Diagram { get; set; }

        public TestView Tests { get; set; }
        
        public object GetId()
        {
            return Pattern.Id;
        }
    }
}
