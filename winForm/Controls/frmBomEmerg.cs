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
using System.Reflection;

namespace Rmes.WinForm.Controls
{
    public partial class frmBomEmerg : Form
    {
        LineSideStockEntity ent;
        public frmBomEmerg(LineSideStockEntity entity)
        {
            InitializeComponent();
            ent=entity;
        }


        private void txtItemCode_Validated(object sender, EventArgs e)
        {
            string item_code = txtItemCode.Text;
            if (string.IsNullOrWhiteSpace(item_code)) return;
            ItemEntity ent_item = ItemFactory.GetByItem(ent.COMPANY_CODE, item_code);
            if (ent_item == null)
            {
                MessageBox.Show("此项物料没有定义");

                txtItemCode.Focus();
                return;

            }
            
            txtItemName.Text = ent_item.ITEM_NAME;
            txtUnit.Text = ent_item.UNIT_CODE;

        }

    

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                ent.ITEM_QTY = int.Parse(txtItemQty.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("请输入有效字符");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtItemCode.Text))
            {
                MessageBox.Show("物料编码不能为空");
                return;
            }
            ent.ITEM_CODE = txtItemCode.Text;
            ent.ITEM_NAME = txtItemName.Text;
            LineSideStockFactory.Storage(ent);
 
            txtItemName.Text = "";
            txtItemCode.Text = "";
            txtItemQty.Text = "";
        }

        
        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }
    }
}



