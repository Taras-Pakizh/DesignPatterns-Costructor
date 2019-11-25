using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace DesignPatterns.Client.View
{
    public class ComboBoxReferenceItem
    {
        public string Image { get; set; }

        public ReferencesType Type { get; set; }

        public string Text { get; set; }

        public static IList<ComboBoxReferenceItem> GetValues()
        {
            return new List<ComboBoxReferenceItem>()
            {
                new ComboBoxReferenceItem()
                {
                    Type = ReferencesType.Aggregation,
                    Image = "../images/aggregation.png",
                    Text = "Aggregation"
                },
                new ComboBoxReferenceItem()
                {
                    Type = ReferencesType.Assosiation,
                    Image = "../images/assosiation.png",
                    Text = "Assosiation"
                },
                new ComboBoxReferenceItem()
                {
                    Type = ReferencesType.Composion,
                    Image = "../images/composition.png",
                    Text = "Composion"
                },
                new ComboBoxReferenceItem()
                {
                    Type = ReferencesType.Dependency,
                    Image = "../images/dependency.png",
                    Text = "Dependency"
                },
                new ComboBoxReferenceItem()
                {
                    Type = ReferencesType.Inheritance,
                    Image = "../images/inheritance.png",
                    Text = "Inheritance"
                },
                new ComboBoxReferenceItem()
                {
                    Type = ReferencesType.Realization,
                    Image = "../images/realization.png",
                    Text = "Realization"
                }
            };
        }
    }
}
