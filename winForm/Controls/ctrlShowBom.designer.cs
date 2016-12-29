namespace Rmes.WinForm.Controls
{
    partial class ctrlShowBom
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvBom = new System.Windows.Forms.DataGridView();
            this.colItemCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGwmc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGxmc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGys = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCompleteQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colItemClass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colItemType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colConfirmFlag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colReplaceFlag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colzddm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colZdmc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRmesid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBom)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.dgvBom);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(575, 414);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "BOM清单";
            // 
            // dgvBom
            // 
            this.dgvBom.AllowUserToAddRows = false;
            this.dgvBom.AllowUserToDeleteRows = false;
            this.dgvBom.ColumnHeadersHeight = 35;
            this.dgvBom.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colItemCode,
            this.colItemName,
            this.colQty,
            this.colGwmc,
            this.colGxmc,
            this.colGys,
            this.colCompleteQty,
            this.colItemClass,
            this.colItemType,
            this.colConfirmFlag,
            this.colReplaceFlag,
            this.colzddm,
            this.colZdmc,
            this.colRmesid});
            this.dgvBom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBom.Location = new System.Drawing.Point(3, 25);
            this.dgvBom.Margin = new System.Windows.Forms.Padding(5);
            this.dgvBom.Name = "dgvBom";
            this.dgvBom.ReadOnly = true;
            this.dgvBom.RowHeadersVisible = false;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            this.dgvBom.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvBom.RowTemplate.Height = 35;
            this.dgvBom.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBom.Size = new System.Drawing.Size(569, 386);
            this.dgvBom.TabIndex = 0;
            this.dgvBom.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBom_CellContentClick);
            this.dgvBom.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBom_CellDoubleClick);
            this.dgvBom.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvBom_MouseClick);
            // 
            // colItemCode
            // 
            this.colItemCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colItemCode.DataPropertyName = "COMP";
            this.colItemCode.HeaderText = "零件";
            this.colItemCode.Name = "colItemCode";
            this.colItemCode.ReadOnly = true;
            this.colItemCode.Width = 67;
            // 
            // colItemName
            // 
            this.colItemName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colItemName.DataPropertyName = "UDESC";
            this.colItemName.HeaderText = "名称";
            this.colItemName.Name = "colItemName";
            this.colItemName.ReadOnly = true;
            this.colItemName.Width = 67;
            // 
            // colQty
            // 
            this.colQty.DataPropertyName = "QTY";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colQty.DefaultCellStyle = dataGridViewCellStyle1;
            this.colQty.HeaderText = "数量";
            this.colQty.Name = "colQty";
            this.colQty.ReadOnly = true;
            this.colQty.Width = 70;
            // 
            // colGwmc
            // 
            this.colGwmc.DataPropertyName = "GWMC";
            this.colGwmc.HeaderText = "工位";
            this.colGwmc.Name = "colGwmc";
            this.colGwmc.ReadOnly = true;
            this.colGwmc.Width = 80;
            // 
            // colGxmc
            // 
            this.colGxmc.DataPropertyName = "GXMC";
            this.colGxmc.HeaderText = "工序";
            this.colGxmc.Name = "colGxmc";
            this.colGxmc.ReadOnly = true;
            this.colGxmc.Visible = false;
            this.colGxmc.Width = 80;
            // 
            // colGys
            // 
            this.colGys.DataPropertyName = "GYSMC";
            this.colGys.HeaderText = "供应商";
            this.colGys.Name = "colGys";
            this.colGys.ReadOnly = true;
            // 
            // colCompleteQty
            // 
            this.colCompleteQty.DataPropertyName = "COMPLETE_QTY";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colCompleteQty.DefaultCellStyle = dataGridViewCellStyle2;
            this.colCompleteQty.HeaderText = "完成数量";
            this.colCompleteQty.Name = "colCompleteQty";
            this.colCompleteQty.ReadOnly = true;
            this.colCompleteQty.Visible = false;
            // 
            // colItemClass
            // 
            this.colItemClass.DataPropertyName = "ITEM_CLASS";
            this.colItemClass.HeaderText = "零件级别";
            this.colItemClass.Name = "colItemClass";
            this.colItemClass.ReadOnly = true;
            this.colItemClass.Visible = false;
            // 
            // colItemType
            // 
            this.colItemType.DataPropertyName = "ITEM_TYPE";
            this.colItemType.HeaderText = "零件类别ABC";
            this.colItemType.Name = "colItemType";
            this.colItemType.ReadOnly = true;
            this.colItemType.Visible = false;
            // 
            // colConfirmFlag
            // 
            this.colConfirmFlag.DataPropertyName = "CONFIRM_FLAG";
            this.colConfirmFlag.HeaderText = "确认标识";
            this.colConfirmFlag.Name = "colConfirmFlag";
            this.colConfirmFlag.ReadOnly = true;
            this.colConfirmFlag.Visible = false;
            // 
            // colReplaceFlag
            // 
            this.colReplaceFlag.DataPropertyName = "REPLACE_FLAG";
            this.colReplaceFlag.HeaderText = "替换标识";
            this.colReplaceFlag.Name = "colReplaceFlag";
            this.colReplaceFlag.ReadOnly = true;
            this.colReplaceFlag.Visible = false;
            // 
            // colzddm
            // 
            this.colzddm.DataPropertyName = "ZDDM";
            this.colzddm.HeaderText = "站点";
            this.colzddm.Name = "colzddm";
            this.colzddm.ReadOnly = true;
            this.colzddm.Visible = false;
            // 
            // colZdmc
            // 
            this.colZdmc.DataPropertyName = "ZDMC";
            this.colZdmc.HeaderText = "站点名称";
            this.colZdmc.Name = "colZdmc";
            this.colZdmc.ReadOnly = true;
            this.colZdmc.Visible = false;
            // 
            // colRmesid
            // 
            this.colRmesid.DataPropertyName = "RMES_ID";
            this.colRmesid.HeaderText = "RMES_ID";
            this.colRmesid.Name = "colRmesid";
            this.colRmesid.ReadOnly = true;
            this.colRmesid.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ctrlShowBom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
            this.Name = "ctrlShowBom";
            this.Size = new System.Drawing.Size(575, 414);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBom)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvBom;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItemCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGwmc;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGxmc;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGys;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCompleteQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItemClass;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItemType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colConfirmFlag;
        private System.Windows.Forms.DataGridViewTextBoxColumn colReplaceFlag;
        private System.Windows.Forms.DataGridViewTextBoxColumn colzddm;
        private System.Windows.Forms.DataGridViewTextBoxColumn colZdmc;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRmesid;
        private System.Windows.Forms.Timer timer1;


    }
}
