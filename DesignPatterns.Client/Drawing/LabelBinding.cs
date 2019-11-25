using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DesignPatterns.Client.Drawing
{
    public class LabelBinding:IObjectBinding
    {
        public string Text { get; set; }

        public int Top { get; set; }

        public int Left { get; set; }

        public LabelBinding() { }

        public LabelBinding(SubjectCanvas subject)
        {
            switch (subject.View.type)
            {
                case SubjectType.Abstract_Class:
                    Text = "<<Abstract class>>\n" + subject.View.Name;
                    break;
                case SubjectType.Interface:
                    Text = "<<Interface>>\n" + subject.View.Name;
                    break;
                case SubjectType.Class:
                    Text = "<<Class>>\n" + subject.View.Name;
                    break;
                default:
                    throw new Exception("Subject cannot be a basic type");
            }

            Left = (int)(subject.Center.X - (subject.Width / 2));

            Top = (int)(subject.Center.Y - (subject.Height / 4));
        }
    }
}
