using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Client.View
{
    public class ComboBoxSubjectItem
    {
        public string Text { get; set; }

        public SubjectType Type { get; set; }

        public static IList<ComboBoxSubjectItem> GetValues()
        {
            return new List<ComboBoxSubjectItem>()
            {
                new ComboBoxSubjectItem()
                {
                    Text = "Class",
                    Type = SubjectType.Class
                },
                new ComboBoxSubjectItem()
                {
                    Text = "Abstract class",
                    Type = SubjectType.Abstract_Class
                },
                new ComboBoxSubjectItem()
                {
                    Text = "Interface",
                    Type = SubjectType.Interface
                },
            };
        }
    }
}
