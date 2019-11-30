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
    /// Логика взаимодействия для ObjectItem.xaml
    /// </summary>
    public partial class ObjectItem : UserControl
    {
        public ObjectItem()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty IdProperty;
        public int Id
        {
            get { return (int)GetValue(IdProperty); }
            set { SetValue(IdProperty, value); }
        }

        public static readonly DependencyProperty PropertiesProperty;
        public IEnumerable<SubjectPropertyView> Properties
        {
            get { return (IEnumerable<SubjectPropertyView>)GetValue(PropertiesProperty); }
            set { SetValue(PropertiesProperty, value); }
        }

        public static readonly DependencyProperty SelectedPropertyProperty;
        public SubjectPropertyView SelectedProperty
        {
            get { return (SubjectPropertyView)GetValue(SelectedPropertyProperty); }
            set { SetValue(SelectedPropertyProperty, value); }
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

        static ObjectItem()
        {
            PropertiesProperty = DependencyProperty.Register
                (nameof(Properties), typeof(IEnumerable<SubjectPropertyView>), typeof(ObjectItem));

            SelectedPropertyProperty = DependencyProperty.Register
                (nameof(SelectedProperty), typeof(SubjectPropertyView), typeof(ObjectItem));

            TypesProperty = DependencyProperty.Register
                (nameof(Types), typeof(IEnumerable<SubjectView>), typeof(ObjectItem));

            SelectedTypeProperty = DependencyProperty.Register
                (nameof(SelectedType), typeof(SubjectView), typeof(ObjectItem));

            IdProperty = DependencyProperty.Register
                (nameof(Id), typeof(int), typeof(ObjectItem));
        }


    }
}
