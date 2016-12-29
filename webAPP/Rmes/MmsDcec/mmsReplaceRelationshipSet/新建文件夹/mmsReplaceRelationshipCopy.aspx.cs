using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rmes.Pub.Data;
using DevExpress.Web.ASPxEditors;
using Rmes.DA.Factory;

namespace Rmes.WebApp.Rmes.MmsDcec.mmsReplaceRelationshipSet
{
    public partial class mmsReplaceRelationshipCopy : System.Web.UI.Page
    {
        private dataConn dc = new dataConn();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
                ListToSo.ClientSideEvents.ItemDoubleClick = "function(s,e) {var index = ListToSo.GetSelectedIndex();if(index!=-1) ListToSo.RemoveItem(index);}";
        }

        protected void cmbPline_Init(object sender, EventArgs e)
        {
            //显示生产线列表
            userManager theUserManager = (userManager)Session["theUserManager"];
            string userId = theUserManager.getUserId();
            string companyCode = theUserManager.getCompanyCode();
            string sql = "select pline_code,pline_name from vw_user_role_program a "
                + "where user_id='" + userId + "' and program_code='mmsReplaceRelationshipSet' and company_code='" + companyCode + "'";
            dc.setTheSql(sql);
            cmbPline.DataSource = dc.GetTable();
            cmbPline.TextField = "pline_name";
            cmbPline.ValueField = "pline_code";
            cmbPline.DataBind();
        }

        protected void cmbFromSo_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string plineCode = e.Parameter;
            string sql = "  select distinct so from sjbomthset where site='" + plineCode + "' order by so";
            (sender as ASPxComboBox).DataSource = dc.GetTable(sql);
            (sender as ASPxComboBox).ValueField = "so";
            (sender as ASPxComboBox).TextField = "so";
            (sender as ASPxComboBox).DataBind();
        }

        protected void CmdCopy_Click(object sender, EventArgs e)
        {
            List<object> theSetType = ASPxGridView1.GetSelectedFieldValues("SETTYPE");
            List<object> theOldPart = ASPxGridView1.GetSelectedFieldValues("OLDPART");
            List<object> theNewPart = ASPxGridView1.GetSelectedFieldValues("NEWPART");
            List<object> thePeFile = ASPxGridView1.GetSelectedFieldValues("PEFILE");
            List<object> theUseTime = ASPxGridView1.GetSelectedFieldValues("USETIME");
            List<object> theEndTime = ASPxGridView1.GetSelectedFieldValues("ENDTIME");
            List<object> theXl = ASPxGridView1.GetSelectedFieldValues("XL");
            List<object> theGroup = ASPxGridView1.GetSelectedFieldValues("THGROUP");
            
            userManager theUserManager = (userManager)Session["theUserManager"];
            string userName = theUserManager.getUserName();
            string companyCode = theUserManager.getCompanyCode();

            for (int i = 0; i < theSetType.Count; i++)
            {
                if (theSetType[i].ToString() == "1")
                {
                    for (int j = 0; j < ListToSo.Items.Count; j++)
                    {
                        BomReplaceFactory.MW_MODIFY_SJBOMTHSET("ADD3", theOldPart[i].ToString(), theNewPart[i].ToString()
                            , ListToSo.Items[i].Value.ToString(), thePeFile[i].ToString(), cmbPline.Value.ToString()
                            , userName, Convert.ToDateTime(theUseTime[i]).ToString("yyyy-MM-dd"), 
                            Convert.ToDateTime(theEndTime[i]).ToString("yyyy-MM-dd"), theGroup[i].ToString(), "0", theXl[i].ToString());
                    }
                }
                if (theSetType[i].ToString() == "0")
                {
                    for (int j = 0; j < ListToSo.Items.Count; j++)
                    {
                        BomReplaceFactory.MW_MODIFY_SJBOMTHSET("ADD", theOldPart[i].ToString(), theNewPart[i].ToString()
                            , ListToSo.Items[i].Value.ToString(), thePeFile[i].ToString(), cmbPline.Value.ToString()
                            , userName, Convert.ToDateTime(theUseTime[i]).ToString("yyyy-MM-dd")
                            , Convert.ToDateTime(theEndTime[i]).ToString("yyyy-MM-dd"), "", "0", theXl[i].ToString());
                    }
                }
            }

            Response.Write("<script>alert('复制成功！');</script>");
        }

        protected void cmbToSO_Init(object sender, EventArgs e)
        {
            string sql = "select so from ((select distinct so from copy_engine_property)union(select distinct config as so from copy_engine_property)) order by so";
            cmbToSO.DataSource = dc.GetTable(sql);
            cmbToSO.TextField = "so";
            cmbToSO.ValueField = "so";
            cmbToSO.DataBind();
        }

        protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters == "one") {
                string sql = "select THGROUP,SO,OLDPART,NEWPART,PEFILE,CREATEUSER,SITE,USETIME,ENDTIME,CREATETIME,XL,SL,SETTYPE "
                    + "from sjbomthset where settype='0' and site='" + cmbPline.Value.ToString() + "' and so='" + cmbFromSo.Text
                    + "' order by createtime,thgroup";
                ASPxGridView1.DataSource = dc.GetTable(sql);
                ASPxGridView1.DataBind();            
            }
            if (e.Parameters == "multi") {
                string sql = "select THGROUP,SO,OLDPART,NEWPART,PEFILE,CREATEUSER,SITE,USETIME,ENDTIME,CREATETIME,XL,SL,SETTYPE "
                    + "from sjbomthset where settype='1' and site='" + cmbPline.Value.ToString() + "' and so='" + cmbFromSo.Text
                    + "' order by createtime,thgroup";
                ASPxGridView1.DataSource = dc.GetTable(sql);
                ASPxGridView1.DataBind();            
            }
        }
    }
}