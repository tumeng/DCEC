
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Rmes.Pub.Data;
using Oracle.DataAccess.Client;
using Rmes.Pub.Function;
public partial class Rmes_Pub_CommonQuery_CommonQuerySingle : System.Web.UI.Page
{
    public string theCompanyCode="";
    public string theProgramCode ="" ;
    public string theProgramName = "";
    public string theQueryFieldCode = "";
    public string theQueryFieldName = "";
    public string theFieldCode = "";
    public string theFieldName = "";
    public string theQuerySql = "";
    public string thePrimaryKey = "";
    public string theConditionStr = "";

    public string theFlag = "";
    public string theQryStr = "";


    public PubJs theJs = new PubJs();



    protected void Page_Load(object sender, EventArgs e)
    {

            //得到相关信息
            userManager theUserManager = (userManager)Session["theUserManager"];

            string theCompanyCode = theUserManager.getCompanyCode();
            string theProgramCode = Request.QueryString["selectProgramValue"].ToString();
            theFlag = Request.QueryString["theFlag"].ToString();
            try
            {
                theQryStr = Request.QueryString["theQryStr"].ToString();
            }
            catch(Exception){
                theQryStr = "";
            }
            
            if(theQryStr.Trim()==""){
               theQryStr =" 1=1 ";
            }

            //调用存储过程得到该程序号对应信息
        //该程序暂时废弃不用，如果以后要启用，请将调用存储过程修改。.NET 4.0下如下调用会产生参数重复插入的问题。 by liuzhy 2013.12.25 x-max XIAN :(
            dataConn theDataConn = new dataConn();
            theDataConn.theComd.CommandType = CommandType.StoredProcedure;
            theDataConn.theComd.CommandText = "MW_QUERY_COMMONQUERY_SINGLE";

            theDataConn.theComd.Parameters.Add("THECOMPANYCODE1", OracleDbType.Varchar2).Value = theCompanyCode;
            theDataConn.theComd.Parameters.Add("THECOMPANYCODE1", OracleDbType.Varchar2).Direction = ParameterDirection.Input;

            theDataConn.theComd.Parameters.Add("THEPROGRAMCODE1", OracleDbType.Varchar2).Value = theProgramCode;
            theDataConn.theComd.Parameters.Add("THEPROGRAMCODE1", OracleDbType.Varchar2).Direction = ParameterDirection.Input;


            theDataConn.theComd.Parameters.Add("THEPROGRAMNAME1", OracleDbType.Varchar2, 50).Direction = ParameterDirection.InputOutput;

            theDataConn.theComd.Parameters.Add("THERETQUERYFIELDCODE1", OracleDbType.Varchar2, 300).Direction = ParameterDirection.InputOutput;

            theDataConn.theComd.Parameters.Add("THERETQUERYFIELDNAME1", OracleDbType.Varchar2, 300).Direction = ParameterDirection.InputOutput;

            theDataConn.theComd.Parameters.Add("THERETFIELDCODE1", OracleDbType.Varchar2, 300).Direction = ParameterDirection.InputOutput;

            theDataConn.theComd.Parameters.Add("THERETFIELDNAME1", OracleDbType.Varchar2, 300).Direction = ParameterDirection.InputOutput;

            theDataConn.theComd.Parameters.Add("THERETQUERYSQL1", OracleDbType.Varchar2, 300).Direction = ParameterDirection.InputOutput;

            theDataConn.theComd.Parameters.Add("THERETPRIMARYKEY1", OracleDbType.Varchar2, 300).Direction = ParameterDirection.InputOutput;

            theDataConn.OpenConn();
            theDataConn.theComd.ExecuteNonQuery();

            theProgramName = (string)theDataConn.theComd.Parameters["THEPROGRAMNAME1"].Value;
            theQueryFieldCode = (string)theDataConn.theComd.Parameters["THERETQUERYFIELDCODE1"].Value;
            theQueryFieldName = (string)theDataConn.theComd.Parameters["THERETQUERYFIELDNAME1"].Value;
            theFieldCode = (string)theDataConn.theComd.Parameters["THERETFIELDCODE1"].Value;
            theFieldName = (string)theDataConn.theComd.Parameters["THERETFIELDNAME1"].Value;
            theQuerySql = (string)theDataConn.theComd.Parameters["THERETQUERYSQL1"].Value;
            thePrimaryKey = (string)theDataConn.theComd.Parameters["THERETPRIMARYKEY1"].Value;

 


            theDataConn.CloseConn();


            //相关属性赋值

            if (theConditionStr == "")
            {
                theConditionStr = " where " + theQryStr;
            }
            else {
                theConditionStr = theConditionStr + " and " + theQryStr;
            }

            SqlDataSource1.SelectCommand = theQuerySql+" "+theConditionStr;

            string[] thePrimaryKeyArray = thePrimaryKey.Split(',');
            GridView1.DataKeyNames = thePrimaryKeyArray;

            //动态生成列
            
            if (!IsPostBack)
            {

                string[] theFieldCodeArray = theFieldCode.Split(',');
                string[] theFieldNameArray = theFieldName.Split(',');
                string[] theQueryFieldCodeArray = theQueryFieldCode.Split(',');
                string[] theQueryFieldNameArray = theQueryFieldName.Split(',');

                BoundField[] field = new BoundField[theFieldCodeArray.Length];
                for (int i = 0; i < theFieldCodeArray.Length; i++) {


                    field[i]=new BoundField();
                    field[i].DataField = theFieldCodeArray[i];
                    field[i].HeaderText = theFieldNameArray[i];
                    field[i].SortExpression = theFieldCodeArray[i];
                    GridView1.Columns.Add(field[i]);
                }
           
                //生成查询方式下拉框
                ListItem[] listItem=new ListItem[theQueryFieldCodeArray.Length];

                //增加空行
                ListItem listItem1 = new ListItem();
                listItem1.Value = "";
                listItem1.Text = "";
                DropDownList1.Items.Add(listItem1);
                for (int j = 0; j < theQueryFieldCodeArray.Length; j++)
                {
                    listItem[j]=new ListItem();
                    listItem[j].Value=theQueryFieldCodeArray[j];
                    listItem[j].Text =theQueryFieldNameArray[j];
                    DropDownList1.Items.Add(listItem[j]);
                    
                }

        }

 



    }
 
    protected void ButtonQuery_Click(object sender, EventArgs e)
    {
        //判断生成查询条件
        if (DropDownList1.Text != "")
        {

            theConditionStr = " where " + DropDownList1.SelectedValue + " like '%" + TextBoxQuery.Text + "%' "+" and  "+theQryStr;
        }
        else
        {
            theConditionStr = " where "+theQryStr;

        }
        SqlDataSource1.SelectCommand = theQuerySql + " " + theConditionStr;
    }

 


    protected void ButtonConfirm_Click(object sender, EventArgs e)
    {
        //得到所选的值
        string theSelectColStr = "";
        string theSelectRowStr = "";

        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        {

            RadioButton cbox = (RadioButton)GridView1.Rows[i].FindControl("RadioButtonOne");
            if (cbox.Checked == true)
            {
                GridViewRow row = (GridViewRow)GridView1.Rows[i];
                theSelectColStr = "";
                for (int m = 1; m < row.Cells.Count; m++)
                {
                    if (theSelectColStr == "")
                    {
                        theSelectColStr =  row.Cells[m].Text;
                    }
                    else {
                        theSelectColStr = theSelectColStr + "===" + row.Cells[m].Text;                  
                    }

                }
                if (theSelectRowStr == "")
                {
                    theSelectRowStr = theSelectColStr;
                }
                else
                {
                    theSelectRowStr = theSelectRowStr + "&&&" + theSelectColStr;
                }

            }

        }

        if (theSelectRowStr == "") {
            theJs.Alert("请选择数据!");
            return;
        }

        //把数据写到前台，返回调用界面
        Response.Write("<script language=JavaScript>");
        Response.Write("window.opener.getCommonValue('"+theFlag+"','" + theSelectRowStr + "');");
        Response.Write("close();");
        Response.Write("</script>");
 

    }
    protected void ButtonCancel_Click(object sender, EventArgs e)
    {
        //把数据写到前台，返回调用界面
 
        Response.Write("<script language=JavaScript>");
        Response.Write("close();</script>");
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            RadioButton rb = (RadioButton)e.Row.FindControl("RadioButtonOne");
            rb.Attributes.Add("onclick", "judge(this)");//给RadioButton添加onclick属性
 
        }
        //处理gridview表头不加粗
        if (e.Row.RowType == DataControlRowType.Header)
        {
            foreach (TableCell cell in e.Row.Cells)
                cell.Attributes.Add("style", "FONT-WEIGHT:normal");
        }  
    }
}
