using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Views
{
    public class MethodParameterView:IViewBase, IDiagramElement
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public int type_id { get; set; }

        public int method_id { get; set; }

        public object GetId()
        {
            return Id;
        }

        public override string ToString()
        {
            return "Parameter " + Name;
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public override bool Equals(object obj)
        {
            var instance = obj as MethodParameterView;
            if (instance == null)
            {
                return false;
            }
            return Id.Equals(instance.Id);
        }

        public bool Compare(IDiagramElement _example)
        {
            var example = _example as MethodParameterView;

            if (example == null)
                return false;

            var properties = GetType().GetProperties().ToList();

            properties.Remove(properties.Single(x => x.Name == nameof(Id)));
            
            return Diagram.CompareDiagramElement<MethodParameterView>(this, example, properties);
        }
    }
}
