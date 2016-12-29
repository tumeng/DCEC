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
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Factory;
using Rmes.DA.Dal;

using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxClasses;
using Rmes.Pub.Data;
using System.Collections.Generic;
using PetaPoco;


/**
 * 功能概述：站点定义
 * 作者：曹路圆
 * 创建时间：2011-07-28
 **/



public partial class Rmes_epd1300 : Rmes.Web.Base.BasePage
{
    private dataConn dc = new dataConn();
    private PubCs pc = new PubCs();
    public string theCompanyCode;
    public Database db = DB.GetInstance();

    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();

        //this.TranslateASPxControl(ASPxGridView1);


        if (!IsPostBack)
        {

        }
        setCondition();
    }


    private void setCondition()
    {
        //初始化GRIDVIEW
        List<StationEntity> all = StationFactory.GetAll();

        ASPxGridView1.DataSource = all;
        ASPxGridView1.DataBind();
    }

    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        //判断当前记录是否可以删除
        string strDelCode = e.Values["STATION_CODE"].ToString();
        string strTableName = "CODE_STATION";

        dataConn theDataConn = new dataConn("select func_check_delete_data('" + strTableName + "','" + theCompanyCode + "','MES','MES','MES','" + strDelCode + "') from dual");

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
            string Sql = "delete from CODE_STATION WHERE  COMPANY_CODE = '" + theCompanyCode + "' and STATION_CODE = '" + strDelCode + "'";
            dc.ExeSql(Sql);
        }

        setCondition();
        e.Cancel = true;
    }


    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxTextBox uCode = ASPxGridView1.FindEditFormTemplateControl("txtStationCode") as ASPxTextBox;
        ASPxTextBox uName = ASPxGridView1.FindEditFormTemplateControl("txtStationName") as ASPxTextBox;
        ASPxComboBox uTcode = ASPxGridView1.FindEditFormTemplateControl("dropStaionType") as ASPxComboBox;
        ASPxComboBox uPcode = ASPxGridView1.FindEditFormTemplateControl("dropPlineCode") as ASPxComboBox;
        ASPxTextBox uScode = ASPxGridView1.FindEditFormTemplateControl("dropStaionArea") as ASPxTextBox;

        StationEntity s = new StationEntity()
        {
            COMPANY_CODE=theCompanyCode,
            PLINE_CODE = uPcode.Value.ToString(),
            STATION_CODE = uCode.Text.Trim(),
            STATION_NAME = uName.Text.Trim(),
            STATION_TYPE_CODE = uTcode.Value as string,
            STATION_AREA_CODE = uScode.Value as string,
            WORKUNIT_CODE = e.NewValues["WORKUNIT_CODE"] as string
        };
        
        s.RMES_ID = s.STATION_CODE;
        StationFactory.Insert(s);

        //string Sql = "INSERT INTO CODE_STATION (COMPANY_CODE,STATION_CODE,STATION_NAME,STATION_TYPE_CODE,PLINE_CODE,STATION_AREA_CODE) "
        //     + "VALUES('" + theCompanyCode + "','" + uCode.Text.Trim() + "','" + uName.Text.Trim() + "','" + uTcode.Value.ToString() + "','" + uPcode.Value.ToString() + "','" + uScode.Value.ToString() + "')";
        //dc.ExeSql(Sql);

        e.Cancel = true;
        ASPxGridView1.CancelEdit();
        setCondition();

    }


    protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {

        ASPxTextBox uCode = ASPxGridView1.FindEditFormTemplateControl("txtStationCode") as ASPxTextBox;
        ASPxTextBox uName = ASPxGridView1.FindEditFormTemplateControl("txtStationName") as ASPxTextBox;
        ASPxComboBox uTcode = ASPxGridView1.FindEditFormTemplateControl("dropStaionType") as ASPxComboBox;
        ASPxComboBox uPcode = ASPxGridView1.FindEditFormTemplateControl("dropPlineCode") as ASPxComboBox;
        ASPxTextBox uScode = ASPxGridView1.FindEditFormTemplateControl("dropStaionArea") as ASPxTextBox;
        string id = e.NewValues["RMES_ID"] as string;
        StationEntity s = new StationEntity()
        {
            RMES_ID=id,
            COMPANY_CODE = theCompanyCode,
            PLINE_CODE = uPcode.Value.ToString(),
            STATION_CODE = uCode.Text.Trim(),
            STATION_NAME = uName.Text.Trim(),
            STATION_TYPE_CODE = uTcode.Value as string,
            STATION_AREA_CODE = uScode.Value as string,
            WORKUNIT_CODE = e.NewValues["WORKUNIT_CODE"] as string
        };

        db.Update("CODE_STATION", "RMES_ID", s);
        

        e.Cancel = true;
        ASPxGridView1.CancelEdit();
        setCondition();

    }
    protected void ASPxGridView1_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditFormEventArgs e)
    {

        ASPxComboBox uTcode = ASPxGridView1.FindEditFormTemplateControl("dropStaionType") as ASPxComboBox;
        ASPxComboBox uPcode = ASPxGridView1.FindEditFormTemplateControl("dropPlineCode") as ASPxComboBox;
        //ASPxComboBox uScode = ASPxGridView1.FindEditFormTemplateControl("dropStaionArea") as ASPxComboBox;


        string Sql4 = "SELECT STATION_TYPE_CODE,STATION_TYPE_CODE||' '||STATION_TYPE_NAME AS SHOWTEXT FROM CODE_STATION_TYPE";
        DataTable dt4 = dc.GetTable(Sql4);
        
        uTcode.DataSource = dt4;
        uTcode.TextField = dt4.Columns[1].ToString();
        uTcode.ValueField = dt4.Columns[0].ToString();


        string Sql2 = "select RMES_ID,pline_code||' '||pline_name as showtext from CODE_PRODUCT_LINE";
        DataTable dt2 = dc.GetTable(Sql2);

        uPcode.DataSource = dt2;
        uPcode.TextField = dt2.Columns[1].ToString();
        uPcode.ValueField = dt2.Columns[0].ToString();


        //string  Sql3 = "SELECT STATION_AREA_CODE,STATION_AREA_CODE||' '||STATION_AREA_NAME AS SHOWTEXT FROM CODE_STATION_AREA";
        //DataTable dt3 = dc.GetTable(Sql3);

        //uScode.DataSource = dt3;
        //uScode.TextField = dt3.Columns[1].ToString();
        //uScode.ValueField = dt3.Columns[0].ToString();



        if (!ASPxGridView1.IsNewRowEditing && ASPxGridView1.IsEditing)
        {
            ///主键不可以修改
            (ASPxGridView1.FindEditFormTemplateControl("txtStationCode") as ASPxTextBox).Enabled = false;
        }
        else
        {
            //uTcode.SelectedIndex = 0;
            //uPcode.SelectedIndex = 0;
            //uScode.SelectedIndex = 0;
        }
    }

    protected void ASPxGridView1_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        //判断超长
        if (e.NewValues["STATION_CODE"].ToString().Length > 30)
        {
            e.RowError = "站点代码字节长度不能超过30！";
        }
        if (e.NewValues["STATION_NAME"].ToString().Length > 30)
        {
            e.RowError = "站点名称字节长度不能超过30！";
        }

        //if (e.NewValues["STATION_TYPE_CODE"].ToString().Length > 30)
        //{
        //    e.RowError = "站点类型字节长度不能超过30！";
        //}

        if (e.NewValues["PLINE_CODE"].ToString().Length > 30)
        {
            e.RowError = "生产线代码字节长度不能超过30！";
        }
        //if (e.NewValues["STATION_AREA_CODE"].ToString().Length > 30)
        //{
        //    e.RowError = "站点区域代码字节长度不能超过30！";
        //}


        //判断为空
        if (e.NewValues["STATION_CODE"].ToString() == "" || e.NewValues["STATION_CODE"].ToString() == null)
        {
            e.RowError = "站点代码不能为空！";
        }
        if (e.NewValues["STATION_NAME"].ToString() == "" || e.NewValues["STATION_NAME"].ToString() == null)
        {
            e.RowError = "站点名称不能为空！";
        }
        //if (e.NewValues["STATION_TYPE_CODE"].ToString() == "" || e.NewValues["STATION_NAME"].ToString() == null)
        //{
        //    e.RowError = "站点类型不能为空！";
        //}
        if (e.NewValues["PLINE_CODE"].ToString() == "" || e.NewValues["STATION_NAME"].ToString() == null)
        {
            e.RowError = "生产线代码不能为空！";
        }
        //if (e.NewValues["STATION_AREA_CODE"].ToString() == "" || e.NewValues["STATION_NAME"].ToString() == null)
        //{
        //    e.RowError = "站点区域代码不能为空！";
        //}



        //判断是否重复
        if (ASPxGridView1.IsNewRowEditing)
        {
            string chSql = "SELECT STATION_CODE, STATION_NAME  FROM CODE_STATION"
                + " WHERE COMPANY_CODE = '" + theCompanyCode + "' AND STATION_CODE='" + e.NewValues["STATION_CODE"].ToString() + "'";
            DataTable dt = dc.GetTable(chSql);
            if (dt.Rows.Count > 0)
            {
                e.RowError = "已存在相同的站点代码！";
            }
        }
    }

    protected int GetMaxSeq(List<StationEntity> temp)
    {
        List<int> seq = new List<int>();
        foreach (StationEntity s in temp)
        {
            seq.Add(int.Parse(s.RMES_ID.Substring(3, 5)));
        }
        int i = 0;
        for (int j = 1; j < seq.Count; j++)
        {
            int t;
            if (seq[i] < seq[j])
            {
                t = seq[i];
                seq[i] = seq[j];
                seq[j] = t;
            }
        }
        return seq[0];
    }

}
