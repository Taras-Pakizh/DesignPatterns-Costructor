using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DesignPatterns.Client.Drawing
{
    public class PathBindingCreator
    {
        public PathBindingCreator() { }

        public PathBindingCreator(Brush stroke, Brush fill, PathType type, 
            int thickness, double[] dashArray)
        {
            StrokeBrush = stroke;

            FillBrush = fill;

            Type = type;

            Thickness = thickness;

            StrokeDashArray = new DoubleCollection(dashArray);
        }

        public Brush StrokeBrush { get; set; } = Brushes.Black;

        public Brush FillBrush { get; set; } = Brushes.Aquamarine;

        public PathType Type { get; set; } = PathType.Solid;

        public int Thickness { get; set; } = 2;

        public DoubleCollection StrokeDashArray { get; set; } 
            = new DoubleCollection(new double[] { 20, 10 });

        public PathBinding Create(PathGeometry geometry)
        {
            var path = new PathBinding();

            switch (Type)
            {
                case PathType.Solid:
                    path = SetSolid(path);
                    break;
                case PathType.Dashed:
                    path = SetDashes(path);
                    break;
            }

            path.Data = geometry;

            return path;
        }

        public PathBinding SetDashes(PathBinding path)
        {
            path.Stroke = StrokeBrush;

            path.Fill = FillBrush;

            path.StrokeDashCap = PenLineCap.Round;

            path.StrokeDashArray = StrokeDashArray;

            return path;
        }

        public PathBinding SetSolid(PathBinding path)
        {
            path.Stroke = StrokeBrush;

            path.Fill = FillBrush;

            path.Thickness = Thickness;

            path.StrokeDashCap = PenLineCap.Flat;

            path.StrokeDashArray = null;

            return path;
        }
    }
}
