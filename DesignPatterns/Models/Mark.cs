using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    public class Mark
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public int mark { get; set; }

        public int percent { get; set; }

        public virtual Pattern pattern { get; set; }

        public Difficulty difficulty { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
