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
    public class AdminView:MVVMView
    {
        public ApplicationView MainView;

        private IEnumerable<SubjectView> _BasicTypes;

        public AdminView(ApplicationView view, IEnumerable<SubjectView> basic)
        {
            _BasicTypes = basic;

            MainView = view;

            PatternVisibility = Visibility.Visible;

            PanelVisibility = Visibility.Collapsed;

            OpenPanelVisibility = Visibility.Collapsed;

            Patterns = MainView.Patterns;
        }





        #region Properties

        private PatternView _selectedPattern;
        public PatternView SelectedPattern
        {
            get { return _selectedPattern; }
            set
            {
                _selectedPattern = value;
                OnPropertyChanged(nameof(SelectedPattern));
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

        private Visibility _patternVisibility;
        public Visibility PatternVisibility
        {
            get { return _patternVisibility; }
            set
            {
                _patternVisibility = value;
                OnPropertyChanged(nameof(PatternVisibility));
            }
        }

        private Visibility _panelVisibility;
        public Visibility PanelVisibility
        {
            get { return _panelVisibility; }
            set
            {
                _panelVisibility = value;
                OnPropertyChanged(nameof(PanelVisibility));
            }
        }

        private Visibility _openPanelVisibility;
        public Visibility OpenPanelVisibility
        {
            get { return _openPanelVisibility; }
            set
            {
                _openPanelVisibility = value;
                OnPropertyChanged(nameof(OpenPanelVisibility));
            }
        }

        private PatternView _newPattern;
        public PatternView NewPattern
        {
            get { return _newPattern; }
            set
            {
                _newPattern = value;
                OnPropertyChanged(nameof(NewPattern));
            }
        }

        private AdminFormElementView _OpenedSubject;
        public AdminFormElementView OpenedSubject
        {
            get { return _OpenedSubject; }
            set
            {
                _OpenedSubject = value;
                OnPropertyChanged(nameof(OpenedSubject));
            }
        }

        private ObservableCollection<AdminFormElementView> _SubjectElements;
        public ObservableCollection<AdminFormElementView> SubjectElements
        {
            get { return _SubjectElements; }
            set
            {
                _SubjectElements = value;
                OnPropertyChanged(nameof(SubjectElements));
            }
        }

        private ObservableCollection<AdminFormElementView> _ReferenceElements;
        public ObservableCollection<AdminFormElementView> ReferenceElements
        {
            get { return _ReferenceElements; }
            set
            {
                _ReferenceElements = value;
                OnPropertyChanged(nameof(ReferenceElements));
            }
        }

        private ObservableCollection<SubjectView> _CreatedTypes;
        public ObservableCollection<SubjectView> CreatedTypes
        {
            get { return _CreatedTypes; }
            set
            {
                _CreatedTypes = value;
                OnPropertyChanged(nameof(CreatedTypes));
            }
        }

        private ObservableCollection<SubjectView> _AllTypes;
        public ObservableCollection<SubjectView> AllTypes
        {
            get { return _AllTypes; }
            set
            {
                _AllTypes = value;
                OnPropertyChanged(nameof(AllTypes));
            }
        }

        #endregion




        #region Command

        private Command _AddPattern;
        public ICommand AddPattern
        {
            get
            {
                if (_AddPattern != null)
                    return _AddPattern;
                _AddPattern = new Command(_AddPattern_Exec);
                return _AddPattern;
            }
        }
        public void _AddPattern_Exec(object parameter)
        {
            NewPattern = new PatternView()
            {
                Id = 0
            };

            SubjectElements = new ObservableCollection<AdminFormElementView>();

            ReferenceElements = new ObservableCollection<AdminFormElementView>();

            CreatedTypes = new ObservableCollection<SubjectView>();

            AllTypes = new ObservableCollection<SubjectView>(_BasicTypes);

            PatternVisibility = Visibility.Collapsed;

            PanelVisibility = Visibility.Visible;

            OpenPanelVisibility = Visibility.Collapsed;
        }

        private Command _AddSubject;
        public ICommand AddSubject
        {
            get
            {
                if (_AddSubject != null)
                    return _AddSubject;
                _AddSubject = new Command(_AddSubject_Exec);
                return _AddSubject;
            }
        }
        private void _AddSubject_Exec(object parameter)
        {
            var subject = new SubjectView()
            {
                Id = IdGenerator.GetId(IdTypes.Subject),
                pattern_Id = NewPattern.Id,
                Name = ""
            };

            CreatedTypes.Add(subject);

            AllTypes.Add(subject);

            var emptySubject = new AdminFormElementView(this, subject)
            {
                SubjectTypes = MainView.SubjectTypes
            };
            
            SubjectElements.Add(emptySubject);
        }

        private Command _AddReference;
        public ICommand AddReference
        {
            get
            {
                if (_AddReference != null)
                    return _AddReference;
                _AddReference = new Command(_AddReference_Exec);
                return _AddReference;
            }
        }
        private void _AddReference_Exec(object parameter)
        {
            var reference = new SubjectReferenceView()
            {
                Id = IdGenerator.GetId(IdTypes.Reference)
            };

            var emptyReference = new AdminFormElementView(this, reference)
            {
                CreatedElements = CreatedTypes,
                ReferenceTypes = MainView.ReferenceTypes
            };

            ReferenceElements.Add(emptyReference);
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
        public void _ToProfile_Exec(object parameter)
        {
            //App function
            throw new NotImplementedException();
        }

        private Command _UpdatePattern;
        public ICommand UpdatePattern
        {
            get
            {
                if (_UpdatePattern != null)
                    return _UpdatePattern;
                _UpdatePattern = new Command(_UpdatePattern_Exec);
                return _UpdatePattern;
            }
        }
        public void _UpdatePattern_Exec(object parameter)
        {
            throw new NotImplementedException();
        }

        private Command _DeletePattern;
        public ICommand DeletePattern
        {
            get
            {
                if (_DeletePattern != null)
                    return _DeletePattern;
                _DeletePattern = new Command(_DeletePattern_Exec);
                return _DeletePattern;
            }
        }
        public void _DeletePattern_Exec(object parameter)
        {
            //set cascade delete before
            throw new NotImplementedException();
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
            var pattern = TaskResultCreator.Create(NewPattern, SubjectElements, ReferenceElements);

            pattern.IsTestActive = false;

            var result = (CRUDResult)await MainView.Client.AdminManager.PostAsync(pattern);

            if (!result.IsSuccess)
            {
                MessageBox.Show(result.Message);

                return;
            }

            PatternVisibility = Visibility.Visible;

            PanelVisibility = Visibility.Collapsed;

            OpenPanelVisibility = Visibility.Collapsed;
        }

        #endregion
    }
}
