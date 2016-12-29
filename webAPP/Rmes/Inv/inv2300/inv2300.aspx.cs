using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rmes.DA.Entity;
using Rmes.DA.Factory;
using Rmes.Web.Base;
using Rmes.Pub.Data;
using Rmes.DA.Base;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using System.Data;
////////////////////////////////////////////
/*
 * Inv/inv2300/inv2300.aspx
 * 查询要料单界面，同时可以手工添加要料单
 * 刘征宇 2014/03/20 添加
 * 待测试 
 */
/////////////////////////////////////////////
namespace Rmes.WebApp.Rmes.Inv.inv2300
{
    public partial class inv2300 : BasePage
    {
        userManager user;
        WorkShopEntity workshop;
        protected void Page_Load(object sender, EventArgs e)
        {
            user = Session["theUserManager"] as userManager;


            if (user == null) Response.End();


            userManager theUserManager = (userManager)Session["theUserManager"];
            string theCompanyCode = theUserManager.getCompanyCode();
            string theUserId = theUserManager.getUserId();

            List<WorkShopEntity> workShop = WorkShopFactory.GetUserWorkShops(theUserId);
            
            //ASPxGridView1.CustomColumnDisplayText += new DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventHandler(ASPxGridView1_CustomColumnDisplayText);
            GridViewDataComboBoxColumn column1 = (ASPxGridView1.Columns["LLXZZZ"] as GridViewDataComboBoxColumn);
            {
                column1.PropertiesComboBox.DataSource = UserFactory.GetAll();
                column1.PropertiesComboBox.ValueField = "USER_CODE";
                column1.PropertiesComboBox.TextField = "USER_NAME";
            }
            GridViewDataComboBoxColumn column2 = (ASPxGridView1.Columns["LLZPXZ"] as GridViewDataComboBoxColumn);
            {
                column2.PropertiesComboBox.DataSource = TeamFactory.GetByWorkShopID("CK_WS01");
                column2.PropertiesComboBox.ValueField = "RMES_ID";
                column2.PropertiesComboBox.TextField = "TEAM_NAME";
            }
            GridViewDataComboBoxColumn column3 = (ASPxGridView1.Columns["LLBS"] as GridViewDataComboBoxColumn);
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Columns.Add("value");
                dt.Columns.Add("text");
                dt.Rows.Add("W", "新建");
                dt.Rows.Add("N", "已发库房");
                dt.Rows.Add("Y", "已领料");
                dt.Rows.Add("E", "无价格");
                column3.PropertiesComboBox.DataSource = dt;
                column3.PropertiesComboBox.ValueField = "value";
                column3.PropertiesComboBox.TextField = "text";
            }


            List<InterIssueEntity> allEntity = InterIssueFactory.GetByWorkShopCode("8101-B1");

            GridViewDataComboBoxColumn colGZH = (ASPxGridView1.Columns["LLGZH"] as GridViewDataComboBoxColumn);
            var a = ((from s in allEntity where !string.IsNullOrWhiteSpace(s.LLGZH) select s.LLGZH).Distinct()).ToList<string>();
            colGZH.PropertiesComboBox.DataSource = a;

            GridViewDataComboBoxColumn colGCH = (ASPxGridView1.Columns["LLGCH"] as GridViewDataComboBoxColumn);
            var b = (from s in allEntity select s.LLGCH).Distinct().ToList<string>();;
            colGCH.PropertiesComboBox.DataSource = b;

            GridViewDataComboBoxColumn colZJDH = (ASPxGridView1.Columns["LLZJDH"] as GridViewDataComboBoxColumn);
            var c = (from s in allEntity select s.LLZJDH).Distinct().ToList<string>();
            for (int i = 0; i < c.Count; i++)
            {
                if (c[i] == null)
                    c[i] = "";
            }
            colZJDH.PropertiesComboBox.DataSource = c;

            DataTable d = new DataTable();
            d.Columns.Add("LLGCH");
            d.Columns.Add("LLGZH");
            d.Columns.Add("LLZJDH");
            d.Columns.Add("LLXMDH");
            d.Columns.Add("LLXMMC");
            d.Columns.Add("LLSL");
            d.Columns.Add("YSSL");
            d.Columns.Add("LLRQ");
            d.Columns.Add("LLCJRQ");
            d.Columns.Add("LLZPXZ");
            d.Columns.Add("LLXZZZ");
            d.Columns.Add("PLAN_CODE");
            d.Columns.Add("LLBS");
            d.Columns.Add("TMBH");

            foreach (InterIssueEntity i in allEntity)
            {
                List<IssueReceivedEntity> rs = IssueReceivedFactory.GetByDetailCode(i.TMBH);
                float sum = 0;
                foreach (IssueReceivedEntity r in rs)
                {
                    sum += (int)r.ITEM_QTY;
                }
                d.Rows.Add(i.LLGCH,i.LLGZH,i.LLZJDH,i.LLXMDH,i.LLXMMC,i.LLSL,sum,i.LLRQ,i.LLCJRQ,i.LLZPXZ,i.LLXZZZ,i.PLAN_CODE,i.LLBS,i.TMBH);
            }

            ASPxGridView1.DataSource = d;
            ASPxGridView1.DataBind();
            //用后台的配置开关，设置是否允许手动添加要料单，在code_config_system中
            if (DB.ReadConfigServer("INV2300_ALLOW_MANUAL_ADD").Equals("FALSE"))
                btnNewBill.Visible = false;
            else
            {
                btnNewBill.Visible = true;
                ASPxComboBox cbGCH = ASPxPopupControl1.FindControl("LLGCH") as ASPxComboBox;
                if (cbGCH != null)
                {
                    List<WorkShopEntity> workshops = WorkShopFactory.GetUserWorkShops(user.getUserId());
                    List<ProjectEntity> projects = new List<ProjectEntity>();
                    foreach (WorkShopEntity w in workshops)
                    {
                        projects.AddRange(ProjectFactory.GetByWorkShop(w.RMES_ID));
                    }
                    foreach (ProjectEntity p in projects)
                    {
                        cbGCH.Items.Add(p.PROJECT_CODE + " | " + p.PROJECT_NAME, p.PROJECT_CODE);
                    }
                    cbGCH.ClientSideEvents.SelectedIndexChanged = "function(s,e){ initWorkCode(s.GetValue().toString());}";
                    ASPxComboBox cbGZH = ASPxPopupControl1.FindControl("LLGZH") as ASPxComboBox;
                    if (cbGZH != null)
                    {
                        cbGZH.Callback += new DevExpress.Web.ASPxClasses.CallbackEventHandlerBase(cbGZH_Callback);
                    }
                    cbGCH.SelectedIndexChanged += new EventHandler(cbGCH_SelectedIndexChanged);
                }
                ASPxComboBox cbTeam = ASPxPopupControl1.FindControl("LLZPXZ") as ASPxComboBox;
                if (cbTeam != null)
                {
                    List<TeamEntity> teams = TeamFactory.GetByUserID(user.getUserId());
                    foreach (TeamEntity t in teams)
                    {
                        cbTeam.Items.Add(t.TEAM_NAME, t.TEAM_CODE);
                    }
                }
            }
        }

        public void btnReset_Click(object sender, EventArgs e)
        {
            int i = InterIssueFactory.ResetLLBS();
        }

        void btnNewBill_Click(object sender, EventArgs e)
        {

        }

        void cbGZH_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string projectcode = e.Parameter;
            ASPxComboBox box = sender as ASPxComboBox;
            if (sender == null) return;
            box.DataSource = ProjectFactory.GetDetailsByProjectCode(projectcode);
            box.TextField = "WORK_CODE";
            box.ValueField = "WORK_CODE";
            box.DataBind();
            //throw new NotImplementedException();
        }


        void cbGCH_SelectedIndexChanged(object sender, EventArgs e)
        {
            ASPxComboBox cbGZH = ASPxPopupControl1.FindControl("LLGZH") as ASPxComboBox;
            cbGZH.DataSource = new List<ProjectDetailEntity>();

            ASPxComboBox cbGCH = ASPxPopupControl1.FindControl("LLGCH") as ASPxComboBox;
            if (cbGCH.SelectedIndex < 0) return;
            string gch = cbGCH.SelectedItem.Value.ToString();

            if (cbGZH != null)
            {
                cbGZH.DataSource = ProjectFactory.GetDetailsByProjectCode(gch);
                cbGZH.TextField = "WORK_CODE";
                cbGZH.ValueField = "WORK_CODE";
                cbGZH.DataBind();
            }

            //throw new NotImplementedException();
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            ASPxComboBox cbGCH = ASPxPopupControl1.FindControl("LLGCH") as ASPxComboBox;
            if (cbGCH == null) return;
            ASPxComboBox cbGZH = ASPxPopupControl1.FindControl("LLGZH") as ASPxComboBox;
            if (cbGZH == null) return;
            ASPxTextBox txtPGDH = ASPxPopupControl1.FindControl("LLPGDH") as ASPxTextBox;
            if (txtPGDH == null) return;
            ASPxTextBox txtXMDH = ASPxPopupControl1.FindControl("LLXMDH") as ASPxTextBox;
            if (txtXMDH == null) return;
            ASPxTextBox txtXMMC = ASPxPopupControl1.FindControl("LLXMMC") as ASPxTextBox;
            if (txtXMMC == null) return;
            ASPxTextBox txtLLSL = ASPxPopupControl1.FindControl("LLSL") as ASPxTextBox;
            if (txtLLSL == null) return;
            ASPxComboBox cbTEAM = ASPxPopupControl1.FindControl("LLZPXZ") as ASPxComboBox;
            if (cbTEAM == null) return;
            ASPxDateEdit cbLLRQ = ASPxPopupControl1.FindControl("LYRQ") as ASPxDateEdit;
            if (cbLLRQ == null) return;
            ASPxTextBox txtXMLX = ASPxPopupControl1.FindControl("LLXMLX") as ASPxTextBox;
            if (txtXMLX == null) return;
                 
            string project_code = cbGCH.SelectedItem.Value.ToString();
            string work_code = cbGZH.SelectedItem.Value.ToString();
            string billno = txtPGDH.Text;
            string item_code = txtXMDH.Text;
            string item_name = txtXMMC.Text;
            int item_qty = Convert.ToInt32(txtLLSL.Text);
            string team_cdoe = cbTEAM.SelectedItem.Value.ToString();
            string llxmlx = txtXMLX.Text;
            DateTime need_date = cbLLRQ.Date;
            UserEntity u = UserFactory.GetByID(user.getUserId());
            TeamEntity t = TeamFactory.GetByTeamCode(team_cdoe);
            UserEntity leader = UserFactory.GetByUserCode(t.LEADER_CODE);
            if (string.IsNullOrWhiteSpace(project_code) || string.IsNullOrWhiteSpace(work_code) || string.IsNullOrWhiteSpace(billno)
                || string.IsNullOrWhiteSpace(item_code) || string.IsNullOrWhiteSpace(team_cdoe) || string.IsNullOrWhiteSpace(item_name)
                ||string.IsNullOrWhiteSpace(llxmlx))
                return;
            if (item_qty < 1 || item_qty > 999999) return;
            if ((need_date - DateTime.Now).TotalMinutes < 0) return;

            ProjectEntity project = ProjectFactory.GetByProjectCode(project_code);
            List<WorkShopEntity> workshops = WorkShopFactory.GetUserWorkShops(user.getUserId());
            if (workshops.Count > 0)
                workshop = workshops[0];
            else
                return;

            if (project != null)
            {

                string barCode = DB.GetInstance().ExecuteScalar<string>("select 'RMESL'||TRIM(TO_CHAR(SEQ_ISSUE_BARCODE.NEXTVAL,'0000000000')) from dual");

                InterIssueEntity interEntity = new InterIssueEntity
                {
                    LLGCH = project_code,//领料合同号
                    LLGZH = work_code,//领料工作号
                    LLCPXH = project.PRODUCT_SERIES,//领料产品型号
                    LLZJDH = "MANUAL",//领料组件代号
                    PLAN_CODE = "R" + billno,//领料派工单号 - 临改单
                    LLLYDW = workshop.WORKSHOP_CODE,//领用单位 - 车间
                    LLZPXZ = team_cdoe,//装配小组
                    LLXH = 1,//领料序号
                    LLXMDH = item_code,//项目代号 - 图号
                    LLLYPC = "1",//领用批次
                    LLSL = item_qty,//领料数量
                    LLCJYH = u.USER_CODE + "/" + u.USER_NAME,//单据创建人
                    LLCJRQ = DateTime.Now,//单据创建日期
                    LLNY = need_date.ToString("yyMM"),//领料年月
                    LLRQ = need_date,//领料日期
                    LLXMMC = item_name,//领料项目名称
                    TMBH = barCode,//领料条码编号
                    LLBS = "W",//领料标识
                    LLXZZZ=leader.USER_CODE+"/"+leader.USER_NAME,
                    LLSPYH=u.USER_CODE+"/"+u.USER_NAME,
                    LLR = leader.USER_CODE + "/" + leader.USER_NAME,
                    LLXMLX=llxmlx.ToUpper()
                };
                object obj = DB.GetInstance().Insert("INTER_ISSUE", "", false, interEntity);
                if (obj.ToString().ToUpper().Equals("TRUE"))
                {
                    Response.Write("<script>alert('添加成功！');</script>");
                    Response.Redirect(Request.Url.ToString());
                }
            }
        }

    }
}