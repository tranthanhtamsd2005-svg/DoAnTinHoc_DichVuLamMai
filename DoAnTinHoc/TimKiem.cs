using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace DoAnTinHoc
{
    public partial class TimKiem : Form
    {
        private PhuHop phuHopForm;
        private string userLogin; // user đang đăng nhập

        //  BIẾN PHÂN TRANG 
         // Phân trang
        int soLuongMoiTrang = 20;
        int trangHienTai = 1;
        List<NguoiDung> danhSachLoc = new List<NguoiDung>();

        public TimKiem(string user)
        {
            InitializeComponent();
            userLogin = user;
        }

        // 1. ĐỌC DANH SÁCH NGƯỜI DÙNG
        public List<NguoiDung> DocDanhSachNguoiDung()
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

                var tkLine = dsTK.FirstOrDefault(x =>
                {
                    string[] t = x.Split('|');
                    return t.Length >= 2 && t[1].Trim() == tenTK;
                });

                if (tkLine == null) continue;

                string[] info = tkLine.Split('|');

                string user = info[1].Trim();
                string gioiTinh = info.Length > 4 ? info[4].Trim() : "";
                string tuoiRaw = info.Length > 5 ? info[5].Trim() : "";
                int tuoiSo = TachTuoi(tuoiRaw);

                string anh = parts[2].Replace("Ảnh:", "").Trim();
                string soThich = parts[4].Replace("Sở thích:", "").Trim();
                string tinhCach = parts[5].Replace("Tính cách:", "").Trim();

                result.Add(new NguoiDung
                {
                    TenTK = user,
                    GioiTinh = gioiTinh,
                    DoTuoi = tuoiSo > 0 ? tuoiSo.ToString() : "",
                    Anh = anh,
                    SoThich = soThich,
                    TinhCach = tinhCach
                });
            }

            return result.Where(x =>x.TenTK.Trim().ToLower() != userLogin.Trim().ToLower()).ToList();

        }

        private int TachTuoi(string tuoiRaw)
        {
            if (string.IsNullOrWhiteSpace(tuoiRaw)) return 0;

            var digits = new string(tuoiRaw.Where(char.IsDigit).ToArray());
            if (int.TryParse(digits, out int age))
                return age;
            return 0;
        }

        private Image LoadImageSafe(string anh)
        {
            if (string.IsNullOrWhiteSpace(anh))
                return new Bitmap(Properties.Resources.user_icon_150670);

            string repoRoot = FindRepoRoot();
            string[] candidates = new[] {
                anh,
                Path.Combine(Application.StartupPath, "Resources", anh),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", anh),
                repoRoot == null ? null : Path.Combine(repoRoot, anh),
                repoRoot == null ? null : Path.Combine(repoRoot, "Ảnh DATH", anh)
            };

            foreach (var c in candidates)
            {
                try
                {
                    if (File.Exists(c))
                    {
                        using (var b = new Bitmap(c))
                            return new Bitmap(b);
                    }
                }
                catch { }
            }

            try
            {
                var name = Path.GetFileNameWithoutExtension(anh).Replace('-', '_');
                var obj = Properties.Resources.ResourceManager.GetObject(name);
                if (obj is Bitmap bmpRes)
                    return new Bitmap(bmpRes);
            }
            catch { }

            return new Bitmap(Properties.Resources.user_icon_150670);
        }

        private string FindRepoRoot()
        {
            try
            {
                var dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
                for (int i = 0; i < 8 && dir != null; i++)
                {
                    var sln = dir.GetFiles("*.sln").FirstOrDefault();
                    if (sln != null) return dir.FullName;
                    dir = dir.Parent;
                }
            }
            catch { }
            return null;
        }
        // 2. GHÉP ĐÔI
        public void LuuGhepDoi(string user1, string user2)
        {
            File.AppendAllLines("GhepDoi.txt", new[] { $"{user1}|{user2}" });
        }

        public void HuyGhepDoi(string tenTK)
        {
            if (!File.Exists("GhepDoi.txt")) return;

            var lines = File.ReadAllLines("GhepDoi.txt").ToList();
            lines.RemoveAll(x =>
            {
                string[] p = x.Split('|');
                return p[0] == tenTK || p[1] == tenTK;
            });

            File.WriteAllLines("GhepDoi.txt", lines);
        }

        public string TimNguoiDaGhep(string tenTK)
        {
            if (!File.Exists("GhepDoi.txt")) return null;

            foreach (var line in File.ReadAllLines("GhepDoi.txt"))
            {
                var p = line.Split('|');
                if (p.Length == 2)
                {
                    if (p[0] == tenTK) return p[1];
                    if (p[1] == tenTK) return p[0];
                }
            }
            return null;
        }
        // 3. PHÂN TRANG
        int TongTrang()
        {
            if (danhSachLoc == null || danhSachLoc.Count == 0) return 1;
            return (int)Math.Ceiling(danhSachLoc.Count / (double)soLuongMoiTrang);
        }

        void LoadPage()
        {
            panelDS.Controls.Clear();
            panelDS.AutoScroll = true;

            if (danhSachLoc == null || danhSachLoc.Count == 0)
            {
                lblPage.Text = "Trang 0/0";
                return;
            }

            if (trangHienTai < 1) trangHienTai = 1;
            if (trangHienTai > TongTrang()) trangHienTai = TongTrang();

            int start = (trangHienTai - 1) * soLuongMoiTrang;
            int end = Math.Min(start + soLuongMoiTrang, danhSachLoc.Count);

            var pageData = danhSachLoc.GetRange(start, end - start);
            VeCard(pageData);

            lblPage.Text = $"Trang {trangHienTai}/{TongTrang()}";
        }

        public void HienThiCard(List<NguoiDung> list)
        {
            danhSachLoc = list ?? new List<NguoiDung>();
            trangHienTai = 1;
            LoadPage();
        }

        // 4. VẼ CARD 
        private void VeCard(List<NguoiDung> list)
        {
            int x = 20, y = 20;

            foreach (var u in list)
            {
                Panel card = new Panel
                {
                    Width = 250,
                    Height = 330,
                    BackColor = Color.White,
                    BorderStyle = BorderStyle.FixedSingle,
                    Location = new Point(x, y)
                };

                // ẢNH
                PictureBox pic = new PictureBox
                {
                    Width = 230,
                    Height = 180,
                    Location = new Point(10, 10),
                    SizeMode = PictureBoxSizeMode.Zoom
                };

                pic.Image = ImageHelper.LoadImageSafe(u.Anh);
                pic.Cursor = Cursors.Hand;
                pic.Click += (s, e) =>
                {
                    if (pic.Image != null)
                    {
                        using (var img = new Bitmap(pic.Image))
                        {
                            var iv = new ImageViewer(img);
                            iv.ShowDialog();
                            iv.Dispose();
                        }
                    }
                };
                card.Controls.Add(pic);

                // TÊN
                Label lblTen = new Label
                {
                    Text = u.TenTK,
                    Font = new Font("Segoe UI", 11, FontStyle.Bold),
                    Location = new Point(10, 200),
                    AutoSize = true
                };
                card.Controls.Add(lblTen);

               
                Button btnChiTiet = new Button
                {
                    Text = "Chi tiết",
                    Width = 90,
                    Location = new Point(10, 270)
                };

                btnChiTiet.Click += (s, e) =>
                {
                    File.AppendAllLines("View.txt",
                        new[] { $"{userLogin}|{u.TenTK}|{DateTime.Now}" });

                    MessageBox.Show(
                        $"Tên: {u.TenTK}\nTuổi: {u.DoTuoi}\nSở thích: {u.SoThich}\nTính cách: {u.TinhCach}"
                    );
                };

                card.Controls.Add(btnChiTiet);

                
                Button btnThich = new Button
                {
                    Text = "Thích",
                    Width = 90,
                    Location = new Point(120, 270)
                };

                btnThich.Click += (s, e) =>
                {
                    string dong = $"{userLogin}|{u.TenTK}";
                    string file = "Like.txt";

                    // Nếu file chưa có thì tạo
                    if (!File.Exists(file))
                        File.WriteAllText(file, "");

                    var lines = File.ReadAllLines(file).ToList();

                    if (lines.Contains(dong))
                    {
                        // === ĐÃ THÍCH → BỎ THÍCH ===
                        lines.Remove(dong);
                        File.WriteAllLines(file, lines);

                        btnThich.Text = "Thích";
                        btnThich.BackColor = Color.White;

                        MessageBox.Show("Đã bỏ thích!");
                    }
                    else
                    {
                        //  CHƯA THÍCH → THÍCH 
                        lines.Add(dong);
                        File.WriteAllLines(file, lines);

                        btnThich.Text = "Đã thích";
                        btnThich.BackColor = Color.LightPink;

                        MessageBox.Show("Đã thêm vào danh sách yêu thích!");
                    }
                };

                card.Controls.Add(btnThich);

                //  NÚT GHÉP ĐÔI 
                Button btnGhep = new Button
                {
                    Text = "Ghép đôi",
                    Width = 200,
                    Location = new Point(25, 300)
                };

                string daGhepCuaToi = TimNguoiDaGhep(userLogin);
                string daGhepCuaNguoiNay = TimNguoiDaGhep(u.TenTK);

                //  TÔI ĐÃ GHÉP 
                if (daGhepCuaToi != null)
                {
                    if (daGhepCuaToi == u.TenTK)
                    {
                        btnGhep.Text = "Bạn đang ghép đôi";
                        btnGhep.BackColor = Color.LightPink;

                        btnGhep.Click += (s, e) =>
                        {
                            if (MessageBox.Show("Bạn có muốn hủy ghép đôi?", "Hủy ghép đôi",
                                MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                HuyGhepDoi(userLogin);
                                MessageBox.Show("Đã hủy ghép đôi!");
                                HienThiCard(DocDanhSachNguoiDung());
                            }
                        };
                    }
                    else
                    {
                        btnGhep.Text = "Không thể ghép thêm";
                        btnGhep.Enabled = false;
                        btnGhep.BackColor = Color.LightGray;
                    }
                }
                else
                {
                    //  TÔI CHƯA GHÉP AI 
                    if (daGhepCuaNguoiNay != null)
                    {
                        btnGhep.Text = "Đang ghép";
                        btnGhep.Enabled = false;
                        btnGhep.BackColor = Color.LightGray;
                    }
                    else
                    {
                        btnGhep.Text = "Ghép đôi";

                        btnGhep.Click += (s, e) =>
                        {
                            if (MessageBox.Show("Bạn có muốn ghép đôi với người này?",
                                                "Ghép đôi", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                LuuGhepDoi(userLogin, u.TenTK);
                                MessageBox.Show("Ghép đôi thành công!");
                                HienThiCard(DocDanhSachNguoiDung());
                            }
                        };
                    }
                }

                card.Controls.Add(btnGhep);

                panelDS.Controls.Add(card);

                x += 270;

                int soCardMoiHang = 3;
                if ((x - 20) / 270 >= soCardMoiHang)
                {
                    x = 20;
                    y += 350;
                }

            }
        }
        

        private void TimKiem_Load(object sender, EventArgs e)
        {
            HienThiCard(DocDanhSachNguoiDung());
        }

         private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string hoten = txtHoTen.Text.Trim().ToLower();
            string gt = cboGioiTinh.Text.Trim().ToLower();
            string tuoi = cboTuoi.Text.Trim().ToLower();
            string tc = cboTinhCach.Text.Trim().ToLower();

            List<NguoiDung> ds = DocDanhSachNguoiDung();

            var result = ds.Where(u =>
                (string.IsNullOrEmpty(hoten) || u.TenTK.ToLower().Contains(hoten)) &&
                (string.IsNullOrEmpty(gt) || u.GioiTinh.ToLower().Contains(gt)) &&
                (string.IsNullOrEmpty(tuoi) || u.DoTuoi.ToLower().Contains(tuoi)) &&
                (string.IsNullOrEmpty(tc) || u.TinhCach.ToLower().Contains(tc))
            ).ToList();

            if (result.Count == 0)
            {
                MessageBox.Show("Không tìm thấy ai phù hợp!");
                return;
            }

            HienThiCard(result);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtHoTen.Text = "";
            cboGioiTinh.SelectedIndex = -1;
            cboTuoi.SelectedIndex = -1;
            cboTinhCach.SelectedIndex = -1;

            HienThiCard(DocDanhSachNguoiDung());

        }

        // NÚT PHÂN TRANG
        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (trangHienTai > 1)
            {
                trangHienTai--;
                LoadPage();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (trangHienTai < TongTrang())
            {
                trangHienTai++;
                LoadPage();
            }
        }

        
    }
}
