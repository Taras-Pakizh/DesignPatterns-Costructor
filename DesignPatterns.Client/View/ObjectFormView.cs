using DesignPatterns.Client.Drawing;
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
    public class ObjectFormView : MVVMView
    {
        public ApplicationView MainView { get; set; }

        public SubjectCanvas Subject { get; set; }

        public ObjectFormView(ApplicationView view, SubjectCanvas element)
        {
            MainView = view;

            Subject = element;

            PropertyElements = new ObservableCollection<FormElement>();

            MethodElements = new ObservableCollection<FormElement>();
        }
        


        //-----------------------------------------------------------------------------------
        #region PropertiesBinding

        private ObservableCollection<FormElement> _PropertyElements;
        public ObservableCollection<FormElement> PropertyElements
        {
            get { return _PropertyElements; }
            set
            {
                _PropertyElements = value;
                OnPropertyChanged(nameof(PropertyElements));
            }
        }

        private ObservableCollection<FormElement> _MethodElements;
        public ObservableCollection<FormElement> MethodElements
        {
            get { return _MethodElements; }
            set
            {
                _MethodElements = value;
                OnPropertyChanged(nameof(MethodElements));
            }
        }
        
        #endregion




        //------------------------------------------------------------------------------------
        #region Commands

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
        private void _Close_Exec(object parameter)
        {
            MainView.FormVisibility = Visibility.Collapsed;

            MainView.CanvasVisibility = Visibility.Visible;

            MainView.InfoPanel.IsEnable = true;
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
        private void _AddProperty_Exec(object parameter)
        {
            var emptyElement = new FormElement(this)
            {
                Elements = MainView.LoadedDiagram.SubjectProperties,
                Types = MainView.LoadedDiagram.Subjects,
            };

            PropertyElements.Add(emptyElement);
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
        private void _AddMethod_Exec(object parameter)
        {
            var emptyMethod = new FormElement(this)
            {
                Elements = MainView.LoadedDiagram.SubjectMethods,
                Types = MainView.LoadedDiagram.Subjects,
                SubElements = new ObservableCollection<FormElement>()
            };

            MethodElements.Add(emptyMethod);
        }
        
        #endregion
    }
}
