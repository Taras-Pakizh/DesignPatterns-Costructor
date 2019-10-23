using DesignPatterns.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DesignPatterns;

namespace DesignPatterns.Services
{
    public abstract class Service<T> where T : class, IViewBase
    {
        protected HttpClient _client;

        protected Difficulty? _difficulty = null;

        public Service(HttpClient client)
        {
            _client = client;
        }

        public Difficulty Difficulty
        {
            get
            {
                if(_difficulty == null)
                {
                    throw new Exception("Not initialized");
                }

                return (Difficulty)_difficulty;
            }
            set
            {
                _difficulty = value;
            }
        }

        protected Tresult _ReadAs<Tresult>(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsAsync<Tresult>().Result;

                return result;
            }
            throw new Exception("Read as is fucked " + typeof(Tresult) + " = " + response.StatusCode);
        }

        protected bool _ReadAsString(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                if (string.IsNullOrEmpty(result))
                    return true;
                else
                    return false;
            }
            throw new Exception("Read as string is fucked");
        }

        public abstract Task<IEnumerable<T>> GetAllAsync();

        public abstract Task<T> GetAsync(int id);

        public abstract Task<object> PostAsync(T model);

        public abstract Task<object> UpdateAsync(T model);

        public abstract Task<object> DeleteAsync(int id);
    }
}
