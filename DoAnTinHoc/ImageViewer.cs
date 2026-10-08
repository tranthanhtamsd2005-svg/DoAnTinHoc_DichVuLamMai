using System;
using System.Drawing;
using System.Windows.Forms;

namespace DoAnTinHoc
{
    public class ImageViewer : Form
    {
        private PictureBox pic;

        public ImageViewer(Image img)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.Black;
            this.TopMost = true;

            pic = new PictureBox
            {
                Dock = DockStyle.Fill,
                Image = img,
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Black,
                Cursor = Cursors.Hand
            };

            pic.Click += (s, e) => this.Close();
            this.Controls.Add(pic);

            this.KeyDown += (s, e) => { if (e.KeyCode == Keys.Escape) this.Close(); };
            this.MouseClick += (s, e) => this.Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (pic != null)
                {
                    if (pic.Image != null) pic.Image.Dispose();
                    pic.Dispose();
                }
            }
            base.Dispose(disposing);
        }
    }
}
