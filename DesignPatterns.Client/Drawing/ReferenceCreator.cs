using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace DesignPatterns.Client.Drawing
{
    public class ReferenceCreator : IGeometryCreator
    {
        public static readonly int ArrowLength = 30;
        
        /// <summary>
        /// Returns 4 Points.
        /// 1 - touch subject,
        /// 2 - перпендикулярна 1 на відстані ArrowLength,
        /// 3 - аналогічно 2, але рядом з 4,
        /// 4 - touch target
        /// </summary>
        /// <param name="element">ReferenceCanvas</param>
        /// <returns></returns>
        public static IList<Point> GetPoints(ICanvasElement element)
        {
            var reference = (ReferenceCanvas)element;
            
            var vector = reference.Subject.Center - reference.Target.Center;

            List<Point> points = null;

            SubjectCanvas first = null; SubjectCanvas second = null;

            if (vector.X <= vector.Y)
            {
                if(reference.Subject.Center.Y < reference.Target.Center.Y)
                {
                    first = reference.Target;
                    second = reference.Subject;
                }
                else
                {
                    first = reference.Subject;
                    second = reference.Target;
                }

                points = new List<Point>()
                {
                    new Point()
                    {
                        X = first.Center.X,
                        Y = first.Center.Y - (first.Height / 2.0)
                    },
                    new Point()
                    {
                        X = first.Center.X,
                        Y = first.Center.Y - (first.Height / 2.0) - ArrowLength
                    },
                    new Point()
                    {
                        X = second.Center.X,
                        Y = second.Center.Y + (second.Height / 2.0) + ArrowLength
                    },
                    new Point()
                    {
                        X = second.Center.X,
                        Y = second.Center.Y + (second.Height / 2.0)
                    },
                };
            }
            else
            {
                if (reference.Subject.Center.X < reference.Target.Center.X)
                {
                    first = reference.Subject;
                    second = reference.Target;
                }
                else
                {
                    first = reference.Target;
                    second = reference.Subject;
                }

                points = new List<Point>()
                {
                    new Point()
                    {
                        X = first.Center.X + (first.Width / 2.0),
                        Y = first.Center.Y
                    },
                    new Point()
                    {
                        X = first.Center.X + (first.Width / 2.0) + ArrowLength,
                        Y = first.Center.Y
                    },
                    new Point()
                    {
                        X = second.Center.X - (second.Width / 2.0) - ArrowLength,
                        Y = second.Center.Y
                    },
                    new Point()
                    {
                        X = second.Center.X - (second.Width / 2.0),
                        Y = second.Center.Y
                    },
                };
            }
            
            if(first.Center != reference.Subject.Center)
            {
                points.Reverse();
            }
            
            return points;
        }
        
        /// <summary>
        /// Returns 2 PathGeomerty. First is line, second is arrow
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public PathGeometry Create(ICanvasElement element)
        {
            var reference = (ReferenceCanvas)element;

            PathGeometry lineGeometry = new PathGeometry();

            IList<Point> points = GetPoints(element);

            PathFigure line = new PathFigure()
            {
                StartPoint = points.First()
            };

            line.Segments.Add(new PolyLineSegment(new Point[] { points[1], points[2]}, true));

            line.IsClosed = false;
            
            lineGeometry.Figures.Add(line);
            
            return lineGeometry;
        }
    }
}
