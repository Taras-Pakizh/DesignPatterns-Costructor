using DesignPatterns;
using DesignPatterns.Views;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using Server.Mapping;
using AutoMapper;

namespace Server.Logic
{
    public class AdminService
    {
        private ApplicationContext _cx;

        public AdminService(ApplicationContext context)
        {
            _cx = context;

            Mapping.Mapping.cx = _cx;
        }
        
        /// <summary>
        /// Map model to Entity, 
        /// Add Entity to DB and returns Entity_View instance
        /// </summary>
        private Entity_View<T, Tview> _Add<Tview, T>(Tview viewModel) 
            where Tview : class, IViewBase
            where T : class
        {
            var model = Mapper.Map<Tview, T>(viewModel);

            _cx.Set<T>().Add(model);

            _cx.SaveChanges();
            
            return new Entity_View<T, Tview>()
            {
                Entity = model,
                View = viewModel
            };
        }

        /// <summary>
        /// Set correct ids to model : Tmodel into properties
        /// Ids gets from entity_Views, if not found - this must be basic type (leave it)
        /// </summary>
        private void _SetIds<T, Tview, Tmodel>(Tmodel model, IEnumerable<PropertyInfo> properties,
                                        IEnumerable<Entity_View<T, Tview>> entity_Views)
            where Tview : class, IViewBase
            where T : class
            where Tmodel : class, IViewBase
        {
            foreach(var property in properties)
            {
                var prev = (int)property.GetValue(model);
                
                var entity = entity_Views.Single(x => (int)x.View.GetId() == prev).Entity;

                var idProperty = entity.GetType().GetProperties().Single(x => x.Name == "Id");

                property.SetValue(model, idProperty.GetValue(entity));
            }
        }

        /// <summary>
        /// Returns hardcoded properties for every needed view type
        /// </summary>
        private IEnumerable<PropertyInfo> _GetIdProperties<Tview>()
            where Tview : class, IViewBase
        {
            var type = typeof(Tview);

            var properties = type.GetProperties();

            if(type == typeof(SubjectView))
            {
                return properties.Where(x => x.Name == "pattern_Id").ToList();
            }
            if(type == typeof(SubjectReferenceView))
            {
                return properties.Where(x => x.Name == "subject_Id" || x.Name == "target_Id").ToList();
            }
            if(type == typeof(SubjectPropertyView))
            {
                return properties.Where(x => x.Name == "Type_Id" || x.Name == "Subject_Id").ToList();
            }
            if(type == typeof(SubjectMethodView))
            {
                return properties.Where(x => x.Name == "Subject_Id" || x.Name == "ReturnValue_Id").ToList();
            }
            if(type == typeof(MethodParameterView))
            {
                return properties.Where(x => x.Name == "type_id" || x.Name == "method_id").ToList();
            }
            if(type == typeof(QuestionView))
            {
                return properties.Where(x => x.Name == "Pattern_id").ToList();
            }
            if(type == typeof(AnswerView))
            {
                return properties.Where(x => x.Name == "question_Id").ToList();
            }

            throw new Exception("_GetIdProperties : Tview type not one of available");
        }
        
        private IEnumerable<Entity_View<T, Tview>> _AddToDatabase<T, Tview, Tprev, Tviewprev>
            (IEnumerable<Tview> views, IEnumerable<PropertyInfo> properties,
            IEnumerable<Entity_View<Tprev, Tviewprev>> data)
            where T : class
            where Tview : class, IViewBase
            where Tprev : class
            where Tviewprev : class, IViewBase
        {
            var resultsEV = new List<Entity_View<T, Tview>>();
            
            foreach (var item in views)
            {
                _SetIds(item, properties, data);

                resultsEV.Add(_Add<Tview, T>(item));
            }
            
            return resultsEV;
        }

        private IEnumerable<Entity_View<Subject, SubjectView>> _GetBasic()
        {
            var entities = _cx.Subjects.Where(x => x.Id <= 10).ToList();

            var views = Mapper.Map<IEnumerable<Subject>, IEnumerable<SubjectView>>(entities);
            
            return entities.Zip(views, (entity, view) => new Entity_View<Subject, SubjectView>()
            {
                Entity = entity,
                View = view
            });
        }

        //-------------------------------------------------------------------------------
        //-----------------------------------Interface-----------------------------------
        //-------------------------------------------------------------------------------

        public CRUDResult Post(CRUDPattern data)
        {
            //sequence
            //1 - pattern, 2 - subject, 3 - (sreference, sproperties, smethods), 4 - mparaterers
            
            try
            {
                var patternEV = _Add<PatternView, Pattern>(data.Pattern);
                
                _cx.SaveChanges();

                //data.Pattern.Id = patternEV.Entity.Id;

                var result = _AddDiagram(data, patternEV);

                if (!result.IsSuccess)
                {
                    throw new Exception(result.Message);
                }
            }
            catch (Exception e)
            {
                return new CRUDResult()
                {
                    IsSuccess = false,
                    Message = e.Message
                };
            }
            
            return new CRUDResult()
            {
                IsSuccess = true,
                Message = "OK",
                Pattern = Mapper.Map<Pattern, PatternView>
                    (_cx.Patterns.Where(x => x.Name == data.Pattern.Name).Single())
            };
        }

        /// <summary>
        /// NOT creates new pattern itself
        /// Adds new diagram objects
        /// </summary>
        private CRUDResult _AddDiagram(CRUDPattern data, Entity_View<Pattern, PatternView> patternEV)
        {
            try
            {
                //var patternEV = new Entity_View<Pattern, PatternView>()
                //{
                //    View = data.Pattern,
                //    Entity = _cx.Patterns.Find(data.Pattern.Id)
                //};

                var subjectsEV = _AddToDatabase<Subject, SubjectView, Pattern, PatternView>
                            (data.Diagram.Subjects, _GetIdProperties<SubjectView>(),
                            new List<Entity_View<Pattern, PatternView>>() { patternEV }).ToList();

                subjectsEV.AddRange(_GetBasic());

                var referencesEV = _AddToDatabase<SubjectReference, SubjectReferenceView, Subject, SubjectView>
                    (data.Diagram.SubjectReferences, _GetIdProperties<SubjectReferenceView>(), subjectsEV);

                var propertiesEV = _AddToDatabase<SubjectProperty, SubjectPropertyView, Subject, SubjectView>
                    (data.Diagram.SubjectProperties, _GetIdProperties<SubjectPropertyView>(), subjectsEV);

                var methodsEV = _AddToDatabase<SubjectMethod, SubjectMethodView, Subject, SubjectView>
                    (data.Diagram.SubjectMethods, _GetIdProperties<SubjectMethodView>(), subjectsEV);

                //------parameters set property "type_id"---------------

                var paramProperties = _GetIdProperties<MethodParameterView>().ToList();

                var subjectProperty = paramProperties.Where(x => x.Name == "type_id").ToList();

                foreach (var item in data.Diagram.MethodParameters)
                {
                    _SetIds(item, subjectProperty, subjectsEV);
                }

                var methodProperty = paramProperties.Where(x => x.Name == "method_id").ToList();

                //-----------------------------------------------------

                var parametersEV = _AddToDatabase<MethodParameter, MethodParameterView, SubjectMethod, SubjectMethodView>
                    (data.Diagram.MethodParameters, methodProperty, methodsEV);
                
                _cx.SaveChanges();
            }
            catch(Exception e)
            {
                return new CRUDResult()
                {
                    IsSuccess = false,
                    Message = e.Message
                };
            }

            return new CRUDResult()
            {
                IsSuccess = true,
                Message = "OK",
                Pattern = Mapper.Map<Pattern, PatternView>
                    (_cx.Patterns.Where(x => x.Name == data.Pattern.Name).Single())
            };
        }

        /// <summary>
        /// NOT creates new pattern itself
        /// Add new test objects
        /// </summary>
        private CRUDResult _AddTests(CRUDPattern data)
        {
            try
            {
                var patternEV = new Entity_View<Pattern, PatternView>()
                {
                    View = data.Pattern,
                    Entity = _cx.Patterns.Find(data.Pattern.Id)
                };

                var questionsEV = _AddToDatabase<Question, QuestionView, Pattern, PatternView>
                (data.Tests.Questions.Select(x => x.Question).ToList(), _GetIdProperties<QuestionView>(),
                new List<Entity_View<Pattern, PatternView>>() { patternEV });

                var answerView = new List<AnswerView>();

                foreach (var item in data.Tests.Questions)
                {
                    answerView.AddRange(item.Variants);
                }

                var answersEV = _AddToDatabase<Answer, AnswerView, Question, QuestionView>
                    (answerView, _GetIdProperties<AnswerView>(), questionsEV);
            }
            catch (Exception e)
            {
                return new CRUDResult()
                {
                    IsSuccess = false,
                    Message = e.Message
                };
            }

            return new CRUDResult()
            {
                IsSuccess = true,
                Message = "OK",
                Pattern = Mapper.Map<Pattern, PatternView>
                    (_cx.Patterns.Where(x => x.Name == data.Pattern.Name).Single())
            };
        }

        /// <summary>
        /// NOT deletes pattern itself ------------ UPDATES its Name and Description
        /// IF IsTestActive - deletes prev tests and adds new
        /// IF IsTestActive - deletes diagram and adds new
        /// </summary>
        public CRUDResult Put(CRUDPattern data)
        {
            var pattern = _cx.Patterns.Find(data.Pattern.Id);

            if (pattern == null)
            {
                return new CRUDResult()
                {
                    IsSuccess = false,
                    Message = "Pattern not found"
                };
            }

            try
            {
                if (data.IsTestActive)
                {
                    var result = _DeleteTests(pattern);

                    if (!result.IsSuccess)
                    {
                        throw new Exception(result.Message);
                    }

                    return _AddTests(data);
                }
                else
                {
                    var result = _DeleteDiagramSubjects(pattern);

                    if (!result.IsSuccess)
                    {
                        throw new Exception(result.Message);
                    }

                    //Update pattern
                    pattern.Name = data.Pattern.Name;

                    pattern.description = data.Pattern.description;

                    _cx.SaveChanges();
                    //---

                    return _AddDiagram(data, new Entity_View<Pattern, PatternView>()
                    {
                        Entity = pattern,
                        View = data.Pattern
                    });
                }
            }
            catch(Exception e)
            {
                return new CRUDResult()
                {
                    IsSuccess = false,
                    Message = e.Message
                };
            }
        }

        /// <summary>
        /// Not deletes pattern itself
        /// Deletes only questions and answers table that related to pattern
        /// </summary>
        private CRUDResult _DeleteTests(Pattern pattern)
        {
            try
            {
                var questions = _cx.Questions.Where(x => x.Pattern.Id == pattern.Id).ToList();

                foreach (var question in questions)
                {
                    _cx.Answers.RemoveRange(question.Answers);

                    _cx.Questions.Remove(question);
                }

                _cx.SaveChanges();
            }
            catch(Exception e)
            {
                return new CRUDResult()
                {
                    IsSuccess = false,
                    Message = e.Message
                };
            }

            return new CRUDResult()
            {
                IsSuccess = true,
                Message = "OK"
            };
        }

        /// <summary>
        /// Not deletes pattern itself
        /// Deletes only diagram subjects tables that related to pattern
        /// </summary>
        private CRUDResult _DeleteDiagramSubjects(Pattern pattern)
        {
            try
            {
                var parameters = _cx.MethodParameters.Where(x => x.method.Subject.pattern.Id == pattern.Id).ToList();
                _cx.MethodParameters.RemoveRange(parameters);
                
                var methods = _cx.SubjectMethods.Where(x => x.Subject.pattern.Id == pattern.Id).ToList();
                _cx.SubjectMethods.RemoveRange(methods);

                var properties = _cx.SubjectProperties.Where(x => x.Subject.pattern.Id == pattern.Id).ToList();
                _cx.SubjectProperties.RemoveRange(properties);

                var references = _cx.SubjectReferences.Where(x => x.subject.pattern.Id == pattern.Id || x.target.pattern.Id == pattern.Id).ToList();
                _cx.SubjectReferences.RemoveRange(references);

                var subjects = _cx.Subjects.Where(x => x.pattern.Id == pattern.Id).ToList();
                _cx.Subjects.RemoveRange(subjects);
                
                _cx.SaveChanges();
            }
            catch (Exception e)
            {
                return new CRUDResult()
                {
                    IsSuccess = false,
                    Message = e.Message
                };
            }

            return new CRUDResult()
            {
                IsSuccess = true,
                Message = "OK"
            };
        }

        /// <summary>
        /// Not deletes pattern itself
        /// Deletes only marks related to pattern
        /// </summary>
        private CRUDResult _DeleteMarks(Pattern pattern)
        {
            try
            {
                var marks = _cx.Marks.Where(x => x.pattern.Id == pattern.Id).ToList();

                _cx.Marks.RemoveRange(marks);

                _cx.SaveChanges();
            }
            catch (Exception e)
            {
                return new CRUDResult()
                {
                    IsSuccess = false,
                    Message = e.Message
                };
            }

            return new CRUDResult()
            {
                IsSuccess = true,
                Message = "OK"
            };
        }

        /// <summary>
        /// Deletes pattern itself and do cascade delete on database
        /// </summary>
        public CRUDResult Delete(int id)
        {
            var pattern = _cx.Patterns.Find(id);

            if(pattern == null)
            {
                return new CRUDResult()
                {
                    IsSuccess = false,
                    Message = "Pattern not found"
                };
            }

            try
            {
                var result = _DeleteTests(pattern);

                if (!result.IsSuccess)
                {
                    throw new Exception(result.Message);
                }

                result = _DeleteDiagramSubjects(pattern);

                if (!result.IsSuccess)
                {
                    throw new Exception(result.Message);
                }

                result = _DeleteMarks(pattern);

                if (!result.IsSuccess)
                {
                    throw new Exception(result.Message);
                }

                _cx.Patterns.Remove(pattern);

                _cx.SaveChanges();
            }
            catch(Exception e)
            {
                return new CRUDResult()
                {
                    IsSuccess = false,
                    Message = e.Message
                };
            }

            return new CRUDResult()
            {
                IsSuccess = true,
                Message = "OK"
            };
        }
    }
}