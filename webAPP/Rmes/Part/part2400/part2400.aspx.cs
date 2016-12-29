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
using DevExpress.Web.ASPxGridLookup;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxClasses;
using Rmes.Pub.Data;
using Rmes.DA.Base;
/**
 * 功能概述：库存待冲抵物料维护
 * 作者：杨少霞
 * 创建时间：2016-11-11
 */

public partial class Rmes_part2400 : Rmes.Web.Base.BasePage
{
    private dataConn dc = new dataConn();
    private PubCs pc = new PubCs();
    public string theCompanyCode, theUserId,theUserCode;
    public string theProgramCode;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();

        theProgramCode = "part2400";
        theUserId = theUserManager.getUserId();
        theUserCode = theUserManager.getUserCode();

        //initPlineCode();
        //初始查询条件生产线
        string Sql = " select distinct a.pline_code,b.pline_name as showtext from vw_user_role_program a left join code_product_line b on a.pline_code=b.pline_code where a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' and a.company_code='" + theCompanyCode + "' ";
        comPLQ.DataSource = dc.GetTable(Sql);
        comPLQ.DataBind();

        initGYS();
        setCondition();
        if (Request["opFlag"] == "getEditM")
        {
            string str1 ="" ;
            string MCode = Request["MCODE"].ToString().Trim();

            string sql = "select nvl(pt_desc2,' ') from copy_pt_mstr where UPPER(pt_part)='" + MCode + "'";
            dc.setTheSql(sql);
            DataTable dt = dc.GetTable(sql);
            if (dt.Rows.Count == 0)
            {
                str1 = "";
                this.Response.Write(str1);
                this.Response.End();
                return;
            }

            string strMCode = dc.GetTable().Rows[0][0].ToString();
            if (strMCode == "")
            {
                str1 = "";
                this.Response.Write(str1);
                this.Response.End();
                return;
            }
            else { str1 = strMCode; }

            this.Response.Write(str1);
            this.Response.End();
        }
        if (Request["opFlag"] == "getEditGYS")
        {
            string str1 = "";
            string GCode = Request["GCODE"].ToString().Trim();

            string sql = "select nvl(ad_name,' ') from copy_ad_mstr where UPPER(ad_addr)='" + GCode + "'";
            dc.setTheSql(sql);
            DataTable dt = dc.GetTable(sql);
            if (dt.Rows.Count == 0)
            {
                str1 = "";
                this.Response.Write(str1);
                this.Response.End();
                return;
            }

            string strGCode = dc.GetTable().Rows[0][0].ToString();
            if (strGCode == "")
            {
                str1 = "";
                this.Response.Write(str1);
                this.Response.End();
                return;
            }
            else { str1 = strGCode; }

            this.Response.Write(str1);
            this.Response.End();
        }
        if (!IsPostBack)
        {
            //comPLQ.SelectedIndex = comPLQ.Items.Count >= 0 ? 0 : -1;//
            StartDateQ.Date = DateTime.Now.AddDays(-1);
            EndDateQ.Date = DateTime.Now.AddDays(1);
        }
    }
    private void initPlineCode()
    {
        ////初始查询条件生产线
        //string Sql = " select distinct a.pline_code,b.pline_name as showtext from vw_user_role_program a left join code_product_line b on a.pline_code=b.pline_code where a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' and a.company_code='" + theCompanyCode + "' ";

        //SqlPL.SelectCommand = Sql;
        //SqlPL.DataBind();
    }
    private void initGYS()
    {
        //初始化供应商下拉列表
        string sql = "select ad_addr from copy_ad_mstr where ad_addr is not null order by ad_addr";

        SqlGYScode.SelectCommand = sql;
        SqlGYScode.DataBind();
    }
    private void setCondition()
    {
        string selSql = "select a.material_code,a.gys_code,a.material_num,a.add_yh,to_char(a.ADD_TIME,'yyyy-mm-dd hh24:mi:ss') ADD_TIME,a.ready_flag,"
                       + "to_char(a.last_handle_time,'yyyy-mm-dd hh24:mi:ss') last_handle_time,a.last_handle_num,a.bill_code,a.gzdd,b.PT_DESC2,c.ad_name "
                       + " from ms_inv_mat a "
                       +" left join copy_pt_mstr b on a.material_code=b.pt_part "
                       + " left join copy_ad_mstr c on a.gys_code=c.ad_addr "
                       +"where 1=1 ";
        //生产线必选，否则没数据
         if (comPLQ.Value == null)
         {
             selSql += " AND a.GZDD = '' ";
         }
         else
         {
             selSql += " and a.gzdd='" + comPLQ.Value.ToString() + "' ";
         }
        if (TxtMatCodeQ.Text != "")
        {
            selSql = selSql + " and a.material_code like '" + TxtMatCodeQ.Text.Trim() + "%'";
        }
        if (txtGysCodeQ.Text != "")
        {
            selSql = selSql + " and a.gys_code like '" + txtGysCodeQ.Text.Trim() + "%'";
        }
        selSql = selSql + " order by 1,2";
        DataTable dt = dc.GetTable(selSql);
        ASPxGridView1.DataSource = dt;
        ASPxGridView1.DataBind();

        string selSql2 = "select material_code,gys_code,cur_handle_num,to_char(CUR_HANDLE_TIME,'yyyy-mm-dd hh24:mi:ss') CUR_HANDLE_TIME,bill_code,gzdd "
                       + "from ms_inv_mat_log "
                       + "where 1=1 ";
        //生产线必选，否则没数据
        if (comPLQ.Value == null)
        {
            selSql2 += " AND GZDD = '' ";
        }
        else
        {
            selSql2 += " and gzdd='" + comPLQ.Value.ToString() + "' ";
        }
        if (TxtMatCodeQ.Text != "")
        {
            selSql2 = selSql2 + " and material_code like UPPER('%" + TxtMatCodeQ.Text.Trim() + "%') ";
        }
        if (txtGysCodeQ.Text != "")
        {
            selSql2 = selSql2 + " and gys_code like UPPER('%" + txtGysCodeQ.Text.Trim() + "%') ";
        }
        selSql2 = selSql2 + " order by 1,2";
        DataTable dt2 = dc.GetTable(selSql2);
        ASPxGridView2.DataSource = dt2;
        ASPxGridView2.DataBind();

    }
    private void setCondition2()
    {
        string selSql2 = "select material_code,gys_code,cur_handle_num,to_char(CUR_HANDLE_TIME,'yyyy-mm-dd hh24:mi:ss') CUR_HANDLE_TIME,bill_code "
                       + "from ms_inv_mat_log "
                       + "where gzdd='" + comPLQ.Value.ToString() + "'";
        if (txtMatCodeLog.Text != "")
        {
            selSql2 = selSql2 + " and material_code like UPPER('%" + txtMatCodeLog.Text.Trim() + "%') ";
        }
        if (txtGysCodeLog.Text != "")
        {
            selSql2 = selSql2 + " and gys_code like UPPER('%" + txtGysCodeLog.Text.Trim() + "%') ";
        }
        if (txtBillCodeQ.Text != "")
        {
            selSql2 = selSql2 + " and bill_code like UPPER('%" + txtBillCodeQ.Text.Trim() + "%') ";
        }
        if (StartDateQ.Value != null)
        {
            selSql2 = selSql2 + " and CUR_HANDLE_TIME >= to_date('" + StartDateQ.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss') ";
        }
        if (EndDateQ.Value != null)
        {
            selSql2 = selSql2 + " and CUR_HANDLE_TIME <= to_date('" + EndDateQ.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss') ";
        }
        selSql2 = selSql2 + " order by 1,2";
        DataTable dt2 = dc.GetTable(selSql2);
        ASPxGridView2.DataSource = dt2;
        ASPxGridView2.DataBind();
    }
    public static bool IsInt(string str)
    {
        if (str == string.Empty)
            return false;
        try
        {
            Convert.ToInt32(str);
            return true;
        }
        catch
        {
            return false;
        }
    }
    protected void txtMCode_Callback(object sender, CallbackEventArgsBase e)
    {
        string pline = e.Parameter;

        string sql = "select INTERNAL_NAME from CODE_INTERNAL where INTERNAL_CODE='" + pline + "' AND INTERNAL_TYPE_CODE='012'";
        DataTable dt = dc.GetTable(sql);
        string qad_site = "";
        if (dt.Rows.Count > 0)
        {
            qad_site = dt.Rows[0][0].ToString();
        }
        string sql2 = "SELECT PTP_PART FROM COPY_PTP_DET WHERE UPPER(PTP_RUN_SEQ1)='JIS' AND UPPER(PTP_SITE)='" + qad_site + "'";
        DataTable dt2 = dc.GetTable(sql2);
        ASPxComboBox mCode = sender as ASPxComboBox;
        mCode.DataSource = dt2;
        mCode.ValueField = "PTP_PART";
        mCode.TextField = "PTP_PART";
        mCode.DataBind();
    }

    //新增
    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxComboBox txtPCode = ASPxGridView1.FindEditFormTemplateControl("comboPCode") as ASPxComboBox;
        ASPxComboBox txtMCode = ASPxGridView1.FindEditFormTemplateControl("txtMCode") as ASPxComboBox;
        ASPxComboBox txtGYSCode = ASPxGridView1.FindEditFormTemplateControl("txtGYSCode") as ASPxComboBox;
        ASPxTextBox txtMNum = ASPxGridView1.FindEditFormTemplateControl("txtMNum") as ASPxTextBox;

        string MCode = txtMCode.Value.ToString().ToUpper();
        string GCode = txtGYSCode.Value.ToString().ToUpper();
        string MNum = txtMNum.Text.Trim();
        
        string Sql = "insert into ms_inv_mat(material_code,material_num,add_yh,add_time,gzdd,gys_code)"
                   + "values('" + MCode + "'," + MNum + ",'" + theUserCode + "',sysdate,'" + txtPCode.Value.ToString() + "','" + txtGYSCode.Value.ToString() + "')";
        dc.ExeSql(Sql);
       
        e.Cancel = true;
        ASPxGridView1.CancelEdit();
        setCondition();
        ASPxGridView1.JSProperties.Add("cpCallbackName", "refresh");

    }

    //删除
    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        //判断当前记录是否可以删除
        string strPLCode = e.Values["PLINE_CODE"].ToString();
        string strMCode = e.Values["MATERIAL_CODE"].ToString().ToUpper();
        string strGCode = e.Values["GYS_CODE"].ToString().ToUpper();
        
        //string chSql = "SELECT * FROM ATPUFR WHERE FR='" + strFR + "' AND SC='" + strSC + "' AND DO='" + strDO + "'";
        //DataTable dt = dc.GetTable(chSql);

        //int theRet = dt.Rows.Count;
        //if (theRet == 0)
        //{
        //    ASPxGridView1.JSProperties.Add("cpCallbackName", "Delete");
        //    ASPxGridView1.JSProperties.Add("cpCallbackRet", theRet);
        //    //theDataConn.CloseConn();
        //}
        //else
        //{
            //确认删除
            string Sql = "delete from ms_inv_mat where material_code='" + strMCode + "' and gys_code='" + strGCode + "' and gzdd='" + strPLCode + "'";
            dc.ExeSql(Sql);
        //}
        e.Cancel = true;
        ASPxGridView1.CancelEdit();
        setCondition();
        ASPxGridView1.JSProperties.Add("cpCallbackName", "refresh");
    }

    //修改
    protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxComboBox txtPCode = ASPxGridView1.FindEditFormTemplateControl("comboPCode") as ASPxComboBox;
        ASPxComboBox txtMCode = ASPxGridView1.FindEditFormTemplateControl("txtMCode") as ASPxComboBox;
        ASPxComboBox txtGYSCode = ASPxGridView1.FindEditFormTemplateControl("txtGYSCode") as ASPxComboBox;
        ASPxTextBox txtMNum = ASPxGridView1.FindEditFormTemplateControl("txtMNum") as ASPxTextBox;

        string MCode = txtMCode.Value.ToString().ToUpper();
        string GCode = txtGYSCode.Value.ToString().ToUpper();
        string MNum = txtMNum.Text.Trim();

        string Sql = "update ms_inv_mat set material_num='" + MNum + "',add_time=sysdate,add_yh='" + theUserCode + "',ready_flag='N' "
                   + "where material_code='" + MCode + "' and gzdd='" + txtPCode.Value.ToString() + "' and gys_code='" + GCode + "'";
        dc.ExeSql(Sql);
        
        e.Cancel = true;
        ASPxGridView1.CancelEdit();
        setCondition();
        ASPxGridView1.JSProperties.Add("cpCallbackName", "refresh");
    }

    //创建EDITFORM前
    protected void ASPxGridView1_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditFormEventArgs e)
    {
        string Sql = " select distinct a.pline_code,b.pline_name as showtext from vw_user_role_program a left join code_product_line b on a.pline_code=b.pline_code where a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' and a.company_code='" + theCompanyCode + "' ";
        DataTable dt = dc.GetTable(Sql);
        ASPxComboBox uPCode = ASPxGridView1.FindEditFormTemplateControl("comboPCode") as ASPxComboBox;
        uPCode.DataSource = dt;
        uPCode.TextField = dt.Columns[1].ToString();
        uPCode.ValueField = dt.Columns[0].ToString();

        if (!ASPxGridView1.IsNewRowEditing && ASPxGridView1.IsEditing)
        {
            (ASPxGridView1.FindEditFormTemplateControl("comboPCode") as ASPxComboBox).Enabled = false;
            (ASPxGridView1.FindEditFormTemplateControl("txtMCode") as ASPxComboBox).Enabled = false;
            (ASPxGridView1.FindEditFormTemplateControl("txtGYSCode") as ASPxComboBox).Enabled = false;
        }
        //if (ASPxGridView1.IsNewRowEditing)
        //{
        //    uPCode.SelectedIndex = uPCode.Items.Count >= 0 ? 0 : -1;//

        //    //string sql1 = "select INTERNAL_NAME from CODE_INTERNAL where INTERNAL_CODE='" + uPCode.Value.ToString() + "' AND INTERNAL_TYPE_CODE='012'";
        //    //DataTable dt1 = dc.GetTable(sql1);
        //    //string qad_site = "";
        //    //if (dt1.Rows.Count > 0)
        //    //{
        //    //    qad_site = dt1.Rows[0][0].ToString();
        //    //}
        //    //string sql2 = "SELECT PTP_PART FROM COPY_PTP_DET WHERE UPPER(PTP_RUN_SEQ1)='JIS' AND UPPER(PTP_SITE)='" + qad_site + "'";
        //    //DataTable dt2 = dc.GetTable(sql2);
        //    //ASPxComboBox mCode = sender as ASPxComboBox;
        //    //mCode.DataSource = dt2;
        //    //mCode.ValueField = "PTP_PART";
        //    //mCode.TextField = "PTP_PART";
        //    //mCode.DataBind();
        //}

        
    }

    //修改数据校验
    protected void ASPxGridView1_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        //判断为空
        if (e.NewValues["MATERIAL_CODE"].ToString() == "" || e.NewValues["MATERIAL_CODE"].ToString() == null)
        {
            e.RowError = "物料代码不能为空！";
        }
        if (e.NewValues["MATERIAL_NUM"].ToString() == "" || e.NewValues["MATERIAL_NUM"].ToString() == null)
        {
            e.RowError = "物料数量不能为空！";
        }
        
        //判断试装台数是否为整数
        if (!IsInt(e.NewValues["MATERIAL_NUM"].ToString()))
        {
            e.RowError = "物料数量必须为正整数!";
        }

        //判断是否重复
        if (ASPxGridView1.IsNewRowEditing)
        {

            string chSql = "select * from ms_inv_mat where material_code='" + e.NewValues["MATERIAL_CODE"].ToString().ToUpper() + "' "
                         + "and gys_code='" + e.NewValues["GYS_CODE"].ToString().ToUpper() + "' and gzdd='" + e.NewValues["GZDD"].ToString() + "'";
            DataTable dt = dc.GetTable(chSql);
            if (dt.Rows.Count > 0)
            {
                e.RowError = "该物料该供应商数据已存在，请选择修改操作!";
            }

        }
    }
    //确认冲抵
    protected void ASPxGridView1_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
    {
        if (e.ButtonID == "SureCD")
        {
            string mCode = ASPxGridView1.GetRowValues(ASPxGridView1.FocusedRowIndex, "MATERIAL_CODE").ToString();
            string gCode = ASPxGridView1.GetRowValues(ASPxGridView1.FocusedRowIndex, "GYS_CODE").ToString().ToUpper();
            string plineCode = ASPxGridView1.GetRowValues(ASPxGridView1.FocusedRowIndex, "GZDD").ToString().ToUpper();
            string cdSql = "update ms_inv_mat set ready_flag='Y' where material_code='" + mCode + "' and gys_code='" + gCode + "' and gzdd='" + plineCode + "'";
            dc.ExeSql(cdSql);

            setCondition();

            ASPxGridView1.JSProperties.Add("cpCallbackName", "refresh");
        }
    }
    protected void ASPxGridView2_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        string sql2 = "select material_code,gys_code,cur_handle_num,to_char(CUR_HANDLE_TIME,'yyyy-mm-dd hh24:mi:ss') CUR_HANDLE_TIME,bill_code "
                    + "from ms_inv_mat_log order by cur_handle_time desc";  //WHERE YHMC='" + theUserId + "'
        DataTable dt2 = dc.GetTable(sql2);
        ASPxGridView2.DataSource = dt2;
        ASPxGridView2.DataBind();
    }
    protected void btnXlsExportPlan_Click(object sender, EventArgs e)
    {
        ASPxGridViewExporter1.WriteXlsToResponse("库存待冲抵物料导出");
    }
    protected void SureAll_Click(object sender, EventArgs e)
    {
        string Sql = "update ms_inv_mat set ready_flag='Y' where gzdd='" + comPLQ.Value.ToString() + "' and ready_flag='N'";
        dc.ExeSql(Sql);

        setCondition();
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        setCondition();
    }

    protected void ButtonLog_Click(object sender, EventArgs e)
    {
        setCondition2();
    }

}
