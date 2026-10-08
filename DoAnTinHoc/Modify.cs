using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace DoAnTinHoc
{
    internal class Modify
    {
        // Đọc danh sách tài khoản từ file
        public List<TaiKhoan> DocFile()
        {
            List<TaiKhoan> danhSach = new List<TaiKhoan>();

            using (FileStream fs = new FileStream("TaiKhoan.txt", FileMode.OpenOrCreate, FileAccess.Read))
            using (StreamReader reader = new StreamReader(fs))
            {
                string chuoi;
                while ((chuoi = reader.ReadLine()) != null)
                {
                    string[] mang = chuoi.Split('|');
                    if (mang.Length >= 5)
                    {
                        TaiKhoan tk = new TaiKhoan();
                        tk.STT = int.Parse(mang[0].Trim());
                        tk.Ten = mang[1].Trim();
                        tk.MatKhau = mang[2].Trim();
                        tk.Email = mang[3].Trim();
                        tk.GioiTinh = mang[4].Trim();

                        danhSach.Add(tk);
                    }
                }
            }
            return danhSach;
        }
        public void GhiTaiKhoanMoi(string ten, string mk, string email, string gioiTinh)
        {
            string filePath = "TaiKhoan.txt";
            int stt = File.Exists(filePath) ? File.ReadAllLines(filePath).Length + 1 : 1;

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine($"{stt} | {ten} | {mk} | {email} | {gioiTinh}");
            }
        }



        // Cập nhật tuổi cho tài khoản ở form câu hỏi 2
        public void CapNhatTuoiChoTaiKhoan(string tenTaiKhoan, int tuoi)
        {
            string filePath = "TaiKhoan.txt";
            if (!File.Exists(filePath)) return;

            var lines = File.ReadAllLines(filePath).ToList();

            for (int i = 0; i < lines.Count; i++)
            {
                string[] parts = lines[i].Split('|');

                // Chuẩn hóa
                for (int j = 0; j < parts.Length; j++)
                    parts[j] = parts[j].Trim();

                if (parts.Length >= 5 && parts[1].Equals(tenTaiKhoan, StringComparison.OrdinalIgnoreCase))
                {
                    // Nếu đã có tuổi -> cập nhật
                    if (parts.Length >= 6)
                    {
                        parts[5] = $"Tuoi{tuoi}";
                    }
                    else
                    {
                        Array.Resize(ref parts, 6);
                        parts[5] = $"Tuoi{tuoi}";
                    }

                    // Ghi lại đúng format
                    lines[i] = string.Join(" | ", parts);
                    break;
                }
            }

            File.WriteAllLines(filePath, lines);
        }

    }
}
