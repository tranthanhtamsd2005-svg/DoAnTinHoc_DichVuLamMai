using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace DoAnTinHoc
{
    public partial class LichSuXem : Form
    {
        string user;
        List<string[]> dsView = new List<string[]>();
        List<NguoiDung> dsNguoiDung = new List<NguoiDung>();

        public LichSuXem(string tenUser)
        {
            InitializeComponent();
            user = tenUser;
        }

        private void LichSuXem_Load(object sender, EventArgs e)
        {
            dsNguoiDung = DocDanhSachNguoiDung();
            TaiLichSu();
            HienThi();
        }

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
                if (parts.Length < 6) continue; // thiếu dữ liệu câu hỏi → bỏ qua

                string tenTK = parts[1].Trim();

                // Tìm dòng tài khoản TƯƠNG ỨNG, nhưng kiểm tra length trước
                var tkLine = dsTK.FirstOrDefault(x =>
                {
                    var t = x.Split('|');
                    return t.Length >= 2 && t[1].Trim() == tenTK;
                });

                if (tkLine == null) continue;

                string[] info = tkLine.Split('|');
                if (info.Length < 6) continue; // thiếu Email / Giới tính / Tuổi → bỏ qua

                string gioiTinh = info.Length > 4 ? info[4].Trim() : "";
                string doTuoi = info.Length > 5 ? info[5].Trim() : "";

                result.Add(new NguoiDung
                {
                    TenTK = info[1].Trim(),
                    GioiTinh = gioiTinh,
                    DoTuoi = doTuoi,
                    Anh = parts[2].Replace("Ảnh:", "").Trim(),
                    SoThich = parts[4].Replace("Sở thích:", "").Trim(),
                    TinhCach = parts[5].Replace("Tính cách:", "").Trim()
                });
            }

            return result;
        }

        private void TaiLichSu()
        {
            if (!File.Exists("View.txt")) return;

            dsView = File.ReadAllLines("View.txt")
                .Select(l => l.Split('|'))
                .Where(x => x.Length == 3)
                .ToList();
        }

        private void XoaMotLichSu(string tenNguoi)
        {
            var lines = File.ReadAllLines("View.txt").ToList();

            // Xóa 1 dòng tương ứng user → người đó
            lines.RemoveAll(l =>
            {
                var p = l.Split('|');
                return p.Length == 3 && p[0] == user && p[1] == tenNguoi;
            });

            File.WriteAllLines("View.txt", lines);
            if (MessageBox.Show("Bạn có muốn xóa khỏi lịch sử?", "Thông báo",
                               MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                MessageBox.Show("Đã xóa khỏi lịch sử!", "Thông báo");
                TaiLichSu();
                HienThi();
            }
        }

        private void HienThi()
        {
            panelLS.Controls.Clear();

            var ds = dsView
                .Where(v => v[0] == user)
                .OrderByDescending(v => DateTime.Parse(v[2]))
                .ToList();

            int x = 20, y = 60;

            foreach (var v in ds)
            {
                var info = dsNguoiDung.FirstOrDefault(p => p.TenTK == v[1]);
                if (info == null) continue;

                Panel card = new Panel
                {
                    Width = 250,
                    Height = 330,
                    BackColor = Color.White,
                    BorderStyle = BorderStyle.FixedSingle,
                    Location = new Point(x, y)
                };

                // Ảnh
                PictureBox pic = new PictureBox
                {
                    Width = 230,
                    Height = 180,
                    Location = new Point(10, 10),
                    SizeMode = PictureBoxSizeMode.Zoom
                };
                // Use centralized ImageHelper to resolve relative/absolute paths and fall back to the default avatar
                try
                {
                    pic.Image = ImageHelper.LoadImageSafe(info.Anh);
                }
                catch
                {
                    // keep picture empty on error (ImageHelper already falls back to default), but avoid crash
                }

                // allow opening fullscreen viewer when clicking the picture
                pic.Cursor = Cursors.Hand;
                pic.Click += (s, e) =>
                {
                    try
                    {
                        if (pic.Image != null)
                        {
                            using (var copy = new Bitmap(pic.Image))
                            using (var viewer = new ImageViewer(copy))
                            {
                                viewer.ShowDialog();
                            }
                        }
                    }
                    catch { }
                };

                card.Controls.Add(pic);

                // Tên
                card.Controls.Add(new Label
                {
                    Text = info.TenTK,
                    Location = new Point(10, 200),
                    Font = new Font("Segoe UI", 11, FontStyle.Bold),
                    AutoSize = true
                });

                // Thời gian xem
                card.Controls.Add(new Label
                {
                    Text = "Xem lúc: " + v[2],
                    Location = new Point(10, 225),
                    ForeColor = Color.Gray,
                    AutoSize = true
                });

                // NÚT XÓA RIÊNG
                Button btnXoa = new Button
                {
                    Text = "Xóa khỏi lịch sử xem",
                    Width = 140,
                    Location = new Point(100, 260)
                };

                btnXoa.Click += (s, e) =>
                {
                    XoaMotLichSu(info.TenTK);
                };

                card.Controls.Add(btnXoa);

                // Thêm card vào panel
                panelLS.Controls.Add(card);

                // Vị trí card tiếp theo
                x += 270;
                if (x + 260 > panelLS.Width)
                {
                    x = 20;
                    y += 350;
                }
            }
        }

        private void btnXoaLSX_Click(object sender, EventArgs e)
        {
            if (!File.Exists("View.txt")) return;

            var lines = File.ReadAllLines("View.txt").ToList();

            // Xóa toàn bộ lịch sử của user
            lines.RemoveAll(l =>
            {
                var p = l.Split('|');
                return p.Length == 3 && p[0] == user;
            });

            File.WriteAllLines("View.txt", lines);

            if (MessageBox.Show("Bạn có muốn xóa toàn bộ lịch sử?", "Thông báo",
                                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                MessageBox.Show("Đã xóa toàn bộ lịch sử!", "Thông báo");
                TaiLichSu();
                HienThi();
            }
        }
    }
}
