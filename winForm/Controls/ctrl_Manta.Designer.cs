namespace Rmes.WinForm.Controls
{
    partial class ctrl_Manta
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvInsite = new System.Windows.Forms.DataGridView();
            this.check1 = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cmdPrint = new System.Windows.Forms.Button();
            this.lblYg = new System.Windows.Forms.Label();
            this.txtJx = new System.Windows.Forms.TextBox();
            this.lblJx = new System.Windows.Forms.Label();
            this.txtUnitNo = new System.Windows.Forms.TextBox();
            this.lblUnit = new System.Windows.Forms.Label();
            this.txtSo = new System.Windows.Forms.TextBox();
            this.lblSo = new System.Windows.Forms.Label();
            this.txtEcm = new System.Windows.Forms.TextBox();
            this.lblMk = new System.Windows.Forms.Label();
            this.txtLsh = new System.Windows.Forms.TextBox();
            this.lblLsh = new System.Windows.Forms.Label();
            this.cmdClear = new System.Windows.Forms.Button();
            this.cmdClear2 = new System.Windows.Forms.Button();
            this.txtPn = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtLsh2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSn = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDc = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtEc = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmdPrint2 = new System.Windows.Forms.Button();
            this.cmdAutoNp = new System.Windows.Forms.Button();
            this.lblAtuoNp = new System.Windows.Forms.Label();
            this.cmdQuery = new System.Windows.Forms.Button();
            this.cmdAuto = new System.Windows.Forms.Button();
            this.lblAuto = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPlancode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInsite)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 50000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.dgvInsite);
            this.groupBox1.ForeColor = System.Drawing.Color.Yellow;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(497, 399);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ENGLISH LSH";
            // 
            // dgvInsite
            // 
            this.dgvInsite.AllowUserToAddRows = false;
            this.dgvInsite.AllowUserToDeleteRows = false;
            this.dgvInsite.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInsite.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSn,
            this.colSo,
            this.colPlancode,
            this.colTm,
            this.colMp});
            this.dgvInsite.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvInsite.Location = new System.Drawing.Point(3, 25);
            this.dgvInsite.Margin = new System.Windows.Forms.Padding(5);
            this.dgvInsite.Name = "dgvInsite";
            this.dgvInsite.RowHeadersVisible = false;
            this.dgvInsite.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.dgvInsite.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvInsite.RowTemplate.Height = 23;
            this.dgvInsite.Size = new System.Drawing.Size(491, 371);
            this.dgvInsite.TabIndex = 0;
            this.dgvInsite.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvInsite_CellContentDoubleClick);
            // 
            // check1
            // 
            this.check1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.check1.AutoSize = true;
            this.check1.ForeColor = System.Drawing.Color.White;
            this.check1.Location = new System.Drawing.Point(6, 409);
            this.check1.Name = "check1";
            this.check1.Size = new System.Drawing.Size(220, 25);
            this.check1.TabIndex = 5;
            this.check1.Text = "Display the  unprinted sn";
            this.check1.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(322, 403);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(67, 35);
            this.button1.TabIndex = 6;
            this.button1.Text = "EXIT";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(410, 403);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(90, 35);
            this.button2.TabIndex = 7;
            this.button2.Text = "REFRESH";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.Color.Black;
            this.groupBox2.Controls.Add(this.cmdClear);
            this.groupBox2.Controls.Add(this.txtLsh);
            this.groupBox2.Controls.Add(this.lblLsh);
            this.groupBox2.Controls.Add(this.txtEcm);
            this.groupBox2.Controls.Add(this.lblMk);
            this.groupBox2.Controls.Add(this.txtSo);
            this.groupBox2.Controls.Add(this.lblSo);
            this.groupBox2.Controls.Add(this.txtUnitNo);
            this.groupBox2.Controls.Add(this.lblUnit);
            this.groupBox2.Controls.Add(this.txtJx);
            this.groupBox2.Controls.Add(this.lblJx);
            this.groupBox2.Controls.Add(this.lblYg);
            this.groupBox2.Controls.Add(this.cmdPrint);
            this.groupBox2.ForeColor = System.Drawing.Color.Yellow;
            this.groupBox2.Location = new System.Drawing.Point(506, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(248, 325);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "ECM BARCODE PRINT";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.BackColor = System.Drawing.Color.Black;
            this.groupBox3.Controls.Add(this.lblAtuoNp);
            this.groupBox3.Controls.Add(this.cmdAutoNp);
            this.groupBox3.Controls.Add(this.cmdClear2);
            this.groupBox3.Controls.Add(this.txtPn);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.txtLsh2);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.txtSn);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.txtDc);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.txtEc);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.cmdPrint2);
            this.groupBox3.ForeColor = System.Drawing.Color.Yellow;
            this.groupBox3.Location = new System.Drawing.Point(760, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(329, 372);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "ECM NAMEPLATE PRINT";
            // 
            // cmdPrint
            // 
            this.cmdPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdPrint.ForeColor = System.Drawing.Color.Black;
            this.cmdPrint.Location = new System.Drawing.Point(167, 283);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(75, 35);
            this.cmdPrint.TabIndex = 0;
            this.cmdPrint.Text = "PRINT";
            this.cmdPrint.UseVisualStyleBackColor = true;
            this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
            // 
            // lblYg
            // 
            this.lblYg.AutoSize = true;
            this.lblYg.Location = new System.Drawing.Point(6, 25);
            this.lblYg.Name = "lblYg";
            this.lblYg.Size = new System.Drawing.Size(98, 21);
            this.lblYg.TabIndex = 1;
            this.lblYg.Text = "EMPLOYEE:";
            // 
            // txtJx
            // 
            this.txtJx.Location = new System.Drawing.Point(89, 148);
            this.txtJx.Name = "txtJx";
            this.txtJx.Size = new System.Drawing.Size(153, 29);
            this.txtJx.TabIndex = 4;
            // 
            // lblJx
            // 
            this.lblJx.AutoSize = true;
            this.lblJx.Location = new System.Drawing.Point(6, 151);
            this.lblJx.Name = "lblJx";
            this.lblJx.Size = new System.Drawing.Size(65, 21);
            this.lblJx.TabIndex = 3;
            this.lblJx.Text = "JIXING:";
            // 
            // txtUnitNo
            // 
            this.txtUnitNo.Location = new System.Drawing.Point(89, 196);
            this.txtUnitNo.Name = "txtUnitNo";
            this.txtUnitNo.Size = new System.Drawing.Size(153, 29);
            this.txtUnitNo.TabIndex = 6;
            // 
            // lblUnit
            // 
            this.lblUnit.AutoSize = true;
            this.lblUnit.Location = new System.Drawing.Point(6, 199);
            this.lblUnit.Name = "lblUnit";
            this.lblUnit.Size = new System.Drawing.Size(53, 21);
            this.lblUnit.TabIndex = 5;
            this.lblUnit.Text = "UNIT:";
            // 
            // txtSo
            // 
            this.txtSo.Location = new System.Drawing.Point(89, 100);
            this.txtSo.Name = "txtSo";
            this.txtSo.Size = new System.Drawing.Size(153, 29);
            this.txtSo.TabIndex = 8;
            // 
            // lblSo
            // 
            this.lblSo.AutoSize = true;
            this.lblSo.Location = new System.Drawing.Point(6, 103);
            this.lblSo.Name = "lblSo";
            this.lblSo.Size = new System.Drawing.Size(36, 21);
            this.lblSo.TabIndex = 7;
            this.lblSo.Text = "SO:";
            // 
            // txtEcm
            // 
            this.txtEcm.Location = new System.Drawing.Point(89, 240);
            this.txtEcm.Name = "txtEcm";
            this.txtEcm.Size = new System.Drawing.Size(153, 29);
            this.txtEcm.TabIndex = 10;
            // 
            // lblMk
            // 
            this.lblMk.AutoSize = true;
            this.lblMk.Location = new System.Drawing.Point(6, 243);
            this.lblMk.Name = "lblMk";
            this.lblMk.Size = new System.Drawing.Size(86, 21);
            this.lblMk.TabIndex = 9;
            this.lblMk.Text = "MO KUAI:";
            // 
            // txtLsh
            // 
            this.txtLsh.Location = new System.Drawing.Point(89, 53);
            this.txtLsh.Name = "txtLsh";
            this.txtLsh.Size = new System.Drawing.Size(153, 29);
            this.txtLsh.TabIndex = 12;
            this.txtLsh.Click += new System.EventHandler(this.txtLsh_Click);
            this.txtLsh.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLsh_KeyPress);
            // 
            // lblLsh
            // 
            this.lblLsh.AutoSize = true;
            this.lblLsh.Location = new System.Drawing.Point(6, 56);
            this.lblLsh.Name = "lblLsh";
            this.lblLsh.Size = new System.Drawing.Size(43, 21);
            this.lblLsh.TabIndex = 11;
            this.lblLsh.Text = "LSH:";
            // 
            // cmdClear
            // 
            this.cmdClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdClear.ForeColor = System.Drawing.Color.Black;
            this.cmdClear.Location = new System.Drawing.Point(86, 283);
            this.cmdClear.Name = "cmdClear";
            this.cmdClear.Size = new System.Drawing.Size(75, 35);
            this.cmdClear.TabIndex = 13;
            this.cmdClear.Text = "CLEAR";
            this.cmdClear.UseVisualStyleBackColor = true;
            this.cmdClear.Click += new System.EventHandler(this.cmdClear_Click);
            // 
            // cmdClear2
            // 
            this.cmdClear2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdClear2.ForeColor = System.Drawing.Color.Black;
            this.cmdClear2.Location = new System.Drawing.Point(165, 283);
            this.cmdClear2.Name = "cmdClear2";
            this.cmdClear2.Size = new System.Drawing.Size(75, 35);
            this.cmdClear2.TabIndex = 25;
            this.cmdClear2.Text = "CLEAR";
            this.cmdClear2.UseVisualStyleBackColor = true;
            this.cmdClear2.Click += new System.EventHandler(this.cmdClear2_Click);
            // 
            // txtPn
            // 
            this.txtPn.Location = new System.Drawing.Point(170, 53);
            this.txtPn.Name = "txtPn";
            this.txtPn.Size = new System.Drawing.Size(153, 29);
            this.txtPn.TabIndex = 24;
            this.txtPn.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPn_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 21);
            this.label1.TabIndex = 23;
            this.label1.Text = "ECM PART(P/N):";
            // 
            // txtLsh2
            // 
            this.txtLsh2.Location = new System.Drawing.Point(170, 240);
            this.txtLsh2.Name = "txtLsh2";
            this.txtLsh2.Size = new System.Drawing.Size(153, 29);
            this.txtLsh2.TabIndex = 22;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 238);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 21);
            this.label2.TabIndex = 21;
            this.label2.Text = "ENGLISH LSH：";
            // 
            // txtSn
            // 
            this.txtSn.Location = new System.Drawing.Point(170, 100);
            this.txtSn.Name = "txtSn";
            this.txtSn.Size = new System.Drawing.Size(153, 29);
            this.txtSn.TabIndex = 20;
            this.txtSn.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSn_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 21);
            this.label3.TabIndex = 19;
            this.label3.Text = "ECM SN(S/N)：";
            // 
            // txtDc
            // 
            this.txtDc.Location = new System.Drawing.Point(170, 196);
            this.txtDc.Name = "txtDc";
            this.txtDc.Size = new System.Drawing.Size(153, 29);
            this.txtDc.TabIndex = 18;
            this.txtDc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDc_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(2, 194);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(148, 21);
            this.label4.TabIndex = 17;
            this.label4.Text = "ECM DATE(D/C)：";
            // 
            // txtEc
            // 
            this.txtEc.Location = new System.Drawing.Point(170, 148);
            this.txtEc.Name = "txtEc";
            this.txtEc.Size = new System.Drawing.Size(153, 29);
            this.txtEc.TabIndex = 16;
            this.txtEc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEc_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 146);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(149, 21);
            this.label5.TabIndex = 15;
            this.label5.Text = "ECM CODE(E/C)：";
            // 
            // cmdPrint2
            // 
            this.cmdPrint2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdPrint2.ForeColor = System.Drawing.Color.Black;
            this.cmdPrint2.Location = new System.Drawing.Point(248, 283);
            this.cmdPrint2.Name = "cmdPrint2";
            this.cmdPrint2.Size = new System.Drawing.Size(75, 35);
            this.cmdPrint2.TabIndex = 14;
            this.cmdPrint2.Text = "PRINT";
            this.cmdPrint2.UseVisualStyleBackColor = true;
            this.cmdPrint2.Click += new System.EventHandler(this.cmdPrint2_Click);
            // 
            // cmdAutoNp
            // 
            this.cmdAutoNp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAutoNp.ForeColor = System.Drawing.Color.Black;
            this.cmdAutoNp.Location = new System.Drawing.Point(6, 283);
            this.cmdAutoNp.Name = "cmdAutoNp";
            this.cmdAutoNp.Size = new System.Drawing.Size(153, 35);
            this.cmdAutoNp.TabIndex = 26;
            this.cmdAutoNp.Text = "AUTO PRINT";
            this.cmdAutoNp.UseVisualStyleBackColor = true;
            this.cmdAutoNp.Click += new System.EventHandler(this.cmdAutoNp_Click);
            // 
            // lblAtuoNp
            // 
            this.lblAtuoNp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAtuoNp.AutoSize = true;
            this.lblAtuoNp.BackColor = System.Drawing.Color.White;
            this.lblAtuoNp.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblAtuoNp.Location = new System.Drawing.Point(7, 331);
            this.lblAtuoNp.Name = "lblAtuoNp";
            this.lblAtuoNp.Size = new System.Drawing.Size(22, 21);
            this.lblAtuoNp.TabIndex = 27;
            this.lblAtuoNp.Text = "...";
            // 
            // cmdQuery
            // 
            this.cmdQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdQuery.Location = new System.Drawing.Point(543, 364);
            this.cmdQuery.Name = "cmdQuery";
            this.cmdQuery.Size = new System.Drawing.Size(161, 35);
            this.cmdQuery.TabIndex = 10;
            this.cmdQuery.Text = "QUERY PRINTED";
            this.cmdQuery.UseVisualStyleBackColor = true;
            this.cmdQuery.Click += new System.EventHandler(this.cmdQuery_Click);
            // 
            // cmdAuto
            // 
            this.cmdAuto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAuto.Location = new System.Drawing.Point(543, 403);
            this.cmdAuto.Name = "cmdAuto";
            this.cmdAuto.Size = new System.Drawing.Size(157, 35);
            this.cmdAuto.TabIndex = 27;
            this.cmdAuto.Text = "AUTO PRINT";
            this.cmdAuto.UseVisualStyleBackColor = true;
            this.cmdAuto.Click += new System.EventHandler(this.cmdAuto_Click);
            // 
            // lblAuto
            // 
            this.lblAuto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAuto.AutoSize = true;
            this.lblAuto.BackColor = System.Drawing.Color.White;
            this.lblAuto.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblAuto.Location = new System.Drawing.Point(706, 416);
            this.lblAuto.Name = "lblAuto";
            this.lblAuto.Size = new System.Drawing.Size(22, 21);
            this.lblAuto.TabIndex = 28;
            this.lblAuto.Text = "...";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Yellow;
            this.label6.Location = new System.Drawing.Point(967, 410);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(122, 21);
            this.label6.TabIndex = 29;
            this.label6.Text = "发动机序列号：";
            // 
            // timer2
            // 
            this.timer2.Interval = 6000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // timer3
            // 
            this.timer3.Interval = 10000;
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "SN";
            this.dataGridViewTextBoxColumn1.HeaderText = "流水号";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "PLAN_SO";
            this.dataGridViewTextBoxColumn2.HeaderText = "SO";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "WORK_TIME";
            this.dataGridViewTextBoxColumn3.HeaderText = "时间";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 150;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "TM";
            this.dataGridViewTextBoxColumn4.HeaderText = "条码";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "MP";
            this.dataGridViewTextBoxColumn5.HeaderText = "铭牌";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
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
            // 
            // colPlancode
            // 
            this.colPlancode.DataPropertyName = "WORK_TIME";
            this.colPlancode.HeaderText = "时间";
            this.colPlancode.Name = "colPlancode";
            this.colPlancode.ReadOnly = true;
            this.colPlancode.Width = 150;
            // 
            // colTm
            // 
            this.colTm.DataPropertyName = "TM";
            this.colTm.HeaderText = "条码";
            this.colTm.Name = "colTm";
            this.colTm.ReadOnly = true;
            // 
            // colMp
            // 
            this.colMp.DataPropertyName = "MP";
            this.colMp.HeaderText = "铭牌";
            this.colMp.Name = "colMp";
            this.colMp.ReadOnly = true;
            // 
            // ctrl_Manta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblAuto);
            this.Controls.Add(this.cmdAuto);
            this.Controls.Add(this.cmdQuery);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.check1);
            this.Controls.Add(this.groupBox1);
            this.Name = "ctrl_Manta";
            this.Size = new System.Drawing.Size(1095, 447);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInsite)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvInsite;
        private System.Windows.Forms.CheckBox check1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button cmdClear;
        private System.Windows.Forms.TextBox txtLsh;
        private System.Windows.Forms.Label lblLsh;
        private System.Windows.Forms.TextBox txtEcm;
        private System.Windows.Forms.Label lblMk;
        private System.Windows.Forms.TextBox txtSo;
        private System.Windows.Forms.Label lblSo;
        private System.Windows.Forms.TextBox txtUnitNo;
        private System.Windows.Forms.Label lblUnit;
        private System.Windows.Forms.TextBox txtJx;
        private System.Windows.Forms.Label lblJx;
        private System.Windows.Forms.Label lblYg;
        private System.Windows.Forms.Button cmdPrint;
        private System.Windows.Forms.Button cmdClear2;
        private System.Windows.Forms.TextBox txtPn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLsh2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDc;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtEc;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button cmdPrint2;
        private System.Windows.Forms.Label lblAtuoNp;
        private System.Windows.Forms.Button cmdAutoNp;
        private System.Windows.Forms.Button cmdQuery;
        private System.Windows.Forms.Button cmdAuto;
        private System.Windows.Forms.Label lblAuto;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Timer timer3;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSn;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPlancode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTm;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMp;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
    }
}
