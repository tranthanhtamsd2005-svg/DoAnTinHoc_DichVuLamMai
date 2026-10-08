using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnTinHoc
{
    public partial class FormHome : Form
    {
        private string tenDangNhap;
        public FormHome(string ten)
        {
            InitializeComponent();
            tenDangNhap = ten;
        }

        private void tìmToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            PhuHop f = new PhuHop(tenDangNhap);
            f.ShowDialog();
        }
        private void đăngXuấtToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn đăng xuất?", "Xác nhận đăng xuất", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Hide();
                DangNhap dangNhap = new DangNhap();
                dangNhap.Show();
                this.Close();
            }
        }
        private void thoátToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void tìmKiếmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TimKiem f = new TimKiem(tenDangNhap);
            f.ShowDialog();
        }

        private void danhSáchYêuThichToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DanhSachYeuThich f = new DanhSachYeuThich(tenDangNhap);
            f.ShowDialog();
        }

        private void lịchSửXemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LichSuXem f = new LichSuXem(tenDangNhap);
            f.ShowDialog();
        }

        private void thôngTinTàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FTrangCaNhan f = new FTrangCaNhan(tenDangNhap);
            f.ShowDialog();
        }

        private void đổiMậtKhẩuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FDoiMatKhau f = new FDoiMatKhau(tenDangNhap);
            f.ShowDialog();
        }

        
    }
}
