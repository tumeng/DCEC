using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;
using Rmes.WinForm.Base;
using Rmes.Pub.Data1;

namespace Rmes.WinForm.Controls
{
    public partial class ctrPlanSn : BaseControl
    {
        public int Batch_Qty=1;
        public string Batch_Code = "";
        public string  PlineCode,CompanyCode,StationCode;
        
        public ctrPlanSn(string planId,string batchCode)
        {
            InitializeComponent();
            Batch_Code=batchCode;
            CompanyCode = LoginInfo.CompanyInfo.COMPANY_CODE;
            PlineCode = LoginInfo.ProductLineInfo.RMES_ID ;
            StationCode = LoginInfo.StationInfo.STATION_CODE;

            List<PlanSnEntity> listSnData = PlanSnFactory.GetSnByPlanCode(planId);
            string adstatus = "A";
            try
            {
                //dataConn dc = new dataConn();
                adstatus = dataConn.GetValue(" select increase_flag from code_sn where pline_code='" + LoginInfo.ProductLineInfo.PLINE_CODE + "' ");
                //dataConn.CloseConn();
            }
            catch { }
            if (adstatus != "A")
            {
                listSnData = PlanSnFactory.GetSnByPlanCodedesc(planId);
            }


            listSn.Items.Clear();
            int i=0;
            int j = 1;
            foreach (PlanSnEntity se in listSnData)
            {
                listSn.Items.Add((j++)+"--"+se.SN+"--"+se.SN_FLAG);
                if(se.SN_FLAG!="N")
                    i++;
            }
            label1.Text = "计划流水号总数：" + listSnData.Count + ";已上线计划流水号数量：" + i;
        }
         private void btnCancel_Click(object sender, EventArgs e)
         {
             (Parent as FrmShowPlanSn).Close();
         }


    }

}
