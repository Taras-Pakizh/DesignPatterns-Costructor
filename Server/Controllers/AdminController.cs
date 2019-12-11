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
    [System.Web.Http.Authorize]
    [System.Web.Http.RoutePrefix("api/Admin")]
    public class AdminController : ApiController
    {
        private ApplicationContext _cx;

        private AdminService _service;

        public AdminController() : base()
        {
            _cx = new ApplicationContext();

            _service = new AdminService(_cx);

            Mapping.Mapping.Initialize();
        }

        /// <summary>
        /// Should return basic types for building diagram
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SubjectView> Get()
        {
            return Mapper.Map<IEnumerable<Subject>, IEnumerable<SubjectView>>
                (_cx.Subjects.Where(x => x.Id <= 10).ToList());
        }

        public CRUDPattern Get(int id)
        {
            DiagramWorker.cx = new ApplicationContext();

            return new CRUDPattern()
            {
                Diagram = DiagramWorker.CreateDiagram(id)
            };
        }

        public CRUDResult Post([FromBody]CRUDPattern pattern)
        {
            return _service.Post(pattern);
        }

        public CRUDResult Put([FromBody]CRUDPattern pattern)
        {
            return _service.Put(pattern);
        }

        public CRUDResult Delete(int id)
        {
            return _service.Delete(id);
        }
    }
}
