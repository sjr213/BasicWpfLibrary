namespace BasicWpfLibrary
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public static class TextBoxFocusBehavior
    {
        public static readonly DependencyProperty ClearOnFocusProperty =
            DependencyProperty.RegisterAttached(
                "ClearOnFocus",
                typeof(bool),
                typeof(TextBoxFocusBehavior),
                new PropertyMetadata(false, OnClearOnFocusChanged));

        public static bool GetClearOnFocus(DependencyObject obj)
        {
            return (bool)obj.GetValue(ClearOnFocusProperty);
        }

        public static void SetClearOnFocus(DependencyObject obj, bool value)
        {
            obj.SetValue(ClearOnFocusProperty, value);
        }

        private static void OnClearOnFocusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBox textBox)
            {
                if ((bool)e.NewValue)
                {
                    // Subscribe to the GotFocus event
                    textBox.GotKeyboardFocus += TextBox_GotKeyboardFocus;
                }
                else
                {
                    // Unsubscribe if the behavior is removed
                    textBox.GotKeyboardFocus -= TextBox_GotKeyboardFocus;
                }
            }
        }

        private static void TextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                // Clear the TextBox content
                textBox.Clear();
                // Optional: Select all text instead of clearing to allow easy overwriting
                // textBox.SelectAll(); 
            }
        }
    }
}
