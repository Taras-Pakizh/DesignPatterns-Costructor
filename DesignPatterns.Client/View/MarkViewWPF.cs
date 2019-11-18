using DesignPatterns.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Client.View
{
    public class MarkViewWPF
    {
        public string Pattern { get; set; }
        
        public int Mark { get; set; }

        public int Percent { get; set; }

        public Difficulty Difficulty { get; set; }

        public override string ToString()
        {
            return "Pattern: " + Pattern + " - Mark: " + Mark + " " + Percent + "% Difficulty: " + Difficulty;
        }

        public MarkViewWPF() { }

        public MarkViewWPF(MarkView mark, IEnumerable<PatternView> patterns)
        {
            var pattern = patterns.SingleOrDefault(x => x.Id == mark.pattern_Id);

            Pattern = pattern.Name;

            Mark = mark.mark;

            Percent = mark.percent;

            Difficulty = mark.difficulty;
        }
    }
}
