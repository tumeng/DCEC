using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using DevExpress.Web.ASPxEditors;

//以下引用，如果采用实体类，请全部复制到新页面
using PetaPoco;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;
using Rmes.Pub.Data;
using Rmes.Pub.Function;
using Rmes.Web.Base;

/**
 * 功能概述：跨线模式定义
 * 作者：杨少霞
 * 创建时间：2016-07-13
 **/

//namespace Rmes.WebApp.Rmes.Epd.epd1A00
//{
    public partial class Rmes_epd1A00 : BasePage
    {
        public string theCompanyCode;
        public string theUserCode;
        public string theProgramCode;
        public dataConn theDc = new dataConn();
        public PubCs thePubCs = new PubCs();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            userManager theUserManager = (userManager)Session["theUserManager"];
            theCompanyCode = theUserManager.getCompanyCode();
            theUserCode = theUserManager.getUserId();
            theProgramCode = "epd1A00";
            queryFunction();

            string plineSql = "SELECT RMES_ID,PLINE_CODE,PLINE_NAME FROM CODE_PRODUCT_LINE WHERE COMPANY_CODE='" + theCompanyCode + "' and pline_code in ( select pline_code from vw_user_role_program where user_id='" + theUserCode + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "') order by pline_code";
            SqlDataSource1.SelectCommand = plineSql;
            SqlDataSource1.DataBind();
        }
        //初始化GRIDVIEW
        private void queryFunction()
        {

            string sql = "SELECT COMPANY_CODE,ALINE_CODE,ALINE_NAME,PLINE_ID,PLINE_CODE,PLINE_NAME FROM ATPU_ACROSSLINE where COMPANY_CODE='" + theCompanyCode + "' and pline_code in (select pline_code from vw_user_role_program where user_id='" + theUserCode + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "') "
                 + "  ORDER BY ALINE_CODE,PLINE_CODE";

            DataTable dt = theDc.GetTable(sql);
            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();

        }
        protected void ASPxGridView1_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            //判断超长
            if (e.NewValues["ALINE_CODE"].ToString().Length > 30)
            {
                e.RowError = "跨线模式代码字节长度不能超过30！";
            }
            if (e.NewValues["ALINE_NAME"].ToString().Length > 50)
            {
                e.RowError = "跨线模式名称字节长度不能超过50！";
            }
            if (e.NewValues["PLINE_ID"].ToString().Length > 30)
            {
                e.RowError = "生产线字节长度不能超过30！";
            }
            //判断为空
            if (e.NewValues["ALINE_CODE"].ToString() == "" || e.NewValues["ALINE_CODE"].ToString() == null)
            {
                e.RowError = "跨线模式代码不能为空！";
            }
            if (e.NewValues["ALINE_NAME"].ToString() == "" || e.NewValues["ALINE_NAME"].ToString() == null)
            {
                e.RowError = "跨线模式名称不能为空！";
            }
            if (e.NewValues["PLINE_ID"].ToString() == "" || e.NewValues["PLINE_ID"].ToString() == null)
            {
                e.RowError = "生产线不能为空！";
            }

            //新增判断键值重复
            if (ASPxGridView1.IsNewRowEditing)
            {
                string chSql = "select COMPANY_CODE,ALINE_CODE,ALINE_NAME,PLINE_ID,PLINE_CODE,PLINE_NAME FROM ATPU_ACROSSLINE"
                    + " WHERE COMPANY_CODE = '" + theCompanyCode + "' and ALINE_CODE='" + e.NewValues["ALINE_CODE"].ToString() + "'";
                dataConn dc = new dataConn(chSql);
                if (dc.GetState() == true)
                {
                    e.RowError = "已存在相同的跨线模式代码！";
                }
            }
        }
        
        protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxTextBox aCode = ASPxGridView1.FindEditFormTemplateControl("txtAlineCode") as ASPxTextBox;
            ASPxTextBox aName = ASPxGridView1.FindEditFormTemplateControl("txtAlineName") as ASPxTextBox;
            ASPxComboBox PlineId = ASPxGridView1.FindEditFormTemplateControl("ASPxComboBox1") as ASPxComboBox;

            string plineCode = "";
            string plineName = "";
            string chSql = "select pline_code,pline_name from code_product_line where rmes_id='"+PlineId.Value.ToString()+"'";
            dataConn dc1 = new dataConn(chSql);
            DataTable dt1 = dc1.GetTable();
            if (dt1.Rows.Count > 0)
            {
                plineCode = dt1.Rows[0]["pline_code"].ToString();
                plineName = dt1.Rows[0]["pline_name"].ToString();
            }

            string upSql = "UPDATE ATPU_ACROSSLINE SET ALINE_NAME='" + aName.Text.Trim() + "',"
                         + "PLINE_ID='" + PlineId.Value.ToString() + "',PLINE_CODE='" + plineCode + "',PLINE_NAME='" + plineName + "'"
                         + "  WHERE  COMPANY_CODE = '" + theCompanyCode + "' and  ALINE_CODE='" + aCode.Text.Trim() + "'";
            theDc.ExeSql(upSql);

            e.Cancel = true;
            ASPxGridView1.CancelEdit();
            queryFunction();
        }
        protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string dSql = "DELETE FROM ATPU_ACROSSLINE WHERE COMPANY_CODE='" + theCompanyCode + "' and ALINE_CODE='" + e.Values["ALINE_CODE"].ToString() + "'";
            theDc.ExeSql(dSql);

            e.Cancel = true;
            queryFunction();
        }
        protected void ASPxGridView1_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditFormEventArgs e)
        {
            if (!ASPxGridView1.IsNewRowEditing && ASPxGridView1.IsEditing)
            {
                //主键不可以修改
                (ASPxGridView1.FindEditFormTemplateControl("txtAlineCode") as ASPxTextBox).Enabled = false;
                ////按照之前开发的样子，生产线也不可以修改
                //(ASPxGridView1.FindEditFormTemplateControl("ASPxComboBox1") as ASPxComboBox).Enabled = false;
            }
        }

        protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            ASPxTextBox aCode = ASPxGridView1.FindEditFormTemplateControl("txtAlineCode") as ASPxTextBox;
            ASPxTextBox aName = ASPxGridView1.FindEditFormTemplateControl("txtAlineName") as ASPxTextBox;
            ASPxComboBox PlineId = ASPxGridView1.FindEditFormTemplateControl("ASPxComboBox1") as ASPxComboBox;

            string plineCode = "";
            string plineName = "";
            string chSql = "select pline_code,pline_name from code_product_line where rmes_id='" + PlineId.Value.ToString() + "'";
            dataConn dc1 = new dataConn(chSql);
            DataTable dt1 = dc1.GetTable();
            if (dt1.Rows.Count > 0)
            {
                plineCode = dt1.Rows[0]["pline_code"].ToString();
                plineName = dt1.Rows[0]["pline_name"].ToString();
            }

            string inSql = "INSERT INTO ATPU_ACROSSLINE (COMPANY_CODE,ALINE_CODE,ALINE_NAME,PLINE_ID,PLINE_CODE,PLINE_NAME) "
                         + "VALUES ('" + theCompanyCode + "','" + aCode.Text.Trim() + "','" + aName.Text.Trim() + "',"
                         + "'" + PlineId.Value.ToString() + "','" + plineCode + "','" + plineName + "')";

            theDc.ExeSql(inSql);

            e.Cancel = true;
            ASPxGridView1.CancelEdit();
            queryFunction();
        }

    }
//}