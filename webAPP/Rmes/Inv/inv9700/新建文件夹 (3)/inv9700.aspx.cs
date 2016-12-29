using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rmes.Pub.Data;
using PetaPoco;
using Rmes.DA.Base;
using DevExpress.Web.ASPxGridView;
using DevExpress.Utils;
using System.Data;
using Rmes.Web.Base;
using DevExpress.Web.ASPxClasses;
using Rmes.DA.Procedures;
using System.IO;
/**
 * 功能概述：入出库查询
 * 作者：游晓航
 * 创建时间：2016-09-01
 */
public partial class Rmes_Inv_inv9700_inv9700 : Rmes.Web.Base.BasePage
{


    private dataConn dc = new dataConn();
    private PubCs thePubCs = new PubCs();
    private string theProgramCode;
    private string theCompanyCode, theUserId, theUserCode, theUserName;

    public Database db = DB.GetInstance();
    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();
        theUserCode = theUserManager.getUserCode();
        theUserId = theUserManager.getUserId();
        theUserName = theUserManager.getUserName();
        theProgramCode = "inv9700";
        if (!IsPostBack)
        {
            ASPxDateEdit1.Date = DateTime.Now;
            ASPxDateEdit2.Date = DateTime.Now.AddDays(1);
            
        }
        initCode();
        setCondition();
        if (Request["opFlag"] == "getBatch")
        {
            string str1 = "", str2 = "", str = "", rc = "", str3 = "";
            string DateEdit1 = Request["DATEEDIT1"].ToString().Trim();
            string DateEdit2 = Request["DATEEDIT2"].ToString().Trim();
            string Chose = Request["CHOSE"].ToString().Trim();
            string PCode = Request["PCODE"].ToString().Trim();
            //Server.UrlDecode（request.form["a"].ToString()）
            DateTime dt1 = Convert.ToDateTime(DateEdit1);
            DateTime dt2 = Convert.ToDateTime(DateEdit2);
            
            //dt1.AddDays(30);
            if (Chose == "RK") { rc = "入库"; }
            else{rc = "出库";}
             // select distinct destination from dp_rckwcb where gzrq>='" + Format(FromRq.Value, "yyyy-mm-dd hh:mm:ss") + "' and gzrq<='" + Format(ToRq.Value, "yyyy-mm-dd hh-mm-ss") + "'  AND GZDD='" + GV_Depot + "' and Rc='出库' order by Destination"I  
            //string activeflag = chkActiveFlag.Checked ? "Y" : "N";//条件表达式
            if (dt1.AddDays(31) < dt2)
            {
                str1 = "Overtime";

            }
            else
            {
                str1 = "ok";
                string sql = "select distinct gzrq from dp_rckwcb where  to_date(gzrq,'yyyy-mm-dd hh24:mi:ss')>= to_date('" + DateEdit1 + "', 'yyyy-mm-dd hh24:mi:ss')  and  to_date(gzrq,'yyyy-mm-dd hh24:mi:ss')<= to_date('" + DateEdit2 + "', 'yyyy-mm-dd hh24:mi:ss') AND GZDD='" + PCode + "' and Rc='" + rc + "' order by Gzrq";
                DataTable dt = dc.GetTable(sql);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string batch = dt.Rows[i][0].ToString();
                    
                    str2 = str2 + "@" + batch;
                }
                if (rc == "出库")
                {
                    string sql3 = "select distinct destination from dp_rckwcb  where  to_date(gzrq,'yyyy-mm-dd hh24:mi:ss')>= to_date('" + DateEdit1 + "', 'yyyy-mm-dd hh24:mi:ss')  and  to_date(gzrq,'yyyy-mm-dd hh24:mi:ss')<= to_date('" + DateEdit2 + "', 'yyyy-mm-dd hh24:mi:ss') AND GZDD='" + PCode + "' and Rc='出库' order by Destination";
                    DataTable dt3 = dc.GetTable(sql3);
                    for (int i = 0; i < dt3.Rows.Count; i++)
                    {
                        string destination = dt3.Rows[i][0].ToString();

                        str3 = str3 + "@" + destination;
                    }
 
                }
            }

            str = str1 + "," + str2 + "," + str3;
            this.Response.Write(str);
            this.Response.End();
        }
    }
    private void initCode()
    {
        //初始化生产线下拉列表
        string sql = "select a.depotid pline_code,a.depotname pline_name from dp_dmdepot a left join REL_USER_DMDEPOT b on a.depotid=b.depotid where b.user_code='" + theUserCode + "' and b.PROGRAM_CODE='INV9700'  ";
            
        //string sql = "select distinct a.pline_code,b.pline_name from vw_user_role_program a left join code_product_line b on a.pline_code=b.pline_code where a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' and a.company_code='" + theCompanyCode + "'";
        SqlCode.SelectCommand = sql;
        SqlCode.DataBind();
    }
    //private void initCode()
    //{
    //    //初始化出库方向下拉列表
    //    string sql = "select distinct a.pline_code,b.pline_name from vw_user_role_program a left join code_product_line b on a.pline_code=b.pline_code where a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' and a.company_code='" + theCompanyCode + "'";
    //    SqlCode.SelectCommand = sql;
    //    SqlCode.DataBind();
    //}
    private void setCondition()
    {
        string sql = "select * from dp_rckwcb where  ";
            
        if (txtPCode.Text.Trim() != "")
        {
            sql = sql + " gzdd = '" + txtPCode.Value.ToString() + "'";
        }
        else { sql = sql + "  gzdd in  (select pline_code from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "') " ;}
        if (ASPxDateEdit1.Text.Trim() != "")
        {
            sql = sql + " AND to_date(gzrq,'yyyy-mm-dd hh24:mi:ss')>= to_date('" + ASPxDateEdit1.Text.Trim() + "', 'yyyy-mm-dd hh24:mi:ss') ";
        }
        if (ASPxDateEdit2.Text.Trim() != "")
        {
            sql = sql + " AND to_date(gzrq,'yyyy-mm-dd hh24:mi:ss')<= to_date('" + ASPxDateEdit2.Text.Trim() + "', 'yyyy-mm-dd hh24:mi:ss')";
        }
        if (txtChose.Text.Trim() != "")
        {
            sql = sql + " AND rc= '" + txtChose.Text.Trim() + "' ";
        }
        if (textSO.Text.Trim() != "")
        {
            sql = sql + " AND so like '%" + textSO.Text.Trim() + "%'";
        }
        if (txtPc.Text.Trim() != "")
        {
            sql = sql + " AND gzrq like '%" + txtPc.Value.ToString() + "%'";
        }
        if (txtFx.Text.Trim() != "")
        {
            sql = sql + " AND destination like '%" + txtFx.Text.Trim() + "%'";
        }
        sql = sql + " order by so,ghtm";
        DataTable dt = dc.GetTable(sql);
        ASPxGridView1.DataSource = dt;
        ASPxGridView1.DataBind();

        string sql2 = "select SO,COUNT(*) SL from dp_rckwcb where ";
         
        if (txtPCode.Text.Trim() != "")
        {
            sql2 = sql2 + " gzdd = '" + txtPCode.Value.ToString() + "'";
        }
        else { sql2 = sql2 + "  gzdd in  (select pline_code from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "') " ;}
        if (ASPxDateEdit1.Text.Trim() != "")
        {
            sql2 = sql2 + " AND to_date(gzrq,'yyyy-mm-dd hh24:mi:ss')>= to_date('" + ASPxDateEdit1.Text.Trim() + "', 'yyyy-mm-dd hh24:mi:ss')";
        }
        if (ASPxDateEdit2.Text.Trim() != "")
        {
            sql2 = sql2 + " AND to_date(gzrq,'yyyy-mm-dd hh24:mi:ss')<= to_date('" + ASPxDateEdit2.Text.Trim() + "', 'yyyy-mm-dd hh24:mi:ss')";
        }
        if (txtChose.Text.Trim() != "")
        {
            sql2 = sql2 + " AND rc= '" + txtChose.Text.Trim() + "' ";
        }
        if (textSO.Text.Trim() != "")
        {
            sql2 = sql2 + " AND so like '%" + textSO.Text.Trim() + "%'";
        }
        if (txtPc.Text.Trim() != "")
        {
            sql2 = sql2 + " AND gzrq like '%" + txtPc.Value.ToString() + "%'";
        }
        if (txtFx.Text.Trim() != "")
        {
            sql2 = sql2 + " AND destination like '%" + txtFx.Text.Trim() + "%'";
        }
            sql2 = sql2 + "  GROUP BY SO order by So";
            DataTable dt2 = dc.GetTable(sql2);
            ASPxGridView2.DataSource = dt2;
            ASPxGridView2.DataBind();
    }

    protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        //ASPxGridView1.JSProperties.Add("cpCallbackValue", ".txt");
        string s = e.Parameters;
        string[] s1 = s.Split('|');
        if (s1.Length > 1)
        {
            if (s1[0] == "TXT")
            {
                string theWriteFile = "Stock" + System.DateTime.Now.ToString("yyyyMMddHHmmss");
                FileStream fs1 = new FileStream("C:\\TRANLIST\\Storage\\" + theWriteFile + ".txt", FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);//创建写入文件 
                //StreamWriter sw = new StreamWriter(fs1);
                StreamWriter sw = new StreamWriter(fs1, System.Text.Encoding.UTF8);
                //sw.WriteLine("流水号   入库日期");//开始写入值

                string sql = "select ghtm,rkdate from dp_rckwcb where  ";
                if (txtPCode.Text.Trim() != "")
                {
                    sql = sql + " gzdd = '" + txtPCode.Value.ToString() + "'";
                }
                else { sql = sql + "  gzdd in  (select pline_code from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "') "; }
                if (ASPxDateEdit1.Text.Trim() != "")
                {
                    sql = sql + " AND to_date(gzrq,'yyyy-mm-dd hh24:mi:ss')>= to_date('" + ASPxDateEdit1.Text.Trim() + "', 'yyyy-mm-dd hh24:mi:ss') ";
                }
                if (ASPxDateEdit2.Text.Trim() != "")
                {
                    sql = sql + " AND to_date(gzrq,'yyyy-mm-dd hh24:mi:ss')<= to_date('" + ASPxDateEdit2.Text.Trim() + "', 'yyyy-mm-dd hh24:mi:ss')";
                }
                if (txtChose.Text.Trim() != "")
                {
                    sql = sql + " AND rc= '" + txtChose.Text.Trim() + "' ";
                }
                if (textSO.Text.Trim() != "")
                {
                    sql = sql + " AND so like '%" + textSO.Text.Trim() + "%'";
                }
                if (txtPc.Text.Trim() != "")
                {
                    sql = sql + " AND gzrq like '%" + txtPc.Value.ToString() + "%'";
                }
                if (txtFx.Text.Trim() != "")
                {
                    sql = sql + " AND destination like '%" + txtFx.Text.Trim() + "%'";
                }
                sql = sql + " order by ghtm,rkdate";
                DataTable dt = dc.GetTable(sql);
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    string Line1 = "";

                    Line1 = Line1 + dt.Rows[j][0].ToString() + "\t";
                    Line1 = Line1 + dt.Rows[j][1].ToString();
                    sw.WriteLine(Line1);//写入到txt
                    //sw.NewLine = Line1;
                }

                sw.Close();
                sw.Dispose();
                fs1.Close();
                fs1.Dispose();
                try
                {
                    ASPxGridView1.JSProperties.Add("cpCallbackValue", theWriteFile + ".txt");
                }
                catch { }
            }
        }
        else
        {
            //string theWriteFile = "";
            ASPxGridView1.JSProperties.Add("cpCallbackValue", ".txt");
 
        }

        setCondition();
        ASPxGridView1.Selection.UnselectAll();
    }
    protected void ASPxGridView2_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        setCondition();
        //ASPxGridView2.Selection.UnselectAll();
    }

    //删除与批量删除
    public void ASPxGridView1_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)

    {
        string s = e.Parameters;
        string[] s1 = s.Split('|');
        string type1 = s1[0];//按钮
        string batch = s1[1];
        string pcode = s1[2];
        string Chose = s1[2];
        string chose="";
         if (Chose == "RK") { chose = "入库"; }
            else{chose = "出库";}
         string a = txtPc.Text.Trim();       
        if (type1 == "Delete")
        {
             
                string strYgmc = ASPxGridView1.GetRowValues(ASPxGridView1.FocusedRowIndex, "YGMC").ToString();
                string strGhtm = ASPxGridView1.GetRowValues(ASPxGridView1.FocusedRowIndex, "GHTM").ToString();
                string strRkdate = ASPxGridView1.GetRowValues(ASPxGridView1.FocusedRowIndex, "RKDATE").ToString();
                string strRc = ASPxGridView1.GetRowValues(ASPxGridView1.FocusedRowIndex, "RC").ToString();
                if (strYgmc != theUserName)
                {
                    e.Result = "Fail,您没有删除纪录" + strGhtm + "的权限！";
                    return;
                }
                string Sql = "Select * from dp_kcb where ghtm='" + strGhtm + "'";
                DataTable dt = dc.GetTable(Sql);
                if (dt.Rows.Count <= 0)
                {
                    e.Result = "Fail,库存中不存在记录'" + strGhtm + "'！操作失败，请确认操作是否有误或先删除'" + strGhtm + "'的出库纪录！";
                    return;
                }
                if (strRc == "入库")
                {
                   
                    string Sql2 = "select lsh from qad_bkfl where lsh='" + strGhtm + "'";
                    DataTable dt2 = dc.GetTable(Sql2);
                    if (dt2.Rows.Count <= 0)
                    {
                        e.Result = "Fail,该流水号" + strGhtm + "已经做过回冲，入库记录不能删除！";
                        return;
                    }
                    string Sql3 = "select rc from dp_rckwcb where ghtm='" + strGhtm + "'  and gzdd='" + pcode + "' order by gzrq desc";
                    DataTable dt3 = dc.GetTable(Sql3);
                    string rc = dt3.Rows[0][0].ToString();
                    if (rc == "出库")
                    {
                        e.Result = "Fail,该发动机最后一次操作是出库操作，不能删除当前入库记录！！";
                        return;
                    }

                    string DelSql1 = "delete from dp_rckwcb where ghtm='" + strGhtm + "' and rkdate='" + strRkdate + "' and gzdd='" + pcode + "'";
                    dc.ExeSql(DelSql1);
                    string DelSql2 = "delete from dp_kcb where  ghtm='" + strGhtm + "' and rkdate='" + strRkdate + "' and gzdd='" + pcode + "'";
                    dc.ExeSql(DelSql2);
                    e.Result = "OK,删除成功！";
                }
                else if (strRc == "出库")
                {
                    string Sql4 = "select ghtm,so,jhdm,ygmc,rklx,rkdate,gzrq,gzdd,rc,sourceplace,destination,batchId,ch from dp_rckwcb where ghtm='" + strGhtm + "' and gzdd='" + pcode + "' and rc='入库' order by gzrq desc";
                    DataTable dt4 = dc.GetTable(Sql4);
                    string ghtm = dt4.Rows[0][0].ToString();
                    string so = dt4.Rows[0][1].ToString();
                    string jhdm = dt4.Rows[0][2].ToString();
                    string ygmc = dt4.Rows[0][3].ToString();
                    string rklx = dt4.Rows[0][4].ToString();
                    string rkdate = dt4.Rows[0][5].ToString();
                    string gzrq = dt4.Rows[0][6].ToString();
                    string gzdd = dt4.Rows[0][7].ToString();
                    string rc = dt4.Rows[0][8].ToString();
                    string sourceplace = dt4.Rows[0][9].ToString();
                    string destination = dt4.Rows[0][10].ToString();
                    string batchId = dt4.Rows[0][11].ToString();
                    string ch = dt4.Rows[0][12].ToString();
                    if (dt4.Rows.Count <= 0)
                    {
                        string Sql5 = "insert into dp_kcb(ghtm,so,jhdm,ygmc,rklx,rkdate,gzrq,gzdd,rc,sourceplace,destination,batchId,ch) "
                                     + "values('" + ghtm + "','" + so + "','" + jhdm + "','" + ygmc + "','" + rklx + "','" + rkdate + "','" + gzrq + "','" + gzdd + "','" + rc + "','" + sourceplace + "','" + destination + "','" + batchId + "','" + ch + "')";
                        dc.ExeSql(Sql5);
                    }
                    else 
                    {
                        string Sql6 = " delete from dp_willrckwcb where ghtm='" + strGhtm + "' and rc='出库' and gzdd='" + pcode + "'";
                        dc.ExeSql(Sql6);
                        string Sql7 ="delete from dp_rckwcb where ghtm='" + strGhtm + "' and rkdate='" + strRkdate + "' and gzdd='" + pcode + "'";
                        dc.ExeSql(Sql7);
                    }
      
                    e.Result = "OK,删除成功！";
 
                }
            else{e.Result = "Fail,删除失败！";}


                
        }
       else 
        {
            if(chose=="入库")
            {
            string Sql = "select YGMC,GHTM,RKDATE from dp_rckwcb where gzrq like'" + batch + "' and rc='"+chose+"'";
            DataTable dt = dc.GetTable(Sql);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string strYgmc = dt.Rows[i][0].ToString();
                    string strGhtm = dt.Rows[i][1].ToString();
                    string strRkdate = dt.Rows[i][2].ToString();
                    if (strYgmc != theUserName)
                    {
                        e.Result = "Fail,您没有删除纪录" + strGhtm + "的权限！";
                        return;
                    }
                    string Sql1 = "Select * from dp_kcb where ghtm='" + strGhtm + "'";
                    DataTable dt1 = dc.GetTable(Sql1);
                    if (dt.Rows.Count <= 0)
                    {
                        e.Result = "Fail,库存中不存在记录'" + strGhtm + "'！操作失败，请确认操作是否有误或先删除'" + strGhtm + "'的出库纪录！";
                        return;
                    }
                    string Sql2 = "select lsh from qad_bkfl where lsh='" + strGhtm + "'";
                    DataTable dt2 = dc.GetTable(Sql2);
                    if (dt2.Rows.Count <= 0)
                    {
                        e.Result = "Fail,该流水号" + strGhtm + "已经做过回冲，入库记录不能删除！";
                        return;
                    }
                    string Sql3 = "select rc from dp_rckwcb where ghtm='" + strGhtm + "'  and gzdd='" + pcode + "' order by gzrq desc";
                    DataTable dt3 = dc.GetTable(Sql3);
                    string rc = dt3.Rows[0][0].ToString();
                    if (rc == "出库")
                    {
                        e.Result = "Fail,该发动机最后一次操作是出库操作，不能删除当前入库记录！！";
                        return;
                    }
                    string DelSql1 = "delete from dp_rckwcb where ghtm='" + strGhtm + "' and rkdate='" + strRkdate + "' and gzdd='" + pcode + "'";
                 
                    dc.ExeSql(DelSql1);
                    string DelSql2 = "delete from dp_kcb where  ghtm='" + strGhtm + "' and rkdate='" + strRkdate + "' and gzdd='" + pcode + "'";
                    dc.ExeSql(DelSql2);
                }

                e.Result = "OK,删除成功！";
             
            }
            }
            else if(chose=="出库")
            {
            string DelSql1 = " delete from dp_rckwcb where to_date(gzrq,'yyyy-mm-dd hh24:mi:ss')>= to_date('" + ASPxDateEdit1.Text.Trim() + "', 'yyyy-mm-dd hh24:mi:ss') and "
                + "to_date(gzrq,'yyyy-mm-dd hh24:mi:ss')<= to_date('" + ASPxDateEdit2.Text.Trim() + "', 'yyyy-mm-dd hh24:mi:ss')  and gzrq like '" + batch + "' and destination like '" + txtFx.Text.Trim() + "' and gzdd='" + pcode + "' and rc='出库'";
                   
                    dc.ExeSql(DelSql1);
            }
            e.Result = "Fail,删除失败！";
        }

          
    }
    protected void btnXlsExport_Click(object sender, EventArgs e)
    {
        ASPxGridViewExporter1.WriteXlsToResponse("入出库信息导出");
    }
    protected void btnTxt_Click(object sender, EventArgs e)
    {
        string theWriteFile = "Stock" + System.DateTime.Now.ToString("yyyyMMddHHmmss");
        FileStream fs1 = new FileStream("C:\\TRANLIST\\Storage\\" + theWriteFile + ".txt", FileMode.Create, FileAccess.Write);//创建写入文件 
        StreamWriter sw = new StreamWriter(fs1);
        //sw.WriteLine("流水号   入库日期");//开始写入值

        string sql = "select ghtm,rkdate from dp_rckwcb where  ";
        if (txtPCode.Text.Trim() != "")
        {
            sql = sql + " gzdd = '" + txtPCode.Value.ToString() + "'";
        }
        else { sql = sql + "  gzdd in  (select pline_code from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "') "; }
        if (ASPxDateEdit1.Text.Trim() != "")
        {
            sql = sql + " AND to_date(gzrq,'yyyy-mm-dd hh24:mi:ss')>= to_date('" + ASPxDateEdit1.Text.Trim() + "', 'yyyy-mm-dd hh24:mi:ss') ";
        }
        if (ASPxDateEdit2.Text.Trim() != "")
        {
            sql = sql + " AND to_date(gzrq,'yyyy-mm-dd hh24:mi:ss')<= to_date('" + ASPxDateEdit2.Text.Trim() + "', 'yyyy-mm-dd hh24:mi:ss')";
        }
        if (txtChose.Text.Trim() != "")
        {
            sql = sql + " AND rc= '" + txtChose.Text.Trim() + "' ";
        }
        if (textSO.Text.Trim() != "")
        {
            sql = sql + " AND so like '%" + textSO.Text.Trim() + "%'";
        }
        if (txtPc.Text.Trim() != "")
        {
            sql = sql + " AND gzrq like '%" + txtPc.Value.ToString() + "%'";
        }
        if (txtFx.Text.Trim() != "")
        {
            sql = sql + " AND destination like '%" + txtFx.Text.Trim() + "%'";
        }
        sql = sql + " order by ghtm,rkdate";
        DataTable dt = dc.GetTable(sql);
        for (int j = 0; j < dt.Rows.Count; j++)
        {
            string Line1 = "";
            
            Line1 = Line1 + dt.Rows[j][0].ToString() + "\t";
            Line1 = Line1 + dt.Rows[j][1].ToString() ;
            sw.WriteLine(Line1);//写入到txt
            //sw.NewLine = Line1;
        }

        sw.Close();
        fs1.Close();
        
    }
}