using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxClasses;

//以下引用，如果采用实体类，请全部复制到新页面
using PetaPoco;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;
using Rmes.Pub.Data;
using Rmes.Web.Base;
using System.Collections.Generic;


/**
 * 功能概述：现编移库操作
 * 作    者：TuMeng
 * 创建时间：2014-05-30
 * 修改时间：
 */

namespace Rmes.WebApp.Rmes.Inv.inv2100
{
    public partial class ssd2100 : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            userManager theUserManager = (userManager)Session["theUserManager"];
            string theCompanyCode = theUserManager.getCompanyCode();
            string theUserID = theUserManager.getUserId();
            if (!IsPostBack)
            {
                List<LineSideStoreEntity> stores = LinesideStoreFactory.GetAll();
                List<PlanEntity> dataSource = PlanFactory.GetByUserID(theUserID);
                PlanCode.DataSource = dataSource;
                PlanCode.ValueField = "ORDER_CODE";
                PlanCode.TextField = "ORDER_CODE";
                PlanCode.DataBind();
                DestinationStore.DataSource = stores;
                DestinationStore.ValueField = "STORE_CODE";
                DestinationStore.TextField = "STORE_NAME";
                DestinationStore.DataBind();
            }
            setCondition();
        }

        private void setCondition()
        {
            if (PlanCode.SelectedIndex < 0) return;
            string planCode = PlanCode.SelectedItem.Value.ToString();
            List<PlanBomEntity> allEntities = PlanBOMFactory.GetByOrderCode(planCode);

            List<LineSideStockEntity> dataSource = new List<LineSideStockEntity>();
            foreach (var a in allEntities)
            {
                LineSideStockEntity ls = LineSideStockFactory.GetStoreItem(a.LINESIDE_STOCK_CODE,a.ITEM_CODE);
                if (ls != null)
                    dataSource.Add(ls);
            }
            ASPxGridView1.DataSource = dataSource;
            ASPxGridView1.DataBind();
        }

        protected void LineSideStore_SelectedIndexChanged(object sender, EventArgs e)
        {
            setCondition();
        }

        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            string planCode = PlanCode.SelectedItem.Value.ToString();
            string resourceStore = ResourceStore.Text;
            string itemCode = ItemCode.Text;
            int resourceQTY = int.Parse(ResourceQTY.Text);
            string destinationStore = ""; int transQTY = 0;
            if (DestinationStore.SelectedIndex < 0)
            {
                ErrorLabel.Visible = true;
                ErrorLabel.Text = "请选择一个目标库！";
                return;
            }
            destinationStore = DestinationStore.SelectedItem.Value.ToString();
            if (resourceStore.Equals(destinationStore) || resourceStore == destinationStore)
            {
                ErrorLabel.Visible = true;
                ErrorLabel.Text = "目标库不能与源库相同！";
                return;
            }
            if (string.IsNullOrEmpty(TransQTY.Text))
            {
                ErrorLabel.Visible = true;
                ErrorLabel.Text = "请输入一个移库数量！";
                return;
            }
            try
            {
                transQTY = int.Parse(TransQTY.Text);
            }
            catch (Exception ex)
            {
                ErrorLabel.Visible = true;
                ErrorLabel.Text = "请输入正确的移库数量！";
                return;
            }
            if ((resourceQTY - transQTY) < 0)
            {
                ErrorLabel.Visible = true;
                ErrorLabel.Text = "移库数量不能大于库存数量！";
                return;
            }
            string msg = LineSideStockFactory.TransStore(planCode,resourceStore, itemCode, destinationStore, transQTY);
            if (msg=="true")
            {
                Response.Write("<script>alert('移库完成！');self.location.href=self.location.href;</script>");
            }
            else
            {
                Response.Write("<script>alert('"+msg+"！');</script>");
                return;
            }
        }

        protected void ASPxGridView1_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
        {
            int rowIndex = int.Parse(e.Parameters);
            string itemCode = ASPxGridView1.GetRowValues(rowIndex, "ITEM_CODE") as string;
            string itemQTY = ASPxGridView1.GetRowValues(rowIndex, "ITEM_QTY").ToString();
            string resourceStore = ASPxGridView1.GetRowValues(rowIndex, "STORE_CODE") as string;

            string result = itemCode + "," + itemQTY + "," + resourceStore;
            e.Result = result;


        }
    }
}