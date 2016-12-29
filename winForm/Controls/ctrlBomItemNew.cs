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
    public partial class ctrlBomItemNew : BaseControl
    {
        string CompanyCode,OrderCode,PlanCode,PlineCode, StationCode, WorkunitCode,SN;
        public ctrlBomItemNew(string pPlanCode,string pSn,string pStationCode)
        {

            InitializeComponent();
            PlanCode = pPlanCode;
            SN = pSn;
            StationCode = pStationCode;
            PlineCode = LoginInfo.ProductLineInfo.RMES_ID;
            CompanyCode = LoginInfo.CompanyInfo.COMPANY_CODE;
                       
            InitItems(pPlanCode,pStationCode);
        }
        private void InitItems(string pPlanCode,string pStationCode)
        {
            StationEntity ent1 = StationFactory.GetByKey(pStationCode);
            WorkunitCode = "";
            txtWorkUnit.Text = ent1.STATION_NAME;
            PlanEntity ent2 = PlanFactory.GetByKey(pPlanCode);
            OrderCode = ent2.ORDER_CODE;
 
            List<PlanStandardBOMEntity> lstBom = PlanStandardBOMFactory.GetByOrderCode(OrderCode);
            List<PlanStandardBOMEntity> lstBom1 = (from p in lstBom
                                                   where p.VIRTUAL_ITEM_CODE == "X" && p.WORKUNIT_CODE == WorkunitCode
                                                   select p).ToList<PlanStandardBOMEntity>();
            
            if (lstBom1.Count == 0)
            {
                cmbVItemCode.SelectedIndex = -1;
                cmbVItemCode.Enabled = false;
            }
            else
            {
                cmbVItemCode.Items.Clear();
                cmbVItemCode.Items.Add("总装件");
                for (int i = 0; i < lstBom1.Count; i++)
                {
                    cmbVItemCode.Items.Add(lstBom1[i].ITEM_CODE + "-" + lstBom1[i].ITEM_NAME);
                }
                cmbVItemCode.Enabled = true;
            }

        }

        private void cmbVirItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbVItemCode.SelectedIndex > 0)
                txtVitemName.Text = cmbVItemCode.Text.Split('-')[1];
            else
                txtVitemName.Text = cmbVItemCode.Text;

        }

        private string GetItemSeq(string itemCode, string virItemCode)
        {
            string item_seq = "";
            List<PlanStandardBOMEntity> p;
            List<PlanStandardBOMEntity> lst_bom= PlanStandardBOMFactory.GetByOrderCodeAndWorkUnit(OrderCode, WorkunitCode);

            if (string.IsNullOrWhiteSpace(virItemCode))
            {
                p = (from s in lst_bom where s.ITEM_CODE == itemCode && s.VIRTUAL_ITEM_CODE == null select s).ToList();
            }
            else
            {

                p = (from s in lst_bom where s.ITEM_CODE == itemCode && s.VIRTUAL_ITEM_CODE == virItemCode select s).ToList();
            }

            item_seq = (p.Count * 10 + 10).ToString("0000");

            return item_seq;
        }
        private void CreateBomItem(string ItemCode,float ItemQty)
        {

            List<PlanStandardBOMEntity> lst_bom = PlanStandardBOMFactory.GetByOrderCodeAndWorkUnit(OrderCode, WorkunitCode);
            var sss = (from a in lst_bom where a.ITEM_CODE == ItemCode && a.WORKUNIT_CODE == WorkunitCode select a).ToList();
            if (sss.Count > 0)
            {
                MessageBox.Show("此物料在此工序BOM中已经存在");
                return;
            }
            PlanStandardBOMEntity ent_bom = lst_bom.First();
            ItemEntity ent_item = ItemFactory.GetByItem(CompanyCode, ItemCode);
            PlanStandardBOMEntity ent_bom1 = new PlanStandardBOMEntity
            {

                COMPANY_CODE = CompanyCode,
                CREATE_TIME = DateTime.Today,
                ITEM_CODE = ItemCode,
                ITEM_NAME = ent_item.ITEM_NAME,
                ITEM_QTY = ItemQty,
                ITEM_SEQ = GetItemSeq(ItemCode, cmbVItemCode.Text),
                PLAN_CODE = ent_bom.PLAN_CODE,
                LOCATION_CODE = ent_bom.LOCATION_CODE,
                PROCESS_CODE = ent_bom.PROCESS_CODE,
                PLINE_CODE = ent_bom.PLINE_CODE,
                VIRTUAL_ITEM_CODE = cmbVItemCode.Text.Split('-')[0],
                WORKSHOP_CODE = ent_bom.WORKSHOP_CODE,
                STORE_ID = ent_bom.STORE_ID,
                ORDER_CODE = ent_bom.ORDER_CODE,
                WORKUNIT_CODE = ent_bom.WORKUNIT_CODE,
                ITEM_UNIT = ent_item.UNIT_CODE,
                LINESIDE_STOCK_CODE = ent_bom.LINESIDE_STOCK_CODE,
                USER_CODE=LoginInfo.UserInfo.USER_CODE
            };

            DB.GetInstance().Insert(ent_bom1);
        }
        private void Add2BomTemp(string ItemCode,float ItemQty)
        {
            //string vitem_code=cmbVItemCode.Text.Split('-')[0];
            //if (cmbVItemCode.Text == "总装件") vitem_code = "main";
                
            
            //List<SNBomTempEntity> bom_temp = SNBomTempFactory.GetByPlanStaton(CompanyCode, PlanCode, PlineCode, StationCode, "", vitem_code);

            //var sss = (from a in bom_temp where a.ITEM_CODE == ItemCode && a.WORKUNIT_CODE == WorkunitCode select a).ToList();
            //if (sss.Count > 0)
            //{
            //    MessageBox.Show("此物料在此工序BOM中已经存在");
            //    return;
            //}
            //SNBomTempEntity ent_bom = bom_temp.First();
            //ItemEntity ent_item = ItemFactory.GetByItem(CompanyCode, ItemCode);
            //SNBomTempEntity ent_bom1 = new SNBomTempEntity
            //{

            //    COMPANY_CODE = CompanyCode,
            //    PLINE_CODE = ent_bom.PLINE_CODE,
            //    WORK_TIME = DateTime.Today,
            //    ITEM_CODE = ItemCode,
            //    ITEM_NAME = ent_item.ITEM_NAME,
            //    ITEM_QTY = ItemQty,

            //    PLAN_CODE = ent_bom.PLAN_CODE,
            //    SN=ent_bom.SN,
            //    LOCATION_CODE = ent_bom.LOCATION_CODE,
            //    PROCESS_CODE = ent_bom.PROCESS_CODE,
            //    ITEM_CLASS_CODE=ent_item.ABC_FLAG,
            //    VIRTUAL_ITEM_CODE = cmbVItemCode.Text.Split('-')[0],
            //    COMPLETE_QTY=0,
            //    WORKSHOP_CODE = ent_bom.WORKSHOP_CODE,
            //    ORDER_CODE = ent_bom.ORDER_CODE,
            //    WORKUNIT_CODE=ent_bom.WORKUNIT_CODE,
            //    USER_ID=ent_bom.USER_ID,

            //    CONFIRM_FLAG="N"
            //};

            //DB.GetInstance().Insert(ent_bom1);
        }
        private void CreateWMSItem()
        {

        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            string item_code = txtItemCode.Text;
            string item_qty = txtItemQty.Text;
            float itemQty;
            if (string.IsNullOrWhiteSpace(item_code))
            {
                MessageBox.Show("物料代码不能为空");
                return;
            }
            if (string.IsNullOrWhiteSpace(item_qty))
            {
                MessageBox.Show("计划数量不能为空");
                return;
            }
            try
            {
                itemQty = float.Parse(item_qty);
            }
            catch
            {
                MessageBox.Show("计划数量必须是数字");
                return;
            }

            Add2BomTemp(item_code, itemQty);
            RMESEventArgs thisEventArg = new RMESEventArgs();
            thisEventArg.MessageHead = "BOM";
            thisEventArg.MessageBody = SN;
            SendDataChangeMessage(thisEventArg);
            (Parent as frmBomNew).Close();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            (Parent as frmBomNew).Close();
        }

        private void txtItemCode_Validated(object sender, EventArgs e)
        {
            string item_code = txtItemCode.Text;
            if (string.IsNullOrWhiteSpace(item_code)) return;
            ItemEntity ent_item = ItemFactory.GetByItem(CompanyCode, item_code);
            if (ent_item == null)
            {
                MessageBox.Show("此项物料没有定义");

                txtItemCode.Focus();
                return;

            }

            txtItemName.Text = ent_item.ITEM_NAME;
            //txtUnit.Text = ent_item.UNIT_CODE;
        }
    }
}
