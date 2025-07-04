using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI_QuanLyThuVien
{
    public partial class frmMainForm : Form
    {
        private Form currentFormChild;
        public frmMainForm()
        {
            InitializeComponent();
        }
        private void openChildForm(Form formChild)
        {
            if (currentFormChild != null)
            {
                currentFormChild.Close();
            }
            currentFormChild = formChild;
            formChild.TopLevel = false;
            formChild.FormBorderStyle = FormBorderStyle.None;
            formChild.Dock = DockStyle.Fill;
            pnMain.Controls.Add(formChild);
            pnMain.Tag = formChild;
            formChild.BringToFront();
            formChild.Show();
        }
        private void btnSach_Click(object sender, EventArgs e)
        {
            openChildForm(new frmSach());
        }

        private void btnTheLoaiSach_Click(object sender, EventArgs e)
        {
            openChildForm(new frmTheLoaiSach());
        }

        private void btnTacGia_Click(object sender, EventArgs e)
        {
            openChildForm(new frmTacGia());
        }

        private void btnTrangThaiTT_Click(object sender, EventArgs e)
        {
            openChildForm(new frmTrangThaiThanhToan());
        }

        private void btnPhiSach_Click(object sender, EventArgs e)
        {
            openChildForm(new frmPhiSach());
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            openChildForm(new frmNhanVien());
        }

        private void btnMuonTra_Click(object sender, EventArgs e)
        {
            openChildForm(new frmMuonTraSach());
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            openChildForm(new frmKhachHang());
        }

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            openChildForm(new frmDoiMatKhau());
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            openChildForm(new());
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            openChildForm(new frmSach());
        }

        private void frmMainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
