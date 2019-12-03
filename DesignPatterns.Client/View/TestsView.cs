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
    public class TestsView:MVVMView
    {
        private ApplicationView _MainView;

        public TestView Tests { get; set; }

        public TestsView(ApplicationView view, TestView tests)
        {
            _MainView = view;

            Tests = tests;

            int id = 1;

            Questions = new ObservableCollection<ComboBoxQuestionItem>
                (Tests.Questions.Select(x => new ComboBoxQuestionItem(x, id++)));
        }



        private ComboBoxQuestionItem _SelectedQuestion;
        public ComboBoxQuestionItem SelectedQuestion
        {
            get { return _SelectedQuestion; }
            set
            {
                _SelectedQuestion = value;
                OnPropertyChanged(nameof(SelectedQuestion));
            }
        }

        private ObservableCollection<ComboBoxQuestionItem> _Questions;
        public ObservableCollection<ComboBoxQuestionItem> Questions
        {
            get { return _Questions; }
            set
            {
                _Questions = value;
                OnPropertyChanged(nameof(Questions));
            }
        }
        
        private Command _Select;
        public ICommand Select
        {
            get
            {
                if (_Select != null)
                    return _Select;
                _Select = new Command(_Select_Exec);
                return _Select;
            }
        }
        public async void _Select_Exec(object parameter)
        {
            //--------------хз-------------------
        }

        private Command _Finish;
        public ICommand Finish
        {
            get
            {
                if (_Finish != null)
                    return _Finish;
                _Finish = new Command(_Finish_Exec);
                return _Finish;
            }
        }
        public async void _Finish_Exec(object parameter)
        {
            Tests.Answers = Questions.Select(x => x.View.Variants.Where(y => y.IsTrue).
                FirstOrDefault()).ToList();

            var result = (TestResult)await _MainView.Client.TestManager.PostAsync(Tests);

            _MainView.Result = new ResultView(_MainView, result);

            _MainView.WorkSpaceVisibility = Visibility.Collapsed;

            _MainView.TestsVisibility = Visibility.Collapsed;

            _MainView.ResultVisibility = Visibility.Visible;

            _MainView.Dispose();
        }
    }
}
