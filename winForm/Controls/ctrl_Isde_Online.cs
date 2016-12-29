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
    //分装线ISDE自动上线模块 显示控件为缸体号txt控件
    public partial class ctrl_Isde_Online : BaseControl
    {
        private string CompanyCode, PlineID, PlineCode1, StationID, len_sn, TheSo, TheSn, ThePlancode,ThePmodel,TheEwm,ThisSmTm, StationName, Station_Code;
        private string ShiftCode, TeamCode, UserID;
        ProductInfoEntity product;
        RMESEventArgs arg = new RMESEventArgs();

        //dataConn dc = new dataConn();
        public ctrl_Isde_Online()
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
            ThisSmTm = "";
            ThePmodel = "";
            ThePlancode = "";
            TheEwm = "";
            arg.MessageHead = "";
            arg.MessageBody = "";
            timer1.Enabled = true;
            timer1.Start();
        }

        protected void ctrlProductScan_RmesDataChanged(object obj, Rmes.WinForm.Base.RMESEventArgs e)
        {
            string sn, quality_status = "A";

            if (e.MessageHead == null) return;
            if (e.MessageHead == "SN")
            {
                //上线后打印条码
                sn = e.MessageBody.ToString();
                product = ProductInfoFactory.GetByCompanyCodeSNSingle(CompanyCode, sn);//获取sn信息
                ThePlancode = product.PLAN_CODE;
                TheSo = product.PLAN_SO;
                PublicClass.PrintTmISDE(product.PLAN_CODE, ThisSmTm);
                txtGTH.Text = "";
                arg.MessageHead = "CLEAR1";
                arg.MessageBody = "";
                SendDataChangeMessage(arg);
                timer1.Enabled = true;
            }
            if (e.MessageHead == "ONLINEONLINE")
            {
                DataTable dt = dataConn.GetTable("select plan_code from data_plan where plan_qty<=online_qty and plan_code='" + ThePlancode + "'");
                if (dt.Rows.Count > 0)
                    ProductDataFactory.ISDE_CREATE_JK_SJJHB(PlineCode1);
                timer1.Enabled = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                timer1.Enabled = false;
                string sql = "select jhmc from isde_jk_sjjhb ";
                DataTable dt = dataConn.GetTable(sql);
                if (dt.Rows.Count == 0)
                {
                    ProductDataFactory.ISDE_CREATE_JK_SJJHB(PlineCode1);
                }

                sql = " select ewghtm,jhdm,jhso,ggxhmc from isde_jk_sjsxb where czzt='0' and rownum=1 ";
                dt = dataConn.GetTable(sql);
                if (dt.Rows.Count == 0)
                {
                    timer1.Enabled = true;
                    return;
                }
                TheEwm = dt.Rows[0][0].ToString();
                ThePlancode = dt.Rows[0][1].ToString();
                TheSo = dt.Rows[0][2].ToString();
                ThePmodel = dt.Rows[0][3].ToString();
                if (ThePlancode == "")
                {
                    timer1.Enabled = true;
                    return;
                }
                //初始化sn框
                TheEwm = TheEwm.Replace("\r\n", "").Trim();
                txtGTH.Text = TheEwm;
                string sn = TheEwm;
                TheSn = sn;
                ThisSmTm = TheSo + "^DCEC^" + sn;
                arg.MessageHead = "FILLSN";
                arg.MessageBody = sn + "^" + TheSo + "^" + ThePmodel + "^" + ThePlancode;
                SendDataChangeMessage(arg);
                arg.MessageHead = "FILLSN1";
                arg.MessageBody = ThisSmTm;
                SendDataChangeMessage(arg);
                arg.MessageHead = "PRINTISDE";
                arg.MessageBody = ThisSmTm + "|" + ThePlancode;
                SendDataChangeMessage(arg);

                //arg.MessageHead = "FILLSN2";
                //arg.MessageBody = TheSo + "^" + ThePmodel + "^" + ThePlancode;
                //SendDataChangeMessage(arg);
            }
            catch
            {
                timer1.Enabled = true;
            }
        }

        private void txtGTH_KeyPress(object sender, KeyPressEventArgs e)
        {
            //二维缸体号回车事件
            if (e.KeyChar != 13) return;
            if (string.IsNullOrWhiteSpace(txtGTH.Text)) return;
            if (ThePlancode == "")
            {
                MessageBox.Show("请先扫描流水号","提示");
                return;
            }
            if (txtGTH.Text.IndexOf('^') > 0)
            {
                txtGTH.Text = GetLsh(txtGTH.Text);//扫的是零件条码 取其中最右的零件流水号
            }
            string sn = txtGTH.Text;
            TheSn = sn;
            ThisSmTm = TheSo + "^DCEC^" + sn;
            arg.MessageHead = "FILLSN1";
            arg.MessageBody = ThisSmTm;
            SendDataChangeMessage(arg);

            arg.MessageHead = "PRINTISDE";
            arg.MessageBody = ThisSmTm + "|" + ThePlancode;
            SendDataChangeMessage(arg);
        }

        private string GetLsh(string gtm)
        {
            int i = gtm.IndexOf('^');
            if (i <= 1)
            {
                return "";
            }
            string[] strAry = gtm.Split('^');
            string strLjdm = "", strGys = "", strlsh = "";
            if (strAry.Length == 2)
            { 
                strLjdm=strAry[0];
                strGys=strAry[1];
            }
            else if (strAry.Length == 3)
            {
                strLjdm = strAry[0];
                strGys = strAry[1];
                strlsh=strAry[2];
            }

            return strlsh;
        }


    }
}
