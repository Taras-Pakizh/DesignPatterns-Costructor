using DesignPatterns.Client.Drawing;
using DesignPatterns.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Client.View
{
    public class ReferenceContent
    {
        public ReferencesType? Type { get; set; }

        public SubjectCanvas Subject { get; set; }

        public SubjectCanvas Target { get; set; }
        
        public bool IsReady
        {
            get
            {
                if (_counter && Target != null)
                {
                    return true;
                }

                return false;
            }
        }

        public void Clear()
        {
            Subject = null;
            Target = null;
            Type = null;
            _counter = false;
        }

        private bool _counter = false;

        public void Click(SubjectCanvas element, ReferencesType type)
        {
            if(!_counter)
            {
                Subject = element;

                Type = type;

                _counter = true;
            }
            else
            {
                Target = element;

                Type = type;
            }
        }

        public IList<ICanvasElement> CreateReference()
        {
            var view = new SubjectReferenceView()
            {
                subject_Id = Subject.View.Id,
                target_Id = Target.View.Id,
                type = (ReferencesType)Type
            };

            var reference = new ReferenceCanvas(view, Subject, Target);

            return new List<ICanvasElement>()
            {
                reference, reference.Arrow
            };
        }
    }
}
