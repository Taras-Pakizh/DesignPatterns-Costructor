using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace DesignPatterns.Client.Drawing
{
    public class ArrowCreator : IGeometryCreator
    {
        public PathGeometry Create(ICanvasElement element)
        {
            var reference = (ReferenceCanvas)element;

            var arrowGeometry = new PathGeometry();

            IList<Point> points = ReferenceCreator.GetPoints(element);
            
            foreach (var figure in _ArrowCreate(reference.View.type, points[2], points[3]))
            {
                arrowGeometry.Figures.Add(figure);
            }

            return arrowGeometry;
        }

        private IEnumerable<PathFigure> _ArrowCreate(ReferencesType type, Point start, Point end)
        {
            bool isHorizaonal = false;

            if (start.X == end.X)
            {
                isHorizaonal = false;
            }
            else if (start.Y == end.Y)
            {
                isHorizaonal = true;
            }
            else
            {
                throw new Exception("Arrow can be only vertical or horizontal");
            }

            IEnumerable<PathFigure> result = null;

            switch (type)
            {
                case ReferencesType.Assosiation:
                    result = _Assosiation(start, end, isHorizaonal);
                    break;
                case ReferencesType.Aggregation:
                    result = _Aggregation(start, end, isHorizaonal);
                    break;
                case ReferencesType.Composion:
                    result = _Composion(start, end, isHorizaonal);
                    break;
                case ReferencesType.Dependency:
                    result = _Dependency(start, end, isHorizaonal);
                    break;
                case ReferencesType.Inheritance:
                    result = _Inheritance(start, end, isHorizaonal);
                    break;
                case ReferencesType.Realization:
                    result = _Realization(start, end, isHorizaonal);
                    break;
            }

            return result;
        }

        private IEnumerable<PathFigure> _Assosiation(Point start, Point end, bool isHorizontal)
        {
            PathFigure figure = new PathFigure()
            {
                StartPoint = start
            };

            figure.Segments.Add(new LineSegment(end, true));

            return new List<PathFigure>() { figure };
        }

        /// <summary>
        /// Returns 4 points which creating a diamond
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="isHorizontal"></param>
        /// <returns></returns>
        private IList<Point> _BasicPoints(Point start, Point end, bool isHorizontal)
        {
            double stepLength = ReferenceCreator.ArrowLength / 3.0;

            double sideLength = ReferenceCreator.ArrowLength / 4.0;

            List<Point> points = new List<Point>();

            if (isHorizontal)
            {
                if (start.X < end.X)
                {
                    stepLength = -stepLength;
                }

                points.Add(new Point()
                {
                    X = start.X + stepLength,
                    Y = start.Y
                });

                points.Add(new Point()
                {
                    X = points.Last().X + stepLength,
                    Y = points.Last().Y - sideLength
                });

                points.Add(end);

                points.Add(new Point()
                {
                    X = end.X - stepLength,
                    Y = end.Y + sideLength
                });
            }
            else
            {
                if (start.Y < end.Y)
                {
                    stepLength = -stepLength;
                }

                points.Add(new Point()
                {
                    X = start.X,
                    Y = start.Y + stepLength
                });

                points.Add(new Point()
                {
                    X = start.X + sideLength,
                    Y = points.Last().Y + stepLength
                });

                points.Add(end);

                points.Add(new Point()
                {
                    X = start.X - sideLength,
                    Y = end.Y - stepLength
                });
            }

            return points;
        }

        private IEnumerable<PathFigure> _Aggregation(Point start, Point end, bool isHorizontal)
        {
            PathFigure figure = new PathFigure()
            {
                StartPoint = start
            };

            var points = _BasicPoints(start, end, isHorizontal);

            points.Add(points.First());

            figure.Segments.Add(new PolyLineSegment(points, true));

            figure.IsClosed = false;

            figure.IsFilled = false;

            return new List<PathFigure>() { figure };
        }

        private IEnumerable<PathFigure> _Composion(Point start, Point end, bool isHorizontal)
        {
            var figure = _Aggregation(start, end, isHorizontal).Single();

            figure.IsFilled = true;

            return new List<PathFigure>() { figure };
        }

        private IEnumerable<PathFigure> _Dependency(Point start, Point end, bool isHorizontal)
        {
            var points = _BasicPoints(start, end, isHorizontal);

            PathFigure line = new PathFigure()
            {
                StartPoint = start
            };

            PathFigure arrow = new PathFigure()
            {
                StartPoint = points[1]
            };
            
            line.Segments.Add(new LineSegment(end, true));

            arrow.Segments.Add(new PolyLineSegment(
                new List<Point>()
                {
                    points[2], points[3]
                },
                true
            ));

            arrow.IsClosed = false;

            arrow.IsFilled = false;

            return new List<PathFigure>() { line, arrow };
        }

        private IEnumerable<PathFigure> _Inheritance(Point start, Point end, bool isHorizontal)
        {
            var points = _BasicPoints(start, end, isHorizontal);

            PathFigure line = new PathFigure()
            {
                StartPoint = start
            };

            PathFigure triangle = new PathFigure()
            {
                StartPoint = points[1]
            };

            line.Segments.Add(new LineSegment(
                new Point()
                {
                    X = (points[1].X + points[3].X) / 2,
                    Y = (points[1].Y + points[3].Y) / 2
                }, true)
            );

            triangle.Segments.Add(new PolyLineSegment(
                new List<Point>()
                {
                    points[2], points[3]
                },
                true
            ));

            triangle.IsClosed = true;

            triangle.IsFilled = false;

            return new List<PathFigure>() { line, triangle };
        }

        private IEnumerable<PathFigure> _Realization(Point start, Point end, bool isHorizontal)
        {
            return _Inheritance(start, end, isHorizontal);
        }
    }
}
