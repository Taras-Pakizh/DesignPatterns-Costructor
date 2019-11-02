using DesignPatterns.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Services
{
    interface IClient
    {
        bool IsAuthorizated { get; }

        bool Authorization(string username, string password);
        
        UserView CurrentUser { get; }

        string Register(string username, string password, Role role);

        void LogOut();
    }
}
