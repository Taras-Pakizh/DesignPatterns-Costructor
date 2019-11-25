using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DesignPatterns.Client.Drawing
{
    public class PathBinding:IObjectBinding
    {
        public Brush Fill { get; set; }

        public Brush Stroke { get; set; }

        public int Thickness { get; set; }

        public PenLineCap StrokeDashCap { get; set; }

        public DoubleCollection StrokeDashArray { get; set; }

        public PathGeometry Data { get; set; }
    }
}
