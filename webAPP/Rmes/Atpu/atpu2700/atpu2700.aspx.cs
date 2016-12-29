using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PetaPoco;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;
using Rmes.Pub.Data;
using Rmes.Web.Base;
using System.Data;
using DevExpress.Web.ASPxGridView;
using Rmes.DA.Procedures;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxClasses;
using System.Windows.Forms;
/**
 * 功能概述：VEPS手动更新
 * 作者：游晓航
 * 创建时间：2016-07-28
 */

    public partial class Rmes_atpu2700 : Rmes.Web.Base.BasePage
    {
        private dataConn dc = new dataConn();
        PubCs thePubCs = new PubCs();
        public string theCompanyCode;
        protected void Page_Load(object sender, EventArgs e)
        {
            userManager theUserManager = (userManager)Session["theUserManager"];
            theCompanyCode = theUserManager.getCompanyCode();
            setCondition();
        }
        private void setCondition()
        {
            string SO, CS;
            SO = ComboSo.Text.Trim();
            CS = ComboCS.Text.Trim();
            string sql = "SELECT  A.* ,B.CSMC  FROM VEPS_CSPZB A LEFT JOIN VEPS_CSRANGE B ON A.CSDM=B.CSDM WHERE ";
            if (SO != "" && CS != "")
            {
                sql = sql + "A.SO='" + ComboSo.Text.Trim() + "'AND A.CSDM='" + ComboCS.Text.Trim() + "'  ORDER BY A.CSDM ";
            }
            else if (SO != "" && CS == "")
            {
                sql = sql + "A.SO='" + ComboSo.Text.Trim() + "'  ORDER BY A.CSDM  ";
            }
            else if (SO == "" && CS != "")
            {
                sql = sql + "A.CSDM='" + ComboCS.Text.Trim() + "'  ORDER BY A.CSDM ";
            }
            else
            {
                sql = sql + "1=1 ORDER BY A.CSDM  ";
            }

            DataTable dt = dc.GetTable(sql);
            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();
            string sql2 = "select distinct so from veps_cspzb order by so ";
            ComboSo.DataSource = dc.GetTable(sql2);
            ComboSo.DataBind();



        }
       
        protected void ComboCSLX_Callback(object sender, CallbackEventArgsBase e)
        {
            string so = e.Parameter;

            string sql = "select DISTINCT  cslxdm from veps_cspzb where so= '" + so + "' order by cslxdm";
            DataTable dt = dc.GetTable(sql);

            ASPxComboBox Cslx = sender as ASPxComboBox;
            Cslx.DataSource = dt;
            Cslx.DataBind();
            Cslx.ValueField = "CSLXDM";
            Cslx.TextField = "CSLXDM";
            Cslx.DataBind();
        }
        protected void ComboCS_Callback(object sender, CallbackEventArgsBase e)
        {
            string so = e.Parameter;

            string sql = "select DISTINCT  csdm from veps_cspzb where so= '" + so + "'   order by csdm";
            DataTable dt = dc.GetTable(sql);

            ASPxComboBox Cs = sender as ASPxComboBox;
            Cs.DataSource = dt;
            Cs.ValueField = "CSDM";
            Cs.TextField = "CSDM";
            Cs.DataBind();
        }
        protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            setCondition();
            ASPxGridView1.Selection.UnselectAll();
        }
        
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                VEPS_SDGX sp = new VEPS_SDGX()
                {

                    // SO1 = e.Values["txtSoQry"].ToString()
                    SO1 = txtSoQry.Text.Trim()


                };

                Procedure.run(sp);

                //MessageBox.Show("VEPS更新成功!", "提示");
                //Show(this, "VEPS更新成功!");
                Response.Write("<script>alert('VEPS更新成功!')</script>");
                // setCondition();
                //e.Cancel = true;
            }
            catch(Exception e1)
            {
                //MessageBox.Show("VEPS更新失败"+e1.Message+"!", "提示");
                //Show(this, "VEPS更新失败" + e1.Message + "!");
                Response.Write("<script>alert('VEPS更新失败!')</script>");
            }
        }
        public static void Show(System.Web.UI.Page page, string msg)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>alert('" + msg.ToString() + "');</script>");
        }
    }
