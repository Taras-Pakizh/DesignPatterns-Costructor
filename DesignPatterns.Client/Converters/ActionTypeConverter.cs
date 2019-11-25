using DesignPatterns.Client.View;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DesignPatterns.Client.Converters
{
    public class ActionTypeConverter : IValueConverter
    {
        private readonly string focused = "#ffb8860b";
        
        private readonly string unfocused = "#111111";


        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return unfocused;
            }

            if (value is CanvasActionType)
            {
                var action = (CanvasActionType)value;

                var control = (string)parameter;
                
                switch (control)
                {
                    case "Panel_Cursor_container":
                        if(action == CanvasActionType.Cursor)
                        {
                            return focused;
                        }
                        else
                        {
                            return unfocused;
                        }
                    case "Panel_SubjectType_container":
                        if (action == CanvasActionType.ObjectCreate)
                        {
                            return focused;
                        }
                        else
                        {
                            return unfocused;
                        }
                    case "Panel_ReferenceType_container":
                        if (action == CanvasActionType.ReferenceCreate)
                        {
                            return focused;
                        }
                        else
                        {
                            return unfocused;
                        }
                    default:
                        MessageBox.Show("Unknown control name - converter");
                        return null;
                }
            }
            throw new Exception("ActionTypeConverter type exception");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
