using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DesignPatterns.Client.Validation
{
    public class ModelValidator
    {
        public string ValidationResults { get; set; }

        public bool IsModelValid(object model)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(model);
            if (Validator.TryValidateObject(model, context, results, true))
            {
                return true;
            }
            ValidationResults = String.Join("\n", results.Select(x => x.ErrorMessage));
            return false;
        }
    }
}
