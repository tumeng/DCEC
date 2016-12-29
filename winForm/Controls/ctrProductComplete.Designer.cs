namespace Rmes.WinForm.Controls
{
    partial class ctrProductComplete
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
            this.btnFinishedD = new System.Windows.Forms.Button();
            this.mnuComplete = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ItemQualified = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemFailed = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemPause = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemQualifiedOffline = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemFailedOffline = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemDiscard = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuComplete.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnFinishedD
            // 
            this.btnFinishedD.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnFinishedD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnFinishedD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnFinishedD.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnFinishedD.ForeColor = System.Drawing.Color.White;
            this.btnFinishedD.Location = new System.Drawing.Point(0, 0);
            this.btnFinishedD.Margin = new System.Windows.Forms.Padding(5);
            this.btnFinishedD.Name = "btnFinishedD";
            this.btnFinishedD.Size = new System.Drawing.Size(92, 45);
            this.btnFinishedD.TabIndex = 6;
            this.btnFinishedD.Text = "完工处理";
            this.btnFinishedD.UseVisualStyleBackColor = false;
            this.btnFinishedD.Click += new System.EventHandler(this.btnFinishedD_Click);
            // 
            // mnuComplete
            // 
            this.mnuComplete.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ItemQualified,
            this.ItemFailed,
            this.ItemPause,
            this.ItemQualifiedOffline,
            this.ItemFailedOffline,
            this.ItemDiscard});
            this.mnuComplete.Name = "mnuComplete";
            this.mnuComplete.Size = new System.Drawing.Size(175, 206);
            // 
            // ItemQualified
            // 
            this.ItemQualified.Enabled = false;
            this.ItemQualified.Font = new System.Drawing.Font("微软雅黑", 14.25F);
            this.ItemQualified.Name = "ItemQualified";
            this.ItemQualified.Size = new System.Drawing.Size(174, 30);
            this.ItemQualified.Text = "合格放行";
            this.ItemQualified.Click += new System.EventHandler(this.ItemQulified_Click);
            // 
            // ItemFailed
            // 
            this.ItemFailed.Enabled = false;
            this.ItemFailed.Font = new System.Drawing.Font("微软雅黑", 14.25F);
            this.ItemFailed.Name = "ItemFailed";
            this.ItemFailed.Size = new System.Drawing.Size(174, 30);
            this.ItemFailed.Text = "不合格放行";
            this.ItemFailed.Click += new System.EventHandler(this.ItemFailed_Click);
            // 
            // ItemPause
            // 
            this.ItemPause.Enabled = false;
            this.ItemPause.Font = new System.Drawing.Font("微软雅黑", 14.25F);
            this.ItemPause.Name = "ItemPause";
            this.ItemPause.Size = new System.Drawing.Size(174, 30);
            this.ItemPause.Text = "部分完成";
            this.ItemPause.Click += new System.EventHandler(this.ItemPause_Click);
            // 
            // ItemQualifiedOffline
            // 
            this.ItemQualifiedOffline.Enabled = false;
            this.ItemQualifiedOffline.Font = new System.Drawing.Font("微软雅黑", 14.25F);
            this.ItemQualifiedOffline.Name = "ItemQualifiedOffline";
            this.ItemQualifiedOffline.Size = new System.Drawing.Size(174, 30);
            this.ItemQualifiedOffline.Text = "合格下线";
            this.ItemQualifiedOffline.Click += new System.EventHandler(this.ItemQualifiedOffline_Click);
            // 
            // ItemFailedOffline
            // 
            this.ItemFailedOffline.Enabled = false;
            this.ItemFailedOffline.Font = new System.Drawing.Font("微软雅黑", 14.25F);
            this.ItemFailedOffline.Name = "ItemFailedOffline";
            this.ItemFailedOffline.Size = new System.Drawing.Size(174, 30);
            this.ItemFailedOffline.Text = "不合格下线";
            this.ItemFailedOffline.Click += new System.EventHandler(this.ItemFailedOffline_Click);
            // 
            // ItemDiscard
            // 
            this.ItemDiscard.Enabled = false;
            this.ItemDiscard.Font = new System.Drawing.Font("微软雅黑", 14.25F);
            this.ItemDiscard.Name = "ItemDiscard";
            this.ItemDiscard.Size = new System.Drawing.Size(174, 30);
            this.ItemDiscard.Text = "报废下线";
            this.ItemDiscard.Click += new System.EventHandler(this.ItemDiscard_Click);
            // 
            // ctrProductComplete
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnFinishedD);
            this.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
            this.Name = "ctrProductComplete";
            this.Size = new System.Drawing.Size(92, 45);
            this.mnuComplete.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnFinishedD;
        private System.Windows.Forms.ContextMenuStrip mnuComplete;
        private System.Windows.Forms.ToolStripMenuItem ItemQualified;
        private System.Windows.Forms.ToolStripMenuItem ItemFailed;
        private System.Windows.Forms.ToolStripMenuItem ItemDiscard;
        private System.Windows.Forms.ToolStripMenuItem ItemQualifiedOffline;
        private System.Windows.Forms.ToolStripMenuItem ItemFailedOffline;
        private System.Windows.Forms.ToolStripMenuItem ItemPause;

    }
}
