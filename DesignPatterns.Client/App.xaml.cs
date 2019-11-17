﻿using DesignPatterns.Client.View;
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
            if (!_context.Client.IsAuthorizated)
            {
                return;
            }

            _currentWindow = new ProfileWindow();

            _currentWindow.DataContext = _context;

            _currentWindow.Show();
        }

        public void Start()
        {
            _currentWindow = new MainWindow();

            _currentWindow.DataContext = _context;

            _currentWindow.Show();
        }

        public void LogOut()
        {
            _currentWindow = new LoginWindow();

            _currentWindow.DataContext = _context;

            _currentWindow.Show();
        }
    }
}