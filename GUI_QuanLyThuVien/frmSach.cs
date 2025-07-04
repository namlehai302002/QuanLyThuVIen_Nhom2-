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
using DAL_QUANLYTHUVIEN;
using DTO_QuanLyThuVien;
using Microsoft.Data.SqlClient;

namespace GUI_QuanLyThuVien
{
    public partial class frmSach : Form
    {
        private readonly BUSSach sachBUS = new BUSSach();
        public frmSach()
        {
            InitializeComponent();
        }
        private void LoadTheLoai()
        {
            try
            {
                string sql = "SELECT MaTheLoai, TenTheLoai FROM TheLoaiSach";
                using (SqlDataReader reader = DBUtil.Query(sql, new List<object>()))
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    cboMaTheLoai.DataSource = dt;
                    cboMaTheLoai.DisplayMember = "MaTheLoai";
                    cboMaTheLoai.ValueMember = "MaTheLoai";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải thể loại: " + ex.Message);
            }
        }
        private void LoadTacGia()
        {
            try
            {
                string sql = "SELECT MaTacGia, TenTacGia FROM TACGIA";
                using (SqlDataReader reader = DBUtil.Query(sql, new List<object>()))
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    cboMaTacGia.DataSource = dt;
                    cboMaTacGia.DisplayMember = "TenTacGia";
                    cboMaTacGia.ValueMember = "MaTacGia";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải tác giả: " + ex.Message);
            }
        }
        private void LoadDanhSach()
        {
            dgvDanhSachSach.DataSource = null;
            dgvDanhSachSach.DataSource = sachBUS.LayTatCaSach();
        }
        private void ResetForm()
        {
            txtMaSach.Text = sachBUS.TaoMaSachTuDong();
            txtMaSach.Enabled = false; 

            txtTieuDe.Clear();
            cboMaTheLoai.SelectedIndex = -1;
            cboMaTacGia.SelectedIndex = -1;
            txtNXB.Clear();
            txtSoLuongTon.Clear();
            rbtDangHoatDong.Checked = false;
            rbtTamNgung.Checked = false;
            dtpNgayTao.Value = DateTime.Now;
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtTimKiem.Text.Trim();
            if (!string.IsNullOrEmpty(tuKhoa))
            {
                dgvDanhSachSach.DataSource = sachBUS.TimKiemSach(tuKhoa);
            }
            else
            {
                LoadDanhSach();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Sach s = new Sach
            {
                MaSach = txtMaSach.Text,
                TieuDe = txtTieuDe.Text,
                MaTheLoai = cboMaTheLoai.Text.Trim(),
                MaTacGia = cboMaTacGia.SelectedValue?.ToString(),
                NhaXuatBan = txtNXB.Text,
                SoLuongTon = int.TryParse(txtSoLuongTon.Text, out int sl) ? sl : 0,
                TrangThai = rbtDangHoatDong.Checked,
                NgayTao = dtpNgayTao.Value
            };

            try
            {
                sachBUS.ThemSach(s);
                MessageBox.Show("Thêm sách thành công!");
                LoadDanhSach();
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
                string maSach = txtMaSach.Text.Trim();
                if (string.IsNullOrEmpty(maSach)) return;

                sachBUS.XoaSach(maSach);
                LoadDanhSach();
                ResetForm();
                MessageBox.Show("Xóa sách thành công!");
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
                var sach = LayThongTinTuForm();
                sachBUS.CapNhatSach(sach);
                LoadDanhSach();
                ResetForm();
                MessageBox.Show("Cập nhật sách thành công!");
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

        private void frmSach_Load(object sender, EventArgs e)
        {
            LoadDanhSach();
            ResetForm();
            LoadTacGia();
            LoadTheLoai();
        }
        private Sach LayThongTinTuForm()
        {
            return new Sach
            {
                MaSach = txtMaSach.Text.Trim(),
                TieuDe = txtTieuDe.Text.Trim(),
                MaTheLoai = cboMaTheLoai.SelectedValue?.ToString() ?? "",
                MaTacGia = cboMaTacGia.SelectedValue?.ToString() ?? "",
                NhaXuatBan = txtNXB.Text.Trim(),
                SoLuongTon = int.TryParse(txtSoLuongTon.Text, out int soLuong) ? soLuong : 0,
                TrangThai = rbtDangHoatDong.Checked ? true : rbtTamNgung.Checked ? false : (bool?)null,
                NgayTao = dtpNgayTao.Value
            };
        }
        private void dgvDanhSachSach_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDanhSachSach.Rows[e.RowIndex];
                txtMaSach.Text = row.Cells["MaSach"].Value.ToString();
                txtTieuDe.Text = row.Cells["TieuDe"].Value.ToString();
                cboMaTheLoai.SelectedValue = row.Cells["MaTheLoai"].Value.ToString();
                cboMaTacGia.SelectedValue = row.Cells["MaTacGia"].Value.ToString();
                txtNXB.Text = row.Cells["NhaXuatBan"].Value.ToString();
                txtSoLuongTon.Text = row.Cells["SoLuongTon"].Value?.ToString();
                bool? trangThai = row.Cells["TrangThai"].Value as bool?;
                rbtDangHoatDong.Checked = trangThai == true;
                rbtTamNgung.Checked = trangThai == false;
                if (DateTime.TryParse(row.Cells["NgayTao"].Value?.ToString(), out DateTime ngay))
                {
                    dtpNgayTao.Value = ngay;
                }

                txtMaSach.Enabled = false;
            }
        }
    }
}
