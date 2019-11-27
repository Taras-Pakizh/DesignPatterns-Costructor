﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using DesignPatterns.Client.Drawing;
using DesignPatterns.Services;
using DesignPatterns.Views;

namespace DesignPatterns.Client.View
{
    public class ApplicationView:MVVMView
    {
        private TheAClient Client { get; } = new TheAClient();

        public ApplicationView()
        {
            ReferenceTypes = new ObservableCollection<ComboBoxReferenceItem>
                (ComboBoxReferenceItem.GetValues());

            SubjectTypes = new ObservableCollection<ComboBoxSubjectItem>
                (ComboBoxSubjectItem.GetValues());

            InfoPanel = new InfoPanelView();
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
        
        public IEnumerable<ICanvasElement> ClickCanvas(Point point)
        {
            var elements = new List<ICanvasElement>();

            foreach(var item in Elements)
            {
                if (item.IsEnter(point))
                {
                    elements.Add(item);
                }
            }

            return elements;
        }

        public SubjectView CreateSubject()
        {
            if (CurrentSubjectType == null)
                return null;

            return new SubjectView()
            {
                Name = "",
                type = (SubjectType)CurrentSubjectType
            };
        }

        public void SelectElement(ICanvasElement element)
        {
            if(SelectedElement != null && SelectedElement is SubjectCanvas)
            {
                ((SubjectCanvas)SelectedElement).UnFocus();
            }

            SelectedElement = element;
            
            if(SelectedElement is SubjectCanvas)
            {
                ((SubjectCanvas)SelectedElement).Focus();

                InfoPanel.SubjectFocus((SubjectCanvas)SelectedElement);
            }
            else
            {
                InfoPanel.RefFocus((ReferenceCanvas)SelectedElement);
            }

            UpdateCanvas();
        }

        public void UpdateCanvas()
        {
            List<IObjectBinding> elements = new List<IObjectBinding>();
            
            foreach (var item in Elements)
            {
                if (item is ReferenceCanvas)
                {
                    ((ReferenceCanvas)item).Update();
                }
            }

            foreach (var list in Elements.Select(x => x.ElementsBinding))
            {
                elements.AddRange(list);
            }

            CanvasBinding = new ObservableCollection<IObjectBinding>(elements);
        }
        

        //--------------------------------------------------------------------------------------
        #region Properties

        private InfoPanelView _infoPanel;
        public InfoPanelView InfoPanel
        {
            get { return _infoPanel; }
            set
            {
                _infoPanel = value;
                OnPropertyChanged(nameof(InfoPanel));
            }
        }

        private ObservableCollection<ComboBoxElementItem> _chooseElements;
        public ObservableCollection<ComboBoxElementItem> ChooseElemets
        {
            get { return _chooseElements; }
            set
            {
                _chooseElements = value;
                OnPropertyChanged(nameof(ChooseElemets));
            }
        }

        //-------------------Implement---------------------
        public ICanvasElement SelectedElement { get; set; }
        
        public SubjectType? CurrentSubjectType { get; set; }

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

        //-------------------Implement---------------------
        public ReferenceContent ReferenceCreator { get; set; } = new ReferenceContent();

        public ReferencesType? CurrentReferenceType { get; set; }

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

        private CanvasActionType? _currentAction;
        public CanvasActionType? CurrentAction
        {
            get { return _currentAction; }
            set
            {
                _currentAction = value;
                OnPropertyChanged(nameof(CurrentAction));
            }
        }
        
        public IList<ICanvasElement> Elements = new List<ICanvasElement>();
        public void AddCanvasElement(ICanvasElement element)
        {
            Elements.Add(element);

            UpdateCanvas();

            InfoPanel = new InfoPanelView(this);
        }
        public void RemoveCanvasElement(ICanvasElement element)
        {
            Elements.Remove(element);

            var toRemove = new List<ICanvasElement>();

            foreach(var item in Elements)
            {
                if(item is ReferenceCanvas)
                {
                    var reference = (ReferenceCanvas)item;

                    if(reference.Subject.Center == ((SubjectCanvas)element).Center)
                    {
                        toRemove.Add(item);
                        toRemove.Add(reference.Arrow);
                    }
                    else if(reference.Target.Center == ((SubjectCanvas)element).Center)
                    {
                        toRemove.Add(item);
                        toRemove.Add(reference.Arrow);
                    }
                }
            }

            foreach(var item in toRemove)
            {
                Elements.Remove(item);
            }

            UpdateCanvas();

            InfoPanel = new InfoPanelView(this);
        }

        private ObservableCollection<IObjectBinding> _canvasBinding;
        public ObservableCollection<IObjectBinding> CanvasBinding
        {
            get { return _canvasBinding; }
            set
            {
                _canvasBinding = value;
                OnPropertyChanged(nameof(CanvasBinding));
            }
        }

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



        //-------------------------------------------------
        #region Commands

        private Command _CanvasClick;
        public ICommand CanvasClick
        {
            get
            {
                if (_CanvasClick != null)
                    return _CanvasClick;
                _CanvasClick = new Command(_CanvasClick_Exec);
                return _CanvasClick;
            }
        }
        private void _CanvasClick_Exec(object parameter)
        {
            Point mousePos = Mouse.GetPosition((IInputElement)parameter);

            var elements = ClickCanvas(mousePos);

            switch (CurrentAction)
            {
                case CanvasActionType.Cursor:

                    if(elements.Count() == 1)
                    {
                        SelectElement(elements.Single());
                    }
                    else if(SelectedElement is SubjectCanvas)
                    {
                        ((SubjectCanvas)SelectedElement).Move(mousePos);

                        UpdateCanvas();

                        SelectedElement = null;
                    }

                    break;
                case CanvasActionType.ObjectCreate:

                    var subject = new SubjectCanvas(CreateSubject(), mousePos);

                    AddCanvasElement(subject);

                    break;
                case CanvasActionType.ReferenceCreate:
                    
                    if (elements.Count() == 1 && elements.Single() is SubjectCanvas)
                    {
                        ReferenceCreator.Click((SubjectCanvas)elements.Single(), 
                            (ReferencesType)CurrentReferenceType);

                        if (ReferenceCreator.IsReady)
                        {
                            var bindings = ReferenceCreator.CreateReference();

                            AddCanvasElement(bindings[0]);

                            AddCanvasElement(bindings[1]);

                            ReferenceCreator.Clear();
                        }
                    }
                    else
                    {
                        ReferenceCreator.Clear();
                    }

                    break;
                default:
                    return;
            }
        }

        private Command _ElementChoose;
        public ICommand ElementChoose
        {
            get
            {
                if (_ElementChoose != null)
                    return _ElementChoose;
                _ElementChoose = new Command(_ElementChoose_Exec);
                return _ElementChoose;
            }
        }
        private void _ElementChoose_Exec(object parameter)
        {
            var combo = (ComboBox)parameter;

            if(combo.SelectedIndex == -1)
            {
                MessageBox.Show("Choose one element");
                return;
            }

            SelectElement((ICanvasElement)combo.SelectedValue);

            ((App)Application.Current).CloseDialog();
        }

        private Command _ActionChoose;
        public ICommand ActionChoose
        {
            get
            {
                if (_ActionChoose != null)
                    return _ActionChoose;
                _ActionChoose = new Command(_ActionChoose_Exec);
                return _ActionChoose;
            }
        }
        private void _ActionChoose_Exec(object parameter)
        {
            var control = (Control)parameter;

            switch (control.Name)
            {
                case "Button_Cursor":
                    CurrentAction = CanvasActionType.Cursor;
                    ReferenceCreator.Clear();
                    break;
                case "Combo_SubjectType":
                    CurrentAction = CanvasActionType.ObjectCreate;
                    var combo = (ComboBox)control;
                    CurrentSubjectType = (SubjectType)combo.SelectedValue;
                    ReferenceCreator.Clear();
                    break;
                case "Combo_ReferenceType":
                    CurrentAction = CanvasActionType.ReferenceCreate;
                    combo = (ComboBox)control;
                    CurrentReferenceType = (ReferencesType)combo.SelectedValue;
                    break;
                default:
                    MessageBox.Show("Unknown control name");
                    return;
            }
        }
        
        #endregion
    }
}
