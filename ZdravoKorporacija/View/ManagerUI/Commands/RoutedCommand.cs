using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    }
}
