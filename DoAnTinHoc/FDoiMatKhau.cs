using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace DoAnTinHoc
{
    public partial class FDoiMatKhau : Form
    {
        private string tenTK;

        public FDoiMatKhau(string username)
        {
            InitializeComponent();
            tenTK = username.Trim();   // Để không bị lỗi khoảng trắng
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            string mkCu = txtMKcu.Text.Trim();
            string mkMoi = txtMKmoi.Text.Trim();
            string xnMK = txtXacNhanMK.Text.Trim();

            if (mkCu == "" || mkMoi == "" || xnMK == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            if (mkMoi != xnMK)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp!");
                return;
            }

            var lines = File.ReadAllLines("TaiKhoan.txt").ToList();

            for (int i = 0; i < lines.Count; i++)
            {
                string[] p = lines[i].Split('|');

                //  CHUẨN HÓA
                for (int j = 0; j < p.Length; j++)
                    p[j] = p[j].Trim();

                //  Xác định đúng tài khoản
                if (p.Length >= 5 && p[1].Equals(tenTK, StringComparison.OrdinalIgnoreCase))
                {
                    string stt = p[0];
                    string matKhauHienTai = p[2];
                    string email = p[3];
                    string gioiTinh = p[4];

                    //  KIỂM TRA MẬT KHẨU CŨ
                    if (!matKhauHienTai.Equals(mkCu))
                    {
                        MessageBox.Show("Mật khẩu cũ không đúng!");
                        return;
                    }

                    //  CẬP NHẬT MẬT KHẨU MỚI (GIỮ ĐÚNG FORMAT)
                    lines[i] = $"{stt} | {tenTK} | {mkMoi} | {email} | {gioiTinh}";

                    File.WriteAllLines("TaiKhoan.txt", lines);

                    MessageBox.Show("Đổi mật khẩu thành công!");
                    this.Close();
                    return;
                }
            }

            MessageBox.Show("Không tìm thấy tài khoản!");
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
