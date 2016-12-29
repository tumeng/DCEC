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

namespace Rmes.WinForm.Controls
{
    public partial class ctrl_initCLonline : BaseControl
    {
        //CL分装线 初始化上线 根据计划取第一条作为上线计划，绑定扫描的条码
        private string CompanyCode, PlineID, PlineCode1, StationID, len_sn, TheSo, TheSn, ThePlancode,ThisSmTm="", TheProductModel="",StationName, Station_Code;
        private string ShiftCode, TeamCode, UserID;
        ProductInfoEntity product;
        //dataConn dc = new dataConn();
        public ctrl_initCLonline()
        {
            InitializeComponent();
            this.RMesDataChanged += new Rmes.WinForm.Base.RMesEventHandler(ctrlProductScan_RmesDataChanged);

            CompanyCode = LoginInfo.CompanyInfo.COMPANY_CODE;
            PlineID = LoginInfo.ProductLineInfo.RMES_ID;
            PlineCode1 = LoginInfo.ProductLineInfo.PLINE_CODE;
            StationID = LoginInfo.StationInfo.RMES_ID;
            Station_Code = LoginInfo.StationInfo.STATION_CODE;
            StationName = LoginInfo.StationInfo.STATION_NAME;
            ShiftCode = LoginInfo.ShiftInfo.SHIFT_CODE;
            TeamCode = LoginInfo.TeamInfo.TEAM_CODE;
            UserID = LoginInfo.UserInfo.USER_ID;
            len_sn = LoginInfo.LEN_SN;
            TheSo = "";
            TheSn = "";
            ThePlancode = "";
        }
        protected void ctrlProductScan_RmesDataChanged(object obj, Rmes.WinForm.Base.RMESEventArgs e)
        {
            string sn, quality_status = "A";
            RMESEventArgs arg = new RMESEventArgs();
            arg.MessageHead = "";
            arg.MessageBody = "";
            if (e.MessageHead == null) return;
            if (e.MessageHead == "SN")
            {
                //上线后打印条码
                sn = e.MessageBody.ToString();
                product = ProductInfoFactory.GetByCompanyCodeSNSingle(CompanyCode, sn);//获取sn信息
                ThisSmTm = product.PLAN_SO + "^DCEC^" + sn;
                PublicClass.PrintTmISDE(product.PLAN_CODE, ThisSmTm);

                arg.MessageHead = "CLEAR1";
                arg.MessageBody = "";
                SendDataChangeMessage(arg);
                timer1.Enabled = true;
            }
            if (e.MessageHead == "REFRESHCLPLAN")
            {
                timer1.Enabled = true;
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            //刷新计划数据
            string sql = "SELECT plan_seq,product_model,plan_code,plan_so,plan_qty,online_qty,offline_qty,customer_name,begin_date,end_date,create_time,remark "
                 + " FROM data_plan  Where run_flag='Y' and confirm_flag='Y' and pline_code='" + PlineCode1 + "' AND  plan_qty > online_qty AND to_char(end_date,'yyyymmdd')<=to_char(sysdate+30,'yyyymmdd')  AND to_char(begin_date,'yyyymmdd')>=to_char(sysdate-30,'yyyymmdd')  order by begin_date,plan_seq ";
            DataTable dt = dataConn.GetTable(sql);
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("当前没有可执行的计划","提示");
                return;
            }
            ThePlancode = dt.Rows[0]["PLAN_CODE"].ToString();
            TheSo = dt.Rows[0]["PLAN_SO"].ToString();
            TheProductModel = dt.Rows[0]["PRODUCT_MODEL"].ToString();

            //显示计划对应的BOM
            RMESEventArgs arg = new RMESEventArgs();
            arg.MessageHead = "CL_PLAN";
            arg.MessageBody = ThePlancode + "^" + TheSo + "^" + TheProductModel;
            SendDataChangeMessage(arg);

            //关联流水号

        }




    }
}
