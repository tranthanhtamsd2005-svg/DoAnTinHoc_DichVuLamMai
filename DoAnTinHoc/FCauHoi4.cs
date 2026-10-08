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
    public partial class FCauHoi4 : Form
    {
        private string tenTaiKhoan;
        private FCauHoi3 formTruoc;
        public FCauHoi4(string tenTaiKhoan, FCauHoi3 formTruoc)
        {
            InitializeComponent();
            this.tenTaiKhoan = tenTaiKhoan;
            this.formTruoc = formTruoc;
        }

        private void btnKeTiep_Click(object sender, EventArgs e)
        {
            string tinhCach = null;

            foreach (RadioButton rd in grbTinhCach.Controls.OfType<RadioButton>())
            {
                if (rd.Checked)
                {
                    tinhCach = rd.Text;
                    break;
                }
            }

            if (string.IsNullOrEmpty(tinhCach))
            {
                MessageBox.Show("Vui lòng chọn tính cách của bạn!");
                return;
            }

            XuLyCauHoi xl = new XuLyCauHoi();
            xl.LuuCauHoi(tenTaiKhoan, $"Tính cách: {tinhCach}");

            MessageBox.Show("Cảm ơn bạn! Bạn đã hoàn thành bộ câu hỏi.");

            // Mở form home
            FormHome home = new FormHome(tenTaiKhoan);
            this.Close();
            home.Show();
        }
        

        private void btnQuayLai3_Click(object sender, EventArgs e)
        {
            this.Hide();
            formTruoc.Show();
        }
    }
}
