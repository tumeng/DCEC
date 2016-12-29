using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rmes.Public.ErrorHandle
{
    public static class EH
    {
        public static HandleLevel TraceLever = HandleLevel.TRACE;
        private static string lastmsg = "";

        public static void Trace(string message)
        {
            ShowMessage(HandleLevel.TRACE, message);
        }
        public static void Info(string message)
        {
            ShowMessage(HandleLevel.INFO, message);
        }
        public static void Warning(string message)
        {
            ShowMessage(HandleLevel.WARNING, message);
        }
        public static void ERROR(string message)
        {
            ShowMessage(HandleLevel.ERROR, message);
        }
        public static void FATALERROR(string message)
        {
            ShowMessage(HandleLevel.FATALERROR, message);
        }
        private static void ShowMessage(HandleLevel h, String m)
        {
            lastmsg = m;
            if (h >= TraceLever)
            {
                //MessageBox.Show(m,"Rmes系统提示：");
            }
        }

        public static string LASTMSG
        {
            get { return lastmsg; }
            set { lastmsg = value; }
        }
    }

    public enum HandleLevel
    { 
        TRACE=0,INFO,WARNING,ERROR,FATALERROR
    }
}
