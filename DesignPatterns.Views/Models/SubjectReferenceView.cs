using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Views
{
    public class SubjectReferenceView:IViewBase, IDiagramElement
    {
        public int Id { get; set; }

        public int subject_Id { get; set; }

        public int target_Id { get; set; }

        public ReferencesType type { get; set; }

        public object GetId()
        {
            return Id;
        }

        public override string ToString()
        {
            return "Reference " + type + " between" + subject_Id + " and" + target_Id;
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public override bool Equals(object obj)
        {
            var instance = obj as SubjectReferenceView;
            if (instance == null)
            {
                return false;
            }
            return Id.Equals(instance.Id);
        }
    }
}
