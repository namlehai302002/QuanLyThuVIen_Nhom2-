using System;
using System.Linq;
using System.Windows.Forms;
using DTO_QuanLyThuVien;
using BLL_QuanLyThuVien;

namespace GUI_QuanLyThuVien
{
    public partial class frmChiTietNhapSach : Form
    {
        private readonly BusChiTietNhapSach busCT = new BusChiTietNhapSach();
        private readonly BusNhapSach busNhap = new BusNhapSach(); // dùng để load cboMaNhap
        private readonly BUSSach busSach = new BUSSach(); // dùng để load cboMaSach

        public frmChiTietNhapSach()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            dgvCTNhapSach.DataSource = busCT.LayTatCa();
            cboMaNhap.DataSource = busNhap.LayTatCaNhapSach();
            cboMaNhap.DisplayMember = "MaNhap";
            cboMaNhap.ValueMember = "MaNhap";

            cboMaSach.DataSource = busSach.LayTatCaSach();
            cboMaSach.DisplayMember = "MaSach";
            cboMaSach.ValueMember = "MaSach";

            txtMaCTNhap.Text = busCT.TaoMaTuDong();
        }

        private ChiTietNhapSach LayDuLieuTuForm()
        {
            return new ChiTietNhapSach
            {
                MaChiTietNhap = txtMaCTNhap.Text.Trim(),
                MaNhap = cboMaNhap.SelectedValue.ToString(),
                MaSach = cboMaSach.SelectedValue.ToString(),
                SoLuong = int.TryParse(txtSoLuong.Text, out int sl) ? sl : 0,
                DonGia = decimal.TryParse(txtDonGia.Text, out decimal dg) ? dg : 0
            };
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                var ct = LayDuLieuTuForm();
                busCT.Them(ct);
                MessageBox.Show("Thêm thành công!");
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm: " + ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                var ct = LayDuLieuTuForm();
                busCT.CapNhat(ct);
                MessageBox.Show("Cập nhật thành công!");
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi sửa: " + ex.Message);
            }
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                string ma = txtMaCTNhap.Text.Trim();
                busCT.Xoa(ma);
                MessageBox.Show("Xóa thành công!");
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa: " + ex.Message);
            }
        }

        private void BtnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaCTNhap.Text = busCT.TaoMaTuDong();
            txtSoLuong.Text = "";
            txtDonGia.Text = "";
            cboMaNhap.SelectedIndex = 0;
            cboMaSach.SelectedIndex = 0;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tuKhoa = guna2TextBox1.Text.Trim().ToLower();
            var ketQua = busCT.LayTatCa()
                              .Where(ct => ct.MaChiTietNhap.ToLower().Contains(tuKhoa) || ct.MaNhap.ToLower().Contains(tuKhoa))
                              .ToList();
            dgvCTNhapSach.DataSource = ketQua;
        }

        private void dgvCTNhapSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtMaCTNhap.Text = dgvCTNhapSach.Rows[e.RowIndex].Cells["MaChiTietNhap"].Value.ToString();
                cboMaNhap.SelectedValue = dgvCTNhapSach.Rows[e.RowIndex].Cells["MaNhap"].Value.ToString();
                cboMaSach.SelectedValue = dgvCTNhapSach.Rows[e.RowIndex].Cells["MaSach"].Value.ToString();
                txtSoLuong.Text = dgvCTNhapSach.Rows[e.RowIndex].Cells["SoLuong"].Value.ToString();
                txtDonGia.Text = dgvCTNhapSach.Rows[e.RowIndex].Cells["DonGia"].Value.ToString();
            }
        }
    }
}
