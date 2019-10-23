using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Views
{
    public class UserView:IViewBase
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public Role Role { get; set; }

        public object GetId()
        {
            return Id;
        }
    }
}
