namespace BasicWpfLibrary;

using System;
using System.Windows.Controls;
using System.Globalization;

public class FloatRangeRule : ValidationRule
{
    public float Min { get; set; }
    public float Max { get; set; }

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

        if ((val < Min) || (val > Max))
        {
            return new ValidationResult(false,
                $"Please enter an value in the range: {Min} to {Max}.");
        }
        return ValidationResult.ValidResult;
    }
}
