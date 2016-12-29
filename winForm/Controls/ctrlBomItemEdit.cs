using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Factory;
using Rmes.WinForm.Base;
using Rmes.Pub.Data1;

namespace Rmes.WinForm.Controls
{
    public partial class ctrlBomItemEdit :BaseControl
    {
        private string CompanyCode, PlineCode = "", LocationCode = "", PlanCode = "", ProductSn = "", Type1 = "", Type2 = "",newBarcode="";
        private string BomRmesID="";
        private string VendorCode="", BatchCode="";
        private string OprType;//操作模式(新增或者编辑
        //private dataConn dc = new dataConn();
        //private dataConnVb dcVb = new dataConnVb();
        public ctrlBomItemEdit(string RmesID)
        {
            InitializeComponent();
            PlineCode=LoginInfo.ProductLineInfo.PLINE_CODE;
            CompanyCode = LoginInfo.CompanyInfo.COMPANY_CODE;
            
            BomRmesID = RmesID;
            
            InitItemInfo(RmesID);
        }
        public ctrlBomItemEdit(string RmesID,string batchcode1)
        {
            InitializeComponent();
            PlineCode = LoginInfo.ProductLineInfo.PLINE_CODE;
            CompanyCode = LoginInfo.CompanyInfo.COMPANY_CODE;

            BomRmesID = RmesID;
            BatchCode = batchcode1;
            InitItemInfo(RmesID);
        }
        public ctrlBomItemEdit()
        {
            InitializeComponent();
            PlineCode = LoginInfo.ProductLineInfo.PLINE_CODE;
            CompanyCode = LoginInfo.CompanyInfo.COMPANY_CODE;
            chkAddNew.Checked = true;
            //ProcessFactory.GetByWorkUnit();

        }
        public ctrlBomItemEdit(string pRmesID,string pVendorCode,string pBatchCode,string type,string barcode1)
        {
            InitializeComponent();
            PlineCode = LoginInfo.ProductLineInfo.PLINE_CODE;
            CompanyCode = LoginInfo.CompanyInfo.COMPANY_CODE;
            Type1 = type;
            BomRmesID = pRmesID;
            VendorCode = pVendorCode;
            BatchCode = pBatchCode;
            newBarcode = barcode1;

            txtProcessCode.Enabled = false;
            txtItemCode.Enabled = false;
            txtItemQty.Enabled = false;
            chkKeyItem.Enabled = false;
            txtVendorCode.Enabled = false;
            txtCompleteQty.Enabled = false;
            txtBatchCode.Enabled = false;

            if (type == "A")
                InitItemInfo(pRmesID,"A");
            if (type == "B")
                InitItemInfo(pRmesID,"B");
            if (type == "D")
                InitItemInfo(pRmesID,"D");
        }
        private void InitItemInfo(string RmesID)
        {
            SNBomTempEntity ent_bom = SNBomTempFactory.GetByKey(RmesID);
            PlineCode = ent_bom.PLINE_CODE;
            LocationCode = ent_bom.LOCATION_CODE;
            PlanCode = ent_bom.PLAN_CODE;
            ProductSn = ent_bom.SN;

            txtItemCode.Text = ent_bom.ITEM_CODE;
            txtProcessCode.Text = ent_bom.PROCESS_CODE;
            txtVendorCode.Text = VendorCode;// == "" ? ent_bom.VENDOR_CODE : VendorCode;
            txtItemQty.Text = ent_bom.ITEM_QTY.ToString();
            txtCompleteQty.Text = ent_bom.COMPLETE_QTY.ToString();
            txtBatchCode.Text = BatchCode;// ent_bom.ITEM_BATCH;

            string item_type = ent_bom.ITEM_TYPE;
            if (item_type == "A")
            {
                button1.Visible = false;
                chkKeyItem.Checked = true;
                LoadItemSn(RmesID);
                //if (BatchCode != "") lstSn.Items.Add(BatchCode);
            }
            else if (item_type == "B")
            {
                if (txtItemQty.Text == "1")
                {
                    button1.Visible = false;
                }
                else
                {
                    button1.Visible = true;
                }
                chkKeyItem.Checked = true;
                LoadItemSn1(RmesID);
            }
            else if (item_type == "D")
            {
                button1.Visible = false;
                chkKeyItem.Checked = true;
                LoadItemSn(RmesID);
            }
        }
        
        private void InitItemInfo(string RmesID,string type1)
        {
            SNBomTempEntity ent_bom = SNBomTempFactory.GetByKey(RmesID);
            PlineCode=ent_bom.PLINE_CODE;
            LocationCode=ent_bom.LOCATION_CODE;
            PlanCode=ent_bom.PLAN_CODE;
            ProductSn=ent_bom.SN;
            
            txtItemCode.Text = ent_bom.ITEM_CODE;
            txtProcessCode.Text = ent_bom.PROCESS_CODE;
            txtVendorCode.Text = VendorCode;// == "" ? ent_bom.VENDOR_CODE : VendorCode;
            txtItemQty.Text = ent_bom.ITEM_QTY.ToString();
            txtCompleteQty.Text = ent_bom.COMPLETE_QTY.ToString();
            txtBatchCode.Text = BatchCode;// ent_bom.ITEM_BATCH;

            string item_type = ent_bom.ITEM_TYPE;
            Type2 = item_type;
            if (item_type == "A")
            {
                button1.Visible = false;
                chkKeyItem.Checked = true;
                LoadItemSn(RmesID);
                //if (BatchCode != "") lstSn.Items.Add(BatchCode);
            }
            else if (item_type == "B")
            {
                if (txtItemQty.Text == "1")
                {
                    button1.Visible = false;
                }
                else
                {
                    button1.Visible = true;
                }
                chkKeyItem.Checked = true;
                LoadItemSn1(RmesID);
            }
            else if (item_type == "D")
            {
                button1.Visible = false;
                chkKeyItem.Checked = true;
                LoadItemSn(RmesID);
            }
        }

        private void LoadItemSn(string RmesID)
        {
            List<SNBomItemSnEntity> lstsn = new List<SNBomItemSnEntity>();
            lstsn = SNBomItemSnFactory.GetByRmesID(RmesID);
            foreach(SNBomItemSnEntity itemsn in lstsn)
            {
                lstSn.Items.Add(txtItemCode.Text+"^"+itemsn.ITEM_VENDOR+"^"+itemsn.ITEM_SN);
            }
        }

        private void LoadItemSn1(string RmesID)
        {
            List<SNBomItemBatchEntity> lstsn = new List<SNBomItemBatchEntity>();
            lstsn = SNBomItemBatchFactory.GetByRmesID(RmesID);
            foreach (SNBomItemBatchEntity itemsn in lstsn)
            {
                lstSn.Items.Add(txtItemCode.Text + "^" + itemsn.ITEM_VENDOR + "^" + itemsn.ITEM_SN);
            }
        } 
   
        private void lstSn_Click(object sender, EventArgs e)
        {
            //txtBatchCode.Text = lstSn.SelectedItem.ToString();
        }

        private void lstSn_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (lstSn.SelectedItems.Count != 1)
            {
                MessageBox.Show("请选择要替换的零件","提示");
                return;
            }
            string oldsn = "";
            try
            {
                oldsn = lstSn.SelectedItem.ToString();
            }
            catch { }
            if (oldsn == "")
            {
                MessageBox.Show("请选择要替换的零件", "提示");
                return;
            }
            DialogResult drt = MessageBox.Show("确认替换？", "提示", MessageBoxButtons.YesNo);
            if (drt == DialogResult.No) return;
            //替换旧的数据 

            string[] msgbody = oldsn.Split('^');
            string itemcode1 = msgbody[0];
            string vendorcode1 = msgbody[1];
            string itemsn1 = msgbody[2];
            
            string newitemcode = txtItemCode.Text;
            string newvendorcode1 = txtVendorCode.Text;
            string newitemsn1 = txtBatchCode.Text;
            string barcode1 = newBarcode;// newitemcode + "^" + newvendorcode1 + "^" + newitemsn1;

            dataConn.ExeSql("insert into data_sn_bom_log select rmes_id,sn,t.company_code,t.plan_code,t.pline_code,t.location_code,t.process_code,t.process_seq,t.item_code,t.item_name,'" + itemsn1 + "',t.item_qty,t.item_class,t.item_type,'" + vendorcode1 + "',t.complete_qty,t.confirm_flag,sysdate,t.create_userid,t.replace_flag,t.station_code,t.station_name,'" + barcode1 + "','" + LoginInfo.UserInfo.USER_CODE + "'||'替换' from data_sn_bom_temp t where rmes_id='" + BomRmesID + "'");
            dataConnVb dcVb = new dataConnVb();
            if (Type2 == "A" || Type2 == "D")
            {
                dataConn.ExeSql("update data_sn_bom_itemsn set item_sn='" + newitemsn1 + "' , item_vendor='" + newvendorcode1 + "' ,barcode='" + barcode1 + "', work_time=sysdate  where bom_rmes_id='" + BomRmesID + "' and item_sn='" + itemsn1 + "' and item_vendor='" + vendorcode1 + "'  ");
                try
                {
                    dcVb.ExeSql("update sjsmljb set GYSDM='" + newvendorcode1 + "', LJLSH='" + newitemsn1 + "',GZRQ=TO_DATE(TO_CHAR(SYSDATE,'YYYY-MM-DD'),'YYYY-MM-DD'),rqsj=sysdate,  BCMC='" + LoginInfo.ShiftInfo.SHIFT_NAME + "',   BZMC='" + LoginInfo.TeamInfo.TEAM_NAME + "', YGMC='" + LoginInfo.UserInfo.USER_NAME + "', BARCODE='" + barcode1 + "' where ghtm='" + ProductSn + "' and gzdd='" + PlineCode + "' and ljdm='" + newitemcode + "' and  zdmc='" + LoginInfo.StationInfo.STATION_NAME + "'  ");
                }
                catch { }
            }
            else if (Type2 == "B")
            {
                dataConn.ExeSql("update DATA_SN_BOM_ITEMBATCH set item_sn='" + newitemsn1 + "' , item_vendor='" + newvendorcode1 + "' ,barcode='" + barcode1 + "', work_time=sysdate  where bom_rmes_id='" + BomRmesID + "' and item_sn='" + itemsn1 + "' and item_vendor='" + vendorcode1 + "'  ");
                try
                {
                    dcVb.ExeSql("update sjsmljb set GYSDM='" + newvendorcode1 + "', LJLSH='" + newitemsn1 + "',GZRQ=TO_DATE(TO_CHAR(SYSDATE,'YYYY-MM-DD'),'YYYY-MM-DD'),rqsj=sysdate,  BCMC='" + LoginInfo.ShiftInfo.SHIFT_NAME + "',   BZMC='" + LoginInfo.TeamInfo.TEAM_NAME + "', YGMC='" + LoginInfo.UserInfo.USER_NAME + "', BARCODE='" + barcode1 + "' where ghtm='" + ProductSn + "' and gzdd='" + PlineCode + "' and ljdm='" + newitemcode + "' and  zdmc='" + LoginInfo.StationInfo.STATION_NAME + "'  ");
                }
                catch { }
            }
            dcVb.CloseConn();
            string gcXlh1 = dataConn.GetValue("select product_series from data_product where sn='" + ProductSn + "' and rownum=1");
            string gcSaveData1 = ProductSn + ";" + "0" + ";" + "AB类零件防错" + ";" + newitemcode + "^" + newvendorcode1 + "^" + newitemsn1 + ";" + " " + ";" + LoginInfo.StationInfo.STATION_NAME + ";" + " " + ";" + PlineCode + ";" + gcXlh1 + ";" + "AB";
            PublicClass.Output_DayData(LoginInfo.StationInfo.STATION_NAME, ProductSn, "sjclb", gcSaveData1);
                //if (ConfirmBomItem())
            //{
            //    RMESEventArgs thisEventArg = new RMESEventArgs();
            //    thisEventArg.MessageHead = "BOM";
            //    thisEventArg.MessageBody = ProductSn;
            //    SendDataChangeMessage(thisEventArg);
            //    (Parent as frmBomItemEdit).Close();
            //}
            (Parent as frmBomItemEdit).Close();
        }

        
        private bool ConfirmBomItem()
        {
            int item_qty =0,complete_qty=0;
            
            if (chkKeyItem.Checked)
            {
                for (int i = 0; i < lstSn.Items.Count; i++)
                {
                    //SNBomTempFactory.HandleBomItemComplete(CompanyCode, BomRmesID, txtItemCode.Text, txtVendorCode.Text, txtBatchCode.Text, 1);
                }
            }
            else
            {
                //try
                //{
                //    complete_qty = int.Parse(txtCompleteQty.Text);
                //    item_qty = int.Parse(txtItemQty.Text);
                //}
                //catch
                //{
                //    MessageBox.Show("完成数量必须是数字类型");
                //    return false;
                //}
                //if (complete_qty >item_qty)
                //{
                //    DialogResult drt = MessageBox.Show("使用数量大于BOM数量，要继续吗？", "提示", MessageBoxButtons.YesNo);
                //    if (drt == DialogResult.No) return false;
                //}
                ////SNBomTempFactory.HandleBomItemComplete(CompanyCode, BomRmesID, txtItemCode.Text, txtVendorCode.Text, txtBatchCode.Text, complete_qty);
            }
            //LineSideStockFactory.OutOfStorage(txtItemCode.Text, txtVendorCode.Text, txtBatchCode.Text, LocationCode, PlineCode, complete_qty);
            return true;
        }

        //private void txtBatchCode_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    bool exists_in_list = false;                                    
        //    if (!chkKeyItem.Checked) return;
        //    if(e.KeyChar!=13) return;

        //    int complete_qty = int.Parse(txtCompleteQty.Text.ToString());
        //    int item_qty = int.Parse(txtItemQty.ToString()); 
            
            
        //    for (int i = 0; i < lstSn.Items.Count; i++)
        //    {
        //        if (lstSn.Items[i].ToString() == txtBatchCode.Text)
        //        {
        //            exists_in_list = true;
        //            break;
        //        }
        //    }
        //    if (!exists_in_list)
        //    {
        //        if (complete_qty >= item_qty)
        //        {
        //            DialogResult drt = MessageBox.Show("使用数量大于BOM数量，要继续吗？", "提示", MessageBoxButtons.YesNo);
        //            if (drt == DialogResult.No) return;
        //        }
        //        lstSn.Items.Add(txtBatchCode.Text);
        //        complete_qty = int.Parse(txtCompleteQty.ToString());
        //        txtCompleteQty.Text = (complete_qty + 1).ToString();
        //    }
            
        //}

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CsGlobalClass.QXSJSM = true;
            (Parent as frmBomItemEdit).Close();
        }

        private void chkAddNew_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkAddNew.Checked)
            //{
            //    OprType = "add";
            //    txtItemCode.Text ="";
            //    //txtProcessCode.Text = "";
            //    txtVendorCode.Text = "";
            //    txtItemQty.Text = "";
            //    txtCompleteQty.Text = "";
            //    txtBatchCode.Text = "";
            //}
            //else
            //{
            //    OprType = "edit";
            //    InitItemInfo(BomRmesID);

            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string newitemcode = txtItemCode.Text;
            string newvendorcode1 = txtVendorCode.Text;
            string newitemsn1 = txtBatchCode.Text;
            string barcode1 = newBarcode;// newitemcode + "^" + newvendorcode1 + "^" + newitemsn1;

            dataConn.ExeSql("insert into data_sn_bom_log select rmes_id,sn,t.company_code,t.plan_code,t.pline_code,t.location_code,t.process_code,t.process_seq,t.item_code,t.item_name,'" + newitemsn1 + "',t.item_qty,t.item_class,t.item_type,'" + newvendorcode1 + "',t.complete_qty,t.confirm_flag,sysdate,t.create_userid,t.replace_flag,t.station_code,t.station_name,'" + barcode1 + "','" + LoginInfo.UserInfo.USER_CODE + "'||'新增批次' from data_sn_bom_temp t where rmes_id='" + BomRmesID + "'");
            dataConnVb dcVb = new dataConnVb();
            if (Type2 == "B")
            {
                dataConn.ExeSql("insert into data_sn_bom_itembatch(rmes_id,bom_rmes_id,item_sn,barcode,item_vendor) values(seq_rmes_id.nextval,'" + BomRmesID + "','" + newitemsn1 + "','" + barcode1 + "','" + newvendorcode1 + "') ");
                try
                {
                    dcVb.ExeSql("update sjsmljb set GYSDM='" + newvendorcode1 + "', LJLSH='" + newitemsn1 + "',GZRQ=TO_DATE(TO_CHAR(SYSDATE,'YYYY-MM-DD'),'YYYY-MM-DD'),rqsj=sysdate,  BCMC='" + LoginInfo.ShiftInfo.SHIFT_NAME + "',   BZMC='" + LoginInfo.TeamInfo.TEAM_NAME + "', YGMC='" + LoginInfo.UserInfo.USER_NAME + "', BARCODE='" + barcode1 + "' where ghtm='" + ProductSn + "' and gzdd='" + PlineCode + "' and ljdm='" + newitemcode + "' and  zdmc='" + LoginInfo.StationInfo.STATION_NAME + "'  ");
                }
                catch { }
            }
            dcVb.CloseConn();
            string gcXlh1 = dataConn.GetValue("select product_series from data_product where sn='" + ProductSn + "' and rownum=1");
            string gcSaveData1 = ProductSn + ";" + "0" + ";" + "AB类零件防错" + ";" + newitemcode + "^" + newvendorcode1 + "^" + newitemsn1 + ";" + " " + ";" + LoginInfo.StationInfo.STATION_NAME + ";" + " " + ";" + PlineCode + ";" + gcXlh1 + ";" + "AB";
            PublicClass.Output_DayData(LoginInfo.StationInfo.STATION_NAME, ProductSn, "sjclb", gcSaveData1);
            (Parent as frmBomItemEdit).Close();
        }

        
    }
}
