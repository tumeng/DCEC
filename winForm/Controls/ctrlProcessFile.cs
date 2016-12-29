using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Factory;
using Rmes.WinForm.Base;
using Rmes.Pub.Data1;
namespace Rmes.WinForm.Controls
{
    public partial class ctrlProcessFile : BaseControl
    {
        private string CompanyCode, PlineID, PlineCode1, StationID, len_sn, TheSo, TheSn, ThePlancode, StationName, Station_Code;
        private string ShiftCode, TeamCode, UserID;
        ProductInfoEntity product;
        //dataConn dc = new dataConn();

        public ctrlProcessFile(string sn)
        {
            InitializeComponent();
            //listView2.View = View.SmallIcon;
            listView1.View = View.SmallIcon;
            this.RMesDataChanged += new RMesEventHandler(ctrlProcessFile_RMesDataChanged);
            //listView2.DoubleClick+=new EventHandler(listView1_DoubleClick);

            CompanyCode = LoginInfo.CompanyInfo.COMPANY_CODE;
            PlineID = LoginInfo.ProductLineInfo.RMES_ID;
            PlineCode1 = LoginInfo.ProductLineInfo.PLINE_CODE;
            StationID = LoginInfo.StationInfo.RMES_ID;
            Station_Code = LoginInfo.StationInfo.STATION_CODE;
            StationName = LoginInfo.StationInfo.STATION_NAME;
            ShiftCode = LoginInfo.ShiftInfo.SHIFT_CODE;
            TeamCode = LoginInfo.TeamInfo.TEAM_CODE;
            UserID = LoginInfo.UserInfo.USER_ID;
            len_sn = LoginInfo.LEN_SN;
            TheSo = "";
            TheSn = "";
            ThePlancode = "";
            product = ProductInfoFactory.GetByCompanyCodeSNSingle(CompanyCode, sn);//获取sn信息
            if (product != null)
            {
                TheSo = product.PLAN_SO;
                TheSn = product.SN;
                ThePlancode = product.PLAN_CODE;
                initInfo("");
            }
        }

        void ctrlProcessFile_RMesDataChanged(object obj, RMESEventArgs e)
        {
            if (e.MessageHead == null) return;
            if (e.MessageHead == "SN" || e.MessageHead == "CHECK")
            {
                string sn = e.MessageBody.ToString();
                product = ProductInfoFactory.GetByCompanyCodeSNSingle(CompanyCode, sn);//获取sn信息
                if (product != null)
                {
                    TheSo = product.PLAN_SO;
                    TheSn = product.SN;
                    ThePlancode = product.PLAN_CODE;
                    initInfo("");
                }
            }
            //if (!string.IsNullOrWhiteSpace(e.MessageHead) && e.MessageHead == "PRCS")
            //{

            //    string msgbody = e.MessageBody as string;
            //    if (string.IsNullOrWhiteSpace(msgbody)) return;
            //    string processcode = "";
            //    if (msgbody.IndexOf('^') <= 0) //只发送了工序的信息
            //        processcode = msgbody;
            //    else
            //        processcode = msgbody.Split('^')[0].ToString();
            //    initInfo(processcode);

            //}
            //else if (e.MessageHead == "MESLL")
            //{
            //    this.Visible = false;
            //    return;
            //}
            //if (e.MessageHead.ToString() == "WORK" || e.MessageHead.ToString() == "QUA")
            //{
            //    this.Visible = true;
            //    return;
            //}
            //throw new NotImplementedException();
        }
        //根据当前生产线、站点、工位、工序选择数据库中的工艺文件  名称
        private void ctrlOptionFile_Load(object sender, EventArgs e)
        {
            //initInfo("");
        }

        private void initInfo(string processcode)
        {
            //listBox1.Items.Clear();
            listView1.Clear();
            //listView2.Clear();
            string plineid = LoginInfo.ProductLineInfo.RMES_ID;
            //string station_code = LoginInfo.StationInfo.RMES_ID.ToString();  //站点代码

            List<WorkProcessFileEntity> processfiles = WorkProcessFileFactory.GetFileName(plineid, processcode, Station_Code, product.PRODUCT_SERIES);
            if (processfiles != null && processfiles.Count > 0)
            {
                
                foreach (WorkProcessFileEntity c in processfiles)
                {
                    ListViewItem item = new ListViewItem();
                    item.Name = c.PROCESS_CODE;
                    item.Text = c.FILE_NAME;
                    item.ToolTipText = c.FILE_URL;
                    item.ImageIndex = 0;
                    listView1.Items.Add(item);
                    //if (c.FILE_TYPE.Equals("A"))
                    //    listView1.Items.Add(item);
                    //else
                    //    listView2.Items.Add(item);
                }
            }
        }
        //双击打开对应文件

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
                string filename="";
            try
            {
                string str = System.Environment.CurrentDirectory;
                ListView l = sender as ListView;
                if (l == null || l.SelectedItems.Count < 1) return;
                filename = l.SelectedItems[0].ToolTipText;
                FileOpen(filename);
            }
            catch
            {
                MessageBox.Show("无效文件"+filename+"，无法正确打开。");
            }
        }
        private void FileOpen(string filename)
        {
            if (filename.StartsWith("http://"))
            {
                FrmShowProcessFile form1 = new FrmShowProcessFile(filename);
                form1.ShowDialog();
            }
            else
            {
                System.Diagnostics.Process.Start(filename);
            }
        }



    }
}
