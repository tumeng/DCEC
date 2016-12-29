namespace RMES.MaterialCal
{
    partial class MatCal
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MatCal));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.checkBoxHandle = new System.Windows.Forms.CheckBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnHandle = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            this.lstBoxPline = new System.Windows.Forms.ListBox();
            this.TimerSF = new System.Windows.Forms.Timer(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.buttonSave);
            this.groupBox1.Controls.Add(this.txtLog);
            this.groupBox1.Controls.Add(this.checkBoxHandle);
            this.groupBox1.Controls.Add(this.btnClear);
            this.groupBox1.Controls.Add(this.btnHandle);
            this.groupBox1.Controls.Add(this.btnRun);
            this.groupBox1.Controls.Add(this.lstBoxPline);
            this.groupBox1.Location = new System.Drawing.Point(16, 20);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(1061, 502);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "三方物料计算";
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonSave.Location = new System.Drawing.Point(249, 370);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(7, 9, 7, 9);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(120, 45);
            this.buttonSave.TabIndex = 18;
            this.buttonSave.Text = "保存日志";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // txtLog
            // 
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.BackColor = System.Drawing.Color.Black;
            this.txtLog.ForeColor = System.Drawing.Color.SpringGreen;
            this.txtLog.Location = new System.Drawing.Point(404, 33);
            this.txtLog.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(648, 444);
            this.txtLog.TabIndex = 17;
            this.txtLog.Text = "";
            this.txtLog.TextChanged += new System.EventHandler(this.txtLog_TextChanged);
            // 
            // checkBoxHandle
            // 
            this.checkBoxHandle.AutoSize = true;
            this.checkBoxHandle.Location = new System.Drawing.Point(249, 200);
            this.checkBoxHandle.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxHandle.Name = "checkBoxHandle";
            this.checkBoxHandle.Size = new System.Drawing.Size(84, 24);
            this.checkBoxHandle.TabIndex = 16;
            this.checkBoxHandle.Text = "手动执行";
            this.checkBoxHandle.UseVisualStyleBackColor = true;
            this.checkBoxHandle.CheckedChanged += new System.EventHandler(this.checkBoxHandle_CheckedChanged);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClear.Location = new System.Drawing.Point(249, 432);
            this.btnClear.Margin = new System.Windows.Forms.Padding(7, 9, 7, 9);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(120, 45);
            this.btnClear.TabIndex = 15;
            this.btnClear.Text = "清空日志";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnHandle
            // 
            this.btnHandle.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHandle.Location = new System.Drawing.Point(249, 240);
            this.btnHandle.Margin = new System.Windows.Forms.Padding(7, 9, 7, 9);
            this.btnHandle.Name = "btnHandle";
            this.btnHandle.Size = new System.Drawing.Size(120, 45);
            this.btnHandle.TabIndex = 14;
            this.btnHandle.Text = "手动执行";
            this.btnHandle.UseVisualStyleBackColor = true;
            this.btnHandle.Click += new System.EventHandler(this.btnHandle_Click);
            // 
            // btnRun
            // 
            this.btnRun.BackColor = System.Drawing.SystemColors.Control;
            this.btnRun.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnRun.Location = new System.Drawing.Point(249, 36);
            this.btnRun.Margin = new System.Windows.Forms.Padding(7, 9, 7, 9);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(120, 45);
            this.btnRun.TabIndex = 12;
            this.btnRun.Text = "启动";
            this.btnRun.UseVisualStyleBackColor = false;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // lstBoxPline
            // 
            this.lstBoxPline.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lstBoxPline.BackColor = System.Drawing.Color.Black;
            this.lstBoxPline.ForeColor = System.Drawing.Color.SpringGreen;
            this.lstBoxPline.FormattingEnabled = true;
            this.lstBoxPline.ItemHeight = 20;
            this.lstBoxPline.Location = new System.Drawing.Point(8, 33);
            this.lstBoxPline.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lstBoxPline.Name = "lstBoxPline";
            this.lstBoxPline.Size = new System.Drawing.Size(229, 444);
            this.lstBoxPline.TabIndex = 0;
            this.lstBoxPline.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstBoxPline_MouseDoubleClick);
            // 
            // TimerSF
            // 
            this.TimerSF.Interval = 60000;
            this.TimerSF.Tick += new System.EventHandler(this.TimerSF_Tick);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // MatCal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1090, 528);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MatCal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "三方物料计算";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MatCal_FormClosing);
            this.Load += new System.EventHandler(this.MatCal_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox lstBoxPline;
        private System.Windows.Forms.RichTextBox txtLog;
        private System.Windows.Forms.CheckBox checkBoxHandle;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnHandle;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Timer TimerSF;
        private System.Windows.Forms.Timer timer1;


    }
}

