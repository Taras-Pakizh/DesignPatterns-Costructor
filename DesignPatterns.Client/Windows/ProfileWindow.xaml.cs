using DesignPatterns.Client.View;
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
    /// Логика взаимодействия для ProfileWindow.xaml
    /// </summary>
    public partial class ProfileWindow : Window
    {
        public ProfileWindow()
        {
            InitializeComponent();
        }

        private ApplicationView Context
        {
            get { return (ApplicationView)DataContext; }
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).LogOut();

            this.Close();
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            if (Context.Client.CurrentUser.Role == Views.Role.Administrator)
            {
                ((App)Application.Current).OpenAdminPanel();

                this.Close();

                return;
            }

            Panel_Profile.Visibility = Visibility.Collapsed;

            Panel_Patterns.Visibility = Visibility.Visible;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Panel_Profile.Visibility = Visibility.Visible;

            Panel_Patterns.Visibility = Visibility.Collapsed;
        }

        private void StartTesting_Click(object sender, RoutedEventArgs e)
        {
            var patternId = Controller_Patterns.SelectedPattern;

            var difficulty = Controller_Patterns.SelectedDifficulty;

            if(patternId == null || difficulty == null)
            {
                MessageBox.Show("Choose pattern and difficulty");

                return;
            }
            
            ((App)Application.Current).Start((int)patternId, (Difficulty)difficulty);

            this.Close();
        }




        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(Context.Client.CurrentUser.Role == Views.Role.Administrator)
            {
                Panel_Tasks.Visibility = Visibility.Collapsed;
            }
        }
    }
}
