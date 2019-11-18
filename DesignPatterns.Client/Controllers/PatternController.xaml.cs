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
using DesignPatterns;

namespace DesignPatterns.Client.Controllers
{
    /// <summary>
    /// Логика взаимодействия для PatternController.xaml
    /// </summary>
    public partial class PatternController : UserControl
    {
        public PatternController()
        {
            InitializeComponent();
        }

        public int? SelectedPattern
        {
            get
            {
                if(Combo_Patterns.SelectedValue != null)
                {
                    return (int)Combo_Patterns.SelectedValue;
                }

                return null;
            }
        }

        public Difficulty? SelectedDifficulty
        {
            get
            {
                if(Combo_Difficulty.SelectedIndex == -1)
                {
                    return null;
                }

                var item = (ComboBoxItem)Combo_Difficulty.SelectedValue;

                return (Difficulty)Enum.Parse(typeof(Difficulty), (string)item.Content);
            }
        }
    }
}
