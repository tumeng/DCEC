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
    public partial class ctrl_init_LqOnline : BaseControl
    {
        //柳汽上线模块
        private string CompanyCode, PlineCode, PlineCode1, StationID, len_sn, TheCustomer, TheSn, ThePlancode, ThePlanso, ThePmodel, StationName, Station_Code, Workdate;
        private string ShiftCode, TeamCode, UserID, UserCode;
        private bool isManual = false;
        private RMESEventArgs arg = new RMESEventArgs();
        //dataConn dc = new dataConn();
        ProductInfoEntity product;
        public ctrl_init_LqOnline()
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
            if (e.MessageHead == "ONLINE")
            {
                //上线完成 自动刷下一个 打印条码
                string sn = e.MessageBody.ToString();
                PlanSnEntity ent = PlanSnFactory.GetBySnPline(sn, PlineCode1);//获取sn信息
                product = ProductInfoFactory.GetByCompanyCodeSNSingle(CompanyCode, sn);//获取sn信息
                
                if (product == null) return;
                //if (check1.Checked)
                    PublicClass.PrintTmFzSx(sn, product.PLAN_SO, product.CUSTOMER_NAME, "", product.PRODUCT_MODEL, 1);
                //timer1.Enabled = true;
                //timer1.Start();
                //timer1_Tick(timer1, new EventArgs());
            }
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
    }
}
