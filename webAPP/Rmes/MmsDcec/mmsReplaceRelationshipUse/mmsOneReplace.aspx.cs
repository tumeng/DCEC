using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rmes.Pub.Data;
using System.Data;
using Oracle.DataAccess.Client;
using Rmes.DA.Factory;
//2018.8.30 取消限量替换
namespace Rmes.WebApp.Rmes.MmsDcec.mmsReplaceRelationshipUse
{
    public partial class mmsOneReplace : System.Web.UI.Page
    {
        private dataConn dc = new dataConn();
        protected void Page_Load(object sender, EventArgs e)
        {
            LabSO.Text = Request["so"].ToString();
            LabPlanCode.Text = Request["planCode"].ToString();
            LabOldPart.Text = Request["oldPart"].ToString();
            LabNewPart.Text = Request["newPart"].ToString();
            LabLocation.Text = Request["locationCode"].ToString();
            LabPlineCode.Text = Request["plineCode"].ToString();
            string itemQry = Request["itemQry"].ToString();

            //统计当天计划里该零件总数
            string sql = "select sum(item_qty) from VW_DATA_PLAN_STANDARD_BOM where ITEM_CODE='" + LabOldPart.Text + "' and begin_date=to_date(to_char(sysdate,'yyyy-mm-dd'),'yyyy-mm-dd')";
            dc.setTheSql(sql);
            string itemSumNum = dc.GetValue();
            LabSumNum.Text = "0";
            if (itemSumNum != "")
                LabSumNum.Text = itemSumNum;

            //统计当天已替换该零件数量
            sql = "select sum(thsl) from sjbomsoth where jhdm in (select plan_code from data_plan where begin_date=to_date(to_char(sysdate,'yyyy-mm-dd'),'yyyy-mm-dd'))";
            dc.setTheSql(sql);
            string itemNum = dc.GetValue();
            LabNum.Text = "0";
            if (itemNum != "")
                LabNum.Text = itemNum;

            LabRate.Text = "0";
            if (LabSumNum.Text != "0")
                LabRate.Text = Convert.ToString(Convert.ToDecimal(LabNum.Text) / Convert.ToDecimal(LabSumNum.Text));
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            userManager theUserManager = (userManager)Session["theUserManager"];
            string userName = theUserManager.getUserName();

            //已经上线的计划需要走流程确认
            if (PubLogic.isEngOnline(LabPlanCode.Text, LabSO.Text, LabOldPart.Text, LabNewPart.Text, LabLocation.Text, LabPlineCode.Text))
            {

                Response.Write("<script>alert('计划已上线，将提交给调度确认.');</script>");

                BomReplaceFactory.MW_MODIFY_SJBOMTHCFM("ADD", LabPlanCode.Text, LabSO.Text, LabOldPart.Text, LabNewPart.Text,
                    userName, LabLocation.Text, LabPlineCode.Text);
            }
            else
            {
                string sql="select nvl(item_flag,'N') from data_plan where plan_code='"+LabPlanCode.Text+"' ";
                if(dc.GetValue(sql)=="Y")
                {
                    Response.Write("<script>alert('计划已库房确认，不能替换！');</script>");
                    return;
                }
                //判断是否指定供应商
                if (PubLogic.isZdgys(LabPlanCode.Text, LabSO.Text, LabOldPart.Text, LabPlineCode.Text))
                    Response.Write("<script>alert('已经指定供应商，不能替换！');</script>");
                else
                {
                    BomReplaceFactory.PL_INSERT_SJBOMSOTH("ADD", LabSO.Text, LabOldPart.Text, LabNewPart.Text, "", "", "",
                        userName, Request.UserHostAddress, LabPlanCode.Text, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), LabLocation.Text, LabNum.Text,
                        "", "0", LabPlineCode.Text);

                    Response.Write("<script>alert('替换成功');</script>");
                }
            }
        }
    }
}