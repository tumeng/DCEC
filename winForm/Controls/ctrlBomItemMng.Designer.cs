namespace Rmes.WinForm.Controls
{
    partial class ctrlBomItemMng
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.dgvBom = new System.Windows.Forms.DataGridView();
            this.colItemChecked = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colItemCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colItemQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCompleteQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVendorCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colItemBatch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProcessCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colItemClassCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colConfirmFlag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRmesID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBom)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(544, 503);
            this.btnOK.Margin = new System.Windows.Forms.Padding(5);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(89, 29);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(662, 503);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(89, 29);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // dgvBom
            // 
            this.dgvBom.AllowUserToAddRows = false;
            this.dgvBom.AllowUserToDeleteRows = false;
            this.dgvBom.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBom.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colItemChecked,
            this.colItemCode,
            this.colItemName,
            this.colItemQty,
            this.colCompleteQty,
            this.colVendorCode,
            this.colItemBatch,
            this.colProcessCode,
            this.colItemClassCode,
            this.colConfirmFlag,
            this.colRmesID});
            this.dgvBom.Location = new System.Drawing.Point(8, 0);
            this.dgvBom.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
            this.dgvBom.Name = "dgvBom";
            this.dgvBom.ReadOnly = true;
            this.dgvBom.RowHeadersVisible = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.dgvBom.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvBom.RowTemplate.Height = 23;
            this.dgvBom.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBom.Size = new System.Drawing.Size(743, 473);
            this.dgvBom.TabIndex = 2;
            this.dgvBom.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBom_CellClick);
            // 
            // colItemChecked
            // 
            this.colItemChecked.HeaderText = "选择";
            this.colItemChecked.Name = "colItemChecked";
            this.colItemChecked.ReadOnly = true;
            this.colItemChecked.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colItemChecked.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.colItemChecked.Width = 70;
            // 
            // colItemCode
            // 
            this.colItemCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colItemCode.DataPropertyName = "ITEM_CODE";
            this.colItemCode.HeaderText = "项目代码";
            this.colItemCode.Name = "colItemCode";
            this.colItemCode.ReadOnly = true;
            this.colItemCode.Width = 99;
            // 
            // colItemName
            // 
            this.colItemName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colItemName.DataPropertyName = "ITEM_NAME";
            this.colItemName.HeaderText = "项目名称";
            this.colItemName.Name = "colItemName";
            this.colItemName.ReadOnly = true;
            this.colItemName.Width = 99;
            // 
            // colItemQty
            // 
            this.colItemQty.DataPropertyName = "ITEM_QTY";
            this.colItemQty.HeaderText = "数量";
            this.colItemQty.Name = "colItemQty";
            this.colItemQty.ReadOnly = true;
            this.colItemQty.Width = 70;
            // 
            // colCompleteQty
            // 
            this.colCompleteQty.DataPropertyName = "COMPLETE_QTY";
            this.colCompleteQty.HeaderText = "完成数量";
            this.colCompleteQty.Name = "colCompleteQty";
            this.colCompleteQty.ReadOnly = true;
            // 
            // colVendorCode
            // 
            this.colVendorCode.DataPropertyName = "VENDOR_CODE";
            this.colVendorCode.HeaderText = "供应商";
            this.colVendorCode.Name = "colVendorCode";
            this.colVendorCode.ReadOnly = true;
            // 
            // colItemBatch
            // 
            this.colItemBatch.DataPropertyName = "ITEM_BATCH";
            this.colItemBatch.HeaderText = "批次号(sn)";
            this.colItemBatch.Name = "colItemBatch";
            this.colItemBatch.ReadOnly = true;
            this.colItemBatch.Width = 120;
            // 
            // colProcessCode
            // 
            this.colProcessCode.DataPropertyName = "PROCESS_CODE";
            this.colProcessCode.HeaderText = "工序";
            this.colProcessCode.Name = "colProcessCode";
            this.colProcessCode.ReadOnly = true;
            this.colProcessCode.Width = 80;
            // 
            // colItemClassCode
            // 
            this.colItemClassCode.DataPropertyName = "ITEM_CLASS_CODE";
            this.colItemClassCode.HeaderText = "Item_CLASS";
            this.colItemClassCode.Name = "colItemClassCode";
            this.colItemClassCode.ReadOnly = true;
            this.colItemClassCode.Visible = false;
            // 
            // colConfirmFlag
            // 
            this.colConfirmFlag.DataPropertyName = "confirm_flag";
            this.colConfirmFlag.HeaderText = "confirm_flag";
            this.colConfirmFlag.Name = "colConfirmFlag";
            this.colConfirmFlag.ReadOnly = true;
            this.colConfirmFlag.Visible = false;
            // 
            // colRmesID
            // 
            this.colRmesID.DataPropertyName = "RMES_ID";
            this.colRmesID.HeaderText = "RmesID";
            this.colRmesID.Name = "colRmesID";
            this.colRmesID.ReadOnly = true;
            this.colRmesID.Visible = false;
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Location = new System.Drawing.Point(8, 503);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(61, 25);
            this.chkAll.TabIndex = 3;
            this.chkAll.Text = "全选";
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(8, 474);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(743, 21);
            this.progressBar1.TabIndex = 4;
            // 
            // ctrlBomItemMng
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.chkAll);
            this.Controls.Add(this.dgvBom);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
            this.Name = "ctrlBomItemMng";
            this.Size = new System.Drawing.Size(758, 537);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBom)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridView dgvBom;
        private System.Windows.Forms.CheckBox chkAll;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colItemChecked;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItemCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItemQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCompleteQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVendorCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItemBatch;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProcessCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItemClassCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colConfirmFlag;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRmesID;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}
