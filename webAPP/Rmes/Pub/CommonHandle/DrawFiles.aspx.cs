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
using Rmes.Pub.Function;


public partial class Rmes_Pub_CommonHandle_DrawFiles : System.Web.UI.Page
{
    private string theSql = "";
    private string opCode = "";
    private PubJs js = new PubJs();
    protected void Page_Load(object sender, EventArgs e)
    {
        opCode = Session["DrawopCode"].ToString().Trim();
        Label2.Text = opCode;
        if (!IsPostBack)
        {
            theSql = "select file_name from file_names where file_name like '" + opCode + "%'";
            dataConn theDc = new dataConn(theSql);
            DataTable dt = theDc.GetTable();
            ListBox1.DataSource = dt;
            ListBox1.DataTextField = "file_name";
            ListBox1.DataValueField = "file_name";
            ListBox1.DataBind();
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        string filename = ListBox1.SelectedValue.ToString();
        if (ListBox1.SelectedValue.ToString() == "")
        {
            js.Alert("请选择要调阅的文件！");
            return;
        }
        Session["FileId"] = filename;
        Session["FileName"] = filename;
        Session["FilePath"] = "D:\\DrawFiles\\" + filename + "";
        Session["FileContentType"] = "image/tiff";
        string url = ResolveUrl("~/Rmes/Pub/CommonHandle/openFileTwo.aspx");
        js.OpenWebForm(url);
    }
}
