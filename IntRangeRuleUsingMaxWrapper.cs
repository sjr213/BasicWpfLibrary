using System.Windows;

namespace BasicWpfLibrary;

using System;
using System.Windows.Controls;
using System.Globalization;

public class Wrapper : DependencyObject
{
    public static readonly DependencyProperty MaxProperty =
        DependencyProperty.Register(nameof(Max), typeof(object), typeof(Wrapper));

    public object Max
    {
        get => GetValue(MaxProperty);
        set => SetValue(MaxProperty, value);
    }
}

public class IntRangeRuleUsingMaxWrapper : ValidationRule
{
    public int Min { get; set; }

    public Wrapper? Wrapper { get; set; }

    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        int val = 0;

        try
        {
            if (((string)value).Length > 0)
                val = int.Parse((string)value);
        }
        catch (Exception e)
        {
            return new ValidationResult(false, $"Illegal characters or {e.Message}");
        }

        if (Wrapper is not { Max: int max })
        {
            return new ValidationResult(false,
                $"Max value is not an integer");
        }

        if ((val < Min) || (val > max))
        {
            return new ValidationResult(false,
                $"Please enter an value in the range: {Min} to {max}.");
        }
        return ValidationResult.ValidResult;
    }
}
