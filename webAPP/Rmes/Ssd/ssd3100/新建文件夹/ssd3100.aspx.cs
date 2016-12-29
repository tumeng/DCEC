using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
using Rmes.Web.Base;
using System.Text;


/**
 * 2016-09-12
 *  唐海林
 * 流水号替换
 * */
public partial class Rmes_ssd3100 : Rmes.Web.Base.BasePage
{
    private dataConn dc = new dataConn();
    private PubCs thePubCs = new PubCs();

    private static string theCompanyCode, theUserId, theusercode;
    public string theProgramCode,errormsg="";
    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();
        theUserId = theUserManager.getUserId();
        theusercode = theUserManager.getUserCode();
        theProgramCode = "ssd3100";
        SetCondition();
    }
    private void SetCondition()
    {
        string sql = "select oldghtm||'--'||newghtm sn from atpu_ghtm_oldnew where ifth='N'";
        DataTable dt = dc.GetTable(sql);
        ASPxListBox1.DataSource = dt;
        ASPxListBox1.DataBind();
    }
    protected void ASPxCallbackPanel6_Callback(object sender, CallbackEventArgsBase e)
    {
        string[] s = e.Parameter.Split('|');
        string flag = s[0];
        string value1 = s[1];
        string value2 = "";
        switch (flag)
        {
            case "THD":
                value2 = s[2];
                string oldsn2 = value1.Replace((char)13, ' ').Trim();
                string newsn2 = value2.Replace((char)13, ' ').Trim();
                string sqlPlan2 = "select count(1) from data_product where sn='" + oldsn2 + "'";
                dc.setTheSql(sqlPlan2);
                string getvalue12 = dc.GetValue();
                if (getvalue12 == "0")
                {
                    ASPxCallbackPanel6.JSProperties.Add("cpCallbackName", "Fail");
                    ASPxCallbackPanel6.JSProperties.Add("cpCallbackRet", "原流水号不合法，不存在");
                    return;
                }
                sqlPlan2 = "select count(1) from data_product where sn='" + newsn2 + "'";
                dc.setTheSql(sqlPlan2);
                getvalue12 = dc.GetValue();
                if (getvalue12 == "0")
                {
                    ASPxCallbackPanel6.JSProperties.Add("cpCallbackName", "Fail");
                    ASPxCallbackPanel6.JSProperties.Add("cpCallbackRet", "新流水号不合法，不存在");
                    return;
                }
                if (!checkTh(oldsn2, newsn2))
                {
                    if (errormsg != "")
                    {
                        ASPxCallbackPanel6.JSProperties.Add("cpCallbackName", "Fail");
                        ASPxCallbackPanel6.JSProperties.Add("cpCallbackRet", errormsg);
                    }
                    return;
                }
                MW_GHTM_TH_DD sp2 = new MW_GHTM_TH_DD()
                {
                    THELSHA = oldsn2,
                    THEMIDLSH="AAAAAAA",
                    THELSHB = newsn2,
                    THEGZDD1 = "",
                    THEYGDM1 = theusercode,
                    THERETSTR1 = ""
                };
                Rmes.DA.Base.Procedure.run(sp2);
                if (sp2.THERETSTR1 != "0")
                {
                    ASPxCallbackPanel6.JSProperties.Add("cpCallbackName", "Fail");
                    ASPxCallbackPanel6.JSProperties.Add("cpCallbackRet", "替换过程出现错误，请重试！");
                    ASPxButton_Import.Enabled = true;
                    return;
                }
                break;
            case "TH":
                value2 = s[2];
                string oldsn = value1;
                string newsn = value2;
                string sqlPlan = "select count(1) from data_product where sn='" + oldsn + "'";
                dc.setTheSql(sqlPlan);
                string getvalue1 = dc.GetValue();
                if (getvalue1 == "0")
                {
                    ASPxCallbackPanel6.JSProperties.Add("cpCallbackName", "Fail");
                    ASPxCallbackPanel6.JSProperties.Add("cpCallbackRet", "原流水号不合法，不存在");
                    return;
                }
                sqlPlan = "select count(1) from data_product where sn='" + newsn + "'";
                dc.setTheSql(sqlPlan);
                getvalue1 = dc.GetValue();
                if (getvalue1 != "0")
                {
                    ASPxCallbackPanel6.JSProperties.Add("cpCallbackName", "Fail");
                    ASPxCallbackPanel6.JSProperties.Add("cpCallbackRet", "新流水号不合法，已使用");
                    return;
                }
                if (!checkTh(oldsn, newsn))
                {
                    if (errormsg != "")
                    {
                        ASPxCallbackPanel6.JSProperties.Add("cpCallbackName", "Fail");
                        ASPxCallbackPanel6.JSProperties.Add("cpCallbackRet", errormsg);
                    }
                    return;
                }
                MW_GHTM_TH sp = new MW_GHTM_TH()
                {
                    THEOLDLSH1 = oldsn,
                    THENEWLSH1 = newsn,
                    THEGZDD1 = "",
                    THEYGDM1 = theusercode,
                    THERETSTR1 = ""
                };
                Rmes.DA.Base.Procedure.run(sp);
                if (sp.THERETSTR1 != "0")
                {
                    ASPxCallbackPanel6.JSProperties.Add("cpCallbackName", "Fail");
                    ASPxCallbackPanel6.JSProperties.Add("cpCallbackRet", "替换过程出现错误，请重试！");
                    ASPxButton_Import.Enabled = true;
                    return;
                }
                break;
            
            case "MULTI":
                string[] s1 = value2.Split(',');
                for (int i = 0; i < s1.Length; i++)
                {
                    int index1 = s1[i].IndexOf("--");
                    string oldsn1 = s1[i].Substring(index1);
                    string newsn1 = s1[i].Substring(index1+2);
                    if (oldsn1 == "" && newsn1 == "")
                    {
                        ASPxCallbackPanel6.JSProperties.Add("cpCallbackName", "Fail");
                        ASPxCallbackPanel6.JSProperties.Add("cpCallbackRet", "流水号不合法！");
                        continue;
                    }
                    sqlPlan = "select count(1) from data_product where sn='" + oldsn1 + "'";
                    dc.setTheSql(sqlPlan);
                    getvalue1 = dc.GetValue();
                    if (getvalue1 == "0")
                    {
                        ASPxCallbackPanel6.JSProperties.Add("cpCallbackName", "Fail");
                        ASPxCallbackPanel6.JSProperties.Add("cpCallbackRet", "原流水号"+oldsn1+"不合法，不存在");
                        return;
                    }
                    sqlPlan = "select count(1) from data_product where sn='" + newsn1 + "'";
                    dc.setTheSql(sqlPlan);
                    getvalue1 = dc.GetValue();
                    if (getvalue1 != "0")
                    {
                        ASPxCallbackPanel6.JSProperties.Add("cpCallbackName", "Fail");
                        ASPxCallbackPanel6.JSProperties.Add("cpCallbackRet", "新流水号"+newsn1+"不合法，已使用");
                        return;
                    }
                    if (!checkTh(oldsn1, newsn1))
                    {
                        if (errormsg != "")
                        {
                            ASPxCallbackPanel6.JSProperties.Add("cpCallbackName", "Fail");
                            ASPxCallbackPanel6.JSProperties.Add("cpCallbackRet", errormsg);
                        }
                        return;
                    }
                    MW_GHTM_TH sp1 = new MW_GHTM_TH()
                    {
                        THEOLDLSH1 = oldsn1,
                        THENEWLSH1 = newsn1,
                        THEGZDD1 = "",
                        THEYGDM1 = theusercode,
                        THERETSTR1 = ""
                    };
                    Rmes.DA.Base.Procedure.run(sp1);
                    if (sp1.THERETSTR1 != "0")
                    {
                        ASPxCallbackPanel6.JSProperties.Add("cpCallbackName", "Fail");
                        ASPxCallbackPanel6.JSProperties.Add("cpCallbackRet", "替换过程出现错误，请重试！");
                        ASPxButton_Import.Enabled = true;
                        return;
                    }

                }
                break;
            default:
                break;
        }

    }
    private bool checkTh(string thisOldLsh, string thisNewLsh)
    {
        //errormsg = "";
        //string sql = "select oldghtm,newghtm from atpu_ghtm_oldnew where oldghtm='" + thisOldLsh + "' and ifth='Y'";
        //DataTable dt = dc.GetTable(sql);
        //if (dt.Rows.Count > 0)
        //{
        //    //Show(this, thisOldLsh + "已被" + dt.Rows[0][1].ToString() + "替换！");
        //    errormsg = thisOldLsh + "已被" + dt.Rows[0][1].ToString() + "替换！";
        //    return false;
        //}
        //sql = "select oldghtm,newghtm from atpu_ghtm_oldnew where newghtm='" + thisNewLsh + "' and ifth='Y'";
        //dt = dc.GetTable(sql);
        //if (dt.Rows.Count > 0)
        //{
        //    //Show(this, thisNewLsh + "已替换了" + dt.Rows[0][0].ToString() + "！");
        //    errormsg = thisNewLsh + "已替换了" + dt.Rows[0][0].ToString() + "！";
        //    return false;
        //}
        return true;
    }
    protected void ASPxListBoxUnused_Callback(object sender, CallbackEventArgsBase e)
    {
        string sql = "select oldghtm||'--'||newghtm sn from atpu_ghtm_oldnew where ifth='N'";
        DataTable dt = dc.GetTable(sql);
        ASPxListBox1.DataSource = dt;
        ASPxListBox1.DataBind();
    }
    public static void Show(System.Web.UI.Page page, string msg)
    {
        page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>alert('" + msg.ToString() + "');</script>");
    }
    protected void ASPxButton_Import_Click(object sender, EventArgs e)
    {
        try
        {
            ASPxButton_Import.Enabled = false;

            string path = File1.Value;//上传文件路径

            if (path == "" && !path.ToUpper().Contains(".TXT"))
            {
                ASPxButton_Import.Enabled = true;
                return;
            }
            string uploadPath = Server.MapPath(Request.ApplicationPath); //上传目录及文件名
            string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";

            if (!Directory.Exists("C:\\inetpub\\wwwroot\\webapp\\Rmes\\File\\excel"))
            {
                Directory.CreateDirectory("C:\\inetpub\\wwwroot\\webapp\\Rmes\\File\\excel");
            }
            uploadPath = uploadPath + "\\Rmes\\File\\excel\\" + fileName; //得到上传目录及文件名称  
            File1.PostedFile.SaveAs(uploadPath);       //上传文件到服务器  

            StreamReader sr = new StreamReader(uploadPath,Encoding.Default);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] strAry = line.Split('\t');
                if (strAry.Length < 2)
                {
                    ASPxCallbackPanel6.JSProperties.Add("cpCallbackName", "Fail");
                    ASPxCallbackPanel6.JSProperties.Add("cpCallbackRet", "文本内容解析错误，格式不合法");
                    return;
                }
                string sql = "insert into ATPU_GHTM_OLDNEW(OLDGHTM,NEWGHTM)values('" + strAry[0] + "','" + strAry[1] + "')";
                dc.ExeSql(sql);
            }
            ASPxButton_Import.Enabled = true;
            SetCondition();
        }
        catch (Exception ex2)
        {
            ASPxCallbackPanel6.JSProperties.Add("cpCallbackName", "Fail");
            ASPxCallbackPanel6.JSProperties.Add("cpCallbackRet", ex2.Message);
            ASPxButton_Import.Enabled = true;
        }
    }
}
