namespace Rmes.WinForm.Controls
{
    partial class ctrl_ewmgm
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
            this.btnprint = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.model = new System.Windows.Forms.ComboBox();
            this.txtlsh = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtso = new System.Windows.Forms.TextBox();
            this.txtjx = new System.Windows.Forms.TextBox();
            this.txtscrq = new System.Windows.Forms.TextBox();
            this.txtkhh = new System.Windows.Forms.TextBox();
            this.tbprint = new System.Windows.Forms.Button();
            this.obprint = new System.Windows.Forms.Button();
            this.Command1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnprint
            // 
            this.btnprint.Location = new System.Drawing.Point(181, 323);
            this.btnprint.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.btnprint.Name = "btnprint";
            this.btnprint.Size = new System.Drawing.Size(96, 40);
            this.btnprint.TabIndex = 0;
            this.btnprint.Text = "正常打印";
            this.btnprint.UseVisualStyleBackColor = true;
            this.btnprint.Click += new System.EventHandler(this.btnprint_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 29);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "选择模板：";
            // 
            // model
            // 
            this.model.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.model.FormattingEnabled = true;
            this.model.Location = new System.Drawing.Point(160, 26);
            this.model.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.model.Name = "model";
            this.model.Size = new System.Drawing.Size(152, 29);
            this.model.TabIndex = 2;
            // 
            // txtlsh
            // 
            this.txtlsh.Location = new System.Drawing.Point(160, 76);
            this.txtlsh.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.txtlsh.Name = "txtlsh";
            this.txtlsh.Size = new System.Drawing.Size(152, 29);
            this.txtlsh.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 279);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 21);
            this.label2.TabIndex = 4;
            this.label2.Text = "客户号：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 229);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 21);
            this.label3.TabIndex = 5;
            this.label3.Text = "生产日期：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 179);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 21);
            this.label4.TabIndex = 6;
            this.label4.Text = "机型：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 129);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 21);
            this.label5.TabIndex = 7;
            this.label5.Text = "SO：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 79);
            this.label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(106, 21);
            this.label6.TabIndex = 8;
            this.label6.Text = "输入流水号：";
            // 
            // txtso
            // 
            this.txtso.Location = new System.Drawing.Point(160, 126);
            this.txtso.Margin = new System.Windows.Forms.Padding(5);
            this.txtso.Name = "txtso";
            this.txtso.Size = new System.Drawing.Size(152, 29);
            this.txtso.TabIndex = 9;
            this.txtso.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtso_KeyPress);
            // 
            // txtjx
            // 
            this.txtjx.Location = new System.Drawing.Point(160, 176);
            this.txtjx.Margin = new System.Windows.Forms.Padding(5);
            this.txtjx.Name = "txtjx";
            this.txtjx.Size = new System.Drawing.Size(152, 29);
            this.txtjx.TabIndex = 10;
            // 
            // txtscrq
            // 
            this.txtscrq.Location = new System.Drawing.Point(160, 226);
            this.txtscrq.Margin = new System.Windows.Forms.Padding(5);
            this.txtscrq.Name = "txtscrq";
            this.txtscrq.Size = new System.Drawing.Size(152, 29);
            this.txtscrq.TabIndex = 11;
            // 
            // txtkhh
            // 
            this.txtkhh.Location = new System.Drawing.Point(160, 276);
            this.txtkhh.Margin = new System.Windows.Forms.Padding(5);
            this.txtkhh.Name = "txtkhh";
            this.txtkhh.Size = new System.Drawing.Size(152, 29);
            this.txtkhh.TabIndex = 12;
            // 
            // tbprint
            // 
            this.tbprint.Location = new System.Drawing.Point(256, 373);
            this.tbprint.Margin = new System.Windows.Forms.Padding(5);
            this.tbprint.Name = "tbprint";
            this.tbprint.Size = new System.Drawing.Size(96, 40);
            this.tbprint.TabIndex = 13;
            this.tbprint.Text = "ISF二维码";
            this.tbprint.UseVisualStyleBackColor = true;
            this.tbprint.Click += new System.EventHandler(this.tbprint_Click);
            // 
            // obprint
            // 
            this.obprint.Location = new System.Drawing.Point(99, 373);
            this.obprint.Margin = new System.Windows.Forms.Padding(5);
            this.obprint.Name = "obprint";
            this.obprint.Size = new System.Drawing.Size(96, 40);
            this.obprint.TabIndex = 14;
            this.obprint.Text = "ISF一维码";
            this.obprint.UseVisualStyleBackColor = true;
            this.obprint.Click += new System.EventHandler(this.obprint_Click);
            // 
            // Command1
            // 
            this.Command1.Location = new System.Drawing.Point(333, 123);
            this.Command1.Margin = new System.Windows.Forms.Padding(5);
            this.Command1.Name = "Command1";
            this.Command1.Size = new System.Drawing.Size(125, 33);
            this.Command1.TabIndex = 15;
            this.Command1.Text = "客户号/机型号";
            this.Command1.UseVisualStyleBackColor = true;
            this.Command1.Click += new System.EventHandler(this.Command1_Click);
            // 
            // ctrl_ewmgm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Command1);
            this.Controls.Add(this.obprint);
            this.Controls.Add(this.tbprint);
            this.Controls.Add(this.txtkhh);
            this.Controls.Add(this.txtscrq);
            this.Controls.Add(this.txtjx);
            this.Controls.Add(this.txtso);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtlsh);
            this.Controls.Add(this.model);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnprint);
            this.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "ctrl_ewmgm";
            this.Size = new System.Drawing.Size(484, 434);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnprint;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox model;
        private System.Windows.Forms.TextBox txtlsh;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtso;
        private System.Windows.Forms.TextBox txtjx;
        private System.Windows.Forms.TextBox txtscrq;
        private System.Windows.Forms.TextBox txtkhh;
        private System.Windows.Forms.Button tbprint;
        private System.Windows.Forms.Button obprint;
        private System.Windows.Forms.Button Command1;
    }
}
