using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnTinHoc
{
    internal static class Program
    {
        
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                var xl = new XuLyCauHoi();
                xl.ChuanHoaDuongDanAnh();
            }
            catch { }

            Application.Run(new DangNhap());
        }
    }
}
