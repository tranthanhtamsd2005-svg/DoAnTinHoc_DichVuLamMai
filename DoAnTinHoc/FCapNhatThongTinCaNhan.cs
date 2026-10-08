using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace DoAnTinHoc
{
    public partial class FCapNhatThongTinCaNhan : Form
    {
        private string taiKhoanDangNhap;
        private string duongDanAnh = "";

        public FCapNhatThongTinCaNhan(string tenTK)
        {
            InitializeComponent();
            taiKhoanDangNhap = tenTK.Trim();
        }

        private void picThem_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";

            if (open.ShowDialog() == DialogResult.OK)
            {
                duongDanAnh = open.FileName;
                picAnh.Image = Image.FromFile(open.FileName);
                picAnh.SizeMode = PictureBoxSizeMode.Zoom;
                picThem.Visible = false;
            }
        }

        void CapNhatAnhTrongCauHoi(string tenTK, string duongDan)
        {
            if (!File.Exists("CauHoi.txt"))
                return;

            var lines = File.ReadAllLines("CauHoi.txt").ToList();

            for (int i = 0; i < lines.Count; i++)
            {
                string[] p = lines[i].Split('|').Select(x => x.Trim()).ToArray();
                if (p.Length < 3) continue;

                if (p[1].Equals(tenTK.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    string finalPath = duongDan;
                    try
                    {
                        if (!string.IsNullOrWhiteSpace(duongDan) && File.Exists(duongDan))
                        {
                            string repoRoot = null;
                            try
                            {
                                var dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
                                for (int k = 0; k < 8 && dir != null; k++)
                                {
                                    var sln = dir.GetFiles("*.sln").FirstOrDefault();
                                    if (sln != null) { repoRoot = dir.FullName; break; }
                                    dir = dir.Parent;
                                }
                            }
                            catch { }
                            if (repoRoot == null) repoRoot = AppDomain.CurrentDomain.BaseDirectory;

                            string genderFolder = "AnhNam";
                            try
                            {
                                if (File.Exists("TaiKhoan.txt"))
                                {
                                    var linesTk = File.ReadAllLines("TaiKhoan.txt");
                                    foreach (var ln in linesTk)
                                    {
                                        var q = ln.Split('|').Select(x => x.Trim()).ToArray();
                                        if (q.Length >= 5 && q[1].Equals(tenTK, StringComparison.OrdinalIgnoreCase))
                                        {
                                            var gt = q[4].ToLower();
                                            genderFolder = (gt == "nữ" || gt == "nu") ? "AnhNu" : "AnhNam";
                                            break;
                                        }
                                    }
                                }
                            }
                            catch { }

                            string anhDathFolder = Path.Combine(repoRoot, "Ảnh DATH", genderFolder);
                            try { Directory.CreateDirectory(anhDathFolder); } catch { }

                            string ext = Path.GetExtension(duongDan);
                            string fileName = tenTK + ext;
                            string destFull = Path.Combine(anhDathFolder, fileName);
                            try { File.Copy(duongDan, destFull, true); finalPath = Path.Combine("Ảnh DATH", genderFolder, fileName); }
                            catch { finalPath = duongDan; }
                        }
                    }
                    catch { }

                    p[2] = "Ảnh:" + finalPath;
                    lines[i] = string.Join(" | ", p);
                    break;
                }
            }

            File.WriteAllLines("CauHoi.txt", lines);
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            string emailMoi = txtEmail.Text.Trim();
            string gioiTinhMoi = radNam.Checked ? "Nam" : radNu.Checked ? "Nữ" : "";

            var lines = File.ReadAllLines("TaiKhoan.txt").ToList();

            for (int i = 0; i < lines.Count; i++)
            {
                string[] p = lines[i].Split('|').Select(x => x.Trim()).ToArray();
                if (p.Length < 5) continue;

                if (p[1].Equals(taiKhoanDangNhap.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    string emailCu = p[3];
                    string gioiTinhCu = p[4];

                    if (emailMoi == "")
                        emailMoi = emailCu;

                    if (gioiTinhMoi == "")
                        gioiTinhMoi = gioiTinhCu;

                    lines[i] = $"{p[0]} | {p[1]} | {p[2]} | {emailMoi} | {gioiTinhMoi}";
                    break;
                }
            }

            File.WriteAllLines("TaiKhoan.txt", lines);

            CapNhatAnhTrongCauHoi(taiKhoanDangNhap, duongDanAnh);

            MessageBox.Show("Cập nhật thành công!");
            this.Close();
        }

        private void FCapNhatThongTinCaNhan_Load(object sender, EventArgs e)
        {
            if (!File.Exists("TaiKhoan.txt"))
                return;

            var ds = File.ReadAllLines("TaiKhoan.txt");

            foreach (var line in ds)
            {
                string[] p = line.Split('|').Select(x => x.Trim()).ToArray();
                if (p.Length < 5) continue;

                if (p[1].Equals(taiKhoanDangNhap.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    txtTen.Text = p[1];
                    txtTen.Enabled = false;

                    txtEmail.Text = p[3];

                    string gt = p[4].ToLower();
                    if (gt == "nam") radNam.Checked = true;
                    else radNu.Checked = true;

                    return;
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXoaTaiKhoan_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn sắp xóa tài khoản của mình!\nBạn sẽ không thể khôi phục lại sau khi xóa. Bạn có chắc chắn?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            string tenTK = taiKhoanDangNhap.Trim();

            // XÓA TRONG TAIKHOAN.TXT
            if (File.Exists("TaiKhoan.txt"))
            {
                var lines = File.ReadAllLines("TaiKhoan.txt").ToList();

                // Xóa dòng chứa tài khoản
                lines = lines.Where(line =>
                {
                    string[] p = line.Split('|').Select(x => x.Trim()).ToArray();
                    return p.Length < 2 || !p[1].Equals(tenTK, StringComparison.OrdinalIgnoreCase);
                }).ToList();

                // Sắp lại STT
                for (int i = 0; i < lines.Count; i++)
                {
                    string[] p = lines[i].Split('|').Select(x => x.Trim()).ToArray();
                    p[0] = (i + 1).ToString();
                    lines[i] = string.Join(" | ", p);
                }

                File.WriteAllLines("TaiKhoan.txt", lines);
            }

            // XÓA TRONG CAUHOI.TXT
            if (File.Exists("CauHoi.txt"))
            {
                var lines = File.ReadAllLines("CauHoi.txt").ToList();

                // Xóa thông tin câu hỏi
                lines = lines.Where(line =>
                {
                    string[] p = line.Split('|').Select(x => x.Trim()).ToArray();
                    return p.Length < 2 || !p[1].Equals(tenTK, StringComparison.OrdinalIgnoreCase);
                }).ToList();

                // Sắp lại STT
                for (int i = 0; i < lines.Count; i++)
                {
                    string[] p = lines[i].Split('|').Select(x => x.Trim()).ToArray();
                    p[0] = (i + 1).ToString();
                    lines[i] = string.Join(" | ", p);
                }

                File.WriteAllLines("CauHoi.txt", lines);
            }
            // THÔNG BÁO
            MessageBox.Show("Xóa tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Đóng FormHome nếu đang mở
            if (Application.OpenForms["FormHome"] != null)
                Application.OpenForms["FormHome"].Close();

            // Đóng FTrangCaNhan nếu đang mở
            if (Application.OpenForms["FTrangCaNhan"] != null)
                Application.OpenForms["FTrangCaNhan"].Close();

            // Đóng chính form này
            this.Close();

            // QUAY VỀ ĐĂNG NHẬP
            DangNhap dn = new DangNhap();
            dn.Show();
        }
    }
}
