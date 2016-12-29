using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using DevExpress.Web.ASPxGridView;

using PetaPoco;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Factory;
using Rmes.DA.Procedures;
using Rmes.Pub.Data;
using Rmes.Web.Base;


/**
 * 功能概述：关键操作记录日志查询
 * 作    者：caoly
 * 创建时间：2014-04-19
 */

public partial class Rmes_sam3300 : Rmes.Web.Base.BasePage
{
    public Database db = DB.GetInstance();
    private dataConn dc = new dataConn();

    protected void Page_Load(object sender, EventArgs e)
    {
        setCondition();
    }
    
    private void setCondition()
    {
        List<KeyWorklogEntity> log = db.Fetch<KeyWorklogEntity>("where DELETE_FLAG='N' order by CREATE_TIME desc");

        GridViewDataComboBoxColumn user = ASPxGridView1.Columns["USER_ID"] as GridViewDataComboBoxColumn;
        user.PropertiesComboBox.DataSource = UserFactory.GetAll();
        user.PropertiesComboBox.ValueField = "USER_ID";
        user.PropertiesComboBox.TextField = "USER_NAME";

        ASPxGridView1.DataSource = log;
        ASPxGridView1.DataBind();
    }

    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        ////删除
        string id = e.Keys["RMES_ID"].ToString();

        //删除只是把enable_flag设置为D，不再显示，防止用户赖账，不承认有过该规则
        string sql = "update DATA_XK_KEY_WORKLOG set DELETE_FLAG='D' where rmes_id='" + id + "'";

        dc.ExeSql(sql);

        setCondition();
        e.Cancel = true;
    }
}
