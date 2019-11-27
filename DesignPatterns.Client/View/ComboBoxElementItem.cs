using DesignPatterns.Client.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Client.View
{
    public class ComboBoxElementItem
    {
        public string Text { get; set; }

        public ICanvasElement Element { get; set; }

        public ComboBoxElementItem() { }

        public ComboBoxElementItem(ICanvasElement element)
        {
            Element = element;

            if(Element is SubjectCanvas)
            {
                var subject = (SubjectCanvas)Element;

                Text = subject.View.type.ToString() + ": " + subject.View.Name;
            }

            if(Element is ArrowCanvas)
            {
                var arrow = (ArrowCanvas)Element;

                Text = arrow.Reference.Subject.View.Name + " and "
                    + arrow.Reference.Target.View.Name + " - dependency: "
                    + arrow.Reference.View.type.ToString();
            }
        }

        public static IList<ComboBoxElementItem> GetValues(IEnumerable<ICanvasElement> elements)
        {
            var list = new List<ComboBoxElementItem>();

            foreach(var item in elements)
            {
                list.Add(new ComboBoxElementItem(item));
            }

            return list;
        }
    }
}
