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
    /// Логика взаимодействия для EditAnswerController.xaml
    /// </summary>
    public partial class EditAnswerController : UserControl
    {
        public EditAnswerController()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty AnswerProperty;
        public string Answer
        {
            get { return (string)GetValue(AnswerProperty); }
            set { SetValue(AnswerProperty, value); }
        }

        public static readonly DependencyProperty IsTrueProperty;
        public bool IsTrue
        {
            get { return (bool)(GetValue(IsTrueProperty)); }
            set { SetValue(IsTrueProperty, value); }
        }
        

        static EditAnswerController()
        {
            AnswerProperty = DependencyProperty.Register
                (nameof(Answer), typeof(string), typeof(EditAnswerController));

            IsTrueProperty = DependencyProperty.Register
                (nameof(IsTrue), typeof(bool), typeof(EditAnswerController));
        }
    }
}
