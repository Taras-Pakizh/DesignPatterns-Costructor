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
    /// Логика взаимодействия для EditPropertyController.xaml
    /// </summary>
    public partial class EditPropertyController : UserControl
    {
        public EditPropertyController()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ElementNameProperty;
        public string ElementName
        {
            get { return (string)GetValue(ElementNameProperty); }
            set { SetValue(ElementNameProperty, value); }
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

        static EditPropertyController()
        {
            ElementNameProperty = DependencyProperty.Register
                (nameof(ElementName), typeof(string), typeof(EditPropertyController));

            TypesProperty = DependencyProperty.Register
                (nameof(Types), typeof(IEnumerable<SubjectView>), typeof(EditPropertyController));

            SelectedTypeProperty = DependencyProperty.Register
                (nameof(SelectedType), typeof(SubjectView), typeof(EditPropertyController));
        }
    }
}
