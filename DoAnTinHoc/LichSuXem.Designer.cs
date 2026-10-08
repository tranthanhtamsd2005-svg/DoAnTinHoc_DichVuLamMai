namespace DoAnTinHoc
{
    partial class LichSuXem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LichSuXem));
            this.panelLS = new System.Windows.Forms.Panel();
            this.btnXoaLSX = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // panelLS
            // 
            this.panelLS.AutoScroll = true;
            this.panelLS.Location = new System.Drawing.Point(4, 51);
            this.panelLS.Name = "panelLS";
            this.panelLS.Size = new System.Drawing.Size(894, 444);
            this.panelLS.TabIndex = 0;
            // 
            // btnXoaLSX
            // 
            this.btnXoaLSX.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnXoaLSX.Image = ((System.Drawing.Image)(resources.GetObject("btnXoaLSX.Image")));
            this.btnXoaLSX.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnXoaLSX.Location = new System.Drawing.Point(679, 3);
            this.btnXoaLSX.Name = "btnXoaLSX";
            this.btnXoaLSX.Size = new System.Drawing.Size(195, 42);
            this.btnXoaLSX.TabIndex = 1;
            this.btnXoaLSX.Text = "Xóa tất cả lịch sử xem";
            this.btnXoaLSX.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnXoaLSX.UseVisualStyleBackColor = true;
            this.btnXoaLSX.Click += new System.EventHandler(this.btnXoaLSX_Click);
            // 
            // LichSuXem
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(910, 507);
            this.Controls.Add(this.btnXoaLSX);
            this.Controls.Add(this.panelLS);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LichSuXem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LichSuXem";
            this.Load += new System.EventHandler(this.LichSuXem_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelLS;
        private System.Windows.Forms.Button btnXoaLSX;
    }
}