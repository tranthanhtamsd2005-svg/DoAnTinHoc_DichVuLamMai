using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace DoAnTinHoc
{
    public partial class DanhSachYeuThich : Form
    {
        string user;
        List<string[]> dsLike = new List<string[]>();
        List<NguoiDung> dsNguoiDung = new List<NguoiDung>();

        public DanhSachYeuThich(string u)
        {
            InitializeComponent();
            user = u;
        }

        private void DanhSachYeuThich_Load(object sender, EventArgs e)
        {
            dsNguoiDung = DocDanhSachNguoiDung();
            TaiDanhSachLike();
            HienThiCard();
        }

        private void TaiDanhSachLike()
        {
            if (!File.Exists("Like.txt")) return;

            dsLike = File.ReadAllLines("Like.txt")
                .Select(l => l.Split('|'))
                .Where(x => x.Length == 2)
                .ToList();
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

        private void HienThiCard()
        {
            panelDS.Controls.Clear();

            // Đọc Like.txt
            var dsLike = File.Exists("Like.txt")
                ? File.ReadAllLines("Like.txt")
                : Array.Empty<string>();

            var toiThich = dsLike
                .Select(line => line.Split('|'))
                .Where(item => item.Length == 2 && item[0] == user)   // userLogin = tài khoản đang đăng nhập
                .Select(item => item[1])                                  // lấy tên người được thích
                .Distinct()
                .ToList();

            // Đọc danh sách người dùng
            var dsNguoiDung = DocDanhSachNguoiDung();

            int x = 20, y = 20;
            int soCardMoiHang = 3;

            foreach (var ten in toiThich)
            {
                var info = dsNguoiDung.FirstOrDefault(p => p.TenTK == ten);
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
                pic.Image = ImageHelper.LoadImageSafe(info.Anh);
                pic.Cursor = Cursors.Hand;
                pic.Click += (s, e) =>
                {
                    if (pic.Image != null)
                    {
                        var iv = new ImageViewer(new Bitmap(pic.Image));
                        iv.ShowDialog();
                        iv.Dispose();
                    }
                };
                card.Controls.Add(pic);

                // Tên
                Label lblTen = new Label
                {
                    Text = info.TenTK,
                    Font = new Font("Segoe UI", 11, FontStyle.Bold),
                    AutoSize = true,
                    Location = new Point(10, 200)
                };
                card.Controls.Add(lblTen);

                //  NÚT CHI TIẾT
                Button btnCT = new Button
                {
                    Text = "Chi tiết",
                    Width = 90,
                    Location = new Point(10, 260)
                };
                btnCT.Click += (s, e) =>
                {
                    File.AppendAllLines("LichSuXem.txt",
                        new[] { $"{user}|{info.TenTK}|{DateTime.Now}" });

                    MessageBox.Show(
                        $"Tên: {info.TenTK}\n" +
                        $"Tuổi: {info.DoTuoi}\n" +
                        $"Sở thích: {info.SoThich}\n" +
                        $"Tính cách: {info.TinhCach}"
                    );
                };
                card.Controls.Add(btnCT);

                //  NÚT BỎ THÍCH 
                Button btnBoThich = new Button
                {
                    Text = "Bỏ thích",
                    Width = 90,
                    Location = new Point(120, 260)
                };
                btnBoThich.Click += (s, e) =>
                {
                    // Xóa trong Like.txt
                    var lines = File.ReadAllLines("Like.txt").ToList();
                    lines.RemoveAll(l =>
                    {
                        var p = l.Split('|');
                        return p.Length == 2 && p[0] == user && p[1] == info.TenTK;
                    });
                    File.WriteAllLines("Like.txt", lines);

                    MessageBox.Show("Đã bỏ thích!");

                    HienThiCard();   // load lại
                };
                card.Controls.Add(btnBoThich);

                
                panelDS.Controls.Add(card);

              
                x += 270;
                if ((x - 20) / 270 >= soCardMoiHang)
                {
                    x = 20;
                    y += 350;
                }
            }
        }

    }
}
