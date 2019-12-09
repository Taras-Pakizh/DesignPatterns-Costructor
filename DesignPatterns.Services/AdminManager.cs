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

        public async Task<IEnumerable<SubjectView>> BasicGet()
        {
            HttpResponseMessage response = await _client.GetAsync("api/" + _controllerName);
            return _ReadAs<IEnumerable<SubjectView>>(response);
        }

        public async override Task<object> DeleteAsync(int id)
        {
            HttpResponseMessage response = await _client.DeleteAsync("api/" + _controllerName + "/" + id);
            return _ReadAs<CRUDResult>(response);
        }

        /// <summary>
        /// Not Implemented
        /// </summary>
        public override Task<IEnumerable<CRUDPattern>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// Not Implemented
        /// </summary>
        public override Task<CRUDPattern> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async override Task<object> PostAsync(CRUDPattern model)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync("api/" + _controllerName, model);
            return _ReadAs<CRUDResult>(response);
        }

        public async override Task<object> UpdateAsync(CRUDPattern model)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync("api/" + _controllerName, model);
            return _ReadAs<CRUDResult>(response);
        }
    }
}
