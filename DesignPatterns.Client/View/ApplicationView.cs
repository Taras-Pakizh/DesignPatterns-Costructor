using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesignPatterns.Services;
using DesignPatterns.Views;

namespace DesignPatterns.Client.View
{
    class ApplicationView:MVVMView
    {
        public TheAClient Client { get; } = new TheAClient();

        public ApplicationView()
        {

        }

        #region Properties

        private Role _role;
        public Role Role
        {
            get { return _role; }
            set
            {
                _role = value;
                OnPropertyChanged(nameof(Role));
            }
        }

        #endregion

        
        //private Command _FilterReports;
        //public ICommand FilterReports
        //{
        //    get
        //    {
        //        if (_FilterReports != null)
        //            return _FilterReports;
        //        _FilterReports = new Command(_FilterReports_Exec);
        //        return _FilterReports;
        //    }
        //}
        
        /* 
        private async void _FilterReports_Exec(object obj)
         */
    }
}
