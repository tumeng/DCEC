namespace Rmes.WinForm.Controls
{
    partial class ctrOfflinePlan
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.GridPlan = new System.Windows.Forms.DataGridView();
            this.ColPlanSeq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColPlanCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPlanSo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProductModel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColPlanQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColRealOnlineQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOfflineQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnCus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColRemark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBeginDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEnddate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCreateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colrunflag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridPlan)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.GridPlan);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(5);
            this.groupBox1.Size = new System.Drawing.Size(941, 232);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "计划信息";
            // 
            // GridPlan
            // 
            this.GridPlan.AllowUserToAddRows = false;
            this.GridPlan.AllowUserToDeleteRows = false;
            this.GridPlan.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GridPlan.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.GridPlan.ColumnHeadersHeight = 30;
            this.GridPlan.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColPlanSeq,
            this.ColPlanCode,
            this.colPlanSo,
            this.colProductModel,
            this.ColPlanQuantity,
            this.ColRealOnlineQuantity,
            this.colOfflineQty,
            this.columnCus,
            this.ColRemark,
            this.colBeginDate,
            this.colEnddate,
            this.colCreateTime,
            this.colrunflag});
            this.GridPlan.Location = new System.Drawing.Point(8, 23);
            this.GridPlan.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
            this.GridPlan.Name = "GridPlan";
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.GridPlan.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.GridPlan.RowTemplate.Height = 23;
            this.GridPlan.Size = new System.Drawing.Size(933, 209);
            this.GridPlan.TabIndex = 0;
            this.GridPlan.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridPlan_CellClick);
            this.GridPlan.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridPlan_CellDoubleClick);
            // 
            // ColPlanSeq
            // 
            this.ColPlanSeq.DataPropertyName = "PLAN_SEQ";
            this.ColPlanSeq.HeaderText = "序";
            this.ColPlanSeq.Name = "ColPlanSeq";
            this.ColPlanSeq.ReadOnly = true;
            this.ColPlanSeq.Width = 30;
            // 
            // ColPlanCode
            // 
            this.ColPlanCode.DataPropertyName = "PLAN_CODE";
            this.ColPlanCode.HeaderText = "计划号";
            this.ColPlanCode.Name = "ColPlanCode";
            this.ColPlanCode.ReadOnly = true;
            this.ColPlanCode.Width = 130;
            // 
            // colPlanSo
            // 
            this.colPlanSo.DataPropertyName = "PLAN_SO";
            this.colPlanSo.HeaderText = "SO";
            this.colPlanSo.Name = "colPlanSo";
            this.colPlanSo.ReadOnly = true;
            // 
            // colProductModel
            // 
            this.colProductModel.DataPropertyName = "PRODUCT_MODEL";
            this.colProductModel.HeaderText = "机型";
            this.colProductModel.Name = "colProductModel";
            this.colProductModel.ReadOnly = true;
            this.colProductModel.Width = 150;
            // 
            // ColPlanQuantity
            // 
            this.ColPlanQuantity.DataPropertyName = "PLAN_QTY";
            this.ColPlanQuantity.HeaderText = "计划数";
            this.ColPlanQuantity.Name = "ColPlanQuantity";
            this.ColPlanQuantity.ReadOnly = true;
            this.ColPlanQuantity.Width = 70;
            // 
            // ColRealOnlineQuantity
            // 
            this.ColRealOnlineQuantity.DataPropertyName = "ONLINE_QTY";
            this.ColRealOnlineQuantity.HeaderText = "上线数";
            this.ColRealOnlineQuantity.Name = "ColRealOnlineQuantity";
            this.ColRealOnlineQuantity.ReadOnly = true;
            this.ColRealOnlineQuantity.Visible = false;
            this.ColRealOnlineQuantity.Width = 70;
            // 
            // colOfflineQty
            // 
            this.colOfflineQty.DataPropertyName = "OFFLINE_QTY";
            this.colOfflineQty.HeaderText = "下线数";
            this.colOfflineQty.Name = "colOfflineQty";
            this.colOfflineQty.ReadOnly = true;
            this.colOfflineQty.Width = 70;
            // 
            // columnCus
            // 
            this.columnCus.DataPropertyName = "CUSTOMER_NAME";
            this.columnCus.HeaderText = "客户";
            this.columnCus.Name = "columnCus";
            this.columnCus.ReadOnly = true;
            this.columnCus.Width = 150;
            // 
            // ColRemark
            // 
            this.ColRemark.DataPropertyName = "REMARK";
            this.ColRemark.HeaderText = "备注";
            this.ColRemark.Name = "ColRemark";
            this.ColRemark.ReadOnly = true;
            this.ColRemark.Width = 200;
            // 
            // colBeginDate
            // 
            this.colBeginDate.DataPropertyName = "BEGIN_DATE";
            this.colBeginDate.HeaderText = "开始日期";
            this.colBeginDate.Name = "colBeginDate";
            this.colBeginDate.ReadOnly = true;
            this.colBeginDate.Width = 120;
            // 
            // colEnddate
            // 
            this.colEnddate.DataPropertyName = "END_DATE";
            this.colEnddate.HeaderText = "结束日期";
            this.colEnddate.Name = "colEnddate";
            this.colEnddate.ReadOnly = true;
            this.colEnddate.Width = 120;
            // 
            // colCreateTime
            // 
            this.colCreateTime.DataPropertyName = "CREATE_TIME";
            this.colCreateTime.HeaderText = "创建时间";
            this.colCreateTime.Name = "colCreateTime";
            this.colCreateTime.ReadOnly = true;
            this.colCreateTime.Width = 210;
            // 
            // colrunflag
            // 
            this.colrunflag.DataPropertyName = "RUN_FLAG";
            this.colrunflag.HeaderText = "RUNFLAG";
            this.colrunflag.Name = "colrunflag";
            this.colrunflag.ReadOnly = true;
            this.colrunflag.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 30000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ctrOfflinePlan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
            this.Name = "ctrOfflinePlan";
            this.Size = new System.Drawing.Size(941, 232);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridPlan)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView GridPlan;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColPlanSeq;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColPlanCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPlanSo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductModel;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColPlanQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColRealOnlineQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOfflineQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnCus;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColRemark;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBeginDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEnddate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCreateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colrunflag;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
    }
}
