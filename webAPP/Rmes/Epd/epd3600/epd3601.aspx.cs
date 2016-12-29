using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using DevExpress.Web.ASPxGridLookup;
using DevExpress.Web.ASPxClasses;
using DevExpress.Web.ASPxEditors;

using PetaPoco;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Procedures;
using Rmes.Pub.Data;
using Rmes.Web.Base;
using System.IO;


/**
 * 功能概述：工序文件上传专用页面
 * 作    者：yangshx
 * 创建时间：2016-06-17
 */


public partial class Rmes_epd3601 : BasePage
{
    private dataConn dc = new dataConn();
    public Database db = DB.GetInstance();
    private PubCs pc = new PubCs();
    public string theCompanyCode, theProgramCode, theUserId, theUserCode;

    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();
        theProgramCode = "epd3600";
        theUserId = theUserManager.getUserId();
        theUserCode = theUserManager.getUserCode();

        string sql = "select a.pline_code,b.rmes_id,b.pline_name from VW_USER_ROLE_PROGRAM a "
                    + "left join code_product_line b on a.pline_code=b.pline_code "
                    + "where a.COMPANY_CODE = '" + theCompanyCode + "' and a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' order by b.PLINE_NAME";
        comboPlineCode.DataSource = dc.GetTable(sql);
        comboPlineCode.DataBind();

        
    }

    protected void listBoxProcess_Callback(object sender, CallbackEventArgsBase e)
    {
        string pline = e.Parameter;

        string sql = "select t.rmes_id,t.process_code||'-'||t.process_name showName from code_process t "
                   + " where t.pline_code='" + pline + "' "
                   + " order by t.process_code";
        DataTable dt = dc.GetTable(sql);

        ASPxComboBox location = sender as ASPxComboBox;
        location.DataSource = dt;
        location.ValueField = "rmes_id";
        location.TextField = "showName";
        location.DataBind();
    }
    protected void comboSeries_Callback(object sender, CallbackEventArgsBase e)
    {
        string pline = e.Parameter;

        //string sql = "select t.rmes_id,t.PRODUCT_SERIES showName from CODE_PRODUCT_SERIES t "
        //           + " where t.pline_code='" + pline + "' "
        //           + " order by t.PRODUCT_SERIES";
        string sql = "select distinct xl rmes_id,xl showName from copy_engine_property t where xl is not null order by xl";
        DataTable dt = dc.GetTable(sql);

        ASPxComboBox series = sender as ASPxComboBox;
        series.DataSource = dt;
        series.ValueField = "rmes_id";
        series.TextField = "showName";
        series.DataBind();
    }
    
    protected void butConfirm_Click(object sender, EventArgs e)
    {
        try
        {
            string pline, process, flag, showName, url, pSeries;

            pline = comboPlineCode.SelectedItem.Value.ToString();
            process = ASPxListBoxProcess.SelectedItem.Value.ToString();
            flag = ASPxComboFlag.SelectedItem.Value.ToString();
            showName = textFileName.Text.Trim();
            pSeries = comboSeries.SelectedItem.Value.ToString();
            string pSeriesCode = comboSeries.SelectedItem.Text;
            //string FilePath = Server.MapPath("/") + "file\\" + flag;
            string FilePath = dc.GetValue("select INTERNAL_VALUE FROM CODE_INTERNAL WHERE COMPANY_CODE='" + theCompanyCode
                        + "' AND INTERNAL_TYPE_CODE='PATH' AND INTERNAL_CODE='PROCESSFILEPATH'");

            string strFileName = "";
            FilePath = FilePath + "\\" + pSeriesCode + "\\";
            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }
            if (fileup.HasFile)
            {
                strFileName = FilePath + System.IO.Path.GetFileName(fileup.FileName);

                try
                {
                    fileup.SaveAs(strFileName);
                }
                catch
                {
                    lblMessage.Text = "文件上传失败！";
                    return;
                }
            }

            //url = "http://10.1.33.1/file/" + flag + "/go.html?p=" + System.IO.Path.GetFileName(fileup.FileName);
            //url = "~/file/" + flag + "/go.html?p=" + System.IO.Path.GetFileName(fileup.FileName);
            //url = "~/file/" + flag + "/" + System.IO.Path.GetFileName(fileup.FileName);
            url = strFileName;
            ProcessFileEntity pf = new ProcessFileEntity();
            pf.COMPANY_CODE = theCompanyCode;
            pf.PLINE_CODE = pline;
            pf.PROCESS_CODE = process;
            pf.FILE_NAME = showName;
            pf.FILE_URL = url;
            pf.FILE_TYPE = flag;
            pf.PRODUCT_SERIES = pSeries;
            pf.INPUT_PERSON = theUserId;//20161031add
            pf.INPUT_TIME = DateTime.Now;//20161031add

            //插入到日志表//20161031add
            try
            {
                string Sql2 = " INSERT INTO DATA_PROCESS_FILE_LOG(rmes_id,company_code,pline_code,product_series,process_code,file_name,file_url,file_type,user_code,flag,rqsj)"
                            + " VALUES( '" + pf.RMES_ID + "', '" + theCompanyCode + "','" + pline + "','" + pSeries + "','" + process + "','" + showName + "','" + url + "','" + flag + "','" + theUserCode + "','ADD',SYSDATE)";
                dc.ExeSql(Sql2);
            }
            catch
            {
                return;
            }

            //string id = db.Insert("DATA_PROCESS_FILE", "RMES_ID", pf).ToString();

            string id1 = db.Insert(pf).ToString();

            //string inSql = "insert into data_process_file(company_code,pline_code,process_code,file_name,file_url,file_type) " +
            //                   "values('" + theCompanyCode + "','" + pline + "','" + process
            //                   + "','" + showName + "','" + url + "','" + flag + "')";
            //dc.ExeSql(inSql);

            lblMessage.Text = fileup.FileName + "上传成功！";
            textFileName.Text = "";
        }
        catch (Exception e2)
        {
            lblMessage.Text = "文件上传失败！"+e2.Message;
            return;
        }
    }

    protected void butCloseWindow_Click(object sender, EventArgs e)
    {
        Response.Write("<script type='text/javascript'>window.opener.location.reload();window.close();</script>");
    }

}