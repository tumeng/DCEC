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
using Rmes.Web.Base;
using PetaPoco;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;
using Rmes.Pub.Data;
using Rmes.DA.Base;
/**
 * 功能概述：铭牌参数维护
 * 作者：游晓航
 * 创建时间：2016-06-24
 * 修改时间：
 */



namespace Rmes.WebApp.Rmes.Atpu.atpu2900
{
    public partial class atpu2900 : BasePage
    {
        private dataConn dc = new dataConn();
        private PubCs pc = new PubCs();
        public string theCompanyCode, theUserId, theUserName,theProgramCode;
        public DateTime OracleEntityTime, OracleSQLTime, theTime;
        protected void Page_Load(object sender, EventArgs e)
        {
            userManager theUserManager = (userManager)Session["theUserManager"];
            theCompanyCode = theUserManager.getCompanyCode();
            theTime = DateTime.Now;
            theUserId = theUserManager.getUserId();
            theUserName = theUserManager.getUserName();
            theProgramCode = "atpu2900";
            if (!IsPostBack)
            {
                initCode();
            }
            setCondition();
            if (Request["opFlag"] == "getEditSeries")
            {
                string str1 = "", sql="";
                string so = Request["SO"].ToString().Trim();
                string fr = Request["FR"].ToString().Trim();
                if (so == "" || so == "null")
                {
                    sql = "select JX,GL,QFJQ,QFPQ,PL,XL,DS,FHCX,XNBH,DYJX,ZS,KHH,GD,PRSG,JZL,PYZS,EDGYL,HB,ENYN,GL1,ZS1,BYGL1,BYZS1,BYGL2,BYZS2,NOX,PM,KHGG,PEL,EPA,PFJD,PFJDHZH,FR,MPLJH,XZJD,DYGLD,ZDJGL,ZXBZ,XZMC,CZYG,XSHZHHMLY,HCLZZLX,SCXKZ,bzso from NAMEPLATE_SO where FR='" + fr + "'";
                }

                else
                {
                    sql = "select JX,GL,QFJQ,QFPQ,PL,XL,DS,FHCX,XNBH,DYJX,ZS,KHH,GD,PRSG,JZL,PYZS,EDGYL,HB,ENYN,GL1,ZS1,BYGL1,BYZS1,BYGL2,BYZS2,NOX,PM,KHGG,PEL,EPA,PFJD,PFJDHZH,FR,MPLJH,XZJD,DYGLD,ZDJGL,ZXBZ,XZMC,CZYG,XSHZHHMLY,HCLZZLX,SCXKZ,bzso from NAMEPLATE_SO where bzso='" + so + "'  ";
                }
                 
                    DataTable dt = dc.GetTable(sql);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < 44; i++)
                    {
                        string str = dt.Rows[0][i].ToString();
                        str1 = str1 + "@" + str;
                    }
                }
                this.Response.Write(str1);
                this.Response.End();
            }

        }

        private void initCode()
        {
            //初始化下拉列表
            string sql = "select distinct a.pline_code,b.pline_name from vw_user_role_program a left join code_product_line b on a.pline_code=b.pline_code where a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' and a.company_code='" + theCompanyCode + "'";
            SqlCode.SelectCommand = sql;
            SqlCode.DataBind();
        }
        private void setCondition()
        {
            //初始化GRIDVIEW
            string sql = "SELECT a.* FROM NAMEPLATE_SO a  where 1=1 ";
            if (txtPCode.Text.Trim() != "")
            {
                sql = sql + " and pline_Code = '" + txtPCode.Value.ToString() + "'";
            }
            else
            {
                sql = sql + " and pline_Code in (select a.pline_code from vw_user_role_program a left join code_product_line b on a.pline_code=b.pline_code where a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' and a.company_code='" + theCompanyCode + "')";
            }
            sql = sql + " ORDER BY MODIFYDATE DESC";
            DataTable dt = dc.GetTable(sql);
            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();
        }

        protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            //判断当前记录是否可以删除
            string strDelCode = e.Values["BZSO"].ToString();
            string strDelCode2 = e.Values["PLINE_CODE"].ToString();
            string strTableName = "NAMEPLATE_SO";

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
                string Sql = "delete from NAMEPLATE_SO WHERE   BZSO = '" + strDelCode + "' and PLINE_CODE = '" + strDelCode2 + "'";
                dc.ExeSql(Sql);
            }


            setCondition();
            e.Cancel = true;
        }


        protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            ASPxTextBox tJX = ASPxGridView1.FindEditFormTemplateControl("txtJX") as ASPxTextBox;
            ASPxTextBox tSO = ASPxGridView1.FindEditFormTemplateControl("txtSO") as ASPxTextBox;
            ASPxTextBox tXL = ASPxGridView1.FindEditFormTemplateControl("txtXL") as ASPxTextBox;
            ASPxTextBox tGL1 = ASPxGridView1.FindEditFormTemplateControl("txtGL1") as ASPxTextBox;
            ASPxTextBox tPFJDHZH = ASPxGridView1.FindEditFormTemplateControl("txtPFJDHZH") as ASPxTextBox;
            ASPxTextBox tGL = ASPxGridView1.FindEditFormTemplateControl("txtGL") as ASPxTextBox;
            ASPxTextBox tQFJQ = ASPxGridView1.FindEditFormTemplateControl("txtQFJQ") as ASPxTextBox;
            ASPxTextBox tQFPQ = ASPxGridView1.FindEditFormTemplateControl("txtQFPQ") as ASPxTextBox;
            ASPxTextBox tZS1 = ASPxGridView1.FindEditFormTemplateControl("txtZS1") as ASPxTextBox;
            ASPxTextBox tXSHZHHMLY = ASPxGridView1.FindEditFormTemplateControl("txtXSHZHHMLY") as ASPxTextBox;
            ASPxTextBox tPL = ASPxGridView1.FindEditFormTemplateControl("txtPL") as ASPxTextBox;
            ASPxTextBox tDS = ASPxGridView1.FindEditFormTemplateControl("txtDS") as ASPxTextBox;
            ASPxTextBox tFHCX = ASPxGridView1.FindEditFormTemplateControl("txtFHCX") as ASPxTextBox;
            ASPxTextBox tBYGL1 = ASPxGridView1.FindEditFormTemplateControl("txtBYGL1") as ASPxTextBox;
            ASPxTextBox tHCLZZLX = ASPxGridView1.FindEditFormTemplateControl("txtHCLZZLX") as ASPxTextBox;
            ASPxTextBox tZS = ASPxGridView1.FindEditFormTemplateControl("txtZS") as ASPxTextBox;
            ASPxTextBox tXNBH = ASPxGridView1.FindEditFormTemplateControl("txtXNBH") as ASPxTextBox;
            ASPxTextBox tDYJX = ASPxGridView1.FindEditFormTemplateControl("txtDYJX") as ASPxTextBox;
            ASPxTextBox tBYZS1 = ASPxGridView1.FindEditFormTemplateControl("txtBYZS1") as ASPxTextBox;
            ASPxTextBox tSCXKZ = ASPxGridView1.FindEditFormTemplateControl("txtSCXKZ") as ASPxTextBox;
            ASPxTextBox tHB = ASPxGridView1.FindEditFormTemplateControl("txtHB") as ASPxTextBox;
            ASPxTextBox tKHGG = ASPxGridView1.FindEditFormTemplateControl("txtKHGG") as ASPxTextBox;
            ASPxTextBox tKHH = ASPxGridView1.FindEditFormTemplateControl("txtKHH") as ASPxTextBox;
            ASPxTextBox tGD = ASPxGridView1.FindEditFormTemplateControl("txtGD") as ASPxTextBox;           
            ASPxTextBox tJZL = ASPxGridView1.FindEditFormTemplateControl("txtJZL") as ASPxTextBox;
            ASPxTextBox tPYZS = ASPxGridView1.FindEditFormTemplateControl("txtPYZS") as ASPxTextBox;
            ASPxTextBox tPFJD = ASPxGridView1.FindEditFormTemplateControl("txtPFJD") as ASPxTextBox;
            ASPxTextBox tBYGL2 = ASPxGridView1.FindEditFormTemplateControl("txtBYGL2") as ASPxTextBox;
            ASPxTextBox tEDGYL = ASPxGridView1.FindEditFormTemplateControl("txtEDGYL") as ASPxTextBox;
            ASPxTextBox tXZJD = ASPxGridView1.FindEditFormTemplateControl("txtXZJD") as ASPxTextBox;
            ASPxTextBox tZXBZ = ASPxGridView1.FindEditFormTemplateControl("txtZXBZ") as ASPxTextBox;
            ASPxTextBox tXZMC = ASPxGridView1.FindEditFormTemplateControl("txtXZMC") as ASPxTextBox;
            ASPxTextBox tBYZS2 = ASPxGridView1.FindEditFormTemplateControl("txtBYZS2") as ASPxTextBox;
            ASPxTextBox tMPLJH = ASPxGridView1.FindEditFormTemplateControl("txtMPLJH") as ASPxTextBox;
            ASPxTextBox tNOX = ASPxGridView1.FindEditFormTemplateControl("txtNOX") as ASPxTextBox;
            ASPxTextBox tPM = ASPxGridView1.FindEditFormTemplateControl("txtPM") as ASPxTextBox;
            ASPxTextBox tPEL = ASPxGridView1.FindEditFormTemplateControl("txtPEL") as ASPxTextBox;      
            ASPxTextBox tZDJGL = ASPxGridView1.FindEditFormTemplateControl("txtZDJGL") as ASPxTextBox;
            ASPxTextBox tEPA = ASPxGridView1.FindEditFormTemplateControl("txtEPA") as ASPxTextBox;
            ASPxTextBox tFR = ASPxGridView1.FindEditFormTemplateControl("txtFR") as ASPxTextBox;
            ASPxTextBox tDYGLD = ASPxGridView1.FindEditFormTemplateControl("txtDYGLD") as ASPxTextBox;
           
            ASPxComboBox cPRSG = ASPxGridView1.FindEditFormTemplateControl("txtPRSG") as ASPxComboBox;
            ASPxComboBox cENYN = ASPxGridView1.FindEditFormTemplateControl("txtENYN") as ASPxComboBox;

            string JX = tJX.Text.Trim();
            string SO = tSO.Text.Trim(); 
            string XL = tXL.Text.Trim();
            string GL1 = tGL1.Text.Trim();
            string PFJDHZH = tPFJDHZH.Text.Trim();
            string GL = tGL.Text.Trim();
            string QFJQ = tQFJQ.Text.Trim();
            string QFPQ = tQFPQ.Text.Trim();
            string ZS1 = tZS1.Text.Trim();
            string XSHZHHMLY = tXSHZHHMLY.Text.Trim();
            string PL = tPL.Text.Trim();
            string DS = tDS.Text.Trim();
            string FHCX = tFHCX.Text.Trim();
            string BYGL1 = tBYGL1.Text.Trim();
            string HCLZZLX = tHCLZZLX.Text.Trim();
            string ZS = tZS.Text.Trim();
            string XNBH = tXNBH.Text.Trim();
            string DYJX = tDYJX.Text.Trim();
            string BYZS1 = tBYZS1.Text.Trim();
            string SCXKZ = tSCXKZ.Text.Trim();
            string HB = tHB.Text.Trim();
            string KHGG = tKHGG.Text.Trim();
            string KHH = tKHH.Text.Trim();
            string GD = tGD.Text.Trim();
            string JZL = tJZL.Text.Trim();
            string PYZS = tPYZS.Text.Trim();
            string PFJD = tPFJD.Text.Trim();
            string BYGL2 = tBYGL2.Text.Trim();
            string EDGYL = tEDGYL.Text.Trim();
            string XZJD = tXZJD.Text.Trim();
            string ZXBZ = tZXBZ.Text.Trim();
            string XZMC = tXZMC.Text.Trim();
            string BYZS2 = tBYZS2.Text.Trim();
            string MPLJH = tMPLJH.Text.Trim();
            string NOX = tNOX.Text.Trim();
            string PM = tPM.Text.Trim();
            string PEL = tPEL.Text.Trim();
            string ZDJGL = tZDJGL.Text.Trim();
            string EPA = tEPA.Text.Trim();
            string FR = tFR.Text.Trim();
            string DYGLD = tDYGLD.Text.Trim();
            string ENYN = "", PRSG = "",pcode="";
            if (cENYN.Text.Trim() != "")
            {
                 ENYN = cENYN.Value.ToString();
            }
            if (cPRSG.Text.Trim() != "")
            {
                PRSG = cPRSG.Value.ToString();
            }
            if (txtPCode.Text.Trim() != "")
            {
                pcode = txtPCode.Value.ToString();
            }

            string Sql = "INSERT INTO NAMEPLATE_SO(PLINE_CODE,JX,BZSO,GL,QFJQ,QFPQ,PL,XL,DS,FHCX,XNBH,DYJX,ZS,KHH,GD,PRSG,YH,JZL,PYZS,EDGYL,HB,ENYN,GL1,ZS1,BYGL1,BYZS1,BYGL2,BYZS2,NOX,PM,KHGG,PEL,EPA,PFJD,PFJDHZH,FR,MPLJH,XZJD,DYGLD,ZDJGL,ZXBZ,XZMC,CZYG,MODIFYDATE,XSHZHHMLY,HCLZZLX,SCXKZ)  "
                   + "VALUES( '" + pcode + "', '" + JX + "','" + SO + "','" + GL + "','" + QFJQ + "','" + QFPQ + "','" + PL + "','" + XL + "', '" + DS + "', '" + FHCX + "','" + XNBH + "','" + DYJX + "','" + ZS + "','" + KHH + "','" + GD + "','" + PRSG + "','" + theUserName + "' ,"
                   + "'" + JZL + "', '" + PYZS + "','" + EDGYL + "','" + HB + "','" + ENYN + "','" + GL1 + "','" + ZS1 + "','" + BYGL1 + "', '" + BYZS1 + "', '" + BYGL2 + "', '" + BYZS2 + "','" + NOX + "','" + PM + "','" + KHGG + "','" + PEL + "','" + EPA + "','" + PFJD + "','" + PFJDHZH + "' ,"
                   + " '" + FR + "', '" + MPLJH + "','" + XZJD + "','" + DYGLD + "','" + ZDJGL + "','" + ZXBZ + "','" + XZMC + "','" + theUserName + "', sysdate, '" + XSHZHHMLY + "','" + HCLZZLX + "','" + SCXKZ + "')";

            dc.ExeSql(Sql);

            e.Cancel = true;
            ASPxGridView1.CancelEdit();
            setCondition();

        }


        protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {

            ASPxTextBox tJX = ASPxGridView1.FindEditFormTemplateControl("txtJX") as ASPxTextBox;
            ASPxTextBox tSO = ASPxGridView1.FindEditFormTemplateControl("txtSO") as ASPxTextBox;
            ASPxTextBox tXL = ASPxGridView1.FindEditFormTemplateControl("txtXL") as ASPxTextBox;
            ASPxTextBox tGL1 = ASPxGridView1.FindEditFormTemplateControl("txtGL1") as ASPxTextBox;
            ASPxTextBox tPFJDHZH = ASPxGridView1.FindEditFormTemplateControl("txtPFJDHZH") as ASPxTextBox;
            ASPxTextBox tGL = ASPxGridView1.FindEditFormTemplateControl("txtGL") as ASPxTextBox;
            ASPxTextBox tQFJQ = ASPxGridView1.FindEditFormTemplateControl("txtQFJQ") as ASPxTextBox;
            ASPxTextBox tQFPQ = ASPxGridView1.FindEditFormTemplateControl("txtQFPQ") as ASPxTextBox;
            ASPxTextBox tZS1 = ASPxGridView1.FindEditFormTemplateControl("txtZS1") as ASPxTextBox;
            ASPxTextBox tXSHZHHMLY = ASPxGridView1.FindEditFormTemplateControl("txtXSHZHHMLY") as ASPxTextBox;
            ASPxTextBox tPL = ASPxGridView1.FindEditFormTemplateControl("txtPL") as ASPxTextBox;
            ASPxTextBox tDS = ASPxGridView1.FindEditFormTemplateControl("txtDS") as ASPxTextBox;
            ASPxTextBox tFHCX = ASPxGridView1.FindEditFormTemplateControl("txtFHCX") as ASPxTextBox;
            ASPxTextBox tBYGL1 = ASPxGridView1.FindEditFormTemplateControl("txtBYGL1") as ASPxTextBox;
            ASPxTextBox tHCLZZLX = ASPxGridView1.FindEditFormTemplateControl("txtHCLZZLX") as ASPxTextBox;
            ASPxTextBox tZS = ASPxGridView1.FindEditFormTemplateControl("txtZS") as ASPxTextBox;
            ASPxTextBox tXNBH = ASPxGridView1.FindEditFormTemplateControl("txtXNBH") as ASPxTextBox;
            ASPxTextBox tDYJX = ASPxGridView1.FindEditFormTemplateControl("txtDYJX") as ASPxTextBox;
            ASPxTextBox tBYZS1 = ASPxGridView1.FindEditFormTemplateControl("txtBYZS1") as ASPxTextBox;
            ASPxTextBox tSCXKZ = ASPxGridView1.FindEditFormTemplateControl("txtSCXKZ") as ASPxTextBox;
            ASPxTextBox tHB = ASPxGridView1.FindEditFormTemplateControl("txtHB") as ASPxTextBox;
            ASPxTextBox tKHGG = ASPxGridView1.FindEditFormTemplateControl("txtKHGG") as ASPxTextBox;
            ASPxTextBox tKHH = ASPxGridView1.FindEditFormTemplateControl("txtKHH") as ASPxTextBox;
            ASPxTextBox tGD = ASPxGridView1.FindEditFormTemplateControl("txtGD") as ASPxTextBox;
            ASPxTextBox tJZL = ASPxGridView1.FindEditFormTemplateControl("txtJZL") as ASPxTextBox;
            ASPxTextBox tPYZS = ASPxGridView1.FindEditFormTemplateControl("txtPYZS") as ASPxTextBox;
            ASPxTextBox tPFJD = ASPxGridView1.FindEditFormTemplateControl("txtPFJD") as ASPxTextBox;
            ASPxTextBox tBYGL2 = ASPxGridView1.FindEditFormTemplateControl("txtBYGL2") as ASPxTextBox;
            ASPxTextBox tEDGYL = ASPxGridView1.FindEditFormTemplateControl("txtEDGYL") as ASPxTextBox;
            ASPxTextBox tXZJD = ASPxGridView1.FindEditFormTemplateControl("txtXZJD") as ASPxTextBox;
            ASPxTextBox tZXBZ = ASPxGridView1.FindEditFormTemplateControl("txtZXBZ") as ASPxTextBox;
            ASPxTextBox tXZMC = ASPxGridView1.FindEditFormTemplateControl("txtXZMC") as ASPxTextBox;
            ASPxTextBox tBYZS2 = ASPxGridView1.FindEditFormTemplateControl("txtBYZS2") as ASPxTextBox;
            ASPxTextBox tMPLJH = ASPxGridView1.FindEditFormTemplateControl("txtMPLJH") as ASPxTextBox;
            ASPxTextBox tNOX = ASPxGridView1.FindEditFormTemplateControl("txtNOX") as ASPxTextBox;
            ASPxTextBox tPM = ASPxGridView1.FindEditFormTemplateControl("txtPM") as ASPxTextBox;
            ASPxTextBox tPEL = ASPxGridView1.FindEditFormTemplateControl("txtPEL") as ASPxTextBox;
            ASPxTextBox tZDJGL = ASPxGridView1.FindEditFormTemplateControl("txtZDJGL") as ASPxTextBox;
            ASPxTextBox tEPA = ASPxGridView1.FindEditFormTemplateControl("txtEPA") as ASPxTextBox;
            ASPxTextBox tFR = ASPxGridView1.FindEditFormTemplateControl("txtFR") as ASPxTextBox;
            ASPxTextBox tDYGLD = ASPxGridView1.FindEditFormTemplateControl("txtDYGLD") as ASPxTextBox;

            ASPxComboBox cPRSG = ASPxGridView1.FindEditFormTemplateControl("txtPRSG") as ASPxComboBox;
            ASPxComboBox cENYN = ASPxGridView1.FindEditFormTemplateControl("txtENYN") as ASPxComboBox;

            string JX = tJX.Text.Trim();
            string SO = tSO.Text.Trim();
            string XL = tXL.Text.Trim();
            string GL1 = tGL1.Text.Trim();
            string PFJDHZH = tPFJDHZH.Text.Trim();
            string GL = tGL.Text.Trim();
            string QFJQ = tQFJQ.Text.Trim();
            string QFPQ = tQFPQ.Text.Trim();
            string ZS1 = tZS1.Text.Trim();
            string XSHZHHMLY = tXSHZHHMLY.Text.Trim();
            string PL = tPL.Text.Trim();
            string DS = tDS.Text.Trim();
            string FHCX = tFHCX.Text.Trim();
            string BYGL1 = tBYGL1.Text.Trim();
            string HCLZZLX = tHCLZZLX.Text.Trim();
            string ZS = tZS.Text.Trim();
            string XNBH = tXNBH.Text.Trim();
            string DYJX = tDYJX.Text.Trim();
            string BYZS1 = tBYZS1.Text.Trim();
            string SCXKZ = tSCXKZ.Text.Trim();
            string HB = tHB.Text.Trim();
            string KHGG = tKHGG.Text.Trim();
            string KHH = tKHH.Text.Trim();
            string GD = tGD.Text.Trim();
            string JZL = tJZL.Text.Trim();
            string PYZS = tPYZS.Text.Trim();
            string PFJD = tPFJD.Text.Trim();
            string BYGL2 = tBYGL2.Text.Trim();
            string EDGYL = tEDGYL.Text.Trim();
            string XZJD = tXZJD.Text.Trim();
            string ZXBZ = tZXBZ.Text.Trim();
            string XZMC = tXZMC.Text.Trim();
            string BYZS2 = tBYZS2.Text.Trim();
            string MPLJH = tMPLJH.Text.Trim();
            string NOX = tNOX.Text.Trim();
            string PM = tPM.Text.Trim();
            string PEL = tPEL.Text.Trim();
            string ZDJGL = tZDJGL.Text.Trim();
            string EPA = tEPA.Text.Trim();
            string FR = tFR.Text.Trim();
            string DYGLD = tDYGLD.Text.Trim();
            string ENYN = "", PRSG = "", pcode = "";
            if (cENYN.Text.Trim() != "")
            {
                ENYN = cENYN.Value.ToString();
            }
            if (cPRSG.Text.Trim() != "")
            {
                PRSG = cPRSG.Value.ToString();
            }
            if (txtPCode.Text.Trim() != "")
            {
                pcode = txtPCode.Value.ToString();
            }
            string DelSql = "delete from NAMEPLATE_SO where BZSO='" + SO + "' and pline_code= '" + pcode + "'";
            dc.ExeSql(DelSql);
            string Sql = "INSERT INTO NAMEPLATE_SO(PLINE_CODE,JX,BZSO,GL,QFJQ,QFPQ,PL,XL,DS,FHCX,XNBH,DYJX,ZS,KHH,GD,PRSG,YH,JZL,PYZS,EDGYL,HB,ENYN,GL1,ZS1,BYGL1,BYZS1,BYGL2,BYZS2,NOX,PM,KHGG,PEL,EPA,PFJD,PFJDHZH,FR,MPLJH,XZJD,DYGLD,ZDJGL,ZXBZ,XZMC,CZYG,MODIFYDATE,XSHZHHMLY,HCLZZLX,SCXKZ)  "
                 + "VALUES( '" + pcode + "', '" + JX + "','" + SO + "','" + GL + "','" + QFJQ + "','" + QFPQ + "','" + PL + "','" + XL + "', '" + DS + "', '" + FHCX + "','" + XNBH + "','" + DYJX + "','" + ZS + "','" + KHH + "','" + GD + "','" + PRSG + "','" + theUserName + "' ,"
                 + "'" + JZL + "', '" + PYZS + "','" + EDGYL + "','" + HB + "','" + ENYN + "','" + GL1 + "','" + ZS1 + "','" + BYGL1 + "', '" + BYZS1 + "', '" + BYGL2 + "', '" + BYZS2 + "','" + NOX + "','" + PM + "','" + KHGG + "','" + PEL + "','" + EPA + "','" + PFJD + "','" + PFJDHZH + "' ,"
                 + " '" + FR + "', '" + MPLJH + "','" + XZJD + "','" + DYGLD + "','" + ZDJGL + "','" + ZXBZ + "','" + XZMC + "','" + theUserName + "', sysdate, '" + XSHZHHMLY + "','" + HCLZZLX + "','" + SCXKZ + "')";

            dc.ExeSql(Sql);

            e.Cancel = true;
            ASPxGridView1.CancelEdit();
            setCondition();

        }
        protected void ASPxGridView1_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditFormEventArgs e)
        {
            if (ASPxGridView1.IsNewRowEditing)
            {
                (ASPxGridView1.FindEditFormTemplateControl("txtPRSG") as ASPxComboBox).SelectedIndex = 0;
                (ASPxGridView1.FindEditFormTemplateControl("txtENYN") as ASPxComboBox).SelectedIndex = 0;
            }
            if (!ASPxGridView1.IsNewRowEditing && ASPxGridView1.IsEditing)
            {
                 //主键不可以修改
                (ASPxGridView1.FindEditFormTemplateControl("txtSO") as ASPxTextBox).Enabled = false;
            }
            
        }

        protected void ASPxGridView1_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {

            if (ASPxGridView1.IsNewRowEditing)
            {
                //判断是否重复
                try
                {
                    string chSql = "select BZSO from NAMEPLATE_SO where bzso='" + e.NewValues["BZSO"].ToString() + "' and pline_code='" + txtPCode.Value.ToString() + "'";

                    DataTable dt = dc.GetTable(chSql);
                    if (dt.Rows.Count > 0)
                    {
                        e.RowError = "当前记录已经存在!如果不正确,请进行修改操作";
                    }
                }
                catch { }
            }
 

        }



    }
}
