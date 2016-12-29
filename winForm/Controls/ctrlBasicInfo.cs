using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Rmes.DA.Base;
using Rmes.WinForm.Base;
using Rmes.DA.Entity;
using Rmes.DA.Factory;
using Rmes.Pub.Data1;
//显示基础登录信息  thl
//Worktime add by liuzhy, and ctrl worktime bill be removed soon.
namespace Rmes.WinForm.Controls
{
    
    public partial class ctrlBasicInfo : Rmes.WinForm.Base.BaseControl
    {
        public //dataConn dc = new dataConn();
        Timer worktimer = new Timer();
        int judge = 0;
        int sumtime = 0;
        int totaltime = 0;
        DateTime starttime;
        string context = "";
        string loginfo = "";
        int contextswitchtime = 12;
        public ctrlBasicInfo()
        {
            InitializeComponent();
            worktimer.Stop();
            worktimer.Tick += new EventHandler(worktimer_Tick);
            worktimer.Interval = 1000;
            loginfo = BasicInfoShow();
            label3.Text = DateTime.Now.ToString();
            label1.Text = loginfo;
            progressBar1.Left = 0;
            progressBar1.Width = this.Width;
            this.RMesDataChanged += new RMesEventHandler(ctrlBasicInfo_RMesDataChanged);
            //initWorkTime();
            worktimer.Start();
        }

        void ctrlBasicInfo_RMesDataChanged(object obj, RMESEventArgs e)
        {
            if (e.MessageHead.Equals("SN") || e.MessageHead.Equals("RESN"))
            {
                initWorkTime();
            }
        }

        void initWorkTime()
        {
            string station_code = LoginInfo.StationInfo.RMES_ID.ToString();  //站点代码
            string pline_code = LoginInfo.ProductLineInfo.RMES_ID.ToString();
            sumtime = OptionFactory.GetSumTime(pline_code, station_code);    //进度条显示的总时间   以秒为单位
            //sumtime = 60;
            if (sumtime > 0)
            {
                worktimer.Stop();
                judge = 2;
                starttime = DateTime.Now;
                progressBar1.Maximum = sumtime;
                progressBar1.Minimum = 0;
                progressBar1.Value = 0;
                totaltime = 0;
                label2.ForeColor = Color.White;
                label2.BackColor = Color.DarkGreen;
                worktimer.Start();
            }
            else
            {
                judge = 0;
            }
            progressBar1.ForeColor = Color.Green;
        }

        int jo = 0;
        void worktimer_Tick(object sender, EventArgs e)
        {
            worktimer.Stop();
            label3.Text = DateTime.Now.ToString();
            if ((DateTime.Now.Second % contextswitchtime) == 0)
            {
                switch (jo)
                {
                    case 0:
                        jo = 1;
                        label1.Text = loginfo;
                        label1.BackColor = SystemColors.Control;
                        label1.ForeColor = Color.Blue;
                        break;
                    default:
                        jo = 0;
                        BroadcastEntity entity = BroadcastFactory.GetContextByCompanyPline(LoginInfo.CompanyInfo.COMPANY_CODE, LoginInfo.ProductLineInfo.RMES_ID);
                        if (entity != null) context = entity.BROADCAST_CONTENT; else context = "";
                        if (!string.IsNullOrWhiteSpace(context))
                        {
                            label1.Text = context;
                            label1.ForeColor = Color.OrangeRed;
                        }
                        break;
                }
            }

            if (judge == 2)
            {
                TimeSpan tspan = DateTime.Now - starttime;
                totaltime = (int)tspan.TotalSeconds;
                progressBar1.Value = totaltime % sumtime;
                label2.Text = "工时:" + totaltime.ToString() + "/" + sumtime;
                label2.Left = this.Width - label2.Width;
                if (totaltime > sumtime)
                    label2.BackColor = Color.DarkOrange;
                if (totaltime > sumtime * 1.2)
                    label2.BackColor = Color.DarkRed;
                if (progressBar1.Value >= progressBar1.Maximum)
                    progressBar1.Value = 0;
            }
            worktimer.Start();
            //throw new NotImplementedException();
        }

        public string BasicInfoShow()
        {
            string _company_name = LoginInfo.CompanyInfo.COMPANY_NAME;  //公司名
            string _ip = LoginInfo.IP.ToString();   //IP
            string stationtype = "";
            try
            {
                stationtype = dataConn.GetValue("select station_type_name from code_station_type where station_type_code='" + LoginInfo.StationInfo.STATION_TYPE + "' ");
            }
            catch
            { }
            string _station_name = LoginInfo.StationInfo.STATION_NAME.ToString();   //站点
            string _pline_name = LoginInfo.ProductLineInfo.PLINE_NAME.ToString();  //生产线

            string _bc_name = LoginInfo.ShiftInfo.SHIFT_NAME.ToString();       //班次
            //string _bz_name = LoginInfo.TeamInfo.TEAM_NAME.ToString();      //班组
            string _user_name = LoginInfo.UserInfo.USER_NAME; //用户名

            //return _pline_name + "/" + _station_name + "/" + _bc_name + "/" + _bz_name + "/" + _user_name;
            return _pline_name + "/" + stationtype+"/"+_station_name + "/" + _bc_name + "/" + _user_name;
        }
    }
}
