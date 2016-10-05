using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreedomJoy
{
    static class Logger
    {
        public static ObservableCollection<string> Logs = new ObservableCollection<string>();
        public static void Log(string message)
        {
            Logs.Add("[" + DateTime.Now + "] " + message); // Todo: Fix this, tie it to a list and tie that to the UI.
        }
    }
}
