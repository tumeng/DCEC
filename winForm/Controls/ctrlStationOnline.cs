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
using Rmes.Pub.Data1;

//上线处理控件，接收SN传来的未上线消息

namespace Rmes.WinForm.Controls
{
    public partial class ctrlStationOnline : BaseControl
    {
        private string CompanyCode, PlineCode, PlineCode1, StationID, len_sn, TheSo, TheSn, ThePlancode, StationName, Station_Code,Workdate;
        private string ShiftCode, TeamCode, UserID, UserCode;
        //dataConn dc = new dataConn();
        public ctrlStationOnline()
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
            UserCode= LoginInfo.UserInfo.USER_CODE;
            len_sn = LoginInfo.LEN_SN;
            Workdate = LoginInfo.WorkDate;
            this.RMesDataChanged += new Rmes.WinForm.Base.RMesEventHandler(ctrlStationOnline_RmesDataChanged);
        }

        protected void ctrlStationOnline_RmesDataChanged(object obj, Rmes.WinForm.Base.RMESEventArgs e)
        {
            string plan_code, sn,order_code,sn_flag;
            if (e.MessageHead == null) return;
            if (e.MessageHead == "ONLINE")
            {
                //DialogResult dlg=MessageBox.Show("是否上线", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ;
                //if (dlg == DialogResult.No) return;

                sn=e.MessageBody.ToString();
                
                //获取当前第一条计划的未上线流水号，打印流水号
                //流水号做上线
                PlanSnEntity ent_sn = PlanSnFactory.GetBySn(sn);
                if (ent_sn == null)//如果没有sn，则msgbd为plancode
                {
                    plan_code = "";
                    sn = "";
                    MessageBox.Show("流水号非法！","提示");
                    //sn = PlanFactory.GetByKey(plan_code).PLAN_BATCH;
                }
                else
                {
                    //order_code = PlanSnFactory.GetBySn(sn).ORDER_CODE;
                    //plan_code = PlanFactory.GetByOrder(order_code).PLAN_CODE;
                    plan_code = ent_sn.PLAN_CODE;
                }
                
                ProductDataFactory.Station_OnLine(plan_code, sn,DateTime.Now.ToString("yyyy-MM-dd"),LoginInfo.StationInfo.STATION_CODE,LoginInfo.ShiftInfo.SHIFT_CODE,LoginInfo.TeamInfo.TEAM_CODE,LoginInfo.UserInfo.USER_CODE);

                RMESEventArgs arg = new RMESEventArgs();
                arg.MessageHead = "SN";
                arg.MessageBody = sn;
                SendDataChangeMessage(arg);
            }
           

        }



        
    }
}
