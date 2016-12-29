namespace Rmes.WinForm.Controls
{
    partial class ctrPlan
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
            this.GridPlan = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtPlanCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPModel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPlanQty = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPlanSo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPlanRemark = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
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
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColPlanSeq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColPlanCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPlanSo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProductModel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColPlanQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColRealOnlineQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOfflineQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnCus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColRemark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBeginDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEnddate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCreateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colrunflag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridPlan)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.GridPlan);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(941, 232);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "计划信息";
            // 
            // GridPlan
            // 
            this.GridPlan.AllowUserToAddRows = false;
            this.GridPlan.AllowUserToDeleteRows = false;
            this.GridPlan.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.GridPlan.ColumnHeadersHeight = 30;
            this.GridPlan.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColPlanSeq,
            this.ColPlanCode,
            this.colPlanSo,
            this.colProductModel,
            this.ColPlanQuantity,
            this.ColRealOnlineQuantity,
            this.colOfflineQty,
            this.columnCus,
            this.ColRemark,
            this.colBeginDate,
            this.colEnddate,
            this.colCreateTime,
            this.colrunflag});
            this.GridPlan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridPlan.Location = new System.Drawing.Point(3, 25);
            this.GridPlan.Margin = new System.Windows.Forms.Padding(5);
            this.GridPlan.Name = "GridPlan";
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.GridPlan.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.GridPlan.RowTemplate.Height = 23;
            this.GridPlan.Size = new System.Drawing.Size(935, 204);
            this.GridPlan.TabIndex = 0;
            this.GridPlan.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridPlan_CellClick);
            this.GridPlan.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridPlan_CellContentClick);
            this.GridPlan.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridPlan_CellDoubleClick);
            this.GridPlan.MouseLeave += new System.EventHandler(this.GridPlan_MouseLeave);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.txtPlanCode);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtPModel);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txtPlanQty);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtPlanSo);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtPlanRemark);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(922, 204);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(13, 10);
            this.panel1.TabIndex = 1;
            // 
            // txtPlanCode
            // 
            this.txtPlanCode.BackColor = System.Drawing.Color.White;
            this.txtPlanCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPlanCode.Enabled = false;
            this.txtPlanCode.Location = new System.Drawing.Point(59, 3);
            this.txtPlanCode.Name = "txtPlanCode";
            this.txtPlanCode.Size = new System.Drawing.Size(159, 29);
            this.txtPlanCode.TabIndex = 34;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 21);
            this.label2.TabIndex = 33;
            this.label2.Text = "计划号";
            // 
            // txtPModel
            // 
            this.txtPModel.BackColor = System.Drawing.Color.White;
            this.txtPModel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPModel.Enabled = false;
            this.txtPModel.Location = new System.Drawing.Point(59, 70);
            this.txtPModel.Name = "txtPModel";
            this.txtPModel.Size = new System.Drawing.Size(159, 29);
            this.txtPModel.TabIndex = 26;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 21);
            this.label6.TabIndex = 26;
            this.label6.Text = "机   型";
            // 
            // txtPlanQty
            // 
            this.txtPlanQty.BackColor = System.Drawing.Color.White;
            this.txtPlanQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPlanQty.Enabled = false;
            this.txtPlanQty.Location = new System.Drawing.Point(59, 102);
            this.txtPlanQty.Name = "txtPlanQty";
            this.txtPlanQty.Size = new System.Drawing.Size(159, 29);
            this.txtPlanQty.TabIndex = 30;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1, 105);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 21);
            this.label5.TabIndex = 23;
            this.label5.Text = "计划数";
            // 
            // txtPlanSo
            // 
            this.txtPlanSo.BackColor = System.Drawing.Color.White;
            this.txtPlanSo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPlanSo.Enabled = false;
            this.txtPlanSo.Location = new System.Drawing.Point(59, 38);
            this.txtPlanSo.Name = "txtPlanSo";
            this.txtPlanSo.Size = new System.Drawing.Size(159, 29);
            this.txtPlanSo.TabIndex = 31;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 21);
            this.label4.TabIndex = 24;
            this.label4.Text = "S     O";
            // 
            // txtPlanRemark
            // 
            this.txtPlanRemark.BackColor = System.Drawing.Color.White;
            this.txtPlanRemark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPlanRemark.Enabled = false;
            this.txtPlanRemark.Location = new System.Drawing.Point(59, 137);
            this.txtPlanRemark.Multiline = true;
            this.txtPlanRemark.Name = "txtPlanRemark";
            this.txtPlanRemark.Size = new System.Drawing.Size(159, 59);
            this.txtPlanRemark.TabIndex = 32;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1, 140);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 21);
            this.label3.TabIndex = 25;
            this.label3.Text = "备   注";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "PLAN_SEQ";
            this.dataGridViewTextBoxColumn1.HeaderText = "序";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 30;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "PLAN_CODE";
            this.dataGridViewTextBoxColumn2.HeaderText = "计划号";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 130;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "PLAN_SO";
            this.dataGridViewTextBoxColumn3.HeaderText = "SO";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "PRODUCT_MODEL";
            this.dataGridViewTextBoxColumn4.HeaderText = "机型";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 170;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "PLAN_QTY";
            this.dataGridViewTextBoxColumn5.HeaderText = "计划数";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Width = 70;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "ONLINE_QTY";
            this.dataGridViewTextBoxColumn6.HeaderText = "上线数";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Width = 70;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "CUSTOMER_NAME";
            this.dataGridViewTextBoxColumn7.HeaderText = "客户";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.Width = 150;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "REMARK";
            this.dataGridViewTextBoxColumn8.HeaderText = "备注";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.Width = 200;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.DataPropertyName = "BEGIN_DATE";
            this.dataGridViewTextBoxColumn9.HeaderText = "开始日期";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.Width = 210;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.DataPropertyName = "END_DATE";
            this.dataGridViewTextBoxColumn10.HeaderText = "结束日期";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.Width = 210;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.DataPropertyName = "CREATE_TIME";
            this.dataGridViewTextBoxColumn11.HeaderText = "创建时间";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.Width = 120;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.DataPropertyName = "RUN_FLAG";
            this.dataGridViewTextBoxColumn12.HeaderText = "RUNFLAG";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.ReadOnly = true;
            this.dataGridViewTextBoxColumn12.Visible = false;
            // 
            // ColPlanSeq
            // 
            this.ColPlanSeq.DataPropertyName = "PLAN_SEQ";
            this.ColPlanSeq.HeaderText = "序";
            this.ColPlanSeq.Name = "ColPlanSeq";
            this.ColPlanSeq.ReadOnly = true;
            this.ColPlanSeq.Width = 30;
            // 
            // ColPlanCode
            // 
            this.ColPlanCode.DataPropertyName = "PLAN_CODE";
            this.ColPlanCode.HeaderText = "计划号";
            this.ColPlanCode.Name = "ColPlanCode";
            this.ColPlanCode.ReadOnly = true;
            this.ColPlanCode.Width = 160;
            // 
            // colPlanSo
            // 
            this.colPlanSo.DataPropertyName = "PLAN_SO";
            this.colPlanSo.HeaderText = "SO";
            this.colPlanSo.Name = "colPlanSo";
            this.colPlanSo.ReadOnly = true;
            // 
            // colProductModel
            // 
            this.colProductModel.DataPropertyName = "PRODUCT_MODEL";
            this.colProductModel.HeaderText = "机型";
            this.colProductModel.Name = "colProductModel";
            this.colProductModel.ReadOnly = true;
            this.colProductModel.Width = 150;
            // 
            // ColPlanQuantity
            // 
            this.ColPlanQuantity.DataPropertyName = "PLAN_QTY";
            this.ColPlanQuantity.HeaderText = "计划数";
            this.ColPlanQuantity.Name = "ColPlanQuantity";
            this.ColPlanQuantity.ReadOnly = true;
            this.ColPlanQuantity.Width = 70;
            // 
            // ColRealOnlineQuantity
            // 
            this.ColRealOnlineQuantity.DataPropertyName = "ONLINE_QTY";
            this.ColRealOnlineQuantity.HeaderText = "上线数";
            this.ColRealOnlineQuantity.Name = "ColRealOnlineQuantity";
            this.ColRealOnlineQuantity.ReadOnly = true;
            this.ColRealOnlineQuantity.Width = 70;
            // 
            // colOfflineQty
            // 
            this.colOfflineQty.DataPropertyName = "OFFLINE_QTY";
            this.colOfflineQty.HeaderText = "下线数";
            this.colOfflineQty.Name = "colOfflineQty";
            this.colOfflineQty.ReadOnly = true;
            this.colOfflineQty.Visible = false;
            this.colOfflineQty.Width = 70;
            // 
            // columnCus
            // 
            this.columnCus.DataPropertyName = "CUSTOMER_NAME";
            this.columnCus.HeaderText = "客户";
            this.columnCus.Name = "columnCus";
            this.columnCus.ReadOnly = true;
            this.columnCus.Width = 150;
            // 
            // ColRemark
            // 
            this.ColRemark.DataPropertyName = "REMARK";
            this.ColRemark.HeaderText = "备注";
            this.ColRemark.Name = "ColRemark";
            this.ColRemark.ReadOnly = true;
            this.ColRemark.Width = 300;
            // 
            // colBeginDate
            // 
            this.colBeginDate.DataPropertyName = "BEGIN_DATE";
            this.colBeginDate.HeaderText = "开始日期";
            this.colBeginDate.Name = "colBeginDate";
            this.colBeginDate.ReadOnly = true;
            this.colBeginDate.Width = 120;
            // 
            // colEnddate
            // 
            this.colEnddate.DataPropertyName = "END_DATE";
            this.colEnddate.HeaderText = "结束日期";
            this.colEnddate.Name = "colEnddate";
            this.colEnddate.ReadOnly = true;
            this.colEnddate.Width = 120;
            // 
            // colCreateTime
            // 
            this.colCreateTime.DataPropertyName = "CREATE_TIME";
            this.colCreateTime.HeaderText = "创建时间";
            this.colCreateTime.Name = "colCreateTime";
            this.colCreateTime.ReadOnly = true;
            this.colCreateTime.Width = 210;
            // 
            // colrunflag
            // 
            this.colrunflag.DataPropertyName = "RUN_FLAG";
            this.colrunflag.HeaderText = "RUNFLAG";
            this.colrunflag.Name = "colrunflag";
            this.colrunflag.ReadOnly = true;
            this.colrunflag.Visible = false;
            // 
            // ctrPlan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
            this.Name = "ctrPlan";
            this.Size = new System.Drawing.Size(941, 232);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridPlan)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPlanQty;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPlanSo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPlanRemark;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label txtPModel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView GridPlan;
        private System.Windows.Forms.TextBox txtPlanCode;
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
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColPlanSeq;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColPlanCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPlanSo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductModel;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColPlanQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColRealOnlineQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOfflineQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnCus;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColRemark;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBeginDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEnddate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCreateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colrunflag;
    }
}
