using System.Windows;

namespace System.Windows
{
    public class UIAActionItem
    {
        public string s_window {get;}
        public string s_fe {get;}
        public RoutedEventArgs s_e{get;}
        public bool ait {get;}

        public UIAActionItem(string w, string fe, RoutedEventArgs e, bool ai)
        {
            s_window = w;
            s_fe = fe;
            s_e = e;
            ait = ai;
        }

    }
}