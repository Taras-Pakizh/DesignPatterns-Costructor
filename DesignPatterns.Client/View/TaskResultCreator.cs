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
        public static CRUDPattern CreateTests(PatternView pattern,
            IEnumerable<AdminFormElementView> Questions)
        {
            var question_answerViews = new List<QuestionAnswersView>();
            
            foreach(var item in Questions)
            {
                var question = ((QuestionView)item.Context);

                question.Name = item.Name;

                question.question = item.QuestionText;

                question.Answers = new List<int>();

                var variants = new List<AnswerView>();

                foreach (var subItem in item.HighSubElements)
                {
                    var answer = ((AnswerView)subItem.Context);

                    answer.answer = subItem.Name;

                    answer.IsTrue = subItem.IsChecked;
                    
                    variants.Add(answer);
                }

                question_answerViews.Add(new QuestionAnswersView()
                {
                    Question = question,
                    Variants = variants
                });
            }

            return new CRUDPattern()
            {
                Pattern = pattern,
                Diagram = new Diagram(),
                Tests = new TestView()
                {
                    Pattern = pattern,
                    Questions = question_answerViews
                }
            };
        }

        public static CRUDPattern Create(PatternView pattern, 
            IEnumerable<AdminFormElementView> subjects,
            IEnumerable<AdminFormElementView> references)
        {
            var subjectViews = new List<SubjectView>();
            var referenceViews = new List<SubjectReferenceView>();
            var propertyViews = new List<SubjectPropertyView>();
            var methodViews = new List<SubjectMethodView>();
            var parameterViews = new List<MethodParameterView>();

            //References---------------------------------------
            foreach(var item in references)
            {
                var context = ((SubjectReferenceView)item.Context);

                context.subject_Id = (int)item.Start.GetId();

                context.target_Id = (int)item.End.GetId();

                context.type = item.SelectedReferenceType.Type;

                referenceViews.Add(context);
            }
            
            foreach(var item in subjects)
            {
                //Subjects------------------------------------------
                var context = ((SubjectView)item.Context);

                context.type = item.SelectedSubjectType.Type;

                subjectViews.Add(context);

                //Properties-----------------------------------------
                foreach(var property in item.HighSubElements)
                {
                    var propertyView = ((SubjectPropertyView)property.Context);

                    propertyView.Name = property.Name;

                    propertyView.Type_Id = (int)property.SelectedElement.GetId();

                    propertyViews.Add(propertyView);
                }

                //Methods----------------------------------------------
                foreach(var method in item.LowSubElements)
                {
                    var methodView = ((SubjectMethodView)method.Context);

                    methodView.Name = method.Name;

                    methodView.ReturnValue_Id = (int)method.SelectedElement.GetId();

                    methodView.parameters = new List<int>();

                    methodViews.Add(methodView);

                    //Parameters---------------------------------------
                    foreach(var parameter in method.HighSubElements)
                    {
                        var paramView = ((MethodParameterView)parameter.Context);

                        paramView.Name = parameter.Name;

                        paramView.type_id = (int)parameter.SelectedElement.GetId();
                        
                        parameterViews.Add(paramView);
                    }
                }
            }
            
            var diagram = new Diagram()
            {
                Pattern = pattern,
                Subjects = subjectViews,
                SubjectReferences = referenceViews,
                SubjectProperties = propertyViews,
                SubjectMethods = methodViews,
                MethodParameters = parameterViews
            };

            return new CRUDPattern()
            {
                Pattern = pattern,
                Diagram = diagram,
                Tests = new TestView()
            };
        }

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
