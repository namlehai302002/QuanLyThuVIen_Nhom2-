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
        private readonly BusNhanVien busNV = new BusNhanVien(); // Load combobox mã NV

        public frmNhapSach()
        {
            InitializeComponent();
            LoadData();
            LoadComboMaNhanVien();
            SinhMaTuDong();
        }

        private void LoadData()
        {
            // Load dữ liệu
            dgvNhapSach.DataSource = busNhap.LayTatCaNhapSach();
        }

        private void LoadComboMaNhanVien()
        {
            var dsNV = busNV.GetNhanVienList();
            cboMaNV.DataSource = dsNV;
            cboMaNV.DisplayMember = "TenNhanVien"; // Hoặc MaNhanVien nếu không có tên
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
                    MaKho = txtMaKho.Text.Trim()
                };

                busNhap.ThemNhapSach(nhap);
                MessageBox.Show("Thêm nhập sách thành công!");
                LoadData();
                ClearForm();
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
                    MaKho = txtMaKho.Text.Trim()
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
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void BtnLamMoi_Click(object sender, EventArgs e)
        {
            ClearForm();
            SinhMaTuDong();
            LoadData();
        }

        private void dgvNhapSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvNhapSach.Rows[e.RowIndex];
                txtMaNhap.Text = row.Cells["MaNhap"].Value?.ToString();
                cboMaNV.SelectedValue = row.Cells["MaNhanVien"].Value?.ToString();
                dtpNgayNhap.Value = Convert.ToDateTime(row.Cells["NgayNhap"].Value);
                txtGhiChu.Text = row.Cells["GhiChu"].Value?.ToString();
                txtMaKho.Text = row.Cells["MaKho"].Value?.ToString();
            }
        }

        private void dgvNhapSach_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string maNhap = dgvNhapSach.Rows[e.RowIndex].Cells["MaNhap"].Value.ToString();
                frmChiTietNhapSach frm = new frmChiTietNhapSach(maNhap);
                frm.ShowDialog();
            }
        }

        private void ClearForm()
        {
            txtMaNhap.Clear();
            txtGhiChu.Clear();
            txtMaKho.Clear();
            cboMaNV.SelectedIndex = 0;
            dtpNgayNhap.Value = DateTime.Now;
        }

        private void SinhMaTuDong()
        {
            txtMaNhap.Text = busNhap.TaoMaNhapTuDong();
        }
    }
}
