using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DoAnTinHoc
{
    public partial class DangNhap : Form
    {
        public DangNhap()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            QuenMatKhau quenMatKhau = new QuenMatKhau();
            this.Hide();
            quenMatKhau.ShowDialog();
            this.Show();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DangKy dangKy = new DangKy();
            this.Hide();
            dangKy.ShowDialog();
            this.Show();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát chương trình không?",
                "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Close();
            }
        }

        private void btnDangnhap_Click(object sender, EventArgs e)
        {
            if (txtTenMKDNhap.Text == "")
            {
                errorProvider1.SetError(txtTenMKDNhap, "Vui lòng nhập tên tài khoản hoặc email");
                return;
            }
            if (txtMatKhauDNhap.Text == "")
            {
                errorProvider1.SetError(txtMatKhauDNhap, "Vui lòng nhập mật khẩu");
                return;
            }

            Modify modify = new Modify();
            List<TaiKhoan> danhSach = modify.DocFile();

            string tenNhap = txtTenMKDNhap.Text.Trim();
            string mk = txtMatKhauDNhap.Text.Trim();

           
            // Lấy đúng USERNAME thật từ file, dù người dùng nhập username hoặc email
            var tk = danhSach.FirstOrDefault(x =>
                (x.Ten.Equals(tenNhap, StringComparison.OrdinalIgnoreCase) ||
                 x.Email.Equals(tenNhap, StringComparison.OrdinalIgnoreCase)) &&
                 x.MatKhau == mk);

            if (tk == null)
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!");
                return;
            }

            // ⭐ Username thật – đảm bảo đúng dòng khi đổi mật khẩu
            string tenTaiKhoanDung = tk.Ten;

            if (tenTaiKhoanDung.ToLower() == "admin")//quản trị
            {
                MessageBox.Show("Đăng nhập quản trị thành công!");
                this.Hide();
                FormAdmin admin = new FormAdmin();
                admin.Show();
                return;
            }

            MessageBox.Show("Đăng nhập thành công!");
            this.Hide();

            XuLyCauHoi xl = new XuLyCauHoi();

            if (!xl.DaTraLoiCauHoi(tenTaiKhoanDung))
            {
                MessageBox.Show("Vui lòng hoàn thành bộ câu hỏi đầu tiên!", "Thông báo");
                FCauHoi1 f1 = new FCauHoi1(tenTaiKhoanDung);
                f1.Show();
            }
            else
            {
                FormHome home = new FormHome(tenTaiKhoanDung);
                home.ShowDialog();
            }
        }

        private void picHide_Click(object sender, EventArgs e)
        {
            if (txtMatKhauDNhap.PasswordChar == '*')
            {
                picShow.BringToFront();
                txtMatKhauDNhap.PasswordChar = '\0';
            }
        }

        private void picShow_Click(object sender, EventArgs e)
        {
            if (txtMatKhauDNhap.PasswordChar == '\0')
            {
                picHide.BringToFront();
                txtMatKhauDNhap.PasswordChar = '*';
            }
        }
    }
}
