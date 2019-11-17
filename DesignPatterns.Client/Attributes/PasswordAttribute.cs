using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DesignPatterns.Client.Attributes
{
    class PasswordAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                var regex = new Regex(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\s).{8,20}$");
                var result = regex.Match((string)value);
                if (result.Success)
                {
                    return true;
                }
                else
                {
                    this.ErrorMessage = "Password requires an upper case letter, lower case letter, non alphanumeric symbol and a number. Length should be between 8 and 20";
                    return false;
                }
            }
            return false;
        }
    }
}
