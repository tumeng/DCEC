namespace Rmes.WinForm.Controls
{
    partial class ctrl_insite
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
            this.groupBox1.Controls.Add(this.dgvInsite);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(624, 232);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "未标定流水号";
            // 
            // dgvInsite
            // 
            this.dgvInsite.AllowUserToAddRows = false;
            this.dgvInsite.AllowUserToDeleteRows = false;
            this.dgvInsite.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInsite.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSn,
            this.colSo,
            this.colPlancode});
            this.dgvInsite.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvInsite.Location = new System.Drawing.Point(3, 25);
            this.dgvInsite.Margin = new System.Windows.Forms.Padding(5);
            this.dgvInsite.Name = "dgvInsite";
            this.dgvInsite.RowHeadersVisible = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.dgvInsite.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvInsite.RowTemplate.Height = 23;
            this.dgvInsite.Size = new System.Drawing.Size(618, 204);
            this.dgvInsite.TabIndex = 0;
            this.dgvInsite.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTj_CellClick);
            this.dgvInsite.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTj_CellContentDoubleClick);
            // 
            // colSn
            // 
            this.colSn.DataPropertyName = "GHTM";
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
            this.colPlancode.DataPropertyName = "PLAN_CODE";
            this.colPlancode.HeaderText = "计划号";
            this.colPlancode.Name = "colPlancode";
            this.colPlancode.ReadOnly = true;
            this.colPlancode.Width = 150;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 60000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ctrl_insite
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "ctrl_insite";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInsite)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvInsite;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSn;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPlancode;
    }
}
