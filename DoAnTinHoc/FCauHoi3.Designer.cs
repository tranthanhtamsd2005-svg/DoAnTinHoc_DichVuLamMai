namespace DoAnTinHoc
{
    partial class FCauHoi3
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FCauHoi3));
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.clbCauHoi3 = new System.Windows.Forms.CheckedListBox();
            this.btnQuayLai2 = new System.Windows.Forms.Button();
            this.btnKeTiep4 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label4.Location = new System.Drawing.Point(290, 9);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 54);
            this.label4.TabIndex = 8;
            this.label4.Text = "3";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(83, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(451, 41);
            this.label1.TabIndex = 9;
            this.label1.Text = "Sở thích và hoạt động yêu thích?";
            // 
            // clbCauHoi3
            // 
            this.clbCauHoi3.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.clbCauHoi3.ForeColor = System.Drawing.Color.Black;
            this.clbCauHoi3.FormattingEnabled = true;
            this.clbCauHoi3.Items.AddRange(new object[] {
            "🎬 Xem phim",
            "🎵 Nghe nhạc",
            "☕ Đi cà phê",
            "🏃‍♀️ Thể thao",
            "✈️ Du lịch",
            "📚 Đọc sách",
            "🌄 Leo núi",
            "🐾 Yêu thú cưng",
            "🍜 Ăn uống",
            "🎮 Chơi game",
            "🧘‍♂️ Yoga",
            "📷 Chụp ảnh"});
            this.clbCauHoi3.Location = new System.Drawing.Point(182, 122);
            this.clbCauHoi3.Name = "clbCauHoi3";
            this.clbCauHoi3.Size = new System.Drawing.Size(245, 202);
            this.clbCauHoi3.TabIndex = 10;
            // 
            // btnQuayLai2
            // 
            this.btnQuayLai2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnQuayLai2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnQuayLai2.Image = ((System.Drawing.Image)(resources.GetObject("btnQuayLai2.Image")));
            this.btnQuayLai2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnQuayLai2.Location = new System.Drawing.Point(138, 346);
            this.btnQuayLai2.Name = "btnQuayLai2";
            this.btnQuayLai2.Size = new System.Drawing.Size(135, 49);
            this.btnQuayLai2.TabIndex = 11;
            this.btnQuayLai2.Text = "Quay lại";
            this.btnQuayLai2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnQuayLai2.UseVisualStyleBackColor = true;
            this.btnQuayLai2.Click += new System.EventHandler(this.btnQuayLai2_Click);
            // 
            // btnKeTiep4
            // 
            this.btnKeTiep4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnKeTiep4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnKeTiep4.Image = ((System.Drawing.Image)(resources.GetObject("btnKeTiep4.Image")));
            this.btnKeTiep4.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnKeTiep4.Location = new System.Drawing.Point(331, 346);
            this.btnKeTiep4.Name = "btnKeTiep4";
            this.btnKeTiep4.Size = new System.Drawing.Size(119, 49);
            this.btnKeTiep4.TabIndex = 12;
            this.btnKeTiep4.Text = "Kế tiếp";
            this.btnKeTiep4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnKeTiep4.UseVisualStyleBackColor = true;
            this.btnKeTiep4.Click += new System.EventHandler(this.btnKeTiep4_Click);
            // 
            // FCauHoi3
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(577, 430);
            this.Controls.Add(this.btnQuayLai2);
            this.Controls.Add(this.btnKeTiep4);
            this.Controls.Add(this.clbCauHoi3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FCauHoi3";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FCauHoi3";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox clbCauHoi3;
        private System.Windows.Forms.Button btnQuayLai2;
        private System.Windows.Forms.Button btnKeTiep4;
    }
}