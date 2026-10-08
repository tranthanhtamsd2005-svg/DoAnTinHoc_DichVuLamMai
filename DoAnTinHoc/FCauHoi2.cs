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
    public partial class FCauHoi2 : Form
    {
        private string tenTaiKhoan;
        private FCauHoi1 fCauHoi1;
        public FCauHoi2(string tenTaiKhoan, FCauHoi1 quayLai1)
        {
            InitializeComponent();
            this.tenTaiKhoan = tenTaiKhoan;
            this.fCauHoi1 = quayLai1;
        }

        private void btnKeTiep3_Click(object sender, EventArgs e)
        {
            if (cboTuoiTu.SelectedIndex == -1 || cboTuoiDen.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn độ tuổi phù hợp!");
                return;
            }

            int tuoiTu = int.Parse(cboTuoiTu.SelectedItem.ToString());
            int tuoiDen = int.Parse(cboTuoiDen.SelectedItem.ToString());

            if (tuoiTu >= tuoiDen)
            {
                MessageBox.Show("Độ tuổi 'đến' phải lớn hơn 'từ'!");
                return;
            }

            // Lưu lựa chọn độ tuổi mong muốn
            XuLyCauHoi xl = new XuLyCauHoi();
            xl.LuuCauHoi(tenTaiKhoan, $"Độ tuổi: {tuoiTu}-{tuoiDen}");

            //  Lưu tuổi hiện tại từ cboTuoiHienTai
            if (cboTuoiHienTai.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn tuổi hiện tại!");
                return;
            }

            int tuoiHienTai = int.Parse(cboTuoiHienTai.SelectedItem.ToString());
            Modify md = new Modify();
            md.CapNhatTuoiChoTaiKhoan(tenTaiKhoan, tuoiHienTai);

            // Chuyển qua câu hỏi 3
            FCauHoi3 fCauHoi3 = new FCauHoi3(tenTaiKhoan, this);
            this.Hide();
            fCauHoi3.ShowDialog();
        }

        private void btnQuayLai1_Click(object sender, EventArgs e)
        {
            this.Hide();
            fCauHoi1.Show();
        }

    }
}
