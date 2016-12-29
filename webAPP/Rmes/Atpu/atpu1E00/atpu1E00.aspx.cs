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
using System.Collections.Generic;
using DevExpress.Web.ASPxGridLookup;
using PetaPoco;
using Rmes.DA.Entity;
using Rmes.DA.Procedures;

/**
 * 功能概述：现场事件控制
 * 作者：杨少霞
 * 创建时间：2016-08-09
**/

namespace Rmes.WebApp.Rmes.Atpu.atpu1E00
{
    public partial class atpu1E00 : BasePage
    {
        private dataConn dc = new dataConn();
        private PubCs pc = new PubCs();
        public string theCompanyCode, theUserId, theUserName, theUserCode;
        public string theProgramCode;

        protected void Page_Load(object sender, EventArgs e)
        {
            userManager theUserManager = (userManager)Session["theUserManager"];
            theCompanyCode = theUserManager.getCompanyCode();
            theUserId = theUserManager.getUserId();
            theUserName = theUserManager.getUserName();
            theUserCode = theUserManager.getUserCode();

            theProgramCode = "atpu1E00";

            setCondition();

            
        }
        private void setCondition()
        {
            string sql = "SELECT A.SJDM,A.SJMC,A.GZDD,A.SJBS,B.PLINE_NAME,A.ROWID FROM ATPUXCSJB A LEFT JOIN CODE_PRODUCT_LINE B ON A.GZDD=B.PLINE_CODE "
                       + " WHERE  A.GZDD in(SELECT PLINE_CODE FROM VW_USER_ROLE_PROGRAM WHERE USER_ID='" + theUserId + "' "
                       + " And program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "')"
                       + " ORDER BY A.GZDD,A.SJDM ";
            DataTable dt = dc.GetTable(sql);

            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();
           
        }
    
        protected void ASPxCbSubmit_Callback1(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
        {
            try
            {
                char[] charSeparators = new char[] { ',' };
                string[] collection = e.Parameter.Split(charSeparators);
                int cnt = collection.Length;
                //删除，执行，关闭事件的处理
                if (collection[0] == "DEL")
                {
                    //删除
                    List<string> s = new List<string>();
                    for (int i = 1; i < cnt; i++)
                    {
                        s.Add(collection[i].ToString());
                    }
                    string[] s1 = s.ToArray();

                    if (ASPxGridView1.Selection.Count == 0)
                    {
                        e.Result = "Fail,请选择要删除的事件！";
                        return;
                    }
                    for (int i = 0; i < s1.Length; i++)
                    {
                        string strRowid = s1[i].ToString();

                        //插入到日志表
                        try
                        {
                            string Sql2 = "INSERT INTO ATPUXCSJB_LOG (GZDD,SJDM,SJMC,SJBS,user_code,flag,rqsj)"
                                + " SELECT GZDD,SJDM,SJMC,SJBS,'" + theUserCode + "' , 'DEL', SYSDATE FROM ATPUXCSJB WHERE ROWID='" + strRowid + "'";
                            dc.ExeSql(Sql2);
                        }
                        catch
                        {
                            return;
                        }
                        string delSql = "delete from ATPUXCSJB where ROWID='" + strRowid + "'";
                        dc.ExeSql(delSql);
                        e.Result = "OK,";
                    }
                }
                if (collection[0] == "ZX")
                {
                    //执行
                    List<string> s = new List<string>();
                    for (int i = 1; i < cnt; i++)
                    {
                        s.Add(collection[i].ToString());
                    }
                    string[] s1 = s.ToArray();

                    if (ASPxGridView1.Selection.Count == 0)
                    {
                        e.Result = "Fail,请选择要执行的事件！";
                        return;
                    }
                    for (int i = 0; i < s1.Length; i++)
                    {
                        string strRowid = s1[i].ToString();
                        //插入到日志表
                        try
                        {
                            string Sql2 = "INSERT INTO ATPUXCSJB_LOG (GZDD,SJDM,SJMC,SJBS,user_code,flag,rqsj)"
                                + " SELECT GZDD,SJDM,SJMC,SJBS,'" + theUserCode + "' , 'BEFOREEDIT', SYSDATE FROM ATPUXCSJB WHERE ROWID='" + strRowid + "'";
                            dc.ExeSql(Sql2);
                        }
                        catch
                        {
                            return;
                        }
                        string delSql = "update ATPUXCSJB set SJBS='1' where ROWID='" + strRowid + "'";
                        dc.ExeSql(delSql);
                        //插入到日志表
                        try
                        {
                            string Sql2 = "INSERT INTO ATPUXCSJB_LOG (GZDD,SJDM,SJMC,SJBS,user_code,flag,rqsj)"
                                + " SELECT GZDD,SJDM,SJMC,SJBS,'" + theUserCode + "' , 'AFTEREDIT', SYSDATE FROM ATPUXCSJB WHERE ROWID='" + strRowid + "'";
                            dc.ExeSql(Sql2);
                        }
                        catch
                        {
                            return;
                        }

                        e.Result = "OK,";
                    }
                }
                if (collection[0] == "GB")
                {
                    //关闭
                    List<string> s = new List<string>();
                    for (int i = 1; i < cnt; i++)
                    {
                        s.Add(collection[i].ToString());
                    }
                    string[] s1 = s.ToArray();

                    if (ASPxGridView1.Selection.Count == 0)
                    {
                        e.Result = "Fail,请选择要关闭的事件！";
                        return;
                    }
                    for (int i = 0; i < s1.Length; i++)
                    {
                        string strRowid = s1[i].ToString();
                        //插入到日志表
                        try
                        {
                            string Sql2 = "INSERT INTO ATPUXCSJB_LOG (GZDD,SJDM,SJMC,SJBS,user_code,flag,rqsj)"
                                + " SELECT GZDD,SJDM,SJMC,SJBS,'" + theUserCode + "' , 'BEFOREEDIT', SYSDATE FROM ATPUXCSJB WHERE ROWID='" + strRowid + "'";
                            dc.ExeSql(Sql2);
                        }
                        catch
                        {
                            return;
                        }
                        string delSql = "update ATPUXCSJB set SJBS='0' where ROWID='" + strRowid + "'";
                        dc.ExeSql(delSql);
                        //插入到日志表
                        try
                        {
                            string Sql2 = "INSERT INTO ATPUXCSJB_LOG (GZDD,SJDM,SJMC,SJBS,user_code,flag,rqsj)"
                                + " SELECT GZDD,SJDM,SJMC,SJBS,'" + theUserCode + "' , 'AFTEREDIT', SYSDATE FROM ATPUXCSJB WHERE ROWID='" + strRowid + "'";
                            dc.ExeSql(Sql2);
                        }
                        catch
                        {
                            return;
                        }

                        e.Result = "OK,";
                    }
                }  
                setCondition();
                ASPxGridView1.DataBind();
            }
            catch (Exception ex)
            {
                e.Result = "Fail,操作失败！";
                return;
            }

        }

        protected void ASPxGridView1_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (e.VisibleIndex < 0) return;
            string SJBS = e.GetValue("SJBS").ToString(); //根据执行情况0，1背景色不同
            if (SJBS=="1")
            {
                e.Row.BackColor = System.Drawing.Color.LightGreen;
            }
        }
    }
}