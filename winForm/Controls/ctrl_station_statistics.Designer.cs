namespace Rmes.WinForm.Controls
{
    partial class ctrl_station_statistics
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.GridPlan = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProductModel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPlanSo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColRealOnlineQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColPlanQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnCus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColPlanCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColRemark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBeginDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridPlan)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.GridPlan);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(5);
            this.groupBox1.Size = new System.Drawing.Size(604, 204);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "统计信息";
            // 
            // GridPlan
            // 
            this.GridPlan.AllowUserToAddRows = false;
            this.GridPlan.AllowUserToDeleteRows = false;
            this.GridPlan.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GridPlan.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.GridPlan.ColumnHeadersHeight = 30;
            this.GridPlan.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colProductModel,
            this.colPlanSo,
            this.ColRealOnlineQuantity,
            this.ColPlanQuantity,
            this.columnCus,
            this.ColPlanCode,
            this.ColRemark,
            this.colBeginDate});
            this.GridPlan.Location = new System.Drawing.Point(8, 24);
            this.GridPlan.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
            this.GridPlan.Name = "GridPlan";
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.GridPlan.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.GridPlan.RowTemplate.Height = 23;
            this.GridPlan.Size = new System.Drawing.Size(588, 171);
            this.GridPlan.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "PRODUCT_MODEL";
            this.dataGridViewTextBoxColumn1.HeaderText = "机型";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 150;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "PLAN_SO";
            this.dataGridViewTextBoxColumn2.HeaderText = "SO";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "COMPLETE_QTY";
            this.dataGridViewTextBoxColumn3.HeaderText = "完成数";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 70;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "PLAN_QTY";
            this.dataGridViewTextBoxColumn4.HeaderText = "计划数";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 70;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "CUSTOMER_NAME";
            this.dataGridViewTextBoxColumn5.HeaderText = "客户";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Width = 150;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "PLAN_CODE";
            this.dataGridViewTextBoxColumn6.HeaderText = "计划号";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Visible = false;
            this.dataGridViewTextBoxColumn6.Width = 130;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "REMARK";
            this.dataGridViewTextBoxColumn7.HeaderText = "备注";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.Visible = false;
            this.dataGridViewTextBoxColumn7.Width = 200;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "WORK_DATE";
            this.dataGridViewTextBoxColumn8.HeaderText = "开始日期";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.Visible = false;
            this.dataGridViewTextBoxColumn8.Width = 120;
            // 
            // colProductModel
            // 
            this.colProductModel.DataPropertyName = "PRODUCT_MODEL";
            this.colProductModel.HeaderText = "机型";
            this.colProductModel.Name = "colProductModel";
            this.colProductModel.Width = 150;
            // 
            // colPlanSo
            // 
            this.colPlanSo.DataPropertyName = "PLAN_SO";
            this.colPlanSo.HeaderText = "SO";
            this.colPlanSo.Name = "colPlanSo";
            // 
            // ColRealOnlineQuantity
            // 
            this.ColRealOnlineQuantity.DataPropertyName = "COMPLETE_QTY";
            this.ColRealOnlineQuantity.HeaderText = "完成数";
            this.ColRealOnlineQuantity.Name = "ColRealOnlineQuantity";
            this.ColRealOnlineQuantity.Width = 70;
            // 
            // ColPlanQuantity
            // 
            this.ColPlanQuantity.DataPropertyName = "PLAN_QTY";
            this.ColPlanQuantity.HeaderText = "计划数";
            this.ColPlanQuantity.Name = "ColPlanQuantity";
            this.ColPlanQuantity.Width = 70;
            // 
            // columnCus
            // 
            this.columnCus.DataPropertyName = "CUSTOMER_NAME";
            this.columnCus.HeaderText = "客户";
            this.columnCus.Name = "columnCus";
            this.columnCus.Width = 150;
            // 
            // ColPlanCode
            // 
            this.ColPlanCode.DataPropertyName = "PLAN_CODE";
            this.ColPlanCode.HeaderText = "计划号";
            this.ColPlanCode.Name = "ColPlanCode";
            this.ColPlanCode.Visible = false;
            this.ColPlanCode.Width = 130;
            // 
            // ColRemark
            // 
            this.ColRemark.DataPropertyName = "REMARK";
            this.ColRemark.HeaderText = "备注";
            this.ColRemark.Name = "ColRemark";
            this.ColRemark.Visible = false;
            this.ColRemark.Width = 200;
            // 
            // colBeginDate
            // 
            this.colBeginDate.DataPropertyName = "WORK_DATE";
            this.colBeginDate.HeaderText = "开始日期";
            this.colBeginDate.Name = "colBeginDate";
            this.colBeginDate.Visible = false;
            this.colBeginDate.Width = 120;
            // 
            // ctrl_station_statistics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
            this.Name = "ctrl_station_statistics";
            this.Size = new System.Drawing.Size(604, 204);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridPlan)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView GridPlan;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductModel;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPlanSo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColRealOnlineQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColPlanQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnCus;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColPlanCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColRemark;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBeginDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
    }
}
