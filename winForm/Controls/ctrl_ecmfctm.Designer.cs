namespace Rmes.WinForm.Controls
{
    partial class ctrl_ecmfctm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.txtLjh = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLsh = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.dgvInsite = new System.Windows.Forms.DataGridView();
            this.colSn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPlancode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInsite)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.txtLjh);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtSo);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtLsh);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.dgvInsite);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(738, 232);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ECM防错条码打印";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(638, 26);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(94, 30);
            this.button2.TabIndex = 14;
            this.button2.Text = "自动打印";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtLjh
            // 
            this.txtLjh.Location = new System.Drawing.Point(397, 28);
            this.txtLjh.Name = "txtLjh";
            this.txtLjh.Size = new System.Drawing.Size(153, 29);
            this.txtLjh.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(326, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 21);
            this.label3.TabIndex = 12;
            this.label3.Text = "零件号：";
            // 
            // txtSo
            // 
            this.txtSo.Location = new System.Drawing.Point(220, 28);
            this.txtSo.Name = "txtSo";
            this.txtSo.Size = new System.Drawing.Size(100, 29);
            this.txtSo.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(179, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 21);
            this.label2.TabIndex = 10;
            this.label2.Text = "SO：";
            // 
            // txtLsh
            // 
            this.txtLsh.Location = new System.Drawing.Point(73, 28);
            this.txtLsh.Name = "txtLsh";
            this.txtLsh.Size = new System.Drawing.Size(100, 29);
            this.txtLsh.TabIndex = 9;
            this.txtLsh.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLsh_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 21);
            this.label1.TabIndex = 8;
            this.label1.Text = "流水号：";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(556, 26);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 30);
            this.button1.TabIndex = 7;
            this.button1.Text = "打印";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dgvInsite
            // 
            this.dgvInsite.AllowUserToAddRows = false;
            this.dgvInsite.AllowUserToDeleteRows = false;
            this.dgvInsite.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvInsite.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInsite.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSn,
            this.colSo,
            this.colPlancode});
            this.dgvInsite.Location = new System.Drawing.Point(3, 70);
            this.dgvInsite.Margin = new System.Windows.Forms.Padding(5);
            this.dgvInsite.Name = "dgvInsite";
            this.dgvInsite.RowHeadersVisible = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.dgvInsite.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvInsite.RowTemplate.Height = 23;
            this.dgvInsite.Size = new System.Drawing.Size(732, 159);
            this.dgvInsite.TabIndex = 0;
            this.dgvInsite.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvInsite_CellContentClick);
            // 
            // colSn
            // 
            this.colSn.DataPropertyName = "SN";
            this.colSn.HeaderText = "流水号";
            this.colSn.Name = "colSn";
            this.colSn.ReadOnly = true;
            // 
            // colSo
            // 
            this.colSo.DataPropertyName = "PLAN_SO";
            this.colSo.HeaderText = "SO";
            this.colSo.Name = "colSo";
            this.colSo.ReadOnly = true;
            this.colSo.Width = 150;
            // 
            // colPlancode
            // 
            this.colPlancode.DataPropertyName = "WORK_TIME";
            this.colPlancode.HeaderText = "日期时间";
            this.colPlancode.Name = "colPlancode";
            this.colPlancode.ReadOnly = true;
            this.colPlancode.Width = 150;
            // 
            // timer1
            // 
            this.timer1.Interval = 10000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ctrl_ecmfctm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "ctrl_ecmfctm";
            this.Size = new System.Drawing.Size(738, 232);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInsite)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvInsite;
        private System.Windows.Forms.TextBox txtLjh;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtLsh;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSn;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPlancode;
    }
}
