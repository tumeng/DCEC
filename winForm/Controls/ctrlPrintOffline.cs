using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using Rmes.Public;
using System.Windows.Forms;
using Rmes.WinForm.Base;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Factory;
using System.IO;
using System.IO.Ports;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using System.Drawing.Printing;


//下线处理控件，接收SN传来的未上线消息

namespace Rmes.WinForm.Controls
{
    public partial class ctrlPrintOffline : BaseControl
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern SafeFileHandle CreateFile(string lpFileName, FileAccess dwDesiredAccess,
            uint dwShareMode, IntPtr lpSecurityAttributes, FileMode dwCreationDisposition,
            uint dwFlagsAndAttributes, IntPtr hTemplateFile);

        [DllImport("fnthex32.dll")]
        public static extern int GETFONTHEX(string BarcodeText, string FontName, string FileName, int Orient,
                                                            int Height, int Width, int IsBold, int IsItalic, StringBuilder ReturnBarcodeCMD);
        string CompanyCode, PlineCode, PlanCode, Sn;

        public ctrlPrintOffline()
        {
            InitializeComponent();
 
            CompanyCode = LoginInfo.CompanyInfo.COMPANY_CODE;
            PlineCode = LoginInfo.ProductLineInfo.RMES_ID;

            this.RMesDataChanged += new Rmes.WinForm.Base.RMesEventHandler(ctrlPrintOffline_RmesDataChanged);
        }

        protected void ctrlPrintOffline_RmesDataChanged(object obj, Rmes.WinForm.Base.RMESEventArgs e)
        {

            if (e.MessageHead == "OFFLINE")
            {
                string[] txt = e.MessageBody.ToString().Split('^');
                Sn = txt[0];
                string quality_status = txt[1];
                PlanSnEntity plansn1 = PlanSnFactory.GetBySn(Sn);
                PlanCode = plansn1.PLAN_CODE;
                PrintOfflineLabel();
            }
            else if (e.MessageHead == "SN")
            {
                RMESEventArgs args = new RMESEventArgs();
                args.MessageHead = "OFFCTRL";
                args.MessageBody = "";
                SendDataChangeMessage(args);
            }
        }

        private void PrintOfflineLabel()
        {
            //List<ProductDataEntity> SNS = ProductDataFactory.GetProductDataByPlanSn(CompanyCode, PlineCode, PlanCode, Sn);
            //if (SNS.Count == 0) return;
            //ProductDataEntity ent_prod = SNS.First<ProductDataEntity>();
            //string project_name = ProjectFactory.GetByProjectCode(ent_prod.PROJECT_CODE).PROJECT_NAME;

            //string code = "";

            //string str1 = "工程信息：" + ent_prod.PROJECT_CODE + "/" + project_name;
            //string str2 = "产品图号：" + ent_prod.PLAN_SO;
            //string str3 = "下线时间：" + ent_prod.COMPLETE_TIME.ToString("yyyy-MM-dd HH:mm:ss");
            //string str4 = "质量状态：" + (ent_prod.QUALITY_STATUS == "A" ? "合格" : "不合格");
            //string str5 = "班组：" + LoginInfo.TeamInfo.TEAM_NAME;
            //string str6 = Sn;

            //StringBuilder project = new StringBuilder(10240);
            //int i1 = GETFONTHEX(str1, "宋体", "temp1", 0, 30, 20, 0, 0, project);
            //StringBuilder jg = new StringBuilder(10240);
            //int i2 = GETFONTHEX(str2, "宋体", "temp1", 0, 30, 20, 0, 0, jg);
            //StringBuilder SO = new StringBuilder(10240);
            //int i3 = GETFONTHEX(str3, "宋体", "temp1", 0, 30, 20, 0, 0, SO);
            //StringBuilder xx = new StringBuilder(10240);
            //int i4 = GETFONTHEX(str4, "宋体", "temp1", 0, 30, 20, 0, 0, xx);
            //StringBuilder bz = new StringBuilder(10240);
            //int i5 = GETFONTHEX(str5, "宋体", "temp1", 0, 30, 20, 0, 0, bz);
            //StringBuilder h = new StringBuilder(10240);
            //int i6 = GETFONTHEX(str6, "宋体", "temp1", 0, 30, 20, 0, 0, h);
            //code = project.ToString() + "^XA^MD30^LH30,10,10^FO20,1^XGtemp1,1,1^FS" +
            //    jg.ToString() + "^MD30^LH30,10,10^FO20,35^XGtemp1,1,1^FS" +
            //    xx.ToString() + "^MD30^LH30,10^FO20,70^XGtemp1,1,1^FS" +
            //    bz.ToString() + "^MD30^LH30,10^FO20,105^XGtemp1,1,1^FS" +
            //    SO.ToString() + "^MD30^LH30,10^FO20,140^XGtemp1,1,1^FS" +
            //        "^FO80,175^BY2,2,50^BCN,100,N,N,N^FD" +Sn + "^FS" +
            //        h.ToString() + "^MD30^LH120,10^FO20,275^XGtemp1,1,1^FS" +
            //                    "^XZ";
            //PrintDriver.SendStringToPrinter("S4M", code);


        }
    }
}
