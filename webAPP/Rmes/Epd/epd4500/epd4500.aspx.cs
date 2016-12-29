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
using Rmes.DA.Procedures;
using System.Data.Odbc;
using Rmes.Public;
using System.Drawing;
using System.IO;
using DevExpress.Data.PivotGrid;
using System.Data;
using DevExpress.Utils;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;

namespace Rmes.WebApp.Rmes.Epd.epd4500
{
    public partial class epd4500 : BasePage
    {
        private dataConn dc = new dataConn();
        PubCs thePubCs = new PubCs();
        protected void Page_Load(object sender, EventArgs e)
        {
            SetCondition();
        }
        private void SetCondition()
        {
            DataTable dt = dc.GetTable("select * from CODE_YEAR_TAR");
            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();
        }
        protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxTextBox yeartotal = ASPxGridView1.FindEditFormTemplateControl("yeartotal") as ASPxTextBox;
            ASPxTextBox jan = ASPxGridView1.FindEditFormTemplateControl("jan") as ASPxTextBox;
            ASPxTextBox feb = ASPxGridView1.FindEditFormTemplateControl("feb") as ASPxTextBox;
            ASPxTextBox mar = ASPxGridView1.FindEditFormTemplateControl("mar") as ASPxTextBox;
            ASPxTextBox total1 = ASPxGridView1.FindEditFormTemplateControl("total1") as ASPxTextBox;
            ASPxTextBox apr = ASPxGridView1.FindEditFormTemplateControl("apr") as ASPxTextBox;
            ASPxTextBox may = ASPxGridView1.FindEditFormTemplateControl("may") as ASPxTextBox;
            ASPxTextBox june = ASPxGridView1.FindEditFormTemplateControl("june") as ASPxTextBox;
            ASPxTextBox total2 = ASPxGridView1.FindEditFormTemplateControl("total2") as ASPxTextBox;
            ASPxTextBox july = ASPxGridView1.FindEditFormTemplateControl("july") as ASPxTextBox;
            ASPxTextBox aug = ASPxGridView1.FindEditFormTemplateControl("aug") as ASPxTextBox;
            ASPxTextBox sep = ASPxGridView1.FindEditFormTemplateControl("sep") as ASPxTextBox;
            ASPxTextBox total3 = ASPxGridView1.FindEditFormTemplateControl("total3") as ASPxTextBox;
            ASPxTextBox oct = ASPxGridView1.FindEditFormTemplateControl("oct") as ASPxTextBox;
            ASPxTextBox nov = ASPxGridView1.FindEditFormTemplateControl("nov") as ASPxTextBox;
            ASPxTextBox dec = ASPxGridView1.FindEditFormTemplateControl("dec") as ASPxTextBox;
            ASPxTextBox total4 = ASPxGridView1.FindEditFormTemplateControl("total4") as ASPxTextBox;
            ASPxTextBox pro = ASPxGridView1.FindEditFormTemplateControl("PRO") as ASPxTextBox;

            string upSql = "UPDATE CODE_YEAR_TAR SET YEAR_TOTAL='"+yeartotal.Text.Trim()+"',JAN='" + jan.Text.Trim() + "',FEB='" + feb.Text.Trim() + "',MAR='" + mar.Text.Trim() + "',TOTAL1='" + total1.Text.Trim() + "'," +
                         "APR='" + apr.Text.Trim() + "',MAY='" + may.Text.Trim() + "',JUNE='" + june.Text.Trim() + "',TOTAL2='" + total2.Text.Trim() + "'," +
                         "JULY='" + july.Text.Trim() + "',AUG='" + aug.Text.Trim() + "',SEP='" + sep.Text.Trim() + "',TOTAL3='" + total3.Text.Trim() + "'," +
                         "OCT='" + oct.Text.Trim() + "',NOV='" + nov.Text.Trim() + "',DEC='" + dec.Text.Trim() + "',TOTAL4='" + total4.Text.Trim() +
                         "' WHERE  PRO='" + pro.Text.Trim() + "'";
            dc.ExeSql(upSql);

            e.Cancel = true;
            ASPxGridView1.CancelEdit();
            SetCondition();
        }
        protected void ASPxGridView1_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditFormEventArgs e)
        {
            if (!ASPxGridView1.IsNewRowEditing && ASPxGridView1.IsEditing)
            {
                //主键不可以修改
                (ASPxGridView1.FindEditFormTemplateControl("pro") as ASPxTextBox).Enabled = false;
                //按照之前开发的样子，生产线也不可以修改
                //(ASPxGridView1.FindEditFormTemplateControl("ASPxComboBox1") as ASPxComboBox).Enabled = false;
            }
        }
    }
}