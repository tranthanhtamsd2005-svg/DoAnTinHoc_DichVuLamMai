namespace DoAnTinHoc
{
    partial class PhuHop
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PhuHop));
            this.panelDS = new System.Windows.Forms.Panel();
            this.btnLocTotNhat = new System.Windows.Forms.Button();
            this.lblPage = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // panelDS
            // 
            this.panelDS.AutoScroll = true;
            this.panelDS.BackColor = System.Drawing.Color.White;
            this.panelDS.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.panelDS.Location = new System.Drawing.Point(0, 85);
            this.panelDS.Name = "panelDS";
            this.panelDS.Size = new System.Drawing.Size(1045, 494);
            this.panelDS.TabIndex = 1;
            // 
            // btnLocTotNhat
            // 
            this.btnLocTotNhat.Font = new System.Drawing.Font("Segoe UI", 15F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnLocTotNhat.ForeColor = System.Drawing.Color.DodgerBlue;
            this.btnLocTotNhat.Image = ((System.Drawing.Image)(resources.GetObject("btnLocTotNhat.Image")));
            this.btnLocTotNhat.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnLocTotNhat.Location = new System.Drawing.Point(637, 7);
            this.btnLocTotNhat.Name = "btnLocTotNhat";
            this.btnLocTotNhat.Size = new System.Drawing.Size(396, 72);
            this.btnLocTotNhat.TabIndex = 0;
            this.btnLocTotNhat.Text = "Lọc những người phù hợp";
            this.btnLocTotNhat.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLocTotNhat.UseVisualStyleBackColor = true;
            this.btnLocTotNhat.Click += new System.EventHandler(this.btnLocTotNhat_Click);
            // 
            // lblPage
            // 
            this.lblPage.AutoSize = true;
            this.lblPage.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblPage.Location = new System.Drawing.Point(481, 596);
            this.lblPage.Name = "lblPage";
            this.lblPage.Size = new System.Drawing.Size(83, 23);
            this.lblPage.TabIndex = 11;
            this.lblPage.Text = "Trang 1/1";
            // 
            // btnNext
            // 
            this.btnNext.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnNext.Location = new System.Drawing.Point(579, 591);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(101, 36);
            this.btnNext.TabIndex = 9;
            this.btnNext.Text = "Sau >>";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnPrev.Location = new System.Drawing.Point(374, 591);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(101, 36);
            this.btnPrev.TabIndex = 10;
            this.btnPrev.Text = "<< Trước";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // PhuHop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1045, 681);
            this.Controls.Add(this.lblPage);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPrev);
            this.Controls.Add(this.btnLocTotNhat);
            this.Controls.Add(this.panelDS);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PhuHop";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PhuHop";
            this.Load += new System.EventHandler(this.PhuHop_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelDS;
        private System.Windows.Forms.Button btnLocTotNhat;
        private System.Windows.Forms.Label lblPage;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
    }
}