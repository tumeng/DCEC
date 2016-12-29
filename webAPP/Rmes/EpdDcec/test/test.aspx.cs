using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rmes.Pub.Data;
using System.Data;
using System.Collections;
using DevExpress.Web.ASPxEditors;
using System.Drawing;

namespace Rmes.WebApp.Rmes.EpdDcec.test
{
    public partial class test : System.Web.UI.Page
    {
        private dataConn dc = new dataConn();
        private static DataTable ht = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ht.Rows.Clear();

                ListNode.ClientSideEvents.ItemDoubleClick = "function(s,e) {var index = ListNode.GetSelectedIndex();if(index!=-1) {var items = ListNode.GetSelectedItems();PanelNode.PerformCallback(items[0].text);}}";
            }
        }
        protected void PanelNode_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string nodeCode = e.Parameter;

            if (nodeCode != "6; *")
            {
                PubCs pc = new PubCs();
                ArrayList al = pc.SplitBySeparator(nodeCode, ";");
                ht.Columns.Add();
                ht.Columns.Add();
                DataRow dr = ht.NewRow();
                dr[0] = al[1].ToString();
                dr[1] = al[0].ToString();
                ht.Rows.Add(dr);
            }
            else
            {
                DataRow dr = ht.NewRow();
                dr[0] = "*";
                dr[1] = "*";
                ht.Rows.Add(dr);
            }

            createTable();
            //ASPxTextBox newNode = createNode(al[1].ToString(),al[0].ToString());
            //PanelNode.Controls.Add(newNode);

            //ASPxImage newLine = new ASPxImage();
            //newLine.ImageUrl = "~/lineH.png";
            //PanelNode.Controls.Add(newLine);

        }
        private void createTable()
        {
            Table tb = new Table();
            tb.ID = "table";
            tb.BorderWidth = 1;
            tb.Width = Unit.Percentage(100);

            TableRow tr = new TableRow();
            TableCell td = new TableCell();

            Table tbInner = new Table();
            tbInner.Height = 100;
            TableRow trInner = new TableRow();
            for (int i = 0; i < ht.Rows.Count; i++)
            {
                if (ht.Rows[i][0].ToString() != "*")
                {
                    TableCell tdInner = new TableCell();
                    ASPxTextBox newNode = createNode(ht.Rows[i][0].ToString(), ht.Rows[i][1].ToString());
                    tdInner.Controls.Add(newNode);
                    trInner.Cells.Add(tdInner);
                }
                if (ht.Rows[i][0].ToString() == "*")
                {
                    tbInner.Rows.Add(trInner);
                    td.Controls.Add(tbInner);
                    tr.Cells.Add(td);
                    tb.Rows.Add(tr);

                    tbInner = new Table();
                    tbInner.Width = Unit.Percentage(100);
                    tbInner.Height = 1;
                    trInner = new TableRow();
                    TableCell tdInner = new TableCell();
                    //ASPxImage newNode = new ASPxImage();
                    //newNode.ImageUrl = "~/lineH.png";
                    //newNode.Width = Unit.Percentage(100);
                    //tdInner.Controls.Add(newNode);
                    tdInner.BackColor = Color.Black;
                    trInner.Cells.Add(tdInner);
                    tbInner.Rows.Add(trInner);
                    td.Controls.Add(tbInner);
                    tr.Cells.Add(td);
                    tb.Rows.Add(tr);

                    tbInner = new Table();
                    tb.Width = Unit.Percentage(100);
                    tb.Height = 100;
                    tbInner = new Table();
                    trInner = new TableRow();
                }
            }
            tbInner.Rows.Add(trInner);
            td.Controls.Add(tbInner);
            tr.Cells.Add(td);
            tb.Rows.Add(tr);

            PanelNode.Controls.Add(tb);
        }
        private ASPxTextBox createNode(string nodeText, string nodeId)
        {

            ASPxTextBox newNode = new ASPxTextBox();
            newNode.Text = nodeText;
            newNode.ID = nodeId;
            newNode.Border.BorderStyle = BorderStyle.Solid;
            newNode.Border.BorderWidth = 1;
            newNode.Height = 100;
            newNode.Width = 170;
            newNode.Enabled = false;
            newNode.Cursor = "pointer";
            newNode.HorizontalAlign = HorizontalAlign.Center;
            //newNode.ClientSideEvents = "function(s,e) {var index = ListNode.GetSelectedIndex();if(index!=-1) {var items = ListNode.GetSelectedItems();PanelNode.PerformCallback(items[0].text);}}";

            return newNode;
        }
        protected void ListNode_Init(object sender, EventArgs e)
        {
            string sql = "select event_id,event_name from demo order by seq";
            dc.setTheSql(sql);
            DataTable dt = dc.GetTable();
            ListNode.DataSource = dt;
            ListNode.DataBind();
        }
    }
}