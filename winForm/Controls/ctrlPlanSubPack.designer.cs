namespace Rmes.WinForm.Controls
{
    partial class ctrlPlanSubPack
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.grdPlan = new System.Windows.Forms.DataGridView();
            this.colSN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPlanSo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPlineCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSubZc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCheckFlag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPlanCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRmesID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStationCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grdPlan)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 2000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // grdPlan
            // 
            this.grdPlan.AllowUserToAddRows = false;
            this.grdPlan.AllowUserToDeleteRows = false;
            this.grdPlan.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdPlan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdPlan.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSN,
            this.colPlanSo,
            this.colPlineCode,
            this.colSubZc,
            this.colCheckFlag,
            this.colPlanCode,
            this.colRmesID,
            this.colStationCode});
            this.grdPlan.Location = new System.Drawing.Point(0, 5);
            this.grdPlan.Margin = new System.Windows.Forms.Padding(5);
            this.grdPlan.Name = "grdPlan";
            this.grdPlan.ReadOnly = true;
            this.grdPlan.RowTemplate.Height = 23;
            this.grdPlan.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdPlan.Size = new System.Drawing.Size(296, 332);
            this.grdPlan.TabIndex = 0;
            this.grdPlan.MouseLeave += new System.EventHandler(this.grdPlan_MouseLeave);
            // 
            // colSN
            // 
            this.colSN.DataPropertyName = "SN";
            this.colSN.HeaderText = "流水号";
            this.colSN.Name = "colSN";
            this.colSN.ReadOnly = true;
            // 
            // colPlanSo
            // 
            this.colPlanSo.DataPropertyName = "PLAN_SO";
            this.colPlanSo.HeaderText = "SO";
            this.colPlanSo.Name = "colPlanSo";
            this.colPlanSo.ReadOnly = true;
            this.colPlanSo.Width = 150;
            // 
            // colPlineCode
            // 
            this.colPlineCode.DataPropertyName = "PLINE_CODE";
            this.colPlineCode.HeaderText = "生产线";
            this.colPlineCode.Name = "colPlineCode";
            this.colPlineCode.ReadOnly = true;
            this.colPlineCode.Visible = false;
            // 
            // colSubZc
            // 
            this.colSubZc.DataPropertyName = "SUB_ZC";
            this.colSubZc.HeaderText = "分装总成零件";
            this.colSubZc.Name = "colSubZc";
            this.colSubZc.ReadOnly = true;
            this.colSubZc.Visible = false;
            // 
            // colCheckFlag
            // 
            this.colCheckFlag.DataPropertyName = "CHECK_FLAG";
            this.colCheckFlag.HeaderText = "校验方式";
            this.colCheckFlag.Name = "colCheckFlag";
            this.colCheckFlag.ReadOnly = true;
            this.colCheckFlag.Visible = false;
            // 
            // colPlanCode
            // 
            this.colPlanCode.DataPropertyName = "PLAN_CODE";
            this.colPlanCode.HeaderText = "计划";
            this.colPlanCode.Name = "colPlanCode";
            this.colPlanCode.ReadOnly = true;
            this.colPlanCode.Visible = false;
            // 
            // colRmesID
            // 
            this.colRmesID.DataPropertyName = "RMES_ID";
            this.colRmesID.HeaderText = "RmesID";
            this.colRmesID.Name = "colRmesID";
            this.colRmesID.ReadOnly = true;
            this.colRmesID.Visible = false;
            // 
            // colStationCode
            // 
            this.colStationCode.DataPropertyName = "STATION_CODE";
            this.colStationCode.HeaderText = "分装站点";
            this.colStationCode.Name = "colStationCode";
            this.colStationCode.ReadOnly = true;
            this.colStationCode.Visible = false;
            // 
            // ctrlPlanSubPack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grdPlan);
            this.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
            this.Name = "ctrlPlanSubPack";
            this.Size = new System.Drawing.Size(296, 342);
            ((System.ComponentModel.ISupportInitialize)(this.grdPlan)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grdPlan;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSN;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPlanSo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPlineCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSubZc;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCheckFlag;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPlanCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRmesID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStationCode;
    }
}
