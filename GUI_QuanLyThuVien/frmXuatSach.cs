using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BLL_QuanLyThuVien;
using BLL_QUANLYTHUVIEN;
using DTO_QuanLyThuVien;

namespace GUI_QuanLyThuVien
{
    public partial class frmXuatSach : Form
    {
        private readonly BUSXuatSach busXuat = new BUSXuatSach();
        private readonly BusNhanVien busNV = new BusNhanVien();

        public frmXuatSach()
        {
            InitializeComponent();
            LoadData();
            LoadComboMaNhanVien();
            txtMaXuat.Text = busXuat.TaoMaXuatTuDong(); // Mã xuất tự động
        }

        private void LoadData()
        {
            var data = busXuat.LayTatCaXuatSach();
            dgvXuatSach.DataSource = data;
        }

        private void LoadComboMaNhanVien()
        {
            var dsNV = busNV.GetNhanVienList();
            cboMaNV.DataSource = dsNV;
            cboMaNV.DisplayMember = "Ten";
            cboMaNV.ValueMember = "MaNhanVien";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                XuatSach xuat = new XuatSach
                {
                    MaXuat = txtMaXuat.Text.Trim(),
                    MaNhanVien = cboMaNV.SelectedValue.ToString(),
                    NgayXuat = dtpNgayXuat.Value,
                    LyDo = txtLyDo.Text.Trim(),
                    MaKho = "MK001" // Hoặc cho người dùng chọn
                };

                busXuat.ThemXuatSach(xuat);
                MessageBox.Show("Thêm xuất sách thành công!");
                LoadData();
                ResetForm();
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
                XuatSach xuat = new XuatSach
                {
                    MaXuat = txtMaXuat.Text.Trim(),
                    MaNhanVien = cboMaNV.SelectedValue.ToString(),
                    NgayXuat = dtpNgayXuat.Value,
                    LyDo = txtLyDo.Text.Trim(),
                    MaKho = "MK001"
                };

                busXuat.CapNhatXuatSach(xuat);
                MessageBox.Show("Cập nhật thành công!");
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                string maXuat = txtMaXuat.Text.Trim();
                busXuat.XoaXuatSach(maXuat);
                MessageBox.Show("Xóa thành công!");
                LoadData();
                ResetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void ResetForm()
        {
            txtMaXuat.Clear();
            txtLyDo.Clear();
            cboMaNV.SelectedIndex = 0;
            dtpNgayXuat.Value = DateTime.Now;
            txtMaXuat.Text = busXuat.TaoMaXuatTuDong();
        }

        private void dgvXuatSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvXuatSach.Rows[e.RowIndex];
                txtMaXuat.Text = row.Cells["MaXuat"].Value.ToString();
                cboMaNV.SelectedValue = row.Cells["MaNhanVien"].Value.ToString();
                dtpNgayXuat.Value = Convert.ToDateTime(row.Cells["NgayNhap"].Value);
                txtLyDo.Text = row.Cells["LyDo"].Value.ToString();
            }
        }

        private void dgvXuatSach_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvXuatSach.Rows[e.RowIndex];

                txtMaXuat.Text = row.Cells["MaXuat"].Value?.ToString();
                cboMaNV.SelectedValue = row.Cells["MaNhanVien"].Value?.ToString();

                if (DateTime.TryParse(row.Cells["NgayXuat"].Value?.ToString(), out DateTime ngayXuat))
                {
                    dtpNgayXuat.Value = ngayXuat;
                }

                txtLyDo.Text = row.Cells["LyDo"].Value?.ToString();
            }
        }
        private void dgvXuatSach_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string maXuat = dgvXuatSach.Rows[e.RowIndex].Cells["MaXuat"].Value.ToString();
                frmChiTietXuatSach frm = new frmChiTietXuatSach(maXuat);
                frm.ShowDialog();
            }
        }

    }
}
