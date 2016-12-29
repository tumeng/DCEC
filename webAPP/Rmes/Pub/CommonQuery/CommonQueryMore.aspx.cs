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
public partial class Rmes_Pub_CommonQuery_CommonQueryMore : System.Web.UI.Page
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



    protected void Page_Load(object sender, EventArgs e)
    {

            //得到相关信息
            userManager theUserManager = (userManager)Session["theUserManager"];

            string theCompanyCode = theUserManager.getCompanyCode();
            string theProgramCode = theUserManager.getProgCode();

            //调用存储过程得到该程序号对应信息
            dataConn theDataConn = new dataConn();
            theDataConn.theComd.CommandType = CommandType.StoredProcedure;
            theDataConn.theComd.CommandText = "MW_QUERY_COMMONQUERY_SINGLE";
            
            theDataConn.theComd.Parameters.Add("THECOMPANYCODE1", OracleDbType.Varchar2).Value = "01";
            theDataConn.theComd.Parameters.Add("THECOMPANYCODE1", OracleDbType.Varchar2).Direction = ParameterDirection.Input;

            theDataConn.theComd.Parameters.Add("THEPROGRAMCODE1", OracleDbType.Varchar2).Value = "selectCompany";
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

            theConditionStr = " where " + DropDownList1.SelectedValue + " like '%" + TextBoxQuery.Text + "%'  order by 1";
        }
        else
        {
            theConditionStr = " order by 1";

        }
        SqlDataSource1.SelectCommand = theQuerySql + " " + theConditionStr;
    }

    protected void CheckBoxAll_CheckedChanged(object sender, EventArgs e)
    {
        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("CheckBoxOne");
            if (CheckBoxAll.Checked == true)
            {
                cbox.Checked = true;
            }
            else
            {
                cbox.Checked = false;
            }
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        CheckBox cbox = (CheckBox)GridView1.Rows[index].FindControl("CheckBoxOne");

        if (cbox.Checked == false)
            cbox.Checked = true;
        else
            cbox.Checked = false;

    }


    protected void ButtonConfirm_Click(object sender, EventArgs e)
    {
        //得到所选的值
        string theSelectColStr = "";
        string theSelectRowStr = "";

        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        {

            CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("CheckBoxOne");
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
                        theSelectColStr = theSelectColStr + "*" + row.Cells[m].Text;                  
                    }

                }
                if (theSelectRowStr == "")
                {
                    theSelectRowStr = theSelectColStr;
                }
                else
                {
                    theSelectRowStr = theSelectRowStr + "#" + theSelectColStr;
                }

            }

        }

        //把数据写到前台，返回调用界面
        string theAlert="alert('"+theSelectRowStr+"');";
        Response.Write("<script language=JavaScript>");
        Response.Write(theAlert );
 
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

    }
}
