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
using Rmes.DA.Base;
/**
 * 功能概述：站点代码表维护
 * 作者：游晓航
 * 创建时间：2016-06-02
 */

public partial class Rmes_epd1200 : Rmes.Web.Base.BasePage
{
    private dataConn dc = new dataConn();
    private PubCs pc = new PubCs();
    public string theCompanyCode;
    private string theUserId;
    public string theProgramCode;

    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();
        theUserId = theUserManager.getUserId();
        theProgramCode = "epd1200";

        setCondition();
        
    }
    
    private void setCondition()
    {
        //string sql = "select T.RMES_ID,T.COMPANY_CODE,T.PLINE_CODE,T.STATION_CODE,T.STATION_NAME,decode(T.STATION_TYPE,'A','上线站点','B','一般站点','C','下线站点','一般站点') STATION_TYPE,STATION_AREA "
        //    + " from CODE_STATION t where t.company_code='" + theCompanyCode + "' order by t.STATION_CODE";
        //string sql = "select T.RMES_ID,T.COMPANY_CODE,T.PLINE_CODE,T.STATION_CODE,T.STATION_NAME,STATION_TYPE,STATION_AREA "
        //    + " from CODE_STATION t where t.company_code='" + theCompanyCode + "' order by t.STATION_CODE";
        string sql = "select DISTINCT A.RMES_ID,A.COMPANY_CODE,A.PLINE_CODE,d.pline_code plinecode1,A.STATION_CODE,A.STATION_NAME,A.STATION_AREA,A.STATION_SEQ,A.STATION_REMARK,B.STATION_TYPE_CODE,B.STATION_TYPE_NAME,C.AREA_CODE,C.AREA_NAME "
             + " from CODE_STATION A LEFT JOIN CODE_STATION_TYPE B ON A.STATION_TYPE = B.STATION_TYPE_CODE "
             + "  LEFT JOIN CODE_STATION_AREA C ON A.STATION_AREA = C.AREA_CODE left join code_product_line d on d.rmes_id=a.pline_code " 
             + " where A.company_code = '" + theCompanyCode + "' and a.pline_code in ( select pline_id from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "') order by A.STATION_SEQ,A.STATION_CODE ";

 

        DataTable dt = dc.GetTable(sql);

        ASPxGridView1.DataSource = dt;
        ASPxGridView1.DataBind();
    }

    //新增
    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxComboBox uPCode = ASPxGridView1.FindEditFormTemplateControl("txtPCode") as ASPxComboBox;
        ASPxTextBox uSCode = ASPxGridView1.FindEditFormTemplateControl("txtSCode") as ASPxTextBox;
        ASPxTextBox uSName = ASPxGridView1.FindEditFormTemplateControl("txtSName") as ASPxTextBox;
        ASPxComboBox uSType = ASPxGridView1.FindEditFormTemplateControl("txtSType") as ASPxComboBox;
        ASPxComboBox uSArea = ASPxGridView1.FindEditFormTemplateControl("txtSArea") as ASPxComboBox;
        ASPxTextBox uSSeq = ASPxGridView1.FindEditFormTemplateControl("txtSSeq") as ASPxTextBox;
        ASPxTextBox uSRemark = ASPxGridView1.FindEditFormTemplateControl("txtSRemark") as ASPxTextBox;
      
        string strPCode = uPCode.Value.ToString();
        
        string strSName = uSName.Text.Trim().ToUpper();
        string strSType = uSType.Value.ToString();
        string strSArea = uSArea.Value.ToString();
        string strSSeq = uSSeq.Text.Trim();
        string strSRemark = uSRemark.Text.Trim();
        //string strSCode = "MESZD"+strPCode+;

        string chSql2 = "SELECT STATION_SEQ FROM CODE_STATION WHERE COMPANY_CODE = '" + theCompanyCode + "' AND STATION_SEQ='" + strSSeq + "'  AND PLINE_CODE='" + strPCode + "'  ";
        DataTable dt2 = dc.GetTable(chSql2);
        if (dt2.Rows.Count > 0)
        {
            string UpSql = "UPDATE CODE_STATION SET STATION_SEQ = STATION_SEQ+1 WHERE COMPANY_CODE = '" + theCompanyCode + "'and STATION_SEQ >= '" + strSSeq + "' AND PLINE_CODE='" + strPCode + "' ";
            dc.ExeSql(UpSql);

            e.Cancel = true;

        }
        string Sql = "INSERT INTO CODE_STATION (RMES_ID,COMPANY_CODE,PLINE_CODE,STATION_CODE,STATION_NAME,STATION_TYPE,STATION_AREA,STATION_SEQ,STATION_REMARK) "
             + "VALUES('MESZD'||TRIM(TO_CHAR(SEQ_USER_ID.NEXTVAL,'000000')), '" + theCompanyCode + "','" + strPCode + "','MESZD'||TRIM(TO_CHAR(SEQ_USER_ID.NEXTVAL,'000000')),'" + strSName + "','" + strSType + "','" + strSArea + "','" + strSSeq + "','" + strSRemark + "')";
        dc.ExeSql(Sql);

        e.Cancel = true;
        ASPxGridView1.CancelEdit();
        setCondition();
    }

    //删除
    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        //判断当前记录是否可以删除
        string strCode = e.Values["RMES_ID"].ToString();
        string strSSeq = e.Values["STATION_SEQ"].ToString();
        string strPCode = e.Values["PLINE_CODE"].ToString();
        string strTableName = "CODE_STATION";

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
            //确认删除
            string Sql = "delete from CODE_STATION WHERE  COMPANY_CODE = '" + theCompanyCode + "' and RMES_ID = '" + strCode + "' AND PLINE_CODE='" + strPCode + "' ";
            dc.ExeSql(Sql);
            string UpSql = "UPDATE CODE_STATION SET STATION_SEQ = STATION_SEQ-1 WHERE COMPANY_CODE = '" + theCompanyCode + "'and STATION_SEQ >= '" + strSSeq + "' AND PLINE_CODE='" + strPCode + "' ";
            dc.ExeSql(UpSql);

        }


        setCondition();
        e.Cancel = true;
    }

    //修改
    protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {

        //ASPxTextBox uCode = ASPxGridView1.FindEditFormTemplateControl("txtPCode") as ASPxTextBox;
        //ASPxTextBox uName = ASPxGridView1.FindEditFormTemplateControl("txtPName") as ASPxTextBox;

        //string strCode = uCode.Text.Trim();
        //string strName = uName.Text.Trim();
        ASPxComboBox uPCode = ASPxGridView1.FindEditFormTemplateControl("txtPCode") as ASPxComboBox;
        ASPxTextBox uSCode = ASPxGridView1.FindEditFormTemplateControl("txtSCode") as ASPxTextBox;
        ASPxTextBox uSName = ASPxGridView1.FindEditFormTemplateControl("txtSName") as ASPxTextBox;
        ASPxComboBox uSType = ASPxGridView1.FindEditFormTemplateControl("txtSType") as ASPxComboBox;
        ASPxComboBox uSArea = ASPxGridView1.FindEditFormTemplateControl("txtSArea") as ASPxComboBox;
        ASPxTextBox uSSeq = ASPxGridView1.FindEditFormTemplateControl("txtSSeq") as ASPxTextBox;
        ASPxTextBox uSRemark = ASPxGridView1.FindEditFormTemplateControl("txtSRemark") as ASPxTextBox;

        string strPCode = uPCode.Value.ToString();
        string strSCode = uSCode.Text.Trim();
        string strSName = uSName.Text.Trim().ToUpper();
        string strSType = uSType.Value.ToString();
        string strSArea = uSArea.Value.ToString();
        string strSSeq = uSSeq.Text.Trim();
        string strSRemark = uSRemark.Text.Trim();
        string NstrSEQ = e.NewValues["STATION_SEQ"].ToString().Trim();
        string OstrSEQ = e.OldValues["STATION_SEQ"].ToString().Trim();

        int Nseq = Convert.ToInt32(NstrSEQ);   

        int Oseq = Convert.ToInt32(OstrSEQ);
        if (Nseq > Oseq )
        {
            string UpSql = "UPDATE CODE_STATION SET STATION_SEQ = STATION_SEQ-1 WHERE COMPANY_CODE = '" + theCompanyCode + "'and STATION_SEQ <= '" + NstrSEQ + "' and STATION_SEQ >'" + OstrSEQ + "'AND PLINE_CODE='" + strPCode + "' ";
            dc.ExeSql(UpSql);

            e.Cancel = true;

        }
        else if (Nseq < Oseq)
        {
            string UpSql = "UPDATE CODE_STATION SET STATION_SEQ = STATION_SEQ+1 WHERE COMPANY_CODE = '" + theCompanyCode + "'and STATION_SEQ >= '" + NstrSEQ + "' and STATION_SEQ <'" + OstrSEQ + "'AND PLINE_CODE='" + strPCode + "' ";
            dc.ExeSql(UpSql);

            e.Cancel = true;

        }

        string Sql = "UPDATE CODE_STATION SET PLINE_CODE='" + strPCode + "',STATION_CODE='" + strSCode + "',STATION_NAME='" + strSName + "',STATION_TYPE='" + strSType + "',STATION_AREA='" + strSArea + "',"
             + "STATION_SEQ='" + strSSeq + "',STATION_REMARK='" + strSRemark + "' WHERE COMPANY_CODE = '" + theCompanyCode + "' and STATION_CODE = '" + strSCode + "'";
        dc.ExeSql(Sql);

        e.Cancel = true;
        ASPxGridView1.CancelEdit();
        setCondition();
    }

    //创建EDITFORM前
    protected void ASPxGridView1_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditFormEventArgs e)
    {
        string Sql = "select STATION_TYPE_CODE,STATION_TYPE_CODE||' '||STATION_TYPE_NAME as showtext from CODE_STATION_TYPE";
        DataTable dt = dc.GetTable(Sql);
        ASPxComboBox uSType = ASPxGridView1.FindEditFormTemplateControl("txtSType") as ASPxComboBox;
        uSType.DataSource = dt;
        uSType.TextField = dt.Columns[1].ToString();
        uSType.ValueField = dt.Columns[0].ToString();


        string PSql = "select RMES_ID PLINE_CODE,PLINE_NAME,PLINE_CODE from CODE_PRODUCT_LINE where pline_code in (select pline_code from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "')";
        DataTable Pdt = dc.GetTable(PSql);
        ASPxComboBox uPcode = ASPxGridView1.FindEditFormTemplateControl("txtPCode") as ASPxComboBox;
        uPcode.DataSource = Pdt;
        uPcode.TextField = Pdt.Columns[1].ToString();
        uPcode.ValueField = Pdt.Columns[0].ToString();
        
        //uPcode.SelectedIndex = uPcode.Items.Count >= 0 ? 0 : -1;

        string ASql = "select distinct AREA_CODE,AREA_CODE||' '||AREA_NAME as showtext from CODE_STATION_AREA where pline_code in "
            +"(select pline_code from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "') order by case AREA_CODE when 'A' then 1 when 'T' then 2 when 'P' then 3 when 'U' then 4 end";
        DataTable Adt = dc.GetTable(ASql);
        ASPxComboBox uSArea = ASPxGridView1.FindEditFormTemplateControl("txtSArea") as ASPxComboBox;
        uSArea.DataSource = Adt;
        uSArea.TextField = Adt.Columns[1].ToString();
        uSArea.ValueField = Adt.Columns[0].ToString();

        if (!ASPxGridView1.IsNewRowEditing && ASPxGridView1.IsEditing)
        {
            ///主键不可以修改
            (ASPxGridView1.FindEditFormTemplateControl("txtSCode") as ASPxTextBox).Enabled = false;
            (ASPxGridView1.FindEditFormTemplateControl("txtPCode") as ASPxComboBox).Enabled = false;
            
        }
        else
        {
            uPcode.SelectedIndex = uPcode.Items.Count >= 0 ? 0 : -1;
            //uTcode.SelectedIndex = 0;
        }
        
    }

    //修改数据校验
    protected void ASPxGridView1_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        //判断为空
        //if (e.NewValues["STATION_CODE"].ToString() == "" || e.NewValues["STATION_CODE"].ToString() == null)
        //{
        //    e.RowError = "站点代码不能为空！";
        //}
        if (e.NewValues["STATION_NAME"].ToString() == "" || e.NewValues["STATION_NAME"].ToString() == null)
        {
            e.RowError = "站点名称不能为空！";
        }

        string strCode = e.NewValues["STATION_CODE"].ToString().Trim();
        string strName = e.NewValues["STATION_NAME"].ToString().Trim().ToUpper();
        string strSEQ = e.NewValues["STATION_SEQ"].ToString().Trim();

        //判断超长
        //if (strCode.Length > 30)
        //{
        //    e.RowError = "站点代码字节长度不能超过30！";
        //}
        if (strName.Length > 30)
        {
            e.RowError = "站点名称字节长度不能超过30！";
        }
        //判断站点顺序是否为数字


        //判断是否重复
        if (ASPxGridView1.IsNewRowEditing)
        {
            string chSql = "SELECT STATION_CODE,STATION_NAME  FROM CODE_STATION WHERE COMPANY_CODE = '" + theCompanyCode + "' AND STATION_NAME='" + strName + "'";
            DataTable dt = dc.GetTable(chSql);
            if (dt.Rows.Count > 0)
            {
                e.RowError = "已存在相同的站点名称！";
            }
            //string chSql2 = "SELECT STATION_SEQ FROM CODE_STATION WHERE COMPANY_CODE = '" + theCompanyCode + "' AND STATION_SEQ='" + strSEQ + "'";
            //DataTable dt2 = dc.GetTable(chSql2);
            //if (dt2.Rows.Count > 0)
            //{
            //    e.RowError = "已存在相同的站点顺序！";
            //}
        }

    }
    protected void btnXlsExport_Click(object sender, EventArgs e)
    {
        ASPxGridViewExporter1.WriteXlsToResponse("站点信息导出");
    }

}
