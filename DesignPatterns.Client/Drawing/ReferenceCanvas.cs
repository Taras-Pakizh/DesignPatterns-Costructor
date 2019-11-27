using DesignPatterns.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DesignPatterns.Client.Drawing
{
    public class ReferenceCanvas : ICanvasElement
    {
        public SubjectReferenceView View { get; set; }

        public SubjectCanvas Subject { get; set; }

        public SubjectCanvas Target { get; set; }
        
        private PathBindingCreator _BindingCreator { get; set; } = new PathBindingCreator();

        public ArrowCanvas Arrow { get; set; }

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

        public ReferenceCanvas(SubjectReferenceView view, SubjectCanvas subject, SubjectCanvas target)
        {
            View = view;

            Subject = subject;

            Target = target;

            if (View.type == ReferencesType.Dependency || View.type == ReferencesType.Realization)
            {
                _BindingCreator.Type = PathType.Dashed;
            }

            _PathBinding = _BindingCreator.Create(GeometryCreator.Create(this));

            Arrow = new ArrowCanvas(this);
        }

        public void Update(SubjectReferenceView view, SubjectCanvas subject, SubjectCanvas target)
        {
            View = view;

            Subject = subject;

            Target = target;

            if (View.type == ReferencesType.Dependency || View.type == ReferencesType.Realization)
            {
                _BindingCreator.Type = PathType.Dashed;
            }
            else
            {
                _BindingCreator.Type = PathType.Solid;
            }

            _PathBinding = _BindingCreator.Create(GeometryCreator.Create(this));

            Arrow.Update();
        }

        public void Update()
        {
            _PathBinding = _BindingCreator.Create(GeometryCreator.Create(this));

            Arrow.Update();
        }

        //--------------------Interface realization ----------------------------

        public IGeometryCreator GeometryCreator { get; set; } = new ReferenceCreator();

        public IEnumerable<IObjectBinding> ElementsBinding { get; set; }

        public bool IsEnter(Point point)
        {
            return false;
        }
    }
}
