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
using DesignPatterns.Client.Validation;
using DesignPatterns.Client.View;
using DesignPatterns.Views;

namespace DesignPatterns.Client.Windows
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private ModelValidator validator = new ModelValidator();

        private ApplicationView Context
        {
            get { return (ApplicationView)DataContext; }
        }

        private bool IsModelValid(object model)
        {
            if (!validator.IsModelValid(model))
            {
                Text_Errors.Text = validator.ValidationResults;
                return false;
            }
            return true;
        }

        public LoginWindow()
        {
            InitializeComponent();
        }

        private async void Enter_Click(object sender, RoutedEventArgs e)
        {
            if(_state == State.Authorization)
            {
                var model = new AuthorizationModel()
                {
                    username = Edit_Login.Text,
                    password = Edit_Password.Password
                };

                if (!IsModelValid(model))
                {
                    return;
                }

                await Context.Authorize(model);
            }
            else if(_state == State.Registration)
            {
                var item = (ComboBoxItem)Combo_Role.SelectedValue;
                
                Role role = (Role)Enum.Parse(typeof(Role), (string)item.Content);

                var model = new RegistrationModel()
                {
                    username = Edit_Login.Text,
                    password = Edit_Password.Password,
                    Role = role
                };

                if(!(await Context.Register(model)))
                {
                    Text_Errors.Text = Context.ErrorMessages;
                }
                else
                {
                    Change_Click(null, null);
                }

                return;
            }
            
            if (Context.IsAuthorized)
            {
                ((App)Application.Current).FinishAuthorization();

                this.Close();
            }
            else
            {
                string message = "Authorization has failed. Check if login and password had been inputed correctly";

                if (Context.ErrorMessages != null)
                {
                    message += "\n" + Context.ErrorMessages;
                }

                Text_Errors.Text = message;
            }
        }

        private State _state = State.Authorization;

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            Button_Change.Content = _state.ToString();

            if (_state == State.Authorization)
            {
                _state = State.Registration;

                Text_Role.Visibility = Visibility.Visible;

                Combo_Role.Visibility = Visibility.Visible;

                Button_Enter.Content = "Sing in";
            }
            else
            {
                _state = State.Authorization;

                Text_Role.Visibility = Visibility.Collapsed;

                Combo_Role.Visibility = Visibility.Collapsed;

                Button_Enter.Content = "Log in";
            }
        }
    }

    enum State
    {
        Registration,
        Authorization
    }
}
