using DesignPatterns.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesignPatterns.Client.Controllers
{
    /// <summary>
    /// Логика взаимодействия для Parameters.xaml
    /// </summary>
    public partial class Parameters : UserControl
    {
        public Parameters()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty IdProperty;
        public int Id
        {
            get { return (int)GetValue(IdProperty); }
            set { SetValue(IdProperty, value); }
        }

        public static readonly DependencyProperty ParametersProperty;
        public IEnumerable<MethodParameterView> AllParameters
        {
            get { return (IEnumerable<MethodParameterView>)GetValue(ParametersProperty); }
            set { SetValue(ParametersProperty, value); }
        }

        public static readonly DependencyProperty SelectedParameterProperty;
        public MethodParameterView SelectedParameter
        {
            get { return (MethodParameterView)GetValue(SelectedParameterProperty); }
            set { SetValue(SelectedParameterProperty, value); }
        }

        public static readonly DependencyProperty TypesProperty;
        public IEnumerable<SubjectView> Types
        {
            get { return (IEnumerable<SubjectView>)(GetValue(TypesProperty)); }
            set { SetValue(TypesProperty, value); }
        }

        public static readonly DependencyProperty SelectedTypeProperty;
        public SubjectView SelectedType
        {
            get { return (SubjectView)GetValue(SelectedTypeProperty); }
            set { SetValue(SelectedTypeProperty, value); }
        }

        static Parameters()
        {
            ParametersProperty = DependencyProperty.Register
                (nameof(AllParameters), typeof(IEnumerable<MethodParameterView>), typeof(Parameters));

            SelectedParameterProperty = DependencyProperty.Register
                (nameof(SelectedParameter), typeof(MethodParameterView), typeof(Parameters));

            TypesProperty = DependencyProperty.Register
                (nameof(Types), typeof(IEnumerable<SubjectView>), typeof(Parameters));

            SelectedTypeProperty = DependencyProperty.Register
                (nameof(SelectedType), typeof(SubjectView), typeof(Parameters));

            IdProperty = DependencyProperty.Register
                (nameof(Id), typeof(int), typeof(Parameters));
        }
    }
}
