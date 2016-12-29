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
using Rmes.Pub.Data1;

namespace Rmes.WinForm.Controls
{
    public partial class ctrl_FxFx : BaseControl
    {
        //返修站点 点击返修完成按钮提醒相关人员在管理工作站确认返修操作，并校验该发动机所有装配站点的所有重要零件是否全部扫描
        private string CompanyCode, PlineCode, PlineID, StationCode, stationname, TheSo, TheSn, ThePlancode;
        //dataConn dc = new dataConn();
        ProductInfoEntity product;
        public ctrl_FxFx()
        {
            InitializeComponent();
            CompanyCode = LoginInfo.CompanyInfo.COMPANY_CODE;
            PlineID = LoginInfo.ProductLineInfo.RMES_ID;
            PlineCode = LoginInfo.ProductLineInfo.PLINE_CODE;
            StationCode = LoginInfo.StationInfo.RMES_ID;
            stationname = LoginInfo.StationInfo.STATION_NAME;
            this.RMesDataChanged += new RMesEventHandler(ctrl_Fx_RMesDataChanged);
        }
        void ctrl_Fx_RMesDataChanged(object obj, Rmes.WinForm.Base.RMESEventArgs e)
        {
            try
            {
                RMESEventArgs arg = new RMESEventArgs();
                arg.MessageHead = "";
                arg.MessageBody = "";
                if (e.MessageHead == null) return;
                if (e.MessageHead == "SN")
                {
                    TheSn = e.MessageBody.ToString();
                    product = ProductInfoFactory.GetByCompanyCodeSNSingle(CompanyCode, TheSn);//获取sn信息
                    if (product == null) return;
                    TheSo = product.PLAN_SO;
                    ThePlancode = product.PLAN_CODE;
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void btnFinishedD_Click(object sender, EventArgs e)
        {
            //校验data_sn_bom_temp 以及data_sn_detect_data_temp
            if (TheSn == "" || TheSn == null || ThePlancode == "" || ThePlancode == null)
            {
                MessageBox.Show("请扫描流水号", "提示");
                return;
            }
            string sql = "select count(1) from data_sn_bom_temp where sn='"+TheSn+"' and plan_code='"+ThePlancode+"' and  confirm_flag='N' and item_type<>'C' ";
            if (dataConn.GetValue(sql) != "0")
            {
                MessageBox.Show("重要零件尚未全部扫描", "提示");
                return;
            }
            sql = "select count(1) from data_sn_detect_data_temp where sn='" + TheSn + "' and plan_code='" + ThePlancode + "' and detect_flag='N'";
            if (dataConn.GetValue(sql) != "0")
            {
                MessageBox.Show("检测数据尚未全部扫描", "提示");
                return;
            }
            sql = "select count(1) from data_sn_qa where sn='" + TheSn + "' and plan_code='" + ThePlancode + "' and qa_flag='N'";
            if (dataConn.GetValue(sql) != "0")
            {
                MessageBox.Show("终检未全部确认", "提示");
                return;
            }
            string usermail = "";
            DataTable dt = dataConn.GetTable("select USER_CODE,USER_MAIL from ATPU_USER_GZQR where pline_code='" + PlineCode + "' and USER_MAIL is not null ");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //向管理人员发动邮件 管理工作站确认
                    try
                    {
                        usermail = dt.Rows[i][1].ToString().Replace("@", "@@");
                        dataConn.ExeSql("insert into ms_jit_mail3(mailaddress,mailsubject,mailbody,mailflag,createtime)  values('" + usermail + "','" + TheSn + "'||'改制发动机操作确认','" + ThePlancode + "'||'" + TheSn + "'||'改制发动机操作确认,请前往管理界面进行确认！','N',sysdate) ");
                    }
                    catch
                    { }
                }
            }
            dataConn.ExeSql("insert into data_sn_detect_query(sn,plan_code,pline_code,work_time,detect_flag,scan_flag) values('" + TheSn + "','" + ThePlancode + "','" + PlineCode + "',sysdate,'N','N') ");
            MessageBox.Show("通知前台管理人员确认！","提示");

        }
    }
}
