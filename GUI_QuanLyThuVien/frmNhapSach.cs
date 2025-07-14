using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BLL_QuanLyThuVien;
using BLL_QUANLYTHUVIEN;
using DTO_QuanLyThuVien;

namespace GUI_QuanLyThuVien
{
    public partial class frmNhapSach : Form
    {
        private readonly BUSNhapSach busNhap = new BUSNhapSach();
        private readonly BusNhanVien busNV = new BusNhanVien(); // để load combobox mã NV

        public frmNhapSach()
        {
            InitializeComponent();
            LoadData();
            LoadComboMaNhanVien();
        }

        private void frmNhapSach_Load(object sender, EventArgs e)
        {

        }

        private void LoadData()
        {
            dgvNhapSach.DataSource = busNhap.LayTatCaNhapSach();
        }

        private void LoadComboMaNhanVien()
        {
            var dsNV = busNV.GetNhanVienList(); // Bạn cần có BUSNhanVien.LayTatCaNhanVien()
            cboMaNV.DataSource = dsNV;
            cboMaNV.DisplayMember = "TenNhanVien"; // hoặc "MaNhanVien" nếu không có tên
            cboMaNV.ValueMember = "MaNhanVien";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                NhapSach nhap = new NhapSach
                {
                    MaNhap = txtMaNhap.Text.Trim(),
                    MaNhanVien = cboMaNV.SelectedValue.ToString(),
                    NgayNhap = dtpNgayNhap.Value,
                    GhiChu = txtGhiChu.Text.Trim(),
                    MaKho = "MK001" // Cứng mã kho hoặc cho textbox nếu có
                };

                busNhap.ThemNhapSach(nhap);
                MessageBox.Show("Thêm nhập sách thành công!");
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                NhapSach nhap = new NhapSach
                {
                    MaNhap = txtMaNhap.Text.Trim(),
                    MaNhanVien = cboMaNV.SelectedValue.ToString(),
                    NgayNhap = dtpNgayNhap.Value,
                    GhiChu = txtGhiChu.Text.Trim(),
                    MaKho = "MK001"
                };

                busNhap.CapNhatNhapSach(nhap);
                MessageBox.Show("Cập nhật thành công!");
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                string maNhap = txtMaNhap.Text.Trim();
                busNhap.XoaNhapSach(maNhap);
                MessageBox.Show("Xóa thành công!");
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void BtnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaNhap.Clear();
            txtGhiChu.Clear();
            cboMaNV.SelectedIndex = 0;
            dtpNgayNhap.Value = DateTime.Now;
            txtMaNhap.Text = busNhap.TaoMaNhapTuDong(); // Gợi ý mã tự động nếu có
        }

        private void dgvNhapSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvNhapSach.Rows[e.RowIndex];
                txtMaNhap.Text = row.Cells["MaNhap"].Value.ToString();
                cboMaNV.SelectedValue = row.Cells["MaNhanVien"].Value.ToString();
                dtpNgayNhap.Value = Convert.ToDateTime(row.Cells["NgayNhap"].Value);
                txtGhiChu.Text = row.Cells["GhiChu"].Value.ToString();
            }
        }

        private void frmNhapSach_Load_1(object sender, EventArgs e)
        {

        }

        private void dgvNhapSach_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }
    }
}
