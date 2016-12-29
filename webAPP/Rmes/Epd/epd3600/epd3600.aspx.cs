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
using Rmes.Pub.Data;
using Rmes.Pub.Function;
using DevExpress.Web.ASPxGridLookup;
using DevExpress.Web.ASPxUploadControl;
using System.IO;
//以下引用，如果采用实体类，请全部复制到新页面
using PetaPoco;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;
using Rmes.Web.Base;

/**
 * 功能概述：工序文件查看修改删除页面
 * 作    者：李蒙蒙
 * 创建时间：2014-04-17
 */


namespace Rmes.WebApp.Rmes.Epd.epd3600
{
    public partial class epd3600 :BasePage
    {
        private dataConn dc = new dataConn();
        public static string theCompanyCode, theProgramCode, theUserId,theUserCode;
        public dataConn theDc = new dataConn();
        public Database db = DB.GetInstance();

        protected void Page_Load(object sender, EventArgs e)
        {
            userManager theUserManager = (userManager)Session["theUserManager"];
            theCompanyCode = theUserManager.getCompanyCode();
            theProgramCode = "epd3600";
            theUserId = theUserManager.getUserId();
            theUserCode = theUserManager.getUserCode();

            queryFunction();
        }

        private void queryFunction()
        {
            string sql = "SELECT a.RMES_ID,a.company_code,a.pline_code,b.pline_code plinecode1,b.pline_name,a.process_code as process_code1,c.process_name,a.file_name,a.process_code as process_id,"
                       +"a.product_series product_series_id,a.file_url,a.file_type,"
                       + "decode(a.file_type,'A','作业指导书','B','质量检查卡','C','工艺图纸') file_type_name, a.product_series product_series "

                       + "FROM data_process_file a "
                       + "left join code_product_line b on a.company_code=b.company_code and a.pline_code=b.rmes_id "
                       + "left join code_process c on a.company_code=c.company_code and a.process_code=c.RMES_ID "
                       //+ "left join data_rounting_remark d on a.PRODUCT_SERIES=d.rmes_id "
                       + " WHERE A.COMPANY_CODE = '" + theCompanyCode + "' "
                       + "and a.pline_code in(select w.rmes_id from VW_USER_ROLE_PROGRAM t left join code_product_line w on t.pline_code=w.pline_code "
                       + "where t.user_id='" + theUserId + "' and t.program_code='" + theProgramCode + "')"
                       + "ORDER BY a.INPUT_TIME desc nulls last";//20161031增加时间列倒序排列
            DataTable dt = theDc.GetTable(sql);

            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();
        }


        // 主表删除
        protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string rmes_id = e.Values["RMES_ID"].ToString();

            //插入到日志表//20161031add
            try
            {
                string Sql1 = " SELECT * FROM data_process_file WHERE rmes_id='" + rmes_id + "'";
                dc.setTheSql(Sql1);
                string company_code = dc.GetTable().Rows[0]["COMPANY_CODE"].ToString();
                string pline_code = dc.GetTable().Rows[0]["PLINE_CODE"].ToString();
                string product_series = dc.GetTable().Rows[0]["PRODUCT_SERIES"].ToString();
                string process_code = dc.GetTable().Rows[0]["PROCESS_CODE"].ToString();
                string file_name = dc.GetTable().Rows[0]["FILE_NAME"].ToString();
                string file_url = dc.GetTable().Rows[0]["FILE_URL"].ToString();
                string file_type = dc.GetTable().Rows[0]["FILE_TYPE"].ToString();
                string Sql2 = " INSERT INTO DATA_PROCESS_FILE_LOG(rmes_id,company_code,pline_code,product_series,process_code,file_name,file_url,file_type,user_code,flag,rqsj)"
                            + " VALUES( '" + rmes_id + "', '" + company_code + "','" + pline_code + "','" + product_series + "','" + process_code + "','" + file_name + "','" + file_url + "','" + file_type + "','" + theUserCode + "','DEL',SYSDATE)";
                dc.ExeSql(Sql2);
            }
            catch
            {
                return;
            }

            ProcessFileEntity detEntity = new ProcessFileEntity { RMES_ID = rmes_id };
            db.Delete(detEntity);

            queryFunction();
            e.Cancel = true;
        }
        protected void btnXlsExport_Click(object sender, EventArgs e)
        {
            ASPxGridViewExporter1.WriteXlsToResponse("工序指导书信息导出");
        }
    }
}