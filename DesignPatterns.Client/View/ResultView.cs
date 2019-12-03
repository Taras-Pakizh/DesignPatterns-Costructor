using DesignPatterns.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DesignPatterns.Client.View
{
    public class ResultView : MVVMView
    {
        private ApplicationView _MainView;
        
        public ResultView(ApplicationView view, AbstractResult _result)
        {
            _MainView = view;

            Result = _result;

            Difficulty = (Difficulty)view.CurrentDifficulty;

            ErrorsVisibility = Visibility.Visible;

            if(Result is TestResult)
            {
                ErrorsVisibility = Visibility.Collapsed;
            }
            else if(Result is DiagramResult)
            {
                var result = (DiagramResult)Result;

                Errors = new ObservableCollection<string>(result.ErrorMessages);
            }
        }

        private AbstractResult _Result;
        public AbstractResult Result
        {
            get { return _Result; }
            set
            {
                _Result = value;
                OnPropertyChanged(nameof(Result));
            }
        }
        
        private Difficulty _Difficulty;
        public Difficulty Difficulty
        {
            get { return _Difficulty; }
            set
            {
                _Difficulty = value;
                OnPropertyChanged(nameof(Difficulty));
            }
        }

        private ObservableCollection<string> _errors;
        public ObservableCollection<string> Errors
        {
            get { return _errors; }
            set
            {
                _errors = value;
                OnPropertyChanged(nameof(Errors));
            }
        }

        private Visibility _ErrorsVisibility;
        public Visibility ErrorsVisibility
        {
            get { return _ErrorsVisibility; }
            set
            {
                _ErrorsVisibility = value;
                OnPropertyChanged(nameof(ErrorsVisibility));
            }
        }

        private Command _ToProfile;
        public ICommand ToProfile
        {
            get
            {
                if (_ToProfile != null)
                    return _ToProfile;
                _ToProfile = new Command(_ToProfile_Exec);
                return _ToProfile;
            }
        }
        private async void _ToProfile_Exec(object parameter)
        {
            await ((App)Application.Current).OpenProfile();
        }
    }
}
