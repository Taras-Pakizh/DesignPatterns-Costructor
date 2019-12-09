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
    /// Логика взаимодействия для EditReferenceController.xaml
    /// </summary>
    public partial class EditReferenceController : UserControl
    {
        public EditReferenceController()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty SubjectsProperty;
        public IEnumerable<SubjectView> Subjects
        {
            get { return (IEnumerable<SubjectView>)(GetValue(SubjectsProperty)); }
            set { SetValue(SubjectsProperty, value); }
        }

        public static readonly DependencyProperty StartProperty;
        public SubjectView Start
        {
            get { return (SubjectView)(GetValue(StartProperty)); }
            set { SetValue(StartProperty, value); }
        }

        public static readonly DependencyProperty EndProperty;
        public SubjectView End
        {
            get { return (SubjectView)(GetValue(EndProperty)); }
            set { SetValue(EndProperty, value); }
        }
        
        public static readonly DependencyProperty TypesProperty;
        public IEnumerable<ComboBoxReferenceItem> Types
        {
            get { return (IEnumerable<ComboBoxReferenceItem>)(GetValue(TypesProperty)); }
            set { SetValue(TypesProperty, value); }
        }

        public static readonly DependencyProperty SelectedTypeProperty;
        public ComboBoxReferenceItem SelectedType
        {
            get { return (ComboBoxReferenceItem)GetValue(SelectedTypeProperty); }
            set { SetValue(SelectedTypeProperty, value); }
        }

        static EditReferenceController()
        {
            SubjectsProperty = DependencyProperty.Register
                (nameof(Subjects), typeof(IEnumerable<SubjectView>), typeof(EditReferenceController));

            StartProperty = DependencyProperty.Register
                (nameof(Start), typeof(SubjectView), typeof(EditReferenceController));

            EndProperty = DependencyProperty.Register
                (nameof(End), typeof(SubjectView), typeof(EditReferenceController));

            TypesProperty = DependencyProperty.Register
                (nameof(Types), typeof(IEnumerable<ComboBoxReferenceItem>), typeof(EditReferenceController));

            SelectedTypeProperty = DependencyProperty.Register
                (nameof(SelectedType), typeof(ComboBoxReferenceItem), typeof(EditReferenceController));
        }
    }
}
