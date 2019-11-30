using DesignPatterns.Client.View;
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
    /// Логика взаимодействия для ObjectMethod.xaml
    /// </summary>
    public partial class ObjectMethod : UserControl
    {
        public ObjectMethod()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty IdProperty;
        public int Id
        {
            get { return (int)GetValue(IdProperty); }
            set { SetValue(IdProperty, value); }
        }

        public static readonly DependencyProperty MethodsProperty;
        public IEnumerable<SubjectMethodView> Methods
        {
            get { return (IEnumerable<SubjectMethodView>)GetValue(MethodsProperty); }
            set { SetValue(MethodsProperty, value); }
        }

        public static readonly DependencyProperty SelectedMethodProperty;
        public SubjectMethodView SelectedMethod
        {
            get { return (SubjectMethodView)GetValue(SelectedMethodProperty); }
            set { SetValue(SelectedMethodProperty, value); }
        }

        public static readonly DependencyProperty ReturnTypesProperty;
        public IEnumerable<SubjectView> ReturnTypes
        {
            get { return (IEnumerable<SubjectView>)(GetValue(ReturnTypesProperty)); }
            set { SetValue(ReturnTypesProperty, value); }
        }

        public static readonly DependencyProperty SelectedReturnTypeProperty;
        public SubjectView SelectedReturnType
        {
            get { return (SubjectView)GetValue(SelectedReturnTypeProperty); }
            set { SetValue(SelectedReturnTypeProperty, value); }
        }

        public static readonly DependencyProperty ParametersProperty;
        public IEnumerable<FormElement> Parameters
        {
            get { return (IEnumerable<FormElement>)(GetValue(ParametersProperty)); }
            set { SetValue(ParametersProperty, value); }
        }

        static ObjectMethod()
        {
            MethodsProperty = DependencyProperty.Register
                (nameof(Methods), typeof(IEnumerable<SubjectMethodView>), typeof(ObjectMethod));

            SelectedMethodProperty = DependencyProperty.Register
                (nameof(SelectedMethod), typeof(SubjectMethodView), typeof(ObjectMethod));

            ReturnTypesProperty = DependencyProperty.Register
                (nameof(ReturnTypes), typeof(IEnumerable<SubjectView>), typeof(ObjectMethod));

            SelectedReturnTypeProperty = DependencyProperty.Register
                (nameof(SelectedReturnType), typeof(SubjectView), typeof(ObjectMethod));

            ParametersProperty = DependencyProperty.Register
                (nameof(Parameters), typeof(IEnumerable<FormElement>), typeof(ObjectMethod));

            IdProperty = DependencyProperty.Register
                (nameof(Id), typeof(int), typeof(ObjectMethod));
        }
    }
}
