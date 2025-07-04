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
using DTO_QuanLyThuVien;

namespace GUI_QuanLyThuVien
{
    public partial class frmKhachHang : Form
    {
        private BusKhachHang busKhachHang = new BusKhachHang();
        public frmKhachHang()
        {
            InitializeComponent();
        }

        private void LoadKhachHangToDGV()
        {
            dgvDanhSachKH.DataSource = busKhachHang.GetKhachHangList();
        }
        private void frmKhachHang_Load(object sender, EventArgs e)
        {
            txtMaKH.Enabled = false;
            LoadKhachHangToDGV();
            SinhMaKhachHangTuDong();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            KhachHang kh = new KhachHang
            {
                MaKhachHang = txtMaKH.Text.Trim(),
                TenKhachHang = txtTenKH.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                SoDienThoai = txtSDT.Text.Trim(),
                CCCD = txtCCCD.Text.Trim(),
                TrangThai = rbtDangHoatDong.Checked,
                NgayTao = dtpNgayTao.Value
            };

            string result = busKhachHang.AddKhachHang(kh);

            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Thêm khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadKhachHangToDGV(); // Load lại dữ liệu
                ClearInputFields();
            }
            else
            {
                MessageBox.Show(result, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ClearInputFields()
        {
            txtTenKH.Clear();
            txtEmail.Clear();
            txtSDT.Clear();
            txtCCCD.Clear();
            rbtDangHoatDong.Checked = true;
            rbtTamNgung.Checked = false;
            dtpNgayTao.Value = DateTime.Now;
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaKH.Text))
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maKH = txtMaKH.Text.Trim();

            DialogResult result = MessageBox.Show($"Bạn có chắc muốn xóa khách hàng \"{maKH}\" không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                string ketQua = busKhachHang.DeleteKhachHang(maKH);
                if (string.IsNullOrEmpty(ketQua))
                {
                    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadKhachHangToDGV(); // Tải lại danh sách
                    ClearInputFields(); // Hàm này sẽ xóa trống các textbox nếu bạn đã tạo
                }
                else
                {
                    MessageBox.Show(ketQua, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrWhiteSpace(txtMaKH.Text))
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tạo đối tượng KhachHang từ dữ liệu nhập
            KhachHang kh = new KhachHang
            {
                MaKhachHang = txtMaKH.Text.Trim(),
                TenKhachHang = txtTenKH.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                SoDienThoai = txtSDT.Text.Trim(),
                CCCD = txtCCCD.Text.Trim(),
                TrangThai = rbtDangHoatDong.Checked, // true nếu đang hoạt động
                NgayTao = dtpNgayTao.Value
            };

            // Gọi BUS để cập nhật
            string result = busKhachHang.UpdateKhachHang(kh);
            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadKhachHangToDGV(); // Cập nhật lại danh sách
            }
            else
            {
                MessageBox.Show(result, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearInputFields();
            SinhMaKhachHangTuDong();
            LoadKhachHangToDGV();
            txtTimKiem.Clear();
        }

        private void dgvDanhSachKH_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvDanhSachKH.Rows.Count)
            {
                DataGridViewRow row = dgvDanhSachKH.Rows[e.RowIndex];

                txtMaKH.Text = row.Cells["MaKhachHang"].Value?.ToString();
                txtTenKH.Text = row.Cells["TenKhachHang"].Value?.ToString();
                txtEmail.Text = row.Cells["Email"].Value?.ToString();
                txtSDT.Text = row.Cells["SoDienThoai"].Value?.ToString();
                txtCCCD.Text = row.Cells["CCCD"].Value?.ToString();

                // Xử lý RadioButton trạng thái
                bool trangThai = false;
                bool.TryParse(row.Cells["TrangThai"].Value?.ToString(), out trangThai);
                if (trangThai)
                {
                    rbtDangHoatDong.Checked = true;
                }
                else
                {
                    rbtTamNgung.Checked = true;
                }

                // Ngày tạo
                if (DateTime.TryParse(row.Cells["NgayTao"].Value?.ToString(), out DateTime ngayTao))
                {
                    dtpNgayTao.Value = ngayTao;
                }
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim();

            if (!string.IsNullOrEmpty(keyword))
            {
                dgvDanhSachKH.DataSource = busKhachHang.SearchKhachHang(keyword);
            }
            else
            {
                LoadKhachHangToDGV(); // Nếu không nhập gì thì hiển thị lại toàn bộ
            }
        }
        private void SinhMaKhachHangTuDong()
        {
            List<KhachHang> dsKH = busKhachHang.GetKhachHangList();
            int max = 0;

            foreach (var kh in dsKH)
            {
                // Giả sử mã có dạng "KH" + số, vd "KH001"
                string ma = kh.MaKhachHang;

                if (ma.StartsWith("KH"))
                {
                    string soStr = ma.Substring(2); // lấy phần số
                    if (int.TryParse(soStr, out int so))
                    {
                        if (so > max)
                            max = so;
                    }
                }
            }

            // Tăng lên 1 và tạo mã mới với 3 chữ số
            string maMoi = "KH" + (max + 1).ToString("D3");

            txtMaKH.Text = maMoi;
        }
    }
}
