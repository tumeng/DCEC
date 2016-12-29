using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rmes.Pub.Data;

using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxClasses;
using DevExpress.Web.ASPxClasses.Internal;

namespace Rmes.WebApp.Rmes.MmsDcec.mmsMaterialRequisition
{
    public partial class mmsMaterialRequisitionDetail : System.Web.UI.Page
    {
        private dataConn dc = new dataConn();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["RequisitionDetail"] != null)
            {
                #region  逐行显示
                //string sql = Session["RequisitionDetail"].ToString() + " order by ljbgy,ljdm,ljgw,flgw";
                //DataTable dt = new DataTable();
                ////DataTable dt1 = new DataTable();
                ////dt1.Columns.Add("LJBGY");
                ////dt1.Columns.Add("LJDM");
                ////dt1.Columns.Add("LJMC");
                ////dt1.Columns.Add("LJGW");
                ////dt1.Columns.Add("FLGW");
                ////dt1.Columns.Add("LJDD");
                ////dt1.Columns.Add("JHDM");
                ////dt1.Columns.Add("SO");
                ////dt1.Columns.Add("LJSL");
                ////dt1.Columns.Add("THLJDM");
                ////dt1.Columns.Add("OLDLJDM");
                ////dt1.Columns.Add("FLGW1");
                ////dt1.Columns.Add("BZ");
                ////dt1.Columns.Add("LJGYS");
                //dt = dc.GetTable(sql);
                ////dt1.Rows.Add(dt.Rows[0][0],dt.Rows[0][1], dt.Rows[0][2], dt.Rows[0][3],
                ////      dt.Rows[0][4], dt.Rows[0][5], dt.Rows[0][6], dt.Rows[0][7],
                ////      dt.Rows[0][8], dt.Rows[0][9],dt.Rows[0][10],
                ////      dt.Rows[0][11], dt.Rows[0][12], dt.Rows[0][13]);
                //int j = 0;
                //for (int i = 0; i < dt.Rows.Count-1; i++)
                //{
                //    //if (i == dt.Rows.Count-1)
                //    //    break;
                //    //if (dt.Rows[j][2] as string == dt.Rows[i + 1][2] as string && dt.Rows[j][3] as string == dt.Rows[i + 1][3] as string && dt.Rows[j][4] as string == dt.Rows[i + 1][4] as string &&
                //    //    dt.Rows[j][5] as string == dt.Rows[i + 1][5] as string && dt.Rows[j][6] as string == dt.Rows[i + 1][6] as string && dt.Rows[j][7] as string == dt.Rows[i + 1][] as string)
                //    //{
                //    //    dt.Rows[i+1][2] = "";
                //    //    dt.Rows[i + 1][3] = "";
                //    //    dt.Rows[i + 1][4] = "";

                //    //    dt.Rows[i+1][5] = "";
                //    //    dt.Rows[i + 1][6] = "";
                //    //    dt.Rows[i + 1][7] = "";
                //    //  //  dt1.Rows.Add("AAAA", dt.Rows[i+1][2], dt.Rows[i+1][3],
                //    //  //dt.Rows[i+1][4], dt.Rows[i+1][5], dt.Rows[i+1][6], dt.Rows[i+1][7],
                //    //  //dt.Rows[i+1][8], dt.Rows[i+1][10],
                //    //  //dt.Rows[i+1][11], dt.Rows[i+1][12], dt.Rows[i+1][13],
                //    //  //dt.Rows[i+1][14], dt.Rows[i+1][15]);
                //    //}
                //    if (dt.Rows[j][2] as string == dt.Rows[i][2] as string && dt.Rows[j][3] as string == dt.Rows[i ][3] as string && dt.Rows[j][4] as string == dt.Rows[i][4] as string )
                //    {
                //        dt.Rows[i][2] = "";
                //        dt.Rows[i][3] = "";
                //        dt.Rows[i][4] = "";
                //    }
                //    else
                //    {
                //        j = i;
                    
                //    }
                //}
                #endregion

                #region  逐行显示
                string sql = Session["RequisitionDetail"].ToString() + " order by ljbgy,ljdm,ljgw,flgw";
                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();
                dt1.Columns.Add("LJBGY");
                dt1.Columns.Add("LJDM");
                dt1.Columns.Add("LJMC");
                dt1.Columns.Add("LJGW");
                dt1.Columns.Add("FLGW");
                dt1.Columns.Add("LJDD");
                dt1.Columns.Add("LJSL");
                dt = dc.GetTable(sql);
                if (dt.Rows.Count < 1)
                    return;
                dt1.Rows.Add(dt.Rows[0][2], dt.Rows[0][3], dt.Rows[0][4], dt.Rows[0][5],
                             dt.Rows[0][6], dt.Rows[0][7], dt.Rows[0][14]);
                dt1.Rows.Add("", dt.Rows[0][0], dt.Rows[0][1], dt.Rows[0][8], "", "");
                int j = 0;
                for (int i = 0; i < dt.Rows.Count - 1; i++)
                {
                    if (dt.Rows[j][2] as string == dt.Rows[i + 1][2] as string && dt.Rows[j][3] as string == dt.Rows[i + 1][3] as string && dt.Rows[j][4] as string == dt.Rows[i + 1][4] as string &&
                        dt.Rows[j][5] as string == dt.Rows[i + 1][5] as string && dt.Rows[j][6] as string == dt.Rows[i + 1][6] as string && dt.Rows[j][7] as string == dt.Rows[i + 1][7] as string)
                    {
                        dt1.Rows.Add("", dt.Rows[i + 1][0], dt.Rows[i + 1][1], dt.Rows[i + 1][8], "", "");
                    }
                    else
                    {
                        j = i + 1;
                        dt1.Rows.Add(dt.Rows[i + 1][2], dt.Rows[i + 1][3], dt.Rows[i + 1][4], dt.Rows[i + 1][5], dt.Rows[i + 1][6], dt.Rows[i + 1][7], dt.Rows[i + 1][14]);
                        dt1.Rows.Add("", dt.Rows[i + 1][0], dt.Rows[i + 1][1], dt.Rows[i + 1][8], "", "");
                    }
                }
                #endregion
                ASPxGridView1.DataSource = dt1;
                ASPxGridView1.DataBind();
            }
        }

        protected void BtnExport_Click(object sender, EventArgs e)
        {
            ASPxGridViewExporter1.WriteXlsToResponse("领料单明细");
        }
        private void MergeCells(InternalTableCell cell, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e,string type)
        {
            int startIndex = 0, rowSpan = 0;
            GetMergeInf(ref startIndex, ref rowSpan, e,type);
            if (e.VisibleIndex == startIndex)
                cell.RowSpan = rowSpan;
            else if (e.VisibleIndex <= startIndex + rowSpan)
                cell.Visible = false;
        }

        private void GetMergeInf(ref int startIndex, ref int rowSpan, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e,string type)
        {

            string s = e.GetValue(type) as string;
            //string ljdm = e.GetValue("LJDM") as string;
            for (int i = 0; i < ASPxGridView1.VisibleRowCount; i++)
            {
                string _s = ASPxGridView1.GetRowValues(i, type) as string;
                //string _ljdm = ASPxGridView1.GetRowValues(i, "LJDM") as string;
                if (s == _s)//&& ljdm==_ljdm
                {
                    startIndex = i;
                    rowSpan++;
                }
            }
            startIndex = startIndex - rowSpan + 1;
        }

        protected void ASPxGridView1_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
        {
            //Dictionary<TableCell, int> cellRowSpans = new Dictionary<TableCell, int>();
            //MergeCells((InternalTableCell)e.Row.Cells[0], e, "LJBGY");
            //MergeCells((InternalTableCell)e.Row.Cells[1], e,"LJDM");
            //MergeCells((InternalTableCell)e.Row.Cells[2], e, "LJMC");
            //MergeCells((InternalTableCell)e.Row.Cells[3], e);
            //MergeCells((InternalTableCell)e.Row.Cells[6], e);
            //MergeCells((InternalTableCell)e.Row.Cells[7], e);
        }

         public static void GroupRows(GridView GridView1, int cellNum)
        {
            int i = 0, rowSpanNum = 1;
            while (i < GridView1.Rows.Count - 1)
            {
                GridViewRow gvr = GridView1.Rows[i]; //得到第一行
                for (++i; i < GridView1.Rows.Count; i++)
                {
                    GridViewRow gvrNext = GridView1.Rows[i];//得到下一行
                    if (gvr.Cells[cellNum].Text == gvrNext.Cells[cellNum].Text)//两行文字进行比较
                    {
                        gvrNext.Cells[cellNum].Visible = false; //将该行该单元格视为不可见
                        rowSpanNum++;
                    }
                    else
                    {
                        gvr.Cells[cellNum].RowSpan = rowSpanNum; //当前单元格向下移指定单元格
                        rowSpanNum = 1;
                        break;
                    }
                    if (i == GridView1.Rows.Count - 1)//如果循环至行尾
                    {
                        gvr.Cells[cellNum].RowSpan = rowSpanNum;
                    }
                }
            }
        }
         protected void ASPxGridView2_DataSelect(object sender, EventArgs e)
         {
             //ASPxGridView grid = (ASPxGridView)sender;
             //string id = grid.GetMasterRowFieldValues("LJDM").ToString();

             //if (Session["RequisitionDetail"] != null)
             //{
             //    string sql = Session["RequisitionDetail"].ToString()+ " and ljdm='"+id+"'";
             //    DataTable dt = dc.GetTable(sql);

             //    grid.DataSource = dt;
             //}
            

       
         }
    }
}