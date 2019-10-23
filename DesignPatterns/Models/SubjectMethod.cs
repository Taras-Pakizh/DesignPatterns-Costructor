using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    public class SubjectMethod
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        [Column("Subject_Id")]
        public virtual Subject Subject { get; set; }

        [Column("ReturnValue_Id")]
        public virtual Subject ReturnValue { get; set; }
        
        public AccessType AccessType { get; set; }

        public virtual ICollection<MethodParameter> parameters { get; set; }
    }
}
