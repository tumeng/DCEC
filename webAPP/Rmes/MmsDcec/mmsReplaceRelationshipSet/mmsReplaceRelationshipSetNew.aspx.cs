using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rmes.Pub.Data;
using DevExpress.Web.ASPxEditors;
using Rmes.DA.Factory;
using System.Data;
using Oracle.DataAccess.Client;

namespace Rmes.WebApp.Rmes.MmsDcec.mmsReplaceRelationshipSet
{
    public partial class mmsReplaceRelationshipSetNew : System.Web.UI.Page
    {
        private dataConn dc = new dataConn();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //显示所有(实)零件
                //sql = "select pt_part, NVL(pt_DESC2,' ') pt_desc from copy_pt_mstr where pt_phantom=0 and pt_status in ('a','A','b','B','d','D','e','E','l','L','m','M','p','P','t','T') and pt_group in ('raw','RAW','M') order by pt_part";
                
                //string sql = "select pt_part, pt_DESC2 pt_desc from copy_pt_mstr where pt_phantom=0 and pt_status in ('a','A','b','B','d','D','e','E','l','L','m','M','p','P','t','T') and pt_group in ('raw','RAW','M') order by pt_part";
                //dc.setTheSql(sql);
                //cmbPart.DataSource = dc.GetTable();
                //cmbPart.TextField = "pt_part";
                //cmbPart.ValueField = "pt_part";
                //cmbPart.DataBind();

                //cmbPartNew.DataSource = dc.GetTable();
                //cmbPartNew.TextField = "pt_part";
                //cmbPartNew.ValueField = "pt_part";
                //cmbPartNew.DataBind();

                //cmbPartNew2.DataSource = dc.GetTable();
                //cmbPartNew2.TextField = "pt_part";
                //cmbPartNew2.ValueField = "pt_part";
                //cmbPartNew2.DataBind();

                ///2016.7.26 原逻辑是多对多替换的原零件默认显示QAD件和非QAD件，因逻辑冲突，改为不显示非QAD件
                //cmbPart2.DataSource = dc.GetTable();
                //cmbPart2.TextField = "pt_part";
                //cmbPart2.ValueField = "pt_part";
                //cmbPart2.DataBind();

                ListConfig.ClientSideEvents.ItemDoubleClick = "function(s,e) {var index = ListConfig.GetSelectedIndex();if(index!=-1) ListConfig.RemoveItem(index);}";
                lstPtFrom.ClientSideEvents.ItemDoubleClick = "function(s,e) {var index = lstPtFrom.GetSelectedIndex();if(index!=-1) lstPtFrom.RemoveItem(index);}";
                lstPtTo.ClientSideEvents.ItemDoubleClick = "function(s,e) {var index = lstPtTo.GetSelectedIndex();if(index!=-1) lstPtTo.RemoveItem(index);}";
            }
        }
        protected void cmbPart2_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            ///2016.7.26 原逻辑是多对多替换的原零件默认显示QAD件和非QAD件，因逻辑冲突，改为不显示非QAD件
            //ASPxComboBox cmbPline = sender as ASPxComboBox;
            //string plineCode = e.Parameter;

            //string sql = "select pt_part,pt_desc from ((select pt_part,pt_desc2 pt_desc,'0' as flag from copy_pt_mstr where pt_phantom=0 and pt_status in ('a','A','b','B','d','D','e','E','l','L','m','M','p','P','t','T') and pt_group in ('raw','RAW','M'))union(select part as pt_part,'非QAD件' pt_desc,'1' as flag from atpubkflpart t where gzdd='" + plineCode + "' and part_type='1')) as b order by flag,pt_part";
            //dc.setTheSql(sql);

            //cmbPart2.DataSource = dc.GetTable();
            //cmbPart2.TextField = "pt_desc";
            //cmbPart2.ValueField = "pt_part";
            //cmbPart2.DataBind();
        }
        protected void cmbSer_Init(object sender, EventArgs e)
        {
            string sql = "select distinct config from copy_engine_property where config is not null and zt<>'O' order by config";
            dc.setTheSql(sql);
            cmbSer.DataSource = dc.GetTable();
            cmbSer.ValueField = "config";
            cmbSer.TextField = "config";
            cmbSer.DataBind();
        }
        protected void DateFrom2_Init(object sender, EventArgs e)
        {
            (sender as ASPxDateEdit).Date = System.DateTime.Today;
        }
        protected void DateTo2_Init(object sender, EventArgs e)
        {
            (sender as ASPxDateEdit).Date = System.DateTime.Today.AddDays(1);
        }
        protected void DateFrom_Init(object sender, EventArgs e)
        {
            (sender as ASPxDateEdit).Date = System.DateTime.Today;
        }
        protected void DateTo_Init(object sender, EventArgs e)
        {
            (sender as ASPxDateEdit).Date = System.DateTime.Today.AddDays(1);
        }
        //protected void cmbPartNew_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        //{
        //    bool checkNoQAD = Convert.ToBoolean(e.Parameter);
        //    if (checkNoQAD)
        //    {
        //        string sql = "select part pt_part,'非QAD件' pt_desc from atpubkflpart t where gzdd='" + cmbPline.Value.ToString() + "' and part_type='1'";
        //        dc.setTheSql(sql);
        //        (sender as ASPxComboBox).DataSource = dc.GetTable();
        //        (sender as ASPxComboBox).ValueField = "pt_part";
        //        (sender as ASPxComboBox).TextField = "pt_desc";
        //        (sender as ASPxComboBox).DataBind();
        //    }
        //    else
        //    {
        //        string sql = "select pt_part, pt_DESC2 pt_desc from copy_pt_mstr where pt_phantom=0 and pt_status in ('a','A','b','B','d','D','e','E','l','L','m','M','p','P','t','T') and pt_group in ('raw','RAW','M') order by pt_part";
        //        dc.setTheSql(sql);
        //        (sender as ASPxComboBox).DataSource = dc.GetTable();
        //        (sender as ASPxComboBox).ValueField = "pt_part";
        //        (sender as ASPxComboBox).TextField = "pt_desc";
        //        (sender as ASPxComboBox).DataBind();
        //    }
        //}
        //protected void cmbPartNew2_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        //{
        //    bool checkNoQAD2 = Convert.ToBoolean(e.Parameter);
        //    if (checkNoQAD2)
        //    {
        //        string sql = "select part pt_part,'非QAD件' pt_desc from atpubkflpart t where gzdd='" + cmbPline.Value.ToString() + "' and part_type='1'";
        //        dc.setTheSql(sql);
        //        (sender as ASPxComboBox).DataSource = dc.GetTable();
        //        (sender as ASPxComboBox).ValueField = "pt_part";
        //        (sender as ASPxComboBox).TextField = "pt_desc";
        //        (sender as ASPxComboBox).DataBind();
        //    }
        //    else
        //    {
        //        string sql = "select pt_part, pt_DESC2 pt_desc from copy_pt_mstr where pt_phantom=0 and pt_status in ('a','A','b','B','d','D','e','E','l','L','m','M','p','P','t','T') and pt_group in ('raw','RAW','M') order by pt_part";
        //        dc.setTheSql(sql);
        //        (sender as ASPxComboBox).DataSource = dc.GetTable();
        //        (sender as ASPxComboBox).ValueField = "pt_part";
        //        (sender as ASPxComboBox).TextField = "pt_desc";
        //        (sender as ASPxComboBox).DataBind();
        //    }
        //}
        protected void BtnConfirm2_Click(object sender, EventArgs e)
        {
            //多对多替换
            string thisPe = txtPe2.Text.Trim();
            //站点
            string thisSite = cmbPline.Value.ToString();
            //登录人
            userManager theUserManager = (userManager)Session["theUserManager"];
            string userNmae = theUserManager.getUserName();

            int maxCount = Math.Max(lstPtFrom.Items.Count,lstPtTo.Items.Count);
            int minCount = Math.Min(lstPtFrom.Items.Count,lstPtTo.Items.Count);

            string thisPart, thisPartNew;
            string thisFromDate = DateFrom2.Date.ToString("yyyy-MM-dd");
            string thisToDate = DateTo2.Date.ToString("yyyy-MM-dd");
            string thisXL = txtXl.Text.Trim();
            string thisSL;
            //检查零件
            for (int i = 0; i < ListConfig.Items.Count; i++)
            {
                string thisSO = ListConfig.Items[i].Value.ToString();

                //多对多替换如果是3个零件换成5个零件，则插入5条记录，前3条一个对一个，后两条原零件为空
                for (int fromCount = 0; fromCount < maxCount; fromCount++)
                {
                    if (lstPtFrom.Items.Count == maxCount)
                        thisPart = lstPtFrom.Items[fromCount].Value.ToString();
                    else if (fromCount < minCount)
                        thisPart = lstPtFrom.Items[fromCount].Value.ToString();
                    else
                        thisPart = "";

                    if (lstPtTo.Items.Count == maxCount)
                    {
                        string temp = lstPtTo.Items[fromCount].Value.ToString();
                        thisPartNew = temp.Split(';')[0].ToString();
                        thisSL = temp.Split(';')[1].ToString();
                    }
                    else if (fromCount < minCount)
                    {
                        string temp = lstPtTo.Items[fromCount].Value.ToString();
                        thisPartNew = temp.Split(';')[0].ToString();
                        thisSL = temp.Split(';')[1].ToString();
                    }
                    else
                    {
                        thisPartNew = "";
                        thisSL = "0";
                    }

                    string check1 = CheckNoQAD2.Value.ToString().ToUpper();
                    //校验零件
                    string sql = "";
                    if (thisPart.Trim() != "")
                    {
                        sql = "select count(1) from copy_pt_mstr where pt_phantom=0 and pt_status in ('a','A','b','B','d','D','e','E','l','L','m','M','p','P','t','T') and pt_group in ('raw','RAW','M') and upper(pt_part)='" + thisPart + "'";
                        if (dc.GetValue(sql) == "0")
                        {
                            Response.Write("<script>alert('BOM零件" + thisPart + "非法！');</script>");
                            return;
                        }
                    }
                    if (thisPartNew.Trim() != "")
                    {
                        if (check1 == "TRUE")
                        {
                            sql = "select count(1) from atpubkflpart t where gzdd='" + cmbPline.Value.ToString() + "' and part_type='1' and upper(part)='" + thisPartNew + "'";
                            if (dc.GetValue(sql) == "0")
                            {
                                Response.Write("<script>alert('替换零件" + thisPartNew + "非法！');</script>");
                                return;
                            }
                        }
                        else
                        {
                            sql = "select count(1) from copy_pt_mstr where pt_phantom=0 and pt_status in ('a','A','b','B','d','D','e','E','l','L','m','M','p','P','t','T') and pt_group in ('raw','RAW','M') and upper(pt_part)='" + thisPartNew + "'";
                            if (dc.GetValue(sql) == "0")
                            {
                                Response.Write("<script>alert('替换零件" + thisPartNew + "非法！');</script>");
                                return;
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < ListConfig.Items.Count; i++)
            {
                //对每个SO执行一个删除
                string thisSO = ListConfig.Items[i].Value.ToString();
                BomReplaceFactory.MW_MODIFY_SJBOMTHSET("DELETE2", "", "", thisSO, thisPe, thisSite, userNmae, thisFromDate, thisToDate, "", "", "");


                //得到组号
                //BomReplaceFactory.FUNC_GET_NEWSID("MT",out id);
                dataConn theDataConn = new dataConn();
                theDataConn.theComd.CommandType = CommandType.StoredProcedure;
                theDataConn.theComd.CommandText = "FUNC_GET_NEWSID";

                theDataConn.theComd.Parameters.Add("in_type", OracleDbType.Varchar2).Value = "MT";
                theDataConn.theComd.Parameters.Add("id", OracleDbType.Varchar2, 50).Direction = ParameterDirection.Output;

                theDataConn.OpenConn();
                theDataConn.theComd.ExecuteNonQuery();
                string id = theDataConn.theComd.Parameters["id"].Value.ToString();
                theDataConn.CloseConn();

                //多对多替换如果是3个零件换成5个零件，则插入5条记录，前3条一个对一个，后两条原零件为空
                for (int fromCount = 0; fromCount < maxCount; fromCount++)
                {
                    if (lstPtFrom.Items.Count == maxCount)
                        thisPart = lstPtFrom.Items[fromCount].Value.ToString();
                    else if (fromCount < minCount)
                        thisPart = lstPtFrom.Items[fromCount].Value.ToString();
                    else
                        thisPart = "";


                    if (lstPtTo.Items.Count == maxCount)
                    {
                        string temp = lstPtTo.Items[fromCount].Value.ToString();
                        thisPartNew = temp.Split(';')[0].ToString();
                        thisSL = temp.Split(';')[1].ToString();
                    }
                    else if (fromCount < minCount)
                    {
                        string temp = lstPtTo.Items[fromCount].Value.ToString();
                        thisPartNew = temp.Split(';')[0].ToString();
                        thisSL = temp.Split(';')[1].ToString();
                    }
                    else
                    {
                        thisPartNew = "";
                        thisSL = "0";
                    }

                    //根据so循环插入记录
                    BomReplaceFactory.MW_MODIFY_SJBOMTHSET("ADD2", thisPart, thisPartNew, thisSO, thisPe, thisSite, userNmae, thisFromDate, thisToDate,id, thisSL, thisXL);
                }
            }

            Response.Write("<script>alert('新增成功！');window.opener.location.href = '../mmsReplaceRelationshipSet/mmsReplaceRelationshipSet.aspx';window.location.href = '../mmsReplaceRelationshipSet/mmsReplaceRelationshipSetNew.aspx';</script>");
        }
        protected void BtnConfirm_Click(object sender, EventArgs e)
        {
            //一对一替换确定
            string thisPart = cmbPart1.Text.Trim().ToUpper();// cmbPart.Value.ToString();
            string thisPartNew = cmbPartNew1.Text.Trim().ToUpper(); //cmbPartNew.Value.ToString();
            string check1 = CheckNoQAD.Value.ToString().ToUpper();
            //校验零件
            string sql = "select count(1) from copy_pt_mstr where pt_phantom=0 and pt_status in ('a','A','b','B','d','D','e','E','l','L','m','M','p','P','t','T') and pt_group in ('raw','RAW','M') and upper(pt_part)='" + thisPart + "'";
            if (dc.GetValue(sql) == "0")
            {
                Response.Write("<script>alert('BOM零件" + thisPart + "非法！');</script>");
                return;
            }
            if (check1 == "TRUE")
            {
                sql = "select count(1) from atpubkflpart t where gzdd='" + cmbPline.Value.ToString() + "' and part_type='1' and upper(part)='" + thisPartNew + "'";
                if (dc.GetValue(sql) == "0")
                {
                    Response.Write("<script>alert('替换零件" + thisPartNew + "非法！');</script>");
                    return;
                }
            }
            else
            {
                sql = "select count(1) from copy_pt_mstr where pt_phantom=0 and pt_status in ('a','A','b','B','d','D','e','E','l','L','m','M','p','P','t','T') and pt_group in ('raw','RAW','M') and upper(pt_part)='" + thisPartNew + "'";
                if (dc.GetValue(sql) == "0")
                {
                    Response.Write("<script>alert('替换零件" + thisPartNew + "非法！');</script>");
                    return;
                }      
            }

            string thisPe = txtPe.Text.Trim();
            //站点
            string thisSite = cmbPline.Value.ToString();
            //登录人
            userManager theUserManager = (userManager)Session["theUserManager"];
            string userNmae = theUserManager.getUserName();

            string thisFromDate = DateFrom.Date.ToString("yyyy-MM-dd");
            string thisToDate = DateTo.Date.ToString("yyyy-MM-dd");
            string thisXL = txtXl.Text.Trim();

            for (int i = 0; i < ListConfig.Items.Count; i++)
            {
                string thisSO = ListConfig.Items[i].Value.ToString().ToUpper();

                BomReplaceFactory.MW_MODIFY_SJBOMTHSET("ADD", thisPart, thisPartNew, thisSO, thisPe, thisSite, userNmae, thisFromDate, thisToDate, "", "0", thisXL);
            }

            Response.Write("<script>alert('新增成功！');window.opener.location.href = '../mmsReplaceRelationshipSet/mmsReplaceRelationshipSet.aspx';window.location.href = '../mmsReplaceRelationshipSet/mmsReplaceRelationshipSetNew.aspx';</script>");
        }
        protected void ListConfig_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            //添加 all config
            if (e.Parameter == "all")
            {
                (sender as ASPxListBox).Items.Clear();
                for (int i = 0; i < cmbSer.Items.Count; i++)
                    (sender as ASPxListBox).Items.Add(cmbSer.Items[i].Value.ToString());
            }
        }

        protected void cmbPline_Init(object sender, EventArgs e)
        {
            //显示生产线列表
            userManager theUserManager = (userManager)Session["theUserManager"];
            string userId = theUserManager.getUserId() ;
            string companyCode =theUserManager.getCompanyCode();
            string sql = "select pline_code,pline_name from vw_user_role_program a "
                + "where user_id='" + userId + "' and program_code='mmsReplaceRelationshipSet' and company_code='" + companyCode + "'";
            dc.setTheSql(sql);
            cmbPline.DataSource = dc.GetTable();
            cmbPline.TextField = "pline_name";
            cmbPline.ValueField = "pline_code";
            cmbPline.DataBind();
        }
    }
}