using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Factory;

namespace Rmes.WinForm.Controls
{
    public partial class FrmDetectPart : Form
    {

        public FrmDetectPart(string SN, string ProcessCode, string ItemName)
        {
            //string sn = SN;
            InitializeComponent();
            label6.Text = SN;
            label7.Text = ProcessCode;
            label8.Text = ItemName;
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            QualitySnItemMat MAT = new QualitySnItemMat();
            MAT.SN = label6.Text.ToString();
            MAT.PROCESS_CODE = label7.Text.ToString();
            MAT.ITEM_NAME = label8.Text.ToString();
            MAT.TEST_PART = textBox1.Text.ToString();
            MAT.TEST_RESULT = textBox2.Text.ToString();
            MAT.TEST_TIME = DateTime.Now.ToString();
            MAT.TEST_USER = LoginInfo.UserInfo.USER_NAME;
            if (string.IsNullOrWhiteSpace(textBox1.Text as string) || string.IsNullOrWhiteSpace(textBox2.Text as string))
            {
                MessageBox.Show("检测零件和检测结果不能为空");
                return;
            }
            QualityFactory.SaveItemMat(MAT);
            this.Close();
        }
    }
}
