namespace Rmes.WinForm.Controls
{
    partial class ctrQCFaultTree
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
            this.listFault1 = new System.Windows.Forms.ListBox();
            this.listFault2 = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // listFault1
            // 
            this.listFault1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.listFault1.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listFault1.FormattingEnabled = true;
            this.listFault1.ItemHeight = 24;
            this.listFault1.Location = new System.Drawing.Point(0, 0);
            this.listFault1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.listFault1.Name = "listFault1";
            this.listFault1.Size = new System.Drawing.Size(210, 316);
            this.listFault1.TabIndex = 0;
            this.listFault1.SelectedIndexChanged += new System.EventHandler(this.listFault1_SelectedIndexChanged);
            // 
            // listFault2
            // 
            this.listFault2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listFault2.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listFault2.FormattingEnabled = true;
            this.listFault2.ItemHeight = 24;
            this.listFault2.Location = new System.Drawing.Point(220, 0);
            this.listFault2.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.listFault2.Name = "listFault2";
            this.listFault2.Size = new System.Drawing.Size(198, 316);
            this.listFault2.TabIndex = 1;
            this.listFault2.DoubleClick += new System.EventHandler(this.listFault2_DoubleClick);
            // 
            // ctrQCFaultTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listFault2);
            this.Controls.Add(this.listFault1);
            this.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
            this.Name = "ctrQCFaultTree";
            this.Size = new System.Drawing.Size(418, 319);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listFault1;
        private System.Windows.Forms.ListBox listFault2;
    }
}
