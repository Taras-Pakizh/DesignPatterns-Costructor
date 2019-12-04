using DesignPatterns.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Services
{
    public class AdminManager : Service<CRUDPattern>
    {
        private static readonly string _controllerName = "Admin";

        public AdminManager(HttpClient client) : base(client)
        {
            _client = client;
        }



        public override Task<object> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public override Task<IEnumerable<CRUDPattern>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public override Task<CRUDPattern> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public override Task<object> PostAsync(CRUDPattern model)
        {
            throw new NotImplementedException();
        }

        public override Task<object> UpdateAsync(CRUDPattern model)
        {
            throw new NotImplementedException();
        }
    }
}
