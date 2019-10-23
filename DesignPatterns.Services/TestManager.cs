using DesignPatterns.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Services
{
    public class TestManager:Service<TestView>
    {
        private static readonly string _controllerName = "Test";

        public TestManager(HttpClient client) : base(client)
        {
            _client = client;
        }

        public override async Task<object> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public override async Task<IEnumerable<TestView>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public override async Task<TestView> GetAsync(int id)
        {
            HttpResponseMessage response = await _client.GetAsync("api/" + _controllerName + "/" + id);
            return _ReadAs<TestView>(response);
        }

        public override async Task<object> PostAsync(TestView model)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync("api/" + _controllerName, model);
            return _ReadAs<TestResult>(response);
        }

        public override async Task<object> UpdateAsync(TestView model)
        {
            throw new NotImplementedException();
        }
    }
}
