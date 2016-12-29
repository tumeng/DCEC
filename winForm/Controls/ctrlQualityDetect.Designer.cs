namespace Rmes.WinForm.Controls
{
    partial class ctrlQualityDetect
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.GridQuality = new System.Windows.Forms.DataGridView();
            this.dataGridViewButtonColumn1 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewComboBoxColumn1 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewComboBoxColumn2 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.colRmesID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetectCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetectDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetectValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetectStandard = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDataUp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDataDown = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetectType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetectUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetectSeq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetectFlag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRemark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCmd = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colImage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQualVal = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colFaultCode = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridQuality)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.CausesValidation = false;
            this.groupBox1.Controls.Add(this.GridQuality);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(358, 192);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "检测数据录入";
            // 
            // GridQuality
            // 
            this.GridQuality.AccessibleRole = System.Windows.Forms.AccessibleRole.SplitButton;
            this.GridQuality.AllowUserToAddRows = false;
            this.GridQuality.AllowUserToDeleteRows = false;
            this.GridQuality.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GridQuality.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.GridQuality.CausesValidation = false;
            this.GridQuality.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridQuality.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colRmesID,
            this.colDetectCode,
            this.colDetectDesc,
            this.colDetectValue,
            this.colDetectStandard,
            this.colDataUp,
            this.colDataDown,
            this.colDetectType,
            this.colDetectUnit,
            this.colDetectSeq,
            this.colDetectFlag,
            this.colRemark,
            this.colCmd,
            this.colImage,
            this.colQualVal,
            this.colFaultCode});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridQuality.DefaultCellStyle = dataGridViewCellStyle2;
            this.GridQuality.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.GridQuality.Location = new System.Drawing.Point(0, 22);
            this.GridQuality.Margin = new System.Windows.Forms.Padding(5);
            this.GridQuality.Name = "GridQuality";
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridQuality.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.GridQuality.RowTemplate.Height = 23;
            this.GridQuality.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.GridQuality.Size = new System.Drawing.Size(354, 168);
            this.GridQuality.TabIndex = 2;
            this.GridQuality.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.GridQuality_CellValidating);
            // 
            // dataGridViewButtonColumn1
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            this.dataGridViewButtonColumn1.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewButtonColumn1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dataGridViewButtonColumn1.HeaderText = "操作";
            this.dataGridViewButtonColumn1.Name = "dataGridViewButtonColumn1";
            this.dataGridViewButtonColumn1.ReadOnly = true;
            this.dataGridViewButtonColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewButtonColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewButtonColumn1.Text = "完成";
            this.dataGridViewButtonColumn1.UseColumnTextForButtonValue = true;
            this.dataGridViewButtonColumn1.Width = 50;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Process_Code";
            this.dataGridViewTextBoxColumn1.HeaderText = "工序";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Visible = false;
            this.dataGridViewTextBoxColumn1.Width = 60;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "DETECT_MAT_CODE";
            this.dataGridViewTextBoxColumn2.HeaderText = "物料代码";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "DETECT_ITEM_CODE";
            this.dataGridViewTextBoxColumn3.HeaderText = "检验项目";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewComboBoxColumn1
            // 
            this.dataGridViewComboBoxColumn1.DataPropertyName = "DETECT_QUAL_VALUE";
            this.dataGridViewComboBoxColumn1.HeaderText = "检测结果";
            this.dataGridViewComboBoxColumn1.Name = "dataGridViewComboBoxColumn1";
            this.dataGridViewComboBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewComboBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // dataGridViewComboBoxColumn2
            // 
            this.dataGridViewComboBoxColumn2.DataPropertyName = "FAULT_CODE";
            this.dataGridViewComboBoxColumn2.HeaderText = "缺陷项目";
            this.dataGridViewComboBoxColumn2.Name = "dataGridViewComboBoxColumn2";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "max_value";
            this.dataGridViewTextBoxColumn4.HeaderText = "数据上限";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "min_value";
            this.dataGridViewTextBoxColumn5.HeaderText = "数据下限";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Visible = false;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "detect_quan_value";
            this.dataGridViewTextBoxColumn6.HeaderText = "定量值";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn6.Width = 237;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "detect_qual_value";
            this.dataGridViewTextBoxColumn7.HeaderText = "定性值";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.Width = 150;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "detect_desc";
            this.dataGridViewTextBoxColumn8.HeaderText = "检测说明";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.DataPropertyName = "BATCHNO";
            this.dataGridViewTextBoxColumn9.HeaderText = "SN";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            this.dataGridViewTextBoxColumn9.Visible = false;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.DataPropertyName = "RMES_ID";
            this.dataGridViewTextBoxColumn10.HeaderText = "RMESID";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            this.dataGridViewTextBoxColumn10.Visible = false;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.DataPropertyName = "USER_ID";
            this.dataGridViewTextBoxColumn11.HeaderText = "检验员";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ReadOnly = true;
            this.dataGridViewTextBoxColumn11.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // colRmesID
            // 
            this.colRmesID.DataPropertyName = "RMES_ID";
            this.colRmesID.HeaderText = "序号";
            this.colRmesID.Name = "colRmesID";
            this.colRmesID.ReadOnly = true;
            this.colRmesID.Visible = false;
            this.colRmesID.Width = 60;
            // 
            // colDetectCode
            // 
            this.colDetectCode.DataPropertyName = "DETECT_CODE";
            this.colDetectCode.HeaderText = "质检项目";
            this.colDetectCode.Name = "colDetectCode";
            this.colDetectCode.ReadOnly = true;
            this.colDetectCode.Visible = false;
            // 
            // colDetectDesc
            // 
            this.colDetectDesc.DataPropertyName = "DETECT_NAME";
            this.colDetectDesc.HeaderText = "项目名称";
            this.colDetectDesc.Name = "colDetectDesc";
            this.colDetectDesc.ReadOnly = true;
            this.colDetectDesc.Width = 200;
            // 
            // colDetectValue
            // 
            this.colDetectValue.DataPropertyName = "DETECT_VALUE";
            this.colDetectValue.HeaderText = "检测数据";
            this.colDetectValue.Name = "colDetectValue";
            this.colDetectValue.Width = 170;
            // 
            // colDetectStandard
            // 
            this.colDetectStandard.DataPropertyName = "DETECT_STANDARD";
            this.colDetectStandard.HeaderText = "检测标准";
            this.colDetectStandard.Name = "colDetectStandard";
            this.colDetectStandard.ReadOnly = true;
            this.colDetectStandard.Visible = false;
            // 
            // colDataUp
            // 
            this.colDataUp.DataPropertyName = "DETECT_MAX";
            this.colDataUp.HeaderText = "数据上限";
            this.colDataUp.Name = "colDataUp";
            this.colDataUp.ReadOnly = true;
            this.colDataUp.Visible = false;
            // 
            // colDataDown
            // 
            this.colDataDown.DataPropertyName = "DETECT_MIN";
            this.colDataDown.HeaderText = "数据下限";
            this.colDataDown.Name = "colDataDown";
            this.colDataDown.ReadOnly = true;
            this.colDataDown.Visible = false;
            // 
            // colDetectType
            // 
            this.colDetectType.DataPropertyName = "DETECT_TYPE";
            this.colDetectType.HeaderText = "检测类型";
            this.colDetectType.Name = "colDetectType";
            this.colDetectType.ReadOnly = true;
            this.colDetectType.Visible = false;
            // 
            // colDetectUnit
            // 
            this.colDetectUnit.DataPropertyName = "DETECT_UNIT";
            this.colDetectUnit.HeaderText = "检测单位";
            this.colDetectUnit.Name = "colDetectUnit";
            this.colDetectUnit.ReadOnly = true;
            this.colDetectUnit.Visible = false;
            // 
            // colDetectSeq
            // 
            this.colDetectSeq.DataPropertyName = "DETECT_SEQ";
            this.colDetectSeq.HeaderText = "检测顺序";
            this.colDetectSeq.Name = "colDetectSeq";
            this.colDetectSeq.ReadOnly = true;
            this.colDetectSeq.Visible = false;
            // 
            // colDetectFlag
            // 
            this.colDetectFlag.DataPropertyName = "DETECT_FLAG";
            this.colDetectFlag.HeaderText = "检测标志";
            this.colDetectFlag.Name = "colDetectFlag";
            this.colDetectFlag.Visible = false;
            // 
            // colRemark
            // 
            this.colRemark.DataPropertyName = "REMARK";
            this.colRemark.HeaderText = "备注说明";
            this.colRemark.Name = "colRemark";
            this.colRemark.ReadOnly = true;
            this.colRemark.Visible = false;
            this.colRemark.Width = 150;
            // 
            // colCmd
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.colCmd.DefaultCellStyle = dataGridViewCellStyle1;
            this.colCmd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.colCmd.HeaderText = "操作";
            this.colCmd.Name = "colCmd";
            this.colCmd.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colCmd.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colCmd.Text = "确定";
            this.colCmd.UseColumnTextForButtonValue = true;
            this.colCmd.Visible = false;
            this.colCmd.Width = 80;
            // 
            // colImage
            // 
            this.colImage.HeaderText = "图像文件";
            this.colImage.Name = "colImage";
            this.colImage.Visible = false;
            // 
            // colQualVal
            // 
            this.colQualVal.HeaderText = "检测结果";
            this.colQualVal.Name = "colQualVal";
            this.colQualVal.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colQualVal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colQualVal.Visible = false;
            // 
            // colFaultCode
            // 
            this.colFaultCode.HeaderText = "不合格原因";
            this.colFaultCode.Name = "colFaultCode";
            this.colFaultCode.Visible = false;
            // 
            // ctrlQualityDetect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CausesValidation = false;
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
            this.Name = "ctrlQualityDetect";
            this.Size = new System.Drawing.Size(361, 195);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridQuality)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn1;
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
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView GridQuality;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn1;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRmesID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDetectCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDetectDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDetectValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDetectStandard;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDataUp;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDataDown;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDetectType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDetectUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDetectSeq;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDetectFlag;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRemark;
        private System.Windows.Forms.DataGridViewButtonColumn colCmd;
        private System.Windows.Forms.DataGridViewTextBoxColumn colImage;
        private System.Windows.Forms.DataGridViewComboBoxColumn colQualVal;
        private System.Windows.Forms.DataGridViewComboBoxColumn colFaultCode;


    }
}
