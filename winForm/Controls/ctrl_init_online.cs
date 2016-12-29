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
    public partial class ctrl_init_online : BaseControl
    {
        private string CompanyCode, PlineCode, PlineCode1, StationID, len_sn, TheCustomer, TheSn, ThePlancode, ThePlanso, ThePmodel, StationName, Station_Code, Workdate;
        private string ShiftCode, TeamCode, UserID, UserCode;
        private bool isManual = false;
        private RMESEventArgs arg = new RMESEventArgs();
        //dataConn dc = new dataConn();
        ProductInfoEntity product;

        public ctrl_init_online()
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

            //保存计划信息
            Savedata(StationName, "", PlineCode1, Workdate);
            //timer1.Enabled = true;
            //timer1.Start();
            //timer1_Tick(timer1, new EventArgs());
            arg.MessageHead = "";
            arg.MessageBody = "";
        }
        protected void ctrlInitOnline_RmesDataChanged(object obj, Rmes.WinForm.Base.RMESEventArgs e)
        {
            if (e.MessageHead == null) return;
            if (e.MessageHead == "ONLINEONLINE")
            {
                //上线完成 自动刷下一个 打印条码
                string sn = e.MessageBody.ToString();
                product = ProductInfoFactory.GetByCompanyCodeSNSingle(CompanyCode, sn);//获取sn信息
                if (product == null) return;
                //if (check1.Checked)
                if (LoginInfo.ProductLineInfo.PLINE_CODE == "Z")
                {
                    PublicClass.PrintTmFz(sn, product.PLAN_SO, product.CUSTOMER_NAME, "", product.PRODUCT_MODEL, 2, product.PLAN_CODE);
                }
                else
                {
                    PublicClass.PrintTmFz(sn, product.PLAN_SO, product.CUSTOMER_NAME, "", product.PRODUCT_MODEL, 2, product.PLAN_CODE);
                }
                //timer1.Enabled = true;
                //timer1.Start();
                //timer1_Tick(timer1, new EventArgs());
            }
        }
        private void Savedata(string stationname, string sn, string plinecode, string workdate)
        {
            string sql = "";
            if (plinecode == "E")
                sql = "select plan_seq,product_model,plan_code,plan_so,plan_qty,online_qty,customer_name,begin_date,end_date,create_time,remark from data_plan where confirm_flag='Y' and plan_type!='C' and plan_type!='D' and run_flag='Y' and pline_code='" + plinecode + "' and to_char(begin_date,'yyyymmdd')>=to_char(sysdate-3,'yyyymmdd') and to_char(end_date,'yyyymmdd')<=to_char(sysdate+3,'yyyymmdd')  order by begin_date,plan_seq ";
            else
                sql = "select plan_seq,product_model,plan_code,plan_so,plan_qty,online_qty,customer_name,begin_date,end_date,create_time,remark from data_plan where confirm_flag='Y' and plan_type!='C' and plan_type!='D' and run_flag='Y' and pline_code='" + plinecode + "' and to_char(begin_date,'yyyy-mm-dd')<='" + LoginInfo.WorkDate + "' and to_char(end_date,'yyyy-mm-dd')>='" + LoginInfo.WorkDate + "'  order by begin_date,plan_seq ";

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
                timer1.Stop();
                if (!isManual)//自动上线
                {
                    string sql = "SELECT sn,plan_seq,product_model,plan_code,plan_so,plan_qty,online_qty,customer_name,begin_date,end_date,create_time,remark "
                     + " FROM vw_data_plan_sn  Where run_flag='Y' and confirm_flag='Y' and plan_type!='C' and plan_type!='D' and pline_code='" + PlineCode1 + "' AND  plan_qty > online_qty AND to_char(end_date,'yyyymmdd')<=to_char(sysdate+30,'yyyymmdd')  AND to_char(begin_date,'yyyymmdd')>=to_char(sysdate-30,'yyyymmdd') and snflag='N'   and sn is not null order by begin_date,plan_seq,plan_code,sn ";
                    DataTable dt = dataConn.GetTable(sql);
                    if (dt.Rows.Count == 0)
                    {
                        timer1.Enabled = true;
                        timer1.Start();
                        return;
                    }
                    try
                    {
                        ThePlancode = dt.Rows[0]["plan_code"].ToString();
                        ThePlanso = dt.Rows[0]["plan_so"].ToString();
                        TheSn = dt.Rows[0]["sn"].ToString();
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
                        arg.MessageHead = "FILLSN";
                        arg.MessageBody = TheSn + "^" + ThePlanso + "^" + ThePmodel + "^" + ThePlancode;
                        SendDataChangeMessage(arg);
                    }
                    else
                    {
                        MessageBox.Show("流水号不合法","提示");
                        timer1.Enabled = true;
                        timer1.Start();
                        //无可用流水号
                    }
                }
                //timer1.Enabled = true;
            }
            catch
            {
                MessageBox.Show("获取流水号出错", "提示");
            }
        }

    }
}
