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
using System.Windows.Shapes;

namespace DesignPatterns.Client.Windows
{
    /// <summary>
    /// Логика взаимодействия для DeleteWindow.xaml
    /// </summary>
    public partial class DeleteWindow : Window
    {
        public DeleteWindow()
        {
            InitializeComponent();
        }

        private string _message;

        public DeleteWindow(string message)
        {
            _message = message;

            InitializeComponent();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            ((App)Application.Current).DetermineDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Text_Message.Text = _message;
        }
    }
}
