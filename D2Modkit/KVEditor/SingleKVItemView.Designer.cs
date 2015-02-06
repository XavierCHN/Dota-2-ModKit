namespace D2ModKit.KVEditor
{
    partial class SingleKVItemView
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
            this.debugName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // debugName
            // 
            this.debugName.AutoSize = true;
            this.debugName.Location = new System.Drawing.Point(3, 49);
            this.debugName.Name = "debugName";
            this.debugName.Size = new System.Drawing.Size(41, 12);
            this.debugName.TabIndex = 0;
            this.debugName.Text = "label1";
            // 
            // SingleKVItemView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.debugName);
            this.Name = "SingleKVItemView";
            this.Size = new System.Drawing.Size(300, 70);
            this.Load += new System.EventHandler(this.SingleKVItemView_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label debugName;

    }
}
