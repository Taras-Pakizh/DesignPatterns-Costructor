using DesignPatterns.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace DesignPatterns.Services
{
    public class TheAClient:IClient
    {
        private readonly string _app_path;

        private string _token;

        public bool IsAuthorizated
        {
            get { if (_token == null) return false; return true; }
        }

        public UserView CurrentUser
        {
            get
            {
                if(_token == null)
                {
                    return null;
                }
                return _currentUser;
            }
        }

        public DiagramManager DiagramManager
        {
            get
            {
                return new DiagramManager(_CreateClient());
            }
        }

        public PatternManager PatternManager
        {
            get
            {
                return new PatternManager(_CreateClient());
            }
        }

        public TestManager TestManager
        {
            get
            {
                return new TestManager(_CreateClient());
            }
        }

        private UserView _currentUser;

        public TheAClient(string app_path = "http://localhost:51649")
        {
            _app_path = app_path;
        }
        
        //------------------------------

        public bool Authorization(string username, string password)
        {
            var pairs = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>( "grant_type", "password" ),
                    new KeyValuePair<string, string>( "username", username ),
                    new KeyValuePair<string, string> ( "Password", password )
                };
            var content = new FormUrlEncodedContent(pairs);

            try
            {
                using (var client = new HttpClient())
                {
                    var response = client.PostAsync(_app_path + "/Token", content).Result;
                    var result = response.Content.ReadAsStringAsync().Result;
                    Dictionary<string, string> tokenDictionary =
                        JsonConvert.DeserializeObject<Dictionary<string, string>>(result);

                    //------------Отримання токена авторизація та обєкту авторизованого користувача
                    _token = tokenDictionary["access_token"];
                    _currentUser = _GetCurrentUser().Result;
                }
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        public string Register(string username, string password, Role role)
        {
            var registerModel = new
            {
                Password = password,
                ConfirmPassword = password,
                Login = username,
                Role = role.ToString()
            };

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_app_path + '/');

                var response = client.PostAsJsonAsync("/api/Account/Register", registerModel).Result;

                return response.StatusCode.ToString();
            }
        }
        
        //---------------------------------

        private async Task<UserView> _GetCurrentUser()
        {
            using (var client = _CreateClient())
            {
                var responce = await client.GetAsync("api/Users");

                if (responce.IsSuccessStatusCode)
                {
                    return await responce.Content.ReadAsAsync<UserView>();
                }

                throw new Exception("Cant get current user");
            }
        }

        private HttpClient _CreateClient()
        {
            if (_token == null)
                throw new Exception("You are not authorizated. Token is null");

            var client = new HttpClient();

            if (!string.IsNullOrWhiteSpace(_token))
            {
                client.DefaultRequestHeaders.Authorization = 
                    new AuthenticationHeaderValue("Bearer", _token);

                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                client.BaseAddress = new Uri(_app_path + "/");
            }

            return client;
        }
    }
}
