using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DoAnTinHoc
{
    internal class XuLyCauHoi
    {
        private string filePath = "CauHoi.txt";

        // Kiểm tra tài khoản đã làm câu hỏi chưa
        public bool DaTraLoiCauHoi(string tenTaiKhoan)
        {
            if (!File.Exists(filePath))
                return false;

            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                string[] parts = line.Split('|');
                if (parts.Length < 2)
                    continue;

                string tk = parts[1].Trim(); // Tên tài khoản nằm ở cột 2

                if (tk == tenTaiKhoan)
                    return true;
            }

            return false;
        }

        // Lưu hoặc cập nhật thông tin câu hỏi của tài khoản
        public void LuuCauHoi(string tenTaiKhoan, string thongTin)
        {
            if (string.IsNullOrEmpty(tenTaiKhoan) || string.IsNullOrEmpty(thongTin)) return;

            List<string> lines = File.Exists(filePath)
                ? File.ReadAllLines(filePath).ToList()
                : new List<string>();

            // Tìm dòng chứa tài khoản (bỏ qua STT phía trước)
            int index = lines.FindIndex(l =>
            {
                string[] parts = l.Split('|');
                if (parts.Length < 2) return false;

                string tk = parts[1].Trim();
                return tk == tenTaiKhoan;
            });

            if (index >= 0)
            {
                // Đã tồn tại → cập nhật nhưng giữ nguyên STT
                if (!lines[index].Contains(thongTin))
                    lines[index] += "|" + thongTin;
            }
            else
            {
                // Thêm mới → tạo STT
                int stt = lines.Count + 1;
                lines.Add($"{stt} | {tenTaiKhoan} | {thongTin}");
            }

            File.WriteAllLines(filePath, lines);
        }

        // Normalize image paths stored in CauHoi.txt
        // Convert absolute paths into repo-relative paths like "Ảnh DATH\\AnhNam\\file.jpg"
        public void ChuanHoaDuongDanAnh()
        {
            if (!File.Exists(filePath)) return;

            var lines = File.ReadAllLines(filePath).ToList();
            
            string log = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ImageFixLog.txt");
            void L(string m) { try { File.AppendAllText(log, $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {m}" + Environment.NewLine); } catch { } }

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
            if (repoRoot == null) repoRoot = AppDomain.CurrentDomain.BaseDirectory;

            for (int i = 0; i < lines.Count; i++)
            {
                var parts = lines[i].Split('|').Select(p => p.Trim()).ToArray();
                if (parts.Length < 3) continue;

                string anhPart = parts[2];
                if (!anhPart.StartsWith("Ảnh:", StringComparison.OrdinalIgnoreCase)) continue;

                string pathValue = anhPart.Substring(4).Trim();
                if (string.IsNullOrWhiteSpace(pathValue)) continue;

                try
                {
                   
                    if (Path.IsPathRooted(pathValue) && File.Exists(pathValue))
                    {
                        string full = Path.GetFullPath(pathValue);
                        L($"Processing absolute image path '{full}' for line {i+1}");
                        if (full.StartsWith(repoRoot, StringComparison.OrdinalIgnoreCase))
                        {
                            string rel = full.Substring(repoRoot.Length).TrimStart(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
                            parts[2] = "Ảnh:" + rel;
                            L($"  converted to relative '{rel}'");
                        }
                        else
                        {
                            
                            string fileName = Path.GetFileName(pathValue);
                            try
                            {
                                var matches = Directory.GetFiles(repoRoot, fileName, SearchOption.AllDirectories);
                                if (matches != null && matches.Length > 0)
                                {
                                   
                                    var found = matches[0];
                                    string rel = found.Substring(repoRoot.Length).TrimStart(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
                                    parts[2] = "Ảnh:" + rel;
                                    L($"  found existing '{found}', set relative '{rel}'");
                                }
                                else
                                {
                                    
                                    string user = parts.Length >= 2 ? parts[1] : null;
                                    string genderFolder = "AnhNam";
                                    try
                                    {
                                        L($"  file outside repo, will copy into repo. user='{user}'");
                                        
                                        string tkPath1 = Path.Combine(repoRoot, "DoAnTinHoc", "TaiKhoan.txt");
                                        string tkPath2 = Path.Combine(repoRoot, "TaiKhoan.txt");
                                        string tkPath = File.Exists(tkPath1) ? tkPath1 : (File.Exists(tkPath2) ? tkPath2 : "TaiKhoan.txt");
                                        if (File.Exists(tkPath) && !string.IsNullOrWhiteSpace(user))
                                        {
                                            foreach (var ln in File.ReadAllLines(tkPath))
                                            {
                                                var q = ln.Split('|').Select(x => x.Trim()).ToArray();
                                                if (q.Length >= 5 && q[1].Equals(user, StringComparison.OrdinalIgnoreCase))
                                                {
                                                    var gt = q[4].ToLower();
                                                    genderFolder = (gt == "nữ" || gt == "nu") ? "AnhNu" : "AnhNam";
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                    catch (Exception ex) { L($"  gender detect error: {ex.Message}"); }

                                    string anhDathFolder = Path.Combine(repoRoot, "Ảnh DATH", genderFolder);
                                    try { Directory.CreateDirectory(anhDathFolder); } catch { }

                                    string destFull = Path.Combine(anhDathFolder, fileName);
                                    try { File.Copy(pathValue, destFull, true); L($"  copied to '{destFull}'"); }
                                    catch (Exception ex) { L($"  copy error: {ex.Message}"); }

                                    string rel = Path.Combine("Ảnh DATH", genderFolder, fileName);
                                    parts[2] = "Ảnh:" + rel;
                                    L($"  set relative path '{rel}'");
                                }
                            }
                            catch (Exception ex)
                            {
                                L($"  search/copy error: {ex.Message}");
                            }
                        }
                    }
                    else if (!Path.IsPathRooted(pathValue))
                    {
                       
                        parts[2] = "Ảnh:" + pathValue.Replace('/', Path.DirectorySeparatorChar).Replace('\\', Path.DirectorySeparatorChar);
                        L($"  normalized relative '{parts[2]}'");
                    }
                }
                catch { }

                
                lines[i] = string.Join(" | ", parts);
            }

            try { File.WriteAllLines(filePath, lines); L($"ChuanHoaDuongDanAnh: written {lines.Count} lines"); } catch (Exception ex) { L($"write error: {ex.Message}"); }
        }


    }
}
