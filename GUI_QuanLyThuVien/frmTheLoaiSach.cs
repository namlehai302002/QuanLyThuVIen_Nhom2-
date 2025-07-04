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
    public partial class frmTheLoaiSach : Form
    {
        BusTheLoaiSach bus = new BusTheLoaiSach();
        public frmTheLoaiSach()
        {
            InitializeComponent();
        }

        private void frmTheLoaiSach_Load(object sender, EventArgs e)
        {
            LoadData();
            ResetForm();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string ma = txtMaTheLoai.Text.Trim(); // đã được gán từ ResetForm
            string ten = txtTenTheLoai.Text.Trim();
            bool trangThai = rbtHoatDong.Checked;
            DateTime ngayTao = dtpNgayTao.Value;

            if (string.IsNullOrEmpty(ten))
            {
                MessageBox.Show("Vui lòng nhập tên thể loại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            TheLoaiSach tls = new TheLoaiSach
            {
                MaTheLoai = ma,
                TenTheLoai = ten,
                TrangThai = trangThai,
                NgayTao = ngayTao
            };

            string result = bus.Add(tls);
            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Thêm thể loại thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                ResetForm();
            }
            else
            {
                MessageBox.Show(result, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string ma = txtMaTheLoai.Text.Trim();

            if (string.IsNullOrEmpty(ma))
            {
                MessageBox.Show("Vui lòng chọn thể loại cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa thể loại này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string deleteResult = bus.Delete(ma);
                if (string.IsNullOrEmpty(deleteResult))
                {
                    MessageBox.Show("Xóa thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ResetForm();
                }
                else
                {
                    MessageBox.Show(deleteResult, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string ma = txtMaTheLoai.Text.Trim();
            string ten = txtTenTheLoai.Text.Trim();
            bool trangThai = rbtHoatDong.Checked;
            DateTime ngayTao = dtpNgayTao.Value;

            if (string.IsNullOrEmpty(ma) || string.IsNullOrEmpty(ten))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            TheLoaiSach tls = new TheLoaiSach
            {
                MaTheLoai = ma,
                TenTheLoai = ten,
                TrangThai = trangThai,
                NgayTao = ngayTao
            };

            string result = bus.Update(tls);
            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Cập nhật thể loại thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                ResetForm();
            }
            else
            {
                MessageBox.Show(result, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void LoadData()
        {
            dgvDanhSachTheLoaiSach.DataSource = bus.GetAll();
        }
        private void ResetForm()
        {
            txtMaTheLoai.Text = TaoMaTheLoaiTuDong();
            txtMaTheLoai.Enabled = false; // không cho sửa
            txtTenTheLoai.Text = "";
            rbtHoatDong.Checked = true;
            dtpNgayTao.Value = DateTime.Now;
            txtTimKiem.Text = "";
            dgvDanhSachTheLoaiSach.ClearSelection();
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtTimKiem.Text.Trim();
            dgvDanhSachTheLoaiSach.DataSource = bus.Search(tuKhoa);
        }

        private void dgvDanhSachTheLoaiSach_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvDanhSachTheLoaiSach.Rows.Count)
            {
                DataGridViewRow row = dgvDanhSachTheLoaiSach.Rows[e.RowIndex];

                txtMaTheLoai.Text = row.Cells["MaTheLoai"].Value?.ToString();
                txtMaTheLoai.Enabled = false; // KHÓA LẠI

                txtTenTheLoai.Text = row.Cells["TenTheLoai"].Value?.ToString();

                bool trangThai = false;
                bool.TryParse(row.Cells["TrangThai"].Value?.ToString(), out trangThai);
                rbtHoatDong.Checked = trangThai;
                rbtTamNgung.Checked = !trangThai;

                if (DateTime.TryParse(row.Cells["NgayTao"].Value?.ToString(), out DateTime ngayTao))
                {
                    dtpNgayTao.Value = ngayTao;
                }
                else
                {
                    dtpNgayTao.Value = DateTime.Now;
                }
            }
        }
        private string TaoMaTheLoaiTuDong()
        {
            var list = bus.GetAll();
            int max = 0;

            foreach (var item in list)
            {
                if (item.MaTheLoai != null && item.MaTheLoai.StartsWith("TL"))
                {
                    if (int.TryParse(item.MaTheLoai.Substring(2), out int so))
                    {
                        if (so > max) max = so;
                    }
                }
            }

            return "TL" + (max + 1).ToString("D3"); // VD: TL001
        }
    }
}
