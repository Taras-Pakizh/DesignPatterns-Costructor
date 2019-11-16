using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Views
{
    public class DiagramView : IViewBase
    {
        public Diagram Diagram { get; set; }

        public Difficulty Difficulty { get; set; }

        public object GetId()
        {
            return Diagram.GetId();
        }
    }
}
