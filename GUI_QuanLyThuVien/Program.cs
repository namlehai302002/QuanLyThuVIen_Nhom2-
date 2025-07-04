// Program.cs
using GUI_QuanLyThuVien;

namespace Nhom2_QuanLyThuVien
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Mở form Welcome (không kiểm tra DialogResult)
            using (var welcomeForm = new frmWelcome())
            {
                welcomeForm.ShowDialog();
            }

            // Mở form đăng nhập
            Application.Run(new frmLogin());
        }
    }
}