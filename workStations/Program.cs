using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Rmes.WinForm.Base;

namespace MES
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string oraclePath = System.Windows.Forms.Application.StartupPath + @"\OCI";
            string libPath = System.Windows.Forms.Application.StartupPath + @"\Lib";
            string allpaths = oraclePath + ";" + libPath;
            Environment.SetEnvironmentVariable("PATH", allpaths);
            //Environment.SetEnvironmentVariable("NLS_LANG", "SIMPLIFIED CHINESE_CHINA.ZHS16GBK", EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("NLS_LANG", "SIMPLIFIED CHINESE_CHINA.AL32UTF8", EnvironmentVariableTarget.Process);

            FormAppContext rmesapp = new FormAppContext();
            Application.Run(rmesapp);
        }
    }
}
