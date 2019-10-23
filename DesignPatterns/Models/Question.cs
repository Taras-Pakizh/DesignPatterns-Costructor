using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    public class Question
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MinLength(1)]
        public string Name { get; set; }

        public string question { get; set; }

        public virtual Pattern Pattern { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
    }
}
