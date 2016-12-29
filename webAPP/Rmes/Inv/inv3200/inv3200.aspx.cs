using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxClasses;

//以下引用，如果采用实体类，请全部复制到新页面
using PetaPoco;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;
using Rmes.Pub.Data;
using Rmes.Web.Base;


public partial class Rmes_inv3200 : BasePage
{
    private dataConn dc = new dataConn();
    public DataTable dt = new DataTable();
    private string theUserId;
    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theUserId = theUserManager.getUserId();

        dt.Columns.Add("PROJECT_CODE");
        dt.Columns.Add("ASSEMBLY_CODE");
        dt.Columns.Add("TEAM_CODE");
        dt.Columns.Add("ITEM_CODE");
        dt.Columns.Add("ITEM_NAME");
        dt.Columns.Add("PLAN_QTY");
        dt.Columns.Add("SEND_QTY");
        dt.Columns.Add("RECEIVE_QTY");
        dt.Columns.Add("CALCULATE_QTY");
        if (!IsPostBack)
        {
            List<WorkShopEntity> workShop = WorkShopFactory.GetUserWorkShops(theUserId);
            string strWorkShop = workShop[0].RMES_ID;

            string sql = "select PROJECT_CODE,PROJECT_CODE||' | '||PROJECT_NAME as SHOWNAME from DATA_PROJECT WHERE STATUS='Y'and WORKSHOP_ID = '" + strWorkShop + "' order by project_code";
            ASPxComboBoxProject.DataSource = dc.GetTable(sql);
            ASPxComboBoxProject.TextField = "SHOWNAME";
            ASPxComboBoxProject.ValueField = "PROJECT_CODE";
            ASPxComboBoxProject.DataBind();

            List<TeamEntity> team = TeamFactory.GetByUserID(theUserId);
            List<TeamEntity> orderTeam = (from c in team orderby c.TEAM_NAME select c).ToList<TeamEntity>();

            ASPxComboBoxTeam.DataSource = orderTeam;
            ASPxComboBoxTeam.TextField = "TEAM_NAME";
            ASPxComboBoxTeam.ValueField = "RMES_ID";
            ASPxComboBoxTeam.DataBind();
            ASPxComboBoxTeam.SelectedIndex = -1;
        }
        BindData();
    }

    protected void ASPxComboBoxZJDH_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        //string project = e.Parameter;
        //List<PlanEntity> plans = PlanFactory.GetByProjectCode(project);
        //List<string> zjdh = (from s in plans where s.PROJECT_CODE == project select s.PRODUCT_MODEL).Distinct<string>().ToList<string>();
        //ASPxComboBoxZJDH.DataSource = zjdh;
        //ASPxComboBoxZJDH.DataBind();
    }

    protected void bt_check_Click(object sender, EventArgs e)
    {
        BindData();
    }

    private void BindData()
    {
        string project = "";
        string zjdh = "";
        string teamCode = "";
        if (ASPxComboBoxProject.SelectedIndex >= 0)
        {
            project = ASPxComboBoxProject.SelectedItem.Value as string;
        }
        if (ASPxComboBoxZJDH.SelectedIndex >= 0)
        {
            zjdh = ASPxComboBoxZJDH.SelectedItem.Value as string;
        }
        if (ASPxComboBoxTeam.SelectedIndex >= 0)
        {
            teamCode = ASPxComboBoxTeam.SelectedItem.Value as string;
        }
        if (string.IsNullOrWhiteSpace(project) && string.IsNullOrWhiteSpace(teamCode)) return;
        if (string.IsNullOrWhiteSpace(project))
        {
            List<PlanBomEntity> allBOMs = PlanBOMFactory.GetByTeamID(teamCode);
            if (allBOMs.Count > 0)
            {
                //doWork(allBOMs);
            }
        }
        else if (!string.IsNullOrWhiteSpace(project))
        {

            List<PlanEntity> plans = PlanFactory.GetByProjectCode(project);
            if (plans == null || plans.Count < 1) return;
            if (!string.IsNullOrWhiteSpace(zjdh))
            {
                plans = (from s in plans where s.PRODUCT_MODEL == zjdh select s).ToList<PlanEntity>();
            }
            string[] planCodes = (from s in plans select s.PLAN_CODE).ToArray<string>();
            List<PlanBomEntity> allBOMs = PlanBOMFactory.GetByPlanCodes(planCodes);
            if (!string.IsNullOrWhiteSpace(teamCode))
            {
                allBOMs = (from s in allBOMs where s.TEAM_CODE == teamCode select s).ToList<PlanBomEntity>();
            }
            if (allBOMs.Count > 0)
            {
                //doWork(allBOMs);
            }
        }
    }

    //private void doWork(List<PlanBomEntity> boms)
    //{
    //    string[] planCodes = (from s in boms select s.PLAN_CODE).Distinct<string>().ToArray<string>();
    //    List<PlanEntity> plans = PlanFactory.GetByPlanCodes(planCodes);
    //    var temp = from b in boms join p in plans on b.PLAN_CODE equals p.PLAN_CODE select new { b.ITEM_CODE, b.ITEM_NAME, b.ITEM_QTY, b.TEAM_CODE, p.PROJECT_CODE, p.PRODUCT_MODEL };
    //    var results = from s in temp
    //                  group s by new { s.PROJECT_CODE, s.PRODUCT_MODEL, s.ITEM_CODE, s.TEAM_CODE } into r
    //                  select new { r.Key.PROJECT_CODE, r.Key.PRODUCT_MODEL, r.Key.ITEM_CODE, r.Key.TEAM_CODE, ITEM_QTY = r.Sum(m => m.ITEM_QTY), ITEM_NAME = r.Max(m => m.ITEM_NAME) };
    //    //var results = from s in boms join p in plans on s.PLAN_CODE.Equals(p)  into t
    //    //              group t by new { s.PLAN_CODE, s.ITEM_CODE, s.TEAM_CODE } into r
    //    //              select new { r.Key.PLAN_CODE, r.Key.ITEM_CODE, r.Key.TEAM_CODE, ITEM_QTY = r.Sum(m => m.ITEM_QTY), ITEM_NAME = r.Max(m => m.ITEM_NAME) };
    //    string[] projectCodes = (from s in plans select s.PROJECT_CODE).Distinct<string>().ToArray<string>();
    //    List<InterIssueEntity> allSendIssues = InterIssueFactory.GetByProjectCodes(projectCodes);
    //    foreach (var r in results)
    //    {

    //        List<InterIssueEntity> sends = (from s in allSendIssues
    //                                        where s.LLGCH == r.PROJECT_CODE && s.LLZJDH == r.PRODUCT_MODEL
    //                                            && s.LLXMDH == r.ITEM_CODE && s.LLZPXZ == r.TEAM_CODE
    //                                        select s).Distinct<InterIssueEntity>().ToList<InterIssueEntity>();
    //        int sumSend = 0, sumReceived = 0;
    //        foreach (var s in sends)
    //        {
    //            sumSend += s.LLSL;
    //            List<IssueReceivedEntity> receiveds = IssueReceivedFactory.GetByDetailCode(s.TMBH);
    //            foreach (var re in receiveds)
    //            {
    //                sumReceived += Convert.ToInt32(re.ITEM_QTY);
    //            }
    //        }
    //        dt.Rows.Add(r.PROJECT_CODE, r.PRODUCT_MODEL, r.TEAM_CODE, r.ITEM_CODE, r.ITEM_NAME, r.ITEM_QTY, sumSend, sumReceived, sumReceived - sumSend);

    //    }
    //    ASPxGridView1.DataSource = dt;
    //    GridViewDataComboBoxColumn comProject = ASPxGridView1.Columns["PROJECT_CODE"] as GridViewDataComboBoxColumn;
    //    comProject.PropertiesComboBox.DataSource = projectCodes.ToList<string>();

    //    GridViewDataComboBoxColumn comZJDH = ASPxGridView1.Columns["ASSEMBLY_CODE"] as GridViewDataComboBoxColumn;
    //    comZJDH.PropertiesComboBox.DataSource = (from s in results select s.PRODUCT_MODEL).Distinct<string>().ToList<string>();

    //    GridViewDataComboBoxColumn col1 = ASPxGridView1.Columns["TEAM_CODE"] as GridViewDataComboBoxColumn;

    //    string[] teamcode = (from s in results orderby s.TEAM_CODE select s.TEAM_CODE).Distinct<string>().ToArray<string>();
    //    List<TeamEntity> teams = TeamFactory.GetByRmesID1(teamcode);
    //    List<TeamEntity> listT = (from c in teams orderby c.TEAM_NAME select c).ToList<TeamEntity>();

    //    col1.PropertiesComboBox.DataSource = listT;

    //    //col1.PropertiesComboBox.DataSource = from s in results group s by new { s.TEAM_CODE,s.ITEM_NAME } into r select new { r.Key.TEAM_CODE, r.Key.ITEM_NAME };
    //    //col1.PropertiesComboBox.DataSource = TeamFactory.GetByUserID(theUserId);

    //    col1.PropertiesComboBox.ValueField = "RMES_ID";
    //    col1.PropertiesComboBox.TextField = "TEAM_NAME";
    //    ASPxGridView1.DataBind();
    //}

    protected void btnXlsExport_Click(object sender, EventArgs e)
    {
        ASPxGridViewExporter1.WriteXlsToResponse("缺件统计报表");
    }

}