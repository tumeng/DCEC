﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;
using Rmes.Pub.Data;
using System.Drawing;
using System.Drawing.Text;
using DevExpress.Web.ASPxUploadControl;
using System.Data;
using System.Collections;
using System.IO;

namespace Rmes.WebApp.Rmes.EpdDcec.epdProcessNote
{
    public partial class epdProcessNoteNew : System.Web.UI.Page
    {
        private dataConn dc = new dataConn();
        private ArrayList la = new ArrayList();
        public string theUserCode, theUserId;

        protected void Page_Load(object sender, EventArgs e)
        {
            //数据库读取装机提示图片路径
            userManager theUserManager = (userManager)Session["theUserManager"];
            string companyCode = theUserManager.getCompanyCode();
            theUserCode = theUserManager.getUserCode();
            theUserId = theUserManager.getUserId();

            if (!IsPostBack)
            {
                
                string sql = "select INTERNAL_VALUE FROM CODE_INTERNAL WHERE COMPANY_CODE='" + companyCode
                    + "' AND INTERNAL_TYPE_CODE='PATH' AND INTERNAL_CODE='PROCESSNOTEPATH'";
                Session["path"] = dc.GetValue(sql);

                //编辑，带出值
                if (Request["opFlag"].ToString() == "edit")
                {
                    string rmesId = Request["rmesId"].ToString();
                    sql = "select * from data_process_note where rmes_id='" + rmesId + "'";
                    DataTable dt=dc.GetTable(sql);
                    cmbPlineType.Value= dt.Rows[0]["pline_code"].ToString();
                    cmbRoutingRemark.Value = dt.Rows[0]["rounting_remark"].ToString();
                    cmbSO.Value = dt.Rows[0]["plan_so"].ToString();
                    cmbComponet.Value = dt.Rows[0]["component_code"].ToString();
                    cmbProcessCode.Value = dt.Rows[0]["process_code"].ToString();

                    if(dt.Rows[0]["from_date"].ToString()!="")
                    DateFrom.Date = Convert.ToDateTime(dt.Rows[0]["from_date"].ToString());
                    if (dt.Rows[0]["to_date"].ToString() != "")
                    DateTo.Date = Convert.ToDateTime(dt.Rows[0]["to_date"].ToString());

                    txtProcessNote.Text = dt.Rows[0]["process_note"].ToString();
                    cmbColor.Text = dt.Rows[0]["process_note_color"].ToString();
                    cmbFont.Value = dt.Rows[0]["process_note_font"].ToString();
                    cmbType.Value = dt.Rows[0]["note_type"].ToString();

                    if (dt.Rows[0]["process_note_color"].ToString() != "")
                        txtProcessNote.ForeColor = Color.FromName(dt.Rows[0]["process_note_color"].ToString());
                    if (dt.Rows[0]["process_note_font"].ToString() != "")
                        txtProcessNote.Font.Size = FontUnit.Point(Convert.ToInt32(dt.Rows[0]["process_note_font"].ToString()));

                    string pics = dt.Rows[0]["process_pic"].ToString();
                    PubCs pc = new PubCs();
                    la= pc.SplitBySeparator(pics, "$");
                    for (int i = 0; i < la.Count; i++)
                    {
                        if (la[i].ToString() != "")
                            ListFiles.Items.Add(la[i].ToString());
                    }
                    cmbType.ClientEnabled = false;
                    cmbPlineType.ClientEnabled = false;
                    cmbRoutingRemark.ClientEnabled = false;
                    cmbSO.ClientEnabled = false;
                    cmbComponet.ClientEnabled = false;
                    cmbProcessCode.ClientEnabled = false;

                    btnConfirm.Visible = false;
                }
                if (Request["opFlag"].ToString() == "add")
                {
                    btnConfirmEdit.Visible = false;
                }

                ListFiles.ClientSideEvents.ItemDoubleClick = "function(s,e) {var index = ListFiles.GetSelectedIndex();if(index!=-1) {if (confirm('确认删除？')) {ListFiles.RemoveItem(index); } else { return; }   }  }";
            }
        }

        protected void cmbPlineType_Init(object sender, EventArgs e)
        {
            //初始化生产线下拉列表
            userManager theUserManager = (userManager)Session["theUserManager"];
            string userId = theUserManager.getUserId();

            string sql = "select a.pline_id pline_code,b.pline_name "
                + "from vw_user_role_program a left join code_product_line b on a.pline_code=b.pline_code "
                + "where a.user_id='" + userId + "' and a.program_code='epdProcessNote' order by pline_name";
            (sender as ASPxComboBox).DataSource = dc.GetTable(sql);
            (sender as ASPxComboBox).ValueField = "pline_code";
            (sender as ASPxComboBox).TextField = "pline_name";
            (sender as ASPxComboBox).DataBind();
        }

        protected void cmbRoutingRemark_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            //根据生产线初始化工艺路线下拉列表
            string plineCode = e.Parameter;
            userManager theUserManager = (userManager)Session["theUserManager"];
            string companyCode = theUserManager.getCompanyCode();

            string sql = "select a.ROUNTING_REMARK "
                + "from DATA_ROUNTING_REMARK a left join code_product_line b on a.pline_code=b.pline_code "
                + "where b.rmes_id='" + plineCode + "' and a.company_code='" + companyCode + "'";
            DataTable dt = dc.GetTable(sql);

            DataRow dr = dt.NewRow();
            dr[0] = "";
            dt.Rows.InsertAt(dr, 0);
            (sender as ASPxComboBox).DataSource = dt;
            (sender as ASPxComboBox).ValueField = "ROUNTING_REMARK";
            (sender as ASPxComboBox).TextField = "ROUNTING_REMARK";
            (sender as ASPxComboBox).DataBind();
        }

        protected void cmbProcessCode_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            //根据生产线初始化工序下拉列表
            string plineCode = e.Parameter;
            userManager theUserManager = (userManager)Session["theUserManager"];
            string companyCode = theUserManager.getCompanyCode();

            string sql = "select a.PROCESS_CODE,a.PROCESS_NAME "
                + "from CODE_PROCESS a  left join code_product_line b on a.pline_code=b.rmes_id "
                + "where a.pline_code='" + plineCode + "' and a.company_code='" + companyCode + "' order by a.PROCESS_CODE";
            DataTable dt = dc.GetTable(sql);

            DataRow dr = dt.NewRow();
            dr[0] = "";
            dt.Rows.InsertAt(dr, 0);            
            (sender as ASPxComboBox).DataSource =dt;
            (sender as ASPxComboBox).ValueField = "PROCESS_CODE";
            (sender as ASPxComboBox).TextField = "PROCESS_CODE";
            (sender as ASPxComboBox).DataBind();
        }
        protected void ASPxCallbackPanel1_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            if (cmbFont.Value != null)
                txtProcessNote.Font.Size = FontUnit.Point(Convert.ToInt32(cmbFont.Value));
            if (cmbColor.Color != null)
                txtProcessNote.ForeColor = cmbColor.Color;
        }

        protected void ASPxUploadControl1_FileUploadComplete(object sender, DevExpress.Web.ASPxUploadControl.FileUploadCompleteEventArgs e)
        {
            string plinecode = "E";
            if (cmbPlineType.Value!=null)
            {
                if (cmbPlineType.Value.ToString() != "")
                    plinecode = dc.GetValue("select pline_code from code_product_line where rmes_id='" + cmbPlineType.Value + "'");
            }
            //catch
            //{
            //    plinecode = "E";
            //}
            if (!Directory.Exists(Session["path"].ToString() + "\\\\" + plinecode))
                Directory.CreateDirectory(Session["path"].ToString() + "\\\\" + plinecode);
            txtpline.Text = plinecode;
            (sender as ASPxUploadControl).SaveAs(Session["path"].ToString() + "\\\\" + plinecode + "\\\\" + (sender as ASPxUploadControl).FileName);
        }
        protected void Confirm_Click(object sender, EventArgs e)
        {
            //确定
            userManager theUserManager = (userManager)Session["theUserManager"];
            string companyCode=theUserManager.getCompanyCode();

            string processPic="";
            foreach (ListEditItem li in ListFiles.Items)
                processPic += li.Text.Substring(li.Text.LastIndexOf("\\") + 1, li.Text.Length -1 - li.Text.LastIndexOf("\\")) + "$";
            if (processPic.Length > 0)
                processPic = processPic.Substring(0, processPic.Length - 1);

            string noteType = cmbType.Value.ToString();


            string routingRemark = "";
            if ((noteType == "A" || noteType == "B") && cmbRoutingRemark.Value != null) routingRemark = cmbRoutingRemark.Value.ToString();
            string so = "";
            if ((noteType == "A" || noteType == "C" || noteType == "E") && cmbSO.Value != null) so = cmbSO.Value.ToString();
            string processCode = "";
            if ((noteType == "A" || noteType == "B" || noteType == "C" || noteType == "D" || noteType == "E") && cmbProcessCode.Value != null) processCode = cmbProcessCode.Value.ToString();
            string componet = "";
            if ((noteType == "D" || noteType == "E") && cmbComponet.Value != null) componet = cmbComponet.Value.ToString();
            string dateFrom = "";
            string dateTo = "";
            if (noteType == "E")
            {
                try
                {
                    dateFrom = Convert.ToDateTime(DateFrom.Text).ToString("yyyy-MM-dd");
                }
                catch
                {
                    dateFrom = "";
                }
                try
                {
                    dateTo = Convert.ToDateTime(DateTo.Text).ToString("yyyy-MM-dd");
                }
                catch
                {
                    dateTo = "";
                }
                //dateFrom = Convert.ToDateTime(DateFrom.Text).ToString("yyyy-MM-dd"); dateTo = Convert.ToDateTime(DateTo.Text).ToString("yyyy-MM-dd"); 
            }
            //取RMES_ID的值
            string sql_rmes_id = "SELECT SEQ_RMES_ID.NEXTVAL FROM DUAL ";
            dc.setTheSql(sql_rmes_id);
            string rmesId = dc.GetTable().Rows[0][0].ToString();

            string sql = "insert into data_process_note"
                +"(rmes_id,company_code,pline_code,rounting_remark,plan_so,process_code,"
                +"process_note,process_note_color,process_note_font,note_type,component_code,"
                + "from_date,to_date,process_pic,INPUT_PERSON,INPUT_TIME)values('" + rmesId + "','" + companyCode + "','" + cmbPlineType.Value.ToString()
                + "','" + routingRemark + "','" + so + "','" + processCode
                + "','" + txtProcessNote.Text + "','" + cmbColor.Text + "','" + cmbFont.Text + "','" + noteType + "','" + componet
                + "','" + dateFrom + "','" + dateTo + "','" + processPic + "','"+theUserId+"',SYSDATE)";
            dc.ExeSql(sql);

            //插入到日志表
            try
            {
                string Sql2 = "INSERT INTO data_process_note_LOG (rmes_id,company_code,pline_code,rounting_remark,plan_so,process_code,"
                    + "process_note,process_note_color,process_note_font,note_type,component_code,from_date,to_date,process_pic,user_code,flag,rqsj)"
                    + " SELECT rmes_id,company_code,pline_code,rounting_remark,plan_so,process_code,"
                    + "process_note,process_note_color,process_note_font,note_type,component_code,from_date,to_date,process_pic,'" + theUserCode + "' , 'ADD', SYSDATE"
                    + " FROM data_process_note WHERE RMES_ID = '" + rmesId + "'";
                dc.ExeSql(Sql2);
            }
            catch
            {
                return;
            }

            Response.Write("<script>window.opener.location.href = '../epdProcessNote/epdProcessNote.aspx';</script>");
            Response.Write("<script>window.location.href = '../epdProcessNote/epdProcessNoteNew.aspx?opFlag=add&rmesId=';</script>");
        }

        protected void ConfirmEdit_Click(object sender, EventArgs e)
        {
            //edit
            string processPic = "";
            //for (int i = 0; i < la.Count; i++)
            //{
            //    processPic += la[i] + ",";
            //}

            foreach (ListEditItem li in ListFiles.Items)
                processPic += li.Text.Substring(li.Text.LastIndexOf("\\") + 1, li.Text.Length - 1 - li.Text.LastIndexOf("\\")) + "$";

            if (processPic.Length > 0)
                processPic = processPic.Substring(0, processPic.Length - 1);

            //插入到日志表
            try
            {
                string Sql2 = "INSERT INTO data_process_note_LOG (rmes_id,company_code,pline_code,rounting_remark,plan_so,process_code,"
                    + "process_note,process_note_color,process_note_font,note_type,component_code,from_date,to_date,process_pic,user_code,flag,rqsj)"
                    + " SELECT rmes_id,company_code,pline_code,rounting_remark,plan_so,process_code,"
                    + "process_note,process_note_color,process_note_font,note_type,component_code,from_date,to_date,process_pic,'" + theUserCode + "' , 'BEFOREEDIT', SYSDATE"
                    + " FROM data_process_note WHERE RMES_ID = '" + Request["rmesId"].ToString() + "'";
                dc.ExeSql(Sql2);
            }
            catch
            {
                return;
            }

            string sql = "update data_process_note set process_note='" + txtProcessNote.Text + "',process_note_color='" + cmbColor.Text
                + "',process_note_font='" + cmbFont.Text + "',from_date='" + DateFrom.Text + "',to_date='" + DateTo.Text 
                + "',process_pic='" + processPic + "',INPUT_PERSON='"+theUserId+"',INPUT_TIME=SYSDATE where rmes_id='" + Request["rmesId"].ToString() + "'";
            dc.ExeSql(sql);

            //插入到日志表
            try
            {
                string Sql2 = "INSERT INTO data_process_note_LOG (rmes_id,company_code,pline_code,rounting_remark,plan_so,process_code,"
                    + "process_note,process_note_color,process_note_font,note_type,component_code,from_date,to_date,process_pic,user_code,flag,rqsj)"
                    + " SELECT rmes_id,company_code,pline_code,rounting_remark,plan_so,process_code,"
                    + "process_note,process_note_color,process_note_font,note_type,component_code,from_date,to_date,process_pic,'" + theUserCode + "' , 'AFTEREDIT', SYSDATE"
                    + " FROM data_process_note WHERE RMES_ID = '" + Request["rmesId"].ToString() + "'";
                dc.ExeSql(Sql2);
            }
            catch
            {
                return;
            }

            Response.Write("<script>window.opener.location.href = '../epdProcessNote/epdProcessNote.aspx';this.close();</script>");
        }

        protected void cmbSO_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            long value = 0;
            if (e.Value == null||!Int64.TryParse(e.Value.ToString(), out value)) //
                return;
            ASPxComboBox comboBox = (ASPxComboBox)source;
            string sql = "SELECT UPPER(PT_PART) PT_PART_ALL FROM COPY_PT_MSTR WHERE (PT_GROUP='58A'  OR PT_GROUP='58B') AND UPPER(PT_PART) like UPPER('%" + e.Value.ToString().ToUpper() + "%') ORDER BY PT_PART";
            comboBox.ValueField = "PT_PART_ALL";
            comboBox.TextField = "PT_PART_ALL";
            comboBox.DataSource = dc.GetTable(sql);
            comboBox.DataBind();
        }

        protected void cmbSO_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            ASPxComboBox comboBox = (ASPxComboBox)source;
            string sql = "SELECT PT_PART_ALL FROM (select UPPER(PT_PART) PT_PART_ALL, row_number()over(order by PT_PART) as rn from COPY_PT_MSTR WHERE (PT_GROUP='58A'  OR PT_GROUP='58B') and  UPPER(PT_PART) LIKE '" + string.Format("%{0}%", e.Filter).ToUpper()
                + "' ) where rn between " + (e.BeginIndex + 1).ToString() + " and " + (e.EndIndex + 1).ToString();
            DataTable dt = dc.GetTable(sql);

            comboBox.ValueField = "PT_PART_ALL";
            comboBox.TextField = "PT_PART_ALL";
            comboBox.DataSource = dt;
            comboBox.DataBind();

        }
        protected void cmbComponet_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            long value = 0;
            if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
                return;
            ASPxComboBox comboBox = (ASPxComboBox)source;
            string sql = "SELECT UPPER(PT_PART) PT_PART_ALL FROM COPY_PT_MSTR WHERE PT_GROUP='O' AND UPPER(PT_PART) like UPPER('%" + e.Value.ToString().ToUpper() + "%') ORDER BY PT_PART";
            comboBox.ValueField = "PT_PART_ALL";
            comboBox.TextField = "PT_PART_ALL";
            comboBox.DataSource = dc.GetTable(sql);
            comboBox.DataBind();
        }

        protected void cmbComponet_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            ASPxComboBox comboBox = (ASPxComboBox)source;
            string sql = "SELECT PT_PART_ALL FROM (select UPPER(PT_PART) PT_PART_ALL, row_number()over(order by PT_PART) as rn from COPY_PT_MSTR WHERE PT_GROUP='O' and UPPER(PT_PART) LIKE '" + string.Format("%{0}%", e.Filter).ToUpper()
                + "') where  rn between " + (e.BeginIndex + 1).ToString() + " and " + (e.EndIndex + 1).ToString();
            DataTable dt = dc.GetTable(sql);

            comboBox.ValueField = "PT_PART_ALL";
            comboBox.TextField = "PT_PART_ALL";
            comboBox.DataSource = dt;
            comboBox.DataBind();
        }

        protected void ASPxCbSubmit_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
        {
            string PicUrl = e.Parameter;
            Response.Write("<script type='text/javascript'>window.open('Rmes/EpdDcec/epdProcessNote/epdPic.aspx?Pic=" + PicUrl+ "');</script>");
            //Response.Write("<script  type='text/javascript'>location.href='../epdProcessNote/epdPic.aspx?Pic=" + PicUrl + "';</script>");
            //Response.Write("<script>window.opener.location.href = '../epdProcessNote/epdProcessNote.aspx';</script>");
            //window.open('/Rmes/EpdDcec/epdProcessNote/epdPic.aspx?Pic=' + Pics);
            //Response.Write("<script>window.opener.location.href = '../epdProcessNote/epdProcessNote.aspx';</script>");
           
        }
    }
}