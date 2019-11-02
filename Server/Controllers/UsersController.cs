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

namespace Server.Controllers
{
    //[System.Web.Http.Authorize]
    public class UsersController : ApiController
    {
        private ApplicationContext _cx = new ApplicationContext();

        public UserView Get()
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(_cx));

            var user = userManager.Users.Where(x => x.UserName == User.Identity.Name).Single();
            
            return Mapper.Map<ApplicationUser, UserView>(user);
        }
    }
}
