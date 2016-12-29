using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxClasses;

//以下引用，如果采用实体类，请全部复制到新页面
using PetaPoco;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;
using Rmes.Pub.Data;
using Rmes.Web.Base;
using System.Collections.Generic;

/**
 * 功能概述：产品检验结果
 * 作    者：涂猛/陈俊
 * 创建时间：2013-12-03
 * 修改时间：2013-12-04
 */


public partial class Rmes_qms2100 : BasePage
{
    

    
    
    PubCs thePc = new PubCs();

    public Database db = DB.GetInstance();
    public string theCompanyCode;
    private dataConn dc = new dataConn();
    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();
        setCondition();
        
    }
    
    private void setCondition()
    {
        //绑定表数据
        //GridViewDataComboBoxColumn dgvComboBoxR = ASPxGridView1.Columns["CurrentResult"] as GridViewDataComboBoxColumn;
        //DataTable dt = new DataTable();
        //dt.Columns.Add("DisplayField", typeof(string));
        //dt.Columns.Add("ValueField", typeof(int));
        //dt.Rows.Add("合格", 1);
        //dt.Rows.Add("不合格", 0);
        //dt.Rows.Add("未知状态", -1);
        //dgvComboBoxR.PropertiesComboBox.DataSource = dt;
        //dgvComboBoxR.PropertiesComboBox.TextField = "DisplayField";
        //dgvComboBoxR.PropertiesComboBox.ValueField = "ValueField";

        //GridViewDataComboBoxColumn dgvComboBoxT = ASPxGridView1.Columns["UnitType"] as GridViewDataComboBoxColumn;
        //DataTable dt1 = new DataTable();
        //dt1.Columns.Add("DisplayField", typeof(string));
        //dt1.Columns.Add("ValueField", typeof(string));
        //dt1.Rows.Add( "数字","N");
        //dt1.Rows.Add("字符","T" );
        //dt1.Rows.Add("判断是否","B" );
        //dt1.Rows.Add("文件路径","F");
        //dgvComboBoxT.PropertiesComboBox.DataSource = dt1;
        //dgvComboBoxT.PropertiesComboBox.TextField = "DisplayField";
        //dgvComboBoxT.PropertiesComboBox.ValueField = "ValueField";



        GridViewDataComboBoxColumn dgvComboBoxU = (ASPxGridView1.Columns["USER_ID"] as GridViewDataComboBoxColumn);
        {
            dgvComboBoxU.PropertiesComboBox.DataSource = UserFactory.GetAll();
            dgvComboBoxU.PropertiesComboBox.ValueField = "USER_ID";
            dgvComboBoxU.PropertiesComboBox.TextField = "USER_NAME";
        }
        
        //string sql = "SELECT  A.RMES_ID,A.BatchNo,A.ProcessCode,A.ItemCode,A.ItemName, A.ItemDescription,A.MinValue,A.MaxValue,A.URL,A.CurrentValue,A.UnitName,A.Ordering,A.TIMESTAMP1,A.WORK_TIME,A.USER_ID,B.PLAN_CODE, "
        //    + "decode(A.CurrentResult,'1','合格','0','不合格'，'-1','未知状态') CURRENTRESULT, "
        //    + "decode(A.UNITTYPE,'N','数值','T','文字','B','判断','F','文件'） UNITTYPE "
        //    + "FROM DATA_SN_QUALITY A"
        //    + " LEFT JOIN DATA_PLAN_SN  B ON A.BatchNo = B.SN ";
        //DataTable dt3 = dc.GetTable(sql);
        //GridViewDataComboBoxColumn dgvComboBoxU = ASPxGridView1.Columns["USER_ID"] as GridViewDataComboBoxColumn;
        //DataTable dt2 = new DataTable();
        //dt2.Columns.Add("DisplayField", typeof(string));
        //dt2.Columns.Add("ValueField", typeof(string));
        //List<QualitySnItem> quality = QualityFactory.GetAll();
        //for (int i = 0; i < quality.Count; i++)
        //{
        //    UserEntity user = UserFactory.GetByID(quality[i].USER_ID);
        //    dt2.Rows.Add(user.USER_NAME, quality[i].USER_ID);
        //}
        //dgvComboBoxU.PropertiesComboBox.DataSource = dt2;
        //dgvComboBoxU.PropertiesComboBox.TextField = "DisplayField";
        //dgvComboBoxU.PropertiesComboBox.ValueField = "ValueField";

        List<SNDetectEntity> entities = SNDetectFactory.GetAll();

        ASPxGridView1.DataSource = entities;
        ASPxGridView1.DataBind();
        //int columnCount = ASPxGridView1.Columns.Count;//获取列的数量 
        //for (int i = 0; i < columnCount; i++)
        //{
        //    GridViewDataHyperLinkColumn colLink = new GridViewDataHyperLinkColumn();
        //    if (((string)ASPxGridView1.GetRowValues(i, "UNITTYPE")) == "F")
        //    {
                
        //    }
        //    else
        //    {
        //        colLink.PropertiesEdit.EnableDefaultAppearance = false;
        //        //e.Row.Cells[8].Enabled = false;
        //        //if (e.GetValue("UNITTYPE").ToString().Trim() == "")
        //        ASPxGridView1.Selection.SetSelection(i, false);
        //    }
        //}
    }

    //protected void ASPxGridView1_Init(object sender, EventArgs e)
    //{
    //    GridViewDataHyperLinkColumn colLink = new GridViewDataHyperLinkColumn();//实例化一个超链接列
    //    colLink.Caption = "下载吧";//设置列头
    //    colLink.PropertiesHyperLinkEdit.Text = "这是个链接";//显示链接的名称
    //    colLink.PropertiesHyperLinkEdit.TextField = "LinkName";//显示链接名称要绑定的字段 
    //    colLink.FieldName = "LinkURL";//该列绑定的字段
    //    colLink.PropertiesHyperLinkEdit.NavigateUrlFormatString = "{0}";//链接地址就是该列绑定的字段
    //    colLink.Visible = true;
    //    colLink.Width = 200;
    //    ASPxGridView1.Columns.Add(colLink);//把该列添加到ASPxGridview
    //}

    protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        //your server-side code
        ASPxGridView grid = (ASPxGridView)sender;
        Session["rmesid"] = "";
       Response.Redirect(HttpContext.Current.Request.Url.ToString() + "/qms2101.aspx" );
      
    }

    //protected void btnXlsExport_Click(object sender, EventArgs e)
    //{
    //    ASPxGridViewExporter1.WriteXlsToResponse();
    //}

    //public void ReadBlob(int idData, string fileName)
    //{
        
    //    //string connString = "server=oratest;User ID=kttest;Password=test";
    //    using (OracleConnection conn = new OracleConnection())
    //    {
    //        try
    //        {
    //            dc.OpenConn();
    //            db.OpenSharedConnection();
    //            conn.Open();
    //            OracleCommand cmd = conn.CreateCommand();
               
    //            // 利用事务处理（必须）
    //            OracleTransaction transaction = cmd.Connection.BeginTransaction();
    //            cmd.Transaction = transaction;

    //            // 获得 OracleLob 指针
    //            string sql = "select FILE_BLOB from QMS_FILE_BLOB where RMES_ID = " + idData;
    //            cmd.CommandText = sql;
    //            OracleDataReader dr = cmd.ExecuteReader();
               
    //            dr.Read();
    //            OracleLob tempLob = dr.GetOracleLob(0);
    //            dr.Close();

    //            // 读取 BLOB 中数据，写入到文件中
    //            FileStream fs = new FileStream(fileName, FileMode.Create);
    //            int length = 1048576;
    //            byte[] Buffer = new byte[length];
    //            int i;
    //            while ((i = tempLob.Read(Buffer, 0, length)) > 0)
    //            {
    //                fs.Write(Buffer, 0, i);
    //            }
    //            fs.Close();
    //            tempLob.Clone();
    //            cmd.Parameters.Clear();

    //            // 提交事务
    //            transaction.Commit();
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //        finally
    //        {
    //            conn.Close();
    //        }
    //    }
    //}

    //public static bool ReadBlobToFile(string idValue, string outFileFullName)
    //{
    //    int PictureCol = 0;

    //    outFileFullName = outFileFullName.Trim();

    //    try
    //    {
    //        OracleCommand cmd = new OracleCommand("select FILE_BLOB from QMS_FILE_BLOB where RMES_ID = " + idValue);
            
    //        OracleDataReader myReader = cmd.ExecuteReader();
    //        myReader.Read();

    //        if (myReader.HasRows == false)
    //        {
    //            return false;
    //        }

    //        byte[] b = new byte[myReader.GetBytes(PictureCol, 0, null, 0, int.MaxValue) - 1];
    //        myReader.GetBytes(PictureCol, 0, b, 0, b.Length);
    //        myReader.Close();

    //        System.IO.FileStream fileStream = new System.IO.FileStream(
    //            outFileFullName, System.IO.FileMode.Create, System.IO.FileAccess.Write);
    //        fileStream.Write(b, 0, b.Length);
    //        fileStream.Close();
    //    }
    //    catch
    //    {
    //        return false;
    //    }

    //    return true;
    //}  
}
