namespace Rmes.WinForm.Controls
{
    partial class ctrlProductRetirement
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
            this.btnRetirement = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnRetirement
            // 
            this.btnRetirement.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnRetirement.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRetirement.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRetirement.Location = new System.Drawing.Point(0, 0);
            this.btnRetirement.Margin = new System.Windows.Forms.Padding(5);
            this.btnRetirement.Name = "btnRetirement";
            this.btnRetirement.Size = new System.Drawing.Size(76, 32);
            this.btnRetirement.TabIndex = 0;
            this.btnRetirement.Text = "报废(&R)";
            this.btnRetirement.UseVisualStyleBackColor = false;
            this.btnRetirement.Click += new System.EventHandler(this.btnQuality_Click);
            // 
            // ctrlProductRetirement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.btnRetirement);
            this.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
            this.Name = "ctrlProductRetirement";
            this.Size = new System.Drawing.Size(76, 32);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRetirement;
    }
}
