using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DevExpress.Web.ASPxGridLookup;
using DevExpress.Web.ASPxClasses;
using DevExpress.Web.ASPxEditors;

using Rmes.DA.Base;
using Rmes.DA.Procedures;
using Rmes.Pub.Data;
using Rmes.Web.Base;

using Rmes.DA.Factory;
using Rmes.DA.Entity;

namespace Rmes.WebApp.Rmes.Epd.epd3900
{
    public partial class epd3900 : BasePage
    {
        private dataConn dc = new dataConn();
        private PubCs pc = new PubCs();
        public string theCompanyCode, theProgramCode, theUserId, theUserCode;

        protected void Page_Load(object sender, EventArgs e)
        {
            userManager theUserManager = (userManager)Session["theUserManager"];
            theCompanyCode = theUserManager.getCompanyCode();
            theProgramCode = "epd3900";
            theUserId = theUserManager.getUserId();
            theUserCode = theUserManager.getUserCode();
            
            queryFunction();
            setCondition();

            string plineSql = "select a.pline_code,b.rmes_id,b.pline_name from VW_USER_ROLE_PROGRAM a "
                   + "left join code_product_line b on a.pline_code=b.pline_code "
                   + "where a.COMPANY_CODE = '" + theCompanyCode + "' and a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' order by b.PLINE_NAME";
            SqlDataSource1.SelectCommand = plineSql;
            SqlDataSource1.DataBind();
        }
        private void setCondition()
        {
            //初始化GRIDVIEW
            string sql = "SELECT A.RMES_ID,A.COMPANY_CODE,A.PLINE_ID_OLD,A.PLINE_CODE_OLD,A.PLINE_NAME_OLD,A.PLINE_ID_NEW,A.PLINE_CODE_NEW,A.PLINE_NAME_NEW,"
                + "A.LOCATION_ID,A.LOCATION_CODE,A.LOCATION_NAME,"
                + "A.PROCESS_ID,A.PROCESS_CODE,A.PROCESS_NAME,A.LOCATION_SEQUENCE,A.ALINE_CODE,A.ALINE_NAME "
                + "FROM ATPU_ACROSS_DATA A "
                + " WHERE A.COMPANY_CODE = '" + theCompanyCode + "' "
                + "and a.pline_id_old in (select w.rmes_id from VW_USER_ROLE_PROGRAM t left join code_product_line w on t.pline_code=w.pline_code "
                + "where t.user_id='" + theUserId + "' and t.program_code='" + theProgramCode + "')"
                + " ORDER BY a.INPUT_TIME desc nulls last,A.ALINE_CODE,A.LOCATION_CODE ";
            DataTable dt = dc.GetTable(sql);

            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();
        }
        protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            ////删除
            string id = e.Keys["RMES_ID"].ToString();

            //插入到日志表
            try
            {
                string Sql2 = "INSERT INTO ATPU_ACROSS_DATA_LOG (RMES_ID,COMPANY_CODE,PLINE_ID_OLD,PLINE_CODE_OLD,PLINE_NAME_OLD,PLINE_ID_NEW,PLINE_CODE_NEW,PLINE_NAME_NEW,LOCATION_ID,"
                    + "LOCATION_CODE,LOCATION_NAME,PROCESS_ID,PROCESS_CODE,PROCESS_NAME,LOCATION_SEQUENCE,ALINE_CODE,ALINE_NAME,user_code,flag,rqsj)"
                    + " SELECT RMES_ID,COMPANY_CODE,PLINE_ID_OLD,PLINE_CODE_OLD,PLINE_NAME_OLD,PLINE_ID_NEW,PLINE_CODE_NEW,PLINE_NAME_NEW,LOCATION_ID,"
                    + "LOCATION_CODE,LOCATION_NAME,PROCESS_ID,PROCESS_CODE,PROCESS_NAME,LOCATION_SEQUENCE,ALINE_CODE,ALINE_NAME,'"
                    + theUserCode + "' , 'DEL', SYSDATE FROM ATPU_ACROSS_DATA WHERE rmes_id='" + id + "'";
                dc.ExeSql(Sql2);
            }
            catch
            {
                return;
            }

            string sql = "delete from ATPU_ACROSS_DATA where rmes_id='" + id + "'";
            dc.ExeSql(sql);

            setCondition();
            e.Cancel = true;
        }

        //初始化GRIDVIEW
        private void queryFunction()
        {
            string sql = "SELECT COMPANY_CODE,ALINE_CODE,ALINE_NAME,PLINE_ID,PLINE_CODE,PLINE_NAME FROM ATPU_ACROSSLINE where COMPANY_CODE='" + theCompanyCode + "'"
                       + "and PLINE_ID in (select w.rmes_id from VW_USER_ROLE_PROGRAM t left join code_product_line w on t.pline_code=w.pline_code "
                       + "where t.user_id='" + theUserId + "' and t.program_code='" + theProgramCode + "')"
                       + "  ORDER BY INPUT_TIME desc nulls last,ALINE_CODE,PLINE_CODE";

            DataTable dt = dc.GetTable(sql);
            ASPxGridView2.DataSource = dt;
            ASPxGridView2.DataBind();

        }
        protected void ASPxGridView2_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
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
            if (ASPxGridView2.IsNewRowEditing)
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

        protected void ASPxGridView2_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxTextBox aCode = ASPxGridView2.FindEditFormTemplateControl("txtAlineCode") as ASPxTextBox;
            ASPxTextBox aName = ASPxGridView2.FindEditFormTemplateControl("txtAlineName") as ASPxTextBox;
            ASPxComboBox PlineId = ASPxGridView2.FindEditFormTemplateControl("ASPxComboBox1") as ASPxComboBox;

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
            //插入到日志表
            try
            {
                string Sql2 = "INSERT INTO ATPU_ACROSSLINE_LOG (COMPANY_CODE,ALINE_CODE,ALINE_NAME,PLINE_ID,PLINE_CODE,PLINE_NAME,user_code,flag,rqsj)"
                    + " SELECT COMPANY_CODE,ALINE_CODE,ALINE_NAME,PLINE_ID,PLINE_CODE,PLINE_NAME,'"
                    + theUserCode + "' , 'BEFOREEDIT', SYSDATE FROM ATPU_ACROSSLINE WHERE COMPANY_CODE='" + theCompanyCode + "' and ALINE_CODE='"
                    + aCode.Text.Trim() + "'";
                dc.ExeSql(Sql2);
            }
            catch
            {
                return;
            }

            string upSql = "UPDATE ATPU_ACROSSLINE SET ALINE_NAME='" + aName.Text.Trim() + "',input_person='"+theUserId+"',input_time=sysdate "
                         + "  WHERE  COMPANY_CODE = '" + theCompanyCode + "' and  ALINE_CODE='" + aCode.Text.Trim() + "' and PLINE_ID='" + PlineId.Value.ToString() + "'";
            dc.ExeSql(upSql);

            //插入到日志表
            try
            {
                string Sql2 = "INSERT INTO ATPU_ACROSSLINE_LOG (COMPANY_CODE,ALINE_CODE,ALINE_NAME,PLINE_ID,PLINE_CODE,PLINE_NAME,user_code,flag,rqsj)"
                    + " SELECT COMPANY_CODE,ALINE_CODE,ALINE_NAME,PLINE_ID,PLINE_CODE,PLINE_NAME,'"
                    + theUserCode + "' , 'AFTEREDIT', SYSDATE FROM ATPU_ACROSSLINE WHERE COMPANY_CODE='" + theCompanyCode + "' and ALINE_CODE='"
                    + aCode.Text.Trim() + "'";
                dc.ExeSql(Sql2);
            }
            catch
            {
                return;
            }

            e.Cancel = true;
            ASPxGridView2.CancelEdit();
            queryFunction();
        }
        protected void ASPxGridView2_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            //插入到日志表
            try
            {
                string Sql2 = "INSERT INTO ATPU_ACROSSLINE_LOG (COMPANY_CODE,ALINE_CODE,ALINE_NAME,PLINE_ID,PLINE_CODE,PLINE_NAME,user_code,flag,rqsj)"
                    + " SELECT COMPANY_CODE,ALINE_CODE,ALINE_NAME,PLINE_ID,PLINE_CODE,PLINE_NAME,'"
                    + theUserCode + "' , 'DEL', SYSDATE FROM ATPU_ACROSSLINE WHERE COMPANY_CODE='" + theCompanyCode + "' and ALINE_CODE='"
                    + e.Values["ALINE_CODE"].ToString() + "'";
                dc.ExeSql(Sql2);
            }
            catch
            {
                return;
            }

            //插入到日志表
            try
            {
                string Sql2 = "INSERT INTO ATPU_ACROSS_DATA_LOG (RMES_ID,COMPANY_CODE,PLINE_ID_OLD,PLINE_CODE_OLD,PLINE_NAME_OLD,PLINE_ID_NEW,PLINE_CODE_NEW,PLINE_NAME_NEW,LOCATION_ID,"
                    + "LOCATION_CODE,LOCATION_NAME,PROCESS_ID,PROCESS_CODE,PROCESS_NAME,LOCATION_SEQUENCE,ALINE_CODE,ALINE_NAME,user_code,flag,rqsj)"
                    + " SELECT RMES_ID,COMPANY_CODE,PLINE_ID_OLD,PLINE_CODE_OLD,PLINE_NAME_OLD,PLINE_ID_NEW,PLINE_CODE_NEW,PLINE_NAME_NEW,LOCATION_ID,"
                    + "LOCATION_CODE,LOCATION_NAME,PROCESS_ID,PROCESS_CODE,PROCESS_NAME,LOCATION_SEQUENCE,ALINE_CODE,ALINE_NAME,'"
                    + theUserCode + "' , 'DEL', SYSDATE FROM ATPU_ACROSS_DATA WHERE COMPANY_CODE='" + theCompanyCode + "' and ALINE_CODE='"
                    + e.Values["ALINE_CODE"].ToString() + "'";
                dc.ExeSql(Sql2);
            }
            catch
            {
                return;
            }

            string dSql = "DELETE FROM ATPU_ACROSSLINE WHERE COMPANY_CODE='" + theCompanyCode + "' and ALINE_CODE='" + e.Values["ALINE_CODE"].ToString() + "'";
            dc.ExeSql(dSql);

            string dSql1 = "DELETE FROM ATPU_ACROSS_DATA WHERE COMPANY_CODE='" + theCompanyCode + "' and ALINE_CODE='" + e.Values["ALINE_CODE"].ToString() + "'";
            dc.ExeSql(dSql1);

            e.Cancel = true;
            queryFunction();
        }
        protected void ASPxGridView2_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditFormEventArgs e)
        {
            if (!ASPxGridView2.IsNewRowEditing && ASPxGridView2.IsEditing)
            {
                //主键不可以修改
                (ASPxGridView2.FindEditFormTemplateControl("txtAlineCode") as ASPxTextBox).Enabled = false;
                ////按照之前开发的样子，生产线也不可以修改
                (ASPxGridView2.FindEditFormTemplateControl("ASPxComboBox1") as ASPxComboBox).Enabled = false;
            }
        }

        protected void ASPxGridView2_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            ASPxTextBox aCode = ASPxGridView2.FindEditFormTemplateControl("txtAlineCode") as ASPxTextBox;
            ASPxTextBox aName = ASPxGridView2.FindEditFormTemplateControl("txtAlineName") as ASPxTextBox;
            ASPxComboBox PlineId = ASPxGridView2.FindEditFormTemplateControl("ASPxComboBox1") as ASPxComboBox;

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

            string inSql = "INSERT INTO ATPU_ACROSSLINE (COMPANY_CODE,ALINE_CODE,ALINE_NAME,PLINE_ID,PLINE_CODE,PLINE_NAME,input_person,input_time) "
                         + "VALUES ('" + theCompanyCode + "','" + aCode.Text.Trim() + "','" + aName.Text.Trim() + "',"
                         + "'" + PlineId.Value.ToString() + "','" + plineCode + "','" + plineName + "','"+theUserId+"',sysdate)";

            dc.ExeSql(inSql);

            //插入到日志表
            try
            {
                string Sql2 = "INSERT INTO ATPU_ACROSSLINE_LOG (COMPANY_CODE,ALINE_CODE,ALINE_NAME,PLINE_ID,PLINE_CODE,PLINE_NAME,user_code,flag,rqsj)"
                    + " SELECT COMPANY_CODE,ALINE_CODE,ALINE_NAME,PLINE_ID,PLINE_CODE,PLINE_NAME,'"
                    + theUserCode + "' , 'ADD', SYSDATE FROM ATPU_ACROSSLINE WHERE COMPANY_CODE='" + theCompanyCode + "' and ALINE_CODE='"
                    + aCode.Text.Trim() + "'";
                dc.ExeSql(Sql2);
            }
            catch
            {
                return;
            }

            string pline = PlineId.Value.ToString();
            string alineCode = aCode.Text.Trim();
            string alineName = aName.Text.Trim();
            AlineFactory.MW_CREATE_ALINE("CREATE", theCompanyCode, pline, alineCode, alineName);

            e.Cancel = true;
            ASPxGridView2.CancelEdit();
            queryFunction();
        }
        protected void btnXlsExport_Click(object sender, EventArgs e)
        {
            gridExport.WriteXlsToResponse("跨线工位工序关系");
        }
    }
}