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
    public class AdminFormElementView : MVVMView 
    {
        private bool _IsChecked;
        public bool IsChecked
        {
            get { return _IsChecked; }
            set
            {
                _IsChecked = value;
                OnPropertyChanged(nameof(IsChecked));
            }
        }

        public string QuestionText { get; set; }

        public string SubjectName
        {
            get
            {
                if(Context is SubjectView)
                {
                    return ((SubjectView)Context).Name;
                }
                return "Incorrect type";
            }
            set
            {
                if (Context is SubjectView)
                {
                    ((SubjectView)Context).Name = value;
                }
            }
        }

        private string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private ComboBoxSubjectItem _SelectedSubjectType;
        public ComboBoxSubjectItem SelectedSubjectType
        {
            get { return _SelectedSubjectType; }
            set
            {
                _SelectedSubjectType = value;
                OnPropertyChanged(nameof(SelectedSubjectType));
            }
        }

        private ObservableCollection<ComboBoxSubjectItem> _SubjectTypes;
        public ObservableCollection<ComboBoxSubjectItem> SubjectTypes
        {
            get { return _SubjectTypes; }
            set
            {
                _SubjectTypes = value;
                OnPropertyChanged(nameof(SubjectTypes));
            }
        }

        private ComboBoxReferenceItem _SelectedReferenceType;
        public ComboBoxReferenceItem SelectedReferenceType
        {
            get { return _SelectedReferenceType; }
            set
            {
                _SelectedReferenceType = value;
                OnPropertyChanged(nameof(SelectedReferenceType));
            }
        }

        private ObservableCollection<ComboBoxReferenceItem> _referenceTypes;
        public ObservableCollection<ComboBoxReferenceItem> ReferenceTypes
        {
            get { return _referenceTypes; }
            set
            {
                _referenceTypes = value;
                OnPropertyChanged(nameof(ReferenceTypes));
            }
        }

        public IViewBase Start { get; set; }

        public IViewBase End { get; set; }

        public IViewBase SelectedElement { get; set; }

        public IEnumerable<IViewBase> AllElements { get; set; }
        
        
        public IEnumerable<IViewBase> CreatedElements { get; set; }

        private ObservableCollection<AdminFormElementView> _HighSubElements;
        public ObservableCollection<AdminFormElementView> HighSubElements
        {
            get { return _HighSubElements; }
            set
            {
                _HighSubElements = value;
                OnPropertyChanged(nameof(HighSubElements));
            }
        }

        private ObservableCollection<AdminFormElementView> _LowSubElements;
        public ObservableCollection<AdminFormElementView> LowSubElements
        {
            get { return _LowSubElements; }
            set
            {
                _LowSubElements = value;
                OnPropertyChanged(nameof(LowSubElements));
            }
        }


        


        public AdminView _View;

        public IViewBase Context { get; set; }

        public AdminFormElementView(AdminView view, IViewBase element)
        {
            _View = view;

            Context = element;

            HighSubElements = new ObservableCollection<AdminFormElementView>();

            LowSubElements = new ObservableCollection<AdminFormElementView>();
        }



        private Command _RemoveSubject;
        public ICommand RemoveSubject
        {
            get
            {
                if (_RemoveSubject != null)
                    return _RemoveSubject;
                _RemoveSubject = new Command(_RemoveSubject_Exec);
                return _RemoveSubject;
            }
        }
        private void _RemoveSubject_Exec(object parameter)
        {
            _View.CreatedTypes.Remove((SubjectView)Context);

            OnPropertyChanged(nameof(CreatedElements));

            var res = _View.AllTypes.Remove((SubjectView)Context);

            OnPropertyChanged(nameof(AllElements));

            _View.SubjectElements.Remove(this);
        }

        private Command _RemoveReference;
        public ICommand RemoveReference
        {
            get
            {
                if (_RemoveReference != null)
                    return _RemoveReference;
                _RemoveReference = new Command(_RemoveReference_Exec);
                return _RemoveReference;
            }
        }
        private void _RemoveReference_Exec(object parameter)
        {
            _View.ReferenceElements.Remove(this);
        }

        private Command _OpenSubject;
        public ICommand OpenSubject
        {
            get
            {
                if (_OpenSubject != null)
                    return _OpenSubject;
                _OpenSubject = new Command(_OpenSubject_Exec);
                return _OpenSubject;
            }
        }
        private void _OpenSubject_Exec(object parameter)
        {
            _View.OpenedSubject = this;

            _View.OpenPanelVisibility = Visibility.Visible;

            _View.PanelVisibility = Visibility.Collapsed;
        }
        
        private Command _AddProperty;
        public ICommand AddProperty
        {
            get
            {
                if (_AddProperty != null)
                    return _AddProperty;
                _AddProperty = new Command(_AddProperty_Exec);
                return _AddProperty;
            }
        }
        public void _AddProperty_Exec(object parameter)
        {
            var property = new SubjectPropertyView()
            {
                Id = IdGenerator.GetId(IdTypes.Property),
                Name = "",
                Subject_Id = (int)Context.GetId()
            };
            
            var emptyProperty = new AdminFormElementView(_View, property)
            {
                AllElements = _View.AllTypes
            };

            HighSubElements.Add(emptyProperty);
        }

        private Command _AddMethod;
        public ICommand AddMethod
        {
            get
            {
                if (_AddMethod != null)
                    return _AddMethod;
                _AddMethod = new Command(_AddMethod_Exec);
                return _AddMethod;
            }
        }
        public void _AddMethod_Exec(object parameter)
        {
            var method = new SubjectMethodView()
            {
                Id = IdGenerator.GetId(IdTypes.Method),
                Name = "",
                Subject_Id = (int)Context.GetId(),
                AccessType = AccessType.Public
            };

            var emptyMethod = new AdminFormElementView(_View, method)
            {
                AllElements = _View.AllTypes
            };

            LowSubElements.Add(emptyMethod);
        }

        private Command _AddParameter;
        public ICommand AddParameter
        {
            get
            {
                if (_AddParameter != null)
                    return _AddParameter;
                _AddParameter = new Command(_AddParameter_Exec);
                return _AddParameter;
            }
        }
        public void _AddParameter_Exec(object parameter)
        {
            var Parameter = new MethodParameterView()
            {
                Id = IdGenerator.GetId(IdTypes.Parameter),
                Name = "",
                method_id = (int)Context.GetId()
            };

            var emptyParameter = new AdminFormElementView(_View, Parameter)
            {
                AllElements = _View.AllTypes
            };

            HighSubElements.Add(emptyParameter);
        }

        private Command _RemovePropOrParam;
        public ICommand RemovePropOrParam
        {
            get
            {
                if (_RemovePropOrParam != null)
                    return _RemovePropOrParam;
                _RemovePropOrParam = new Command(_RemovePropOrParam_Exec);
                return _RemovePropOrParam;
            }
        }
        public void _RemovePropOrParam_Exec(object parameter)
        {
            if(Context is SubjectPropertyView)
            {
                    _View.SubjectElements.
                        Single(x => x.HighSubElements.Contains(this)).
                        HighSubElements.Remove(this);
            }
            else if(Context is MethodParameterView)
            {
                _View.SubjectElements.
                    Single(x => x.LowSubElements.
                    Any(y => y.HighSubElements.Contains(this))).
                    LowSubElements.Single(z=>z.HighSubElements.Contains(this)).
                    HighSubElements.Remove(this);
            }
            else
            {
                throw new Exception("You broke removepropOrParam. I have no idea how");
            }
        }

        private Command _RemoveMethod;
        public ICommand RemoveMethod
        {
            get
            {
                if (_RemoveMethod != null)
                    return _RemoveMethod;
                _RemoveMethod = new Command(_RemoveMethod_Exec);
                return _RemoveMethod;
            }
        }
        public void _RemoveMethod_Exec(object parameter)
        {
            _View.SubjectElements.
                Single(x => x.LowSubElements.Contains(this)).
                LowSubElements.Remove(this);
        }
        
        private Command _Close;
        public ICommand Close
        {
            get
            {
                if (_Close != null)
                    return _Close;
                _Close = new Command(_Close_Exec);
                return _Close;
            }
        }
        public void _Close_Exec(object parameter)
        {
            _View.OpenPanelVisibility = Visibility.Collapsed;

            _View.PanelVisibility = Visibility.Visible;
        }

        private Command _AddAnswer;
        public ICommand AddAnswer
        {
            get
            {
                if (_AddAnswer != null)
                    return _AddAnswer;
                _AddAnswer = new Command(_AddAnswer_Exec);
                return _AddAnswer;
            }
        }
        private void _AddAnswer_Exec(object parameter)
        {
            var answer = new AnswerView()
            {
                Id = IdGenerator.GetId(IdTypes.Answer),
                answer = "",
                IsTrue = false,
                question_Id = (int)Context.GetId()
            };

            var emptyAnswer = new AdminFormElementView(_View, answer)
            {
                Name = "",
                IsChecked = false
            };

            HighSubElements.Add(emptyAnswer);
        }

        private Command _RemoveAnswer;
        public ICommand RemoveAnswer
        {
            get
            {
                if (_RemoveAnswer != null)
                    return _RemoveAnswer;
                _RemoveAnswer = new Command(_RemoveAnswer_Exec);
                return _RemoveAnswer;
            }
        }
        private void _RemoveAnswer_Exec(object parameter)
        {
            _View.SelectedQuestion.HighSubElements.Remove(this);
        }
    }
}
