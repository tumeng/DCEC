/**************************
 * 功能：维护系统菜单结构
 * 
 * 说明：通过拖放的方式来修改层级和顺序，通过ASPx控件提供的Form方式来进行新增、删除、修改
 * 
 * 作者：刘征宇
 * 
 * 修订：20110803
 * 
 * 已知问题：1、数据编辑、新增、删除时候检验可能不完全（待测试）2、节点拖放数据校验可能不完全（待测试）
 * 
 **************************/

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Rmes.Pub.Data;
using Rmes.Pub.Function;
using DevExpress.Web.ASPxCallbackPanel;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxClasses;

public partial class Rmes_Inv9200 : Rmes.Web.Base.BasePage
{
    public String theSql;
    public String theQueryCondition;
    public String theCompanyCode;
    public PubJs thePubJs = new PubJs();
    public string theContentPrintSql = "";

    dataConn conn = new dataConn();

    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();

        bindData();
    }

    void ASPxTreeList1_CellEditorInitialize(object sender, DevExpress.Web.ASPxTreeList.TreeListColumnEditorEventArgs e)
    {
        //ASPxComboBox dropTeamCode = ASPxTreeList1.FindEditFormTemplateControl("dropPlineCode") as ASPxComboBox;

        //if (e.Column.FieldName == "TEAM_CODE")
        //{
        //    string sql = "";
        //    DataTable dt = null;
        //    sql = "SELECT TEAM_CODE, TEAM_NAME FROM CODE_TEAM ORDER BY TEAM_NAME";
        //    dt = conn.GetTable(sql);

        //    ASPxComboBox dropTeamCode = e.Editor as ASPxComboBox;
        //    dropTeamCode.ValueField = "TEAM_CODE";
        //    dropTeamCode.TextField = "TEAM_NAME";
        //    dropTeamCode.DataSource = dt;
        //    dropTeamCode.DataBind();
        //    if (!ASPxTreeList1.IsNewNodeEditing)
        //        dropTeamCode.SelectedIndex = dropTeamCode.Items.IndexOfValue(ASPxTreeList1.FocusedNode.GetValue("TEAM_CODE"));
        //    else
        //        dropTeamCode.SelectedIndex = dropTeamCode.Items.IndexOfValue("");
        //}

        //string no_input_form = "项目名称,项目代号,装入序号,工艺序号,合同号,工作号,批次,合并数量,实际数量,组件代号,工艺路线,送入单位,";
        //if (no_input_form.Contains(e.Column.FieldName + ","))
        //{
        //    e.Editor.ForeColor = System.Drawing.Color.Gray;
        //    e.Editor.BackColor = System.Drawing.Color.LightGray;
        //    e.Editor.Font.Italic = true;
        //    e.Editor.ReadOnly = true;
        //    e.Editor.Enabled = false;
        //}


        //string condition = "", sql = "";
        //DataTable dt = null;
        //if (e.Column.FieldName == "LEAF_FLAG")
        //{
        //    ASPxCheckBox cbx = e.Editor as ASPxCheckBox;
        //    cbx.Checked = e.Value == null ? false : e.Value.Equals("Y");
        //}
        //if (e.Column.FieldName == "MENU_CODE_FATHER")
        //{
        //    condition = " LEAF_FLAG='N' and COMPANY_CODE='" + theCompanyCode + "'";
        //    if ((!ASPxTreeList1.IsNewNodeEditing))
        //        condition += " and menu_code <> '" + ASPxTreeList1.FocusedNode.GetValue("MENU_CODE") + "'";
        //    sql = "select '' as MENU_CODE,'无(用于创建根菜单)' as TEXTFIELD,0 as MENU_LEVEL,0 as MENU_INDEX from dual UNION select MENU_CODE,MENU_NAME ||'('|| MENU_CODE ||')' AS TEXTFIELD,MENU_LEVEL,MENU_INDEX from code_menu where " + condition + " ORDER BY MENU_LEVEL,MENU_INDEX";
        //    dt = conn.GetTable(sql);
        //    ASPxComboBox cb = e.Editor as ASPxComboBox;
        //    cb.ValueField = "MENU_CODE";
        //    cb.TextField = "TEXTFIELD";
        //    cb.DataSource = dt;
        //    cb.DataBind();

        //    string father_code = "";
        //    if ((!ASPxTreeList1.IsNewNodeEditing))
        //        father_code = ASPxTreeList1.FocusedNode.GetValue("MENU_CODE_FATHER").ToString();
        //    else
        //        father_code = ASPxTreeList1.FocusedNode.GetValue("LEAF_FLAG").ToString() == "Y" ? ASPxTreeList1.FocusedNode.GetValue("MENU_CODE_FATHER").ToString() : ASPxTreeList1.FocusedNode.GetValue("MENU_CODE").ToString();
        //    cb.SelectedIndex = cb.Items.IndexOfValue(father_code);
        //}

        //if (e.Column.FieldName == "PROGRAM_CODE")
        //{
        //    condition = " COMPANY_CODE='" + theCompanyCode + "'";
        //    sql = "SELECT '' AS PROGRAM_CODE,'无(用于创建菜单组)' AS TEXTFIELD FROM DUAL UNION SELECT PROGRAM_CODE,PROGRAM_NAME || '(' || PROGRAM_CODE || ')' AS TEXTFIELD FROM CODE_PROGRAM WHERE " + condition + " ORDER BY PROGRAM_CODE";
        //    dt = conn.GetTable(sql);
        //    ASPxComboBox cb1 = e.Editor as ASPxComboBox;
        //    cb1.ValueField = "PROGRAM_CODE";
        //    cb1.TextField = "TEXTFIELD";
        //    cb1.DataSource = dt;
        //    cb1.DataBind();
        //    if (!ASPxTreeList1.IsNewNodeEditing)
        //        cb1.SelectedIndex = cb1.Items.IndexOfValue(ASPxTreeList1.FocusedNode.GetValue("PROGRAM_CODE"));
        //    else
        //        cb1.SelectedIndex = cb1.Items.IndexOfValue("");

        //}

        //if (e.Column.FieldName == "COMPANY_CODE")
        //{
        //    condition = " COMPANY_CODE='" + theCompanyCode + "'";
        //    sql = "SELECT COMPANY_CODE,COMPANY_NAME || '(' || COMPANY_CODE || ')' AS TEXTFIELD FROM CODE_COMPANY WHERE " + condition;
        //    dt = conn.GetTable(sql);
        //    ASPxComboBox cb2 = e.Editor as ASPxComboBox;
        //    cb2.ValueField = "COMPANY_CODE";
        //    cb2.TextField = "TEXTFIELD";
        //    cb2.DataSource = dt;
        //    cb2.DataBind();
        //    cb2.SelectedIndex = cb2.Items.Count > 0 ? 0 : -1;

        //}

        //if (ASPxTreeList1.IsNewNodeEditing)
        //{
        //    string must_input_form = "MENU_CODE,MENU_NAME,MENU_NAME_EN,MENU_INDEX,";
        //    string no_input_form = "LEAF_FLAG,MENU_LEVEL,";

        //    if (must_input_form.Contains(e.Column.FieldName + ","))
        //    {
        //        e.Editor.Border.BorderWidth = 2;
        //    }
        //    if (no_input_form.Contains(e.Column.FieldName + ","))
        //    {
        //        e.Editor.ForeColor = System.Drawing.Color.Gray;
        //        e.Editor.BackColor = System.Drawing.Color.LightGray;
        //        e.Editor.Font.Italic = true;
        //        e.Editor.ReadOnly = true;
        //    }
        //    if (e.Column.FieldName.Equals("MENU_INDEX"))
        //        e.Editor.Value = ASPxTreeList1.FocusedNode.HasChildren ? (ASPxTreeList1.FocusedNode.ChildNodes.Count + 1).ToString() : (ASPxTreeList1.FocusedNode.ParentNode.ChildNodes.Count + 1).ToString();
        //}
        //else
        //{
        //    string no_input_form = "LEAF_FLAG,MENU_LEVEL,MENU_CODE,";
        //    if (no_input_form.Contains(e.Column.FieldName + ","))
        //    {
        //        e.Editor.ForeColor = System.Drawing.Color.Gray;
        //        e.Editor.BackColor = System.Drawing.Color.LightGray;
        //        e.Editor.Font.Italic = true;
        //        e.Editor.ReadOnly = true;
        //    }
        //}
    }


    void ASPxTreeList1_NodeUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        //string project = e.OldValues["项目代号"].ToString();
        //string batch = e.OldValues["批次"].ToString();
        //string plan = e.OldValues["工作号"].ToString();
        //int num = int.Parse(e.OldValues["工艺序号"].ToString());
        ////string oldTeam = e.OldValues["TEAM_CODE"].ToString();
        //string team = e.NewValues["TEAM_CODE"].ToString();
        
        //string sql = "";
        //if (e.OldValues["TEAM_CODE"] == null)
        //{
        //    sql = "INSERT INTO REL_PROCESS_TEAM(RMES_ID, PROJECT_CODE, BATCH_WORK_CODE, PLAN_CODE, PROCESS_NUM, TEAM_CODE)"
        //        + " VALUES(SEQ_RMES_ID.NEXTVAL,'" + project + "','" + batch + "','" + plan + "'," + num + ",'" + team + "')";
        //}
        //else
        //{
        //    sql = "UPDATE REL_PROCESS_TEAM SET TEAM_CODE ='" + team + "' WHERE PROCESS_NUM = " + num;
        //}

        //dataConn dc = new dataConn();
        //dc.ExeSql(sql);

        //ASPxTreeList1.CancelEdit();
        //e.Cancel = true;
        //bindData();

    }


    private void bindData()
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        this.TranslateASPxControl(ASPxTreeList1);

        theCompanyCode = theUserManager.getCompanyCode();
        theContentPrintSql = "select * from code_work_unit";

        DataTable dt = conn.GetTable(theContentPrintSql);
        ASPxTreeList1.KeyFieldName = "WORKUNIT_CODE";
        ASPxTreeList1.ParentFieldName = "WORKUNIT_PARENTCODE";
        ASPxTreeList1.SettingsEditing.Mode = DevExpress.Web.ASPxTreeList.TreeListEditMode.PopupEditForm;
        ASPxTreeList1.SettingsPopupEditForm.HorizontalAlign = DevExpress.Web.ASPxClasses.PopupHorizontalAlign.Center;
        ASPxTreeList1.SettingsPopupEditForm.VerticalAlign = DevExpress.Web.ASPxClasses.PopupVerticalAlign.WindowCenter;
        ASPxTreeList1.SettingsPopupEditForm.AllowResize = true;
        ASPxTreeList1.SettingsPopupEditForm.Modal = true;
        ASPxTreeList1.SettingsPopupEditForm.Width = 500;
        ASPxTreeList1.SettingsPopupEditForm.Caption = "工作单元编辑";

        ASPxTreeList1.DataSource = dt;
        ASPxTreeList1.DataBind();

        ASPxTreeList1.CollapseAll();
        ASPxTreeList1.ExpandToLevel(2);
        //ASPxTreeList1.SettingsEditing.AllowNodeDragDrop = true;
        //ASPxTreeList1.ProcessDragNode += new DevExpress.Web.ASPxTreeList.TreeListNodeDragEventHandler(ASPxTreeList1_ProcessDragNode);
        ASPxTreeList1.CellEditorInitialize += new DevExpress.Web.ASPxTreeList.TreeListColumnEditorEventHandler(ASPxTreeList1_CellEditorInitialize);
        //ASPxTreeList1.NodeValidating += new DevExpress.Web.ASPxTreeList.TreeListNodeValidationEventHandler(ASPxTreeList1_NodeValidating);
        //ASPxTreeList1.NodeInserting += new DevExpress.Web.Data.ASPxDataInsertingEventHandler(ASPxTreeList1_NodeInserting);
        ASPxTreeList1.NodeUpdating += new DevExpress.Web.Data.ASPxDataUpdatingEventHandler(ASPxTreeList1_NodeUpdating);
        //ASPxTreeList1.NodeDeleting += new DevExpress.Web.Data.ASPxDataDeletingEventHandler(ASPxTreeList1_NodeDeleting);

        //ASPxTreeList1.HtmlRowPrepared += new DevExpress.Web.ASPxTreeList.TreeListHtmlRowEventHandler(ASPxTreeList1_HtmlRowPrepared);
        //ASPxTreeList1.HtmlDataCellPrepared += new DevExpress.Web.ASPxTreeList.TreeListHtmlDataCellEventHandler(ASPxTreeList1_HtmlDataCellPrepared);
        //ASPxTreeList1.ParseValue += new DevExpress.Web.Data.ASPxParseValueEventHandler(ASPxTreeList1_ParseValue);
    }


}
