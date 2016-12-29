namespace Rmes.WinForm.Controls
{
    partial class ctrl_yqxxtsdy
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
            this.label6 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.CmdTc = new System.Windows.Forms.Button();
            this.CmdQx = new System.Windows.Forms.Button();
            this.CmdDy = new System.Windows.Forms.Button();
            this.txtSl = new System.Windows.Forms.TextBox();
            this.cmdDyms = new System.Windows.Forms.Button();
            this.TxtTstm = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtyh = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtso = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtjx = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(125, 4);
            this.label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(159, 28);
            this.label6.TabIndex = 11;
            this.label6.Text = "特殊条码打印：";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtjx);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtso);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtyh);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.TxtTstm);
            this.panel1.Controls.Add(this.CmdTc);
            this.panel1.Controls.Add(this.CmdQx);
            this.panel1.Controls.Add(this.CmdDy);
            this.panel1.Controls.Add(this.txtSl);
            this.panel1.Controls.Add(this.cmdDyms);
            this.panel1.Location = new System.Drawing.Point(9, 35);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(375, 361);
            this.panel1.TabIndex = 10;
            // 
            // CmdTc
            // 
            this.CmdTc.Location = new System.Drawing.Point(244, 294);
            this.CmdTc.Margin = new System.Windows.Forms.Padding(5);
            this.CmdTc.Name = "CmdTc";
            this.CmdTc.Size = new System.Drawing.Size(96, 31);
            this.CmdTc.TabIndex = 7;
            this.CmdTc.Text = "退出";
            this.CmdTc.UseVisualStyleBackColor = true;
            this.CmdTc.Click += new System.EventHandler(this.CmdTc_Click);
            // 
            // CmdQx
            // 
            this.CmdQx.Location = new System.Drawing.Point(131, 294);
            this.CmdQx.Margin = new System.Windows.Forms.Padding(5);
            this.CmdQx.Name = "CmdQx";
            this.CmdQx.Size = new System.Drawing.Size(96, 31);
            this.CmdQx.TabIndex = 6;
            this.CmdQx.Text = "取消";
            this.CmdQx.UseVisualStyleBackColor = true;
            this.CmdQx.Click += new System.EventHandler(this.CmdQx_Click);
            // 
            // CmdDy
            // 
            this.CmdDy.Location = new System.Drawing.Point(18, 294);
            this.CmdDy.Margin = new System.Windows.Forms.Padding(5);
            this.CmdDy.Name = "CmdDy";
            this.CmdDy.Size = new System.Drawing.Size(96, 31);
            this.CmdDy.TabIndex = 4;
            this.CmdDy.Text = "打印";
            this.CmdDy.UseVisualStyleBackColor = true;
            this.CmdDy.Click += new System.EventHandler(this.CmdDy_Click);
            // 
            // txtSl
            // 
            this.txtSl.Location = new System.Drawing.Point(122, 235);
            this.txtSl.Name = "txtSl";
            this.txtSl.Size = new System.Drawing.Size(40, 29);
            this.txtSl.TabIndex = 3;
            this.txtSl.Text = "2";
            this.txtSl.Visible = false;
            // 
            // cmdDyms
            // 
            this.cmdDyms.Location = new System.Drawing.Point(18, 233);
            this.cmdDyms.Margin = new System.Windows.Forms.Padding(5);
            this.cmdDyms.Name = "cmdDyms";
            this.cmdDyms.Size = new System.Drawing.Size(96, 31);
            this.cmdDyms.TabIndex = 2;
            this.cmdDyms.Text = "打印数";
            this.cmdDyms.UseVisualStyleBackColor = true;
            this.cmdDyms.Click += new System.EventHandler(this.cmdDyms_Click);
            // 
            // TxtTstm
            // 
            this.TxtTstm.Location = new System.Drawing.Point(121, 14);
            this.TxtTstm.Margin = new System.Windows.Forms.Padding(5);
            this.TxtTstm.Name = "TxtTstm";
            this.TxtTstm.Size = new System.Drawing.Size(184, 29);
            this.TxtTstm.TabIndex = 9;
            this.TxtTstm.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtTstm_KeyPress);
            this.TxtTstm.Leave += new System.EventHandler(this.TxtTstm_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 21);
            this.label1.TabIndex = 10;
            this.label1.Text = "流水号：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 179);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 21);
            this.label2.TabIndex = 12;
            this.label2.Text = "客户：";
            // 
            // txtyh
            // 
            this.txtyh.Location = new System.Drawing.Point(121, 176);
            this.txtyh.Margin = new System.Windows.Forms.Padding(5);
            this.txtyh.Name = "txtyh";
            this.txtyh.Size = new System.Drawing.Size(184, 29);
            this.txtyh.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 21);
            this.label3.TabIndex = 14;
            this.label3.Text = "SO：";
            // 
            // txtso
            // 
            this.txtso.Location = new System.Drawing.Point(121, 68);
            this.txtso.Margin = new System.Windows.Forms.Padding(5);
            this.txtso.Name = "txtso";
            this.txtso.Size = new System.Drawing.Size(184, 29);
            this.txtso.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 125);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 21);
            this.label4.TabIndex = 16;
            this.label4.Text = "机型：";
            // 
            // txtjx
            // 
            this.txtjx.Location = new System.Drawing.Point(121, 122);
            this.txtjx.Margin = new System.Windows.Forms.Padding(5);
            this.txtjx.Name = "txtjx";
            this.txtjx.Size = new System.Drawing.Size(184, 29);
            this.txtjx.TabIndex = 15;
            // 
            // ctrl_yqxxtsdy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label6);
            this.Controls.Add(this.panel1);
            this.Name = "ctrl_yqxxtsdy";
            this.Size = new System.Drawing.Size(400, 412);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button CmdTc;
        private System.Windows.Forms.Button CmdQx;
        private System.Windows.Forms.Button CmdDy;
        private System.Windows.Forms.TextBox txtSl;
        private System.Windows.Forms.Button cmdDyms;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtjx;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtso;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtyh;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtTstm;
    }
}
