using DesignPatterns.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace DesignPatterns.Client.Drawing
{
    public class SubjectCanvas:ICanvasElement
    {
        public Point Center { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }
        
        public PathType BorderType { get; private set; } = PathType.Solid;

        public SubjectView View { get; set; }
        
        private PathBindingCreator _BindingCreator { get; set; } = new PathBindingCreator();

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
                    _pathBinding, _labelBinding
                };
            }
        }
        
        private LabelBinding _labelBinding;
        private LabelBinding _LabelBinding
        {
            get
            {
                return _labelBinding;
            }
            set
            {
                _labelBinding = value;

                ElementsBinding = new List<IObjectBinding>()
                {
                    _pathBinding, _labelBinding
                };
            }
        }
        

        public SubjectCanvas(SubjectView view, Point center, int width = 100, int height = 50)
        {
            View = view;

            Center = center;

            Width = width;

            Height = height;

            _LabelBinding = new LabelBinding(this);

            _PathBinding = _BindingCreator.Create(GeometryCreator.Create(this));
        }

        public void Move(Point point)
        {
            Center = point;

            _LabelBinding = new LabelBinding(this);

            _PathBinding = _BindingCreator.Create(GeometryCreator.Create(this));
        }

        public void Focus()
        {
            if(BorderType != PathType.Dashed)
            {
                _PathBinding = _BindingCreator.SetDashes(_PathBinding);
            }

            BorderType = PathType.Dashed;
        }

        public void UnFocus()
        {
            if(BorderType != PathType.Solid)
            {
                _PathBinding = _BindingCreator.SetSolid(_PathBinding);
            }

            BorderType = PathType.Solid;
        }

        //------------Interface realization --------------------

        public IGeometryCreator GeometryCreator { get; set; } = new SubjectCreator();

        public IEnumerable<IObjectBinding> ElementsBinding { get; set; }

        public bool IsEnter(Point point)
        {
            var delta = Center - point;

            if(Math.Abs(delta.X) <= Width / 2 
                && Math.Abs(delta.Y) <= Height / 2)
            {
                return true;
            }

            return false;
        }
    }
}
