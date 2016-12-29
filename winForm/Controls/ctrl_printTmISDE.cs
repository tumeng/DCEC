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
    public partial class ctrl_printTmISDE : BaseControl
    {
        //条码打印 通用的条码打印界面
        private string CompanyCode, PlineID, PlineCode1, StationID, len_sn, TheSo, TheSn, ThePlancode, StationName, Station_Code,isde_plancode,isde_sn;
        private string ShiftCode, TeamCode, UserID;
        ProductInfoEntity product;
        //dataConn dc = new dataConn();
        public ctrl_printTmISDE()
        {
            InitializeComponent();
            //this.RMesDataChanged += new Rmes.WinForm.Base.RMesEventHandler(ctrlProductScan_RmesDataChanged);

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
        public ctrl_printTmISDE(string sn12,string plancode12)
        {
            InitializeComponent();
            //this.RMesDataChanged += new Rmes.WinForm.Base.RMesEventHandler(ctrlProductScan_RmesDataChanged);

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
            isde_plancode = plancode12;
            isde_sn = sn12;
            TxtPrtSo.Text = plancode12;
            TxtPrtGhtm.Text = sn12;
        }
        protected void ctrlProductScan_RmesDataChanged(object obj, Rmes.WinForm.Base.RMESEventArgs e)
        {
            string sn, quality_status = "A";
            RMESEventArgs arg = new RMESEventArgs();
            arg.MessageHead = "";
            arg.MessageBody = "";
            if (e.MessageHead == null) return;

        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.ParentForm.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.ParentForm.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (TxtPrtGhtm.Text.Trim() == "" || TxtPrtSo.Text.Trim() == "")
            {
                MessageBox.Show("打印内容不能为空","提示");
                return;
            }
            PublicClass.PrintTmISDE(TxtPrtSo.Text.Trim().ToUpper(), TxtPrtGhtm.Text.Trim().ToUpper());
            this.ParentForm.Close();
        }
    }
}
