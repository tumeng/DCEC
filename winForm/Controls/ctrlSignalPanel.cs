using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Factory;
using Rmes.WinForm.Base;

namespace Rmes.WinForm.Controls
{
    public partial class ctrlSignalPanel : BaseControl
    {
        Timer timer1 = new Timer();
        //1、产品到位信号（无监控、正常、触发、断开）
        //2、操作开始信号（无监控、正常、触发、断开）
        //3、操作完成信号（无监控、正常、触发、断开）
        //4、产品放行信号（无监控、正常、触发、断开）
        //5、网络连接状态（无监控、信号差、正常、断开）
        //6、数据库状态（无监控、繁忙、连接、断开）
        //7、硬件接口状态（无监控、信号差、正常、断开） //now did not display in the view :(
        public ctrlSignalPanel()
        {
            InitializeComponent();
            imageList1.Images.Add(Rmes.WinForm.Resource.white);
            imageList1.Images.Add(Rmes.WinForm.Resource.yellow);
            imageList1.Images.Add(Rmes.WinForm.Resource.green);
            imageList1.Images.Add(Rmes.WinForm.Resource.red);
            foreach (ListViewItem a in listView1.Items)
            {
                a.ImageIndex =  0;
            }
            listView1.Enabled = false;

            this.RMesDataChanged += new RMesEventHandler(ctrlSignalPanel_OnRMesDataChanged);

            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 5000;
            timer1.Start();
        }
        void ctrlSignalPanel_OnRMesDataChanged(object obj, RMESEventArgs e)
        {
            string msgcode = e.MessageHead;
           string msgvalue;
           if (msgcode == "workpiece_arrival_plc")
           {
               msgvalue = e.MessageBody.ToString();
               listView1.Items[0].ImageIndex = msgvalue == "TRUE" ? 2 : 0;
           }
           else if (msgcode == "going_out")
           {
               msgvalue = e.MessageBody.ToString();
               listView1.Items[3].ImageIndex = msgvalue == "TRUE" ? 2 : 0;
           }

        }
        void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            try
            {
                System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();
                System.Net.NetworkInformation.PingReply rep = ping.Send("192.168.2.75", 1000);
                if (rep.Status == System.Net.NetworkInformation.IPStatus.BadDestination)
                    listView1.Items[4].ImageIndex = 3;
                else if (rep.Status == System.Net.NetworkInformation.IPStatus.TimedOut)
                    listView1.Items[4].ImageIndex = 1;
                else if (rep.Status == System.Net.NetworkInformation.IPStatus.Success)
                    listView1.Items[4].ImageIndex = 2;
                else
                    listView1.Items[4].ImageIndex = 0;
                listView1.Items[5].ImageIndex = DB.GetInstance().Connection.State == ConnectionState.Open ? 2 : 1;

            }
            catch
            { }
            timer1.Start();
        }
    }
}
