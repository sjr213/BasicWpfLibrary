using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows;

// https://www.codeproject.com/Tips/285358/All-purpose-Boolean-to-Visibility-Converter
namespace BasicWpfLibrary.Converters
{
    /// <summary>
    /// Converts Boolean Values to Control.Visibility values
    /// </summary>
    public class BoolToVisibilityConverter : IValueConverter
    {
        //Set to true if you want to show control when boolean value is true
        //Set to false if you want to hide/collapse control when value is true
        private bool visibleValue = true;
        public bool VisibleValue
        {
            get { return visibleValue; }
            set { visibleValue = value; }
        }

        //Set to true if you just want to hide the control
        //else set to false if you want to collapse the control
        private bool isHidden = false;
        public bool IsHidden
        {
            get { return isHidden; }
            set { isHidden = value; }
        }

        private object GetVisibility(object value)
        {
            if (value is not bool)
                return DependencyProperty.UnsetValue;

            bool objValue = (bool)value;
            if (objValue != VisibleValue && IsHidden)
            {
                return Visibility.Hidden;
            }
            if (objValue != VisibleValue && !IsHidden)
            {
                return Visibility.Collapsed;
            }
            return Visibility.Visible;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return GetVisibility(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}