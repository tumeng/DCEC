using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Rmes.WinForm.Base;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Factory;
using System.Reflection;
using System.Diagnostics;
using Rmes.Pub.Data1;
using System.Runtime.InteropServices;
using System.Threading;

namespace Rmes.WinForm.Controls
{
    //上线站点初始化，初始化条码，生成最新发动机条码
    public partial class ctrl_init_online_1 : BaseControl
    {
        [DllImport("jwldll.dll")]
        //[DllImport(@"D:\CHKMES\rmes.workstation\Lib\FNTHEX32.dll")]
        public static extern int jwlInit( );
        public static extern int jwlStatus( );
        public static extern int jwlReturnOrigin();
        public static extern int jwlOpenFile( string str1);
        public static extern int jwlPrint(string str1);

        private string CompanyCode, PlineCode, PlineCode1, StationID, len_sn, TheCustomer, TheSn, ThePlancode,ThePlanso,ThePmodel, StationName, Station_Code, Workdate;
        private string ShiftCode, TeamCode, UserID, UserCode;
        private bool isManual = false;
        private RMESEventArgs arg = new RMESEventArgs();
        //dataConn dc = new dataConn();
        public ctrl_init_online_1()
        {
            InitializeComponent();
            CompanyCode = LoginInfo.CompanyInfo.COMPANY_CODE;
            PlineCode = LoginInfo.ProductLineInfo.RMES_ID;
            PlineCode1 = LoginInfo.ProductLineInfo.PLINE_CODE;
            StationID = LoginInfo.StationInfo.RMES_ID;
            Station_Code = LoginInfo.StationInfo.STATION_CODE;
            StationName = LoginInfo.StationInfo.STATION_NAME;
            ShiftCode = LoginInfo.ShiftInfo.SHIFT_CODE;
            TeamCode = LoginInfo.TeamInfo.TEAM_CODE;
            UserID = LoginInfo.UserInfo.USER_ID;
            UserCode = LoginInfo.UserInfo.USER_CODE;
            len_sn = LoginInfo.LEN_SN;
            Workdate = LoginInfo.WorkDate;
            this.RMesDataChanged += new Rmes.WinForm.Base.RMesEventHandler(ctrlInitOnline_RmesDataChanged);
            initonline();
            //初始化打印机
            try
            {
                int ret = jwlInit();
                switch (ret)
                {
                    case 0:
                        break;
                    case 1:
                        MessageBox.Show("注册表错误!", "提示");
                        break;
                    case 2:
                        MessageBox.Show("系统参数错误!!", "提示");
                        break;
                    case 3:
                        MessageBox.Show("加密盒错误!", "提示");
                        break;
                    default :
                        MessageBox.Show("端口错误!", "提示");
                        break;
                }
            }
            catch
            {
                MessageBox.Show("初始化打印机错误","提示");
            }
            //保存计划信息
            Savedata(StationName, "", PlineCode1, Workdate);
            timer1.Enabled = true;
            
            arg.MessageHead = "";
            arg.MessageBody = "";
        }
        protected void ctrlInitOnline_RmesDataChanged(object obj, Rmes.WinForm.Base.RMESEventArgs e)
        {
            if (e.MessageHead == null) return;
            if (e.MessageHead == "SN")
            {
                //上线完成 自动刷下一个
                timer1.Enabled = true;
            }
        }
        private void initonline()
        {


        }
        private void Savedata(string stationname, string sn, string plinecode, string workdate)
        {
            string sql = "";
            if (plinecode == "E")
                sql = "select plan_seq,product_model,plan_code,plan_so,plan_qty,online_qty,customer_name,begin_date,end_date,create_time,remark from data_plan where run_flag='Y' and pline_code='" + plinecode + "' and to_char(begin_date,'yyyymmdd')>=to_char(sysdate-3,'yyyymmdd') and to_char(end_date,'yyyymmdd')<=to_char(sysdate+3,'yyyymmdd')  order by begin_date,plan_seq ";
            else
                sql = "select plan_seq,product_model,plan_code,plan_so,plan_qty,online_qty,customer_name,begin_date,end_date,create_time,remark from data_plan where run_flag='Y' and pline_code='" + plinecode + "' and to_char(begin_date,'yyyy-mm-dd')<='" + LoginInfo.WorkDate + "' and to_char(end_date,'yyyy-mm-dd')>='" + LoginInfo.WorkDate + "'  order by begin_date,plan_seq ";

            string SaveStr = "";
            DataTable dt = dataConn.GetTable(sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    try
                    {
                        if (string.IsNullOrEmpty(dt.Rows[i][j].ToString()))
                        {
                            SaveStr = SaveStr + " ;";
                        }
                        else
                        {
                            SaveStr = SaveStr + dt.Rows[i][j].ToString() + ";";
                        }
                    }
                    catch
                    {
                        SaveStr = SaveStr + " ;";
                    }
                }
                SaveStr = SaveStr.Substring(0, SaveStr.Length - 1);
                SaveStr = SaveStr + (char)13 + (char)10;
            }
            PublicClass.Output_DayData(stationname, sn, "sjjhb", SaveStr);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                timer1.Enabled = false;
                if (!isManual)//自动上线
                {
                    string sql = "SELECT plan_seq,product_model,plan_code,plan_so,plan_qty,online_qty,customer_name,begin_date,end_date,create_time,remark "
                     + " FROM vw_data_plan_sn  Where run_flag='Y' and confirm_flag='Y' and plan_type!='C' and plan_type!='D' and pline_code='" + PlineCode + "' AND  plan_qty > online_qty AND to_char(end_date,'yyyymmdd')<=to_char(sysdate+30,'yyyymmdd')  AND to_char(begin_date,'yyyymmdd')>=to_char(sysdate-30,'yyyymmdd') and snflag='N' and sn <> '' and sn is not null order by begin_date,plan_seq,sn ";
                    DataTable dt = dataConn.GetTable(sql);
                    if (dt.Rows.Count == 0) return;
                    try
                    {
                        ThePlancode = dt.Rows[0]["plan_code"].ToString();
                        ThePlanso = dt.Rows[0]["plan_so"].ToString();
                        TheSn = dt.Rows[0]["snflag"].ToString();
                        ThePmodel = dt.Rows[0]["product_model"].ToString();
                        TheCustomer = dt.Rows[0]["customer_name"].ToString();
                    }
                    catch
                    {
                        ThePlancode = "";
                        ThePlanso = "";
                        TheSn = "";
                        ThePmodel = "";
                        TheCustomer = "";
                    }
                    if (TheSn != "")
                    {
                        //打印
                        if (ThePlancode != "" && ThePlanso != "" && ThePmodel != "")
                        {
                            string ms = dataConn.GetValue("select ms from atpumsb where so='" + ThePlanso + "'");
                            if (string.IsNullOrEmpty(ms))
                            {
                                DialogResult dlg = MessageBox.Show("当前SO打印面数没有指定,请手工指定,点击YES是1,点击NO是2!", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (dlg == DialogResult.No)
                                {
                                    ms = "2";
                                }
                                else
                                {
                                    ms = "1";
                                }
                            }
                            string jx = dataConn.GetValue("select GET_SXJX('" + ThePlanso + "') from dual ");
                            if (jx == "#")
                            {
                                jx = ThePmodel;
                            }
                            TxtPrtMs.Text = ms;
                            TxtPrtJx.Text = jx;
                            TxtPrtLsh.Text = TheSn;
                        }
                        else
                        {
                            MessageBox.Show("三项打印内容不能为空！", "提示");
                            return;
                        }

                        int ret = jwlStatus();
                        switch(ret)
                        {
                            case 1:
                                if (isManual || chkdy.Checked == false)
                                { }
                                else
                                {
                                    CmdPrttm_Click(this, e);
                                }
                                if (TxtPrtMs.Text != "1" && TxtPrtMs.Text != "2")
                                {
                                    MessageBox.Show("打印面数必须是1或者2！", "提示");
                                    return;
                                }
                                try
                                {
                                    int a = Convert.ToInt32(TxtPrtLsh.Text);
                                    int b = Convert.ToInt32(TxtPrtMs.Text);
                                }
                                catch
                                {
                                    MessageBox.Show("流水号必须是数字类型！", "提示");
                                    return;
                                }
                                autoPrint();//自动打印
                                break;
                            case 2:
                                MessageBox.Show("打印头为准备好！", "提示");
                                break;
                            default :
                                break;
                        }

                        arg.MessageHead = "FILLSN";
                        arg.MessageBody = TheSn;
                        SendDataChangeMessage(arg);
                    }
                    else
                    {
                        //无可用流水号
                    }
                }
                //timer1.Enabled = true;
            }
            catch
            {
                MessageBox.Show("打印出错","提示");
            }
        }
        private void Open_File(string file)
        {
            string strFile = "";
            strFile = file.Trim();
            int i = jwlOpenFile(strFile);
            switch (i)
            {
                case 1:
                    MessageBox.Show("文件不存在!", "提示");
                    break;
                case 2:
                    MessageBox.Show("非法!", "提示");
                    break;
                case 3:
                    MessageBox.Show("原点!", "提示");
                    break;
                case 4:
                    MessageBox.Show("设备不存在!", "提示");
                    break;
            }
        }
        private void autoPrint()
        {
            string dyjx = TxtPrtJx.Text.Trim();
            string dylsh = TxtPrtLsh.Text.Trim();
            int dyms = Convert.ToInt32(TxtPrtMs.Text.Trim());
            string dywjmc = "";
            if (dyjx.IndexOf(" ")>0)
            {
                dywjmc = dyjx.Replace(" ", "");
            }
            if (dyms == 1)
            {
                dywjmc = dywjmc + "_1.jwl";
                //调用第三方dll打开文件
                Open_File(dywjmc);
                string dylsh1 = "☆" + dylsh + "☆";
                string dystr = dylsh1 + "," + dyjx;
                //调用第三方dll打印
                int ret = jwlPrint(dystr);
                //判断返回标识
                switch (ret)
                {
                    case 0:
                        //MessageBox.Show("","提示");
                        break;
                    case 1:
                        MessageBox.Show("急停！", "提示");
                        return;
                    case 2:
                        MessageBox.Show("字库错误！有字符不在字库内！", "提示");
                        return;
                    case 3:
                        MessageBox.Show("超出标记范围", "提示");
                        return;
                    case 4:
                        MessageBox.Show("设备没有初始化！", "提示");
                        return;
                    case 5:
                        MessageBox.Show("没有指定打印文件", "提示");
                        return;
                    case 6:
                        MessageBox.Show("参数传递错误！打印字符过长！", "提示");
                        return;
                }
            }
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    if (i == 0)
                    {
                        dywjmc = dywjmc + "_1.jwl";
                        //调用第三方dll打开文件
                        Open_File(dywjmc);
                        string dylsh1 = "☆" + dylsh + "☆";
                        string dystr = dylsh1 + "," + dyjx;
                        //调用第三方dll打印
                        int ret = jwlPrint(dystr);
                        //判断返回标识
                        switch (ret)
                        {
                            case 0:
                                //MessageBox.Show("","提示");
                                break;
                            case 1:
                                MessageBox.Show("急停！", "提示");
                                return;
                            case 2:
                                MessageBox.Show("字库错误！有字符不在字库内！", "提示");
                                return;
                            case 3:
                                MessageBox.Show("超出标记范围", "提示");
                                return;
                            case 4:
                                MessageBox.Show("设备没有初始化！", "提示");
                                return;
                            case 5:
                                MessageBox.Show("没有指定打印文件", "提示");
                                return;
                            case 6:
                                MessageBox.Show("参数传递错误！打印字符过长！", "提示");
                                return;
                        }
                        Thread.Sleep(3000);
                    }
                    else if (i == 1)
                    {
                        dywjmc = dywjmc + "_2.jwl";
                        //调用第三方dll打开文件
                        Open_File(dywjmc);
                        //string dylsh1 = "☆" + dylsh + "☆";
                        string dystr = dylsh + "," + dyjx;
                        //调用第三方dll打印
                        int ret = jwlPrint(dystr);
                        //判断返回标识
                        switch (ret)
                        {
                            case 0:
                                //MessageBox.Show("","提示");
                                break;
                            case 1:
                                MessageBox.Show("急停！", "提示");
                                return;
                            case 2:
                                MessageBox.Show("字库错误！有字符不在字库内！", "提示");
                                return;
                            case 3:
                                MessageBox.Show("超出标记范围", "提示");
                                return;
                            case 4:
                                MessageBox.Show("设备没有初始化！", "提示");
                                return;
                            case 5:
                                MessageBox.Show("没有指定打印文件", "提示");
                                return;
                            case 6:
                                MessageBox.Show("参数传递错误！打印字符过长！", "提示");
                                return;
                        }
                    }
                }
            }

        }

        private void CmdPrttm_Click(object sender, EventArgs e)
        {
            if(TxtPrtLsh.Text!="")
            {
                //PublicClass.PrintTmFz(TxtPrtLsh.Text, ThePlanso, TheCustomer, "", ThePmodel, 2);
            }
        }

        private void cmdMPrt_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void cmdManual_Click(object sender, EventArgs e)
        {
            if (isManual) //手动改自动
            {
                isManual = false;
                groupBox1.Text = "自动上线";
                groupBox1.BackColor = Color.Blue;
                cmdManual.Text = "手工打印";
                timer1.Enabled = true;
                cmdMPrt.Enabled = false;
                CmdPrttm.Enabled = false;
                timer1_Tick(this, e);
            }
            else
            {
                isManual = true;
                groupBox1.Text = "暂停上线";
                groupBox1.BackColor = Color.Red;
                cmdManual.Text = "自动打印";
                TxtPrtJx.Text = "";
                TxtPrtLsh.Text = "";
                cmdMPrt.Enabled = true;
                CmdPrttm.Enabled = true;
                timer1.Enabled = false;
            }
        }

        private void cmdReset_Click(object sender, EventArgs e)
        {
            int ret=jwlReturnOrigin();
        }
    }
}
