using DesignPatterns.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.Logic
{
    public class Entity_View<T, Tview> 
        where T : class
        where Tview : class, IViewBase
    {
        public T Entity { get; set; }

        public Tview View { get; set; }
    }
}
