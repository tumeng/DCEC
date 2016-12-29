using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace RMES.MaterialCal
{
    static class Program
    {
        private static System.Threading.Mutex _mutex;

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            _mutex = new System.Threading.Mutex(false, "Global\\RMES.MaterialCal");
            if (_mutex.WaitOne(0, false) == false)
            {
                MessageBox.Show("程序不能同时运行多个副本，点“确定”退出\r\n您可以在任务栏的系统托盘区域找到本程序的图标。\r\n如有疑问，请联系系统管理员。");
                return;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MatCal());
        }
    }
}
