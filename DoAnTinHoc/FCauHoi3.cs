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
    public partial class FCauHoi3 : Form
    {
        private string tenTaiKhoan;
        private FCauHoi2 fCauHoi2;

        public FCauHoi3(string tenTaiKhoan, FCauHoi2 quayLai2)
        {
            InitializeComponent();
            this.tenTaiKhoan = tenTaiKhoan;
            this.fCauHoi2 = quayLai2;
        }

        private void btnKeTiep4_Click(object sender, EventArgs e)
        {

            string soThich = "";

            // Duyệt các mục đã được check trong CheckedListBox
            foreach (var item in clbCauHoi3.CheckedItems)
            {
                soThich += item.ToString() + ", ";
            }

            if (string.IsNullOrEmpty(soThich))
            {
                MessageBox.Show("Vui lòng chọn ít nhất một sở thích!");
                return;
            }

            soThich = soThich.TrimEnd(',', ' ');

            XuLyCauHoi xl = new XuLyCauHoi();
            xl.LuuCauHoi(tenTaiKhoan, $"Sở thích: {soThich}");

            FCauHoi4 f4 = new FCauHoi4(tenTaiKhoan, this);
            this.Hide();    
            f4.Show();

        }

        private void btnQuayLai2_Click(object sender, EventArgs e)
        {
            this.Hide();
            fCauHoi2.Show();
        }
    }
}
