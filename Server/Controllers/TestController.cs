﻿using DesignPatterns.Views;
using DesignPatterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using AutoMapper;
using Server.Logic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Server.Controllers
{
    //[System.Web.Http.Authorize]
    public class TestController : ApiController
    {
        private ApplicationContext _cx = new ApplicationContext();

        public TestView Get(int id)
        {
            var questions = _cx.Questions.Where(x => x.Pattern.Id == id).ToList();

            var answers = new List<Answer>();

            foreach(var question in questions)
            {
                answers.AddRange(question.Answers.ToList());
            }

            var tests = new TestView(
                Mapper.Map<Pattern, PatternView>(_cx.Patterns.Find(id)),
                Mapper.Map<IEnumerable<Question>, IEnumerable<QuestionView>>(questions),
                Mapper.Map<IEnumerable<Answer>, IEnumerable<AnswerView>>(answers)
            );

            return tests;
        }

        // POST api/Diagram
        public TestResult Post([FromBody]TestView diagramView)
        {
            if (!ModelState.IsValid)
            {
                return new TestResult("Model state isn't valid");
            }

            TestResult result = null;

            try
            {
                result = new TestResult(diagramView.Pattern, diagramView.Answers);

                var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(_cx));

                var user = userManager.Users.Where(x => x.UserName == User.Identity.Name).Single();

                var mark = new Mark()
                {
                    difficulty = Difficulty.Easy,
                    mark = result.Mark,
                    pattern = _cx.Patterns.Find(result.Pattern.Id),
                    percent = result.Percentage,
                    User = user
                };

                _cx.Marks.Add(mark);

                _cx.SaveChanges();
            }
            catch(Exception e)
            {
                return new TestResult("Bad Request " + e.Message);
            }

            return result;
        }
    }
}