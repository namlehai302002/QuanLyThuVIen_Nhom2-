using GUI_QuanLyThuVien;

namespace Nhom2_QuanLyThuVien
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Mở frmWelcome dưới dạng modal (ShowDialog)
            using (var welcomeForm = new frmWelcome())
            {
                welcomeForm.ShowDialog();
            }

            // Sau khi frmWelcome đóng thì mở frmLogin
            Application.Run(new frmMainForm());
        }
    }
}