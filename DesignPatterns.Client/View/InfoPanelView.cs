using DesignPatterns.Client.Drawing;
using DesignPatterns.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DesignPatterns.Client.View
{
    public class InfoPanelView : MVVMView
    {
        private IEnumerable<SubjectCanvas> _SubjectCanvases;

        private IEnumerable<ReferenceCanvas> _RefCanvases;
        
        private ApplicationView _MainView;
        
        public ICanvasElement FocusedElement;

        public InfoPanelView()
        {
            UnFocus();
        }

        public InfoPanelView(ApplicationView view)
        {
            _SubjectCanvases = 
                (view.Elements.Where(x => x is SubjectCanvas)
                .Select(y=>(SubjectCanvas)y) .ToList());

            _RefCanvases =
                (view.Elements.Where(x => x is ReferenceCanvas)
                .Select(y=>(ReferenceCanvas)y).ToList());
            
            AllSubjectViews = new ObservableCollection<SubjectView>(view.LoadedDiagram.Subjects
                .Where(x=>x.type == SubjectType.Class || x.type == SubjectType.Abstract_Class || x.type == SubjectType.Interface));

            SubjectViews = new ObservableCollection<SubjectView>
                (_SubjectCanvases.Select(x => x.View).ToList());
            
            _MainView = view;

            ReferenceTypes = _MainView.ReferenceTypes;

            SubjectTypes = _MainView.SubjectTypes;

            UnFocus();
        }

        public void UnFocus()
        {
            SubjectVisibility = Visibility.Collapsed;

            RefVisibility = Visibility.Collapsed;
        }

        public void SubjectFocus(SubjectCanvas element)
        {
            FocusedElement = element;

            SubjectVisibility = Visibility.Visible;

            RefVisibility = Visibility.Collapsed;


            SelectedSubject = ((SubjectCanvas)FocusedElement).View;

            SelectedSubjectType = SubjectTypes.Single(x => x.Type == SelectedSubject.type);

            AllSubjectViews = new ObservableCollection<SubjectView>(_MainView.LoadedDiagram.Subjects
                .Where(x => x.type == SelectedSubject.type));
        }

        public void RefFocus(ReferenceCanvas element)
        {
            FocusedElement = element;

            SubjectVisibility = Visibility.Collapsed;

            RefVisibility = Visibility.Visible;
            
            //------------Implement-------------
        }

        public void Update()
        {
            if(FocusedElement is SubjectCanvas)
            {
                SelectedSubject = ((SubjectCanvas)FocusedElement).View;

                SelectedSubjectType = SubjectTypes.Single(x => x.Type == SelectedSubject.type);

                AllSubjectViews = new ObservableCollection<SubjectView>(_MainView.LoadedDiagram.Subjects
                    .Where(x => x.type == SelectedSubject.type).ToList());
            }
            
            _MainView.UpdateCanvas();
        }


        #region PropertiesBinding

        public Visibility _SubjecyVisibility;
        public Visibility SubjectVisibility
        {
            get { return _SubjecyVisibility; }
            set
            {
                _SubjecyVisibility = value;
                OnPropertyChanged(nameof(SubjectVisibility));
            }
        }

        public Visibility _RefVisibility;
        public Visibility RefVisibility
        {
            get { return _RefVisibility; }
            set
            {
                _RefVisibility = value;
                OnPropertyChanged(nameof(RefVisibility));
            }
        }

        private SubjectView _SelectedSubject;
        public SubjectView SelectedSubject
        {
            get { return _SelectedSubject; }
            set
            {
                _SelectedSubject = value;
                if(value == null)
                {
                    _SelectedSubject = new SubjectView()
                    {
                        Name = "",
                        type = SelectedSubjectType.Type
                    };
                }
                OnPropertyChanged(nameof(SelectedSubject));
            }
        }

        private SubjectView _SelectedStart;
        public SubjectView SelectedStart
        {
            get { return _SelectedStart; }
            set
            {
                _SelectedStart = value;
                OnPropertyChanged(nameof(SelectedStart));
            }
        }

        private SubjectView _SelectedEnd;
        public SubjectView SelectedEnd
        {
            get { return _SelectedEnd; }
            set
            {
                _SelectedEnd = value;
                OnPropertyChanged(nameof(SelectedEnd));
            }
        }

        private SubjectReferenceView _SelectedRef;
        public SubjectReferenceView SelectedRef
        {
            get { return _SelectedRef; }
            set
            {
                _SelectedRef = value;
                OnPropertyChanged(nameof(SelectedRef));
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

        private ReferencesType? _SelectedRefType;
        public ReferencesType? SelectedRefType
        {
            get { return _SelectedRefType; }
            set
            {
                _SelectedRefType = value;
                OnPropertyChanged(nameof(SelectedRefType));
            }
        }

        private ObservableCollection<SubjectView> _allSubjectViews;
        public ObservableCollection<SubjectView> AllSubjectViews
        {
            get { return _allSubjectViews; }
            set
            {
                _allSubjectViews = value;
                OnPropertyChanged(nameof(AllSubjectViews));
            }
        }

        private ObservableCollection<SubjectView> _SubjectViews;
        public ObservableCollection<SubjectView> SubjectViews
        {
            get { return _SubjectViews; }
            set
            {
                _SubjectViews = value;
                OnPropertyChanged(nameof(SubjectViews));
            }
        }
        
        private ObservableCollection<ComboBoxSubjectItem> _subjectTypes;
        public ObservableCollection<ComboBoxSubjectItem> SubjectTypes
        {
            get { return _subjectTypes; }
            set
            {
                _subjectTypes = value;
                OnPropertyChanged(nameof(SubjectTypes));
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

        #endregion


        #region Commands

        private Command _SelectSubject;
        public ICommand SelectSubject
        {
            get
            {
                if (_SelectSubject != null)
                    return _SelectSubject;
                _SelectSubject = new Command(_SelectSubject_Exec);
                return _SelectSubject;
            }
        }
        private void _SelectSubject_Exec(object parameter)
        {
            var element = (SubjectCanvas)FocusedElement;

            element.View = SelectedSubject;

            element.Update();
            
            Update();
        }

        private Command _SelectSubjectType;
        public ICommand SelectSubjectType
        {
            get
            {
                if (_SelectSubjectType != null)
                    return _SelectSubjectType;
                _SelectSubjectType = new Command(_SelectSubjectType_Exec);
                return _SelectSubjectType;
            }
        }
        private void _SelectSubjectType_Exec(object parameter)
        {
            var element = (SubjectCanvas)FocusedElement;

            element.View = new SubjectView()
            {
                Name = "",
                type = SelectedSubjectType.Type
            };
            
            element.Update();
            
            Update();
        }

        private Command _SelectRefType;
        public ICommand SelectRefType
        {
            get
            {
                if (_SelectRefType != null)
                    return _SelectRefType;
                _SelectRefType = new Command(_SelectRef_Exec);
                return _SelectRefType;
            }
        }
        private void _SelectRef_Exec(object parameter)
        {
            
        }

        private Command _SelectRefStart;
        public ICommand SelectRefStart
        {
            get
            {
                if (_SelectRefStart != null)
                    return _SelectRefStart;
                _SelectRefStart = new Command(_SelectRefStart_Exec);
                return _SelectRefStart;
            }
        }
        private void _SelectRefStart_Exec(object parameter)
        {
            
        }

        private Command _SelectRefEnd;
        public ICommand SelectRefEnd
        {
            get
            {
                if (_SelectRefEnd != null)
                    return _SelectRefEnd;
                _SelectRefEnd = new Command(_SelectRefEnd_Exec);
                return _SelectRefEnd;
            }
        }
        private void _SelectRefEnd_Exec(object parameter)
        {
            
        }

        private Command _Open;
        public ICommand Open
        {
            get
            {
                if (_Open != null)
                    return _Open;
                _Open = new Command(_Open_Exec);
                return _Open;
            }
        }
        private void _Open_Exec(object parameter)
        {
            
        }

        private Command _DeleteSubject;
        public ICommand DeleteSubject
        {
            get
            {
                if (_DeleteSubject != null)
                    return _DeleteSubject;
                _DeleteSubject = new Command(_DeleteSubject_Exec);
                return _DeleteSubject;
            }
        }
        private void _DeleteSubject_Exec(object parameter)
        {
            _MainView.RemoveCanvasElement(FocusedElement);
        }

        private Command _DeleteRef;
        public ICommand DeleteRef
        {
            get
            {
                if (_DeleteRef != null)
                    return _DeleteRef;
                _DeleteRef = new Command(_DeleteRef_Exec);
                return _DeleteRef;
            }
        }
        private void _DeleteRef_Exec(object parameter)
        {
            
        }

        #endregion
    }
}
