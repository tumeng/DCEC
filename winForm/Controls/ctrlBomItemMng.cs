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


namespace Rmes.WinForm.Controls
{
    public partial class ctrlBomItemMng : BaseControl
    {
        private int selCount=0;
        public string planCode, orderCode,SN;
        public string companyCode, plineCode, stationCode,WorkunitCode,LinesideStock;
        private string oldVendorCode;
        public string VItemCode;

        public ctrlBomItemMng(string pPlanCode, string pSn, List<SNBomTempEntity> pDS)
        {
            InitializeComponent();
            stationCode = LoginInfo.StationInfo.RMES_ID;
            plineCode = LoginInfo.ProductLineInfo.RMES_ID;
            companyCode = LoginInfo.CompanyInfo.COMPANY_CODE;
            SN = pDS.First().SN;

            StationEntity ent1 = StationFactory.GetByKey(stationCode);
            WorkunitCode = "";
            LineSideStoreEntity ent2 = LinesideStoreFactory.GetByWKUnit(companyCode, WorkunitCode);
            LinesideStock = ent2.STORE_CODE;

            dgvBom.AutoGenerateColumns = false;
            dgvBom.RowHeadersVisible = false;
            
            dgvBom.DataSource = pDS;

        }
        
        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            dgvBom.Columns["colItemChecked"].ReadOnly = true;
            for (int i = 0; i < dgvBom.Rows.Count; i++)
            {
                dgvBom.Rows[i].Cells["colItemChecked"].Value = chkAll.Checked;
                
            }
            selCount = chkAll.Checked?dgvBom.Rows.Count:0;
            dgvBom.Columns["colItemChecked"].ReadOnly = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            (Parent as frmBomItemMng).Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (selCount == 0) return;
            progressBar1.Maximum = selCount + 1;
            progressBar1.Value = 0;
            for (int i = 0; i < dgvBom.Rows.Count; i++)
            {
                if (dgvBom.Rows[i].Cells["colItemChecked"].EditedFormattedValue.ToString() == "True")
                {
                    string rmes_id=dgvBom.Rows[i].Cells["colRmesID"].Value.ToString();
                    string strItemQty = dgvBom.Rows[i].Cells["colItemQty"].Value.ToString();
                    string item_code=dgvBom.Rows[i].Cells["colItemCode"].Value.ToString();
                    object obj_vendor_code = dgvBom.Rows[i].Cells["colVendorCode"].Value;
                    object obj_batch_code = dgvBom.Rows[i].Cells["colItemBatch"].Value;
            
                    float complete_qty = float.Parse(strItemQty);
                    string vendor_code = obj_vendor_code == null ? "" : obj_vendor_code.ToString();
                    string batch_code = obj_batch_code == null ? "" : obj_batch_code.ToString();
                    SNBomTempFactory.HandleBomItemComplete(companyCode, rmes_id,item_code, vendor_code, batch_code, complete_qty);
                    LineSideStockFactory.OutOfStorage(item_code, vendor_code, batch_code, LinesideStock, plineCode, complete_qty);
                    progressBar1.Value = progressBar1.Value + 1;
                }
            }
            progressBar1.Value = progressBar1.Maximum;
            MessageBox.Show("物料消耗数据批处理完成！");

            RMESEventArgs thisEventArg = new RMESEventArgs();
            thisEventArg.MessageHead = "BOM";
            thisEventArg.MessageBody = SN;
            SendDataChangeMessage(thisEventArg);
            (Parent as frmBomItemMng).Close();
        }

 
        private void dgvBom_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvBom.Columns[e.ColumnIndex].Name != "colItemChecked") return;
            string item_checked = dgvBom.Rows[e.RowIndex].Cells["colItemChecked"].EditedFormattedValue.ToString();
            string confirm_flag = dgvBom.Rows[e.RowIndex].Cells["colConfirmFlag"].Value.ToString();
            if(confirm_flag=="Y")
            {
                MessageBox.Show("此项物料已经消耗确认，不能撤消");
                return;
            }
            dgvBom.Columns["colItemChecked"].ReadOnly = true;
            if (item_checked == "False")
            {
                dgvBom.Rows[e.RowIndex].Cells["colItemChecked"].Value = true;
                selCount = selCount + 1;
            }
            else
            {
                dgvBom.Rows[e.RowIndex].Cells["colItemChecked"].Value = false;
                selCount = selCount - 1;
            }
         
            dgvBom.Columns["colItemChecked"].ReadOnly = false;

        }



    }
}
