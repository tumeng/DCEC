using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using Rmes.WinForm.Base;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;
using Rmes.Public.Base;
using LabLed.Components;
using System.IO;
using System.Windows.Forms;

namespace Rmes.WinForm.Controls
{
    public partial class ctrlLed : BaseControl
    {
        ImageList imglist = new ImageList();

        //LedDAL dal = new LedDAL();
        private const int WM_LED_NOTIFY = 1025;
        LedDLL LEDSender = new LedDLL();
        IntPtr handle = new IntPtr();
        int dev = -1;
        public ctrlLed()
        {
            InitializeComponent();
            ledView.View = View.Details;
            ledView.Columns.Add("警示", -2, HorizontalAlignment.Center);
            ledView.Columns.Add("名称", -2, HorizontalAlignment.Center);
            ledView.Columns.Add("IP地址", 100, HorizontalAlignment.Left);
            ledView.Columns.Add("端口号", -2, HorizontalAlignment.Center);
            ledView.Columns.Add("LED宽度", -2, HorizontalAlignment.Right);
            ledView.Columns.Add("LED高度", -2, HorizontalAlignment.Right);
            ledView.Columns.Add("控制器", -2, HorizontalAlignment.Left);
            ledView.Columns.Add("屏幕类型", -2, HorizontalAlignment.Center);
            ledView.Columns.Add("位置", 100, HorizontalAlignment.Left);
            ledView.Columns.Add("状态", -2, HorizontalAlignment.Center);
            ledView.Columns.Add("最后修改日期", 120, HorizontalAlignment.Left);
            ledView.Columns.Add("控制卡地址", -2, HorizontalAlignment.Center);

            //设置警示图标

            imglist.Images.Add(Rmes.WinForm.Resource.red);
            imglist.Images.Add(Rmes.WinForm.Resource.yellow);
            imglist.Images.Add(Rmes.WinForm.Resource.green);
            ledView.SmallImageList = imglist;
            ledView.LargeImageList = imglist;
            ledView.CheckBoxes = true;
            timer1.Interval = 10000;
            timer1.Start();
        }
        private void btn_led_Click(object sender, EventArgs e)
        {

            //MessageBox.Show(Application.StartupPath.ToString());
            List<ScreenLEDEntity> listled = ScreenLedFactory.GetAllLed();


            foreach (ScreenLEDEntity LEDen in listled)
            {

                string[] str = new string[] { "", LEDen.SCREENNAME, LEDen.IPADDRESS, LEDen.PORT, LEDen.WIDTH.ToString(), LEDen.HEIGHT.ToString(), LEDen.CONTROLER, LEDen.SCREENTYPE, LEDen.POSITION, LEDen.STATUS, LEDen.LASTUPDATE.ToString(), LEDen.CADDRESS.ToString() };
                ListViewItem Item = new ListViewItem(str);
                //(0 is green )(1 is yellow)(2 is red)
                if (LEDen.STATUS == "0")
                {
                    Item.ImageIndex = 2;
                }
                else
                    if (LEDen.STATUS == "1")
                    {
                        Item.ImageIndex = 1;
                    }
                    else
                        Item.ImageIndex = 0;

                ledView.Items.Add(Item);
            }
            //this.OpenLed();
        }
        
        public void Dispose()
        {

        }
        

        //得到LED参数

        private void GetParam(ref LedDLL.DEVICEPARAM param)
        {
            param.locPort = (uint)Convert.ToInt16(8009);
            param.rmtPort = Convert.ToUInt32(6666);
        }
        //连接LED
        private void OpenLed()
        {
            ScreenLEDEntity twobig = ScreenLedFactory.GetByKey("192.168.113.73");
            LedDLL.DEVICEPARAM param = new LedDLL.DEVICEPARAM();
            GetParam(ref param);
            dev = LEDSender.LED_Open(ref param, LedDLL.NOTIFY_BLOCK, 0, 0);
            if (dev == -1)
            {
                twobig.STATUS = "2";
                ScreenLedFactory.Update(twobig);
            }
            else
            {
                twobig.STATUS = "1";
                ScreenLedFactory.Update(twobig);
            }
        }
       
        public void SendLed(string ipaddress)
        {

            ScreenLEDEntity screen = ScreenLedFactory.GetByKey(ipaddress);

            LedPic Led1 = new LedPic();
            LedProcFactory LedPlan = new LedProcFactory(null);
            //LedPic Led1 = new LedPic(twobig);//发送图片
            //LedProc LedPlan = new LedProc(null);
            Led1.Pics = LedPlan.CreatPlanPic(screen.PLINE_CODE);
            int ses = Led1.Pics.Count * 5000;
            timer1.Interval = ses > 60000 ? ses : 60000;
            Led1.SendPics(screen.WIDTH, screen.HEIGHT, screen.CADDRESS, screen.IPADDRESS, screen.PORT);
           
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            //if (IPS.Contains("192.168.113.73"))
            //    this.SendLed("192.168.113.73");
            List<ScreenLEDEntity> listscreen = ScreenLedFactory.GetAllLed();
            foreach (ScreenLEDEntity screen in listscreen)
            {
                if(IPS.Contains(screen.IPADDRESS))
                    this.SendLed(screen.IPADDRESS);
            }
            timer1.Start();
        }
        
        List<string> IPS = new List<string>();
        private void ledView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            string ip = e.Item.SubItems[2].Text;
            if (e.Item.Checked)
                IPS.Add(ip);
            else if (IPS.Contains(ip))
                IPS.Remove(ip);
        }
    }
}
