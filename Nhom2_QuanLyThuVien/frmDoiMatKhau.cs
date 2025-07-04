using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL_QuanLyThuVien;
using DTO_QuanLyThuVien;

namespace GUI_QuanLyThuVien
{
    public partial class frmDoiMatKhau : Form
    {
        private readonly DALNhanVien dalNV = new DALNhanVien();
        private readonly NhanVien? nhanVienDangNhap;
        private string maNhanVien;  // Biến lưu mã nhân viên được truyền vào

        // Trạng thái hiển thị mật khẩu
        private bool hienCu = false;
        private bool hienMoi = false;
        private bool hienLai = false;
        public frmDoiMatKhau(string maNhanVien)
        {
            InitializeComponent();
            this.maNhanVien = maNhanVien;

            // Ẩn mật khẩu và không cho chỉnh sửa các trường
            txtTenNV.ReadOnly = true;
            txtMaNV.ReadOnly = true;
            txtMatKhauCu.ReadOnly = true;
            txtMatKhauCu.PasswordChar = '*';
            txtMatKhauMoi.PasswordChar = '*';
            txtXacNhanMatKhauMoi.PasswordChar = '*';

            // Lấy thông tin nhân viên từ database
            nhanVienDangNhap = dalNV.GetNhanVienByMa(maNhanVien);
            if (nhanVienDangNhap != null)
            {
                txtMaNV.Text = nhanVienDangNhap.MaNhanVien;
                txtTenNV.Text = nhanVienDangNhap.Ten;
                txtMatKhauCu.Text = nhanVienDangNhap.MatKhau;
            }
            else
            {
                MessageBox.Show("Không tìm thấy nhân viên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }
        public frmDoiMatKhau()
        {
            InitializeComponent();
        }

        private void frmDoiMatKhau_Load(object sender, EventArgs e)
        {
            btnDoiMatKhau.Enabled = false;
            btnDoiMatKhau.FillColor = Color.LightGray;

            // chu xuong dong 
            labelXacNhanMatKhau.Text = "Xác nhận mật\nkhẩu mới";

            //hover
            btnDoiMatKhau.HoverState.FillColor = Color.FromArgb(100, 88, 255); // Màu khi hover
            btnDoiMatKhau.HoverState.ForeColor = Color.White;
            btnDoiMatKhau.HoverState.BorderColor = Color.DeepSkyBlue;
            btnDoiMatKhau.HoverState.Font = new Font("Segoe UI", 10, FontStyle.Bold); // Nếu muốn chữ nổi bật hơn
            btnDoiMatKhau.Animated = true;

            //hieu ung khi click
            btnDoiMatKhau.PressedColor = Color.MediumPurple; // Màu nền khi nhấn
            btnDoiMatKhau.PressedDepth = 70; // Độ "nhấn", càng lớn càng sâu
            btnDoiMatKhau.Click += (s, e) =>
            {
                btnDoiMatKhau.Text = "Đang xử lý..."; // Hiệu ứng nhỏ khi click
                                                      // Thêm hiệu ứng khác nếu bạn muốn
            };

            // Khi TextBox được focus (chuột nhấp vào)
            txtMatKhauCu.Enter += (s, e) =>
            {
                txtMatKhauCu.BorderThickness = 2; // Viền to hơn
                txtMatKhauCu.BorderColor = Color.DeepSkyBlue; // Tùy chỉnh màu viền nếu muốn
            };

            // Khi TextBox mất focus (chuột rời đi)
            txtMatKhauCu.Leave += (s, e) =>
            {
                txtMatKhauCu.BorderThickness = 1; // Trở lại bình thường
                txtMatKhauCu.BorderColor = Color.FromArgb(213, 218, 223);

            };
            //-------------TxtMatKhauMoi-------------------
            txtMatKhauMoi.Enter += (s, e) =>
            {
                txtMatKhauMoi.BorderThickness = 2; // Viền to hơn
                txtMatKhauMoi.BorderColor = Color.DeepSkyBlue; // Tùy chỉnh màu viền nếu muốn
            };

            // Khi TextBox mất focus (chuột rời đi)
            txtMatKhauMoi.Leave += (s, e) =>
            {
                txtMatKhauMoi.BorderThickness = 1; // Trở lại bình thường
                txtMatKhauMoi.BorderColor = Color.FromArgb(213, 218, 223);

            };
            //-------------TxtXacNhanMatKhauMoi-------------------
            txtXacNhanMatKhauMoi.Enter += (s, e) =>
            {
                txtXacNhanMatKhauMoi.BorderThickness = 2; // Viền to hơn
                txtXacNhanMatKhauMoi.BorderColor = Color.DeepSkyBlue; // Tùy chỉnh màu viền nếu muốn
            };

            // Khi TextBox mất focus (chuột rời đi)
            txtXacNhanMatKhauMoi.Leave += (s, e) =>
            {
                txtXacNhanMatKhauMoi.BorderThickness = 1; // Trở lại bình thường
                txtXacNhanMatKhauMoi.BorderColor = Color.FromArgb(213, 218, 223);

            };
            //KiemTraNhapDayDu
            txtMatKhauCu.TextChanged += (s, e) => KiemTraNhapDayDu();
            txtMatKhauMoi.TextChanged += (s, e) => KiemTraNhapDayDu();
            txtXacNhanMatKhauMoi.TextChanged += (s, e) => KiemTraNhapDayDu();
        }
        private void KiemTraNhapDayDu()
        {
            bool dayDu = !string.IsNullOrWhiteSpace(txtMatKhauCu.Text)
                      && !string.IsNullOrWhiteSpace(txtMatKhauMoi.Text)
                      && !string.IsNullOrWhiteSpace(txtXacNhanMatKhauMoi.Text);

            btnDoiMatKhau.Enabled = dayDu;
            btnDoiMatKhau.FillColor = dayDu ? Color.FromArgb(60, 154, 226) : Color.LightGray;
        }

        private void btnShow_CheckedChanged(object sender, EventArgs e)
        {
            hienCu = !hienCu;
            if (hienCu)
            {
                txtMatKhauCu.PasswordChar = '\0'; // hiện mật khẩu
                btnShow.Text = ""; // đổi text nút nếu muốn
            }
            else
            {
                txtMatKhauCu.PasswordChar = '*'; // ẩn mật khẩu
                btnShow.Text = "";
            }
        }

        private void btnShowNew_CheckedChanged(object sender, EventArgs e)
        {
            hienMoi = !hienMoi;
            if (hienMoi)
            {
                txtMatKhauMoi.PasswordChar = '\0';
                btnShowNew.Text = "";
            }
            else
            {
                txtMatKhauMoi.PasswordChar = '*';
                btnShowNew.Text = "";
            }
        }

        private void btnShowNewA_CheckedChanged(object sender, EventArgs e)
        {
            hienLai = !hienLai;
            if (hienLai)
            {
                txtXacNhanMatKhauMoi.PasswordChar = '\0';
                btnShowNewA.Text = "";
            }
            else
            {
                txtXacNhanMatKhauMoi.PasswordChar = '*';
                btnShowNewA.Text = "";
            }
        }

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            if (nhanVienDangNhap == null)
            {
                MessageBox.Show("Không có thông tin nhân viên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string mkMoi = txtMatKhauMoi.Text.Trim();
            string nhapLai = txtXacNhanMatKhauMoi.Text.Trim();

            if (string.IsNullOrEmpty(mkMoi) || string.IsNullOrEmpty(nhapLai))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (mkMoi != nhapLai)
            {
                MessageBox.Show("Mật khẩu mới không trùng khớp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            nhanVienDangNhap.MatKhau = mkMoi;

            try
            {
                dalNV.UpdateNhanVien(nhanVienDangNhap);
                MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đổi mật khẩu thất bại: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
