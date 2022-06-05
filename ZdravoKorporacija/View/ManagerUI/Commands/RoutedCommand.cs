using System.Windows.Input;

namespace ZdravoKorporacija.View.ManagerUI.Commands
{
    public static class RoutedCommand
    {

        public static readonly RoutedUICommand Help = new RoutedUICommand(
            "Help", "HelpWindow", typeof(RoutedCommand), new InputGestureCollection()
            {
                new KeyGesture(Key.H, ModifierKeys.Control)
            }
            );

        public static readonly RoutedUICommand Back = new RoutedUICommand(
            "Back", "BackButton", typeof(RoutedCommand), new InputGestureCollection()
            {
                new KeyGesture(Key.B, ModifierKeys.Control)
            }
            );

        public static readonly RoutedUICommand Logout = new RoutedUICommand(
            "Logout", "LogoutButton", typeof(RoutedCommand), new InputGestureCollection()
            {
                new KeyGesture(Key.L, ModifierKeys.Control)
            }
            );

        public static readonly RoutedUICommand Notification = new RoutedUICommand(
           "Notification", "NotificationButton", typeof(RoutedCommand), new InputGestureCollection()
           {
                new KeyGesture(Key.N, ModifierKeys.Control)
           }
           );

    }
}
