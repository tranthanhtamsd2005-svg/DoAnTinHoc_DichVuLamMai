using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace DoAnTinHoc
{
    public partial class FTrangCaNhan : Form
    {
        private string tenTK;

        public FTrangCaNhan(string tk)
        {
            InitializeComponent();
            tenTK = tk.Trim();
        }

        void TaiThongTin()
        {
            if (!File.Exists("TaiKhoan.txt") || !File.Exists("CauHoi.txt"))
            {
                MessageBox.Show("Không tìm thấy file dữ liệu!");
                return;
            }

            var tkData = File.ReadAllLines("TaiKhoan.txt")
                             .Select(x => x.Split('|').Select(y => y.Trim()).ToArray())
                             .FirstOrDefault(p => p.Length >= 5 &&
                               p[1].Equals(tenTK.Trim(), StringComparison.OrdinalIgnoreCase));

            if (tkData == null)
            {
                MessageBox.Show("Không tìm thấy tài khoản!");
                return;
            }

            string email = tkData[3];
            string gioiTinh = tkData[4];

            // Ảnh
            var chData = File.ReadAllLines("CauHoi.txt")
                             .Select(x => x.Split('|').Select(y => y.Trim()).ToArray())
                             .FirstOrDefault(p => p.Length >= 3 &&
                               p[1].Equals(tenTK.Trim(), StringComparison.OrdinalIgnoreCase));

            string anh = "";
            if (chData != null)
                anh = chData[2].Replace("Ảnh:", "").Trim();

            lblTen.Text = tenTK;
            lblEmail.Text = email;
            lblGioiTinh.Text = gioiTinh;

            try
            {
                picAVT.Image = ImageHelper.LoadImageSafe(anh);
            }
            catch
            {
                picAVT.Image = null;
            }

           
            picAVT.Cursor = Cursors.Hand;
            picAVT.Click -= PicAVT_Click; 
            picAVT.Click += PicAVT_Click;
        }

        private void FTrangCaNhan_Load(object sender, EventArgs e)
        {
            TaiThongTin();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            FCapNhatThongTinCaNhan f = new FCapNhatThongTinCaNhan(tenTK);

            f.ShowDialog();

            TaiThongTin();
        }

    private void PicAVT_Click(object sender, EventArgs e)
    {
        try
        {
            if (picAVT.Image != null)
            {
                using (var copy = new Bitmap(picAVT.Image))
                using (var viewer = new ImageViewer(copy))
                {
                    viewer.ShowDialog();
                }
            }
        }
        catch { }
    }
    }
}
