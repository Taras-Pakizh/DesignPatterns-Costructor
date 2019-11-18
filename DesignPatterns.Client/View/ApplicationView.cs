using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesignPatterns.Services;
using DesignPatterns.Views;

namespace DesignPatterns.Client.View
{
    class ApplicationView:MVVMView
    {
        private TheAClient Client { get; } = new TheAClient();

        public ApplicationView()
        {

        }
        
        public string ErrorMessages { get; set; } = "";

        public async Task<bool> Authorize(AuthorizationModel model)
        {
            ErrorMessages = "";

            bool result = false;

            try
            {
                result = await Client.Authorization(model.username, model.password);

                if (result)
                {
                    CurrentUser = Client.CurrentUser;

                    IsAuthorized = true;
                    
                    Patterns = new ObservableCollection<PatternView>(await Client.PatternManager.GetAllAsync());

                    var marks = await Client.UserInfo();

                    var markViews = new List<MarkViewWPF>();

                    foreach(var mark in marks)
                    {
                        markViews.Add(new MarkViewWPF(mark, Patterns));
                    }

                    Marks = new ObservableCollection<MarkViewWPF>(markViews);
                }
            }
            catch(Exception e)
            {
                ErrorMessages = e.Message;
            }

            return result;
        }

        public async Task<bool> Register(RegistrationModel model)
        {
            ErrorMessages = "";

            bool result = false;

            try
            {
                var respond = await Client.Register(model.username, model.password, model.Role);

                if(respond != "OK")
                {
                    ErrorMessages = respond;

                    return false;
                }

                result = true;
            }
            catch (Exception e)
            {
                ErrorMessages = e.Message;
            }

            return result;
        }

        public void LogOut()
        {
            Client.LogOut();
            IsAuthorized = false;
            CurrentUser = null;
        }

        public async void LoadTasks(int patternId, Difficulty difficulty)
        {
            if (!IsAuthorized)
            {
                throw new Exception("Can't start task before authorization");
            }

            CurrentPattern = Patterns.Single(x => x.Id == patternId);

            CurrentDifficulty = difficulty;

            switch (CurrentDifficulty)
            {
                case Difficulty.Easy:
                    Tests = await Client.TestManager.GetAsync(CurrentPattern.Id);
                    break;
                case Difficulty.Medium:
                case Difficulty.Hard:
                    LoadedDiagram = await Client.DiagramManager.GetAsync(CurrentPattern.Id);
                    break;
            }
        }

        #region Properties

        private Diagram _loadedDiagram;
        public Diagram LoadedDiagram
        {
            get { return _loadedDiagram; }
            set
            {
                _loadedDiagram = value;
                OnPropertyChanged(nameof(LoadedDiagram));
            }
        }

        private TestView _tests;
        public TestView Tests
        {
            get { return _tests; }
            set
            {
                _tests = value;
                OnPropertyChanged(nameof(Tests));
            }
        }

        private bool _isAuthorized;
        public bool IsAuthorized
        {
            get { return _isAuthorized; }
            set
            {
                _isAuthorized = value;
                OnPropertyChanged(nameof(IsAuthorized));
            }
        }

        private UserView _currentUser;
        public UserView CurrentUser
        {
            get { return _currentUser; }
            set
            {
                _currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
            }
        }

        private ObservableCollection<PatternView> _patterns;
        public ObservableCollection<PatternView> Patterns
        {
            get { return _patterns; }
            set
            {
                _patterns = value;
                OnPropertyChanged(nameof(Patterns));
            }
        }

        private PatternView _currentPattern;
        public PatternView CurrentPattern
        {
            get { return _currentPattern; }
            set
            {
                _currentPattern = value;
                OnPropertyChanged(nameof(CurrentPattern));
            }
        }

        private Difficulty? _currentDifficulty;
        public Difficulty? CurrentDifficulty
        {
            get { return _currentDifficulty; }
            set
            {
                _currentDifficulty = value;
                OnPropertyChanged(nameof(CurrentDifficulty));
            }
        }

        private ObservableCollection<MarkViewWPF> _marks;
        public ObservableCollection<MarkViewWPF> Marks
        {
            get { return _marks; }
            set
            {
                _marks = value;
                OnPropertyChanged(nameof(Marks));
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
