using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using DevExpress.Web;

namespace Rmes.Web.Base
{
    /// <summary>
    ///BasePage 的摘要说明
    /// </summary>
    public abstract class BasePage : System.Web.UI.Page
    {
        public BasePage()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public override string StyleSheetTheme
        {
            get
            {
                if (Request.Cookies["DXCurrentThemeASPxperience"] != null)
                    return Request.Cookies["DXCurrentThemeASPxperience"].Value;
                else
                    //return base.StyleSheetTheme;
                    return "Theme1";
            }
            set
            {
                base.StyleSheetTheme = value;
            }
        }

        protected override void OnPreInit(EventArgs e)
        {
            try
            {
                Rmes.Pub.Data.userManager user = (Rmes.Pub.Data.userManager)Session["theUserManager"];
                base.OnPreInit(e);
            }
            catch(Exception ex)
            {
                Rmes.Public.ErrorHandle.EH.LASTMSG = ex.Message;
                Response.Redirect("~/Exception/DefaultException.aspx");
            }
        }

        public void TranslateASPxControl(object c)
        {

            //Type t = c.GetType();
            //System.Reflection.PropertyInfo p = t.GetProperty("SettingsText");
            //object obj = p.GetValue(c, null);

            if (c.GetType() == (new DevExpress.Web.ASPxTreeList.ASPxTreeList()).GetType())
            {
                DevExpress.Web.ASPxTreeList.ASPxTreeList cc = c as DevExpress.Web.ASPxTreeList.ASPxTreeList;
                cc.SettingsText.CommandCancel = "取消";
                cc.SettingsText.CommandDelete = "删除";
                cc.SettingsText.CommandEdit = "编辑";
                cc.SettingsText.CommandNew = "新增";
                cc.SettingsText.CommandUpdate = "更新";
                cc.SettingsText.ConfirmDelete = "确认删除？";
                cc.SettingsText.LoadingPanelText = "正在载入...";
                cc.SettingsText.RecursiveDeleteError = "该节点还有子节点，无法直接删除！";
                //DevExpress.Web.ASPxTreeList.TreeListCommandColumn tcc = cc.Columns["EditColumn"] as DevExpress.Web.ASPxTreeList.TreeListCommandColumn;
            }
            if (c.GetType() == (new DevExpress.Web.ASPxGridView.ASPxGridView()).GetType())
            {
                DevExpress.Web.ASPxGridView.ASPxGridView gv = c as DevExpress.Web.ASPxGridView.ASPxGridView;
                gv.SettingsText.CommandCancel = "取消";
                gv.SettingsText.CommandDelete = "删除";
                gv.SettingsText.CommandEdit = "编辑";
                gv.SettingsText.CommandNew = "新增";
                gv.SettingsText.CommandSelect = "选取";
                gv.SettingsText.CommandUpdate = "更新";
                gv.SettingsText.ConfirmDelete = "确认删除？";
                gv.SettingsEditing.Mode = DevExpress.Web.ASPxGridView.GridViewEditingMode.PopupEditForm;
                gv.SettingsText.EmptyDataRow = "没有发现数据";
                //gv.Styles.GroupPanel.HorizontalAlign = HorizontalAlign.Right;
            }
        }


    }
}