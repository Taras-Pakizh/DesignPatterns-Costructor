using AutoMapper;
using DesignPatterns;
using DesignPatterns.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Server.Repository;

namespace Server.Controllers
{
    public class PatternController : ApiController
    {
        private ApplicationContext _cx;

        private Repository<Pattern, PatternView> _repo;

        public PatternController() : base()
        {
            _cx = new ApplicationContext();

            _repo = new Repository<Pattern, PatternView>(_cx);
        }

        // GET api/Report
        public IEnumerable<PatternView> Get()
        {
            return Mapper.Map<IEnumerable<Pattern>, IEnumerable<PatternView>>
                (_repo.GetAll().ToList());
        }

        // GET api/Report/5
        public PatternView Get(int id)
        {
            return Mapper.Map<Pattern, PatternView>(_repo.Get(id));
        }

        //// POST api/Report
        //public IHttpActionResult Post([FromBody]ApproveView model)
        //{
        //    if (!ModelState.IsValid) { return BadRequest(ModelState); }

        //    try { service.Add(model); }

        //    catch (Exception e) { BadRequest(e.Message); }

        //    return Ok();
        //}

        //// PUT api/Report
        //public IHttpActionResult Put([FromBody]ApproveView model)
        //{
        //    if (!ModelState.IsValid) { return BadRequest(ModelState); }

        //    try { service.Update(model); }

        //    catch (Exception e) { BadRequest(e.Message); }

        //    return Ok();
        //}

        //// DELETE api/Report/5
        //public IHttpActionResult Delete(int id)
        //{
        //    try { service.Delete(id); }

        //    catch (Exception e) { BadRequest(e.Message); }

        //    return Ok();
        //}
    }
}