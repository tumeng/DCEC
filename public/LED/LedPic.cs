using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LabLed.Components;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;

namespace Rmes.Public.Base
{
    public class LedPic
    {
        private const int WM_LED_NOTIFY = 1025;
        LedDLL LEDSender = new LedDLL();
        IntPtr handle = new IntPtr();
        int dev = -1;
       // LedDAL dal = new LedDAL();
        Thread td;
        //screenLEDEntity ledentity;
        //public LedPic(screenLEDEntity entity)
        //{
        //    ledentity = entity;
        //    this.OpenLed();

        //}
        //public void startsend()
        //{
        //    if (td == null)
        //        td = new Thread(this.SendPics);
        //    // td.IsAlive
        //    td.Start();
        //}
        //释放大屏
        public LedPic()
        { }
        public void Dispose()
        {

        }
        public string PicturePath
        {
            get;
            set;
        }
        public List<string> Pics { get; set; }
        public  void SendPics(int width, int height, int caddress, string ipaddress, string port)
        {
            LedDLL.RECT r = new LedDLL.RECT();
            LedDLL.DEVICEPARAM param = new LedDLL.DEVICEPARAM();
            //param.devType = 2;
           // param.ComPort = 1;
            param.locPort = 8009;
            //param.Speed = 4;
            param.rmtPort = 6666;
            param.devType = 1;
            //IntPtr handle = new IntPtr();
            int dev1 = LEDSender.LED_Open(ref param, LedDLL.NOTIFY_BLOCK, 0,0 );
            LEDSender.MakeRoot(LedDLL.eRootType.ROOT_PLAY, LedDLL.eScreenType.SCREEN_COLOR);
            //LedPic Led1 = new LedPic(null);
            foreach (string sendpic in Pics)
            {
                this.PicturePath = sendpic;
                LEDSender.AddLeaf(5000);
                r.left = 0;
                r.top = 0;
                r.right = width;
                r.bottom = height;
                // string filepath = Application.StartupPath + "\\path.bmp";
                LEDSender.AddPicture(PicturePath, ref r, 1, 1, 1, 0);
            }
            LEDSender.LED_SendToScreen(dev1, Convert.ToByte(caddress),ipaddress , Convert.ToUInt16(port));

           

        }
    }

}

