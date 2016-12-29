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

namespace Rmes.WinForm.Controls
{
    public partial class ctrProductComplete : BaseControl
    {
        public string PlanCode="",ProductCode = "",SN="",QualityStatus="A";
        public string CompanyCode = "", PlineCode = "", StationCode = "";
        public string ShiftCode="",TeamCode="",UserID="";

        public ctrProductComplete()
        {
            InitializeComponent();
            PlineCode = LoginInfo.ProductLineInfo.RMES_ID;
            StationCode = LoginInfo.StationInfo.RMES_ID;
            CompanyCode = LoginInfo.CompanyInfo.COMPANY_CODE;
            ShiftCode=LoginInfo.ShiftInfo.SHIFT_CODE;
            TeamCode = LoginInfo.TeamInfo.TEAM_CODE;
            UserID = LoginInfo.UserInfo.USER_ID;

            btnFinishedD.BackColor = QualityStatus == "A" ? Color.DarkGreen : Color.DarkRed;


            this.RMesDataChanged += new Rmes.WinForm.Base.RMesEventHandler(ctrProductComplete_RMesDataChanged);
        }

        void ctrProductComplete_RMesDataChanged(object obj, Rmes.WinForm.Base.RMESEventArgs e)
        {
            if (e.MessageHead.ToString() == "MESLL")
            {
                this.Visible = false;
                return;
            }
            if (e.MessageHead.ToString() == "WORK")
            {
                this.Visible = true;
                return;
            }
            if (e.MessageHead.ToString() == "SN")
            {
                SN = e.MessageBody.ToString();
                SetMenuItemStatus(true);

                List<ProductInfoEntity> product = ProductInfoFactory.GetByCompanyCodeSN(CompanyCode, SN);
                if (product.Count > 0) PlanCode = product.First().PLAN_CODE;
            }
            else if (e.MessageHead.ToString() == "PLAN")
            {
                PlanCode = e.MessageBody.ToString();
                SetMenuItemStatus(true);
            }

            else if (e.MessageHead == "SCP" || e.MessageHead == "OFFLINE")
            {
                SetMenuItemStatus(false);
                ItemQualifiedOffline.Enabled = false;
                ItemFailedOffline.Enabled = false;
            }
            else if (e.MessageHead == "OFFCTRL")
            {
                ItemQualifiedOffline.Enabled = true;
                ItemFailedOffline.Enabled = true;
            }
           
        }

        void SetMenuItemStatus(bool ItemStatus)
        {
            ItemQualified.Enabled = ItemStatus;
            ItemFailed.Enabled = ItemStatus;
            ItemPause.Enabled = ItemStatus;
        }
 
        private void btnFinishedD_Click(object sender, EventArgs e)
        {
            mnuComplete.Show(btnFinishedD, new Point(0, 0));
        }


        private void ItemQulified_Click(object sender, EventArgs e)
        {
            List<SNControlEntity> list_control = SNControlFactory.GetByPlanStatonSn(CompanyCode, PlineCode, StationCode, PlanCode, SN);
            foreach (SNControlEntity lst in list_control)
            {
                string control_script = list_control.First<SNControlEntity>().CONTROL_SCRIPT;
                if (lst.CONTROL_SCRIPT == "工序完工模块")
                {
                    MessageBox.Show("工序完工模块没有操作完成，请重新进行");
                    return;
                }
                
                DialogResult drt = MessageBox.Show(control_script+"没有报完成，要继续吗？", "提示", MessageBoxButtons.YesNo);
                if (drt == DialogResult.No) return;

            }
            
           
            ProductSnFactory.HandleStationComplete(CompanyCode, PlineCode, StationCode, PlanCode, SN,  "A");
            RMESEventArgs args = new RMESEventArgs();
            args.MessageHead = "SCP";
            SendDataChangeMessage(args);

        }

        private void ItemFailed_Click(object sender, EventArgs e)
        {
            //List<SNControlEntity> list_control = SNControlFactory.GetByPlanStatonSn(CompanyCode, PlineCode, StationCode, PlanCode, SN);
            List<SNControlEntity> list_control = SNControlFactory.GetByPlanStatonSn(CompanyCode, PlineCode, StationCode, PlanCode, SN);
            if (list_control.Count > 0)
            {
                string control_script = list_control.First<SNControlEntity>().CONTROL_SCRIPT;
                DialogResult drt = MessageBox.Show(control_script + "没有报完成，要继续吗？", "提示", MessageBoxButtons.YesNo);
                if (drt == DialogResult.No) return;
            }
            ProductSnFactory.HandleStationComplete(CompanyCode, PlineCode, StationCode, PlanCode, SN,  "B");
            RMESEventArgs args = new RMESEventArgs();
            args.MessageHead = "SCP";
            SendDataChangeMessage(args);
           
        }
        private void ItemDiscard_Click(object sender, EventArgs e)
        {

        }

        private void ItemQualifiedOffline_Click(object sender, EventArgs e)
        {
            RMESEventArgs args = new RMESEventArgs();
            args.MessageHead = "OFFLINE";
            args.MessageBody = SN+"^A";
            SendDataChangeMessage(args);
        }

        private void ItemFailedOffline_Click(object sender, EventArgs e)
        {
            RMESEventArgs args = new RMESEventArgs();
            args.MessageHead = "OFFLINE";
            args.MessageBody = SN + "^B";
            SendDataChangeMessage(args);
        }

        private void ItemPause_Click(object sender, EventArgs e)
        {
            ProductSnFactory.HandleStationPause(CompanyCode, PlineCode, StationCode, PlanCode, SN, "A");
            RMESEventArgs args = new RMESEventArgs();
            args.MessageHead = "PAUSE";
            args.MessageBody = SN + "^P";
            SendDataChangeMessage(args);
        }

    

       
        
    }
}
