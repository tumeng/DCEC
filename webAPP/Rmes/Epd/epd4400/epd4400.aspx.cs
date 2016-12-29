using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//以下引用，如果采用实体类，请全部复制到新页面
using PetaPoco;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;
using Rmes.Pub.Data;
using Rmes.Web.Base;
using Rmes.DA.Procedures;
using System.Data.Odbc;
using Rmes.Public;
using System.Drawing;
using System.IO;
using DevExpress.Data.PivotGrid;
using System.Data;
using DevExpress.Utils;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
namespace Rmes.WebApp.Rmes.Epd.ep4400
{
    public partial class epd4400 : BasePage
    {
        private dataConn dc = new dataConn();
        PubCs thePubCs = new PubCs();
        protected void Page_Load(object sender, EventArgs e)
        {
            SetCondition();
        }
        private void SetCondition()
        {
            DataTable dt = dc.GetTable("select * from CODE_MON_TAR");
            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();
        }
        protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxTextBox zbct = ASPxGridView1.FindEditFormTemplateControl("zbct") as ASPxTextBox;
            ASPxTextBox wcct = ASPxGridView1.FindEditFormTemplateControl("wcct") as ASPxTextBox;
            ASPxTextBox snct = ASPxGridView1.FindEditFormTemplateControl("snct") as ASPxTextBox;
            ASPxTextBox tqct = ASPxGridView1.FindEditFormTemplateControl("tqct") as ASPxTextBox;
            ASPxTextBox zbyj = ASPxGridView1.FindEditFormTemplateControl("zbyj") as ASPxTextBox;
            ASPxTextBox wcyj = ASPxGridView1.FindEditFormTemplateControl("wcyj") as ASPxTextBox;
            ASPxTextBox snyj = ASPxGridView1.FindEditFormTemplateControl("snyj") as ASPxTextBox;
            ASPxTextBox tqyj = ASPxGridView1.FindEditFormTemplateControl("tqyj") as ASPxTextBox;
            ASPxTextBox zbck = ASPxGridView1.FindEditFormTemplateControl("zbck") as ASPxTextBox;
            ASPxTextBox wcck = ASPxGridView1.FindEditFormTemplateControl("wcck") as ASPxTextBox;
            ASPxTextBox snck = ASPxGridView1.FindEditFormTemplateControl("snck") as ASPxTextBox;
            ASPxTextBox tqck = ASPxGridView1.FindEditFormTemplateControl("tqck") as ASPxTextBox;
            ASPxTextBox pro = ASPxGridView1.FindEditFormTemplateControl("PRO") as ASPxTextBox;

            string upSql = "UPDATE CODE_MON_TAR SET ZBCT='" + zbct.Text.Trim() + "',WCCT='" + wcct.Text.Trim() + "',SNCT='" + snct.Text.Trim() + "',TQCT='" + tqct.Text.Trim() + "'," +
                         "ZBYJ='" + zbyj.Text.Trim() + "',WCYJ='" + wcyj.Text.Trim() + "',SNYJ='" + snyj.Text.Trim() + "',TQYJ='" + tqyj.Text.Trim() +"'," +
                         "ZBCK='" + zbck.Text.Trim() + "',WCCK='" + wcck.Text.Trim() + "',SNCK='" + snck.Text.Trim() + "',TQCK='" + tqck.Text.Trim() +
                         "' WHERE  PRO='" + pro.Text.Trim() + "'";
            dc.ExeSql(upSql);

            e.Cancel = true;
            ASPxGridView1.CancelEdit();
            SetCondition();
        }
        protected void ASPxGridView1_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditFormEventArgs e)
        {
            if (!ASPxGridView1.IsNewRowEditing && ASPxGridView1.IsEditing)
            {
                //主键不可以修改
                (ASPxGridView1.FindEditFormTemplateControl("pro") as ASPxTextBox).Enabled = false;
                //按照之前开发的样子，生产线也不可以修改
                //(ASPxGridView1.FindEditFormTemplateControl("ASPxComboBox1") as ASPxComboBox).Enabled = false;
            }
        }
    }
}