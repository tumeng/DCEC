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
using System.Windows.Forms;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxClasses;
using Rmes.Pub.Data;
using Rmes.DA.Base;
using Rmes.Web.Base;

/**
 * 功能概述：SO托架类型维护
 * 作者：杨少霞
 * 创建时间：2016-08-08
**/

namespace Rmes.WebApp.Rmes.Atpu.atpu1B00
{
    public partial class atpu1B00 : BasePage
    {
        private dataConn dc = new dataConn();
        private PubCs pc = new PubCs();
        public string theCompanyCode, theUserId, theUserName, theUserCode;
        public string theProgramCode;

        protected void Page_Load(object sender, EventArgs e)
        {
            userManager theUserManager = (userManager)Session["theUserManager"];
            theCompanyCode = theUserManager.getCompanyCode();
            theUserId = theUserManager.getUserId();
            theUserName = theUserManager.getUserName();
            theUserCode = theUserManager.getUserCode();
            theProgramCode = "atpu1B00";

            setCondition();

            string Sql = "SELECT SO FROM copy_engine_property order by so ";
            SqlDataSource1.SelectCommand = Sql;
            SqlDataSource1.DataBind();

            string sql2 = "select distinct tjlx from dmtjb order by tjlx";
            SqlDataSource2.SelectCommand = sql2;
            SqlDataSource2.DataBind();
        }
        private void setCondition()
        {

            string sql = "SELECT SO,TJLX,YHMC,RQSJ FROM ATPUSOTJB order by RQSJ desc nulls last";
            DataTable dt = dc.GetTable(sql);

            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();
        }
        //新增
        protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            ASPxComboBox uSO = ASPxGridView1.FindEditFormTemplateControl("comboSO") as ASPxComboBox;
            ASPxComboBox tjlx = ASPxGridView1.FindEditFormTemplateControl("comboTJLX") as ASPxComboBox;

            string Sql = "INSERT INTO ATPUSOTJB (SO,TJLX,YHMC,RQSJ) "
                   + "VALUES( '" + uSO.Value.ToString() + "','" + tjlx.Value.ToString() + "','" + theUserName + "',sysdate)";
            dc.ExeSql(Sql);
            //插入日志表
            string logSql = "INSERT INTO ATPUSOTJLOG (SO,TJLX,YHMC,RQSJ,CZSM) "
                   + "VALUES( '" + uSO.Value.ToString() + "','" + tjlx.Value.ToString() + "','" + theUserName + "',sysdate,'ADD')";
            dc.ExeSql(logSql);

            e.Cancel = true;
            ASPxGridView1.CancelEdit();
            setCondition();
        }

        //删除
        protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            //判断当前记录是否可以删除
            string strCode = e.Values["SO"].ToString();
            string strTableName = "ATPUSOTJB";

            dataConn theDataConn = new dataConn("select func_check_delete_data('" + strTableName + "','" + theCompanyCode + "','MES','MES','MES','" + strCode + "') from dual");

            theDataConn.OpenConn();
            string theRet = theDataConn.GetValue();
            if (theRet != "Y")
            {
                ASPxGridView1.JSProperties.Add("cpCallbackName", "Delete");
                ASPxGridView1.JSProperties.Add("cpCallbackRet", theRet);
                theDataConn.CloseConn();
            }
            else
            {
                //插入日志表
                string logSql = "INSERT INTO ATPUSOTJLOG (SO,TJLX,YHMC,RQSJ,CZSM) "
                   + "VALUES( '" + e.Values["SO"].ToString() + "','" + e.Values["TJLX"].ToString() + "','" + theUserName + "',sysdate,'DEL')";
                dc.ExeSql(logSql);
                //确认删除
                string Sql = "delete from ATPUSOTJB WHERE  SO =  '" + strCode + "'";
                dc.ExeSql(Sql);
                
            }

            e.Cancel = true;
            setCondition();
        }

        //数据校验
        protected void ASPxGridView1_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            //判断为空
            if (e.NewValues["SO"].ToString() == "" || e.NewValues["SO"].ToString() == null)
            {
                e.RowError = "SO不能为空！";
            }

            string strSO = e.NewValues["SO"].ToString().Trim();
            //判断超长
            if (strSO.Length > 30)
            {
                e.RowError = "初始值字节长度不能超过30！";
            }

            //判断是否重复
            if (ASPxGridView1.IsNewRowEditing)
            {
                string chSql = "SELECT SO  FROM ATPUSOTJB WHERE SO = '" + strSO + "'";
                DataTable dt = dc.GetTable(chSql);
                if (dt.Rows.Count > 0)
                {
                    e.RowError = "已存在相同的SO号！";
                }

            }

        }
        
    }
}