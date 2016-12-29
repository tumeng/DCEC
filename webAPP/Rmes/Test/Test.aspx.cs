using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//以下引用，如果采用实体类，请全部复制到新页面
using PetaPoco;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;
using Rmes.Pub.Data;
using Rmes.Web.Base;
using System.Text;


namespace Rmes.WebApp.Rmes.Test
{
    public partial class Test : BasePage
    {
        public Database db = DB.GetInstance();
        protected void Page_Load(object sender, EventArgs e)
        {
            //List<PlanBomEntity> entity = PlanBOMFactory.GetAll();
            //XtraReport1 re = new XtraReport1(entity);
            //ReportViewer1.Report = re;
        }

        protected void bt3_Click(object sender, EventArgs e)
        {
            db.Execute("call SAP_HANDLE_EXPORT_INSTORE()");//44
            SAPMessageTransEntity msgEntity = new SAPMessageTransEntity
            {
                MESSAGE_CODE = "0044",
                WORK_DATE = DateTime.Now,
                HANDLE_FLAG = "0",
            };
            DB.GetInstance().Insert(msgEntity);
        }

        protected void bt2_Click(object sender, EventArgs e)
        {
            db.Execute("call SAP_HANDLE_EXPORT_PROCESS()");//41
            SAPMessageTransEntity msgEntity = new SAPMessageTransEntity
            {
                MESSAGE_CODE = "0041",
                WORK_DATE = DateTime.Now,
                HANDLE_FLAG = "0",
            };
            DB.GetInstance().Insert(msgEntity);
        }

        

        protected void bt1_Click(object sender, EventArgs e)
        {
            db.Execute("call SAP_HANDLE_EXPORT_BOM()");//43
            SAPMessageTransEntity msgEntity = new SAPMessageTransEntity
            {
                MESSAGE_CODE = "0043",
                WORK_DATE = DateTime.Now,
                HANDLE_FLAG = "0",
            };
            DB.GetInstance().Insert(msgEntity);
        }

        protected void bt5_Click(object sender, EventArgs e)
        {
            System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(LongTask));
            thread.Start();

            Session["State"] = 1;
            OpenProgressBar(this.Page);
        }

        private void LongTask()
        {
            //模拟长时间任务
            //每个循环模拟任务进行到不同的阶段
            for (int i = 0; i < 1001; i++)
            {
                System.Threading.Thread.Sleep(100);
                //设置每个阶段的state值，用来显示当前的进度
                Session["State"] = i + 1;
            }
            //任务结束
            

        }

        public static void OpenProgressBar(System.Web.UI.Page Page)
        {
            StringBuilder sbScript = new StringBuilder();

            sbScript.Append("<script language='JavaScript' type='text/javascript'>\n");
            sbScript.Append("<!--\n");
            //需要IE5.5以上支持
            sbScript.Append("window.showModalDialog('/Rmes/Test/ProgressBarTest.aspx?maxnum="+"1000"+"','','dialogHeight: 40px; dialogWidth: 350px; edge: Raised; center: Yes; help: No; resizable: No; status: No;scroll:No;');\n");
            //IE5.5以下使用window.open
            //sbScript.Append("window.open('Progress.aspx','', 'height=100, width=350, toolbar =no, menubar=no, scrollbars=no, resizable=no, location=no, status=no');\n");
            sbScript.Append("// -->\n");
            sbScript.Append("</script>\n");

            Page.RegisterClientScriptBlock("OpenProgressBar", sbScript.ToString());
        }
    }
}