using DesignPatterns.Views;
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
    public class DiagramController : ApiController
    {
        private ApplicationContext _cx = new ApplicationContext();

        // GET api/Diagram/id
        /// <summary>
        /// Повинен повертати значення правильного обєкту плюс рандомну інформацію
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Diagram Get(int id)
        {
            return DiagramWorker.CreateRandomDiagram(id);
            //return DiagramWorker.CreateDiagram(id);
        }

        // POST api/Diagram
        public DiagramResult Post([FromBody]DiagramView diagramView)
        {
            if (!ModelState.IsValid)
            {
                return new DiagramResult("Model state isn't valid");
            }

            DiagramResult result = null;

            var diagram = diagramView.Diagram;

            try
            {
                var worker = new DiagramWorker(diagram);

                result = worker.Compare(diagramView.Difficulty);

                var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(_cx));

                var user = userManager.Users.Where(x => x.UserName == User.Identity.Name).Single();

                var mark = new Mark()
                {
                    difficulty = diagramView.Difficulty,
                    mark = result.Mark,
                    percent = result.Percentage,
                    pattern = _cx.Patterns.Find(result.Pattern.Id),
                    User = user
                };
                
                _cx.Marks.Add(mark);

                _cx.SaveChanges();
            }
            catch (Exception e)
            {
                return new DiagramResult("Bad Request. Exception: " + e.Message);
            }

            return result;
        }
    }
}