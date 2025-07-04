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
    public partial class frmTrangThaiThanhToan : Form
    {
        BusTrangThaiThanhToan bus = new BusTrangThaiThanhToan();
        public frmTrangThaiThanhToan()
        {
            InitializeComponent();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string ma = GenerateNewMaTrangThai(); // Mã tự động
            string ten = txtTenTrangThai.Text.Trim();
            DateTime ngayTao = dtpNgayTao.Value;

            if (string.IsNullOrEmpty(ten))
            {
                MessageBox.Show("Vui lòng nhập tên trạng thái!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            TrangThaiThanhToan tttt = new TrangThaiThanhToan
            {
                MaTrangThai = ma,
                TenTrangThai = ten,
                NgayTao = ngayTao
            };

            string result = bus.Add(tttt);

            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show($"Thêm trạng thái thành công! Mã trạng thái là: {ma}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                ClearForm();
            }
            else
            {
                MessageBox.Show(result, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string ma = txtMaTrangThai.Text.Trim();

            if (string.IsNullOrEmpty(ma))
            {
                MessageBox.Show("Vui lòng chọn mã trạng thái để xoá!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xoá trạng thái này không?",
                                                  "Xác nhận xoá",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string deleteResult = bus.Delete(ma);

                if (string.IsNullOrEmpty(deleteResult))
                {
                    MessageBox.Show("Xoá trạng thái thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ClearForm();
                }
                else
                {
                    MessageBox.Show(deleteResult, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void btnSua_Click(object sender, EventArgs e)
        {
            string ma = txtMaTrangThai.Text.Trim();
            string ten = txtTenTrangThai.Text.Trim();
            DateTime ngayTao = dtpNgayTao.Value;

            if (string.IsNullOrEmpty(ma))
            {
                MessageBox.Show("Vui lòng chọn mã trạng thái để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(ten))
            {
                MessageBox.Show("Vui lòng nhập tên trạng thái!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            TrangThaiThanhToan tttt = new TrangThaiThanhToan
            {
                MaTrangThai = ma,
                TenTrangThai = ten,
                NgayTao = ngayTao
            };

            string result = bus.Update(tttt);

            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                ClearForm();
            }
            else
            {
                MessageBox.Show(result, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearForm();
            LoadData();
        }
        private void LoadData()
        {
            dgvTrangThai.DataSource = bus.GetAll();
            txtMaTrangThai.Text = GenerateNewMaTrangThai();
        }

        private void ClearForm()
        {
            txtMaTrangThai.Text = GenerateNewMaTrangThai();
            txtTenTrangThai.Clear();
            dtpNgayTao.Value = DateTime.Now;
            txtTimKiem.Clear();
        }
        private string GenerateNewMaTrangThai()
        {
            var all = bus.GetAll();
            int max = 0;

            foreach (var item in all)
            {
                if (item.MaTrangThai.StartsWith("TT"))
                {
                    string numPart = item.MaTrangThai.Substring(2);
                    if (int.TryParse(numPart, out int num))
                    {
                        if (num > max)
                            max = num;
                    }
                }
            }

            return $"TT{(max + 1).ToString("D3")}";
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                LoadData();
                return;
            }

            var ketQua = bus.Search(keyword);

            if (ketQua.Count == 0)
            {
                MessageBox.Show("Không tìm thấy trạng thái nào phù hợp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            dgvTrangThai.DataSource = ketQua;
        }

        private void dgvTrangThai_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvTrangThai.Rows[e.RowIndex];

                txtMaTrangThai.Text = row.Cells["MaTrangThai"].Value?.ToString();
                txtTenTrangThai.Text = row.Cells["TenTrangThai"].Value?.ToString();

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

        private void frmTrangThaiThanhToan_Load(object sender, EventArgs e)
        {
            LoadData();
            txtMaTrangThai.ReadOnly = true;
        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
