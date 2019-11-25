using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace DesignPatterns.Client.Drawing
{
    public class SubjectCreator : IGeometryCreator
    {
        public PathGeometry Create(ICanvasElement element)
        {
            var subject = (SubjectCanvas)element;
            
            PathGeometry geometry = new PathGeometry();

            PathFigure rectancle = new PathFigure()
            {
                StartPoint = new Point()
                {
                    X = subject.Center.X - subject.Width / 2,
                    Y = subject.Center.Y - subject.Height / 2
                }
            };

            var segments = new List<Point>()
            {
                new Point()
                {
                    X = subject.Center.X + subject.Width / 2,
                    Y = subject.Center.Y - subject.Height / 2
                },
                new Point()
                {
                    X = subject.Center.X + subject.Width / 2,
                    Y = subject.Center.Y + subject.Height / 2
                },
                new Point()
                {
                    X = subject.Center.X - subject.Width / 2,
                    Y = subject.Center.Y + subject.Height / 2
                }
            };

            rectancle.Segments.Add(new PolyLineSegment(segments, true));

            rectancle.IsClosed = true;

            geometry.Figures.Add(rectancle);

            return geometry;
        }
    }
}
