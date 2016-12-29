using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rmes.Web.Base;
using System.Data;
using Rmes.Pub.Data;
using PetaPoco;
using Rmes.DA.Base;
using DevExpress.Web.ASPxClasses;
using Rmes.DA.Factory;
using Rmes.DA.Entity;
using System.Data.OleDb;
using System.IO;
using DevExpress.Web.ASPxGridView;
using DevExpress.Utils;
using DevExpress.Web.ASPxEditors;
using Rmes.DA.Procedures;
/**
 * 2016-08-30
 * 游晓航
 * 临时措施替换
 * */

namespace Rmes.WebApp.Rmes.Inv.inv9500
{
    public partial class inv9500 : BasePage
    {
        private dataConn dc = new dataConn();
        private PubCs thePubCs = new PubCs();

        private static string theCompanyCode, theUserId, theUserName;
        public string programcode, MachineName;
        public Database db = DB.GetInstance();
        DataTable DT = new DataTable();
        DataColumn DC = new DataColumn("PT_PART");
        DataTable DT2 = new DataTable();
        DataColumn DC2 = new DataColumn("PT_PART");
        protected void Page_Load(object sender, EventArgs e)
        {
            userManager theUserManager = (userManager)Session["theUserManager"];
            theCompanyCode = theUserManager.getCompanyCode();
            theUserId = theUserManager.getUserId();
            theUserName = theUserManager.getUserName();
            //获取MachineName
            MachineName = System.Net.Dns.GetHostName();
            programcode = "inv9500";
           
            //ASPxListBoxPart1.Attributes.Add("ondblclick", "ListBoxDblClick(this);");
            if (!IsPostBack)
            { 

            }
            if (Request["opFlag"] == "getEditSeries")
            {
                string str1 = "";
                //string plancode = Request["PLANCODE"].ToString();
                //string item = Request["ITEM"].ToString();
                //string location = Request["LOCATION"].ToString();
                //string process = Request["PROCESS"].ToString();
                string plancode = Request["PLANCODE"].ToString().Trim();
                string item = Request["ITEM"].ToString().Trim();
                string location = Request["LOCATION"].ToString().Trim();
                string process = Request["PROCESS"].ToString().Trim();
                    string sql1 = "select * from data_plan_standard_bom where ITEM_CODE='" + item + "'  and PLAN_CODE='" + plancode + "'";
                    DataTable dt1 = dc.GetTable(sql1);
                    if (dt1.Rows.Count <= 0)
                    {
                        str1 = "Fail,该零件在计划BOM中不存在！";
                        
                    }
                    else
                    {
                        string sql2 = "select * from data_plan_standard_bom where LOCATION_CODE='" + location + "'  and PLAN_CODE='" + plancode + "'";
                        DataTable dt2 = dc.GetTable(sql2);
                        if (dt2.Rows.Count <= 0)
                        {
                            str1 = "Fail,工位有误！";
                           
                        }
                        else
                        {
                            string sql3 = "select * from data_plan_standard_bom where PROCESS_CODE='" + process + "'  and PLAN_CODE='" + plancode + "'";
                            DataTable dt3 = dc.GetTable(sql3);
                            if (dt3.Rows.Count <= 0)
                            {
                                str1 = "Fail,工序有误！";
                             
                            }
                            else
                            {
                                string sql4 = "select ljdm2 from sjbomsothmuti where ljdm2='" + item + "' and istrue='1' and jhdm='" + plancode + "'";
                                DataTable dt4 = dc.GetTable(sql4);
                                if (dt4.Rows.Count > 0)
                                {
                                    str1 = "Fail,该零件为多对多替换的替换件！";
                                  
                                }
                                else
                                {

                                    string sql5 = "select ljdm1 from sjbomsothmuti where ljdm2='" + item + "' and istrue='1' and jhdm='" + plancode + "'";
                                    DataTable dt5 = dc.GetTable(sql5);
                                    if (dt5.Rows.Count > 0)
                                    {
                                        str1 = "Fail,该零件在多对多替换中已被替换！";
                                      
                                    }
                                    else
                                    {
                                        string sql6 = "select ljdm1 from sjbomsoth where ljdm2='" + item + "' and istrue='1' and jhdm='" + plancode + "'";
                                        DataTable dt6 = dc.GetTable(sql6);
                                        if (dt6.Rows.Count > 0)
                                        {
                                            str1 = "Fail,该零件在一对一替换中已被替换！";

                                        }
                                        else { str1 = "OK,成功！"; }
                                    }
                                }
                            }
                        }
                    }
               
                
                this.Response.Write(str1);
                this.Response.End();
            }
            if (Request["opFlag"] == "getEditSeries2")
            {
                string str1 = "";
                string plancode = Request["PLANCODE"].ToString().Trim();
                string item = Request["ITEM"].ToString().Trim();
                string location = Request["LOCATION"].ToString().Trim();
                string process = Request["PROCESS"].ToString().Trim();

                string sql1 = "select * from COPY_PT_MSTR where PT_PART='" + item + "'";
                DataTable dt1 = dc.GetTable(sql1);
                if (dt1.Rows.Count <= 0)
                {
                    str1 = "Fail,零件号不存在！";

                }
                else { str1 = "OK,成功！"; }
                this.Response.Write(str1);
                this.Response.End();
            }

        }
      
        //protected void listBoxPART2_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        //{
        //    string[] param = e.Parameter.Split(',');
        //    string plancode = param[0];
        //    string item = param[1];
        //    string location = param[2];
        //    string process = param[3];
        //    string sl = param[4];
        //    string sql1 = "select * from COPY_PT_MSTR where PT_PART='" + item + "'";
        //    DataTable dt1 = dc.GetTable(sql1);
        //    if (dt1.Rows.Count <= 0)
        //    {
        //        ASPxListBoxPart2.JSProperties.Add("cpCallbackName", "Fail");
        //        ASPxListBoxPart2.JSProperties.Add("cpCallbackRet", "零件号不存在！");
        //        return;
        //    }


        //    string txt1 = item + "; " + location + "; " + process + "; " + sl;

        //    //DataTable dt = new DataTable();
        //    //DataColumn dc1 = new DataColumn("PT_PART");
        //    //DT2.Columns.Add(DC2);
        //    //DT2.Rows.Add(txt1);

        //    //ASPxListBoxPart2.DataSource = DT2;
        //    //ASPxListBoxPart2.DataBind();
        //}

        protected void ASPxCbSubmit_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
        {
            // string collection = e.Parameter;
           string[] param = e.Parameter.Split(',');
           try
           {
               if (param[0] == "Save")
               {

                   string plancode = param[1];
                   string dh = param[2];

                   int count = 0, count1 = 0, count2 = 0;

                   string sql1 = "select PLINE_CODE,PLAN_SO from DATA_PLAN where PLAN_CODE='" + plancode + "'";
                   DataTable dt1 = dc.GetTable(sql1);
                   if (dt1.Rows.Count <= 0) { e.Result = "Fail,计划不存在！"; }
                   else
                   {
                       string GZDD = dt1.Rows[0][0].ToString();
                       string SO = dt1.Rows[0][1].ToString();
                       string Insql = " insert into sjbomsothxc(SO,LJDM1,LJDM2,RQSJ,YGMC,ZDMC,JHDM,LRSJ,ISTRUE,QRYGMC,GWMC,BZ,GZDD,GWMC1,THGROUP,SL,GXMC1,OPFLAG) "
                                     + "select SO,LJDM1,LJDM2,sysdate,'" + theUserName + "','" + MachineName + "',JHDM,sysdate,ISTRUE,QRYGMC,GWMC,BZ,GZDD,GWMC1,THGROUP,SL,GXMC1,'DEL' "
                                     + " from sjbomsothmuti where THGROUP='" + dh + "' and JHDM='" + plancode + "'";
                       dc.ExeSql(Insql);
                       //先删除同计划同单号的信息
                       string Delsql = "delete from sjbomsothmuti where jhdm='" + plancode + "' and thgroup='" + dh + "'";
                       dc.ExeSql(Delsql);
                       count1 = ASPxListBoxPart1.SelectedItems.Count;
                       count2 = ASPxListBoxPart2.SelectedItems.Count;
                       if (count1 > count2) { count = count1; }
                       else { count = count2; }
                       string Ljdm1 = "", Gwmc = "", ljdm2 = "", Gwmc2 = "", GXMC2 = "", sl2 = "";
                       for (int i = 0; i < count; i++)
                       {
                           
                           if (i < count1)
                           {
                               string Opart = ASPxListBoxPart1.SelectedItems[i].ToString();
                               string[] Opart1 = Opart.Split(',');
                               Ljdm1 = Opart1[0];
                               Gwmc = Opart1[1];
                           }

                           if (i < count2)
                           {
                               string Rpart = ASPxListBoxPart2.SelectedItems[i].ToString();
                               string[] Rpart1 = Rpart.Split(',');
                               ljdm2 = Rpart1[0];
                               Gwmc2 = Rpart1[1];
                               GXMC2 = Rpart1[2];
                               sl2 = Rpart1[3];
                           }

                           string Insql2 = "insert into sjbomsothmuti(SO,LJDM1,LJDM2,RQSJ,YGMC,ZDMC,JHDM,LRSJ,ISTRUE,QRYGMC,GWMC,BZ,GZDD,GWMC1,THGROUP,SL,GXMC1) "
                                     + "values('" + SO + "','" + Ljdm1 + "','" + ljdm2 + "',sysdate,'" + theUserName + "','" + MachineName + "','" + plancode + "',sysdate,1,'','" + Gwmc + "','','" + GZDD + "','" + Gwmc2 + "','" + dh + "','" + sl2 + "','" + GXMC2 + "')";

                           dc.ExeSql(Insql2);
                           string Insql3 = "insert into sjbomsothxc(SO,LJDM1,LJDM2,RQSJ,YGMC,ZDMC,JHDM,LRSJ,ISTRUE,QRYGMC,GWMC,BZ,GZDD,GWMC1,THGROUP,SL,GXMC1,OPFLAG) "
                                         + "values('" + SO + "','" + Ljdm1 + "','" + ljdm2 + "',sysdate,'" + theUserName + "','" + MachineName + "','" + plancode + "',sysdate,1,'','" + Gwmc + "','','" + GZDD + "','" + Gwmc2 + "','" + dh + "','" + sl2 + "','" + GXMC2 + "','ADD')";
                           dc.ExeSql(Insql3);

                       }

                       e.Result = "OK,替换关系保存成功！";
                   }


               }
               if (param[0] == "Delete")
               {
                   string plancode = param[1];
                   string dh = param[2];
                   string Insql4 = "insert into sjbomsothxc (select SO,LJDM1,LJDM2,sysdate,'" + theUserName + "','" + MachineName + "',JHDM,sysdate,ISTRUE,QRYGMC,GWMC,BZ,GZDD,GWMC1,THGROUP,SL,GXMC1,'DEL' from sjbomsothmuti where THGROUP='" + dh + "' and JHDM='" + plancode + "')";
                   dc.ExeSql(Insql4);
                   string Delsql = "delete from sjbomsothmuti where jhdm='" + plancode + "' and thgroup='" + dh + "'";
                   dc.ExeSql(Delsql);
                   e.Result = "OK,删除成功！";

               }
           }

           catch (Exception e1)
           {
               e.Result = "Fail,替换关系保存失败！";
               return;
           }
        }
        public void ASPxGridView1_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string ThisSO = "", PlineCode = "", StationCode = "";
            string Chsql2 = "SELECT PLAN_SO,PLINE_CODE FROM DATA_PLAN WHERE PLAN_CODE='" + ASPxTextPlanCode.Text.Trim() + "'";
            DataTable Chdt2 = dc.GetTable(Chsql2);
            if (Chdt2.Rows.Count <= 0)
            {

                ASPxGridView1.JSProperties.Add("cpCallbackName", "Fail");
                ASPxGridView1.JSProperties.Add("cpCallbackRet", "计划号不存在！");
                 
                return;
            }
            else
            {
                ThisSO = Chdt2.Rows[0][0].ToString();
                PlineCode = Chdt2.Rows[0][1].ToString();
            }
            string Chsql3 = "SELECT STATION_CODE FROM CODE_STATION WHERE STATION_NAME='" + ASPxTextSation.Text.Trim() + "'";
            DataTable Chdt3 = dc.GetTable(Chsql3);
            if (Chdt3.Rows.Count <= 0)
            {

                ASPxGridView1.JSProperties.Add("cpCallbackName", "Fail");
                ASPxGridView1.JSProperties.Add("cpCallbackRet", "站点名称不存在！");
                return;
            }
            else
            {
                StationCode = Chdt3.Rows[0][0].ToString();

            }
            ProductDataFactory.PL_QUERY_BOMZJTS(ThisSO, StationCode, PlineCode, "", ASPxTextPlanCode.Text.Trim(), "");
            ProductDataFactory.PL_UPDATE_BOMZJTS(ThisSO, StationCode, ASPxTextPlanCode.Text.Trim(), PlineCode, "");

            string Delsql = "delete from RSTBOMQD_XC ";
            dc.ExeSql(Delsql);
            string Insql = "INSERT INTO RSTBOMQD_XC (SELECT '" + ASPxTextPlanCode.Text.Trim() + "',GWMC,COMP,UDESC,QTY,GXMC,ZDMC,ZDDM,GYSMC from RSTBOMQD)";
            dc.ExeSql(Insql);

            string sql = "SELECT GWMC,GXMC,COMP,QTY FROM rstbomqd_xc where zddm='" + StationCode + "' and jhmc='" + ASPxTextPlanCode.Text.Trim() + "' order by gwmc,gxmc";
            DataTable dt = dc.GetTable(sql);

            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();



        }
    }
}