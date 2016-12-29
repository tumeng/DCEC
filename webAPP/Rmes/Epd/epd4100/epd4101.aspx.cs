using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rmes.DA.Entity;
using DevExpress.Web.ASPxGridView;
using Rmes.DA.Factory;
using System.Data;
using Rmes.Web.Base;
using Rmes.DA.Base;
using Rmes.Pub.Data;

namespace Rmes.WebApp.Rmes.Epd.epd4100
{
    public partial class epd4101 : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindData();
            
        }

        public void ASPxGridView1_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
        {
            userManager theUserManager = (userManager)Session["theUserManager"];
            string _ids = e.Parameters;
            string[] ids = _ids.Split(',');
            //int index = Convert.ToInt32(e.Parameters);
            //string rmesID = ASPxGridView1.GetRowValues(index, "RMES_ID").ToString();
            
            
            DB.GetInstance().BeginTransaction();
            try
            {
                string batch_temp = "'B'||TO_CHAR(SYSDATE,'yyyymmdd')||to_char(SEQ_BATCHID.NEXTVAL,'fm0000')";
                string sql = "select " + batch_temp + " from dual";

                string batch_id = DB.GetInstance().ExecuteScalar<string>(sql);

                DateTime time = DateTime.Now;
                int num = 0;


                foreach (var id in ids)
                {
                    ItemLineSideEntity entity = ItemLineSideFactory.GetByID(id);
                    ItemLineSideStore2LineEntity tempStoreEntity = new ItemLineSideStore2LineEntity
                    {
                        COMPANY_CODE = "01",
                        WORKSHOP = "8101",
                        ITEM_CODE = entity.ITEM_CODE,
                        ITEM_NAME=entity.ITEM_NAME,
                        ITEM_QTY = entity.STAND_QTY,
                        CREATE_TIME = DateTime.Now,
                        CREATE_USER_ID = theUserManager.getUserId(),
                        T_LINESIDESTORE = entity.LINESIDE_STORE_CODE,
                        S_LINESIDESTORE = entity.RESOURCE_STORE,
                    };
                    DB.GetInstance().Insert(tempStoreEntity);

                    IMESStore2LineEntity storeEntity = new IMESStore2LineEntity
                    {

                        WERKS = "8101",
                        AUFNR=DateTime.Now.ToString("yyyyMMdd").Insert(0,"A000"),
                        SUBMAT = entity.ITEM_CODE,
                        MATKT = entity.ITEM_NAME,
                        MENGE = entity.STAND_QTY.ToString(),
                        SLGORT = entity.RESOURCE_STORE,
                        TLGORT = entity.LINESIDE_STORE_CODE,
                        SERIAL = DateTime.Now.ToString("yyyyMMddhhmmss"),
                        WKDT = time,
                        BATCH=batch_id,
                        CHARG1 = batch_id,
                        CHARG2 = batch_id,
                        PRIND = "0"
                    };
                    DB.GetInstance().Insert(storeEntity);
                    num++;
                    SAPMessageTransEntity msgEntity = new SAPMessageTransEntity
                    {
                        MESSAGE_CODE = "0046",
                        WORK_DATE = DateTime.Now,
                        HANDLE_FLAG = "0",
                    };
                    DB.GetInstance().Insert(msgEntity);
                }
                string sql1 = "update IMES_DATA_STORE2LINE set KUNNR=" + num + " where BATCH='" + batch_id + "'";
                DB.GetInstance().Execute(sql1);
                DB.GetInstance().CompleteTransaction();
                e.Result = "success";
            }
            catch (Exception ex)
            {
                DB.GetInstance().AbortTransaction();
                e.Result = "false";
            }
        }

        public void BindData()
        {
            List<LinesideAutoIssueEntity> allEntity = LinesideAutoIssueFactory.GetAll();
            ASPxGridView1.DataSource = allEntity;

            GridViewDataComboBoxColumn comStock = ASPxGridView1.Columns["LINESIDE_STORE_CODE"] as GridViewDataComboBoxColumn;
            List<LineSideStoreEntity> all = LinesideStoreFactory.GetLineSideStore();
            comStock.PropertiesComboBox.DataSource = all;
            comStock.PropertiesComboBox.TextField = "STORE_NAME";
            comStock.PropertiesComboBox.ValueField = "STORE_CODE";

            GridViewDataComboBoxColumn comResourceStore = ASPxGridView1.Columns["RESOURCE_STORE"] as GridViewDataComboBoxColumn;
            List<LineSideStoreEntity> _all = LinesideStoreFactory.GetMaterialStore();
            comResourceStore.PropertiesComboBox.DataSource = _all;
            comResourceStore.PropertiesComboBox.TextField = "STORE_NAME";
            comResourceStore.PropertiesComboBox.ValueField = "STORE_CODE";

            GridViewDataComboBoxColumn comType = ASPxGridView1.Columns["BATCH_TYPE"] as GridViewDataComboBoxColumn;

            DataTable dt = new DataTable();
            dt.Columns.Add("text");
            dt.Columns.Add("value");
            dt.Rows.Add("天", 0);
            dt.Rows.Add("周", 1);
            dt.Rows.Add("旬", 2);
            dt.Rows.Add("月", 3);
            dt.Rows.Add("季度", 4);

            comType.PropertiesComboBox.DataSource = dt;
            comType.PropertiesComboBox.TextField = "text";
            comType.PropertiesComboBox.ValueField = "value";

            ASPxGridView1.DataBind();

        }
    }
}