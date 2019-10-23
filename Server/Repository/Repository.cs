using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using DesignPatterns;
using DesignPatterns.Views;

namespace Server.Repository
{
    public class Repository<T, Tview> where T : class where Tview : IViewBase
    {
        protected ApplicationContext context;
        protected DbSet<T> set;

        public Repository()
        {
            context = new ApplicationContext();
            set = context.Set<T>();
        }

        public Repository(ApplicationContext cx)
        {
            context = cx;
            set = context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return set;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await set.ToListAsync();
        }

        public T Get(int id)
        {
            return set.Find(id);
        }

        public void Delete(int id)
        {
            set.Remove(set.Find(id));
            context.SaveChanges();
        }

        public void Add(Tview model)
        {
            var entity = Mapper.Map<Tview, T>(model);
            Mapping.Mapping.cx.Set<T>().Add(entity);
            Mapping.Mapping.cx.SaveChanges();
        }

        public void Update(Tview viewModel)
        {
            var entity = Mapper.Map<Tview, T>(viewModel);
            var model = Mapping.Mapping.cx.Set<T>().Find(viewModel.GetId());

            foreach (var property in typeof(T).GetProperties())
            {
                property.SetValue(model, property.GetValue(entity));
            }
            Mapping.Mapping.cx.SaveChanges();
        }

    }
}