using DesignPatterns.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Services
{
    public class DiagramManager:Service<Diagram>
    {
        private static readonly string _controllerName = "Diagram";

        public DiagramManager(HttpClient client) : base(client)
        {
            _client = client;
        }
        
        /// <summary>
        /// Перед використанням необхідно ініціалізувати властивість Difficulty
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override async Task<object> PostAsync(Diagram model)
        {
            if(_difficulty == null)
            {
                throw new Exception("Difficulty not initialized");
            }

            var view = new DiagramView()
            {
                Diagram = model,
                Difficulty = (Difficulty)_difficulty
            };
            
            HttpResponseMessage response = await _client.PostAsJsonAsync("api/" + _controllerName, view);
            return _ReadAs<DiagramResult>(response);
        }

        public override async Task<object> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public override async Task<IEnumerable<Diagram>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
        
        public override async Task<Diagram> GetAsync(int id)
        {
            HttpResponseMessage response = await _client.GetAsync("api/" + _controllerName + "/" + id);
            return _ReadAs<Diagram>(response);
        }

        public override async Task<object> UpdateAsync(Diagram model)
        {
            throw new NotImplementedException();
        }
    }
}
