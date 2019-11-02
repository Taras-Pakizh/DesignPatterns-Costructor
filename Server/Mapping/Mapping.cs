using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using DesignPatterns;
using DesignPatterns.Views;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Server.Mapping
{
    public static class Mapping
    {
        public static bool isInitialize { get; private set; }
        public static ApplicationContext cx { get { return context; } }

        private static ApplicationContext context = new ApplicationContext();

        private static RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>
            (new RoleStore<IdentityRole>(context));

        public static void Initialize()
        {
            if (isInitialize)
                return;
            
            Mapper.Initialize(ctg =>
            {
                //--------------------DB to View-----------------------

                ctg.CreateMap<Answer, AnswerView>()
                    .ForMember(x => x.question_Id,
                                y => y.MapFrom(z => z.question.Id));
                ctg.CreateMap<Mark, MarkView>()
                    .ForMember(x => x.pattern_Id,
                                y => y.MapFrom(z => z.pattern.Id))
                    .ForMember(x => x.User_Id,
                                y => y.MapFrom(z => z.User.Id));
                ctg.CreateMap<MethodParameter, MethodParameterView>()
                    .ForMember(x => x.method_id,
                                y => y.MapFrom(z => z.method.Id))
                    .ForMember(x => x.type_id,
                                y => y.MapFrom(z => z.type.Id));
                ctg.CreateMap<Pattern, PatternView>()
                    .ForMember(x => x.subjects,
                                y => y.MapFrom(z => z.subjects.Select(s => s.Id)));
                ctg.CreateMap<Question, QuestionView>()
                    .ForMember(x => x.Pattern_id,
                                y => y.MapFrom(z => z.Pattern.Id))
                    .ForMember(x => x.Answers,
                                y => y.MapFrom(z => z.Answers.Select(s => s.Id)));
                ctg.CreateMap<SubjectMethod, SubjectMethodView>()
                    .ForMember(x => x.Subject_Id,
                                y => y.MapFrom(z => z.Subject.Id))
                    .ForMember(x => x.ReturnValue_Id,
                                y => y.MapFrom(z => z.ReturnValue.Id))
                    .ForMember(x => x.parameters,
                                y => y.MapFrom(z => z.parameters.Select(s => s.Id)));
                ctg.CreateMap<SubjectProperty, SubjectPropertyView>()
                    .ForMember(x => x.Subject_Id,
                                y => y.MapFrom(z => z.Subject.Id))
                    .ForMember(x => x.Type_Id,
                                y => y.MapFrom(z => z.Type.Id));
                ctg.CreateMap<SubjectReference, SubjectReferenceView>()
                    .ForMember(x => x.subject_Id,
                                y => y.MapFrom(z => z.subject.Id))
                    .ForMember(x => x.target_Id,
                                y => y.MapFrom(z => z.target.Id));
                ctg.CreateMap<Subject, SubjectView>()
                    .ForMember(x => x.pattern_Id,
                                y => y.MapFrom(z => z.pattern.Id));
                ctg.CreateMap<ApplicationUser, UserView>()
                    .ForMember(x => x.Id,
                                y => y.MapFrom(z => z.Id))
                    .ForMember(x => x.UserName,
                                y => y.MapFrom(z => z.UserName))
                    .ForMember(x => x.Role,
                                y => y.MapFrom(z => (Role)Enum.Parse(typeof(Role), roleManager.FindById(z.Roles.Single().RoleId).Name)));

                //---------------------------View to DB------------------------------

                ctg.CreateMap<AnswerView, Answer>()
                    .ForMember(x => x.question,
                                y => y.MapFrom(z => cx.Questions.Find(z.question_Id)));
                ctg.CreateMap<MarkView, Mark>()
                    .ForMember(x => x.pattern,
                                y => y.MapFrom(z => cx.Patterns.Find(z.pattern_Id)))
                    .ForMember(x => x.User,
                                y => y.MapFrom(z => cx.Users.Find(z.User_Id)));
                ctg.CreateMap<MethodParameterView, MethodParameter>()
                    .ForMember(x => x.method,
                                y => y.MapFrom(z => cx.SubjectMethods.Find(z.method_id)))
                    .ForMember(x => x.type,
                                y => y.MapFrom(z => cx.Subjects.Find(z.type_id)));
                ctg.CreateMap<PatternView, Pattern>()
                    .ForMember(x => x.subjects,
                                y => y.MapFrom(z => cx.Subjects.Where(w=>z.subjects.Contains(w.Id))));
                ctg.CreateMap<QuestionView, Question>()
                    .ForMember(x => x.Pattern,
                                y => y.MapFrom(z => cx.Patterns.Find(z.Pattern_id)))
                    .ForMember(x => x.Answers,
                                y => y.MapFrom(z => cx.Answers.Where(w=>z.Answers.Contains(w.Id))));
                ctg.CreateMap<SubjectMethodView, SubjectMethod>()
                    .ForMember(x => x.Subject,
                                y => y.MapFrom(z => cx.Subjects.Find(z.Subject_Id)))
                    .ForMember(x => x.ReturnValue,
                                y => y.MapFrom(z => cx.Subjects.Find(z.ReturnValue_Id)))
                    .ForMember(x => x.parameters,
                                y => y.MapFrom(z => cx.MethodParameters.Where(w=>z.parameters.Contains(w.Id))));
                ctg.CreateMap<SubjectPropertyView, SubjectProperty>()
                    .ForMember(x => x.Subject,
                                y => y.MapFrom(z => cx.Subjects.Find(z.Subject_Id)))
                    .ForMember(x => x.Type,
                                y => y.MapFrom(z => cx.Subjects.Find(z.Type_Id)));
                ctg.CreateMap<SubjectReferenceView, SubjectReference>()
                    .ForMember(x => x.subject,
                                y => y.MapFrom(z => cx.Subjects.Find(z.subject_Id)))
                    .ForMember(x => x.target,
                                y => y.MapFrom(z => cx.Subjects.Find(z.target_Id)));
                ctg.CreateMap<SubjectView, Subject>()
                    .ForMember(x => x.pattern,
                                y => y.MapFrom(z => cx.Patterns.Find(z.pattern_Id)));
                ctg.CreateMap<UserView, ApplicationUser>()
                    .ForMember(x => x.Id,
                                y => y.MapFrom(z => z.Id))
                    .ForMember(x => x.UserName,
                                y => y.MapFrom(z => z.UserName))
                    .ForAllOtherMembers(x => x.MapFrom(z=>cx.Users.Find(z.Id)));
            });
            isInitialize = true;
        }

    }
}