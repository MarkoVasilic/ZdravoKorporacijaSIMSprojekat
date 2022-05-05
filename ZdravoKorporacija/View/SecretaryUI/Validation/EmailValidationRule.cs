using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace ZdravoKorporacija.View.SecretaryUI.Validation
{
    public class EmailValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                var text = value as string;
                if (text.Length == 0)
                    return new ValidationResult(false, "This field is necessary!");

                Regex r = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
                if (r.IsMatch(text))
                {
                    return new ValidationResult(true, null);
                }
                return new ValidationResult(false, "Invalid email format!");
            }
            catch
            {
                return new ValidationResult(false, "Unknown error occured!");
            }
        }
    }
}
