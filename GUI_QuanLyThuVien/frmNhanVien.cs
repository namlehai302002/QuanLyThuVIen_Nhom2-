using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DTO_QuanLyThuVien;
using DAL_QUANLYTHUVIEN;
using BLL_QUANLYTHUVIEN;

namespace GUI_QuanLyThuVien
{
    public partial class frmNhanVien : Form
    {
        public BusNhanVien busNhanVien = new BusNhanVien();
        public frmNhanVien()
        {
            InitializeComponent();
        }
        private void LoadDanhSachNhanVien()
        {
            dgvDanhSachNV.DataSource = busNhanVien.GetNhanVienList();
            dgvDanhSachNV.Columns["MaNhanVien"].HeaderText = "Mã Nhân Viên";
            dgvDanhSachNV.Columns["Ten"].HeaderText = "Họ Tên";
            dgvDanhSachNV.Columns["Email"].HeaderText = "Email";
            dgvDanhSachNV.Columns["MatKhau"].HeaderText = "Mật Khẩu";
            dgvDanhSachNV.Columns["SoDienThoai"].HeaderText = "Số Điện Thoại";
            dgvDanhSachNV.Columns["VaiTro"].Visible = false;
            dgvDanhSachNV.Columns["TrangThai"].Visible = false;
            dgvDanhSachNV.Columns["NgayTao"].HeaderText = "Ngày Tạo";

            dgvDanhSachNV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            txtMaNV.Text = TaoMaNhanVienTuDong();
            LoadDanhSachNhanVien();
            btnSua.Enabled = false;
            txtMaNV.Enabled = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string maNV = txtMaNV.Text;
            string Ten = txtTen.Text;
            string email = txtEmail.Text;
            string matKhau = txtMatKhau.Text;
            string soDienThoai = txtSDT.Text;

            bool vaiTro = rbtNhanVien.Checked ? false : true;
            bool trangThai = rbtDangHoatDong.Checked ? true : false;

            if (string.IsNullOrEmpty(maNV) || string.IsNullOrEmpty(Ten) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(matKhau) || string.IsNullOrEmpty(soDienThoai))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin nhân viên.");
                return;
            }

            NhanVien nv = new NhanVien
            {
                MaNhanVien = maNV,
                Ten = Ten,
                Email = email,
                MatKhau = matKhau,
                SoDienThoai = soDienThoai,
                VaiTro = vaiTro,
                TrangThai = trangThai,
                NgayTao = dtpNgayTao.Value
            };

            string result = busNhanVien.AddNhanVien(nv);

            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Thêm nhân viên thành công.");
                ClearForm();
                LoadDanhSachNhanVien();

                // Gán mã mới cho ô txtMaNV sau khi thêm thành công
                txtMaNV.Text = TaoMaNhanVienTuDong();
            }
            else
            {
                MessageBox.Show(result);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string maNV = txtMaNV.Text;
            string Ten = txtTen.Text;
            string email = txtEmail.Text;
            string matKhau = txtMatKhau.Text;
            string sdt = txtSDT.Text;
            bool vaiTro = rbtNhanVien.Checked ? false : true;
            bool trangThai = rbtDangHoatDong.Checked ? true : false;

            if (string.IsNullOrEmpty(maNV) || string.IsNullOrEmpty(Ten) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(matKhau))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin nhân viên.");
                return;
            }

            DateTime ngayTao = dtpNgayTao.Value;
            if (ngayTao.Year < 1753)
            {
                ngayTao = DateTime.Now;
            }

            NhanVien nv = new NhanVien
            {
                MaNhanVien = maNV,
                Ten = Ten,
                Email = email,
                MatKhau = matKhau,
                SoDienThoai = sdt,
                VaiTro = vaiTro,
                TrangThai = trangThai,
                NgayTao = ngayTao
            };

            string result = busNhanVien.UpdateNhanVien(nv);

            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Cập nhật nhân viên thành công.");
                ClearForm();
                LoadDanhSachNhanVien();
            }
            else
            {
                MessageBox.Show(result);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maNV = txtMaNV.Text;

            if (string.IsNullOrEmpty(maNV))
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xóa.");
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                string deleteResult = busNhanVien.DeleteNhanVien(maNV);

                if (string.IsNullOrEmpty(deleteResult))
                {
                    MessageBox.Show("Xóa nhân viên thành công.");
                    ClearForm();
                    LoadDanhSachNhanVien();
                }
                else
                {
                    MessageBox.Show(deleteResult);
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearForm();
            LoadDanhSachNhanVien();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                LoadDanhSachNhanVien();
                return;
            }

            var ketQua = busNhanVien.SearchNhanVien(keyword);

            dgvDanhSachNV.DataSource = null;
            dgvDanhSachNV.DataSource = ketQua;

            dgvDanhSachNV.Columns["MaNhanVien"].HeaderText = "Mã Nhân Viên";
            dgvDanhSachNV.Columns["Ten"].HeaderText = "Họ Tên";
            dgvDanhSachNV.Columns["Email"].HeaderText = "Email";
            dgvDanhSachNV.Columns["MatKhau"].HeaderText = "Mật Khẩu";
            dgvDanhSachNV.Columns["SoDienThoai"].HeaderText = "Số Điện Thoại";
            dgvDanhSachNV.Columns["VaiTro"].Visible = false;
            dgvDanhSachNV.Columns["TrangThai"].Visible = false;
            dgvDanhSachNV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void ClearForm()
        {
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = true;
            txtMaNV.Clear();
            txtTen.Clear();
            txtEmail.Clear();
            txtMatKhau.Clear();
            txtSDT.Clear();
            dtpNgayTao.Value = DateTime.Now;
            rbtNhanVien.Checked = true;
            rbtDangHoatDong.Checked = true;
        }

        private void dgvDanhSachNV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDanhSachNV.Rows[e.RowIndex];
                txtMaNV.Text = row.Cells["MaNhanVien"].Value.ToString();
                txtTen.Text = row.Cells["Ten"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                txtMatKhau.Text = row.Cells["MatKhau"].Value.ToString();
                txtSDT.Text = row.Cells["SoDienThoai"].Value.ToString();
                rbtNhanVien.Checked = !(bool)row.Cells["VaiTro"].Value;
                rbtDangHoatDong.Checked = (bool)row.Cells["TrangThai"].Value;

                btnSua.Enabled = true;
            }
        }
        private string TaoMaNhanVienTuDong()
        {
            string sql = "SELECT MAX(MaNhanVien) FROM NhanVien";
            string maxMa = DBUtil.ExecuteScalar<string>(sql, new List<object>());

            if (string.IsNullOrEmpty(maxMa))
                return "NV001";

            string numberPart = maxMa.Substring(2);
            int num = int.Parse(numberPart);

            num++;

            return "NV" + num.ToString("D3");
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
                    }
    }
}
