using System;
using System.Drawing;
using System.IO;
using System.Linq;

namespace DoAnTinHoc
{
    internal static class ImageHelper
    {
        private static readonly string LogFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ImageFixLog.txt");

        private static void Log(string msg)
        {
            try
            {
                var line = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {msg}" + Environment.NewLine;
                File.AppendAllText(LogFileName, line);
            }
            catch { }
        }

        public static string ResolveImageFullPath(string anh)
        {
            if (string.IsNullOrWhiteSpace(anh)) return null;

            anh = anh.Trim();
            anh = anh.Replace('/', Path.DirectorySeparatorChar).Replace('\\', Path.DirectorySeparatorChar);

            try
            {
                if (Path.IsPathRooted(anh) && File.Exists(anh))
                    return Path.GetFullPath(anh);
            }
            catch { }

            string repoRoot = FindRepoRoot() ?? AppDomain.CurrentDomain.BaseDirectory;
            Log($"ResolveImageFullPath: input='{anh}' repoRoot='{repoRoot}'");

            string[] candidates = new string[]
            {
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, anh),
                Path.Combine(ApplicationStartUpPath(), anh),
                Path.Combine(repoRoot, anh),
                Path.Combine(repoRoot, "Ảnh DATH", anh),
                Path.Combine(repoRoot, "DoAnTinHoc", anh),
                Path.Combine(repoRoot, "DoAnTinHoc", "Ảnh DATH", anh),

                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", anh),
                Path.Combine(ApplicationStartUpPath(), "Resources", anh)
            };

            foreach (var c in candidates.Where(x => !string.IsNullOrWhiteSpace(x)))
            {
                try
                {
                    var full = Path.GetFullPath(c);
                    Log($"  trying candidate '{full}'");
                    if (File.Exists(full))
                    {
                        Log($"  found '{full}'");
                        return full;
                    }
                }
                catch { }
            }

            Log($"  not found candidates for '{anh}'");
            return null;
        }

        public static Image LoadImageSafe(string anh)
        {
            try
            {
                var path = ResolveImageFullPath(anh);
                Log($"LoadImageSafe: resolved='{path}' for input='{anh}'");
                if (!string.IsNullOrWhiteSpace(path))
                {
                    using (var b = new Bitmap(path))
                        return new Bitmap(b);
                }
            }
            catch { }

            try
            {
                var name = Path.GetFileNameWithoutExtension(anh ?? "").Replace('-', '_');
                var obj = Properties.Resources.ResourceManager.GetObject(name);
                if (obj is Bitmap bmpRes) return new Bitmap(bmpRes);
            }
            catch { }

            Log($"LoadImageSafe: falling back to embedded/default for '{anh}'");
            return new Bitmap(Properties.Resources.user_icon_150670);
        }

        private static string FindRepoRoot()
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

        private static string ApplicationStartUpPath()
        {
            try { return System.Windows.Forms.Application.StartupPath; }
            catch { return AppDomain.CurrentDomain.BaseDirectory; }
        }
    }
}
