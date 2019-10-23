using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    public class ApplicationContext: IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext() : base("name=Pattern") { }

        public static ApplicationContext Create()
        {
            return new ApplicationContext();
        }
        
        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<Mark> Marks { get; set; }
        public virtual DbSet<MethodParameter> MethodParameters { get; set; }
        public virtual DbSet<Pattern> Patterns { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<SubjectMethod> SubjectMethods { get; set; }
        public virtual DbSet<SubjectProperty> SubjectProperties { get; set; }
        public virtual DbSet<SubjectReference> SubjectReferences { get; set; }

    }
}
