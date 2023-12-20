using System.Windows;

namespace BasicWpfLibrary;

using System;
using System.Windows.Controls;
using System.Globalization;

public class ExternalViewModel : DependencyObject
{
    public static readonly DependencyProperty StartProperty =
        DependencyProperty.Register(nameof(Start), typeof(object), typeof(ExternalViewModel));

    public object Start
    {
        get => GetValue(StartProperty);
        set => SetValue(StartProperty, value);
    }

    public static readonly DependencyProperty EndProperty =
        DependencyProperty.Register(nameof(End), typeof(object), typeof(ExternalViewModel));

    public object End
    {
        get => GetValue(EndProperty);
        set => SetValue(EndProperty, value);
    }

    public static readonly DependencyProperty AlternateProperty =
        DependencyProperty.Register(nameof(Alternate), typeof(object), typeof(ExternalViewModel));

    public object Alternate
    {
        get => GetValue(AlternateProperty);
        set => SetValue(AlternateProperty, value);
    }
}

// If not Alternate - just return Valid
// Else if Abs(Start-End) < MinDifference - value must == 1
// Else MinSteps < value <= MaxSteps
public class StepIntRangeRule : ValidationRule
{
    public float MinDifference { get; set; }
    public int MinSteps { get; set; }
    public int MaxSteps { get; set; }

    public ExternalViewModel? Vm { get; set; }

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

        if (Vm is not { Start: float start })
        {
            return new ValidationResult(false, $"Start value is not a float");
        }

        if (Vm is not { End: float end })
        {
            return new ValidationResult(false, $":End is not a float");
        }

        if (Vm is not { Alternate: bool alternate })
        {
            return new ValidationResult(false, $"Alternate is not a bool");
        }

        if(! alternate) return ValidationResult.ValidResult;

        if (Math.Abs(start - end) > MinDifference)
        {
            if(val <= MinSteps || val > MaxSteps)
                return new ValidationResult(false,
                    $"Please enter an value in the range: {MinSteps+1} to {MaxSteps}.");
        }
        else
        {
            if(val != MinSteps)
                return new ValidationResult(false,
                    $"For a non-changing coefficient enter {MinSteps}.");
        }

        return ValidationResult.ValidResult;
    }
}