using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Views
{
    public class SubjectPropertyView:IViewBase, IDiagramElement
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public int Type_Id { get; set; }
        
        public int Subject_Id { get; set; }

        public object GetId()
        {
            return Id;
        }

        public override string ToString()
        {
            return "Property " + Name;
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public override bool Equals(object obj)
        {
            var instance = obj as SubjectPropertyView;
            if (instance == null)
            {
                return false;
            }
            return Id.Equals(instance.Id);
        }
    }
}
