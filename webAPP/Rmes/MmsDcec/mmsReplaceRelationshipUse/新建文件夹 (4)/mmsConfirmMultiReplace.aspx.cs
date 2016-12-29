using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rmes.Pub.Data;
using System.Data;
using Rmes.DA.Factory;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;

namespace Rmes.WebApp.Rmes.MmsDcec.mmsReplaceRelationshipUse
{
    public partial class mmsConfirmMultiReplace : System.Web.UI.Page
    {
        private dataConn dc = new dataConn();
        private string plineCode, userName,Plan_code="",Plan_so="";
        protected void Page_Load(object sender, EventArgs e)
        {
            plineCode = Request["plineCode"].ToString();

            userManager theUserManager = (userManager)Session["theUserManager"];
            userName = theUserManager.getUserName();

            if (!IsPostBack)
            {
                DatePlan.Date = System.DateTime.Today;
                Plan_code = "";
                Plan_so = "";
            }
        }

        protected void gridPlan_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            //查询计划
            string sql = "select PLAN_CODE ,PLAN_SO ,PLAN_QTY ,CUSTOMER_NAME ,ONLINE_QTY  FROM DATA_PLAN WHERE BEGIN_DATE=to_date('" + DatePlan.Date.ToString("yyyy-MM-dd") + "','yyyy-mm-dd') AND PLINE_CODE='" + Request["plineCode"].ToString() + "' ORDER BY BEGIN_DATE,PLAN_CODE";
            gridPlan.DataSource = dc.GetTable(sql);
            gridPlan.DataBind();
        }
        protected void grid_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            //由计划显示替换关系
            string par = e.Parameters as string;
            string[] parstr = par.Split(';');

            string flag = parstr[2];
            if (flag == "init")
            {
                string so = parstr[0];
                string planCode = parstr[1];
                
                Plan_code = planCode;
                Plan_so = so;

                string sql = "select '" + planCode + "' plan_code,ljdm1 ,gwmc ,ljdm2 ,gwmc1 ,gxmc1 ,sl ,ygmc ,lrsj ,thgroup ,istrue  "
                    + "from sjbomsothmuti where gzdd='" + plineCode + "' and jhdm='" + planCode + "' and so='" + so + "'";
                (sender as ASPxGridView).DataSource = dc.GetTable(sql);
                (sender as ASPxGridView).DataBind();

                (sender as ASPxGridView).Columns[0].FixedStyle = DevExpress.Web.ASPxGridView.GridViewColumnFixedStyle.Left;
            }

            //全部确认
            if (flag == "all") {

                //全部确认
                try
                {
                    DateTime beginDate = DatePlan.Date;// DateTime.Parse(parstr[0].ToString());
                    int str1 = 0, str2 = 0, str3 = 0;
                    showAlert(this, "开始处理！");
                    string sql12 = "select PLAN_CODE ,PLAN_SO ,PLAN_QTY ,CUSTOMER_NAME ,ONLINE_QTY  FROM DATA_PLAN WHERE BEGIN_DATE=to_date('" + beginDate.ToString("yyyy-MM-dd") + "','yyyy-mm-dd') AND PLINE_CODE='" + Request["plineCode"].ToString() + "' ORDER BY BEGIN_DATE,PLAN_CODE";
                    DataTable dt12 = dc.GetTable(sql12);
                    for (int j = 0; j < dt12.Rows.Count; j++)
                    {
                        string planCode = dt12.Rows[j][0].ToString();
                        string so = dt12.Rows[j][1].ToString();
                        string sql = "select IS_JHOFFLINE('" + planCode + "','" + so + "','" + plineCode + "') from dual";
                        if (dc.GetValue(sql) == "1")
                        {
                            //Response.Write("<script>alert('计划已下线！');</script>");
                            str1 = 1;
                            continue;
                        }

                        sql = "select nvl(item_flag,'N') from data_plan where plan_code='" + planCode + "' ";
                        if (dc.GetValue(sql) == "Y")
                        {
                            str1 = 1;
                            //Response.Write("<script>alert('计划已库房确认，不能替换！');</script>");
                            continue;
                        }

                        sql = "select distinct thgroup  from sjbomsothmuti where gzdd='" + plineCode + "' and jhdm='" + planCode
                            + "' and so='" + so + "'";
                        DataTable dt = dc.GetTable(sql);
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            //判断是否可以替换
                            if (isCanConfirm(dt.Rows[i][0].ToString(), planCode, so) && isCanConfirm2(dt.Rows[i][0].ToString(), planCode, so))
                            {
                                BomReplaceFactory.MW_INSERT_SJBOMSOTHMUTI("CFM", so, "", "", userName, Request.UserHostAddress, planCode, "", "", "", plineCode, dt.Rows[i][0].ToString(), "", "");
                                //Response.Write("<script>alert('处理成功！');</script>");
                            }
                            else
                            {
                                //提交调度确认
                                //Response.Write("<script>alert('计划已上线，将提交给调度确认！');</script>");
                                BomReplaceFactory.MW_MODIFY_SJBOMTHMUTICFM("ADD", planCode, so, dt.Rows[i][0].ToString(), userName, plineCode);
                                //Response.Write("<script>alert('已提交！');</script>");
                                str2 = 1;
                            }
                        }
                    }
                    if (str2 == 1)
                    {
                        grid.JSProperties.Add("cpAlertContent", "计划已上线，将提交给调度确认！");
                        //Response.Write("<script>alert('计划已上线，将提交给调度确认！');</script>");
                    }
                    if (str1 == 1)
                    {
                        grid.JSProperties.Add("cpAlertContent", "存在未处理计划！");
                        //Response.Write("<script>alert('存在未处理计划！');</script>");
                    }
                    else
                    {
                        grid.JSProperties.Add("cpAlertContent", "处理成功！");
                        //Response.Write("<script>alert('处理成功！');</script>");
                    }
                }
                catch (Exception e1)
                {
                    grid.JSProperties.Add("cpAlertContent", "处理失败"+ e1.Message.ToString() + "！");
                }
            }

        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            //全部确认
            //string planCode = gridPlan.SelectedItem.GetValue("PLAN_CODE").ToString();
            //string so = gridPlan.SelectedItem.GetValue("PLAN_SO").ToString();
            string planCode = Plan_code;
            string so = Plan_so;

            string sql = "select IS_JHOFFLINE('" + planCode + "','" + so + "','" + plineCode + "') from dual";
            if (dc.GetValue(sql) == "1")
            {
                Response.Write("<script>alert('计划已下线！');</script>");
                return;
            }

            sql = "select nvl(item_flag,'N') from data_plan where plan_code='" + planCode + "' ";
            if (dc.GetValue(sql) == "Y")
            {
                Response.Write("<script>alert('计划已库房确认，不能替换！');</script>");
                return;
            }

            sql = "select distinct thgroup  from sjbomsothmuti where gzdd='" + plineCode + "' and jhdm='" + planCode
                + "' and so='" + so + "'";
            DataTable dt = dc.GetTable(sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //判断是否可以替换
                if (isCanConfirm(dt.Rows[i][0].ToString(), planCode, so) && isCanConfirm2(dt.Rows[i][0].ToString(), planCode, so))
                {
                    BomReplaceFactory.MW_INSERT_SJBOMSOTHMUTI("CFM", so, "", "", userName, Request.UserHostAddress, planCode, "", "", "", plineCode, dt.Rows[i][0].ToString(), "", "");
                    Response.Write("<script>alert('处理成功！');</script>");
                }
                else
                {
                    //提交调度确认
                    Response.Write("<script>alert('计划已上线，将提交给调度确认！');</script>");
                    BomReplaceFactory.MW_MODIFY_SJBOMTHMUTICFM("ADD", planCode, so, dt.Rows[i][0].ToString(), userName, plineCode);
                    Response.Write("<script>alert('已提交！');</script>");
                }
            }

        }
        private bool isCanConfirm(string group, string planCode, string so)
        {
            //判断计划的原零件是否可以替换
            string sql = "select ljdm1,gwmc from sjbomsothmuti  where gzdd='" + plineCode + "' and jhdm='" + planCode + "' and so='" + so + "' and thgroup='" + group + "' and ljdm1 is not null";
            DataTable dt = dc.GetTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string oldPart = dt.Rows[i][0].ToString();
                string locationCode = dt.Rows[i][1].ToString();

                sql = "select IS_ENGONLINE_MUTI('" + planCode + "','" + so + "','" + group + "','" + locationCode + "','" + plineCode + "') from dual";
                string result = dc.GetValue(sql);
                if (result != "0" && result != "3")
                    return false;
            }
            return true;
        }
        private bool isCanConfirm2(string group, string planCode, string so)
        {
            //判断计划的替换零件是否可以替换
            string sql = "select ljdm2,gwmc1 from sjbomsothmuti  where gzdd='" + plineCode + "' and jhdm='" + planCode + "' and so='" + so + "' and thgroup='" + group + "' and ljdm2 is not null";
            DataTable dt = dc.GetTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string oldPart = dt.Rows[i][0].ToString();
                string locationCode = dt.Rows[i][1].ToString();

                sql = "select IS_ENGONLINE_MUTI('" + planCode + "','" + so + "','" + group + "','" + locationCode + "','" + plineCode + "') from dual";
                string result = dc.GetValue(sql);
                if (result != "0" && result != "3")
                    return false;
            }
            return true;
        }
        protected void cmbGroup_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string par = e.Parameter as string;
            //ASPxComboBox cmbgroup1 = sender as ASPxComboBox;
            string[] parstr = par.Split(';');
            string so = parstr[0];
            string planCode = parstr[1];

            string sql = "select distinct thgroup from sjbomsothmuti where so='" + so + "' and jhdm='" + planCode + "' and gzdd='" + Request["plineCode"].ToString() + "'";
            //cmbGroup.DataSource = dc.GetTable(sql);
            //cmbGroup.TextField = "thgroup";
            //cmbGroup.ValueField = "thgroup";
            //cmbGroup.DataBind();
            (sender as ASPxComboBox).DataSource = dc.GetTable(sql);
            (sender as ASPxComboBox).TextField = "thgroup";
            (sender as ASPxComboBox).ValueField = "thgroup";
            (sender as ASPxComboBox).DataBind();
            

        }
        protected void grid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            //删除
            string isTrue = e.Values["ISTRUE"].ToString();
            string planCode = gridPlan.SelectedItem.GetValue("PLAN_CODE").ToString();
            string so = gridPlan.SelectedItem.GetValue("PLAN_SO").ToString();
            string theGroup = gridPlan.SelectedItem.GetValue("THGROUP").ToString();
            //判断是否已经确认，删除确认过的替换关系增加调度确认功能 20100318
            string sql = "select IS_JHONLINE('" + planCode + "','" + so
                    + "','" + plineCode + "') from dual";

            //if (isTrue == "1")
            if (dc.GetValue(sql) == "1")//计划已上线
            {
                Response.Write("<script>alert('计划已上线，删除将提交调度.');</script>");
                BomReplaceFactory.MW_MODIFY_SJBOMTHMUTICFM_DEL("ADD", planCode, so, theGroup, userName, plineCode);
            }
            else
                BomReplaceFactory.MW_INSERT_SJBOMSOTHMUTI("DELETE", so, "", "", userName, Request.UserHostAddress, planCode, "", "", "", plineCode, theGroup, "", "");
        }

        protected void ASPxCallback1_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
        {
            //按组号确认
            string par = e.Parameter as string;
            string[] parstr = par.Split(';');
            string so = parstr[0];
            string planCode = parstr[1];
            string theGroup = parstr[2];

            if (isCanConfirm(theGroup, planCode, so) && isCanConfirm2(theGroup, planCode, so))
                BomReplaceFactory.MW_INSERT_SJBOMSOTHMUTI("CFM", so, "", "", userName, Request.UserHostAddress, planCode, "", "", "", plineCode, theGroup, "", "");
            else
            {
                //如果已经上线，提交调度确认20061202
                Response.Write("<script>alert('计划已上线，将提交给调度确认！');</script>");
                BomReplaceFactory.MW_MODIFY_SJBOMTHMUTICFM("ADD", planCode, so, theGroup, userName, plineCode);
                Response.Write("<script>alert('已提交！');</script>");
            }
        }
        protected void ASPxCallback3_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
        {

        }
        //前端弹出alert消息
        public static void showAlert(System.Web.UI.Page page, string msg)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>alert('" + msg.ToString() + "');</script>");
        }

        protected void ASPxCallback2_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
        {
            //按组删除
            string par = e.Parameter as string;
            string[] parstr = par.Split(';');
            string so = parstr[0];
            string planCode = parstr[1];
            string theGroup = parstr[2];

            string sql = "select IS_JHONLINE('" + planCode.ToString() + "','" + so.ToString() + "','" + plineCode + "') from dual";

            //判断是否已经确认，删除确认过的替换关系增加调度确认功能 20100318
            //string sql = "select istrue from sjbomsothmuti where gzdd='" + plineCode + "' and jhdm='" + planCode + "' and so='" + so + "' and thgroup='" + theGroup + "'";
            if (dc.GetValue(sql) == "1")
            {
                Response.Write("<script>alert('组号" + theGroup + "的替换关系已经上线，删除将提交调度.');</script>");
                BomReplaceFactory.MW_MODIFY_SJBOMTHMUTICFM_DEL("ADD", planCode, so, theGroup, userName, plineCode);
            }
            else
                BomReplaceFactory.MW_INSERT_SJBOMSOTHMUTI("DELETE", so, "", "", userName, Request.UserHostAddress, planCode, "", "", "", plineCode, theGroup, "", "");
        }
    }
}