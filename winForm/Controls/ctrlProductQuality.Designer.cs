namespace Rmes.WinForm.Controls
{
    partial class ctrlProductQuality
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
            this.btnQuality = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnQuality
            // 
            this.btnQuality.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnQuality.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnQuality.Location = new System.Drawing.Point(0, 0);
            this.btnQuality.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.btnQuality.Name = "btnQuality";
            this.btnQuality.Size = new System.Drawing.Size(133, 35);
            this.btnQuality.TabIndex = 0;
            this.btnQuality.Text = "合格(&G)";
            this.btnQuality.UseVisualStyleBackColor = true;
            this.btnQuality.Click += new System.EventHandler(this.btnQuality_Click);
            // 
            // ctrlProductQuality
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnQuality);
            this.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
            this.Name = "ctrlProductQuality";
            this.Size = new System.Drawing.Size(133, 35);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnQuality;
    }
}
