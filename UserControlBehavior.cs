
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BasicWpfLibrary
{
    public class UserControlBehavior
    {
        public static readonly DependencyProperty IsVisibleChangedCommandProperty =
            DependencyProperty.RegisterAttached("IsVisibleChangedCommand", typeof(ICommand),
                typeof(UserControlBehavior),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(IsVisibleChangedCommandChanged)));

        private static void IsVisibleChangedCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UserControl element = (UserControl)d;

            element.IsVisibleChanged += element_IsVisibleChanged;
        }

        static void element_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            UserControl element = (UserControl)sender;

            ICommand command = GetIsVisibleChangedCommand(element);

            command.Execute(e);
        }

        public static void SetIsVisibleChangedCommand(UIElement element, ICommand value)
        {
            element.SetValue(IsVisibleChangedCommandProperty, value);
        }

        public static ICommand GetIsVisibleChangedCommand(UIElement element)
        {
            return (ICommand)element.GetValue(IsVisibleChangedCommandProperty);
        }
    }
}

