using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Views
{
    public class AbstractResult
    {
        public string Message { get; set; }

        public bool IsModelValid { get; protected set; }

        public PatternView Pattern { get; set; }

        public int Percentage { get; set; }

        public int Mark { get; set; }

    }
}
