using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DoAnTinHoc
{
    public partial class FormAdmin : Form
    {
        private List<NguoiDung> dsNguoiDung = new List<NguoiDung>();

        public FormAdmin()
        {
            InitializeComponent();
            Load += FormAdmin_Load;
        }

        private void FormAdmin_Load(object sender, EventArgs e)
        {
            dsNguoiDung = DocDanhSachNguoiDung();

            ThongKeGioiTinh();
            ThongKeSoThich();
            TaiBangTaiKhoan();
            ThongKeSoThichPhoBien();
        }

       
        // ĐỌC FILE NGƯỜI DÙNG
        private List<NguoiDung> DocDanhSachNguoiDung()
        {
            var result = new List<NguoiDung>();

            if (!File.Exists("TaiKhoan.txt") || !File.Exists("CauHoi.txt"))
                return result;

            var dsTK = File.ReadAllLines("TaiKhoan.txt");
            var dsCH = File.ReadAllLines("CauHoi.txt");

            foreach (var line in dsCH)
            {
                if (!line.Contains("|")) continue;

                string[] parts = line.Split('|');
                if (parts.Length < 6) continue;

                string tenTK = parts[1].Trim();

                string tkLine = dsTK.FirstOrDefault(x =>
                {
                    var t = x.Split('|');
                    return t.Length >= 2 && t[1].Trim() == tenTK;
                });

                if (tkLine == null) continue;

                var info = tkLine.Split('|');

                result.Add(new NguoiDung
                {
                    TenTK = info[1].Trim(),
                    Email = info.Length > 3 ? info[3].Trim() : "",
                    GioiTinh = info.Length > 4 ? info[4].Trim() : "",
                    DoTuoi = info.Length > 5 ? info[5].Trim() : "",
                    Anh = parts[2].Replace("Ảnh:", "").Trim(),
                    SoThich = parts[4].Replace("Sở thích:", "").Trim(),
                    TinhCach = parts[5].Replace("Tính cách:", "").Trim()
                });
            }

            return result;
        }
        // THỐNG KÊ GIỚI TÍNH
        private void ThongKeGioiTinh()
        {
            int soNam = dsNguoiDung.Count(x => x.GioiTinh.Equals("Nam", StringComparison.OrdinalIgnoreCase));
            int soNu = dsNguoiDung.Count(x => x.GioiTinh.Contains("Nữ") || x.GioiTinh.Contains("Nu"));

            lblSoNam.Text = soNam.ToString();
            lblSoNu.Text = soNu.ToString();

            chartGioiTinh.Series.Clear();
            Series s = new Series("GioiTinh")
            {
                ChartType = SeriesChartType.Pie,
                IsValueShownAsLabel = true,
                Label = "#VALX: #PERCENT{P0}"
            };

            s.Points.AddXY("Nam", soNam);
            s.Points.AddXY("Nữ", soNu);

            chartGioiTinh.Series.Add(s);
        }

        // THỐNG KÊ SỞ THÍCH
        private void ThongKeSoThich()
        {
            Dictionary<string, int> dem = new Dictionary<string, int>();

            foreach (var u in dsNguoiDung)
            {
                if (string.IsNullOrWhiteSpace(u.SoThich)) continue;

                var lst = u.SoThich.Split(new[] { ',', ';', '|' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var s in lst)
                {
                    string st = s.Trim();
                    if (!dem.ContainsKey(st)) dem[st] = 0;
                    dem[st]++;
                }
            }

            var top = dem.OrderByDescending(x => x.Value).Take(5);

            chartSoThich.Series.Clear();
            Series ser = new Series("SoThich")
            {
                ChartType = SeriesChartType.Column,
                IsValueShownAsLabel = true
            };

            foreach (var x in top)
                ser.Points.AddXY(x.Key, x.Value);

            chartSoThich.Series.Add(ser);
        }
        // TẢI DỮ LIỆU DATA GRID QUẢN LÝ TÀI 
        private void TaiBangTaiKhoan()
        {
            dgvTaiKhoan.Rows.Clear();

            foreach (var u in dsNguoiDung)
            {
                int row = dgvTaiKhoan.Rows.Add();

                dgvTaiKhoan.Rows[row].Cells["TenTk"].Value = u.TenTK;
                dgvTaiKhoan.Rows[row].Cells["Email"].Value = u.Email;
                dgvTaiKhoan.Rows[row].Cells["GioiTinh"].Value = u.GioiTinh;
                dgvTaiKhoan.Rows[row].Cells["Tuoi"].Value = u.DoTuoi.Replace("Tuoi", "");

            }
        }

        private void ThongKeSoThichPhoBien()
        {
            if (!File.Exists("CauHoi.txt"))
            {
                lblSoThich.Text = "Không có dữ liệu";
                return;
            }

            var most = File.ReadAllLines("CauHoi.txt")
                .Select(line => line.Split('|'))
                .Where(p => p.Length > 4)
                .Select(p => p[4].Replace("Sở thích:", "").Trim())
                .SelectMany(s => s.Split(','))
                .Select(s => s.Trim())
                .Where(s => s != "")
                .GroupBy(s => s)
                .OrderByDescending(g => g.Count())
                .FirstOrDefault();

            lblSoThich.Text = most != null ? most.Key : "Không có dữ liệu";
        }

        private string LayTenTaiKhoanDangChon()
        {
            if (dgvTaiKhoan.CurrentRow == null) return null;

            return dgvTaiKhoan.CurrentRow.Cells["TenTk"].Value?.ToString();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string user = LayTenTaiKhoanDangChon();
            if (string.IsNullOrEmpty(user))
            {
                MessageBox.Show("Hãy chọn tài khoản cần xóa!");
                return;
            }

            if (MessageBox.Show($"Bạn có chắc muốn xóa '{user}' ?",
                "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            var lines = File.ReadAllLines("TaiKhoan.txt").ToList();
            lines.RemoveAll(x => x.Split('|')[1].Trim() == user);
            File.WriteAllLines("TaiKhoan.txt", lines);

            MessageBox.Show("Đã xóa tài khoản!");

            dsNguoiDung = DocDanhSachNguoiDung();
            TaiBangTaiKhoan();
            ThongKeGioiTinh();
            ThongKeSoThichPhoBien();
        }

        private void btnKhoa_Click(object sender, EventArgs e)
        {
            string user = LayTenTaiKhoanDangChon();
            if (string.IsNullOrEmpty(user))
            {
                MessageBox.Show("Hãy chọn tài khoản!");
                return;
            }

            var lines = File.ReadAllLines("TaiKhoan.txt").ToList();

            for (int i = 0; i < lines.Count; i++)
            {
                var p = lines[i].Split('|');
                if (p[1].Trim() == user)
                {
                    p[2] = "123456";   // reset mật khẩu
                    lines[i] = string.Join("|", p);
                    break;
                }
            }

            File.WriteAllLines("TaiKhoan.txt", lines);

            MessageBox.Show("Đã khóa tài khoản! (Mật khẩu mới: 123456)");
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvTaiKhoan.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn tài khoản để sửa!");
                return;
            }

            var row = dgvTaiKhoan.CurrentRow;

            string oldTen = row.Cells["TenTk"].Value.ToString();
            string oldEmail = row.Cells["Email"].Value.ToString();
            string oldGT = row.Cells["GioiTinh"].Value.ToString();
            string oldTuoi = row.Cells["Tuoi"].Value.ToString(); // vd: Tuoi25

            //  NHẬP THÔNG TIN MỚI 
            string newTen = Microsoft.VisualBasic.Interaction.InputBox(
                "Tên tài khoản mới:", "Sửa", oldTen);

            if (string.IsNullOrWhiteSpace(newTen)) return;

            string newEmail = Microsoft.VisualBasic.Interaction.InputBox(
                "Email mới:", "Sửa", oldEmail);

            if (string.IsNullOrWhiteSpace(newEmail)) return;

            string newGT = Microsoft.VisualBasic.Interaction.InputBox(
                "Giới tính (Nam/Nữ):", "Sửa", oldGT);

            if (string.IsNullOrWhiteSpace(newGT)) return;

            string newTuoiRaw = Microsoft.VisualBasic.Interaction.InputBox(
                "Tuổi mới:", "Sửa", oldTuoi.Replace("Tuoi", "")); // chỉ lấy số

            if (!int.TryParse(newTuoiRaw, out int tuoiMoi))
            {
                MessageBox.Show("Tuổi phải là số!");
                return;
            }

            string newTuoi = "Tuoi" + tuoiMoi;

            //  CẬP NHẬT LÊN DGV 
            row.Cells["TenTk"].Value = newTen;
            row.Cells["Email"].Value = newEmail;
            row.Cells["GioiTinh"].Value = newGT;
            row.Cells["Tuoi"].Value = newTuoi;

            //  GHI LẠI FILE TaiKhoan.txt 
            var lines = File.ReadAllLines("TaiKhoan.txt").ToList();

            for (int i = 0; i < lines.Count; i++)
            {
                var parts = lines[i].Split('|');
                if (parts.Length < 6) continue;

                string user = parts[1].Trim();

                if (user == oldTen)
                {
                   
                    lines[i] = $"{parts[0].Trim()} | {newTen} | {parts[2].Trim()} | {newEmail} | {newGT} | {newTuoi}";
                    break;
                }
            }

            File.WriteAllLines("TaiKhoan.txt", lines);

            MessageBox.Show("Cập nhật tài khoản thành công!");
        }

        
    }
}
