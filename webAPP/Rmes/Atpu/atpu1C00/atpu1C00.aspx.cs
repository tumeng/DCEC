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
using System.Windows.Forms;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxClasses;
using Rmes.Pub.Data;
using Rmes.DA.Base;
using Rmes.Web.Base;
using DevExpress.Web.ASPxGridLookup;
/**
 * 功能概述：现场扫描零件维护
 * 作者：杨少霞
 * 创建时间：2016-08-08
**/

namespace Rmes.WebApp.Rmes.Atpu.atpu1C00
{
    public partial class atpu1C00 : BasePage
    {
        private dataConn dc = new dataConn();
        private PubCs pc = new PubCs();
        public string theCompanyCode, theUserId, theUserName,theUserCode;
        public string theProgramCode;

        protected void Page_Load(object sender, EventArgs e)
        {
            userManager theUserManager = (userManager)Session["theUserManager"];
            theCompanyCode = theUserManager.getCompanyCode();
            theUserId = theUserManager.getUserId();
            theUserName = theUserManager.getUserName();
            theUserCode = theUserManager.getUserCode();
            theProgramCode = "atpu1C00";
            if (!IsPostBack)
            {
                //Session["atpu1C00STATION_NAME"] = "";

            }
            //if(Session["atpu1C00STATION_NAME"].ToString() != "")
            //{
                
            //    SqlDataSource_Station.SelectCommand = Session["atpu1C00STATION_NAME"].ToString();
            //    SqlDataSource_Station.DataBind();
              
 
            //}
            //初始化站点名称begin
            if (Request["opFlag"] == "getEditZD")
            {
                string str1 = "";
                string zdmc = Request["zddmC"].ToString();
                string sql = "select STATION_CODE from CODE_STATION where STATION_NAME='" + zdmc + "'";
                dc.setTheSql(sql);
                if (dc.GetTable().Rows.Count == 0)
                {
                    str1 = "";
                    this.Response.Write(str1);
                    this.Response.End();
                    return;
                }
                string zddm = dc.GetTable().Rows[0][0].ToString();
                if (zdmc == "")
                {
                    str1 = "";
                    this.Response.Write(str1);
                    this.Response.End();
                    return;
                }
                str1 = zddm;
                this.Response.Write(str1);
                this.Response.End();
            }
            //初始化站点名称end

            //初始化零件名称begin
            if (Request["opFlag"] == "getEditLJDM")
            {
                string str1 = "";
                string ljdm = Request["ljdmC"].ToString();
                string sql = "select PT_DESC2 from COPY_PT_MSTR where PT_PART='" + ljdm + "'";
                dc.setTheSql(sql);
                if (dc.GetTable().Rows.Count == 0)
                {
                    str1 = "";
                    this.Response.Write(str1);
                    this.Response.End();
                    return;
                }
                string ljmc = dc.GetTable().Rows[0][0].ToString();
                if (ljdm == "")
                {
                    str1 = "";
                    this.Response.Write(str1);
                    this.Response.End();
                    return;
                }
                str1 = ljmc;
                this.Response.Write(str1);
                this.Response.End();
            }
            //初始化零件名称end

            if (Request["opFlag"] == "getEditSeries")
            {
                string str = "";
                string pcode = Request["PCode"].ToString().Trim();

                //dataConn theDataConn = new dataConn(" select FUNC_GET_PLANSITE（'" + pcode + "','D'）from dual");
                //theDataConn.OpenConn();
                //string gQadSite = theDataConn.GetValue();
                //if (gQadSite != "")
                //{
                string sql = "SELECT distinct STATION_NAME FROM CODE_STATION  where PLINE_CODE=RH_GET_DATA('L','" + pcode + "','','','')";
                Session["1C00STATION"] = sql;
                SqlDataSource2.SelectCommand = sql;
                SqlDataSource2.DataBind();
                //}

                this.Response.Write(str);
                this.Response.End();
            }

            setCondition();


            //string Sql2 = "SELECT distinct STATION_NAME FROM CODE_STATION order by STATION_NAME ";
            //SqlDataSource2.SelectCommand = Sql2;
            //SqlDataSource2.DataBind();
            //SqlDataSource22.SelectCommand = Sql2;
            //SqlDataSource22.DataBind();
            string Sql3 = "SELECT distinct PT_PART,PT_DESC2 FROM COPY_PT_MSTR where pt_phantom=0 order by PT_PART ";
            SqlDataSource3.SelectCommand = Sql3;
            SqlDataSource3.DataBind();

            string Sql4 = "select a.pline_code,b.rmes_id,b.pline_name from VW_USER_ROLE_PROGRAM a "
                        + "left join code_product_line b on a.pline_code=b.pline_code "
                        + "where a.COMPANY_CODE = '" + theCompanyCode + "' and a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' order by b.PLINE_NAME";
            SqlDataSource4.SelectCommand = Sql4;
            SqlDataSource4.DataBind();

            string Sql8 = "select internal_code,internal_name from code_internal  "
                        + "where COMPANY_CODE = '" + theCompanyCode + "' and internal_type_code='011' order by internal_code";
            SqlLJLB.SelectCommand = Sql8;
            SqlLJLB.DataBind();
            //if (!IsPostBack)
            //{
            //    string sql = "SELECT distinct STATION_NAME FROM CODE_STATION ";
            //    Session["1C00STATION"] = sql;
            //    SqlDataSource2.SelectCommand = sql;
            //    SqlDataSource2.DataBind();
            //}
        }
        private void setCondition()
        {
            string sql = "SELECT A.ZDDM,A.ZDMC,A.GZDD,A.LJDM,A.LJMC,B.PLINE_CODE PLINECODE1,B.PLINE_NAME,C.INTERNAL_NAME PART_ABC_NAME,A.PART_ABC FROM ATPUZDLJMLB A LEFT JOIN CODE_PRODUCT_LINE B ON A.GZDD=B.PLINE_CODE "
                       + " LEFT JOIN (SELECT * FROM CODE_INTERNAL WHERE INTERNAL_TYPE_CODE='011' ) C ON A.PART_ABC=C.INTERNAL_CODE "
                       + " WHERE  A.GZDD in(SELECT PLINE_CODE FROM VW_USER_ROLE_PROGRAM WHERE USER_ID='" + theUserId + "' "
                       + " And program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "')"
                       + " ORDER BY a.INPUT_TIME desc nulls last, A.GZDD,A.ZDDM,A.LJDM ";
            DataTable dt = dc.GetTable(sql);

            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();
            string sql2 = "SELECT A.*,B.PLINE_NAME,C.INTERNAL_NAME PART_ABC_NAME FROM ATPUSMLJB A LEFT JOIN CODE_PRODUCT_LINE B ON A.PLINE_CODE=B.PLINE_CODE "
                        + " LEFT JOIN (SELECT * FROM CODE_INTERNAL WHERE INTERNAL_TYPE_CODE='011' ) C ON A.PART_ABC=C.INTERNAL_CODE "
                        + " WHERE  A.PLINE_CODE in(SELECT PLINE_CODE FROM VW_USER_ROLE_PROGRAM WHERE USER_ID='" + theUserId + "' "
                        + " And program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "')"
                        + " ORDER BY a.INPUT_TIME desc nulls last,A.PLINE_CODE,A.ABOM_DESC ";
            DataTable dt2 = dc.GetTable(sql2);

            ASPxGridView2.DataSource = dt2;
            ASPxGridView2.DataBind();
        }
        //新增
        protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            ASPxComboBox pLine = ASPxGridView1.FindEditFormTemplateControl("comboPLine") as ASPxComboBox;
            ASPxComboBox zdmc = ASPxGridView1.FindEditFormTemplateControl("comboStationCode") as ASPxComboBox;
            //ASPxTextBox zddm = ASPxGridView1.FindEditFormTemplateControl("txtStationName") as ASPxTextBox;
            ASPxGridLookup gridlookup = ASPxGridView1.FindEditFormTemplateControl("comboLJDM") as ASPxGridLookup;
            ASPxComboBox part_ABC = ASPxGridView1.FindEditFormTemplateControl("comboLJLB") as ASPxComboBox;
            string zddm = dc.GetValue("select STATION_CODE from CODE_STATION where STATION_NAME='" + zdmc.Value.ToString() + "'");
            string ljdm = gridlookup.Value.ToString();
            string ljmc = gridlookup.Text.Trim();
            
            string Sql = "INSERT INTO ATPUZDLJMLB (GZDD,ZDDM,ZDMC,LJDM,LJMC,INPUT_PERSON,INPUT_TIME,PART_ABC) "
                   + "VALUES( '" + pLine.Value.ToString() + "','" + zddm + "','" + zdmc.Value.ToString() + "','" + ljdm + "','" + ljmc + "','"+theUserId+"',SYSDATE,'"+part_ABC.Value.ToString()+"')";
            dc.ExeSql(Sql);
            //插入到日志表161103
            try
            {
                string Sql2 = "INSERT INTO ATPUZDLJMLB_LOG (GZDD,ZDDM,ZDMC,LJDM,LJMC,user_code,flag,rqsj,PART_ABC)"
                    + " SELECT GZDD,ZDDM,ZDMC,LJDM,LJMC,'" + theUserCode + "' , 'ADD', SYSDATE,PART_ABC FROM ATPUZDLJMLB WHERE  GZDD =  '" + pLine.Value.ToString() + "' and ZDMC =  '" + zdmc.Value.ToString() + "' and LJDM =  '" + ljdm + "'";
                dc.ExeSql(Sql2);
            }
            catch
            {
                return;
            }

            e.Cancel = true;
            ASPxGridView1.CancelEdit();
            setCondition();
        }
        //修改
        protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            
        }
        //创建EDITFORM前
        protected void ASPxGridView1_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditFormEventArgs e)
        {
            string Sql3 = "SELECT PT_PART,PT_DESC2 FROM COPY_PT_MSTR where pt_phantom=0 order by PT_PART ";
            SqlDataSource3.SelectCommand = Sql3;
            SqlDataSource3.DataBind();
            
        }
        //修改数据校验
        protected void ASPxGridView1_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            //判断为空
            string strPLine = e.NewValues["GZDD"].ToString().Trim();
            string strZddm = e.NewValues["ZDMC"].ToString().Trim();
            string strLjdm = e.NewValues["LJDM"].ToString().Trim();

            //判断是否重复
            if (ASPxGridView1.IsNewRowEditing)
            {
                string chSql = "SELECT GZDD,ZDDM,LJDM  FROM ATPUZDLJMLB WHERE  GZDD =  '" + strPLine + "' "
                             +"and ZDMC =  '" + strZddm + "' and LJDM =  '" + strLjdm + "'";
                DataTable dt = dc.GetTable(chSql);
                if (dt.Rows.Count > 0)
                {
                    e.RowError = "已存在相同的记录！";
                }

            }
        }
        protected void ASPxGridView2_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            //判断当前记录是否可以删除
            string dPLine = e.Values["PLINE_CODE"].ToString();
            
            string dLjmc = e.Values["ABOM_DESC"].ToString();
            string strTableName = "ATPUSMLJB";

            dataConn theDataConn = new dataConn("select func_check_delete_data('" + strTableName + "','MES','MES','MES','MES','" + dLjmc + "') from dual");

            theDataConn.OpenConn();
            string theRet = theDataConn.GetValue();
            if (theRet != "Y")
            {
                ASPxGridView2.JSProperties.Add("cpCallbackName", "Delete");
                ASPxGridView2.JSProperties.Add("cpCallbackRet", theRet);
                theDataConn.CloseConn();
            }
            else
            {
                //插入到日志表
                try
                {
                    string Sql2 = "INSERT INTO ATPUSMLJB_LOG (PLINE_CODE,ABOM_DESC,PART_ABC,user_code,flag,rqsj)"
                        + " SELECT PLINE_CODE,ABOM_DESC,PART_ABC,'" + theUserCode + "' , 'DEL', SYSDATE FROM ATPUSMLJB "
                        + "WHERE  PLINE_CODE =  '" + dPLine + "' and ABOM_DESC =  '" + dLjmc + "'";
                    dc.ExeSql(Sql2);
                }
                catch
                {
                    return;
                }
                //确认删除
                string Sql = "delete from ATPUSMLJB WHERE   PLINE_CODE =  '" + dPLine + "' and ABOM_DESC =  '" + dLjmc + "'";
                dc.ExeSql(Sql);

            }
            
            setCondition();
            e.Cancel = true;
        }
        protected void ASPxGridView2_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {

            ASPxComboBox plineCode = ASPxGridView2.FindEditFormTemplateControl("txtPCode") as ASPxComboBox;
            ASPxTextBox ljmc = ASPxGridView2.FindEditFormTemplateControl("txtName") as ASPxTextBox;
            ASPxComboBox partNAME_ABC = ASPxGridView2.FindEditFormTemplateControl("comboLJLB_NAME") as ASPxComboBox;

            string Sql = "INSERT INTO ATPUSMLJB (PLINE_CODE,ABOM_DESC,PART_ABC,INPUT_PERSON,INPUT_TIME) "
                   + "VALUES( '" + plineCode.Value.ToString() + "','" + ljmc.Text.Trim() + "','" + partNAME_ABC.Value.ToString() + "','" + theUserId + "',SYSDATE)";
            dc.ExeSql(Sql);
            //插入到日志表
            try
            {
                string Sql2 = "INSERT INTO ATPUSMLJB_LOG (PLINE_CODE,ABOM_DESC,PART_ABC,user_code,flag,rqsj)"
                    + " SELECT PLINE_CODE,ABOM_DESC,PART_ABC,'" + theUserCode + "' , 'ADD', SYSDATE FROM ATPUSMLJB "
                    + "WHERE  PLINE_CODE =  '" + plineCode.Value.ToString() + "' and ABOM_DESC =  '" + ljmc.Text.Trim() + "'";
                dc.ExeSql(Sql2);
            }
            catch
            {
                return;
            }

            e.Cancel = true;
            ASPxGridView2.CancelEdit();
            setCondition();
        }

        protected void ASPxGridView2_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditFormEventArgs e)
        {

            string PSql = "select PLINE_CODE,PLINE_NAME from CODE_PRODUCT_LINE where pline_code in (select pline_code from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "')";
            DataTable Pdt = dc.GetTable(PSql);
            ASPxComboBox uPcode = ASPxGridView2.FindEditFormTemplateControl("txtPCode") as ASPxComboBox;
            uPcode.DataSource = Pdt;
            uPcode.TextField = Pdt.Columns[1].ToString();
            uPcode.ValueField = Pdt.Columns[0].ToString();

        }

        protected void ASPxGridView2_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            //判断是否重复
            if (ASPxGridView2.IsNewRowEditing)
            {
                string chSql = "";

                chSql = "SELECT ABOM_DESC  FROM ATPUSMLJB"
                          + " WHERE  PLINE_CODE='" + e.NewValues["PLINE_CODE"].ToString() + "' and ABOM_DESC='" + e.NewValues["ABOM_DESC"].ToString() + "'   ";

                DataTable dt = dc.GetTable(chSql);
                if (dt.Rows.Count > 0)
                {
                    e.RowError = "已存在相同的生产线和零件名称！";
                }
            }


        }
        protected void btnXlsExport_Click(object sender, EventArgs e)
        {
            ASPxGridViewExporter1.WriteXlsToResponse("按零件号维护现场扫描零件信息导出");
        }
        protected void btnXlsExport2_Click(object sender, EventArgs e)
        {
            ASPxGridViewExporter2.WriteXlsToResponse("按名称维护现场扫描零件信息导出");
        }
        protected void ASPxGridView1_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            try
            {

                if (Session["1C00STATION"].ToString() != "")
                {
                    string sql = Session["1C00STATION"] as string;
                    SqlDataSource2.SelectCommand = sql;
                    SqlDataSource2.DataBind();
                }
            }
            catch
            { }
        }

        protected void comboStationCode_Init(object sender, EventArgs e)
        {
            try
            {

                if (Session["1C00STATION"].ToString() != "")
                {
                    string sql = Session["1C00STATION"] as string;
                    SqlDataSource2.SelectCommand = sql;
                    SqlDataSource2.DataBind();
                }
            }
            catch
            { }
        }

        protected void comboStationCode_Callback(object sender, CallbackEventArgsBase e)
        {
            string pline = e.Parameter;

            string sql = "select RMES_ID,STATION_NAME from CODE_STATION where PLINE_CODE=RH_GET_DATA('L','" + pline + "','','','') order by STATION_NAME";
            DataTable dt = dc.GetTable(sql);
            //Session["atpu1C00STATION_NAME"] = sql;
            //SqlDataSource_Station.SelectCommand = sql;
            //SqlDataSource_Station.DataBind();
            ASPxComboBox station = sender as ASPxComboBox;
            station.DataSource = dt;
            station.ValueField = "STATION_NAME";
            station.TextField = "STATION_NAME";
            station.DataBind();
        }

        protected void ASPxGridView1_RowDeleting1(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            //判断当前记录是否可以删除
            string dPLine = e.Values["GZDD"].ToString();
            string dZddm = e.Values["ZDMC"].ToString();
            string dLjdm = e.Values["LJDM"].ToString();
            string strTableName = "ATPUZDLJMLB";

            dataConn theDataConn = new dataConn("select func_check_delete_data('" + strTableName + "','MES','MES','" + dPLine + "','" + dZddm + "','" + dLjdm + "') from dual");

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
                //插入到日志表
                try
                {
                    string Sql2 = "INSERT INTO ATPUZDLJMLB_LOG (GZDD,SO,ZDDM,ZDMC,LJDM,LJMC,user_code,flag,rqsj,PART_ABC)"
                        + " SELECT GZDD,SO,ZDDM,ZDMC,LJDM,LJMC,'" + theUserCode + "' , 'DEL', SYSDATE,PART_ABC FROM ATPUZDLJMLB "
                        + "WHERE  GZDD =  '" + dPLine + "' and ZDMC =  '" + dZddm + "' and LJDM =  '" + dLjdm + "'";
                    dc.ExeSql(Sql2);
                }
                catch
                {
                    return;
                }
                //确认删除
                string Sql = "delete from ATPUZDLJMLB WHERE   GZDD =  '" + dPLine + "' and ZDMC =  '" + dZddm + "' and LJDM =  '" + dLjdm + "'";
                dc.ExeSql(Sql);

            }

            e.Cancel = true;
            setCondition();
        }
    }
}