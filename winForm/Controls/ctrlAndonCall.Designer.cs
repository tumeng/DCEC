namespace Rmes.WinForm.Controls
{
    partial class ctrlAndonCall
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
            this.btnAndon = new System.Windows.Forms.Button();
            this.ctmenuAndon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itemQuality = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.itemProcess = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.itemMat = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.ctmenuAndon.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAndon
            // 
            this.btnAndon.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAndon.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAndon.Location = new System.Drawing.Point(0, 0);
            this.btnAndon.Margin = new System.Windows.Forms.Padding(5);
            this.btnAndon.Name = "btnAndon";
            this.btnAndon.Size = new System.Drawing.Size(156, 50);
            this.btnAndon.TabIndex = 0;
            this.btnAndon.Text = "ANDON";
            this.btnAndon.UseVisualStyleBackColor = true;
            this.btnAndon.Click += new System.EventHandler(this.btnAndon_Click);
            // 
            // ctmenuAndon
            // 
            this.ctmenuAndon.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ctmenuAndon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemQuality,
            this.toolStripSeparator1,
            this.itemProcess,
            this.toolStripSeparator2,
            this.itemMat});
            this.ctmenuAndon.Name = "ctmenuAndon";
            this.ctmenuAndon.ShowImageMargin = false;
            this.ctmenuAndon.Size = new System.Drawing.Size(174, 106);
            // 
            // itemQuality
            // 
            this.itemQuality.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.itemQuality.Name = "itemQuality";
            this.itemQuality.Size = new System.Drawing.Size(173, 30);
            this.itemQuality.Text = "质量问题呼叫";
            this.itemQuality.Click += new System.EventHandler(this.itemQuality_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(170, 6);
            // 
            // itemProcess
            // 
            this.itemProcess.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.itemProcess.Name = "itemProcess";
            this.itemProcess.Size = new System.Drawing.Size(173, 30);
            this.itemProcess.Text = "装配问题呼叫";
            this.itemProcess.Click += new System.EventHandler(this.itemProcess_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(170, 6);
            // 
            // itemMat
            // 
            this.itemMat.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.itemMat.Name = "itemMat";
            this.itemMat.Size = new System.Drawing.Size(173, 30);
            this.itemMat.Text = "物料问题呼叫";
            this.itemMat.Click += new System.EventHandler(this.itemMat_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Location = new System.Drawing.Point(3, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "再次点击取消呼叫";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ctrlAndonCall
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAndon);
            this.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
            this.Name = "ctrlAndonCall";
            this.Size = new System.Drawing.Size(156, 90);
            this.ctmenuAndon.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAndon;
        private System.Windows.Forms.ContextMenuStrip ctmenuAndon;
        private System.Windows.Forms.ToolStripMenuItem itemQuality;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem itemProcess;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem itemMat;
        private System.Windows.Forms.Label label1;
    }
}
