namespace Rmes.WinForm.Controls
{
    partial class ctrl_FxFx
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
            this.btnFinishedD = new System.Windows.Forms.Button();
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
            this.btnFinishedD.Size = new System.Drawing.Size(91, 46);
            this.btnFinishedD.TabIndex = 7;
            this.btnFinishedD.Text = "返修完成";
            this.btnFinishedD.UseVisualStyleBackColor = false;
            this.btnFinishedD.Click += new System.EventHandler(this.btnFinishedD_Click);
            // 
            // ctrl_FxFx
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnFinishedD);
            this.Name = "ctrl_FxFx";
            this.Size = new System.Drawing.Size(91, 46);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnFinishedD;

    }
}
