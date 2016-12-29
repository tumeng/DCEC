using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PetaPoco;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;
using Rmes.Pub.Data;
using Rmes.Web.Base;
using System.Data;
using DevExpress.Web.ASPxGridView;

namespace Rmes.WebApp.Rmes.Sam.sam3400
{
    public partial class sam3400 : BasePage
    {
        private dataConn dc = new dataConn();
        PubCs thePubCs = new PubCs();
        protected void Page_Load(object sender, EventArgs e)
        {
            BindData();
        }
        public void BindData()
        {
            //DataTable dt = dc.GetTable("select * from CODE_ITEM");
            DataTable dt = dc.GetTable("select PT_PART,PT_DESC1,PT_DESC2,PT_STATUS,PT_PHANTOM,PT_GROUP,ROWNUM from copy_pt_mstr t");
            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("Value");
            dt1.Columns.Add("Text");
            dt1.Rows.Add("A", "单件管理");
            dt1.Rows.Add("B", "批量管理");
            //GridViewDataComboBoxColumn manflag = ASPxGridView1.Columns["MANAGE_FLAG"] as GridViewDataComboBoxColumn;
            //manflag.PropertiesComboBox.DataSource = dt1;
            //manflag.PropertiesComboBox.ValueField = "Value";
            //manflag.PropertiesComboBox.TextField = "Text";
        }
        protected void btnXlsExport_Click(object sender, EventArgs e)
        {
            ASPxGridViewExporter1.WriteXlsxToResponse("零件代码信息导出");
        }
    }
}