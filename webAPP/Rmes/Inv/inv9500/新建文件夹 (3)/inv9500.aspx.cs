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
                Session["inv9500sql"] = "";
                //Session["inv9501sql"] = "";

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
                string location = Request["LOCATION"].ToString().ToUpper().Trim();
                string process = Request["PROCESS"].ToString().Trim();
                string ysl = Request["YSL"].ToString().Trim();
                //string ljh = item.Substring(1, item.Length - 1);
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
                                    else
                                    {
                                        string sql7 = "select QTY from rstbomqd_xc where comp='" + item + "'   and jhmc='" + plancode + "'";

                                        string strsl = dc.GetValue(sql7);
                                        if (strsl == "" || ysl == "")
                                        {
                                            str1 = "Fail,替换时原零件数量为空！";
                                            
                                        }
                                        else
                                        {
                                            int sl = Convert.ToInt32(strsl);
                                            int sl2 = Convert.ToInt32(ysl);
                                            if (sl2 > sl)
                                            {
                                                str1 = "Fail,替换时原零件数量不能大于该零件的当前数量！";
                                            }
                                            else { str1 = "OK,成功！"; }
                                        }
                                    }
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
                string location = Request["LOCATION"].ToString().ToUpper().Trim();
                string process = Request["PROCESS"].ToString().Trim();


                string sql1 = "select * from COPY_PT_MSTR where PT_PART='" + item + "'";
                DataTable dt1 = dc.GetTable(sql1);
                if (dt1.Rows.Count <= 0)
                {
                    str1 = "Fail,零件号不存在！";

                }
                else
                {
                    string sql2 = "select * from data_plan_standard_bom where LOCATION_CODE='" + location + "'  and PLAN_CODE='" + plancode + "'";
                    DataTable dt2 = dc.GetTable(sql2);
                    if (dt2.Rows.Count <= 0)
                    {
                        str1 = "Fail,替换工位有误！";

                    }
                    else
                    {
                        string sql3 = "select * from data_plan_standard_bom where PROCESS_CODE='" + process + "'  and PLAN_CODE='" + plancode + "'";
                        DataTable dt3 = dc.GetTable(sql3);
                        if (dt3.Rows.Count <= 0)
                        {
                            str1 = "Fail,替换工序有误！";

                        }

                        else { str1 = "OK,成功！"; }

                    }
                }


                this.Response.Write(str1);
                this.Response.End();
            }
            ASPxListBoxPart1.ClientSideEvents.ItemDoubleClick = "function(s,e) {var index = listBoxPART1.GetSelectedIndex();if(index!=-1) listBoxPART1.RemoveItem(index);}";
            ASPxListBoxPart2.ClientSideEvents.ItemDoubleClick = "function(s,e) {var index = listBoxPART2.GetSelectedIndex();if(index!=-1) listBoxPART2.RemoveItem(index);}";
            if (Session["inv9500sql"] as string != "")
            {
                DataTable dt = dc.GetTable(Session["inv9500sql"] as string);
                ASPxGridView1.DataSource = dt;
                ASPxGridView1.DataBind();
            }
            //if (Session["inv9501sql"] as string != "")
            //{
            //    DataTable dt2 = dc.GetTable(Session["inv9501sql"] as string);
            //    ASPxGridView2.DataSource = dt2;
            //    ASPxGridView2.DataBind();
            //}
            SetCondition();
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
                    string testdh = "";
                    if (txtDH.Text.Trim() != "")
                    {
                        testdh = dh.Substring(0, 2);
                    }
                    if (testdh.ToUpper() == "MT")
                    {
                        e.Result = "Fail,单号不能以“MT”开头！";
                        return;
                    }
                    int count = 0, count1 = 0, count2 = 0;


                    string sql1 = "select offline_qty,PLINE_CODE,PLAN_SO from DATA_PLAN where PLAN_CODE='" + plancode + "'";
                    DataTable dt1 = dc.GetTable(sql1);
                    if (dt1.Rows.Count <= 0) { e.Result = "Fail,计划不存在！"; }


                    else
                    {

                        //该计划序中只要有一台发动机下线，就不能再进行现场零件替换
                        string offline_qty = dt1.Rows[0][0].ToString();

                        if (Convert.ToInt32(offline_qty) > 0)
                        {
                            e.Result = "Fail,该计划序有发动机已下线，不能保存替换关系！";
                            return;
                        }
                        string GZDD = dt1.Rows[0][0].ToString();
                        string SO = dt1.Rows[0][1].ToString();
                        string Insql = " insert into sjbomsothxc(SO,LJDM1,LJDM2,RQSJ,YGMC,ZDMC,JHDM,LRSJ,ISTRUE,QRYGMC,GWMC,BZ,GZDD,GWMC1,THGROUP,SL,GXMC1,OPFLAG) "
                                      + "select SO,LJDM1,LJDM2,sysdate,'" + theUserName + "','" + MachineName + "',JHDM,sysdate,ISTRUE,QRYGMC,GWMC,BZ,GZDD,GWMC1,THGROUP,SL,GXMC1,'DEL' "
                                      + " from sjbomsothmuti where THGROUP='" + dh + "' and JHDM='" + plancode + "'";
                        dc.ExeSql(Insql);
                        //先删除同计划同单号的信息
                        string Delsql = "delete from sjbomsothmuti where jhdm='" + plancode + "' and thgroup='" + dh + "'";
                        dc.ExeSql(Delsql);
                        count1 = ASPxListBoxPart1.Items.Count;
                        count2 = ASPxListBoxPart2.Items.Count;
                        if (count1 > count2) { count = count1; }
                        else { count = count2; }
                        string Ljdm1 = "", Gwmc = "", ljdm2 = "", Gwmc2 = "", GXMC2 = "", sl2 = "", Gxmc = "", sl1 = "", sl = "";
                        for (int i = 0; i < count; i++)
                        {

                            if (i < count1)//获取要替换的原零件信息：零件号、工位、工序、数量
                            {
                                string Opart = ASPxListBoxPart1.Items[i].ToString();
                                string[] Opart1 = Opart.Split(',');
                                Ljdm1 = Opart1[0];
                                Gwmc = Opart1[1];
                                Gxmc = Opart1[2];
                                sl1 = Opart1[3];
                            }

                            if (i < count2)//获取要替换的替换件信息：零件号、工位、工序、数量
                            {
                                string Rpart = ASPxListBoxPart2.Items[i].ToString();
                                string[] Rpart1 = Rpart.Split(',');
                                ljdm2 = Rpart1[0];
                                Gwmc2 = Rpart1[1];
                                GXMC2 = Rpart1[2];
                                sl2 = Rpart1[3];
                            }
                            //判断要替换的原零件数量是否大于原来最大数量
                            string chsql2 = "SELECT  QTY FROM rstbomqd_xc where comp='" + Ljdm1 + "' and gwmc='" + Gwmc + "' and gxmc='" + Gxmc + "' ";
                            DataTable chdt2 = dc.GetTable(chsql2);
                            if (chdt2.Rows.Count > 0)
                            {
                                sl = chdt2.Rows[0][0].ToString();

                            }

                            if (Convert.ToInt32(sl) < Convert.ToInt32(sl1))
                            {
                                e.Result = "Fail,要替换的原零件数量不能大于零件最大数量！";
                                return;
                            }

                            //修改原来的数量改哪个表？
                            // string Updsql1 = "update RSTBOMQD set QTY=QTY-'" + sl1 + "' where ";
                            string Insql2 = "insert into sjbomsothmuti(SO,LJDM1,LJDM2,RQSJ,YGMC,ZDMC,JHDM,LRSJ,ISTRUE,QRYGMC,GWMC,BZ,GZDD,GWMC1,THGROUP,SL,GXMC1,LJSL1,GXMC) "
                                      + "values('" + SO + "','" + Ljdm1 + "','" + ljdm2 + "',sysdate,'" + theUserName + "','" + MachineName + "','" + plancode + "',sysdate,1,'','" + Gwmc + "','','" + GZDD + "','" + Gwmc2 + "','" + dh + "','" + sl2 + "','" + GXMC2 + "','" + sl1 + "','" + Gxmc + "')";

                            dc.ExeSql(Insql2);
                            //保存替换关系到多对多替换表中
                            string Insql3 = "insert into sjbomsothxc(SO,LJDM1,LJDM2,RQSJ,YGMC,ZDMC,JHDM,LRSJ,ISTRUE,QRYGMC,GWMC,BZ,GZDD,GWMC1,THGROUP,SL,GXMC1,OPFLAG,LJSL1,GXMC) "
                                          + "values('" + SO + "','" + Ljdm1 + "','" + ljdm2 + "',sysdate,'" + theUserName + "','" + MachineName + "','" + plancode + "',sysdate,1,'','" + Gwmc + "','','" + GZDD + "','" + Gwmc2 + "','" + dh + "','" + sl2 + "','" + GXMC2 + "','ADD','" + sl1 + "','" + Gxmc + "')";
                            dc.ExeSql(Insql3);

                        }

                        e.Result = "OK,替换关系保存成功！";
                    }


                }
                if (param[0] == "Delete")
                {
                    string plancode = param[1];
                    string dh = param[2];
                    string testdh = "";
                    if (txtDH.Text.Trim() != "")
                    {
                        testdh = dh.Substring(0, 2);
                    }
                    if (testdh.ToUpper() == "MT")
                    {
                        e.Result = "Fail,单号不能以“MT”开头！";
                        return;
                    }
                    string Insql4 = "insert into sjbomsothxc (select SO,LJDM1,LJDM2,sysdate,'" + theUserName + "','" + MachineName + "',JHDM,sysdate,ISTRUE,QRYGMC,GWMC,BZ,GZDD,GWMC1,THGROUP,SL,GXMC1,'DEL',LJSL1,GXMC from sjbomsothmuti where THGROUP='" + dh + "' and JHDM='" + plancode + "')";
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
        public void ASPxListBoxPart1_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string dh = txtDH.Text;
            string testdh = "";
            if (txtDH.Text.Trim() != "")
            {
                testdh = dh.Substring(0, 2);
            }
            if (testdh.ToUpper() == "MT")
            {
                ASPxListBoxPart1.JSProperties.Add("cpCallbackName", "Fail");
                ASPxListBoxPart1.JSProperties.Add("cpCallbackRet", "单号不能以“MT”开头！");

                return;
            }
            string sql1 = "select ljdm1||','||gwmc||','||gxmc||','||ljsl1 as PT_PART from sjbomsothmuti where JHDM='" + txtPlanCode.Text.Trim() + "' and THGROUP='" + txtDH.Text.Trim() + "' and ljdm1 is not null";
            DataTable dt1 = dc.GetTable(sql1);

            ASPxListBoxPart1.DataSource = dt1;
            ASPxListBoxPart1.DataBind();
            //string sql2 = "select ljdm2||','||gwmc1||','||gxmc1||','||sl   as PT_PART from sjbomsothmuti where JHDM='" + txtPlanCode.Text.Trim() + "' and THGROUP='" + txtDH.Text.Trim() + "' and ljdm2 is not null";
            //DataTable dt2 = dc.GetTable(sql2);

            //ASPxListBoxPart2.DataSource = dt2;
            //ASPxListBoxPart2.DataBind();

        }
        public void ASPxListBoxPart2_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string dh = txtDH.Text;
            string testdh = "";
            if (txtDH.Text.Trim() != "")
            {
                testdh = dh.Substring(0, 2);
            }
            if (testdh.ToUpper() == "MT")
            {
                //ASPxListBoxPart2.JSProperties.Add("cpCallbackName", "Fail");
                //ASPxListBoxPart2.JSProperties.Add("cpCallbackRet", "单号不能以“MT”开头！");

                return;
            }
            string sql2 = "select ljdm2||','||gwmc1||','||gxmc1||','||sl   as PT_PART from sjbomsothmuti where JHDM='" + txtPlanCode.Text.Trim() + "' and THGROUP='" + txtDH.Text.Trim() + "' and ljdm2 is not null";
            DataTable dt2 = dc.GetTable(sql2);

            ASPxListBoxPart2.DataSource = dt2;
            ASPxListBoxPart2.DataBind();

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
            string Chsql3 = "SELECT STATION_CODE FROM CODE_STATION WHERE STATION_NAME='" + ASPxTextSation.Text.ToUpper().Trim() + "'";
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

            string sql = "SELECT GWMC,GXMC,COMP,QTY,GYSMC FROM rstbomqd_xc where zddm='" + StationCode + "' and jhmc='" + ASPxTextPlanCode.Text.Trim() + "' order by gwmc,gxmc";
            DataTable dt = dc.GetTable(sql);
            Session["inv9500sql"] = sql;
            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();



        }
        public void ComboItem_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            ASPxComboBox asp = (ASPxComboBox)sender;
            string StationCode = "";
            string Chsql3 = "SELECT STATION_CODE FROM CODE_STATION WHERE STATION_NAME='" + ASPxTextSation.Text.ToUpper().Trim() + "'";
            DataTable Chdt3 = dc.GetTable(Chsql3);
            if (Chdt3.Rows.Count <= 0)
            {
                return;
            }
            else
            {
                StationCode = Chdt3.Rows[0][0].ToString();
            }
            string sql = "SELECT distinct COMP FROM rstbomqd_xc where zddm='" + StationCode + "' order by comp ";
            sqlItem.SelectCommand = sql;
            sqlItem.DataBind();
            asp.DataBind();
        }
        public void ComboLocation_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            ASPxComboBox asp = (ASPxComboBox)sender;

            string StationCode = "", sql = "";
            string Chsql3 = "SELECT STATION_CODE FROM CODE_STATION WHERE STATION_NAME='" + ASPxTextSation.Text.ToUpper().Trim() + "'";
            DataTable Chdt3 = dc.GetTable(Chsql3);
            if (Chdt3.Rows.Count <= 0)
            {
                return;
            }
            else
            {
                StationCode = Chdt3.Rows[0][0].ToString();

            }
            string item = e.Parameter;


            if (item != "")
            {
                sql = "SELECT distinct GWMC  FROM rstbomqd_xc where zddm='" + StationCode + "' and comp='" + item + "' ";
            }


            //else if (ComboItem.SelectedItem.Text != "")
            //{
            //    string sql2 = "SELECT distinct COMP FROM rstbomqd_xc where zddm='" + StationCode + "'oder by comp ";
            //    DataTable dt2 = dc.GetTable(sql2);
            //    string comp = dt2.Rows[0][0].ToString();
            //    sql = "SELECT distinct GWMC  FROM rstbomqd_xc where zddm='" + StationCode + "' and comp='" + ComboItem.SelectedItem.Value.ToString() + "' ";
            //}
            else
            {
                string sql2 = "SELECT distinct COMP FROM rstbomqd_xc where zddm='" + StationCode + "'order by comp ";
                DataTable dt2 = dc.GetTable(sql2);
                //string comp = dt2.Rows[0][0].ToString();
                string comp = "";
                try
                {
                    comp = dt2.Rows[0][0].ToString();
                }
                catch { }

                sql = "SELECT distinct GWMC  FROM rstbomqd_xc where zddm='" + StationCode + "' and comp='" + comp + "'order by gwmc ";

            }
            sqlItem2.SelectCommand = sql;
            sqlItem2.DataBind();
            asp.DataBind();

        }
        public void ComboProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            ASPxComboBox asp = (ASPxComboBox)sender;
            string StationCode = "", sql = "", item = "", location = "";
            string Chsql3 = "SELECT  STATION_CODE FROM CODE_STATION WHERE STATION_NAME='" + ASPxTextSation.Text.ToUpper().Trim() + "'";
            DataTable Chdt3 = dc.GetTable(Chsql3);
            if (Chdt3.Rows.Count <= 0)
            {
                return;
            }
            else
            {
                StationCode = Chdt3.Rows[0][0].ToString();

            }
            string[] param = e.Parameter.Split(',');
            item = param[0];

            location = param[1];
            if (item != "null" && location == "###")
            {
                sql = "SELECT distinct GXMC FROM rstbomqd_xc where zddm='" + StationCode + "' and comp='" + item + "' ";
            }
            else if (item != "null" && location != "###")
            {

                sql = "SELECT distinct GXMC FROM rstbomqd_xc where zddm='" + StationCode + "' and comp='" + item + "' and gwmc='" + location + "' ";
            }

            else
            {
                string sql2 = "SELECT distinct COMP FROM rstbomqd_xc where zddm='" + StationCode + "'order by comp ";
                DataTable dt2 = dc.GetTable(sql2);
                string comp = "";
                try
                {
                    comp = dt2.Rows[0][0].ToString();
                }
                catch { }
                string sql3 = "SELECT distinct GWMC  FROM rstbomqd_xc where zddm='" + StationCode + "' and comp='" + comp + "'order by gwmc";
                DataTable dt3 = dc.GetTable(sql3);
                string gwmc = "";
                try
                {
                    gwmc = dt3.Rows[0][0].ToString();
                }
                catch
                { }
                sql = "SELECT distinct GXMC FROM rstbomqd_xc where zddm='" + StationCode + "' and comp='" + comp + "' and gwmc='" + gwmc + "'";
            }
            //else
            //{
            //    sql = "SELECT distinct GXMC FROM rstbomqd_xc where zddm='" + StationCode + "'  ";
            //}
            sqlItem3.SelectCommand = sql;
            sqlItem3.DataBind();
            asp.DataBind();

        }
        public void ASPxGridView2_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
        //    string Chsql2 = "SELECT PLAN_SO,PLINE_CODE FROM DATA_PLAN WHERE PLAN_CODE='" + ASPxTextPlanCode.Text.Trim() + "'";
        //    DataTable Chdt2 = dc.GetTable(Chsql2);
        //    if (Chdt2.Rows.Count <= 0)
        //    {

        //        return;
        //    }
        //    else
        //    {

        //        string Chsql3 = "SELECT STATION_CODE FROM CODE_STATION WHERE STATION_NAME='" + ASPxTextSation.Text.ToUpper().Trim() + "'";
        //        DataTable Chdt3 = dc.GetTable(Chsql3);
        //        if (Chdt3.Rows.Count <= 0)
        //        {
        //            return;
        //        }

        //    }

        //    string sql2 = "SELECT JHDM,PART,GXDM,CZTS,WJPATH,TSTYPE FROM RST_ATPU_ZJTS where JHDM='" + ASPxTextPlanCode.Text.Trim() + "' AND GXDM IN (select a.process_code  from rel_location_process a ,"
        //    + "rel_station_location b,code_station c where c.station_code=b.station_code and b.location_code=a.location_code and c.station_name='" + ASPxTextSation.Text.ToUpper().Trim() + "')";
        //    DataTable dt2 = dc.GetTable(sql2);
        //    Session["inv9501sql"] = sql2;
        //    ASPxGridView2.DataSource = dt2;
        //    ASPxGridView2.DataBind();

            SetCondition();

        }
        private void SetCondition()
        {
            string Chsql2 = "SELECT PLAN_SO,PLINE_CODE FROM DATA_PLAN WHERE PLAN_CODE='" + ASPxTextPlanCode.Text.Trim() + "'";
            DataTable Chdt2 = dc.GetTable(Chsql2);
            if (Chdt2.Rows.Count <= 0)
            {

            }
            else
            {

                string Chsql3 = "SELECT STATION_CODE FROM CODE_STATION WHERE STATION_NAME='" + ASPxTextSation.Text.ToUpper().Trim() + "'";
                DataTable Chdt3 = dc.GetTable(Chsql3);
                if (Chdt3.Rows.Count <= 0)
                {
                    
                }

            }

            string sql2 = "SELECT JHDM,PART,GXDM,CZTS,WJPATH,TSTYPE FROM RST_ATPU_ZJTS where JHDM='" + ASPxTextPlanCode.Text.Trim() + "' AND GXDM IN (select a.process_code  from rel_location_process a ,"
            + "rel_station_location b,code_station c where c.station_code=b.station_code and b.location_code=a.location_code and c.station_name='" + ASPxTextSation.Text.ToUpper().Trim() + "')";
            DataTable dt2 = dc.GetTable(sql2);
            //Session["inv9501sql"] = sql2;
            ASPxGridView2.DataSource = dt2;
            ASPxGridView2.DataBind();
        }
        protected void ASPxGridView2_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            

            //判断当前记录是否可以删除
            string strDelCode = e.Values["PART"].ToString();
            string strCzts = e.Values["CZTS"].ToString();
            string strGxdm = e.Values["GXDM"].ToString();
            string strPlancode = e.Values["JHDM"].ToString();
            string strType = e.Values["TSTYPE"].ToString();
            string strTableName = "RST_ATPU_ZJTS";

            dataConn theDataConn = new dataConn("select func_check_delete_data('" + strTableName + "','" + theCompanyCode + "','MES','MES','MES','" + strCzts + "') from dual");

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
                string Lsql = "insert into RST_ATPU_ZJTS_LOG(PART,CZTS,GXDM,GZDD,JHDM,JHSO,TSTYPE,RQSJ,WJPATH,NOTE_COLOR,NOTE_FONT,EDIT_USER,EDIT_FLAG,EDIT_DATE)select PART,CZTS,GXDM,GZDD,JHDM,JHSO,TSTYPE,RQSJ,WJPATH,NOTE_COLOR,NOTE_FONT, "
                             + "'" + theUserName + "','DEL',SYSDATE FROM RST_ATPU_ZJTS WHERE PART = '" + strDelCode + "' and CZTS='" + strCzts + "' and GXDM='" + strGxdm + "' and JHDM='" + strPlancode + "' and TSTYPE='" + strType + "'";
                dc.ExeSql(Lsql);
                string Sql = "delete from RST_ATPU_ZJTS WHERE PART = '" + strDelCode + "' and CZTS='" + strCzts + "' and GXDM='" + strGxdm + "' and JHDM='" + strPlancode + "' and TSTYPE='" + strType + "'";
                dc.ExeSql(Sql);
            }


            //if (Session["inv9501sql"] as string != "")
            //{
            //    DataTable dt2 = dc.GetTable(Session["inv9501sql"] as string);
            //    ASPxGridView2.DataSource = dt2;
            //    ASPxGridView2.DataBind();
            //}
            SetCondition();
            e.Cancel = true;
        }


        protected void ASPxGridView2_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            ASPxComboBox uGxdm = ASPxGridView2.FindEditFormTemplateControl("txtGxdm") as ASPxComboBox;
            ASPxTextBox uPath = ASPxGridView2.FindEditFormTemplateControl("txtPath") as ASPxTextBox;
            ASPxTextBox uCzts = ASPxGridView2.FindEditFormTemplateControl("txtCzts") as ASPxTextBox;
            string gxdm = uGxdm.Text.Trim();
            string path = uPath.Text.Trim();
            string czts = uCzts.Text.Trim();

            string Gsql = "select plan_so,pline_code from data_plan where plan_code='" + ASPxTextPlanCode.Text.Trim() + "'";
            DataTable Gdt = dc.GetTable(Gsql);
            string so = "", gzdd = "";
            if (Gdt.Rows.Count > 0)
            {
                so = Gdt.Rows[0][0].ToString();
                gzdd = Gdt.Rows[0][1].ToString();
            }
            //插入到日志表
            string Lsql = "insert into RST_ATPU_ZJTS_LOG(PART,CZTS,GXDM,GZDD,JHDM,JHSO,TSTYPE,RQSJ,WJPATH,NOTE_COLOR,NOTE_FONT,EDIT_USER,EDIT_FLAG,EDIT_DATE) "
                + "VALUES( 'LSCS', '" + czts + "','" + gxdm + "','" + gzdd + "','" + ASPxTextPlanCode.Text.Trim() + "','" + so + "','C',sysdate,'" + path + "','','','" + theUserName + "','ADD',sysdate)";
            dc.ExeSql(Lsql);
            //到正式表
            string Sql = "INSERT INTO RST_ATPU_ZJTS (PART,CZTS,GXDM,GZDD,JHDM,JHSO,TSTYPE,RQSJ,WJPATH,NOTE_COLOR,NOTE_FONT) "
                 + "VALUES( 'LSCS', '" + czts + "','" + gxdm + "','" + gzdd + "','" + ASPxTextPlanCode.Text.Trim() + "','" + so + "','C',sysdate,'" + path + "','','')";
            dc.ExeSql(Sql);

            e.Cancel = true;
            ASPxGridView2.CancelEdit();
            //if (Session["inv9501sql"] as string != "")
            //{
            //    DataTable dt2 = dc.GetTable(Session["inv9501sql"] as string);
            //    ASPxGridView2.DataSource = dt2;
            //    ASPxGridView2.DataBind();
            //}
            SetCondition();

        }


        protected void ASPxGridView2_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxComboBox uGxdm = ASPxGridView2.FindEditFormTemplateControl("txtGxdm") as ASPxComboBox;
            ASPxTextBox uPath = ASPxGridView2.FindEditFormTemplateControl("txtPath") as ASPxTextBox;
            ASPxTextBox uCzts = ASPxGridView2.FindEditFormTemplateControl("txtCzts") as ASPxTextBox;
            string gxdm = uGxdm.Text.Trim();
            string path = uPath.Text.Trim();
            string czts = uCzts.Text.Trim();
            string part = ASPxGridView2.GetRowValues(ASPxGridView2.EditingRowVisibleIndex, new string[] { "PART" }).ToString();
            //string part = e.NewValues["PART"].ToString();
            //修改前数据插入到日志表
            string Lsql = "insert into RST_ATPU_ZJTS_LOG(PART,CZTS,GXDM,GZDD,JHDM,JHSO,TSTYPE,RQSJ,WJPATH,NOTE_COLOR,NOTE_FONT,EDIT_USER,EDIT_FLAG,EDIT_DATE)select PART,CZTS,GXDM,GZDD,JHDM,JHSO,TSTYPE,RQSJ,WJPATH,NOTE_COLOR,NOTE_FONT, "
                             + "'" + theUserName + "','BEFOREDIT',SYSDATE FROM RST_ATPU_ZJTS WHERE GXDM = '" + gxdm + "' AND jhdm='" + ASPxTextPlanCode.Text.Trim() + "'  AND PART='" + part + "' ";
            dc.ExeSql(Lsql);
            string Sql = "UPDATE RST_ATPU_ZJTS SET CZTS='" + czts + "', WJPATH='" + path + "'"
                 + " WHERE   GXDM = '" + gxdm + "' AND jhdm='" + ASPxTextPlanCode.Text.Trim() + "'  AND PART='" + part + "' ";
            dc.ExeSql(Sql);
            //修改后数据插入到日志表
            string Lsql2 = "insert into RST_ATPU_ZJTS_LOG(PART,CZTS,GXDM,GZDD,JHDM,JHSO,TSTYPE,RQSJ,WJPATH,NOTE_COLOR,NOTE_FONT,EDIT_USER,EDIT_FLAG,EDIT_DATE)select PART,CZTS,GXDM,GZDD,JHDM,JHSO,TSTYPE,RQSJ,WJPATH,NOTE_COLOR,NOTE_FONT, "
                            + "'" + theUserName + "','AFTEREDIT',SYSDATE FROM RST_ATPU_ZJTS WHERE GXDM = '" + gxdm + "' AND jhdm='" + ASPxTextPlanCode.Text.Trim() + "'  AND PART='" + part + "'";
            dc.ExeSql(Lsql2);
            e.Cancel = true;
            ASPxGridView2.CancelEdit();
            //if (Session["inv9501sql"] as string != "")
            //{
            //    DataTable dt2 = dc.GetTable(Session["inv9501sql"] as string);
            //    ASPxGridView2.DataSource = dt2;
            //    ASPxGridView2.DataBind();
            //}
            SetCondition();

        }
        protected void ASPxGridView2_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditFormEventArgs e)
        {

            string Sql = "select a.process_code  from rel_location_process a ,rel_station_location b,code_station c where c.station_code=b.station_code "
            + " and b.location_code=a.location_code and c.station_name='" + ASPxTextSation.Text.ToUpper().Trim() + "'";
            DataTable dt = dc.GetTable(Sql);
            ASPxComboBox Ugxdm = ASPxGridView2.FindEditFormTemplateControl("txtGxdm") as ASPxComboBox;

            Ugxdm.DataSource = dt;
            Ugxdm.TextField = dt.Columns[0].ToString();
            Ugxdm.ValueField = dt.Columns[0].ToString();
            //Ugxdm.SelectedIndex = Ugxdm.Items.Count >= 0 ? 0 : -1;

            if (!ASPxGridView2.IsNewRowEditing && ASPxGridView2.IsEditing)
            {
                ///主键不可以修改
                (ASPxGridView2.FindEditFormTemplateControl("txtGxdm") as ASPxComboBox).Enabled = false;
            }

        }

        protected void ASPxGridView2_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {

            if (e.NewValues["WJPATH"].ToString().Length > 30)
            {
                e.RowError = "文件路径字节长度不能超过300！";
            }

            if (e.NewValues["CZTS"].ToString().Length > 3000)
            {
                e.RowError = "操作提示字节长度不能超过3000！";
            }
            if (ASPxGridView2.IsNewRowEditing)
            {
                
               string gxdm = e.NewValues["GXDM"].ToString();
               string chSql = "select * from rst_atpu_zjts where gxdm='" + gxdm + "' and jhdm='" + ASPxTextPlanCode.Text.Trim() + "'";
               DataTable dt = dc.GetTable(chSql);
               if (dt.Rows.Count > 0)
               {
                   e.RowError = "改计划序在该工序下已经维护过文件路径！";
 
               }
            }


        }
    }
}