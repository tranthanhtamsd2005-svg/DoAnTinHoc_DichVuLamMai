using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace DoAnTinHoc
{
    public partial class FCauHoi1 : Form
    {
        private string tenTaiKhoan;
      
        private string anhPath = "";

        public FCauHoi1(string tenTaiKhoan)
        {
            InitializeComponent();
            this.tenTaiKhoan = tenTaiKhoan;
        }

     
        private void picThem_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            open.Title = "Chọn ảnh của bạn";

            if (open.ShowDialog() == DialogResult.OK)
            {
                anhPath = open.FileName;
                picAnh.Image = Image.FromFile(open.FileName);
                picAnh.SizeMode = PictureBoxSizeMode.Zoom;
                picThem.Visible = false;
                btnKeTiep2.Enabled = true;
            }
        }

        private void btnKeTiep2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(anhPath))
            {
                MessageBox.Show("Vui lòng chọn ảnh trước khi tiếp tục!", "Thiếu ảnh", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string destRel = CopyImageToRepo(anhPath, tenTaiKhoan);
                XuLyCauHoi xl = new XuLyCauHoi();
                xl.LuuCauHoi(tenTaiKhoan, $"Ảnh:{destRel}");
                FCauHoi2 fCauHoi2 = new FCauHoi2(tenTaiKhoan, this);
                this.Hide();
                fCauHoi2.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi ghi file: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string CopyImageToRepo(string sourcePath, string tenTK)
        {
            if (string.IsNullOrWhiteSpace(sourcePath) || !File.Exists(sourcePath)) return sourcePath;
            string repoRoot = null;
            try
            {
                var dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
                for (int i = 0; i < 8 && dir != null; i++)
                {
                    var sln = dir.GetFiles("*.sln").FirstOrDefault();
                    if (sln != null) { repoRoot = dir.FullName; break; }
                    dir = dir.Parent;
                }
            }
            catch { }

            if (repoRoot == null)
                repoRoot = AppDomain.CurrentDomain.BaseDirectory;

            string genderFolder = "AnhNam"; 
            try
            {
                string tkPath1 = Path.Combine(repoRoot, "DoAnTinHoc", "TaiKhoan.txt");
                string tkPath2 = Path.Combine(repoRoot, "TaiKhoan.txt");
                string tkPath = File.Exists(tkPath1) ? tkPath1 : (File.Exists(tkPath2) ? tkPath2 : "TaiKhoan.txt");

                if (File.Exists(tkPath))
                {
                    var lines = File.ReadAllLines(tkPath);
                    foreach (var line in lines)
                    {
                        var p = line.Split('|').Select(x => x.Trim()).ToArray();
                        if (p.Length >= 5 && p[1].Equals(tenTK, StringComparison.OrdinalIgnoreCase))
                        {
                            var gt = p[4].ToLower();
                            genderFolder = (gt == "nữ" || gt == "nu") ? "AnhNu" : "AnhNam";
                            break;
                        }
                    }
                }
            }
            catch { }

            string anhDathFolder = Path.Combine(repoRoot, "Ảnh DATH", genderFolder);
            try { Directory.CreateDirectory(anhDathFolder); } catch { }

            string ext = Path.GetExtension(sourcePath);
            string fileName = tenTK + ext;
            string destFull = Path.Combine(anhDathFolder, fileName);

            try
            {
                File.Copy(sourcePath, destFull, true);
            }
            catch { }
            string rel = Path.Combine("Ảnh DATH", genderFolder, fileName);
            return rel;
        }

        private void FCauHoi1_Load(object sender, EventArgs e)
        {
            btnKeTiep2.Enabled = false;
        }


    }
}
