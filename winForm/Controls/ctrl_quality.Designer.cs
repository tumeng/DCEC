namespace Rmes.WinForm.Controls
{
    partial class ctrl_quality
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.GridQuality = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQuestion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAnswer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStandard = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFlag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUserid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRmesid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridQuality)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.CausesValidation = false;
            this.groupBox1.Controls.Add(this.GridQuality);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(877, 493);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "质量问答";
            // 
            // GridQuality
            // 
            this.GridQuality.AccessibleRole = System.Windows.Forms.AccessibleRole.SplitButton;
            this.GridQuality.AllowUserToAddRows = false;
            this.GridQuality.AllowUserToDeleteRows = false;
            this.GridQuality.CausesValidation = false;
            this.GridQuality.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridQuality.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colQuestion,
            this.colAnswer,
            this.colStandard,
            this.colFlag,
            this.colUserid,
            this.colTime,
            this.colRmesid});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GridQuality.DefaultCellStyle = dataGridViewCellStyle1;
            this.GridQuality.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridQuality.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.GridQuality.Location = new System.Drawing.Point(3, 25);
            this.GridQuality.Margin = new System.Windows.Forms.Padding(5);
            this.GridQuality.Name = "GridQuality";
            this.GridQuality.RowHeadersVisible = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            this.GridQuality.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.GridQuality.RowTemplate.Height = 23;
            this.GridQuality.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.GridQuality.Size = new System.Drawing.Size(871, 465);
            this.GridQuality.TabIndex = 0;
            this.GridQuality.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.GridQuality_CellValidating);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "QUESTION";
            this.dataGridViewTextBoxColumn1.HeaderText = "问题";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 200;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "ANSWER_VALUE";
            this.dataGridViewTextBoxColumn2.HeaderText = "回答";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 200;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "STANDARD_ANSWER";
            this.dataGridViewTextBoxColumn3.HeaderText = "标准答案";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Visible = false;
            this.dataGridViewTextBoxColumn3.Width = 150;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "USER_ID";
            this.dataGridViewTextBoxColumn5.HeaderText = "操作员";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "WORK_TIME";
            this.dataGridViewTextBoxColumn6.HeaderText = "操作时间";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 200;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "RMES_ID";
            this.dataGridViewTextBoxColumn7.HeaderText = "序号";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Visible = false;
            this.dataGridViewTextBoxColumn7.Width = 60;
            // 
            // colQuestion
            // 
            this.colQuestion.DataPropertyName = "QUESTION";
            this.colQuestion.HeaderText = "问题";
            this.colQuestion.Name = "colQuestion";
            this.colQuestion.ReadOnly = true;
            this.colQuestion.Width = 200;
            // 
            // colAnswer
            // 
            this.colAnswer.DataPropertyName = "ANSWER_VALUE";
            this.colAnswer.HeaderText = "回答";
            this.colAnswer.Name = "colAnswer";
            this.colAnswer.Width = 150;
            // 
            // colStandard
            // 
            this.colStandard.DataPropertyName = "STANDARD_ANSWER";
            this.colStandard.HeaderText = "标准答案";
            this.colStandard.Name = "colStandard";
            this.colStandard.ReadOnly = true;
            this.colStandard.Visible = false;
            this.colStandard.Width = 150;
            // 
            // colFlag
            // 
            this.colFlag.DataPropertyName = "QA_FLAG";
            this.colFlag.HeaderText = "标志位";
            this.colFlag.Name = "colFlag";
            this.colFlag.ReadOnly = true;
            this.colFlag.Visible = false;
            this.colFlag.Width = 150;
            // 
            // colUserid
            // 
            this.colUserid.DataPropertyName = "USER_ID";
            this.colUserid.HeaderText = "操作员";
            this.colUserid.Name = "colUserid";
            this.colUserid.ReadOnly = true;
            this.colUserid.Visible = false;
            // 
            // colTime
            // 
            this.colTime.DataPropertyName = "WORK_TIME";
            this.colTime.HeaderText = "操作时间";
            this.colTime.Name = "colTime";
            this.colTime.ReadOnly = true;
            this.colTime.Visible = false;
            this.colTime.Width = 200;
            // 
            // colRmesid
            // 
            this.colRmesid.DataPropertyName = "RMES_ID";
            this.colRmesid.HeaderText = "序号";
            this.colRmesid.Name = "colRmesid";
            this.colRmesid.ReadOnly = true;
            this.colRmesid.Visible = false;
            this.colRmesid.Width = 60;
            // 
            // ctrl_quality
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "ctrl_quality";
            this.Size = new System.Drawing.Size(877, 493);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridQuality)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView GridQuality;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQuestion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAnswer;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStandard;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFlag;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUserid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRmesid;

    }
}
