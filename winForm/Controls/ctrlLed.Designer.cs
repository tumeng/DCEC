﻿namespace Rmes.WinForm.Controls
{
    partial class ctrlLed
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
            this.btn_led = new System.Windows.Forms.Button();
            this.ledView = new System.Windows.Forms.ListView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // btn_led
            // 
            this.btn_led.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_led.Location = new System.Drawing.Point(449, 0);
            this.btn_led.Margin = new System.Windows.Forms.Padding(5);
            this.btn_led.Name = "btn_led";
            this.btn_led.Size = new System.Drawing.Size(210, 45);
            this.btn_led.TabIndex = 0;
            this.btn_led.Text = "显示led信息";
            this.btn_led.UseVisualStyleBackColor = true;
            this.btn_led.Click += new System.EventHandler(this.btn_led_Click);
            // 
            // ledView
            // 
            this.ledView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ledView.Location = new System.Drawing.Point(0, 0);
            this.ledView.Margin = new System.Windows.Forms.Padding(5);
            this.ledView.Name = "ledView";
            this.ledView.Size = new System.Drawing.Size(449, 324);
            this.ledView.TabIndex = 1;
            this.ledView.UseCompatibleStateImageBehavior = false;
            this.ledView.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.ledView_ItemChecked);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ctrlLed
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ledView);
            this.Controls.Add(this.btn_led);
            this.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
            this.Name = "ctrlLed";
            this.Size = new System.Drawing.Size(658, 327);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_led;
        private System.Windows.Forms.ListView ledView;
        private System.Windows.Forms.Timer timer1;
    }
}
