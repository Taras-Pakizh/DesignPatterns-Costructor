using DesignPatterns;
using DesignPatterns.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using System.Reflection;

namespace Server.Logic
{
    public class DiagramWorker
    {
        //---------------------Static------------------------

        public static ApplicationContext cx = new ApplicationContext();

        private static readonly int _DefaultTypesCount = 10;

        public static Diagram CreateDiagram(int pattern_Id)
        {
            var pattern = cx.Patterns.Find(pattern_Id);

            if (pattern == null)
            {
                throw new Exception("Pattern ID is not existing");
            }

            var diagram = new Diagram();

            diagram.Pattern = Mapper.Map<Pattern, PatternView>(pattern);

            var subjects = pattern.subjects.ToList();

            subjects.AddRange(cx.Subjects.Take(_DefaultTypesCount));

            diagram.Subjects = Mapper.Map<IEnumerable<Subject>, IEnumerable<SubjectView>>
                (subjects);

            var subjectIds = pattern.subjects.ToList().Select(s => s.Id).ToList();

            var methods = cx.SubjectMethods.Where(x => subjectIds.Contains(x.Subject.Id)).ToList();

            diagram.SubjectMethods = Mapper.Map<IEnumerable<SubjectMethod>, IEnumerable<SubjectMethodView>>
                (methods);

            diagram.SubjectProperties = Mapper.Map<IEnumerable<SubjectProperty>, IEnumerable<SubjectPropertyView>>
                (cx.SubjectProperties.Where(x=>subjectIds.Contains(x.Subject.Id)));

            diagram.SubjectReferences = Mapper.Map<IEnumerable<SubjectReference>, IEnumerable<SubjectReferenceView>>
                (cx.SubjectReferences.Where(x=>subjectIds.Contains(x.subject.Id) || subjectIds.Contains(x.target.Id)));

            var methodIds = methods.Select(x => x.Id).ToList();

            diagram.MethodParameters = Mapper.Map<IEnumerable<MethodParameter>, IEnumerable<MethodParameterView>>
                (cx.MethodParameters.Where(x=>methodIds.Contains(x.method.Id)));

            return diagram;
        }

        /// <summary>
        /// Передає діаграму з правильними елементами і рандомно вибраними
        /// </summary>
        /// <param name="patter_Id"></param>
        /// <returns></returns>
        public static Diagram CreateRandomDiagram(int patter_Id)
        {
            var diagram = DiagramWorker.CreateDiagram(patter_Id);

            var randomDiagram = new Diagram()
            {
                Pattern = diagram.Pattern,

                SubjectReferences = new List<SubjectReferenceView>(),

                Subjects = Mapper.Map<IEnumerable<Subject>, IEnumerable<SubjectView>>
                    (_GetRandomValues<Subject>(10)),

                SubjectMethods = Mapper.Map<IEnumerable<SubjectMethod>, IEnumerable<SubjectMethodView>>
                    (_GetRandomValues<SubjectMethod>(5)),

                SubjectProperties = Mapper.Map<IEnumerable<SubjectProperty>, IEnumerable<SubjectPropertyView>>
                    (_GetRandomValues<SubjectProperty>(5)),

                MethodParameters = Mapper.Map<IEnumerable<MethodParameter>, IEnumerable<MethodParameterView>>
                    (_GetRandomValues<MethodParameter>(5))
            };

            diagram = diagram + randomDiagram;

            diagram.SubjectReferences = new List<SubjectReferenceView>();

            return diagram;
        }

        private static IEnumerable<T> _GetRandomValues<T>(int count)
            where T : class
        {
            Random random = new Random();

            var set = cx.Set<T>();

            var result = new HashSet<T>();

            for (int i = 0; i < count; ++i)
            {
                var index = random.Next(1, set.Count());

                var item = set.Find(index);

                if(item != null)
                {
                    result.Add(item);
                }
            }

            return result;
        }

        //----------------------

        private Diagram _received;

        private Diagram _correct;

        public DiagramWorker(Diagram diagram)
        {
            _received = diagram;

            _correct = DiagramWorker.CreateDiagram(_received.Pattern.Id);

            //Minus all basic types
            _correct.Subjects = _correct.Subjects.Where(x => x.type == SubjectType.Abstract_Class ||
                x.type == SubjectType.Class || x.type == SubjectType.Interface).ToList();

            //Set Ids to subjectreferences
            foreach(var reference in _received.SubjectReferences)
            {
                var cxReferences = cx.SubjectReferences
                    .Where(x => x.subject.Id == reference.subject_Id &&
                                x.target.Id == reference.target_Id &&
                                x.type == reference.type).ToList();

                if(cxReferences.Count != 0)
                {
                    reference.Id = cxReferences.Single().Id;
                }
            }
        }

        public DiagramResult Compare(Difficulty difficulty)
        {
            var result = new DiagramResult(_received.Pattern);

            foreach(var property in _received.GetType().GetProperties())
            {
                if(property.Name == "Pattern")
                {
                    continue;
                }

                var type = property.PropertyType.GetGenericArguments()[0];
                
                var methodInfo = typeof(DiagramWorker).GetRuntimeMethods()
                    .Where(x => x.Name == "CompareList").Single();

                var genericMethod = methodInfo.MakeGenericMethod(type);
                
                genericMethod.Invoke(this, new object[] 
                    { property.GetValue(_correct), property.GetValue(_received), result });
            }

            result.Finish(difficulty);

            return result;
        }

        private void CompareList<T>(IEnumerable<T> correct, IEnumerable<T> received, DiagramResult result)
            where T : IViewBase, IDiagramElement
        {
            double percentage = 100.0 / correct.Count();

            var listComparationSuccess = new List<T>();

            foreach(var item in correct)
            {
                if(CompareElement<T>(received, item, out T founded))
                {
                    result.AddComparationSuccess<T>(percentage);

                    listComparationSuccess.Add(founded);
                }
                else
                {
                    result.AddError<T>(item.ToString() + " - Not found in diagram");
                }
            }

            var listFails = received.ToList();

            foreach(var item in listComparationSuccess)
            {
                listFails.Remove(item);
            }
            
            var ids = correct.Select(x => (int)x.GetId()).ToList();

            foreach(var item in listFails)
            {
                if (!ids.Contains((int)item.GetId()))
                {
                    result.AddExtraErrorPercentage<T>(percentage);

                    result.AddError<T>(item.ToString() + " - extra in diagram");
                }
            }

            //---------doublicates-------------

            if (typeof(T) == typeof(SubjectReferenceView))
                return;

            ids = received.Select(x => (int)x.GetId()).ToList();
            
            foreach(var item in received)
            {
                int count = ids.Where(x => x == (int)item.GetId()).Count();

                if(count > 1)
                {
                    result.AddExtraErrorPercentage<T>(percentage * (count - 1));

                    result.AddError<T>("There is dublicates of element  - " + item.ToString());

                    ids.RemoveAll(x => x == (int)item.GetId());
                }
            }
        }

        private bool CompareElement<T>(IEnumerable<T> receiveds, T example, out T founded)
            where T : IDiagramElement
        {
            foreach (var received in receiveds)
            {
                if (received.Compare(example))
                {
                    founded = received;

                    return true;
                }
            }

            founded = default(T);

            return false;
        }
    }
}