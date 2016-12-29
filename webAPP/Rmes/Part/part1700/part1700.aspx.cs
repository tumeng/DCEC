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

/**
 * 功能概述：到货差异调整
 * 作者：游晓航
 * 创建时间：2016-08-18
 */
public partial class Rmes_Part_part1700_part1700 : Rmes.Web.Base.BasePage  
{
        private dataConn dc = new dataConn();
        private PubCs thePubCs = new PubCs();
        private string theProgramCode;
        private string theCompanyCode, theUserId, theUserCode,theUserName;

        public Database db = DB.GetInstance();
        protected void Page_Load(object sender, EventArgs e)
        {
            userManager theUserManager = (userManager)Session["theUserManager"];
            theCompanyCode = theUserManager.getCompanyCode();
            theUserCode = theUserManager.getUserCode();
            theUserId = theUserManager.getUserId();
            theUserName = theUserManager.getUserName();
            theProgramCode = "part1700";
            if (!IsPostBack)
            {
                ASPxDateEdit2.Date = DateTime.Now;
                ASPxDateEdit1.Date = DateTime.Now.AddDays (-1);
            }
            initCode();
            setCondition();
        }
        private void initCode()
        {
            //初始化用户下拉列表
            string sql = "select distinct a.pline_code,b.pline_name from vw_user_role_program a left join code_product_line b on a.pline_code=b.pline_code where a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' and a.company_code='" + theCompanyCode + "'";
            SqlCode.SelectCommand = sql;
            SqlCode.DataBind();
        }
        private void setCondition()
        {
            string datetime = ASPxDateEdit2.Date.ToShortDateString() + " 23:59:59";
            string sql = "select  a.material_code ,a.gzdd,a.gys_code ,sum(a.material_num) material_num ,a.create_time ,a.qadsite ,b.ptp_buyer ,a.flag  from atpufsb a,copy_ptp_det b where  a.flag='N' and a.material_code=b.ptp_part and a.qadsite=b.ptp_site ";

            if (txtPCode.Text.Trim()!= "")
            {
               sql = sql + "and a.gzdd = '" + txtPCode.Value.ToString() + "'";
           }
            if (ASPxDateEdit1.Text.Trim() != "")
            {
                sql = sql + " AND a.create_time>=to_date('" + ASPxDateEdit1.Text.Trim() + "','YYYY-MM-DD hh24:mi:ss') ";
            }
           if (ASPxDateEdit2.Text.Trim() != "")
           {
               sql = sql + " AND a.create_time<=to_date('" + datetime + "','YYYY-MM-DD hh24:mi:ss') ";
           }
           if (textMcode.Text.Trim() != "")
           {
               sql = sql + " AND a.material_code like '%" + textMcode.Text.Trim() + "%' ";
           }
           if (textGcode.Text.Trim() != "")
           {
               sql = sql + " AND a.gys_code like '%" + textGcode.Text.Trim() + "%'" ; 
           }

           sql = sql + " group by a.material_code,a.gys_code,a.create_time,a.qadsite,a.flag,b.ptp_buyer,a.gzdd order by a.create_time,a.material_code ";
           DataTable dt = dc.GetTable(sql);
            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();
        }

        protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            setCondition();
            ASPxGridView1.Selection.UnselectAll();
        }

        //差异调整
        public void ASPxGridView1_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
        {
            string s = e.Parameters;
            string[] s1 = s.Split('|');
            string type1 = s1[0];//按钮
            int rowIndex = int.Parse(s1[1]);//index
             
             
            ASPxGridView atl1 = (ASPxGridView)sender;//选中的行数是什么
            int count1 = atl1.Selection.Count;
            
            if (type1 == "Adjust")
            {
                for (int i = 0; i < count1 && rowIndex < rowIndex + count1; i++, rowIndex++)
                {
                    string strMaterialnum = ASPxGridView1.GetRowValues(rowIndex, "MATERIAL_NUM").ToString();
                    string gzdd = ASPxGridView1.GetRowValues(rowIndex, "GZDD").ToString();
                    string qadsite = ASPxGridView1.GetRowValues(rowIndex, "QADSITE").ToString();
                    string materialcode = ASPxGridView1.GetRowValues(rowIndex, "MATERIAL_CODE").ToString();
                    
                    int Materialnum = Convert.ToInt32(strMaterialnum);

                    //插入到日志表
                    try
                    {
                        string Sql3 = "INSERT INTO ATPUFSB_LOG (bill_code,material_code,material_num,gzdd,create_time,flag,yhmc,handle_time,qadsite,gys_code,rmes_id,user_code,flag_log,rqsj)"
                            + " SELECT bill_code,material_code,material_num,gzdd,create_time,flag,yhmc,handle_time,qadsite,gys_code,rmes_id,'"
                            + theUserCode + "' , 'BEFOREEDIT', SYSDATE FROM atpufsb WHERE gzdd='" + gzdd + "' and qadsite='" + qadsite + "' and material_code='" + materialcode + "' "
                            + "and  create_time>=to_date('" + ASPxDateEdit1.Text.Trim() + "','yyyy-mm-dd')  and  create_time <=to_date('" + ASPxDateEdit2.Text.Trim() + "','yyyy-mm-dd')";
                        dc.ExeSql(Sql3);
                    }
                    catch
                    {
                         
                    }

                    string Sql = "update atpufsb set flag='Y',handle_time=sysdate,yhmc='" + theUserName + "' where gzdd='" + gzdd + "' and qadsite='" + qadsite + "' and material_code='" + materialcode + "' "
                               + "and  create_time>=to_date('" + ASPxDateEdit1.Text.Trim() + "','yyyy-mm-dd')  and  create_time <=to_date('" + ASPxDateEdit2.Text.Trim() + "','yyyy-mm-dd')";
                    dc.ExeSql(Sql);

                    //插入到日志表
                    try
                    {
                        string Sql3 = "INSERT INTO ATPUFSB_LOG (bill_code,material_code,material_num,gzdd,create_time,flag,yhmc,handle_time,qadsite,gys_code,rmes_id,user_code,flag_log,rqsj)"
                            + " SELECT bill_code,material_code,material_num,gzdd,create_time,flag,yhmc,handle_time,qadsite,gys_code,rmes_id,'"
                            + theUserCode + "' , 'AFTEREDIT', SYSDATE FROM atpufsb WHERE gzdd='" + gzdd + "' and qadsite='" + qadsite + "' and material_code='" + materialcode + "' "
                            + "and  create_time>=to_date('" + ASPxDateEdit1.Text.Trim() + "','yyyy-mm-dd')  and  create_time <=to_date('" + ASPxDateEdit2.Text.Trim() + "','yyyy-mm-dd')";
                        dc.ExeSql(Sql3);
                    }
                    catch
                    {
                        return;
                    }
                    string Sql2 = "select material_num from ms_over_mat  where material_code='" + materialcode + "' and gzdd='" + gzdd + "' and qadsite='" + qadsite + "'";
                    DataTable dt2 = dc.GetTable(Sql2);
                    if (dt2.Rows.Count > 0)
                    {
                        string strmn = dt2.Rows[0][0].ToString();
                        int mn = Convert.ToInt32(strmn);
                        Materialnum = mn - Materialnum;
                    }
                    else
                    {
                        Materialnum = 0 - Materialnum;
                    }
                    strMaterialnum = Convert.ToString(Materialnum);
                    MS_MODIFY_OVER_MAT sp = new MS_MODIFY_OVER_MAT()
                    {

                        FUNC1 = "UPDATE",
                        MATERIALCODE1 = materialcode,
                        LINESIDENUM1 = strMaterialnum,
                        GZDD1 = gzdd,
                        YHDM1 = theUserCode,
                        QADSITE1 = qadsite
                    };
                    Rmes.DA.Base.Procedure.run(sp);
                }
                e.Result = "OK,回冲池数量已调整！";
            }
            else { e.Result = "Fail,调整失败！"; }
        }
    
}