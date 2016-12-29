/**************************
 * 功能：维护系统菜单结构 * 
 * 说明：通过拖放的方式来修改层级和顺序，通过ASPx控件提供的Form方式来进行新增、删除、修改 * 
 * 作者：刘征宇 * 
 * 修订：20110803 * 
 * 已知问题：1、数据编辑、新增、删除时候检验可能不完全（待测试）2、节点拖放数据校验可能不完全（待测试）
 * 
 * 2013年12月6日 修改 by 刘征宇
 * 数据绑定和CRUD使用了ORM方式，拖拽和编辑页面初始化还使用老的dataconn方式，以后逐步修改
 * 对应svn上47版的Rmes.DA项目，使用前请更Update Rmes.DA，在任务栏图标区停止VS的WebDev，清理并重新生成Web项目
 * 剩余问题：测试
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
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Factory;

using DevExpress.Web.ASPxCallbackPanel;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxClasses;

public partial class Rmes_Sam_sam1500_sam1500 : Rmes.Web.Base.BasePage
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

    void ASPxTreeList1_ProcessDragNode(object sender, DevExpress.Web.ASPxTreeList.TreeListNodeDragEventArgs e)
    {
        string current_menu_code = e.Node.GetValue("MENU_CODE").ToString();
        string new_parent_code = e.NewParentNode.GetValue("MENU_CODE").ToString();
        bool father_is_leaf = e.NewParentNode.GetValue("LEAF_FLAG").ToString() == "Y";
        if (!father_is_leaf && current_menu_code != new_parent_code)
        {
            string sql = "update CODE_MENU set menu_code_father='" + new_parent_code + "' where menu_code='" + current_menu_code + "' and COMPANY_CODE='" + theCompanyCode + "'";
            if (conn.ExeSql(sql) > 0)
                e.Handled = true;
            e.Cancel = false;
            bindData();
        }
        else if (father_is_leaf && e.NewParentNode.ParentNode.Level > 0 && e.NewParentNode.ParentNode.GetValue("MENU_CODE").ToString().Equals(e.Node.ParentNode.GetValue("MENU_CODE").ToString()))
        {
            string dragedindex = e.Node.GetValue("MENU_INDEX").ToString();
            string dropedindex = e.NewParentNode.GetValue("MENU_INDEX").ToString();
            string sql = "update CODE_MENU set menu_index='" + dropedindex + "' where menu_code='" + current_menu_code + "' and COMPANY_CODE='" + theCompanyCode + "'";
            int n = conn.ExeSql(sql);
            sql = "update CODE_MENU set menu_index='" + dragedindex + "' where menu_code='" + e.NewParentNode.GetValue("MENU_CODE") + "' and COMPANY_CODE='" + theCompanyCode + "'";
            n += conn.ExeSql(sql);
            if (n == 2)
            {
                e.Handled = true;
                e.Cancel = false;
                bindData();
            }
        }
        else
        {
            e.Handled = true;
            e.Cancel = true;
        }

    }

    void ASPxTreeList1_CellEditorInitialize(object sender, DevExpress.Web.ASPxTreeList.TreeListColumnEditorEventArgs e)
    {
        string condition = "", sql = "";
        DataTable dt = null;
        if (e.Column.FieldName == "LEAF_FLAG")
        {
            ASPxCheckBox cbx = e.Editor as ASPxCheckBox;
            cbx.Checked = e.Value == null ? false : e.Value.Equals("Y");
        }
        if (e.Column.FieldName == "MENU_CODE_FATHER")
        {
            condition = " LEAF_FLAG='N' and COMPANY_CODE='" + theCompanyCode + "'";
            if ((!ASPxTreeList1.IsNewNodeEditing))
                condition += " and menu_code <> '" + ASPxTreeList1.FocusedNode.GetValue("MENU_CODE") + "'";
            sql = "select '' as MENU_CODE,'无(用于创建根菜单)' as TEXTFIELD,0 as MENU_LEVEL,0 as MENU_INDEX from dual UNION select MENU_CODE,MENU_NAME ||'('|| MENU_CODE ||')' AS TEXTFIELD,MENU_LEVEL,MENU_INDEX from code_menu where " + condition + " ORDER BY MENU_LEVEL,MENU_INDEX";
            dt = conn.GetTable(sql);
            ASPxComboBox cb = e.Editor as ASPxComboBox;
            cb.ValueField = "MENU_CODE";
            cb.TextField = "TEXTFIELD";
            cb.DataSource = dt;
            cb.DataBind();

            string father_code = "";
            if ((!ASPxTreeList1.IsNewNodeEditing))
            {
                if (ASPxTreeList1.FocusedNode.GetValue("MENU_CODE_FATHER")!=null)//System.DBNull.Value
                {
                    father_code = ASPxTreeList1.FocusedNode.GetValue("MENU_CODE_FATHER").ToString();
                }
            }
            else
            {
                string menucode = "";
                if (ASPxTreeList1.FocusedNode.GetValue("MENU_CODE") == System.DBNull.Value)
                {
                    menucode = ASPxTreeList1.FocusedNode.GetValue("MENU_CODE").ToString();
                }
                string me_father_code = "";
                if (ASPxTreeList1.FocusedNode.GetValue("MENU_CODE_FATHER") != null)
                {
                    me_father_code = ASPxTreeList1.FocusedNode.GetValue("MENU_CODE_FATHER").ToString();
                }
                father_code = ASPxTreeList1.FocusedNode.GetValue("LEAF_FLAG").ToString() == "Y" ? me_father_code: menucode;
            }
                cb.SelectedIndex = cb.Items.IndexOfValue(father_code);
        }

        if (e.Column.FieldName == "PROGRAM_CODE")
        {
            condition = " COMPANY_CODE='" + theCompanyCode + "'";
            sql = "SELECT '' AS PROGRAM_CODE,'无(用于创建菜单组)' AS TEXTFIELD FROM DUAL UNION SELECT PROGRAM_CODE,PROGRAM_NAME || '(' || PROGRAM_CODE || ')' AS TEXTFIELD FROM CODE_PROGRAM WHERE " + condition + " ORDER BY PROGRAM_CODE";
            dt = conn.GetTable(sql);
            ASPxComboBox cb1 = e.Editor as ASPxComboBox;
            cb1.ValueField = "PROGRAM_CODE";
            cb1.TextField = "TEXTFIELD";
            cb1.DataSource = dt;
            cb1.DataBind();
            if (!ASPxTreeList1.IsNewNodeEditing)
                cb1.SelectedIndex = cb1.Items.IndexOfValue(ASPxTreeList1.FocusedNode.GetValue("PROGRAM_CODE"));
            else
                cb1.SelectedIndex = cb1.Items.IndexOfValue("");

        }

        if (e.Column.FieldName == "COMPANY_CODE")
        {
            condition = " COMPANY_CODE='" + theCompanyCode + "'";
            sql = "SELECT COMPANY_CODE,COMPANY_NAME || '(' || COMPANY_CODE || ')' AS TEXTFIELD FROM CODE_COMPANY WHERE " + condition;
            dt = conn.GetTable(sql);
            ASPxComboBox cb2 = e.Editor as ASPxComboBox;
            cb2.ValueField = "COMPANY_CODE";
            cb2.TextField = "TEXTFIELD";
            cb2.DataSource = dt;
            cb2.DataBind();
            cb2.SelectedIndex = cb2.Items.Count > 0 ? 0 : -1;
        }

        if (ASPxTreeList1.IsNewNodeEditing)
        {
            string must_input_form = "MENU_CODE,MENU_NAME,MENU_NAME_EN,MENU_INDEX,";
            string no_input_form = "LEAF_FLAG,MENU_LEVEL,";

            if (must_input_form.Contains(e.Column.FieldName + ","))
            {
                e.Editor.Border.BorderWidth = 2;
            }
            if (no_input_form.Contains(e.Column.FieldName + ","))
            {
                e.Editor.ForeColor = System.Drawing.Color.Gray;
                e.Editor.BackColor = System.Drawing.Color.LightGray;
                e.Editor.Font.Italic = true;
                e.Editor.ReadOnly = true;
            }
            if (e.Column.FieldName.Equals("MENU_INDEX"))
                e.Editor.Value = ASPxTreeList1.FocusedNode.HasChildren ? (ASPxTreeList1.FocusedNode.ChildNodes.Count + 1).ToString() : (ASPxTreeList1.FocusedNode.ParentNode.ChildNodes.Count + 1).ToString();
        }
        else
        {
            string no_input_form = "LEAF_FLAG,MENU_LEVEL,MENU_CODE,";
            if (no_input_form.Contains(e.Column.FieldName + ","))
            {
                e.Editor.ForeColor = System.Drawing.Color.Gray;
                e.Editor.BackColor = System.Drawing.Color.LightGray;
                e.Editor.Font.Italic = true;
                e.Editor.ReadOnly = true;
            }
        }
    }

    void ASPxTreeList1_NodeValidating(object sender, DevExpress.Web.ASPxTreeList.TreeListNodeValidationEventArgs e)
    {
        string result = this.parseMenuData(true,
            e.IsNewNode ? operate.AddNew : operate.Update,
            theCompanyCode,
            O2S(e.NewValues["MENU_CODE"]),
            O2S(e.NewValues["MENU_NAME"]),
            O2S(e.NewValues["MENU_NAME_EN"]),
            O2S(e.NewValues["MENU_CODE_FATHER"]),
            O2S(e.NewValues["MENU_LEVEL"]),
            O2S(e.NewValues["MENU_INDEX"]),
            O2S(e.NewValues["LEAF_FLAG"]),
            O2S(e.NewValues["PROGRAM_CODE"])
            );
        if (result.Length > 0) e.NodeError = result;
    }

    void ASPxTreeList1_NodeDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {

        string result = this.parseMenuData(false,
          operate.Delete,
          theCompanyCode,
          O2S(e.Values["MENU_CODE"]),
          O2S(e.Values["MENU_NAME"]),
          O2S(e.Values["MENU_NAME_EN"]),
          O2S(e.Values["MENU_CODE_FATHER"]),
          O2S(e.Values["MENU_LEVEL"]),
          O2S(e.Values["MENU_INDEX"]),
          O2S(e.Values["LEAF_FLAG"]),
          O2S(e.Values["PROGRAM_CODE"])
          );
        if (result.Length > 0)
        {
            e.Cancel = false;
        }
        else
        {
            ASPxTreeList1.CancelEdit();
            e.Cancel = true;
            bindData();
        }
    }

    void ASPxTreeList1_NodeUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        string result = this.parseMenuData(false,
          operate.Update,
          theCompanyCode,
          O2S(e.OldValues["MENU_CODE"]),
          O2S(e.NewValues["MENU_NAME"]),
          O2S(e.NewValues["MENU_NAME_EN"]),
          O2S(e.NewValues["MENU_CODE_FATHER"]),
          O2S(e.NewValues["MENU_LEVEL"]),
          O2S(e.NewValues["MENU_INDEX"]),
          O2S(e.NewValues["LEAF_FLAG"]),
          O2S(e.NewValues["PROGRAM_CODE"])
          );
        if (result.Length > 0)
        {
            e.Cancel = false;
        }
        else
        {
            ASPxTreeList1.CancelEdit();
            e.Cancel = true;
            bindData();
        }
    }

    void ASPxTreeList1_NodeInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        string result = this.parseMenuData(false,
          operate.AddNew,
          theCompanyCode,
          O2S(e.NewValues["MENU_CODE"]),
          O2S(e.NewValues["MENU_NAME"]),
          O2S(e.NewValues["MENU_NAME_EN"]),
          O2S(e.NewValues["MENU_CODE_FATHER"]),
          O2S(e.NewValues["MENU_LEVEL"]),
          O2S(e.NewValues["MENU_INDEX"]),
          O2S(e.NewValues["LEAF_FLAG"]),
          O2S(e.NewValues["PROGRAM_CODE"])
          );
        if (result.Length > 0)
        {
            e.Cancel = false;
        }
        else
        {
            ASPxTreeList1.CancelEdit();
            e.Cancel = true;
            bindData();
        }
    }

    /// <summary>
    /// 绑定数据到AspxTreeList1
    /// </summary>
    private void bindData()
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        this.TranslateASPxControl(ASPxTreeList1);

        theCompanyCode = theUserManager.getCompanyCode();
        //theContentPrintSql = "SELECT * FROM CODE_MENU WHERE COMPANY_CODE='" + theCompanyCode + "' ORDER BY MENU_INDEX";

        //DataTable dt = conn.GetTable(theContentPrintSql);

        ASPxTreeList1.KeyFieldName = "MENU_CODE";
        ASPxTreeList1.ParentFieldName = "MENU_CODE_FATHER";


        ASPxTreeList1.SettingsEditing.Mode = DevExpress.Web.ASPxTreeList.TreeListEditMode.PopupEditForm;
        ASPxTreeList1.SettingsPopupEditForm.HorizontalAlign = DevExpress.Web.ASPxClasses.PopupHorizontalAlign.Center;

        ASPxTreeList1.SettingsPopupEditForm.VerticalOffset = 10;
        //caoly 20130110 改成离当前行下面10个像素正中间弹出修改或新增框，原来下面的一句是整个页面的正中间，不容易找到
        //ASPxTreeList1.SettingsPopupEditForm.VerticalAlign = DevExpress.Web.ASPxClasses.PopupVerticalAlign.WindowCenter; 

        ASPxTreeList1.SettingsPopupEditForm.AllowResize = true;
        ASPxTreeList1.SettingsPopupEditForm.Modal = true;
        ASPxTreeList1.SettingsPopupEditForm.Width = 500;
        ASPxTreeList1.SettingsPopupEditForm.Caption = "菜单编辑";
        
        //ASPxTreeList1.DataSource = dt;
        ASPxTreeList1.DataSource = MenuItemFactory.GetByCompany(theCompanyCode);
        ASPxTreeList1.DataBind();

        ASPxTreeList1.SettingsEditing.AllowNodeDragDrop = true;
        ASPxTreeList1.ProcessDragNode += new DevExpress.Web.ASPxTreeList.TreeListNodeDragEventHandler(ASPxTreeList1_ProcessDragNode);
        ASPxTreeList1.CellEditorInitialize += new DevExpress.Web.ASPxTreeList.TreeListColumnEditorEventHandler(ASPxTreeList1_CellEditorInitialize);
        ASPxTreeList1.NodeValidating += new DevExpress.Web.ASPxTreeList.TreeListNodeValidationEventHandler(ASPxTreeList1_NodeValidating);
        ASPxTreeList1.NodeInserting += new DevExpress.Web.Data.ASPxDataInsertingEventHandler(ASPxTreeList1_NodeInserting);
        ASPxTreeList1.NodeUpdating += new DevExpress.Web.Data.ASPxDataUpdatingEventHandler(ASPxTreeList1_NodeUpdating);
        ASPxTreeList1.NodeDeleting += new DevExpress.Web.Data.ASPxDataDeletingEventHandler(ASPxTreeList1_NodeDeleting);

    }

    enum operate { AddNew = 0, Update = 1, Delete = 2 };

    private string parseMenuData(bool OnlyCheck, operate operatit, string companycode, string menucode, string menuname, string menunameEN, string menucodefather, string menulevel, string menuindex, string leafflag, string programcode)
    {        //COMPANY_CODE,MENU_CODE, MENU_NAME,MENU_NAME_EN, MENU_CODE_FATHER, MENU_LEVEL, MENU_INDEX, LEAF_FLAG,PROGRAM_CODE

        //进行菜单数据CRUD之前的检查。
        companycode = companycode == null ? "" : companycode.Trim();
        menucode = menucode == null ? "" : menucode.Trim();
        menuname = menuname == null ? "" : menuname.Trim();
        menunameEN = menunameEN == null ? "" : menunameEN.Trim();
        menucodefather = menucodefather == null ? "" : menucodefather.Trim();
        menulevel = menulevel == null ? "" : menulevel.Trim();
        menuindex = menuindex == null ? "" : menuindex.Trim();
        programcode = programcode == null ? "" : programcode.Trim();
        leafflag = programcode.Equals("") ? "N" : "Y";

        if (string.Empty.Equals(companycode)) return "公司代码为空，您的Session可能已经过期，请重新登录";
        if (string.Empty.Equals(menucode)) return "菜单代码为空，请重新输入";
        if (string.Empty.Equals(menuname)) return "菜单标题为空，请重新输入";
        if (string.Empty.Equals(menunameEN)) return "菜单英文标题为空，请重新输入";

        if (string.Empty.Equals(menuindex)) return "菜单索引为空，请重新输入";
        //if (string.Empty.Equals(menucodefather)) return "上级菜单为空，请重新输入";
        //if (string.Empty.Equals(menulevel)) return "菜单层级为空，请重新输入";
        //if (string.Empty.Equals(programcode)) return "为空，请重新输入";

        string sql = "";
        if (operatit == operate.AddNew)
        {
            sql = "select count(*) from CODE_MENU where MENU_CODE='" + menucode + "' and COMPANY_CODE='" + companycode + "'";
            DataTable dt = conn.GetTable(sql);
            if (Convert.ToInt32(dt.Rows[0][0]) > 0)
                return "已有相同的菜单编码\"" + menucode + "\"，请重新输入。";
        }

        //CRUD
        if (!OnlyCheck)//如果不仅仅是检查数据，则进行CRUD操作，否则只执行上面的语句，进行数据检查。
        {
            if (string.Empty.Equals(menulevel)) menulevel = ASPxTreeList1.FocusedNode.HasChildren ? (ASPxTreeList1.FocusedNode.Level + 1).ToString() : ASPxTreeList1.FocusedNode.Level.ToString();
            if (string.Empty.Equals(menuindex)) menuindex = "99";

            MenuItemEntity menu1 = new MenuItemEntity()
            {
                COMPANY_CODE = companycode,
                MENU_CODE = menucode,
                MENU_CODE_FATHER = menucodefather,
                LEAF_FLAG = leafflag,
                MENU_INDEX = Convert.ToInt32(menuindex),
                MENU_LEVEL = Convert.ToInt32(menulevel),
                MENU_NAME = menuname,
                MENU_NAME_EN = menunameEN,
                PROGRAM_CODE = programcode
            };
            int ret = 0;
            object obj = null;
            string strReturn = "未知操作！";
            switch (operatit)
            {
                case operate.AddNew:
                    obj = MenuItemFactory.AddNew(menu1);
                    if (obj != null) strReturn = "";
                    else strReturn = "添加失败！";
                    //sql = "INSERT INTO CODE_MENU (COMPANY_CODE, MENU_CODE, MENU_NAME, MENU_NAME_EN, MENU_CODE_FATHER, MENU_LEVEL,MENU_INDEX, LEAF_FLAG, PROGRAM_CODE) VALUES (:COMPANY_CODE, :MENU_CODE, :MENU_NAME, :MENU_NAME_EN, :MENU_CODE_FATHER, :MENU_LEVEL, :MENU_INDEX, :LEAF_FLAG, :PROGRAM_CODE)";
                    break;
                case operate.Update:
                    ret = MenuItemFactory.Update(menu1);
                    if (ret == 1) strReturn = "";
                    else strReturn = "修改失败！";
                    //sql = "UPDATE CODE_MENU SET MENU_NAME = :MENU_NAME, MENU_NAME_EN = :MENU_NAME_EN, MENU_CODE_FATHER = :MENU_CODE_FATHER, MENU_LEVEL = :MENU_LEVEL, MENU_INDEX = :MENU_INDEX, LEAF_FLAG = :LEAF_FLAG, PROGRAM_CODE = :PROGRAM_CODE WHERE COMPANY_CODE = :COMPANY_CODE AND MENU_CODE = :MENU_CODE";
                    break;
                case operate.Delete:
                    ret = MenuItemFactory.Remove(menu1);
                    if (ret == 1) strReturn = "";
                    else strReturn = "移除失败！";
                    //sql = "DELETE FROM CODE_MENU WHERE COMPANY_CODE = :COMPANY_CODE AND MENU_CODE = :MENU_CODE";
                    break;
                default: sql = ""; strReturn = "无效的操作"; break;
            }
            return strReturn;
            ///以前用的是下面的sql方式，现在改为上面的Entity方式。

            //if (sql == "") return "错误的SQL语句。";
            //if (conn.theComd.Connection.State == ConnectionState.Closed) conn.theComd.Connection.Open();

            //conn.theComd.CommandText = sql;
            //conn.theComd.Parameters.Clear();

            //conn.theComd.Parameters.Add("COMPANY_CODE", Oracle.DataAccess.Client.OracleDbType.Varchar2).Value = companycode;
            //conn.theComd.Parameters.Add("MENU_CODE", Oracle.DataAccess.Client.OracleDbType.Varchar2).Value = menucode;

            //if (operatit != operate.Delete)
            //{
            //    conn.theComd.Parameters.Add("MENU_NAME", Oracle.DataAccess.Client.OracleDbType.Varchar2).Value = menuname;
            //    conn.theComd.Parameters.Add("MENU_NAME_EN", Oracle.DataAccess.Client.OracleDbType.Varchar2).Value = menunameEN;
            //    conn.theComd.Parameters.Add("MENU_CODE_FATHER", Oracle.DataAccess.Client.OracleDbType.Varchar2).Value = menucodefather;
            //    conn.theComd.Parameters.Add("MENU_LEVEL", Oracle.DataAccess.Client.OracleDbType.Int32).Value = menulevel;
            //    conn.theComd.Parameters.Add("MENU_INDEX", Oracle.DataAccess.Client.OracleDbType.Int32).Value = menuindex;
            //    conn.theComd.Parameters.Add("LEAF_FLAG", Oracle.DataAccess.Client.OracleDbType.Varchar2).Value = leafflag;
            //    conn.theComd.Parameters.Add("PROGRAM_CODE", Oracle.DataAccess.Client.OracleDbType.Varchar2).Value = programcode;
            //}
            //if (conn.theComd.ExecuteNonQuery() == 1)
            //{
            //    conn.theComd.Parameters.Clear();
            //    conn.theComd.Connection.Close();
            //    return "";
            //}
            //else
            //{
            //    conn.theComd.Parameters.Clear();
            //    conn.theComd.Connection.Close();
            //    return "执行SQL出错!!";
            //}
        }
        return "";
    }

    /// <summary>
    /// 将object转换为string类型，如果object 是 null 则返回 空字符串，而不是返回null对象
    /// </summary>
    /// <param name="o">object</param>
    /// <returns>一个字符串，可能是""，可能是Convert.ToString(o)</returns>
    private string O2S(object o)
    {
        if (o == null) return "";
        return Convert.ToString(o);
    }

}
