using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DesignPatterns.Client.Drawing
{
    public interface IGeometryCreator
    {
        PathGeometry Create(ICanvasElement element);
    }
}
