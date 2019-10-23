using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DesignPatterns.Views;

namespace DesignPatterns.Services
{
    public class PatternManager:Service<PatternView>
    {
        private static readonly string _controllerName = "Pattern";

        public PatternManager(HttpClient client) : base(client)
        {
            _client = client;
        }

        //Later should be realized for admin
        public override async Task<object> PostAsync(PatternView model)
        {
            throw new NotImplementedException();
        }

        public override async Task<object> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// GET: api/Pattern
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<PatternView>> GetAllAsync()
        {
            HttpResponseMessage response = await _client.GetAsync("api/" + _controllerName);
            return _ReadAs<IEnumerable<PatternView>>(response);
        }

        /// <summary>
        /// GET: api/Pattern/id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override async Task<PatternView> GetAsync(int id)
        {
            HttpResponseMessage response = await _client.GetAsync("api/" + _controllerName + "/" + id);
            return _ReadAs<PatternView>(response);
        }

        public override async Task<object> UpdateAsync(PatternView model)
        {
            throw new NotImplementedException();
        }
    }
}
