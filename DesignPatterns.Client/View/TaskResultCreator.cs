using DesignPatterns.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesignPatterns.Client.Drawing;

namespace DesignPatterns.Client.View
{
    public class TaskResultCreator
    {
        public static Diagram DiagramCreate(
            IEnumerable<ICanvasElement> canvasElements, 
            IEnumerable<ObjectFormView> elementsContent,
            Diagram data)
        {
            var diagram = new Diagram()
            {
                Pattern = data.Pattern
            };
            
            diagram.Subjects = elementsContent.Select(x => x.Subject.View).ToList();

            var referencesCanvas = canvasElements.Where(x => x is ReferenceCanvas)
                .Select(y => (ReferenceCanvas)y).ToList();
            
            foreach(var refCanvas in referencesCanvas)
            {
                refCanvas.View.subject_Id = refCanvas.Subject.View.Id;

                refCanvas.View.target_Id = refCanvas.Target.View.Id;
            }

            diagram.SubjectReferences = referencesCanvas.Select(x => x.View).ToList();

            var properties = new List<SubjectPropertyView>();

            var methods = new List<SubjectMethodView>();

            var parameters = new List<MethodParameterView>();

            foreach(var item in elementsContent)
            {
                //------------------

                var SubjectProperties = item.PropertyElements.Select(x => new
                {
                    View = (SubjectPropertyView)x.SelectedElement,
                    Type = x.SelectedType
                }).ToList();

                var propList = new List<SubjectPropertyView>();

                foreach(var property in SubjectProperties)
                {
                    propList.Add(new SubjectPropertyView()
                    {
                        Id = property.View.Id,
                        Name = property.View.Name,

                        Subject_Id = item.Subject.View.Id,
                        Type_Id = (int)property.Type.GetId()
                    });
                }

                properties.AddRange(propList);

                //----------------

                var SubjectMethods = item.MethodElements.Select(x => new
                {
                    View = (SubjectMethodView)x.SelectedElement,
                    Return = x.SelectedType,
                    Parameters = x.SubElements.Select(y=>new
                    {
                        View = (MethodParameterView)y.SelectedElement,
                        Type = y.SelectedType
                    })
                }).ToList();

                var methodList = new List<SubjectMethodView>();
                var parametersList = new List<MethodParameterView>();

                foreach (var method in SubjectMethods)
                {
                    methodList.Add(new SubjectMethodView()
                    {
                        Id = method.View.Id,
                        AccessType = method.View.AccessType,
                        Name = method.View.Name,
                        parameters = method.View.parameters,

                        Subject_Id = item.Subject.View.Id,
                        ReturnValue_Id = (int)method.Return.GetId()
                    });

                    foreach(var arg in method.Parameters)
                    {
                        parameters.Add(new MethodParameterView()
                        {
                            Id = arg.View.Id,
                            Name = arg.View.Name,

                            method_id = method.View.Id,
                            type_id = (int)arg.Type.GetId()
                        });
                    }
                }

                methods.AddRange(methodList);

                parameters.AddRange(parametersList);
            }

            diagram.SubjectProperties = properties;

            diagram.SubjectMethods = methods;

            diagram.MethodParameters = parameters;

            return diagram;
        }
        
    }
}
