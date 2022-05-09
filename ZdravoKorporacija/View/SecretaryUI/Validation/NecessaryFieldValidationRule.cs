using System.Globalization;
using System.Windows.Controls;

namespace ZdravoKorporacija.View.SecretaryUI.Validation
{
    public class NecessaryFieldValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                var text = value as string;
                if (text.Length == 0)
                    return new ValidationResult(false, "This field is necessary!");
                else
                    return new ValidationResult(true, null);
            }
            catch
            {
                return new ValidationResult(false, "Unknown error occured.");
            }

        }
    }
}
