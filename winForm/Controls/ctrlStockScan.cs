using System;

using System.Collections.Generic;

using System.ComponentModel;

using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rmes.WinForm.Base;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Factory;
using System.Reflection;
using System.Windows.Forms.VisualStyles;
using Rmes.WinForm.Controls.DataGridViewColumns;
using PetaPoco;

namespace Rmes.WinForm.Controls
{

    /// <summary>
    /// 线边库收料功能模块
    /// </summary>
    public partial class ctrlStockScan : BaseControl
    {
        private string LinesideStockCode, LinesideStockName;
        private string CompanyCode,WorkshopCode, PlineCode, StationCode,WorkunitCode;

        public ctrlStockScan()
        {
            InitializeComponent();
            gridLSS.AutoGenerateColumns = false;
            this.RMesDataChanged += new Rmes.WinForm.Base.RMesEventHandler(ctrlStockScan_RMesDataChanged);

            CompanyCode = LoginInfo.CompanyInfo.COMPANY_CODE;
            WorkshopCode = LoginInfo.WorkShopInfo.WORKSHOP_CODE;
            PlineCode = LoginInfo.ProductLineInfo.PLINE_CODE;
            StationCode = LoginInfo.StationInfo.STATION_CODE;
            
            
        }

        public void InitLinesideStock()
        {

            List<LineSideStockEntity> lst = LineSideStockFactory.GetByStoreCode(LinesideStockCode);
            gridLSS.DataSource = lst;

        }

        void ctrlStockScan_RMesDataChanged(object obj, RMESEventArgs e)
        {
            //if (e.MessageHead == "MESLL")
            //{
            //    WorkunitCode = StationFactory.GetByKey(StationCode).WORKUNIT_CODE;
            //    LineSideStoreEntity ent = LinesideStoreFactory.GetByWKUnit(CompanyCode, WorkunitCode);
                
            //    LinesideStockCode = ent.STORE_CODE;
            //    LinesideStockName = ent.STORE_NAME;
            //    InitLinesideStock();
            //    string txt = e.MessageBody.ToString();
            //    this.Visible = true;
            //    return;

            //    //testIssueReceive frm = new testIssueReceive(txt);
            //    //frm.receive += new testIssueReceive.IssueReceived(received);
            //    //frm.ShowDialog(this.Parent);
                
            //}

            //else if (e.MessageHead.ToString() == "WORK" || e.MessageHead.ToString() == "QUA")
            //{
            //    this.Visible = false;
            //    return;
            //}
        }

        private void btnReceive_Click(object sender, EventArgs e)
        {
            LineSideStockEntity ent = new LineSideStockEntity()
            {
                COMPANY_CODE = CompanyCode,
                PLINE_CODE = PlineCode,
                STORE_CODE = LinesideStockCode,
                STORE_NAME = LinesideStockName,
                LOCATION_CODE = WorkunitCode,
                WORKSHOP_CODE=WorkshopCode
            };
            FrmIssueReceived frmIssue = new FrmIssueReceived(ent);
            frmIssue.ShowDialog();
            InitLinesideStock();
        }   



 
        
       



    }
}
