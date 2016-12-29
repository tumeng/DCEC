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
using System.IO;
using Rmes.Pub.Function;
using Rmes.Pub.Data;
public partial class Rmes_Pub_CommonQuery_CommonDisplay : System.Web.UI.Page
{
    private dataConn dc = new dataConn();
    private int theColNum = 0;


    private string querySql;

    protected void Page_Load(object sender, EventArgs e)
    {
        //string querySql = Request.QueryString["querySql"];
        //string querySql = "select * from data_plan";
        //直接从session中得到sql语句，如果采取问号的方式，需要对字符进行urlencode编码和urldecode解码
        querySql = (string)Session["thePrintSql"];


        if (querySql != "")
        {
            dc.setTheSql(querySql);
            DataTable dt = dc.GetTable();
            GridView1.DataSource = dt;
            theColNum = dt.Columns.Count;
            GridView1.DataBind();
            
        }
    }



    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            for (int j = 0; j <= theColNum - 1; j++)
            {
                e.Row.Cells[j].Attributes.Add("class", "text");

            }
        }
        //处理gridview表头不加粗
        if (e.Row.RowType == DataControlRowType.Header)
        {
            foreach (TableCell cell in e.Row.Cells)
                cell.Attributes.Add("style", "FONT-WEIGHT:normal");
        }   
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        GridView1.AllowPaging = false;
        GridView1.DataBind();

        string style = @"<style> .text { mso-number-format:\@; } </script> ";
        Response.ClearContent();
        Response.AddHeader("content-disposition", "attachment; filename=MyExcelFile.xls");
        //Response.AddHeader("content-disposition", "inline; filename=MyExcelFile.xls");
        Response.ContentType = "application/ms-excel";
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);

        GridView1.RenderControl(htw);
        Response.Write(style);
        Response.Write(sw.ToString());
        Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        if (querySql != "")
        {
            dc.setTheSql(querySql);
            DataTable dt = dc.GetTable();
            GridView1.DataSource = dt;
            theColNum = dt.Columns.Count;
            this.GridView1.PageIndex = e.NewPageIndex; 


            GridView1.DataBind();
                
        } 
  // GridView1.DataBind();

        
    }
}
