namespace Rmes.WinForm.Controls
{
    partial class ctrQualityPlan
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.GridPlan = new System.Windows.Forms.DataGridView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.columnProject = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnAssemblyCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPlanSo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColPlanOnlineQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColRealOnlineQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColOfflineQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColPlanSeq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColPlanCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPlanBatch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColRunFlag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSnFlag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.GridPlan)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // GridPlan
            // 
            this.GridPlan.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GridPlan.ColumnHeadersHeight = 30;
            this.GridPlan.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnProject,
            this.columnAssemblyCode,
            this.colPlanSo,
            this.columProductName,
            this.ColPlanOnlineQuantity,
            this.ColRealOnlineQuantity,
            this.ColOfflineQuantity,
            this.ColPlanSeq,
            this.ColPlanCode,
            this.colPlanBatch,
            this.ColRunFlag,
            this.colSnFlag});
            this.GridPlan.Location = new System.Drawing.Point(5, 25);
            this.GridPlan.Margin = new System.Windows.Forms.Padding(5);
            this.GridPlan.Name = "GridPlan";
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.GridPlan.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.GridPlan.RowTemplate.Height = 23;
            this.GridPlan.Size = new System.Drawing.Size(681, 199);
            this.GridPlan.TabIndex = 0;
            this.GridPlan.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridPlan_CellClick);
            this.GridPlan.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridPlan_CellDoubleClick);
            this.GridPlan.MouseLeave += new System.EventHandler(this.GridPlan_MouseLeave);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 2000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.GridPlan);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(941, 232);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "计划信息";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label1.Location = new System.Drawing.Point(694, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(244, 199);
            this.label1.TabIndex = 0;
            this.label1.Text = ".";
            // 
            // columnProject
            // 
            this.columnProject.DataPropertyName = "PROJECT_CODE";
            this.columnProject.HeaderText = "工程号";
            this.columnProject.Name = "columnProject";
            // 
            // columnAssemblyCode
            // 
            this.columnAssemblyCode.DataPropertyName = "PRODUCT_MODEL";
            this.columnAssemblyCode.HeaderText = "组件号";
            this.columnAssemblyCode.Name = "columnAssemblyCode";
            this.columnAssemblyCode.Width = 125;
            // 
            // colPlanSo
            // 
            this.colPlanSo.DataPropertyName = "PLAN_SO";
            this.colPlanSo.HeaderText = "项目代码";
            this.colPlanSo.Name = "colPlanSo";
            this.colPlanSo.Width = 150;
            // 
            // columProductName
            // 
            this.columProductName.DataPropertyName = "PLAN_SO_NAME";
            this.columProductName.HeaderText = "项目名称";
            this.columProductName.Name = "columProductName";
            this.columProductName.Width = 210;
            // 
            // ColPlanOnlineQuantity
            // 
            this.ColPlanOnlineQuantity.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColPlanOnlineQuantity.DataPropertyName = "PLAN_QTY";
            this.ColPlanOnlineQuantity.HeaderText = "计划数";
            this.ColPlanOnlineQuantity.Name = "ColPlanOnlineQuantity";
            this.ColPlanOnlineQuantity.ReadOnly = true;
            this.ColPlanOnlineQuantity.Width = 70;
            // 
            // ColRealOnlineQuantity
            // 
            this.ColRealOnlineQuantity.DataPropertyName = "ONLINE_QTY";
            this.ColRealOnlineQuantity.HeaderText = "上线";
            this.ColRealOnlineQuantity.Name = "ColRealOnlineQuantity";
            this.ColRealOnlineQuantity.ReadOnly = true;
            this.ColRealOnlineQuantity.Width = 50;
            // 
            // ColOfflineQuantity
            // 
            this.ColOfflineQuantity.DataPropertyName = "OFFLINE_QTY";
            this.ColOfflineQuantity.HeaderText = "下线";
            this.ColOfflineQuantity.Name = "ColOfflineQuantity";
            this.ColOfflineQuantity.ReadOnly = true;
            this.ColOfflineQuantity.Width = 50;
            // 
            // ColPlanSeq
            // 
            this.ColPlanSeq.DataPropertyName = "PLAN_SEQUENCE";
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
            this.ColPlanCode.Width = 120;
            // 
            // colPlanBatch
            // 
            this.colPlanBatch.DataPropertyName = "PLAN_BATCH";
            this.colPlanBatch.HeaderText = "批次号";
            this.colPlanBatch.Name = "colPlanBatch";
            this.colPlanBatch.ReadOnly = true;
            this.colPlanBatch.Visible = false;
            this.colPlanBatch.Width = 50;
            // 
            // ColRunFlag
            // 
            this.ColRunFlag.DataPropertyName = "RUN_FLAG";
            this.ColRunFlag.HeaderText = "状态";
            this.ColRunFlag.Name = "ColRunFlag";
            this.ColRunFlag.Visible = false;
            this.ColRunFlag.Width = 50;
            // 
            // colSnFlag
            // 
            this.colSnFlag.DataPropertyName = "SN_FLAG";
            this.colSnFlag.HeaderText = "SN";
            this.colSnFlag.Name = "colSnFlag";
            this.colSnFlag.Visible = false;
            this.colSnFlag.Width = 120;
            // 
            // ctrPlan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
            this.Name = "ctrPlan";
            this.Size = new System.Drawing.Size(941, 232);
            ((System.ComponentModel.ISupportInitialize)(this.GridPlan)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView GridPlan;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnProject;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnAssemblyCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPlanSo;
        private System.Windows.Forms.DataGridViewTextBoxColumn columProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColPlanOnlineQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColRealOnlineQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColOfflineQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColPlanSeq;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColPlanCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPlanBatch;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColRunFlag;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSnFlag;
    }
}
