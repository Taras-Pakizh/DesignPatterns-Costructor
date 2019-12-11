using DesignPatterns.Client.Windows;
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

            TestVisibility = Visibility.Collapsed;

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

        private AdminFormElementView _SelectedQuestion;
        public AdminFormElementView SelectedQuestion
        {
            get { return _SelectedQuestion; }
            set
            {
                _SelectedQuestion = value;
                if(_SelectedQuestion != null)
                {
                    TestContentVisibility = Visibility.Visible;
                }
                OnPropertyChanged(nameof(SelectedQuestion));
            }
        }

        private ObservableCollection<AdminFormElementView> _Questions;
        public ObservableCollection<AdminFormElementView> Questions
        {
            get { return _Questions; }
            set
            {
                _Questions = value;
                OnPropertyChanged(nameof(Questions));
            }
        }

        private Visibility _TestVisibility;
        public Visibility TestVisibility
        {
            get { return _TestVisibility; }
            set
            {
                _TestVisibility = value;
                OnPropertyChanged(nameof(TestVisibility));
            }
        }

        private Visibility _TestContentVisibility;
        public Visibility TestContentVisibility
        {
            get { return _TestContentVisibility; }
            set
            {
                _TestContentVisibility = value;
                OnPropertyChanged(nameof(TestContentVisibility));
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

            TestVisibility = Visibility.Collapsed;
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
        public async void _UpdatePattern_Exec(object parameter)
        {
            if (SelectedPattern == null)
            {
                return;
            }

            NewPattern = SelectedPattern;
            
            var crud = await MainView.Client.AdminManager.GetAsync(NewPattern.Id);

            var diagram = crud.Diagram;
            
            //set values to SubjectsElements, referenceElements, CreatedTypes, AllTypes, IdGenerator
            //Update name of pattern !!!!!!!-------------------------------

            SubjectElements = new ObservableCollection<AdminFormElementView>();

            ReferenceElements = new ObservableCollection<AdminFormElementView>();

            CreatedTypes = new ObservableCollection<SubjectView>();

            AllTypes = new ObservableCollection<SubjectView>(_BasicTypes);

            IdGenerator.SetDiagramIds(diagram);

            foreach(var item in diagram.Subjects.Where(x => x.Id > 10).ToList())
            {
                var subject = new AdminFormElementView(this, item)
                {
                    SubjectTypes = MainView.SubjectTypes,

                    SelectedSubjectType = MainView.SubjectTypes.Single(x => x.Type == item.type)
                };
                
                CreatedTypes.Add(item);

                AllTypes.Add(item);
                
                //-------------------Subjects
                foreach(var highSubItem in diagram.SubjectProperties.Where(x=>x.Subject_Id == item.Id))
                {
                    subject.HighSubElements.Add(new AdminFormElementView(this, highSubItem)
                    {
                        AllElements = AllTypes,

                        SelectedElement = diagram.Subjects.Single(x => x.Id == highSubItem.Type_Id),

                        Name = highSubItem.Name
                    });
                }

                foreach(var lowSubItem in diagram.SubjectMethods.Where(x=>x.Subject_Id == item.Id))
                {
                    //-----------------Methods
                    var method = new AdminFormElementView(this, lowSubItem)
                    {
                        AllElements = AllTypes,

                        Name = lowSubItem.Name,

                        SelectedElement = diagram.Subjects.Single(x=>x.Id == lowSubItem.ReturnValue_Id),
                    };
                    
                    //---------------------Parameters
                    foreach(var highSubItem in diagram.MethodParameters.Where(x=>x.method_id == lowSubItem.Id))
                    {
                        method.HighSubElements.Add(new AdminFormElementView(this, highSubItem)
                        {
                            AllElements = AllTypes,

                            SelectedElement = diagram.Subjects.Single(x=>x.Id == highSubItem.type_id),

                            Name = highSubItem.Name
                        });
                    }

                    subject.LowSubElements.Add(method);
                }

                SubjectElements.Add(subject);
            }

            //---------------------------References
            foreach(var item in diagram.SubjectReferences)
            {
                ReferenceElements.Add(new AdminFormElementView(this, item)
                {
                    CreatedElements = CreatedTypes,

                    ReferenceTypes = MainView.ReferenceTypes,

                    SelectedReferenceType = MainView.ReferenceTypes.Single(x=>x.Type == item.type),

                    Start = diagram.Subjects.Single(x=>x.Id == item.subject_Id),

                    End = diagram.Subjects.Single(x=>x.Id == item.target_Id)
                });
            }
            
            PatternVisibility = Visibility.Collapsed;

            PanelVisibility = Visibility.Visible;

            OpenPanelVisibility = Visibility.Collapsed;

            TestVisibility = Visibility.Collapsed;
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
            if (SelectedPattern == null)
                return;

            ((App)Application.Current).ShowDialog(this,
                new DeleteWindow("Do you want to delete pattern: " + SelectedPattern.Name));
        }

        private Command _UpdateTests;
        public ICommand UpdateTests
        {
            get
            {
                if (_UpdateTests != null)
                    return _UpdateTests;
                _UpdateTests = new Command(_UpdateTests_Exec);
                return _UpdateTests;
            }
        }
        private async void _UpdateTests_Exec(object parameter)
        {
            if(SelectedPattern == null)
            {
                return;
            }

            var testView = await MainView.Client.TestManager.GetAsync(SelectedPattern.Id);

            Questions = new ObservableCollection<AdminFormElementView>();

            int maxAnswer = 0;

            foreach(var question in testView.Questions)
            {
                var variants = new ObservableCollection<AdminFormElementView>();

                foreach(var answer in question.Variants)
                {
                    variants.Add(new AdminFormElementView(this, answer)
                    {
                        Name = answer.answer,
                        IsChecked = answer.IsTrue
                    });

                    if(answer.Id > maxAnswer)
                    {
                        maxAnswer = answer.Id;
                    }
                }

                Questions.Add(new AdminFormElementView(this, question.Question)
                {
                    Name = question.Question.Name,
                    QuestionText = question.Question.question,
                    HighSubElements = variants
                });
            }
            
            if(testView.Questions.Count() != 0)
            {
                IdGenerator._questionId = testView.Questions.Max(x => x.Question.Id) + 1;

                IdGenerator._answerId = maxAnswer;
            }

            TestContentVisibility = Visibility.Collapsed;

            PatternVisibility = Visibility.Collapsed;

            TestVisibility = Visibility.Visible;
        }

        private Command _AddQuestion;
        public ICommand AddQuestion
        {
            get
            {
                if (_AddQuestion != null)
                    return _AddQuestion;
                _AddQuestion = new Command(_AddQuestion_Exec);
                return _AddQuestion;
            }
        }
        private void _AddQuestion_Exec(object parameter)
        {
            var question = new QuestionView()
            {
                Id = IdGenerator.GetId(IdTypes.Question),
                Name = "",
                Pattern_id = SelectedPattern.Id,
                question = ""
            };
            
            var emptyQuestion = new AdminFormElementView(this, question)
            {
                Name = "",
                QuestionText = ""
            };

            Questions.Add(emptyQuestion);

            SelectedQuestion = emptyQuestion;

            TestContentVisibility = Visibility.Visible;
        }

        private Command _RemoveQuestion;
        public ICommand RemoveQuestion
        {
            get
            {
                if (_RemoveQuestion != null)
                    return _RemoveQuestion;
                _RemoveQuestion = new Command(_RemoveQuestion_Exec);
                return _RemoveQuestion;
            }
        }
        private void _RemoveQuestion_Exec(object parameter)
        {
            if (SelectedQuestion == null)
                return;

            ((App)Application.Current).ShowDialog(this,
                new DeleteWindow("Do you want to delete question: " + SelectedQuestion.Name));
        }

        private Command _SubmitDelete;
        public ICommand SubmitDelete
        {
            get
            {
                if (_SubmitDelete != null)
                    return _SubmitDelete;
                _SubmitDelete = new Command(_SubmitDelete_Exec);
                return _SubmitDelete;
            }
        }
        private async void _SubmitDelete_Exec(object parameter)
        {
            string respond = parameter as string;

            if (respond == "OK")
            {
                if(PatternVisibility == Visibility.Visible && 
                    TestVisibility == Visibility.Collapsed)
                {
                    var result = (CRUDResult)await MainView.Client.AdminManager.
                        DeleteAsync(SelectedPattern.Id);

                    if (!result.IsSuccess)
                    {
                        MessageBox.Show(result.Message);

                        return;
                    }

                    MainView.Patterns = new ObservableCollection<PatternView>
                        (await MainView.Client.PatternManager.GetAllAsync());

                    Patterns = MainView.Patterns;

                    SelectedPattern = null;
                }
                else
                {
                    Questions.Remove(SelectedQuestion);

                    if (Questions.Count() == 0)
                    {
                        TestContentVisibility = Visibility.Collapsed;
                    }
                    else
                    {
                        SelectedQuestion = Questions.FirstOrDefault();
                    }
                }
            }

            ((App)Application.Current).CloseDialog();
        }

        private Command _SaveTests;
        public ICommand SaveTests
        {
            get
            {
                if (_SaveTests != null)
                    return _SaveTests;
                _SaveTests = new Command(_SaveTests_Exec);
                return _SaveTests;
            }
        }
        private async void _SaveTests_Exec(object parameter)
        {
            var tests = TaskResultCreator.CreateTests(SelectedPattern, Questions); ;

            tests.IsTestActive = true;

            var result = (CRUDResult)await MainView.Client.AdminManager.UpdateAsync(tests);

            if (!result.IsSuccess)
            {
                MessageBox.Show(result.Message);

                return;
            }

            PatternVisibility = Visibility.Visible;

            PanelVisibility = Visibility.Collapsed;

            OpenPanelVisibility = Visibility.Collapsed;

            TestVisibility = Visibility.Collapsed;

            TestContentVisibility = Visibility.Collapsed;
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

            var result = new CRUDResult();

            if (Patterns.Select(x => x.Id).ToList().Contains(NewPattern.Id))
            {
                result = (CRUDResult)await MainView.Client.AdminManager.UpdateAsync(pattern);
            }
            else
            {
                result = (CRUDResult)await MainView.Client.AdminManager.PostAsync(pattern);
            }
            
            if (!result.IsSuccess)
            {
                MessageBox.Show(result.Message);

                return;
            }

            PatternVisibility = Visibility.Visible;

            PanelVisibility = Visibility.Collapsed;

            OpenPanelVisibility = Visibility.Collapsed;

            TestVisibility = Visibility.Collapsed;

            MainView.Patterns = new ObservableCollection<PatternView>
                (await MainView.Client.PatternManager.GetAllAsync());

            Patterns = MainView.Patterns;

        }

        #endregion
    }
}
