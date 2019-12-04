using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Views
{
    public class CRUDResult
    {
        public PatternView Pattern { get; set; }

        public string Message { get; set; }

        public bool IsSuccess { get; set; }
    }
}
