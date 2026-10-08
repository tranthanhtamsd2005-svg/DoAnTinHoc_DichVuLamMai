using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnTinHoc
{
    public partial class QuenMatKhau : Form
    {
        public QuenMatKhau()
        {
            InitializeComponent();
            lblMatKhauMoi.Text = "";
        }
        
        private void btnLayLaiMK_Click(object sender, EventArgs e)
        {
            string email = txtEmailQMK.Text.Trim(); 

            Modify modify = new Modify();
            List<TaiKhoan> danhSach = modify.DocFile();

            TaiKhoan tk = danhSach.FirstOrDefault(t =>
                t.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

            if (tk != null) 
            {
                lblMatKhauMoi.ForeColor = Color.Blue;
                lblMatKhauMoi.Text = "Mật khẩu: " + tk.MatKhau;
            }
            else
            {
                lblMatKhauMoi.ForeColor = Color.Red;
                lblMatKhauMoi.Text = "Không tìm thấy tài khoản với email này!";
            }
        }
    }
}
