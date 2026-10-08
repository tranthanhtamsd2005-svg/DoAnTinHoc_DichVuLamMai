namespace DoAnTinHoc
{
    partial class DanhSachYeuThich
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DanhSachYeuThich));
            this.panelDS = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // panelDS
            // 
            this.panelDS.AutoScroll = true;
            this.panelDS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDS.Location = new System.Drawing.Point(0, 0);
            this.panelDS.Name = "panelDS";
            this.panelDS.Size = new System.Drawing.Size(1158, 578);
            this.panelDS.TabIndex = 1;
            // 
            // DanhSachYeuThich
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1158, 578);
            this.Controls.Add(this.panelDS);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DanhSachYeuThich";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DanhSachYeuThich";
            this.Load += new System.EventHandler(this.DanhSachYeuThich_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelDS;
    }
}