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
using DesignPatterns.Client.View;

namespace DesignPatterns.Client.Controllers
{
    /// <summary>
    /// Логика взаимодействия для EditElementController.xaml
    /// </summary>
    public partial class EditElementController : UserControl
    {
        public EditElementController()
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
        public IEnumerable<ComboBoxSubjectItem> Types
        {
            get { return (IEnumerable<ComboBoxSubjectItem>)(GetValue(TypesProperty)); }
            set { SetValue(TypesProperty, value); }
        }

        public static readonly DependencyProperty SelectedTypeProperty;
        public ComboBoxSubjectItem SelectedType
        {
            get { return (ComboBoxSubjectItem)GetValue(SelectedTypeProperty); }
            set { SetValue(SelectedTypeProperty, value); }
        }

        static EditElementController()
        {
            ElementNameProperty = DependencyProperty.Register
                (nameof(ElementName), typeof(string), typeof(EditElementController));

            TypesProperty = DependencyProperty.Register
                (nameof(Types), typeof(IEnumerable<ComboBoxSubjectItem>), typeof(EditElementController));

            SelectedTypeProperty = DependencyProperty.Register
                (nameof(SelectedType), typeof(ComboBoxSubjectItem), typeof(EditElementController));
        }
    }
}
