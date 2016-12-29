namespace Rmes.WinForm.Controls
{
    partial class ctrlOptionShow
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
            this.gridOptionList = new System.Windows.Forms.DataGridView();
            this.operate = new System.Windows.Forms.DataGridViewButtonColumn();
            this.option_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.option_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.etime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.operates = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridOptionList)).BeginInit();
            this.SuspendLayout();
            // 
            // gridOptionList
            // 
            this.gridOptionList.AllowUserToAddRows = false;
            this.gridOptionList.AllowUserToDeleteRows = false;
            this.gridOptionList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridOptionList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.gridOptionList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridOptionList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.operate,
            this.option_code,
            this.option_name,
            this.stime,
            this.etime,
            this.operates});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.NullValue = null;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridOptionList.DefaultCellStyle = dataGridViewCellStyle3;
            this.gridOptionList.Location = new System.Drawing.Point(3, 3);
            this.gridOptionList.Name = "gridOptionList";
            this.gridOptionList.ReadOnly = true;
            this.gridOptionList.RowTemplate.Height = 23;
            this.gridOptionList.Size = new System.Drawing.Size(552, 119);
            this.gridOptionList.TabIndex = 0;
            this.gridOptionList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridOptionList_CellContentClick);
            // 
            // operate
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.NullValue = "开始";
            this.operate.DefaultCellStyle = dataGridViewCellStyle1;
            this.operate.HeaderText = "操作";
            this.operate.Name = "operate";
            this.operate.ReadOnly = true;
            this.operate.Width = 35;
            // 
            // option_code
            // 
            this.option_code.DataPropertyName = "OPTION_CODE";
            this.option_code.HeaderText = "工序代码";
            this.option_code.Name = "option_code";
            this.option_code.ReadOnly = true;
            this.option_code.Width = 78;
            // 
            // option_name
            // 
            this.option_name.DataPropertyName = "OPTION_NAME";
            this.option_name.HeaderText = "工序名称";
            this.option_name.Name = "option_name";
            this.option_name.ReadOnly = true;
            this.option_name.Width = 78;
            // 
            // stime
            // 
            this.stime.DataPropertyName = "START_TIME";
            this.stime.HeaderText = "开始时间";
            this.stime.Name = "stime";
            this.stime.ReadOnly = true;
            this.stime.Width = 78;
            // 
            // etime
            // 
            this.etime.DataPropertyName = "END_TIME";
            this.etime.HeaderText = "结束时间";
            this.etime.Name = "etime";
            this.etime.ReadOnly = true;
            this.etime.Width = 78;
            // 
            // operates
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.NullValue = "结束";
            this.operates.DefaultCellStyle = dataGridViewCellStyle2;
            this.operates.HeaderText = "操作";
            this.operates.Name = "operates";
            this.operates.ReadOnly = true;
            this.operates.Width = 35;
            // 
            // ctrlOptionShow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridOptionList);
            this.Name = "ctrlOptionShow";
            this.Size = new System.Drawing.Size(560, 126);
            ((System.ComponentModel.ISupportInitialize)(this.gridOptionList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gridOptionList;
        private System.Windows.Forms.DataGridViewButtonColumn operate;
        private System.Windows.Forms.DataGridViewTextBoxColumn option_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn option_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn stime;
        private System.Windows.Forms.DataGridViewTextBoxColumn etime;
        private System.Windows.Forms.DataGridViewButtonColumn operates;
    }
}
