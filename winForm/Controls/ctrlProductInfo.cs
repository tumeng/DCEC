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
using System.Drawing;

namespace Rmes.WinForm.Controls
{
    public partial class ctrlProductInfo : BaseControl
    {
        public ctrlProductInfo()
        {
            InitializeComponent();
            this.RMesDataChanged += new RMesEventHandler(ctrlProductInfo_RMesDataChanged);
        }
        void ctrlProductInfo_RMesDataChanged(object obj, Rmes.WinForm.Base.RMESEventArgs e)
        {
            if (e.MessageHead == null) return;
            if (e.MessageHead == "SN" || e.MessageHead == "ONLINE" || e.MessageHead == "OFFLINE" || e.MessageHead == "RECHECK" || e.MessageHead == "RESN")
            {
                //ProductInfoEntity product = e.MessageBody as ProductInfoEntity;
                string sn = e.MessageBody as string;
  
                ProductInfoEntity product = ProductInfoFactory.GetByCompanyCodeSNSingle(LoginInfo.CompanyInfo.COMPANY_CODE, sn);
                if (product == null) return;
                label1.Text = product.PLAN_CODE;  //计划号
                label2.Text = product.PLAN_SO;    //SO
                label3.Text = product.PRODUCT_MODEL; //机型
                //label2.BackColor = SystemColors.GradientInactiveCaption;
            }
            if (e.MessageHead == "FILLSN")//初始化控件
            {
                string[] txt = e.MessageBody.ToString().Split('^');
                string sn1 = txt[0];
                label1.Text = txt[3];  //计划号
                label2.Text = txt[1];    //SO
                label3.Text = txt[2]; //机型
            }
            if (e.MessageHead == "FILLSN2")//初始化控件
            {
                string[] txt = e.MessageBody.ToString().Split('^');
                label1.Text = txt[2];  //计划号
                label2.Text = txt[0];    //SO
                label3.Text = txt[1]; //机型
            }
            if (e.MessageHead == "CLEAR")//初始化控件
            {
                label1.Text = "";  //计划号
                label2.Text = "";    //SO
                label3.Text = ""; //机型
            }
            //////if (e.MessageHead == "YT")
            //////{
            //////    label2.BackColor = Color.Green;    //SO
            //////    label3.BackColor = Color.Green;  //机型
            //////}
            //////if (e.MessageHead == "JC")
            //////{
            //////    label2.BackColor = Color.Red;    //SO
            //////    label3.BackColor = Color.Red;  //机型
            //////}
            //////if (e.MessageHead == "FYT")
            //////{
            //////    label2.BackColor = SystemColors.GradientInactiveCaption;    //SO
            //////    label3.BackColor = SystemColors.GradientInactiveCaption;  //机型
            //////}
            //////if (e.MessageHead == "FJC")
            //////{
            //////    label2.BackColor = SystemColors.GradientInactiveCaption;    //SO
            //////    label3.BackColor = SystemColors.GradientInactiveCaption;  //机型
            //////}
            //else if(e.MessageHead==null)
            //{
            //    label1.Text = "";
            //    label2.Text = "";
            //    label3.Text = "";
            //}
            //if (e.MessageHead == "SCP")
            //{
            //    label1.Text = "";
            //    label2.Text = "";
            //    label3.Text = "";
            //}
            //if (e.MessageHead == "MESLL")
            //{
            //    this.Visible = false;
            //    return;
            //}
            //if (e.MessageHead.ToString() == "WORK")
            //{
            //    this.Visible = true;
            //    return;
            //}
            if (e.MessageHead == "JC")
            {
                label2.BackColor = Color.Red;
            }
            if (e.MessageHead == "FJC")
            {
                label2.BackColor = SystemColors.GradientInactiveCaption;
            }
            if (e.MessageHead == "YT")
            {
                label2.BackColor = Color.Green;    //SO
                label3.BackColor = Color.Green;  //机型
            }
            if (e.MessageHead == "FYT")
            {
                label2.BackColor = SystemColors.GradientInactiveCaption;
                label3.BackColor = SystemColors.GradientInactiveCaption;  //机型
            }
        }
        
    }
}
