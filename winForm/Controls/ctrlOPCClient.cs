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
using Rmes.DA.Factory;
using Rmes.DA.Entity;

namespace Rmes.WinForm.Controls
{
    public partial class ctrlOPCClient : BaseControl
    {
        Timer timer1;
        string CompanyCode, PlineCode, StationCode;
        public ctrlOPCClient()
        {
            InitializeComponent();
            CompanyCode = LoginInfo.CompanyInfo.COMPANY_CODE;
            PlineCode = LoginInfo.ProductLineInfo.RMES_ID;
            StationCode = LoginInfo.StationInfo.RMES_ID;

            timer1 = new Timer();
            timer1.Interval = 1000;
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Start();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            OPCMessageEntity ent = OPCMessageFactory.GetMyMessage();
            if (ent != null)
            {
                if (ent.MESSAGE_CODE == "sn") if(!DealSn(ent.MESSAGE_VALUE)) return;
                
                RMESEventArgs arg = new RMESEventArgs();
                arg.MessageHead = ent.MESSAGE_CODE=="sn"?"SN":ent.MESSAGE_CODE;
                arg.MessageBody = ent.MESSAGE_VALUE;

                SendDataChangeMessage(arg);
                ent.MESSAGE_FLAG = "R";
                OPCMessageFactory.UpdateMessage(ent);
                
            }
            timer1.Start(); 
        }

        private bool DealSn(string pSn)
        {
            string sn = pSn;
            RMESEventArgs arg = new RMESEventArgs();
            PlanSnEntity plansn1 = PlanSnFactory.GetBySn(sn);
            if (plansn1 == null) return false;
            arg.MessageHead = plansn1.SN_FLAG == "N" ? "ONLINE" : "SN";
            string plan_code = plansn1.PLAN_CODE;
            string quality_status = "A", complete_flag = "0";

            List<ProductCompleteEntity> prc = ProductCompleteFactory.GetByPlanSn(CompanyCode, PlineCode, plan_code, sn, StationCode);
            if (prc.Count > 0) complete_flag = prc.First<ProductCompleteEntity>().COMPLETE_FLAG;
            if (complete_flag == "1")
            {
                MessageBox.Show("此项装配任务已经完成下线");
                return false;
            }
            List<ProductDataEntity> prd = ProductDataFactory.GetProductDataByPlanSn(CompanyCode, PlineCode, plan_code, sn);
            //if (prd.Count > 0) quality_status = prd.First<ProductDataEntity>().QUALITY_STATUS;
            if (quality_status == "B")
            {
                DialogResult dlg = MessageBox.Show("此项装配工件质量不合格，要继续装配吗", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlg == DialogResult.No) return false;
            }
            return true;

        }
    }
}
