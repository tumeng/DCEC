namespace Rmes.WinForm.Controls
{
    partial class ctrlNavigation
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuQuality = new System.Windows.Forms.ToolStripMenuItem();
            this.条码打印ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.工艺文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.装机图片ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.当天计划ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.报废处理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reload = new System.Windows.Forms.ToolStripMenuItem();
            this.本站点完成情况F3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.改制BOM对比清单ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(164, 59);
            this.button1.TabIndex = 0;
            this.button1.Text = "导航";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuQuality,
            this.条码打印ToolStripMenuItem,
            this.工艺文件ToolStripMenuItem,
            this.装机图片ToolStripMenuItem,
            this.当天计划ToolStripMenuItem,
            this.报废处理ToolStripMenuItem,
            this.reload,
            this.本站点完成情况F3ToolStripMenuItem,
            this.改制BOM对比清单ToolStripMenuItem});
            this.menu.Name = "contextMenuStrip1";
            this.menu.Size = new System.Drawing.Size(322, 296);
            // 
            // mnuQuality
            // 
            this.mnuQuality.Font = new System.Drawing.Font("微软雅黑", 14.25F);
            this.mnuQuality.Name = "mnuQuality";
            this.mnuQuality.Size = new System.Drawing.Size(321, 30);
            this.mnuQuality.Text = "质量问答F10";
            this.mnuQuality.Click += new System.EventHandler(this.mnuQuality_Click);
            // 
            // 条码打印ToolStripMenuItem
            // 
            this.条码打印ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.条码打印ToolStripMenuItem.Name = "条码打印ToolStripMenuItem";
            this.条码打印ToolStripMenuItem.Size = new System.Drawing.Size(321, 30);
            this.条码打印ToolStripMenuItem.Text = "条码打印F9";
            this.条码打印ToolStripMenuItem.Click += new System.EventHandler(this.条码打印ToolStripMenuItem_Click);
            // 
            // 工艺文件ToolStripMenuItem
            // 
            this.工艺文件ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.工艺文件ToolStripMenuItem.Name = "工艺文件ToolStripMenuItem";
            this.工艺文件ToolStripMenuItem.Size = new System.Drawing.Size(321, 30);
            this.工艺文件ToolStripMenuItem.Text = "工艺文件F8";
            this.工艺文件ToolStripMenuItem.Click += new System.EventHandler(this.工艺文件ToolStripMenuItem_Click);
            // 
            // 装机图片ToolStripMenuItem
            // 
            this.装机图片ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.装机图片ToolStripMenuItem.Name = "装机图片ToolStripMenuItem";
            this.装机图片ToolStripMenuItem.Size = new System.Drawing.Size(321, 30);
            this.装机图片ToolStripMenuItem.Text = "装机图片F7";
            this.装机图片ToolStripMenuItem.Click += new System.EventHandler(this.装机图片ToolStripMenuItem_Click);
            // 
            // 当天计划ToolStripMenuItem
            // 
            this.当天计划ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.当天计划ToolStripMenuItem.Name = "当天计划ToolStripMenuItem";
            this.当天计划ToolStripMenuItem.Size = new System.Drawing.Size(321, 30);
            this.当天计划ToolStripMenuItem.Text = "当天计划F6";
            this.当天计划ToolStripMenuItem.Click += new System.EventHandler(this.当天计划ToolStripMenuItem_Click);
            // 
            // 报废处理ToolStripMenuItem
            // 
            this.报废处理ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.报废处理ToolStripMenuItem.Name = "报废处理ToolStripMenuItem";
            this.报废处理ToolStripMenuItem.Size = new System.Drawing.Size(321, 30);
            this.报废处理ToolStripMenuItem.Text = "报废处理F2";
            this.报废处理ToolStripMenuItem.Click += new System.EventHandler(this.报废处理ToolStripMenuItem_Click);
            // 
            // reload
            // 
            this.reload.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.reload.Name = "reload";
            this.reload.Size = new System.Drawing.Size(321, 30);
            this.reload.Text = "重新登录F1";
            this.reload.Click += new System.EventHandler(this.reload_Click);
            // 
            // 本站点完成情况F3ToolStripMenuItem
            // 
            this.本站点完成情况F3ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.本站点完成情况F3ToolStripMenuItem.Name = "本站点完成情况F3ToolStripMenuItem";
            this.本站点完成情况F3ToolStripMenuItem.Size = new System.Drawing.Size(321, 30);
            this.本站点完成情况F3ToolStripMenuItem.Text = "本站点完成情况F3";
            this.本站点完成情况F3ToolStripMenuItem.Click += new System.EventHandler(this.本站点完成情况ToolStripMenuItem_Click);
            // 
            // 改制BOM对比清单ToolStripMenuItem
            // 
            this.改制BOM对比清单ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.改制BOM对比清单ToolStripMenuItem.Name = "改制BOM对比清单ToolStripMenuItem";
            this.改制BOM对比清单ToolStripMenuItem.Size = new System.Drawing.Size(321, 30);
            this.改制BOM对比清单ToolStripMenuItem.Text = "改制返修记录及差异清单F11";
            this.改制BOM对比清单ToolStripMenuItem.Click += new System.EventHandler(this.改制BOM对比清单ToolStripMenuItem_Click);
            // 
            // ctrlNavigation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.Controls.Add(this.button1);
            this.Name = "ctrlNavigation";
            this.Size = new System.Drawing.Size(164, 59);
            this.menu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ContextMenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem mnuQuality;
        private System.Windows.Forms.ToolStripMenuItem reload;
        private System.Windows.Forms.ToolStripMenuItem 条码打印ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 工艺文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 报废处理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 装机图片ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 当天计划ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 本站点完成情况F3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 改制BOM对比清单ToolStripMenuItem;
    }
}
