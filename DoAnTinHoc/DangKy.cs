using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace DoAnTinHoc
{
    public partial class DangKy : Form
    {
        public DangKy()
        {
            InitializeComponent();
        }

        public bool CheckAccout(string tenTK)
        {
            return Regex.IsMatch(tenTK, @"^[\p{L}0-9 ]{6,24}$");
        }
        public bool CheckEmail(string em)
        {
            return Regex.IsMatch(em, @"^[a-zA-Z0-9._%+-]+@gmail\.com$");
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            string tenTaiKhoan = txtTenTKDKy.Text.Trim();
            string matKhau = txtMatKhauDKy.Text.Trim();
            string email = txtEmailDKy.Text.Trim(); 


            Modify modify = new Modify();
            List<TaiKhoan> danhSach = modify.DocFile(); 
            string tentk = txtTenTKDKy.Text;
            string matkhau = txtMatKhauDKy.Text;
            string xnmatkhau = txtNhapLaiMk.Text;
            string emaill = txtEmailDKy.Text;
            string gioiTinh= radNam.Checked ? "Nam" : "Nữ";
            if (!CheckAccout(tentk))
            {
                MessageBox.Show("Tên tài khoản không hợp lệ. Tên tài khoản phải từ 6-24 ký tự và chỉ chứa chữ cái và số.");
                return;
            }
            if (!CheckAccout(matkhau))
            {
                MessageBox.Show("Mật khẩu không hợp lệ. Mật khẩu phải từ 6-24 ký tự và chỉ chứa chữ cái và số.");
                return;
            }
            if (matkhau != xnmatkhau)
            {
                MessageBox.Show("Mật khẩu không khớp.");
                return;
            }
            if (!CheckEmail(emaill))
            {
                MessageBox.Show("Email không hợp lệ. Vui lòng nhập email đúng định dạng.");
                return;
            }
            if (danhSach.Any(tk => tk.Email.Equals(emaill, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Email đã được sử dụng!");
                return;
            }


            
            modify.GhiTaiKhoanMoi(tenTaiKhoan, matKhau, email, gioiTinh);

            if (MessageBox.Show("Bạn đã đăng ký thành công!","Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes){
                this.Close();
            }
        }
    }
}
        
    
 
