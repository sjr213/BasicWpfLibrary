namespace BasicWpfLibrary;

using System;
using System.Windows.Controls;
using System.Globalization;

public class FloatRule : ValidationRule
{
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        float val = 0f;

        try
        {
            if (((string)value).Length > 0)
                val = float.Parse((string)value);
        }
        catch (Exception e)
        {
            return new ValidationResult(false, $"Illegal characters or {e.Message}");
        }

        return ValidationResult.ValidResult;
    }
}