using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Rmes.WinForm.Base;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Factory;
using System.Reflection;
using Rmes.Pub.Data1;


namespace Rmes.WinForm.Controls
{
    public partial class ctrlNavigation : Rmes.WinForm.Base.BaseControl
    {
        public string CompanyCode, StationCode, WorkunitCode, Thesn="",TheSmTm="",ThePlanCode="",oldPlinecode="";
        //dataConn dc = new dataConn();
        public ctrlNavigation()
        {
            InitializeComponent();

            StationCode = LoginInfo.StationInfo.STATION_CODE;
            CompanyCode = LoginInfo.CompanyInfo.COMPANY_CODE;
            //string work_unit = StationFactory.GetByKey(StationCode).WORKUNIT_CODE;
            //string work_unit = "";
            //List<DetectDataEntity> lst_detect_item = DetectDataFactory.GetByWorkunit(CompanyCode, work_unit);
            //mnuQuality.Enabled = lst_detect_item.Count == 0 ? false : true;
            if (LoginInfo.StationInfo.STATION_TYPE == "ST15")
            {
                mnuQuality.Visible = true;
            }
            else
            {
                mnuQuality.Visible = false;
            }
            if (LoginInfo.ProductLineInfo.PLINE_CODE == "CL")
            {
                报废处理ToolStripMenuItem.Visible = true;
            }
            else
            {
                报废处理ToolStripMenuItem.Visible = false;
            }
            if (LoginInfo.StationInfo.STATION_TYPE == "ST05")
            {
                改制BOM对比清单ToolStripMenuItem.Visible = true;
            }
            else
            {
                改制BOM对比清单ToolStripMenuItem.Visible = false;
            }
            this.RMesDataChanged += new Rmes.WinForm.Base.RMesEventHandler(ctrlNavigation_RmesDataChanged);
            //RMESEventArgs arg = new RMESEventArgs();
            //arg.MessageHead = "WORK";
            //arg.MessageBody = "back to work";
            //SendDataChangeMessage(arg);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.menu.Show(button1,new Point(0,0));
        }

        private void issueItem_Click(object sender, EventArgs e)
        {

            //BaseForm form = (BaseForm)this.ParentForm;
            //if (!form.Text.Equals(LoginInfo.CompanyInfo.COMPANY_NAME_BRIEF + "MES系统"))
            //{
            //    form.Owner.Show();
            //    form.Dispose();
            //}

            //WorkunitCode = StationFactory.GetByKey(StationCode).WORKUNIT_CODE;
            //LineSideStoreEntity ent = LinesideStoreFactory.GetByWKUnit(CompanyCode, WorkunitCode);
            //if (ent == null)
            //{
            //    MessageBox.Show("此工作中心不存在相应的线边库");
            //    return;
            //}
            //RMESEventArgs arg = new RMESEventArgs();
            //arg.MessageHead = "MESLL";
            //arg.MessageBody = "ready to received";
            //SendDataChangeMessage(arg);

            
        }

        private void workItem_Click(object sender, EventArgs e)
        {
            RMESEventArgs arg = new RMESEventArgs();
            arg.MessageHead = "WORK";
            arg.MessageBody = "back to work";
            SendDataChangeMessage(arg);
            BaseForm form = (BaseForm)this.ParentForm;
            if (!form.Text.Equals(LoginInfo.CompanyInfo.COMPANY_NAME_BRIEF + "MES系统"))
            {
                form.Owner.Show();
                form.Dispose();
            }
        }

        private void mnuQuality_Click(object sender, EventArgs e)
        {
            //质量检验 质检站点
            if (Thesn == "" || StationCode == "")
            {
                MessageBox.Show("请输入发动机流水号","提示");
                return;
            }
            frmQa ps = new frmQa("B", Thesn, StationCode);
            ps.StartPosition = FormStartPosition.Manual;
            ps.WindowState = FormWindowState.Maximized;
            //ps.Show(this);
            ps.ShowDialog();

            //RMESEventArgs arg = new RMESEventArgs();
            //arg.MessageHead = "QUA";
            //arg.MessageBody = "Quality Detect";
            //SendDataChangeMessage(arg);

            //BaseForm form = (BaseForm)this.ParentForm;
            //if (!form.Text.Equals(LoginInfo.CompanyInfo.COMPANY_NAME_BRIEF + "MES系统"))
            //{
            //    form.Owner.Show();
            //    form.Dispose();
            //}
        }

        protected void ctrlNavigation_RmesDataChanged(object obj, Rmes.WinForm.Base.RMESEventArgs e)
        {
            //if (e.MessageHead == "QUACTRL") mnuQuality.Enabled = true;
            RMESEventArgs arg = new RMESEventArgs();
            arg.MessageHead = "";
            arg.MessageBody = "";
            if (e.MessageHead == null) return;
            if (e.MessageHead == "SN" || e.MessageHead == "RECHECK" || e.MessageHead == "CHECKOK")
            {
                Thesn = e.MessageBody.ToString();
            }
            if (e.MessageHead == "PRINTISDE")
            {
                string[] txt = e.MessageBody.ToString().Split('|');
                TheSmTm = txt[0];
                ThePlanCode=txt[1];
            }
            if (e.MessageHead == "SHOWBOMLQ")
            {
                string txt = e.MessageBody.ToString();
                ProductInfoEntity product = ProductInfoFactory.GetByCompanyCodeSNSingle(LoginInfo.CompanyInfo.COMPANY_CODE, txt);
                if (product == null) return;
                string oldplancode = "";
                oldplancode = dataConn.GetValue("select jhmcold from atpujhsn where jhmc='" + product.PLAN_CODE + "'  and is_valid='N' and rownum=1");
                if (oldplancode == "")
                    return;

                ProductInfoEntity productold = ProductInfoFactory.GetByCompanyCodeSNSingleLQ(LoginInfo.CompanyInfo.COMPANY_CODE, txt, oldplancode);

                oldPlinecode = productold.PLINE_CODE;
                
            }
        }

        private void reload_Click(object sender, EventArgs e)
        {
            //重新登录
            BaseForm tempForm = (BaseForm)this.ParentForm;

            ClearEvents(tempForm);
            Application.Restart();
            //BaseForm tempForm = (BaseForm)this.ParentForm;
            //tempForm.FormClosing += new FormClosingEventHandler(tempForm_FormClosing);
            //tempForm.FormClosed += new FormClosedEventHandler(tempForm_FormClosed);

            //frmReLogin reload = new frmReLogin();
            //reload.Show(tempForm);
            //tempForm.Hide();
        }
        /// <summary>
        /// 清除一个对象的某个事件所挂钩的delegate
        /// </summary>
        /// <param name="ctrl">控件对象</param>
        /// <param name="eventName">事件名称，默认的</param>
        public void ClearEvents(object ctrl, string eventName = "_EventAll")
        {
            if (ctrl == null) return;
            BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.IgnoreCase | BindingFlags.Static;
            EventInfo[] events = ctrl.GetType().GetEvents(bindingFlags);
            if (events == null || events.Length < 1) return;

            for (int i = 0; i < events.Length; i++)
            {
                try
                {
                    EventInfo ei = events[i];
                    //只删除指定的方法，默认是_EventAll，前面加_是为了和系统的区分，防以后雷同
                    if (eventName != "_EventAll" && ei.Name != eventName) continue;

                    /********************************************************
                     * class的每个event都对应了一个同名(变了，前面加了Event前缀)的private的delegate类
                     * 型成员变量（这点可以用Reflector证实）。因为private成
                     * 员变量无法在基类中进行修改，所以为了能够拿到base 
                     * class中声明的事件，要从EventInfo的DeclaringType来获取
                     * event对应的成员变量的FieldInfo并进行修改
                     ********************************************************/
                    FieldInfo[] fis = ei.DeclaringType.GetFields(bindingFlags);
                    FieldInfo fi = ei.DeclaringType.GetField(("EVENT_" + ei.Name).ToUpper(), bindingFlags);
                    if (fi != null)
                    {
                        // 将event对应的字段设置成null即可清除所有挂钩在该event上的delegate
                        fi.SetValue(ctrl, null);
                    }
                }
                catch { }
            }
        }

        void tempForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        void tempForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        

        private void mnuPrintSn_Click(object sender, EventArgs e)
        {
            //BaseForm form;
            //BaseForm tempForm = (BaseForm)this.ParentForm;
            //if (tempForm.Text.Equals("条码打印")) return;
            //if (tempForm.Text.Equals(LoginInfo.CompanyInfo.COMPANY_NAME_BRIEF + "MES系统"))
            //{
            //    form = tempForm;
            //}
            //else
            //{
            //    form = (BaseForm)tempForm.Owner;
            //    tempForm.Dispose();
            //}

            //FrmBarCode checkForm = new FrmBarCode(form);

            //checkForm.Show(form);
            //form.Hide();
        }

        private void 条码打印ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //打印条码
            if (LoginInfo.ProductLineInfo.PLINE_TYPE_CODE == "B") //分装
            {
                frmPrintTm ps = new frmPrintTm(TheSmTm,ThePlanCode);
                ps.StartPosition = FormStartPosition.Manual;
                ps.StartPosition = FormStartPosition.CenterScreen;
                ps.Show(this);
            }
            else
            {
                frmPrintTm ps = new frmPrintTm();
                ps.StartPosition = FormStartPosition.Manual;
                ps.StartPosition = FormStartPosition.CenterScreen;
                ps.Show(this);
            }
        }

        private void 工艺文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //查看工艺文件
            frmProcessFile ps = new frmProcessFile(Thesn);
            ps.StartPosition = FormStartPosition.Manual;
            ps.StartPosition = FormStartPosition.CenterScreen;
            ps.Show(this);
        }

        private void 报废处理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //分装CL报废处理
            DialogResult drt = MessageBox.Show("执行该功能将对缸盖进行报废处理，要继续吗？", "提示", MessageBoxButtons.YesNo);
            if (drt == DialogResult.No) return;

            string ghtm = Microsoft.VisualBasic.Interaction.InputBox("请输入缸盖流水号", "输入");
            if (ghtm.Trim() == "")
            {
                return;
            }
            string outstr = "";
            ProductDataFactory.WGG_MODIFY_JHB(ghtm, LoginInfo.ProductLineInfo.PLINE_CODE, out outstr);
            if (outstr != "GOOD")
            {
                MessageBox.Show(outstr,"提示");
            }
            //刷新计划
            RMESEventArgs arg = new RMESEventArgs();
            arg.MessageHead = "REFRESHCLPLAN";
            arg.MessageBody = "";
            SendDataChangeMessage(arg);
        }

        private void 装机图片ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LoginInfo.ProductLineInfo.PLINE_CODE == "L")
            {
                FrmShowProcessPic ps = new FrmShowProcessPic("", oldPlinecode);
                ps.StartPosition = FormStartPosition.Manual;
                ps.Location = new Point(0, 400);
                ps.Show();
            }
            else
            {
                //显示装机图片
                FrmShowProcessPic ps = new FrmShowProcessPic("A");
                ps.StartPosition = FormStartPosition.Manual;
                ps.StartPosition = FormStartPosition.CenterScreen;
                ps.Show(this);
            }
        }

        private void 当天计划ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmShowPlan ps = new FrmShowPlan();
            ps.StartPosition = FormStartPosition.Manual;
            ps.StartPosition = FormStartPosition.CenterScreen;
            ps.Show(this);
        }

        private void 本站点完成情况ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmShowCompleteStatistics ps = new FrmShowCompleteStatistics();
            ps.StartPosition = FormStartPosition.Manual;
            ps.StartPosition = FormStartPosition.CenterScreen;
            ps.Show(this);
            //DataTable dt = new DataTable();
            //Form frm_complete = new Form();
            //frm_complete.Text = "完工情况查看";
            //frm_complete.WindowState = FormWindowState.Maximized;
            //DataGridView gvc = new DataGridView();
            //gvc.Columns.Add("colplan_code", "计划号");
            //gvc.Columns.Add("colsn", "SN");
            //gvc.Columns.Add("colso", "SO");
            //gvc.Columns.Add("colpmodel", "机型");
            //gvc.Columns.Add("colteam", "班组");
            //gvc.Columns.Add("colstime", "开始时间");
            //gvc.Columns.Add("coletime", "结束时间");
            //gvc.Columns.Add("colempty", "");
            ////gvc.Columns[0].DataPropertyName = "PLAN_CODE";
            ////gvc.Columns[1].DataPropertyName = "SN";
            ////gvc.Columns[2].DataPropertyName = "PLAN_SO";
            ////gvc.Columns[3].DataPropertyName = "PRODUCT_MODEL";
            ////gvc.Columns[4].DataPropertyName = "TEAM_CODE";
            ////gvc.Columns[5].DataPropertyName = "START_TIME";
            ////gvc.Columns[6].DataPropertyName = "COMPLETE_TIME";
            //gvc.Columns[7].Width = Screen.PrimaryScreen.Bounds.Width - 613;
            //gvc.Columns[0].Width = 90;
            //gvc.Columns[1].Width = 90;
            //gvc.Columns[2].Width = 90;
            //gvc.Columns[4].Width = 90;
            //gvc.Columns[5].Width = 120;
            //gvc.Columns[6].Width = 120;
            //gvc.Columns[4].Visible = false;
            //string sql = string.Format("select * FROM data_complete t where t.SHIFT_CODE='{0}' "
            //                            + "and t.team_code='{1}' and t.station_code='{2}' and "
            //                            + "T.START_TIME>TO_DATE(to_char(SYSDATE,'yyyy/mm/dd')||'00:00:00','yyyy/mm/dd hh24:mi:ss')",
            //                            LoginInfo.ShiftInfo.SHIFT_CODE,
            //                            LoginInfo.TeamInfo.TEAM_CODE, LoginInfo.StationInfo.RMES_ID);
            //dt = dataConn.GetTable(sql);
            //gvc.Height = 500;
            //gvc.Width = 1000;
            //gvc.RowHeadersVisible = false;
            //gvc.Dock = System.Windows.Forms.DockStyle.Fill;
            //gvc.AllowUserToAddRows = false;
            //gvc.AutoGenerateColumns = false;
            //gvc.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            //DataRow dr = dt.NewRow();
            //dr[1] = "合计："+dt.Rows.Count.ToString();
            //dt.Rows.Add(dr);
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    gvc.Rows.Add();
            //    Color color = Color.White;
            //    if (i % 2 == 0) //奇数行显示浅蓝色
            //    {
            //        color = Color.FromArgb(208, 247, 252);//Color.FromArgb(144,220,238)  Color.FromArgb(144, 220, 238)
            //    }
            //    for (int j = 0; j < gvc.Columns.Count; j++)
            //    {
            //        gvc.Rows[i].Cells[j].Style.BackColor = color;

            //    }
            //    gvc.Rows[i].Cells[0].Value = dt.Rows[i]["PLAN_CODE"];
            //    gvc.Rows[i].Cells[1].Value = dt.Rows[i]["SN"];
            //    gvc.Rows[i].Cells[2].Value = dt.Rows[i]["PLAN_SO"];
            //    gvc.Rows[i].Cells[3].Value = dt.Rows[i]["PRODUCT_MODEL"];
            //    gvc.Rows[i].Cells[4].Value = dt.Rows[i]["TEAM_CODE"];
            //    gvc.Rows[i].Cells[5].Value = dt.Rows[i]["START_TIME"];
            //    gvc.Rows[i].Cells[6].Value = dt.Rows[i]["COMPLETE_TIME"];
            //}
            ////Label lab1 = new Label();
            ////lab1.Text = dt.Rows.Count.ToString();

            //frm_complete.Width = 1000;
            //frm_complete.Height = 500;
            //frm_complete.Top = 6;
            //frm_complete.Left = 6;
            ////frm_complete.Controls.Add(lab1);
            //frm_complete.Controls.Add(gvc);
            //frm_complete.Show(this);
            //gvc.ClearSelection();
        }

        private void 改制BOM对比清单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //显示改制BOM对比清单 及 改制BOM扫描结果 及检测数据扫描结果
            if (Thesn == "")
            {
                MessageBox.Show("请扫描发动机流水号", "提示");
                return;
            }
            ProductInfoEntity product = ProductInfoFactory.GetByCompanyCodeSNSingle(CompanyCode, Thesn);//获取sn信息
            if (product == null)
            {
                MessageBox.Show("无此发动机信息", "提示");
                return;
            }
            ThePlanCode = product.PLAN_CODE;
            FrmShowGZqd ps = new FrmShowGZqd(Thesn,ThePlanCode);
            ps.StartPosition = FormStartPosition.Manual;
            ps.StartPosition = FormStartPosition.CenterScreen;
            ps.Show(this);

        }

        //private void mnuPrintQual_Click(object sender, EventArgs e)
        //{
        //    BaseForm form;
        //    BaseForm tempForm = (BaseForm)this.ParentForm;
        //    if (tempForm.Text.Equals("断路器质检报告打印")) return;
        //    if (tempForm.Text.Equals(LoginInfo.CompanyInfo.COMPANY_NAME_BRIEF + "MES系统"))
        //    {
        //        form = tempForm;
        //    }
        //    else
        //    {
        //        form = (BaseForm)tempForm.Owner;
        //        tempForm.Dispose();
        //    }

        //    //frmPrintQual checkForm = new frmPrintQual(form);

        //    //checkForm.Show(form);
        //    //form.Hide();

        //}

        

        //private void mnuPrintQual_1_Click(object sender, EventArgs e)
        //{
        //    BaseForm form;
        //    BaseForm tempForm = (BaseForm)this.ParentForm;
        //    if (tempForm.Text.Equals("开关柜质检报告打印")) return;
        //    if (tempForm.Text.Equals(LoginInfo.CompanyInfo.COMPANY_NAME_BRIEF + "MES系统"))
        //    {
        //        form = tempForm;
        //    }
        //    else
        //    {
        //        form = (BaseForm)tempForm.Owner;
        //        tempForm.Dispose();
        //    }

        //    //frmPrintQual_1 checkForm = new frmPrintQual_1(form);

        //    //checkForm.Show(form);
        //    //form.Hide();
        //}


        
    }
}
