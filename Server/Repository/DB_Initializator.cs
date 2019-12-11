using DesignPatterns;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Server.Repository
{
    public class DB_Initializator : CreateDatabaseIfNotExists<ApplicationContext>
    {
        public void Update(ApplicationContext context)
        {
            Seed(context);
        }

        /// <summary>
        /// Дописати ініціалізацію для базових типів даних
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(ApplicationContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            var roleList = new List<IdentityRole>()
            {
                new IdentityRole { Name = "Student" },
                new IdentityRole { Name = "Administrator" }
            };

            var users = new List<ApplicationUser>()
            {
                new ApplicationUser()
                {
                    UserName = "Pakizh_student"
                },

                new ApplicationUser()
                {
                    UserName = "Pakizh_admin"
                }
            };

            string password = "Taras20.";

            foreach (var item in users.Zip(roleList, (_user, _role) => new { user = _user, role = _role }))
            {
                roleManager.Create(item.role);

                var result = userManager.Create(item.user, password);

                if (result.Succeeded)
                {
                    userManager.AddToRole(item.user.Id, item.role.Name);
                }
            }

            var basicTypes = new List<Subject>()
            {
                new Subject()
                {
                    Name = "Int",
                    type = SubjectType.Int
                },
                new Subject()
                {
                    Name = "Double",
                    type = SubjectType.Double
                },
                new Subject()
                {
                    Name = "Bool",
                    type = SubjectType.Bool
                },
                new Subject()
                {
                    Name = "String",
                    type = SubjectType.String
                },
                new Subject()
                {
                    Name = "Char",
                    type = SubjectType.Char
                },
                new Subject()
                {
                    Name = "Float",
                    type = SubjectType.Float
                },
                new Subject()
                {
                    Name = "Long",
                    type = SubjectType.Long
                },
                new Subject()
                {
                    Name = "Short",
                    type = SubjectType.Short
                },
                new Subject()
                {
                    Name = "Byte",
                    type = SubjectType.Byte
                },
                new Subject()
                {
                    Name = "Void",
                    type = SubjectType.Void
                }
            };

            foreach(var item in basicTypes)
            {
                context.Subjects.Add(item);
            }

            context.SaveChanges();

            base.Seed(context);
        }
    }
}