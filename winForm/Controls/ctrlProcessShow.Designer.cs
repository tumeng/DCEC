namespace Rmes.WinForm.Controls
{
    partial class ctrlProcessShow
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gridProcessList = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.colRmesID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCmdStart = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colCmdPause = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colRoutingCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colParentRoutingCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProcessCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProcessName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStartTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEndTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCompleteFlag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProcessSeq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colWorkTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRoutingLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridProcessList)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridProcessList
            // 
            this.gridProcessList.AllowUserToAddRows = false;
            this.gridProcessList.AllowUserToDeleteRows = false;
            this.gridProcessList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridProcessList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colRmesID,
            this.colCmdStart,
            this.colCmdPause,
            this.colRoutingCode,
            this.colParentRoutingCode,
            this.colProcessCode,
            this.colProcessName,
            this.colStartTime,
            this.colEndTime,
            this.colCompleteFlag,
            this.colProcessSeq,
            this.colWorkTime,
            this.colRoutingLevel});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle3.NullValue = null;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridProcessList.DefaultCellStyle = dataGridViewCellStyle3;
            this.gridProcessList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridProcessList.Location = new System.Drawing.Point(3, 25);
            this.gridProcessList.Margin = new System.Windows.Forms.Padding(5);
            this.gridProcessList.Name = "gridProcessList";
            this.gridProcessList.ReadOnly = true;
            this.gridProcessList.RowHeadersVisible = false;
            this.gridProcessList.RowTemplate.Height = 23;
            this.gridProcessList.Size = new System.Drawing.Size(568, 233);
            this.gridProcessList.TabIndex = 0;
            this.gridProcessList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridProcessList_CellContentClick);
            this.gridProcessList.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridProcessList_CellContentDoubleClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.gridProcessList);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(574, 261);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "装配工艺工序信息";
            // 
            // colRmesID
            // 
            this.colRmesID.DataPropertyName = "Rmes_ID";
            this.colRmesID.HeaderText = "RmesID";
            this.colRmesID.Name = "colRmesID";
            this.colRmesID.ReadOnly = true;
            this.colRmesID.Visible = false;
            // 
            // colCmdStart
            // 
            this.colCmdStart.HeaderText = "操作1";
            this.colCmdStart.Name = "colCmdStart";
            this.colCmdStart.ReadOnly = true;
            this.colCmdStart.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colCmdStart.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colCmdStart.Text = "开始";
            this.colCmdStart.UseColumnTextForButtonValue = true;
            this.colCmdStart.Width = 75;
            // 
            // colCmdPause
            // 
            this.colCmdPause.HeaderText = "操作2";
            this.colCmdPause.Name = "colCmdPause";
            this.colCmdPause.ReadOnly = true;
            this.colCmdPause.Text = "暂停";
            // 
            // colRoutingCode
            // 
            this.colRoutingCode.DataPropertyName = "ROUTING_CODE";
            this.colRoutingCode.HeaderText = "工艺代码";
            this.colRoutingCode.Name = "colRoutingCode";
            this.colRoutingCode.ReadOnly = true;
            this.colRoutingCode.Visible = false;
            // 
            // colParentRoutingCode
            // 
            this.colParentRoutingCode.DataPropertyName = "PARENT_ROUTING_CODE";
            this.colParentRoutingCode.HeaderText = "父项工艺";
            this.colParentRoutingCode.Name = "colParentRoutingCode";
            this.colParentRoutingCode.ReadOnly = true;
            this.colParentRoutingCode.Visible = false;
            // 
            // colProcessCode
            // 
            this.colProcessCode.DataPropertyName = "PROCESS_CODE";
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.colProcessCode.DefaultCellStyle = dataGridViewCellStyle1;
            this.colProcessCode.HeaderText = "工序代码";
            this.colProcessCode.Name = "colProcessCode";
            this.colProcessCode.ReadOnly = true;
            this.colProcessCode.Width = 135;
            // 
            // colProcessName
            // 
            this.colProcessName.DataPropertyName = "PROCESS_NAME";
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.colProcessName.DefaultCellStyle = dataGridViewCellStyle2;
            this.colProcessName.HeaderText = "工序名称";
            this.colProcessName.Name = "colProcessName";
            this.colProcessName.ReadOnly = true;
            this.colProcessName.Width = 190;
            // 
            // colStartTime
            // 
            this.colStartTime.DataPropertyName = "START_TIME";
            this.colStartTime.HeaderText = "开始时间";
            this.colStartTime.Name = "colStartTime";
            this.colStartTime.ReadOnly = true;
            this.colStartTime.Width = 165;
            // 
            // colEndTime
            // 
            this.colEndTime.DataPropertyName = "COMPLETE_TIME";
            this.colEndTime.HeaderText = "结束时间";
            this.colEndTime.Name = "colEndTime";
            this.colEndTime.ReadOnly = true;
            this.colEndTime.Width = 165;
            // 
            // colCompleteFlag
            // 
            this.colCompleteFlag.DataPropertyName = "COMPLETE_FLAG";
            this.colCompleteFlag.HeaderText = "完成状态";
            this.colCompleteFlag.Name = "colCompleteFlag";
            this.colCompleteFlag.ReadOnly = true;
            this.colCompleteFlag.Visible = false;
            // 
            // colProcessSeq
            // 
            this.colProcessSeq.DataPropertyName = "PROCESS_SEQ";
            this.colProcessSeq.HeaderText = "装配次序";
            this.colProcessSeq.Name = "colProcessSeq";
            this.colProcessSeq.ReadOnly = true;
            this.colProcessSeq.Visible = false;
            // 
            // colWorkTime
            // 
            this.colWorkTime.DataPropertyName = "WORKTIME_LAST";
            this.colWorkTime.HeaderText = "累计时间";
            this.colWorkTime.Name = "colWorkTime";
            this.colWorkTime.ReadOnly = true;
            // 
            // colRoutingLevel
            // 
            this.colRoutingLevel.DataPropertyName = "ROUTING_LEVEL";
            this.colRoutingLevel.HeaderText = "工艺层级";
            this.colRoutingLevel.Name = "colRoutingLevel";
            this.colRoutingLevel.ReadOnly = true;
            this.colRoutingLevel.Visible = false;
            // 
            // ctrlProcessShow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
            this.Name = "ctrlProcessShow";
            this.Size = new System.Drawing.Size(574, 261);
            ((System.ComponentModel.ISupportInitialize)(this.gridProcessList)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gridProcessList;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRmesID;
        private System.Windows.Forms.DataGridViewButtonColumn colCmdStart;
        private System.Windows.Forms.DataGridViewButtonColumn colCmdPause;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRoutingCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colParentRoutingCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProcessCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProcessName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStartTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEndTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCompleteFlag;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProcessSeq;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWorkTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRoutingLevel;
    }
}
