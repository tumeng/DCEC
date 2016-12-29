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
using Rmes.DA.Factory;
using Rmes.DA.Entity;

namespace Rmes.WinForm.Controls
{
    public partial class ctrlAndonCall : BaseControl
    {
        Timer timer1 = new Timer();
        string currentBtnText = "";
        string onandonBtnText = "正在呼叫...";
        string afterandonBtntext = "";
        int status = 0;//0 is normal, 1 is calling, 2 is called. when 1 and 2, you can cancel andon call; by liuzhy
        Dictionary<andontype, string> dict = new Dictionary<andontype, string>();
        AndonAlertEntity entity1;

        public ctrlAndonCall()
        {
            InitializeComponent();
            this.RMesDataChanged += new RMesEventHandler(ctrlAndonCall_RMesDataChanged);
            timer1.Interval = 2000;//Andon Message will be sent after this time; by liuzhy
            timer1.Stop();
            timer1.Tick += new EventHandler(timer1_Tick);
            currentBtnText = btnAndon.Text;

            dict.Add(andontype.quality, "已发出质量呼叫");
            dict.Add(andontype.process, "已发出装配呼叫");
            dict.Add(andontype.mat, "已发出物料呼叫");
            label1.Visible = false;
        }

        void ctrlAndonCall_RMesDataChanged(object obj, RMESEventArgs e)
        {
            if (e.MessageHead == "MESLL")
            {
                this.Visible = false;
                return;
            }
            if (e.MessageHead.ToString() == "WORK" || e.MessageHead.ToString() == "QUA")
            {
                this.Visible = true;
                return;
            }
        }

        void timer1_Tick(object sender, EventArgs e)
        {
            if (status != 1 || status == 2) return;
            timer1.Stop();
            entity1 = new AndonAlertEntity()
            {
                ANDON_ALERT_CONTENT = afterandonBtntext,
                ANDON_ALERT_TIME = DateTime.Now,
                ANDON_TYPE_CODE = "A",
                ANSWER_FLAG = "N",
                COMPANY_CODE = LoginInfo.CompanyInfo.COMPANY_CODE,
                EMPLOYEE_CODE = LoginInfo.UserInfo.USER_ID,
                LOCATION_CODE = LoginInfo.StationInfo.RMES_ID,
                PLINE_CODE = LoginInfo.ProductLineInfo.RMES_ID,
                REPORT_FLAG = "Y",
                STOP_FLAG = "1",
                TEAM_CODE=LoginInfo.TeamInfo.TEAM_CODE
            };
            AndonFactory.SaveAndonAlert(entity1);
            btnAndon.Text = afterandonBtntext;
            btnAndon.BackColor = Color.MediumVioletRed;
            status = 2;
            //throw new NotImplementedException();
        }

        private void btnAndon_Click(object sender, EventArgs e)
        {
            switch (status)
            {
                case 0: // normal status
                    int menuheight = ctmenuAndon.Height;
                    ctmenuAndon.Width = btnAndon.Width;
                    ctmenuAndon.Show(btnAndon, new Point(0, 0));
                    break;
                case 1: // prepare andon call, can be canceled
                    timer1.Stop();
                    status = 0;
                    entity1 = null;
                    btnAndon.Text = currentBtnText;
                    btnAndon.BackColor = SystemColors.Control;
                    label1.Visible = false;
                    break;
                case 2: // calling, can be canceled, but it should be canceld by others who has the privilage.
                    timer1.Stop();
                    status = 0;
                    entity1.ANSWER_FLAG = "Y";
                    entity1.ANDON_ANSWER_TIME = DateTime.Now;
                    entity1.REPORT_FLAG = "N";
                    entity1.MATERIAL_CODE = "-1";
                    AndonFactory.SaveAndonAlert(entity1);
                    entity1 = null;
                    btnAndon.BackColor = SystemColors.Control;
                    btnAndon.Text = currentBtnText;
                    label1.Visible = false;
                    break;
                default:
                    status = 0;
                    timer1.Stop();
                    entity1 = null;
                    break;
            }
        }

        private void itemQuality_Click(object sender, EventArgs e)
        {
            startAndonCall(andontype.quality);
        }

        private void itemProcess_Click(object sender, EventArgs e)
        {
            startAndonCall(andontype.process);
        }

        private void itemMat_Click(object sender, EventArgs e)
        {
            startAndonCall(andontype.mat);
        }

        void startAndonCall(andontype atype)
        {
            if (status != 0) return;
            label1.Visible = true;
            btnAndon.Text = onandonBtnText;
            btnAndon.BackColor = Color.LightYellow;
            afterandonBtntext = dict[atype];
            status = 1;
            timer1.Start();
        }
        enum andontype
        {
            quality,
            process,
            mat
        }
    }
}
