
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Security;

namespace TicketingSystem
{
    public class CheckBugAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid
        (object value, ValidationContext validationContext)
        {
            string sErrorMessage = "The word 'bug' should not be used in the title!";
            if (value != null)
            {

                string s = (string)value;
                s = s.ToLower();
                int lul = s.ToLower().IndexOf("bug");

                if (s.IndexOf("bug") >= 0) return new ValidationResult(sErrorMessage); 
            }
            return ValidationResult.Success;

        }
    }
}
