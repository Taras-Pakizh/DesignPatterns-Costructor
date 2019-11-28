using DesignPatterns.Client.View;
using DesignPatterns.Client.Windows;
using DesignPatterns.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DesignPatterns.Client
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ApplicationView _context;

        private Window _currentWindow;

        public App()
        {
            _currentWindow = new LoginWindow();

            _context = new ApplicationView();

            _currentWindow.DataContext = _context;

            _currentWindow.Show();
        }

        public void FinishAuthorization()
        {
            if (!_context.IsAuthorized)
            {
                return;
            }

            _currentWindow = new ProfileWindow();
            
            _currentWindow.DataContext = _context;

            _currentWindow.Show();
        }

        public void Start(int patternId, Difficulty difficulty)
        {
            _currentWindow = new MainWindow();

            _context.LoadTasks(patternId, difficulty);

            _currentWindow.DataContext = _context;

            _currentWindow.Show();
        }

        public void LogOut()
        {
            _currentWindow = new LoginWindow();

            _context.LogOut();

            _currentWindow.DataContext = _context;

            _currentWindow.Show();
        }

        public Window Dialog { get; set; }

        public void ShowDialog()
        {
            Dialog = new ChooseElementWindow();

            Dialog.DataContext = _context;

            Dialog.Show();

            _currentWindow.IsEnabled = false;
        }

        public void CloseDialog()
        {
            Dialog.Close();

            Dialog = null;

            _currentWindow.IsEnabled = true;
        }

        public void DetermineDialog()
        {
            Dialog = null;

            _currentWindow.IsEnabled = true;
        }
    }
}
