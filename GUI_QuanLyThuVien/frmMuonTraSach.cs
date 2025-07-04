using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL_QuanLyThuVien;
using BLL_QUANLYTHUVIEN;
using DTO_QuanLyThuVien;

namespace GUI_QuanLyThuVien
{
    public partial class frmMuonTraSach : Form
    {
        BusMuonTraSach bus = new BusMuonTraSach();
        BusKhachHang busKhachHang = new BusKhachHang();
        BusNhanVien busNhanVien = new BusNhanVien();
        public frmMuonTraSach()
        {
            InitializeComponent();
            LoadData();
            LoadTrangThai();
            LoadMaMuonTraMoi();
            LoadComboBoxes();
            LoadTrangThai();
            dgvDanhSachMuonTraSach.CellClick += dgvDanhSachMuonTraSach_CellClick;
            txtMaMuonTra.ReadOnly = false;
        }
        private void LoadData()
        {
            dgvDanhSachMuonTraSach.DataSource = bus.GetAll();
        }
        private void LoadTrangThai()
        {
            var trangThaiList = new[]
            {
            new { Ma = "TT001", Ten = "Chưa trả" },
            new { Ma = "TT002", Ten = "Đã trả" },
            new { Ma = "TT003", Ten = "Quá hạn" },
            new { Ma = "TT004", Ten = "Đang mượn" }
            };

            cboMaTrangThai.DataSource = null;
            cboMaTrangThai.DataSource = trangThaiList;
            cboMaTrangThai.DisplayMember = "Ma";
            cboMaTrangThai.ValueMember = "Ma";
            cboMaTrangThai.SelectedIndex = -1;
        }
        private void LoadMaMuonTraMoi()
        {
            txtMaMuonTra.Text = bus.TaoMaMuonTraMoi();
        }
        private void frmMuonTraSach_Load(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            var muon = new MuonTraSach
            {
                MaMuonTra = txtMaMuonTra.Text.Trim(),
                MaKhachHang = cboMaKhachHang.Text,
                MaNhanVien = cboMaNhanVien.Text,
                NgayMuon = DateOnly.FromDateTime(dtpNgayMuon.Value),
                NgayTra = DateOnly.FromDateTime(dtpNgayTra.Value),
                MaTrangThai = cboMaTrangThai.SelectedValue?.ToString() ?? "",
                NgayTao = dtpNgayTao.Value
            };

            string result = bus.Add(muon);
            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Thêm thành công!");
                LoadData();
                LoadMaMuonTraMoi(); // Tạo mã mới sau khi thêm thành công
                txtMaMuonTra.ReadOnly = false; // Cho phép nhập mã mới
            }
            else
            {
                MessageBox.Show(result);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            var muon = new MuonTraSach
            {
                MaMuonTra = txtMaMuonTra.Text.Trim(),
                MaKhachHang = cboMaKhachHang.Text,
                MaNhanVien = cboMaNhanVien.Text,
                NgayMuon = DateOnly.FromDateTime(dtpNgayMuon.Value),
                NgayTra = DateOnly.FromDateTime(dtpNgayTra.Value),
                MaTrangThai = cboMaTrangThai.SelectedValue?.ToString() ?? "",
                NgayTao = dtpNgayTao.Value
            };

            string result = bus.Update(muon);
            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Cập nhật thành công!");
                LoadData();
            }
            else
            {
                MessageBox.Show(result);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string ma = txtMaMuonTra.Text.Trim();
            if (string.IsNullOrEmpty(ma))
            {
                MessageBox.Show("Vui lòng chọn mã cần xóa.");
                return;
            }

            DialogResult dialog = MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                string result = bus.Delete(ma);
                if (string.IsNullOrEmpty(result))
                {
                    MessageBox.Show("Xóa thành công!");
                    LoadData();
                    LoadMaMuonTraMoi(); // Tạo mã mới sau khi xóa
                    txtMaMuonTra.ReadOnly = false; // Cho phép nhập mã mới
                }
                else
                {
                    MessageBox.Show(result);
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadMaMuonTraMoi();
            txtMaMuonTra.ReadOnly = false; // Cho phép nhập mã mới khi làm mới

            cboMaKhachHang.SelectedIndex = -1;
            cboMaNhanVien.SelectedIndex = -1;
            cboMaTrangThai.SelectedIndex = -1;
            dtpNgayMuon.Value = DateTime.Now;
            dtpNgayTra.Value = DateTime.Now;
            dtpNgayTao.Value = DateTime.Now;
            txtTimKiem.Clear();

            LoadData();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {

            string keyword = txtTimKiem.Text.Trim();
            dgvDanhSachMuonTraSach.DataSource = bus.Search(keyword);
        }

        private void dgvDanhSachMuonTraSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDanhSachMuonTraSach.Rows[e.RowIndex];
                txtMaMuonTra.Text = row.Cells["MaMuonTra"].Value?.ToString() ?? "";
                cboMaKhachHang.Text = row.Cells["MaKhachHang"].Value?.ToString() ?? "";
                cboMaNhanVien.Text = row.Cells["MaNhanVien"].Value?.ToString() ?? "";

                var ngayMuonObj = row.Cells["NgayMuon"].Value;
                if (ngayMuonObj is DateOnly ngayMuonDateOnly)
                    dtpNgayMuon.Value = ngayMuonDateOnly.ToDateTime(TimeOnly.MinValue);
                else if (ngayMuonObj != null)
                    dtpNgayMuon.Value = Convert.ToDateTime(ngayMuonObj);

                var ngayTraObj = row.Cells["NgayTra"].Value;
                if (ngayTraObj is DateOnly ngayTraDateOnly)
                    dtpNgayTra.Value = ngayTraDateOnly.ToDateTime(TimeOnly.MinValue);
                else if (ngayTraObj != null)
                    dtpNgayTra.Value = Convert.ToDateTime(ngayTraObj);

                var maTrangThai = row.Cells["MaTrangThai"].Value?.ToString() ?? "";
                cboMaTrangThai.SelectedValue = maTrangThai;

                var ngayTaoObj = row.Cells["NgayTao"].Value;
                if (ngayTaoObj is DateTime ngayTaoDateTime)
                    dtpNgayTao.Value = ngayTaoDateTime;
                else if (ngayTaoObj != null)
                    dtpNgayTao.Value = Convert.ToDateTime(ngayTaoObj);

                // Khi chọn dòng dữ liệu, khóa không cho sửa mã mượn trả
                txtMaMuonTra.ReadOnly = true;
            }
        }

        private void dgvDanhSachMuonTraSach_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string ma = dgvDanhSachMuonTraSach.Rows[e.RowIndex].Cells["MaMuonTra"].Value?.ToString() ?? "";
                if (!string.IsNullOrEmpty(ma))
                {
                    frmChiTietMuonSach frmChiTiet = new frmChiTietMuonSach(ma);
                    frmChiTiet.ShowDialog();
                }
            }
        }
        private void LoadComboBoxes()
        {
            var listKhachHang = busKhachHang.GetKhachHangList();
            cboMaKhachHang.DataSource = null;
            cboMaKhachHang.DataSource = listKhachHang;
            cboMaKhachHang.DisplayMember = "MaKhachHang";
            cboMaKhachHang.ValueMember = "MaKhachHang";
            cboMaKhachHang.SelectedIndex = -1;

            var listNhanVien = busNhanVien.GetNhanVienList();
            cboMaNhanVien.DataSource = null;
            cboMaNhanVien.DataSource = listNhanVien;
            cboMaNhanVien.DisplayMember = "MaNhanVien";
            cboMaNhanVien.ValueMember = "MaNhanVien";
            cboMaNhanVien.SelectedIndex = -1;
        }
    }
}
