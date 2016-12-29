namespace Rmes.WinForm.Controls
{
    partial class ctrl_sgbd
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.txtnewlsh = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtoldlsh = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtoldsqd = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvSg = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtlsh = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtoaso = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtoalsh = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colsqd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colfdjlsh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.coltime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.coloalsh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.coloaso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPlancode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colsl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colsbd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSg)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.button2);
            this.groupBox3.Controls.Add(this.txtnewlsh);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.txtoldlsh);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.txtoldsqd);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.dataGridView1);
            this.groupBox3.Location = new System.Drawing.Point(3, 268);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(859, 215);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "变更流水号对应关系";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(108, 166);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 30);
            this.button2.TabIndex = 9;
            this.button2.Text = "绑定";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtnewlsh
            // 
            this.txtnewlsh.Location = new System.Drawing.Point(108, 122);
            this.txtnewlsh.Name = "txtnewlsh";
            this.txtnewlsh.Size = new System.Drawing.Size(145, 29);
            this.txtnewlsh.TabIndex = 8;
            this.txtnewlsh.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtnewlsh_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 125);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 21);
            this.label6.TabIndex = 7;
            this.label6.Text = "新流水号：";
            // 
            // txtoldlsh
            // 
            this.txtoldlsh.Location = new System.Drawing.Point(108, 75);
            this.txtoldlsh.Name = "txtoldlsh";
            this.txtoldlsh.Size = new System.Drawing.Size(145, 29);
            this.txtoldlsh.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 78);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 21);
            this.label5.TabIndex = 5;
            this.label5.Text = "旧流水号：";
            // 
            // txtoldsqd
            // 
            this.txtoldsqd.Location = new System.Drawing.Point(108, 28);
            this.txtoldsqd.Name = "txtoldsqd";
            this.txtoldsqd.Size = new System.Drawing.Size(145, 29);
            this.txtoldsqd.TabIndex = 4;
            this.txtoldsqd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtoldsqd_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 21);
            this.label4.TabIndex = 3;
            this.label4.Text = "申请单：";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colsqd,
            this.colfdjlsh,
            this.coltime});
            this.dataGridView1.Location = new System.Drawing.Point(276, 14);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(5);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(575, 193);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.dgvSg);
            this.groupBox2.Location = new System.Drawing.Point(0, 68);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(865, 194);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // dgvSg
            // 
            this.dgvSg.AllowUserToAddRows = false;
            this.dgvSg.AllowUserToDeleteRows = false;
            this.dgvSg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSg.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.coloalsh,
            this.coloaso,
            this.colPlancode,
            this.colsl,
            this.Column2,
            this.Column3,
            this.Column4,
            this.colsbd});
            this.dgvSg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSg.Location = new System.Drawing.Point(3, 25);
            this.dgvSg.Margin = new System.Windows.Forms.Padding(5);
            this.dgvSg.Name = "dgvSg";
            this.dgvSg.RowHeadersVisible = false;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.dgvSg.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvSg.RowTemplate.Height = 23;
            this.dgvSg.Size = new System.Drawing.Size(859, 166);
            this.dgvSg.TabIndex = 1;
            this.dgvSg.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSg_CellContentClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.txtlsh);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtoaso);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtoalsh);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(865, 68);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "绑定流水号";
            // 
            // txtlsh
            // 
            this.txtlsh.Location = new System.Drawing.Point(515, 22);
            this.txtlsh.Name = "txtlsh";
            this.txtlsh.Size = new System.Drawing.Size(153, 29);
            this.txtlsh.TabIndex = 6;
            this.txtlsh.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtlsh_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(451, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 21);
            this.label3.TabIndex = 5;
            this.label3.Text = "流水号：";
            // 
            // txtoaso
            // 
            this.txtoaso.Location = new System.Drawing.Point(314, 22);
            this.txtoaso.Name = "txtoaso";
            this.txtoaso.Size = new System.Drawing.Size(120, 29);
            this.txtoaso.TabIndex = 4;
            this.txtoaso.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtoaso_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(260, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 21);
            this.label2.TabIndex = 3;
            this.label2.Text = "SO：";
            // 
            // txtoalsh
            // 
            this.txtoalsh.Location = new System.Drawing.Point(70, 22);
            this.txtoalsh.Name = "txtoalsh";
            this.txtoalsh.Size = new System.Drawing.Size(184, 29);
            this.txtoalsh.TabIndex = 2;
            this.txtoalsh.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtoalsh_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "申请单：";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(674, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 30);
            this.button1.TabIndex = 0;
            this.button1.Text = "绑定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "OALSH";
            this.dataGridViewTextBoxColumn1.HeaderText = "申请单流水号";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 200;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "SO";
            this.dataGridViewTextBoxColumn2.HeaderText = "SO";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 120;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "SOQY";
            this.dataGridViewTextBoxColumn3.HeaderText = "SO所属区域";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 150;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "SL";
            this.dataGridViewTextBoxColumn4.HeaderText = "数量";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "COUNT2";
            this.dataGridViewTextBoxColumn5.HeaderText = "已标定数量";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 120;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "ECMCODE";
            this.dataGridViewTextBoxColumn6.HeaderText = "ECMCODE";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "YHMC";
            this.dataGridViewTextBoxColumn7.HeaderText = "客户名称";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "SECOND_BD";
            this.dataGridViewTextBoxColumn8.HeaderText = "二次标定";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.DataPropertyName = "OALSH";
            this.dataGridViewTextBoxColumn9.HeaderText = "申请单流水号";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            this.dataGridViewTextBoxColumn9.Width = 200;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.DataPropertyName = "FDJLSH";
            this.dataGridViewTextBoxColumn10.HeaderText = "发动机流水号";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            this.dataGridViewTextBoxColumn10.Width = 150;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.DataPropertyName = "RECORD_TIME";
            this.dataGridViewTextBoxColumn11.FillWeight = 200F;
            this.dataGridViewTextBoxColumn11.HeaderText = "记录时间";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ReadOnly = true;
            this.dataGridViewTextBoxColumn11.Width = 150;
            // 
            // colsqd
            // 
            this.colsqd.DataPropertyName = "OALSH";
            this.colsqd.HeaderText = "申请单流水号";
            this.colsqd.Name = "colsqd";
            this.colsqd.ReadOnly = true;
            this.colsqd.Width = 200;
            // 
            // colfdjlsh
            // 
            this.colfdjlsh.DataPropertyName = "FDJLSH";
            this.colfdjlsh.HeaderText = "发动机流水号";
            this.colfdjlsh.Name = "colfdjlsh";
            this.colfdjlsh.ReadOnly = true;
            this.colfdjlsh.Width = 150;
            // 
            // coltime
            // 
            this.coltime.DataPropertyName = "RECORD_TIME";
            this.coltime.FillWeight = 200F;
            this.coltime.HeaderText = "记录时间";
            this.coltime.Name = "coltime";
            this.coltime.ReadOnly = true;
            this.coltime.Width = 150;
            // 
            // coloalsh
            // 
            this.coloalsh.DataPropertyName = "OALSH";
            this.coloalsh.HeaderText = "申请单流水号";
            this.coloalsh.Name = "coloalsh";
            this.coloalsh.ReadOnly = true;
            this.coloalsh.Width = 200;
            // 
            // coloaso
            // 
            this.coloaso.DataPropertyName = "SO";
            this.coloaso.HeaderText = "SO";
            this.coloaso.Name = "coloaso";
            this.coloaso.ReadOnly = true;
            this.coloaso.Width = 120;
            // 
            // colPlancode
            // 
            this.colPlancode.DataPropertyName = "SOQY";
            this.colPlancode.HeaderText = "SO所属区域";
            this.colPlancode.Name = "colPlancode";
            this.colPlancode.ReadOnly = true;
            this.colPlancode.Width = 150;
            // 
            // colsl
            // 
            this.colsl.DataPropertyName = "SL";
            this.colsl.HeaderText = "数量";
            this.colsl.Name = "colsl";
            this.colsl.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "COUNT2";
            this.Column2.HeaderText = "已标定数量";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 120;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "ECMCODE";
            this.Column3.HeaderText = "ECMCODE";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "YHMC";
            this.Column4.HeaderText = "客户名称";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // colsbd
            // 
            this.colsbd.DataPropertyName = "SECOND_BD";
            this.colsbd.HeaderText = "二次标定";
            this.colsbd.Name = "colsbd";
            this.colsbd.ReadOnly = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(779, 20);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 30);
            this.button3.TabIndex = 7;
            this.button3.Text = "刷新";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // ctrl_sgbd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "ctrl_sgbd";
            this.Size = new System.Drawing.Size(865, 486);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSg)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtlsh;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtoaso;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtoalsh;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dgvSg;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtnewlsh;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtoldlsh;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtoldsqd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn coloalsh;
        private System.Windows.Forms.DataGridViewTextBoxColumn coloaso;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPlancode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colsl;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn colsbd;
        private System.Windows.Forms.DataGridViewTextBoxColumn colsqd;
        private System.Windows.Forms.DataGridViewTextBoxColumn colfdjlsh;
        private System.Windows.Forms.DataGridViewTextBoxColumn coltime;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
    }
}
