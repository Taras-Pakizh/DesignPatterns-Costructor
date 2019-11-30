using DesignPatterns.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DesignPatterns.Client.View
{
    public class FormElement : MVVMView
    {
        public IViewBase SelectedElement { get; set; }
        
        public IEnumerable<IViewBase> Elements { get; set; }

        public IViewBase SelectedType { get; set; }

        public IEnumerable<IViewBase> Types { get; set; }

        private ObservableCollection<FormElement> _SubElements;
        public ObservableCollection<FormElement> SubElements
        {
            get { return _SubElements; }
            set
            {
                _SubElements = value;
                OnPropertyChanged(nameof(SubElements));
            }
        }

        public int Id { get; set; }



        private ObjectFormView _View;

        public FormElement(ObjectFormView view)
        {
            _View = view;

            Id = Counter;

            ++Counter;
        }

        private static int Counter = 0;



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
        private void _AddParameter_Exec(object parameter)
        {
            int Id = (int)parameter;

            var emptyParameter = new FormElement(_View)
            {
                Elements = _View.MainView.LoadedDiagram.MethodParameters,
                Types = _View.MainView.LoadedDiagram.Subjects
            };

            SubElements.Add(emptyParameter);
        }

        private Command _RemoveProperty;
        public ICommand RemoveProperty
        {
            get
            {
                if (_RemoveProperty != null)
                    return _RemoveProperty;
                _RemoveProperty = new Command(_RemoveProperty_Exec);
                return _RemoveProperty;
            }
        }
        private void _RemoveProperty_Exec(object parameter)
        {
            _View.PropertyElements.Remove(this);
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
        private void _RemoveMethod_Exec(object parameter)
        {
            _View.MethodElements.Remove(this);
        }

        private Command _RemoveParameter;
        public ICommand RemoveParameter
        {
            get
            {
                if (_RemoveParameter != null)
                    return _RemoveParameter;
                _RemoveParameter = new Command(_RemoveParameter_Exec);
                return _RemoveParameter;
            }
        }
        private void _RemoveParameter_Exec(object parameter)
        {
            _View.MethodElements.
                Single(x => x.SubElements.Contains(this)).
                SubElements.Remove(this);            
        }
    }
}
