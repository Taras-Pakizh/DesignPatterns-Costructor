using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    public class SubjectProperty
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MinLength(1)]
        public string Name { get; set; }

        [Column("Type_Id")]
        public virtual Subject Type { get; set; }

        [Column("Subject_Id")]
        public virtual Subject Subject { get; set; }
    }
}
