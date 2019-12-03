using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Views
{
    public class SubjectMethodView:IViewBase, IDiagramElement
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public int Subject_Id { get; set; }
        
        public int ReturnValue_Id { get; set; }

        public AccessType AccessType { get; set; }

        public ICollection<int> parameters { get; set; }

        public object GetId()
        {
            return Id;
        }

        public override string ToString()
        {
            return "Method " + Name; 
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public override bool Equals(object obj)
        {
            var instance = obj as SubjectMethodView;
            if(instance == null)
            {
                return false;
            }
            return Id.Equals(instance.Id);
        }

        public bool Compare(IDiagramElement _example)
        {
            var example = _example as SubjectMethodView;

            if (example == null)
                return false;

            var properties = GetType().GetProperties().ToList();

            properties.Remove(properties.Single(x => x.Name == nameof(Id)));

            properties.Remove(properties.Single(x => x.Name == nameof(AccessType)));

            properties.Remove(properties.Single(x => x.Name == nameof(parameters)));

            return Diagram.CompareDiagramElement<SubjectMethodView>(this, example, properties);
        }
    }
}
