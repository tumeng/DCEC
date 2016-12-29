using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//以下引用，如果采用实体类，请全部复制到新页面
using PetaPoco;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;
using Rmes.Pub.Data;
using Rmes.Web.Base;
using System.Data;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxClasses;
using DevExpress.Web.ASPxTreeList;

/**
 * 功能概述：质检条码管理
 * 作    者：涂猛
 * 创建时间：2014-09-28
 */


namespace Rmes.WebApp.Rmes.Qms.qms3000
{
    public partial class qms3000 : BasePage
    {
        public Database db = DB.GetInstance();

        dataConn conn = new dataConn();

        protected void Page_Load(object sender, EventArgs e)
        {
                BindData();  
        }

        protected void ASPxTreeList1_CustomCallback(object sender, DevExpress.Web.ASPxTreeList.TreeListCustomCallbackEventArgs e)
        {
            
        }

        private void BindData()
        {
            ASPxTreeList1.ClearNodes();
            string pline = ASPxComboBoxPline.Value as string;
            //List<DetectBarCodeEntity> entity = db.Fetch<DetectBarCodeEntity>("where PLINE_CODE=@0 order by SEQ_LEVEL,SEQ_VALUE", pline);
            //ASPxTreeList1.DataSource = entity;
            //ASPxTreeList1.DataBind();

            //ASPxTreeList1.CellEditorInitialize += new DevExpress.Web.ASPxTreeList.TreeListColumnEditorEventHandler(ASPxTreeList1_CellEditorInitialize);
            //ASPxTreeList1.NodeValidating += new DevExpress.Web.ASPxTreeList.TreeListNodeValidationEventHandler(ASPxTreeList1_NodeValidating);
            //ASPxTreeList1.NodeInserting += new DevExpress.Web.Data.ASPxDataInsertingEventHandler(ASPxTreeList1_NodeInserting);
            //ASPxTreeList1.NodeUpdating += new DevExpress.Web.Data.ASPxDataUpdatingEventHandler(ASPxTreeList1_NodeUpdating);
            //ASPxTreeList1.NodeDeleting += new DevExpress.Web.Data.ASPxDataDeletingEventHandler(ASPxTreeList1_NodeDeleting);
            //ASPxTreeList1.HtmlRowPrepared += new DevExpress.Web.ASPxTreeList.TreeListHtmlRowEventHandler(ASPxTreeList1_HtmlRowPrepared);

            //手动加载节点  解决第一层老是按1，10，2，20，3，4。。。。。。的排序问题
            List<DetectBarCodeEntity> entity = db.Fetch<DetectBarCodeEntity>("where PLINE_CODE=@0 and SEQ_LEVEL=1 order by to_number(seq_value,'99')", pline);

            foreach (var d in entity)
            {
                TreeListNode tln=ASPxTreeList1.AppendNode(d.RMES_ID);
                tln["RMES_ID"] = d.RMES_ID;

                tln["PLINE_CODE"] = d.PLINE_CODE;
                tln["SEQ_NAME"] = d.SEQ_NAME;
                tln["SEQ_VALUE"] = d.SEQ_VALUE;
                tln["SEQ_FATHER"] = d.SEQ_FATHER;
                tln["SEQ_LEVEL"] = d.SEQ_LEVEL;
                List<DetectBarCodeEntity> child = db.Fetch<DetectBarCodeEntity>("where PLINE_CODE=@0 and SEQ_LEVEL=2 and SEQ_FATHER=@1 order by SEQ_VALUE", pline, d.RMES_ID);
                for (int i = 0; i < child.Count; i++)
                {
                    TreeListNode ctln = ASPxTreeList1.AppendNode(child[i].RMES_ID,tln);
                    ctln["RMES_ID"] = child[i].RMES_ID;

                    ctln["PLINE_CODE"] = child[i].PLINE_CODE;
                    ctln["SEQ_NAME"] = child[i].SEQ_NAME;
                    ctln["SEQ_VALUE"] = child[i].SEQ_VALUE;
                    ctln["SEQ_FATHER"] = child[i].SEQ_FATHER;
                    ctln["SEQ_LEVEL"] = child[i].SEQ_LEVEL;
                }
            }
            

        }

        protected void ASPxTreeList1_HtmlRowPrepared(object sender, DevExpress.Web.ASPxTreeList.TreeListHtmlRowEventArgs e)
        {
            //ASPxTextBox level = ASPxTreeList1.FindEditFormTemplateControl("txtSEQLevel") as ASPxTextBox;
            //level.Enabled = false;
            //level.ForeColor = System.Drawing.Color.Gray;
            //level.BackColor = System.Drawing.Color.LightGray;

        }

        protected void ASPxTreeList1_CellEditorInitialize(object sender, DevExpress.Web.ASPxTreeList.TreeListColumnEditorEventArgs e)
        {
            string pline = ASPxComboBoxPline.Value as string;
            string condition = "", sql = "";
            DataTable dt = null;

            if (e.Column.FieldName == "SEQ_FATHER")
            {

                condition = " SEQ_LEVEL='1' and PLINE_CODE='" + pline + "'";

                sql = "select '' as RMES_ID,'无(用于创建根菜单)' as TEXTFIELD from dual UNION select RMES_ID,SEQ_NAME ||'('|| SEQ_VALUE ||')' AS TEXTFIELD from CODE_DETECT_BARCODE where " + condition;
                dt = conn.GetTable(sql);
                ASPxComboBox cb = e.Editor as ASPxComboBox;
                cb.ValueField = "RMES_ID";
                cb.TextField = "TEXTFIELD";
                cb.DataSource = dt;
                cb.DataBind();

                //string father_code = "";
                //if ((!ASPxTreeList1.IsNewNodeEditing))
                //    father_code = ASPxTreeList1.FocusedNode.GetValue("MENU_CODE_FATHER").ToString();
                //else
                //    father_code = ASPxTreeList1.FocusedNode.GetValue("LEAF_FLAG").ToString() == "Y" ? ASPxTreeList1.FocusedNode.GetValue("MENU_CODE_FATHER").ToString() : ASPxTreeList1.FocusedNode.GetValue("MENU_CODE").ToString();
                //cb.SelectedIndex = cb.Items.IndexOfValue(father_code);
            }

            if (e.Column.FieldName == "PLINE_CODE")
            {
                DataTable PlineDT = new DataTable();
                PlineDT.Columns.Add("TEXT");
                PlineDT.Columns.Add("VALUE");
                
                if (pline == "1")
                {
                    PlineDT.Rows.Add("开关柜生产线", "1"); 
                }
                else
                {
                    PlineDT.Rows.Add("断路器生产线", "2");
                }
                ASPxComboBox cb = e.Editor as ASPxComboBox;
                cb.ValueField = "VALUE";
                cb.TextField = "TEXT";
                cb.DataSource = PlineDT;
                cb.DataBind();

                //string father_code = "";
                //if ((!ASPxTreeList1.IsNewNodeEditing))
                //    father_code = ASPxTreeList1.FocusedNode.GetValue("MENU_CODE_FATHER").ToString();
                //else
                //    father_code = ASPxTreeList1.FocusedNode.GetValue("LEAF_FLAG").ToString() == "Y" ? ASPxTreeList1.FocusedNode.GetValue("MENU_CODE_FATHER").ToString() : ASPxTreeList1.FocusedNode.GetValue("MENU_CODE").ToString();
                //cb.SelectedIndex = cb.Items.IndexOfValue(father_code);
            }



            if (ASPxTreeList1.IsNewNodeEditing)
            {
                string must_input_form = "MENU_CODE,MENU_NAME,MENU_NAME_EN,MENU_INDEX,";
                string no_input_form = "SEQ_LEVEL,";

                if (must_input_form.Contains(e.Column.FieldName + ","))
                {
                    e.Editor.Border.BorderWidth = 2;
                }
                if (no_input_form.Contains(e.Column.FieldName + ","))
                {
                    e.Editor.ForeColor = System.Drawing.Color.Gray;
                    e.Editor.BackColor = System.Drawing.Color.LightGray;
                    e.Editor.Font.Italic = true;
                    e.Editor.ReadOnly = true;
                }
                //if (e.Column.FieldName.Equals("SEQ_VALUE"))
                //    e.Editor.Value = ASPxTreeList1.FocusedNode.HasChildren ? (ASPxTreeList1.FocusedNode.ChildNodes.Count + 1).ToString() : (ASPxTreeList1.FocusedNode.ParentNode.ChildNodes.Count + 1).ToString();
            }
            else
            {
                string no_input_form = "SEQ_LEVEL,";
                if (no_input_form.Contains(e.Column.FieldName + ","))
                {
                    e.Editor.ForeColor = System.Drawing.Color.Gray;
                    e.Editor.BackColor = System.Drawing.Color.LightGray;
                    e.Editor.Font.Italic = true;
                    e.Editor.ReadOnly = true;
                }
            }
        }

        protected void ASPxTreeList1_NodeValidating(object sender, DevExpress.Web.ASPxTreeList.TreeListNodeValidationEventArgs e)
        {
           
        }

        protected void ASPxTreeList1_NodeDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string rmes_id = e.Values["RMES_ID"] as string;
            DetectBarCodeEntity entity = new DetectBarCodeEntity()
            {
                RMES_ID = rmes_id
            };
           
            db.Delete(entity);
            e.Cancel = true;
            ASPxTreeList1.CancelEdit();
            BindData();
        }

        protected void ASPxTreeList1_NodeUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            string rmes_id = e.Keys["RMES_ID"] as string;
            
            DetectBarCodeEntity entity = new DetectBarCodeEntity()
            {
                RMES_ID = rmes_id
            };
            entity.SEQ_VALUE = e.NewValues["SEQ_VALUE"] as string;
            entity.SEQ_NAME = e.NewValues["SEQ_NAME"] as string;
            entity.SEQ_FATHER = e.NewValues["SEQ_FATHER"] as string;
            entity.PLINE_CODE = e.NewValues["PLINE_CODE"] as string;
            if (string.IsNullOrEmpty(entity.SEQ_FATHER) || entity.SEQ_FATHER == "f001")
                entity.SEQ_LEVEL = "1";
            else
                entity.SEQ_LEVEL = "2";
            db.Update(entity);
            e.Cancel = true;
            ASPxTreeList1.CancelEdit();
            BindData();
        }

        protected void ASPxTreeList1_NodeInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            DetectBarCodeEntity entity = new DetectBarCodeEntity();
            entity.SEQ_VALUE = e.NewValues["SEQ_VALUE"] as string;
            entity.SEQ_NAME = e.NewValues["SEQ_NAME"] as string;
            entity.SEQ_FATHER = e.NewValues["SEQ_FATHER"] as string;
            entity.PLINE_CODE = e.NewValues["PLINE_CODE"] as string;
            if (string.IsNullOrEmpty(entity.SEQ_FATHER) || entity.SEQ_FATHER == "f001")
                entity.SEQ_LEVEL = "1";
            else
                entity.SEQ_LEVEL = "2";
            db.Insert(entity);
            e.Cancel = true;
            ASPxTreeList1.CancelEdit();
            BindData();
        }

        protected void comSEQFather_Callback(object sender, CallbackEventArgsBase e)
        {
            
        }
    }
}