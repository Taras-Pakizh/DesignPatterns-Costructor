using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace DesignPatterns.Views
{
    public class Diagram:IViewBase
    {
        public PatternView Pattern { get; set; }
        
        public IEnumerable<SubjectView> Subjects { get; set; }

        public IEnumerable<SubjectMethodView> SubjectMethods { get; set; }

        public IEnumerable<SubjectPropertyView> SubjectProperties { get; set; }

        public IEnumerable<SubjectReferenceView> SubjectReferences { get; set; }

        public IEnumerable<MethodParameterView> MethodParameters { get; set; }

        public object GetId()
        {
            return Pattern.Id;
        }

        public static Diagram operator+(Diagram left, Diagram right)
        {
            if(left.Pattern.Id != right.Pattern.Id)
            {
                throw new Exception("Cant add different patterns");
            }

            var result = new Diagram()
            {
                Pattern = left.Pattern,
                Subjects = Shufle<SubjectView>(left.Subjects, right.Subjects),
                SubjectMethods = Shufle<SubjectMethodView>(left.SubjectMethods, right.SubjectMethods),
                SubjectProperties = Shufle<SubjectPropertyView>(left.SubjectProperties, right.SubjectProperties),
                SubjectReferences = Shufle<SubjectReferenceView>(left.SubjectReferences, right.SubjectReferences),
                MethodParameters = Shufle<MethodParameterView>(left.MethodParameters, right.MethodParameters)
            };

            return result;
        }

        private static IEnumerable<T> Shufle<T>(IEnumerable<T> left, IEnumerable<T> right)
        {
            var result = new List<T>(left.Count() + right.Count());
            result.AddRange(left);
            result.AddRange(right);

            Random random = new Random();

            result = result.OrderBy(x => random.Next()).ToList();

            var h = new HashSet<T>(result);

            return h;
        }

        public static bool CompareDiagramElement<T>(T instance, T example, IEnumerable<PropertyInfo> properties)
            where T : IDiagramElement
        {
            foreach(var property in properties)
            {
                var value = property.GetValue(instance);

                var trueValue = property.GetValue(example);

                if (value is int)
                {
                    if ((int)value != (int)trueValue)
                        return false;
                    else continue;
                }
                else if (value is string)
                {
                    if ((string)value != (string)trueValue)
                        return false;
                    else continue;
                }
                else if (value is ReferencesType)
                {
                    if ((ReferencesType)value != (ReferencesType)trueValue)
                        return false;
                    else continue;
                }
                else if (value is SubjectType)
                {
                    if ((SubjectType)value != (SubjectType)trueValue)
                        return false;
                    else continue;
                }
                else
                {
                    throw new Exception("This type of property is not defined");
                }
            }

            return true;
        }
    }
}
