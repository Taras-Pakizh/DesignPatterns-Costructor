using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace DesignPatterns.Client.Drawing
{
    public interface ICanvasElement
    {
        bool IsEnter(Point point);

        IEnumerable<IObjectBinding> ElementsBinding { get; set; }

        IGeometryCreator GeometryCreator { get; set; }
    }
}
