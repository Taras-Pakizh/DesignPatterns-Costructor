using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace DesignPatterns.Client.Drawing
{
    public class ArrowCanvas : ICanvasElement
    {
        private PathBindingCreator _BindingCreator { get; set; } = new PathBindingCreator();
        
        public ReferenceCanvas Reference { get; set; }

        private PathBinding _pathBinding;
        private PathBinding _PathBinding
        {
            get
            {
                return _pathBinding;
            }
            set
            {
                _pathBinding = value;

                ElementsBinding = new List<IObjectBinding>()
                {
                    _pathBinding
                };
            }
        }

        public ArrowCanvas(ReferenceCanvas reference)
        {
            Reference = reference;

            _BindingCreator.FillBrush = Brushes.Black;

            _PathBinding = _BindingCreator.Create(GeometryCreator.Create(Reference));
        }

        public void Update()
        {
            _PathBinding = _BindingCreator.Create(GeometryCreator.Create(Reference));
        }

        //-------------------Interface realization------------------

        public IGeometryCreator GeometryCreator { get; set; } = new ArrowCreator();

        public IEnumerable<IObjectBinding> ElementsBinding { get; set; }

        public bool IsEnter(Point point)
        {
            var points = ReferenceCreator.GetPoints(Reference);

            var targetPoint = points.Last();

            var oppositePoint = points[2];

            if(targetPoint.X == oppositePoint.X && targetPoint.Y == oppositePoint.Y)
            {
                throw new Exception("Arrow can be only vertical or horizontal");
            }

            var center = new Point()
            {
                X = (targetPoint.X + oppositePoint.X) / 2,
                Y = (targetPoint.Y + oppositePoint.Y) / 2
            };

            var delta = center - point;

            if (Math.Abs(delta.X) <= ReferenceCreator.ArrowLength / 2
                && Math.Abs(delta.Y) <= ReferenceCreator.ArrowLength / 2)
            {
                return true;
            }

            return false;
        }
    }
}
