using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace DoAnTinHoc
{
    public partial class PhuHop : Form
    {
        private string taiKhoanDangNhap;

        // BIẾN PHÂN TRANG 
        int pageSize = 20;                // số người mỗi trang
        int currentPage = 1;               // trang hiện tại
        List<NguoiDung> cachedList;      // danh sách hiện đang hiển thị (đã lọc)

        public PhuHop(string tenTK)
        {
            InitializeComponent();
            taiKhoanDangNhap = tenTK;
        }
        // 1. ĐỌC DỮ LIỆU NGƯỜI DÙNG
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

                string[]    parts = line.Split('|');
                if (parts.Length < 6) continue;

                string tenTK = parts[1].Trim();

                var tk = dsTK.FirstOrDefault(x =>
                {
                    string[] t = x.Split('|');
                    return t.Length >= 2 && t[1].Trim() == tenTK;
                });

                if (tk == null) continue;

                string[] info = tk.Split('|');

                string user = info[1].Trim();
                string email = info.Length > 3 ? info[3].Trim() : "";
                string gioiTinh = info.Length > 4 ? info[4].Trim().ToLower() : "";

                string anh = parts[2].Replace("Ảnh:", "").Trim();
                string tuoi = parts[3].Replace("Độ tuổi:", "").Trim();
                string soThich = parts[4].Replace("Sở thích:", "").Trim();
                string tinhCach = parts[5].Replace("Tính cách:", "").Trim();

                result.Add(new NguoiDung
                {
                    TenTK = user,
                    Email = email,
                    GioiTinh = gioiTinh,
                    Anh = anh,
                    DoTuoi = tuoi,
                    SoThich = soThich,
                    TinhCach = tinhCach
                });
            }

            return result;
        }

        // 2. LẤY TUỔI SỐ
        private int GetAgeValue(string tuoi)
        {
            if (tuoi.Contains("-"))
            {
                string[] t = tuoi.Split('-');
                if (int.TryParse(t[0], out int x))
                    return x;
            }
            return 20;
        }

        private int TachTuoi(string tuoiRaw)
        {
            if (string.IsNullOrWhiteSpace(tuoiRaw)) return 0;

            // If the input contains a range like "20-25", return the lower bound
            if (tuoiRaw.Contains("-"))
            {
                string[] t = tuoiRaw.Split('-');
                if (int.TryParse(t[0], out int x))
                    return x;
            }

            // Try to parse a single integer value
            if (int.TryParse(tuoiRaw, out int y)) return y;

            // Fallback default
            return 0;
        }

        // 3. TÍNH ĐIỂM TƯƠNG THÍCH (PHIÊN BẢN MỚI)
        private int TinhDiemPhuHop(NguoiDung me, NguoiDung other)
        {
            int score = 0;

            // Sở thích giống / chứa từ khóa
            if (me.SoThich.Contains(other.SoThich) || other.SoThich.Contains(me.SoThich))
                score += 30;

            // Tính cách gần giống
            if (me.TinhCach.Contains(other.TinhCach) || other.TinhCach.Contains(me.TinhCach))
                score += 30;

            // Độ tuổi gần nhau
            int t1 = GetAgeValue(me.DoTuoi);
            int t2 = GetAgeValue(other.DoTuoi);
            int diff = Math.Abs(t1 - t2);

            if (diff <= 2) score += 40;
            else if (diff <= 4) score += 30;
            else if (diff <= 6) score += 20;
            else score += 10;

            return Math.Min(100, score);
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


        public void LuuGhepDoi(string user1, string user2)
        {
            File.AppendAllLines("GhepDoi.txt", new[] { $"{user1}|{user2}" });
        }

        public void HuyGhepDoi(string tenTK)
        {
            if (!File.Exists("GhepDoi.txt")) return;

            var lines = File.ReadAllLines("GhepDoi.txt").ToList();
            lines.RemoveAll(x => x.Contains(tenTK + "|") || x.EndsWith("|" + tenTK));
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

        // 4. LỌC GIỚI TÍNH
        public List<NguoiDung> LocNguoiPhuHop()
        {
            var ds = DocDanhSachNguoiDung();

            var me = ds.FirstOrDefault(x => x.TenTK.Trim().ToLower() == taiKhoanDangNhap.Trim().ToLower());
            if (me == null) return new List<NguoiDung>();

            string genderMe = me.GioiTinh.Trim().ToLower();
            string target = genderMe == "nam" ? "nữ" : "nam";

            return ds.Where(x =>
                x.TenTK.Trim().ToLower() != taiKhoanDangNhap.Trim().ToLower() &&
                x.GioiTinh.Trim().ToLower() == target).ToList();
        }

        // 5. LỌC NGƯỜI PHÙ HỢP NHẤT
        public List<NguoiDung> LocNguoiPhuHopNhat()
        {
            var ds = LocNguoiPhuHop();
            var me = DocDanhSachNguoiDung().FirstOrDefault(x => x.TenTK == taiKhoanDangNhap);

            if (me == null) return new List<NguoiDung>();

            string daGhep = TimNguoiDaGhep(taiKhoanDangNhap);

            if (daGhep != null)
            {
                var found = ds.FirstOrDefault(x => x.TenTK == daGhep);
                if (found != null) return new List<NguoiDung> { found };
                return new List<NguoiDung>();
            }

            return ds
                .Select(u => new { User = u, Score = TinhDiemPhuHop(me, u) })
                .OrderByDescending(x => x.Score)
                .Select(x => x.User)
                .ToList();
        }

        // 6. HÀM PHÂN TRANG
        int TotalPages()
        {
            if (cachedList == null || cachedList.Count == 0) return 1;
            return (int)Math.Ceiling(cachedList.Count / (double)pageSize);
        }

        void LoadPage()
        {
            panelDS.Controls.Clear();

            if (cachedList == null || cachedList.Count == 0)
            {
                lblPage.Text = "Trang 0/0";
                return;
            }

            if (currentPage < 1) currentPage = 1;
            if (currentPage > TotalPages()) currentPage = TotalPages();

            int start = (currentPage - 1) * pageSize;
            int end = Math.Min(start + pageSize, cachedList.Count);

            var pageData = cachedList.GetRange(start, end - start);

            VeCard(pageData);

            lblPage.Text = $"Trang {currentPage}/{TotalPages()}";
        }

        public void HienThiNguoi(List<NguoiDung> list)
        {
            cachedList = list ?? new List<NguoiDung>();
            currentPage = 1;
            LoadPage();
        }

        public void VeCard(List<NguoiDung> list)
        {
            int x = 20, y = 20;
            var me = DocDanhSachNguoiDung().First(xx => xx.TenTK == taiKhoanDangNhap);

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

                Label lblTen = new Label
                {
                    Text = u.TenTK,
                    Font = new Font("Segoe UI", 11, FontStyle.Bold),
                    Location = new Point(10, 200),
                    AutoSize = true
                };
                card.Controls.Add(lblTen);

                //  % Tương thích 
                int score = TinhDiemPhuHop(me, u);

                // Progress bar nền
                Panel barBg = new Panel
                {
                    Width = 230,
                    Height = 10,
                    BackColor = Color.LightGray,
                    Location = new Point(10, 225)
                };
                card.Controls.Add(barBg);

                // Thanh % màu
                Panel barValue = new Panel
                {
                    Width = Math.Min((int)(score * 2.3), 230),
                    Height = 10,
                    BackColor = score >= 70 ? Color.LimeGreen :
                score >= 40 ? Color.Orange :
                Color.Red
                };

                barBg.Controls.Add(barValue);

                Label lblScore = new Label
                {
                    Text = $"{score}%",
                    AutoSize = true,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    Location = new Point(10, 240)
                };
                card.Controls.Add(lblScore);

                //  NÚT CHI TIẾT 
                Button btnChiTiet = new Button
                {
                    Text = "Chi tiết",
                    Width = 90,
                    Location = new Point(10, 270)
                };

                btnChiTiet.Click += (s, e) =>
                {
                    File.AppendAllLines("View.txt",
                        new[] { $"{taiKhoanDangNhap}|{u.TenTK}|{DateTime.Now}" });

                    MessageBox.Show(
                        $"Tên: {u.TenTK}\nTuổi: {u.DoTuoi}\nSở thích: {u.SoThich}\nTính cách: {u.TinhCach}"
                    );
                };

                card.Controls.Add(btnChiTiet);

                //  NÚT THÍCH 
                Button btnThich = new Button
                {
                    Text = "Thích",
                    Width = 90,
                    Location = new Point(120, 270)
                };

                btnThich.Click += (s, e) =>
                {
                    string dong = $"{taiKhoanDangNhap}|{u.TenTK}";
                    string file = "Like.txt";

                    // Nếu file chưa có thì tạo
                    if (!File.Exists(file))
                        File.WriteAllText(file, "");

                    var lines = File.ReadAllLines(file).ToList();

                    if (lines.Contains(dong))
                    {
                        //  ĐÃ THÍCH → BỎ THÍCH 
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

                string daGhepCuaToi = TimNguoiDaGhep(taiKhoanDangNhap);
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
                                HuyGhepDoi(taiKhoanDangNhap);
                                MessageBox.Show("Đã hủy ghép đôi!");
                                HienThiNguoi(DocDanhSachNguoiDung());
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
                                LuuGhepDoi(taiKhoanDangNhap, u.TenTK);
                                MessageBox.Show("Ghép đôi thành công!");
                                HienThiNguoi(DocDanhSachNguoiDung());
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

        private void PhuHop_Load(object sender, EventArgs e)
        {
            var list = LocNguoiPhuHop();
            HienThiNguoi(list);
        }

        // NÚT PHÂN TRANG
        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                LoadPage();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (currentPage < TotalPages())
            {
                currentPage++;
                LoadPage();
            }
        }

        private void btnLocTotNhat_Click(object sender, EventArgs e)
        {
             var list = LocNguoiPhuHopNhat();

            if (list.Count == 0)
            {
                MessageBox.Show("Không có ai phù hợp.");
                return;
            }

            HienThiNguoi(list);
        }
    }

}
