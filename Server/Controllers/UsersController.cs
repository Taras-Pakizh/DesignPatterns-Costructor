using DesignPatterns;
using DesignPatterns.Views;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Server.Repository;

namespace Server.Controllers
{
    //[System.Web.Http.Authorize]
    public class UsersController : ApiController
    {
        private ApplicationContext _cx;
        
        public UsersController()
        {
            _cx = new ApplicationContext();
        }

        public UserView Get()
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(_cx));

            var user = userManager.Users.Where(x => x.UserName == User.Identity.Name).Single();
            
            return Mapper.Map<ApplicationUser, UserView>(user);
        }

        public IEnumerable<MarkView> Get(string id)
        {
            var user = _cx.Users.Find(id);

            if(user == null)
            {
                return null;
            }

            var marks = _cx.Marks.Where(x => x.User.Id == id).ToList();

            return Mapper.Map<IEnumerable<Mark>, IEnumerable<MarkView>>(marks);
        }
    }
}
