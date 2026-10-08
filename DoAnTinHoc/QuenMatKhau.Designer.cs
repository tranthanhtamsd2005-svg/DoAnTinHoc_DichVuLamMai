namespace DoAnTinHoc
{
    partial class QuenMatKhau
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QuenMatKhau));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtEmailQMK = new System.Windows.Forms.TextBox();
            this.lblEmailQMK = new System.Windows.Forms.Label();
            this.btnLayLaiMK = new System.Windows.Forms.Button();
            this.lblMatKhauMoi = new System.Windows.Forms.Label();
            this.lblKetQuaQMK = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DoAnTinHoc.Properties.Resources._0af3c9613761d2d2394d99312aeba397;
            this.pictureBox1.Location = new System.Drawing.Point(139, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(339, 184);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // txtEmailQMK
            // 
            this.txtEmailQMK.Location = new System.Drawing.Point(279, 226);
            this.txtEmailQMK.Name = "txtEmailQMK";
            this.txtEmailQMK.Size = new System.Drawing.Size(222, 22);
            this.txtEmailQMK.TabIndex = 1;
            // 
            // lblEmailQMK
            // 
            this.lblEmailQMK.AutoSize = true;
            this.lblEmailQMK.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblEmailQMK.ForeColor = System.Drawing.Color.Blue;
            this.lblEmailQMK.Location = new System.Drawing.Point(136, 224);
            this.lblEmailQMK.Name = "lblEmailQMK";
            this.lblEmailQMK.Size = new System.Drawing.Size(127, 22);
            this.lblEmailQMK.TabIndex = 4;
            this.lblEmailQMK.Text = "Email đăng ký:";
            // 
            // btnLayLaiMK
            // 
            this.btnLayLaiMK.AutoSize = true;
            this.btnLayLaiMK.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnLayLaiMK.ForeColor = System.Drawing.Color.Red;
            this.btnLayLaiMK.Location = new System.Drawing.Point(237, 345);
            this.btnLayLaiMK.Name = "btnLayLaiMK";
            this.btnLayLaiMK.Size = new System.Drawing.Size(150, 32);
            this.btnLayLaiMK.TabIndex = 2;
            this.btnLayLaiMK.Text = "Lấy lại mật khẩu";
            this.btnLayLaiMK.UseVisualStyleBackColor = true;
            this.btnLayLaiMK.Click += new System.EventHandler(this.btnLayLaiMK_Click);
            // 
            // lblMatKhauMoi
            // 
            this.lblMatKhauMoi.AutoSize = true;
            this.lblMatKhauMoi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblMatKhauMoi.ForeColor = System.Drawing.Color.Blue;
            this.lblMatKhauMoi.Location = new System.Drawing.Point(136, 279);
            this.lblMatKhauMoi.Name = "lblMatKhauMoi";
            this.lblMatKhauMoi.Size = new System.Drawing.Size(121, 22);
            this.lblMatKhauMoi.TabIndex = 4;
            this.lblMatKhauMoi.Text = "Mật khẩu mới:";
            // 
            // lblKetQuaQMK
            // 
            this.lblKetQuaQMK.AutoSize = true;
            this.lblKetQuaQMK.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblKetQuaQMK.Location = new System.Drawing.Point(275, 279);
            this.lblKetQuaQMK.Name = "lblKetQuaQMK";
            this.lblKetQuaQMK.Size = new System.Drawing.Size(0, 22);
            this.lblKetQuaQMK.TabIndex = 6;
            // 
            // QuenMatKhau
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(641, 450);
            this.Controls.Add(this.lblKetQuaQMK);
            this.Controls.Add(this.btnLayLaiMK);
            this.Controls.Add(this.lblMatKhauMoi);
            this.Controls.Add(this.lblEmailQMK);
            this.Controls.Add(this.txtEmailQMK);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "QuenMatKhau";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quên mật khẩu";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtEmailQMK;
        private System.Windows.Forms.Label lblEmailQMK;
        private System.Windows.Forms.Button btnLayLaiMK;
        private System.Windows.Forms.Label lblMatKhauMoi;
        private System.Windows.Forms.Label lblKetQuaQMK;
    }
}