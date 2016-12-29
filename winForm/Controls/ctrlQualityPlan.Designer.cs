namespace Rmes.WinForm.Controls
{
    partial class ctrlQualityPlan
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.GridQuality = new System.Windows.Forms.DataGridView();
            this.dataGridViewButtonColumn1 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colComplate = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colProcesscode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colprocessbz = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colmaterialname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colItemDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.coljytxxh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.coljytxdm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.coldx = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colCurrentValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaxValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMinValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colcheckrequire = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colnumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCurrentResult = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colBatchNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRmesId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colItemCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUnitName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUnitType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOrdering = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colWorkTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUserId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridQuality)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.GridQuality);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(610, 346);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "质量录入信息";
            // 
            // GridQuality
            // 
            this.GridQuality.AccessibleRole = System.Windows.Forms.AccessibleRole.SplitButton;
            this.GridQuality.AllowUserToAddRows = false;
            this.GridQuality.AllowUserToDeleteRows = false;
            this.GridQuality.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GridQuality.ColumnHeadersHeight = 30;
            this.GridQuality.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colComplate,
            this.colProcesscode,
            this.colprocessbz,
            this.colmaterialname,
            this.colItemDescription,
            this.coljytxxh,
            this.coljytxdm,
            this.coldx,
            this.colCurrentValue,
            this.colMaxValue,
            this.colMinValue,
            this.colcheckrequire,
            this.colnumber,
            this.Remark,
            this.colCurrentResult,
            this.colBatchNo,
            this.colRmesId,
            this.colItemCode,
            this.colItemName,
            this.colUnitName,
            this.colUnitType,
            this.colOrdering,
            this.colWorkTime,
            this.colUserId});
            this.GridQuality.Location = new System.Drawing.Point(0, 25);
            this.GridQuality.Margin = new System.Windows.Forms.Padding(5);
            this.GridQuality.Name = "GridQuality";
            this.GridQuality.RowTemplate.Height = 23;
            this.GridQuality.Size = new System.Drawing.Size(602, 319);
            this.GridQuality.TabIndex = 1;
            this.GridQuality.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridQuality_CellClick);
            this.GridQuality.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridQuality_CellDoubleClick);
            // 
            // dataGridViewButtonColumn1
            // 
            this.dataGridViewButtonColumn1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.dataGridViewButtonColumn1.HeaderText = "操作";
            this.dataGridViewButtonColumn1.Name = "dataGridViewButtonColumn1";
            this.dataGridViewButtonColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewButtonColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewButtonColumn1.Text = "完成";
            this.dataGridViewButtonColumn1.UseColumnTextForButtonValue = true;
            this.dataGridViewButtonColumn1.Width = 50;
            // 
            // colComplate
            // 
            this.colComplate.HeaderText = "操作";
            this.colComplate.Name = "colComplate";
            this.colComplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colComplate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colComplate.Text = "完成";
            this.colComplate.UseColumnTextForButtonValue = true;
            this.colComplate.Width = 50;
            // 
            // colProcesscode
            // 
            this.colProcesscode.DataPropertyName = "LTXA1";
            this.colProcesscode.HeaderText = "工序";
            this.colProcesscode.Name = "colProcesscode";
            this.colProcesscode.ReadOnly = true;
            // 
            // colprocessbz
            // 
            this.colprocessbz.DataPropertyName = "VORNR";
            this.colprocessbz.HeaderText = "工序步骤";
            this.colprocessbz.Name = "colprocessbz";
            this.colprocessbz.ReadOnly = true;
            this.colprocessbz.Visible = false;
            // 
            // colmaterialname
            // 
            this.colmaterialname.DataPropertyName = "MATNR";
            this.colmaterialname.HeaderText = "检验物料";
            this.colmaterialname.Name = "colmaterialname";
            this.colmaterialname.ReadOnly = true;
            // 
            // colItemDescription
            // 
            this.colItemDescription.DataPropertyName = "KURZTEXT";
            this.colItemDescription.HeaderText = "检验项目";
            this.colItemDescription.Name = "colItemDescription";
            this.colItemDescription.ReadOnly = true;
            // 
            // coljytxxh
            // 
            this.coljytxxh.DataPropertyName = "merknr";
            this.coljytxxh.HeaderText = "检验特性序号";
            this.coljytxxh.Name = "coljytxxh";
            this.coljytxxh.ReadOnly = true;
            this.coljytxxh.Visible = false;
            // 
            // coljytxdm
            // 
            this.coljytxdm.DataPropertyName = "verwmerkm";
            this.coljytxdm.HeaderText = "检验特性代码";
            this.coljytxdm.Name = "coljytxdm";
            this.coljytxdm.ReadOnly = true;
            this.coljytxdm.Visible = false;
            // 
            // coldx
            // 
            this.coldx.HeaderText = "定性检验值";
            this.coldx.Name = "coldx";
            this.coldx.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.coldx.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // colCurrentValue
            // 
            this.colCurrentValue.DataPropertyName = "QUANTITAT";
            this.colCurrentValue.HeaderText = "定量检测值";
            this.colCurrentValue.Name = "colCurrentValue";
            this.colCurrentValue.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // colMaxValue
            // 
            this.colMaxValue.DataPropertyName = "ULIMIT";
            this.colMaxValue.HeaderText = "数据上限";
            this.colMaxValue.Name = "colMaxValue";
            this.colMaxValue.ReadOnly = true;
            this.colMaxValue.Width = 90;
            // 
            // colMinValue
            // 
            this.colMinValue.DataPropertyName = "LLIMIT";
            this.colMinValue.HeaderText = "数据下限";
            this.colMinValue.Name = "colMinValue";
            this.colMinValue.ReadOnly = true;
            this.colMinValue.Width = 90;
            // 
            // colcheckrequire
            // 
            this.colcheckrequire.DataPropertyName = "TEXT";
            this.colcheckrequire.HeaderText = "数据检验要求";
            this.colcheckrequire.Name = "colcheckrequire";
            this.colcheckrequire.ReadOnly = true;
            this.colcheckrequire.Width = 120;
            // 
            // colnumber
            // 
            this.colnumber.DataPropertyName = "SOLLSTPUMF";
            this.colnumber.HeaderText = "检验数量";
            this.colnumber.Name = "colnumber";
            this.colnumber.ReadOnly = true;
            // 
            // Remark
            // 
            this.Remark.DataPropertyName = "DESC1";
            this.Remark.HeaderText = "检验说明";
            this.Remark.Name = "Remark";
            this.Remark.ReadOnly = true;
            this.Remark.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // colCurrentResult
            // 
            this.colCurrentResult.HeaderText = "检测结果";
            this.colCurrentResult.Name = "colCurrentResult";
            this.colCurrentResult.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colCurrentResult.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colCurrentResult.Visible = false;
            // 
            // colBatchNo
            // 
            this.colBatchNo.HeaderText = "SN";
            this.colBatchNo.Name = "colBatchNo";
            this.colBatchNo.Visible = false;
            // 
            // colRmesId
            // 
            this.colRmesId.HeaderText = "RMESID";
            this.colRmesId.Name = "colRmesId";
            this.colRmesId.Visible = false;
            // 
            // colItemCode
            // 
            this.colItemCode.HeaderText = "物料代码";
            this.colItemCode.Name = "colItemCode";
            this.colItemCode.Visible = false;
            // 
            // colItemName
            // 
            this.colItemName.HeaderText = "物料名称";
            this.colItemName.Name = "colItemName";
            this.colItemName.Visible = false;
            // 
            // colUnitName
            // 
            this.colUnitName.HeaderText = "监测单位";
            this.colUnitName.Name = "colUnitName";
            this.colUnitName.Visible = false;
            // 
            // colUnitType
            // 
            this.colUnitType.HeaderText = "检测类型";
            this.colUnitType.Name = "colUnitType";
            this.colUnitType.Visible = false;
            // 
            // colOrdering
            // 
            this.colOrdering.HeaderText = "序列";
            this.colOrdering.Name = "colOrdering";
            this.colOrdering.Visible = false;
            // 
            // colWorkTime
            // 
            this.colWorkTime.HeaderText = "时间";
            this.colWorkTime.Name = "colWorkTime";
            this.colWorkTime.Visible = false;
            // 
            // colUserId
            // 
            this.colUserId.HeaderText = "操作工";
            this.colUserId.Name = "colUserId";
            this.colUserId.Visible = false;
            // 
            // ctrlQualityPlan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
            this.Name = "ctrlQualityPlan";
            this.Size = new System.Drawing.Size(613, 349);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridQuality)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView GridQuality;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn1;
        private System.Windows.Forms.DataGridViewButtonColumn colComplate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProcesscode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colprocessbz;
        private System.Windows.Forms.DataGridViewTextBoxColumn colmaterialname;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItemDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn coljytxxh;
        private System.Windows.Forms.DataGridViewTextBoxColumn coljytxdm;
        private System.Windows.Forms.DataGridViewComboBoxColumn coldx;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCurrentValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaxValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMinValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colcheckrequire;
        private System.Windows.Forms.DataGridViewTextBoxColumn colnumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn Remark;
        private System.Windows.Forms.DataGridViewComboBoxColumn colCurrentResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBatchNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRmesId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItemCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUnitName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUnitType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOrdering;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWorkTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUserId;

    }
}
