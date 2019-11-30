using DesignPatterns.Client.Drawing;
using DesignPatterns.Client.Windows;
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
        
        private ApplicationView _MainView;
        
        public ICanvasElement FocusedElement;

        public bool IsCommandEnable { get; set; } = true;
        

        public InfoPanelView(ApplicationView view)
        {
            _MainView = view;

            ReferenceTypes = _MainView.ReferenceTypes;

            SubjectTypes = _MainView.SubjectTypes;

            IsEnable = true;

            UnFocus();
        }



        public void UnFocus()
        {
            SubjectVisibility = Visibility.Collapsed;

            RefVisibility = Visibility.Collapsed;

            FocusedElement = null;
        }

        public void SubjectFocus(SubjectCanvas element)
        {
            FocusedElement = element;
            
            SubjectVisibility = Visibility.Visible;

            RefVisibility = Visibility.Collapsed;

            Update();
        }

        public void RefFocus(ReferenceCanvas element)
        {
            FocusedElement = element;
            
            SubjectVisibility = Visibility.Collapsed;

            RefVisibility = Visibility.Visible;

            Update();
        }

        public void Update()
        {
            IsCommandEnable = false;

            if(FocusedElement is SubjectCanvas)
            {
                SelectedSubject = ((SubjectCanvas)FocusedElement).View;

                SelectedSubjectType = SubjectTypes.Single(x => x.Type == SelectedSubject.type);

                AllSubjectViews = new ObservableCollection<SubjectView>(_MainView.LoadedDiagram.Subjects
                    .Where(x => x.type == SelectedSubject.type).ToList());

                if(SelectedSubject?.Name == "")
                {
                    SelectedSubject = null;
                }
            }
            else if (FocusedElement is ReferenceCanvas)
            {
                var element = (ReferenceCanvas)FocusedElement;

                _SubjectCanvases = (_MainView.Elements.Where(x => x is SubjectCanvas)
                    .Select(y => (SubjectCanvas)y).ToList());

                SubjectViews = new ObservableCollection<SubjectView>
                    (_SubjectCanvases.Select(x => x.View).ToList());

                SelectedRefType = ReferenceTypes
                    .Where(x => x.Type == element.View.type).Single();

                SelectedStart = element.Subject.View;

                SelectedEnd = element.Target.View;
            }

            IsCommandEnable = true;

            _MainView.UpdateCanvas();
        }



        //----------------------------------------------------------------------------------
        #region PropertiesBinding

        private bool _InfoPanelEnable;
        public bool IsEnable
        {
            get { return _InfoPanelEnable; }
            set
            {
                _InfoPanelEnable = value;
                OnPropertyChanged(nameof(IsEnable));
            }
        }

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

        private ComboBoxReferenceItem _SelectedRefType;
        public ComboBoxReferenceItem SelectedRefType
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




        //-----------------------------------------------------------------------------------
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
            if (!IsCommandEnable)
                return;

            var element = FocusedElement as SubjectCanvas;

            if (element == null)
                return;

            IsCommandEnable = false;

            element.View = SelectedSubject;

            element.Update();
            
            Update();

            IsCommandEnable = true;
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
            if (!IsCommandEnable)
                return;

            var element = FocusedElement as SubjectCanvas;

            if (element == null)
                return;

            IsCommandEnable = false;
            
            element.View = new SubjectView()
            {
                Name = "",
                type = SelectedSubjectType.Type
            };
            
            element.Update();
            
            Update();

            IsCommandEnable = true;
        }

        private Command _SelectRefType;
        public ICommand SelectRefType
        {
            get
            {
                if (_SelectRefType != null)
                    return _SelectRefType;
                _SelectRefType = new Command(_SelectRefType_Exec);
                return _SelectRefType;
            }
        }
        private void _SelectRefType_Exec(object parameter)
        {
            if (!IsCommandEnable)
                return;

            var element = FocusedElement as ReferenceCanvas;

            if (element == null)
                return;

            IsCommandEnable = false;

            element.View.type = SelectedRefType.Type;

            element.Update();

            Update();

            IsCommandEnable = true;
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
            if (!IsCommandEnable)
                return;

            var element = FocusedElement as ReferenceCanvas;

            if (element == null)
                return;

            IsCommandEnable = false;

            var newSubject = _SubjectCanvases.Where(x => x.View == SelectedStart).Single();

            element.View.subject_Id = newSubject.View.Id;

            element.Update(element.View, newSubject, element.Target);
            
            Update();

            IsCommandEnable = true;
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
            if (!IsCommandEnable)
                return;

            var element = FocusedElement as ReferenceCanvas;

            if (element == null)
                return;

            IsCommandEnable = false;

            var newTarget = _SubjectCanvases.Where(x => x.View == SelectedEnd).Single();

            element.View.target_Id = newTarget.View.Id;

            element.Update(element.View, element.Subject, newTarget);

            Update();

            IsCommandEnable = true;
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
            _MainView.CanvasVisibility = Visibility.Collapsed;

            _MainView.FormVisibility = Visibility.Visible;

            IsEnable = false;
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
            var element = FocusedElement as SubjectCanvas;

            if (element == null)
                return;

            string message = "Do you want to delete " + 
                element.View.type.ToString() + " : " + element.View.Name;
            
            ((App)Application.Current).ShowDialog(this, 
                new DeleteWindow(message));
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
            var element = FocusedElement as ReferenceCanvas;

            if (element == null)
                return;

            string message = "Do you want to delete dependency\n" +
                element.Subject.View.Name + " and "
                    + element.Target.View.Name + " - type: "
                    + element.View.type.ToString();

            ((App)Application.Current).ShowDialog(this,
                new DeleteWindow(message));
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
        private void _SubmitDelete_Exec(object parameter)
        {
            string respond = parameter as string;

            if(respond == "OK")
            {
                _MainView.RemoveCanvasElement(FocusedElement);

                UnFocus();

                ((App)Application.Current).CloseDialog();
            }
        }

        #endregion
    }
}
