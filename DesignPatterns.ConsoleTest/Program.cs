using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesignPatterns.Services;
using DesignPatterns.Views;

namespace DesignPatterns.ConsoleTest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //TheAClient client = new TheAClient();

            //Console.WriteLine("Are you ready!!!");

            //Console.ReadKey();

            //if(client.Authorization("Pakizh_student", "Taras20."))
            //{
            //    Console.WriteLine("Authorization success");

            //    if (client.IsAuthorizated)
            //    {
            //        Console.WriteLine("Authorizes is true");

            //        Console.WriteLine(client.CurrentUser.UserName + " " + 
            //            client.CurrentUser.Role.ToString() + " ID: " + client.CurrentUser.Id);

            //        #region PatternTest

            //        var Pmanager = client.PatternManager;

            //        var patterns = await Pmanager.GetAllAsync();

            //        foreach (var pattern in patterns)
            //        {
            //            Console.WriteLine("Id: " + pattern.Id + " Name: " + pattern.Name
            //                + " Subjects: " + string.Join(" ", pattern.subjects));
            //        }

            //        var strategy = patterns.Single();

            //        #endregion

            //        #region DiagramTest

            //        if (false)
            //        {
            //            var Dmanager = client.DiagramManager;

            //            Dmanager.Difficulty = Difficulty.Hard;

            //            var diagram = await Dmanager.GetAsync(strategy.Id);

            //            var references = diagram.SubjectReferences.ToList();

            //            foreach (var item in references)
            //            {
            //                item.Id = 0;
            //            }

            //            var newDiagram = new Diagram()
            //            {
            //                Subjects = diagram.Subjects.Where(x => x.Id > 10).ToList(),
            //                Pattern = diagram.Pattern,
            //                MethodParameters = diagram.MethodParameters,
            //                SubjectMethods = diagram.SubjectMethods,
            //                SubjectProperties = diagram.SubjectProperties,
            //                //SubjectReferences = diagram.SubjectReferences
            //                SubjectReferences = references
            //            };

            //            var result = (DiagramResult)await Dmanager.PostAsync(newDiagram);

            //            Console.WriteLine(result.ToString());
            //        }

            //        #endregion

            //        #region TestTest

            //        if (true)
            //        {
            //            var TManager = client.TestManager;

            //            var tests = await TManager.GetAsync(strategy.Id);

            //            //tests.Answers = tests.Questions.Select(x => x.Variants.Where(y => y.IsTrue).Single()).ToList();

            //            var answers = new List<AnswerView>();

            //            foreach(var item in tests.Questions)
            //            {
            //                answers.Add(item.Variants.FirstOrDefault());
            //            }

            //            tests.Answers = answers;

            //            var result = (TestResult)await TManager.PostAsync(tests);

            //            Console.WriteLine(result.ToString());
            //        }

            //        #endregion
            //    }
            //}

            Console.ReadLine();
        }
    }
}
