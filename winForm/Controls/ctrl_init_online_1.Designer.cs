namespace Rmes.WinForm.Controls
{
    partial class ctrl_init_online_1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctrl_init_online_1));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmdReset = new System.Windows.Forms.Button();
            this.cmdJh = new System.Windows.Forms.Button();
            this.cmdManual = new System.Windows.Forms.Button();
            this.groupbox2 = new System.Windows.Forms.GroupBox();
            this.TxtPrtMs = new System.Windows.Forms.TextBox();
            this.CmdPrttm = new System.Windows.Forms.Button();
            this.cmdMPrt = new System.Windows.Forms.Button();
            this.TxtPrtJx = new System.Windows.Forms.TextBox();
            this.TxtPrtLsh = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmdprntm = new System.Windows.Forms.Button();
            this.Text1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkdy = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupbox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cmdReset);
            this.groupBox1.Controls.Add(this.cmdJh);
            this.groupBox1.Controls.Add(this.cmdManual);
            this.groupBox1.Controls.Add(this.groupbox2);
            this.groupBox1.Controls.Add(this.cmdprntm);
            this.groupBox1.Controls.Add(this.Text1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.chkdy);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(306, 248);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "打印条码";
            // 
            // cmdReset
            // 
            this.cmdReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdReset.Location = new System.Drawing.Point(106, 206);
            this.cmdReset.Name = "cmdReset";
            this.cmdReset.Size = new System.Drawing.Size(85, 36);
            this.cmdReset.TabIndex = 7;
            this.cmdReset.Text = "调整原点";
            this.cmdReset.UseVisualStyleBackColor = true;
            this.cmdReset.Click += new System.EventHandler(this.cmdReset_Click);
            // 
            // cmdJh
            // 
            this.cmdJh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdJh.Location = new System.Drawing.Point(203, 206);
            this.cmdJh.Name = "cmdJh";
            this.cmdJh.Size = new System.Drawing.Size(85, 36);
            this.cmdJh.TabIndex = 6;
            this.cmdJh.Text = "当天计划";
            this.cmdJh.UseVisualStyleBackColor = true;
            this.cmdJh.Visible = false;
            // 
            // cmdManual
            // 
            this.cmdManual.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdManual.Location = new System.Drawing.Point(7, 206);
            this.cmdManual.Name = "cmdManual";
            this.cmdManual.Size = new System.Drawing.Size(85, 36);
            this.cmdManual.TabIndex = 5;
            this.cmdManual.Text = "手工打印";
            this.cmdManual.UseVisualStyleBackColor = true;
            this.cmdManual.Click += new System.EventHandler(this.cmdManual_Click);
            // 
            // groupbox2
            // 
            this.groupbox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupbox2.Controls.Add(this.TxtPrtMs);
            this.groupbox2.Controls.Add(this.CmdPrttm);
            this.groupbox2.Controls.Add(this.cmdMPrt);
            this.groupbox2.Controls.Add(this.TxtPrtJx);
            this.groupbox2.Controls.Add(this.TxtPrtLsh);
            this.groupbox2.Controls.Add(this.label5);
            this.groupbox2.Controls.Add(this.label4);
            this.groupbox2.Controls.Add(this.label3);
            this.groupbox2.Controls.Add(this.label2);
            this.groupbox2.Location = new System.Drawing.Point(7, 62);
            this.groupbox2.Name = "groupbox2";
            this.groupbox2.Size = new System.Drawing.Size(293, 138);
            this.groupbox2.TabIndex = 4;
            this.groupbox2.TabStop = false;
            this.groupbox2.Text = "手动打印";
            // 
            // TxtPrtMs
            // 
            this.TxtPrtMs.Location = new System.Drawing.Point(31, 103);
            this.TxtPrtMs.Name = "TxtPrtMs";
            this.TxtPrtMs.Size = new System.Drawing.Size(24, 29);
            this.TxtPrtMs.TabIndex = 9;
            this.TxtPrtMs.Text = "1";
            // 
            // CmdPrttm
            // 
            this.CmdPrttm.Image = ((System.Drawing.Image)(resources.GetObject("CmdPrttm.Image")));
            this.CmdPrttm.Location = new System.Drawing.Point(251, 29);
            this.CmdPrttm.Name = "CmdPrttm";
            this.CmdPrttm.Size = new System.Drawing.Size(36, 23);
            this.CmdPrttm.TabIndex = 8;
            this.CmdPrttm.UseVisualStyleBackColor = true;
            this.CmdPrttm.Click += new System.EventHandler(this.CmdPrttm_Click);
            // 
            // cmdMPrt
            // 
            this.cmdMPrt.Location = new System.Drawing.Point(99, 101);
            this.cmdMPrt.Name = "cmdMPrt";
            this.cmdMPrt.Size = new System.Drawing.Size(75, 26);
            this.cmdMPrt.TabIndex = 6;
            this.cmdMPrt.Text = "打印";
            this.cmdMPrt.UseVisualStyleBackColor = true;
            this.cmdMPrt.Click += new System.EventHandler(this.cmdMPrt_Click);
            // 
            // TxtPrtJx
            // 
            this.TxtPrtJx.Location = new System.Drawing.Point(74, 61);
            this.TxtPrtJx.Name = "TxtPrtJx";
            this.TxtPrtJx.Size = new System.Drawing.Size(170, 29);
            this.TxtPrtJx.TabIndex = 5;
            // 
            // TxtPrtLsh
            // 
            this.TxtPrtLsh.Location = new System.Drawing.Point(74, 27);
            this.TxtPrtLsh.Name = "TxtPrtLsh";
            this.TxtPrtLsh.Size = new System.Drawing.Size(170, 29);
            this.TxtPrtLsh.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(59, 106);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 21);
            this.label5.TabIndex = 3;
            this.label5.Text = "面";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(-1, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 21);
            this.label4.TabIndex = 2;
            this.label4.Text = "共";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 21);
            this.label3.TabIndex = 1;
            this.label3.Text = "机型：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 21);
            this.label2.TabIndex = 0;
            this.label2.Text = "流水号：";
            // 
            // cmdprntm
            // 
            this.cmdprntm.Location = new System.Drawing.Point(187, 27);
            this.cmdprntm.Name = "cmdprntm";
            this.cmdprntm.Size = new System.Drawing.Size(119, 29);
            this.cmdprntm.TabIndex = 3;
            this.cmdprntm.Text = "定制打印条码";
            this.cmdprntm.UseVisualStyleBackColor = true;
            // 
            // Text1
            // 
            this.Text1.Location = new System.Drawing.Point(129, 27);
            this.Text1.Name = "Text1";
            this.Text1.Size = new System.Drawing.Size(24, 29);
            this.Text1.TabIndex = 1;
            this.Text1.Text = "2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(155, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 21);
            this.label1.TabIndex = 2;
            this.label1.Text = "份";
            // 
            // chkdy
            // 
            this.chkdy.AutoSize = true;
            this.chkdy.Location = new System.Drawing.Point(7, 29);
            this.chkdy.Name = "chkdy";
            this.chkdy.Size = new System.Drawing.Size(125, 25);
            this.chkdy.TabIndex = 0;
            this.chkdy.Text = "自动打印条码";
            this.chkdy.UseVisualStyleBackColor = true;
            // 
            // ctrl_init_online
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
            this.Name = "ctrl_init_online";
            this.Size = new System.Drawing.Size(312, 254);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupbox2.ResumeLayout(false);
            this.groupbox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupbox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button cmdprntm;
        private System.Windows.Forms.TextBox Text1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkdy;
        private System.Windows.Forms.Button cmdManual;
        private System.Windows.Forms.Button CmdPrttm;
        private System.Windows.Forms.Button cmdMPrt;
        private System.Windows.Forms.TextBox TxtPrtJx;
        private System.Windows.Forms.TextBox TxtPrtLsh;
        private System.Windows.Forms.Button cmdReset;
        private System.Windows.Forms.Button cmdJh;
        private System.Windows.Forms.TextBox TxtPrtMs;
    }
}
