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
    public partial class frmTacGia : Form
    {
        private BusTacGia busTacGia = new BusTacGia();

        public bool? TrangThai { get; private set; }
        public frmTacGia()
        {
            InitializeComponent();
        }

        private void frmTacGia_Load(object sender, EventArgs e)
        {
            LoadDanhSachTacGia();
            btnSua.Enabled = false;
            txtMaTG.Enabled = false;
            txtMaTG.Text = TaoMaTacGiaTuDong();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string maTG = txtMaTG.Text.Trim();
            string tenTG = txtTenTG.Text.Trim();

            if (string.IsNullOrEmpty(maTG) || string.IsNullOrEmpty(tenTG))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }

            TacGia tg = new TacGia
            {
                MaTacGia = maTG,
                TenTacGia = tenTG,
                QuocTich = txtQuocTich.Text.Trim(),
                TrangThai = rbtDangHoatDong.Checked ? true : (rbtTamNgung.Checked ? false : (bool?)null),
                NgayTao = dtpNgayTao.Value
            };

            string result = busTacGia.AddTacGia(tg);
            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Thêm tác giả thành công.");
                ClearForm();
                LoadDanhSachTacGia();
            }
            else
            {
                MessageBox.Show(result);
            }
        }
        private void LoadDanhSachTacGia()
        {
            dgvDanhSachTG.DataSource = busTacGia.GetAllTacGia();
            dgvDanhSachTG.Columns["MaTacGia"].HeaderText = "Mã Tác Giả";
            dgvDanhSachTG.Columns["TenTacGia"].HeaderText = "Tên Tác Giả";
            dgvDanhSachTG.Columns["QuocTich"].HeaderText = "Quốc Tịch";
            dgvDanhSachTG.Columns["TrangThai"].HeaderText = "Trạng Thái";
            dgvDanhSachTG.Columns["NgayTao"].HeaderText = "Ngày Tạo";
            dgvDanhSachTG.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maTG = txtMaTG.Text.Trim();

            if (string.IsNullOrEmpty(maTG))
            {
                MessageBox.Show("Vui lòng chọn tác giả để xóa.");
                return;
            }

            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa tác giả này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                string result = busTacGia.DeleteTacGia(maTG);
                if (string.IsNullOrEmpty(result))
                {
                    MessageBox.Show("Xóa tác giả thành công.");
                    ClearForm();
                    LoadDanhSachTacGia();
                }
                else
                {
                    MessageBox.Show($"Xóa thất bại: {result}");
                }
            }
        }
        private string TaoMaTacGiaTuDong()
        {
            var list = busTacGia.GetAllTacGia();
            int max = 0;

            foreach (var item in list)
            {
                if (!string.IsNullOrEmpty(item.MaTacGia) && item.MaTacGia.StartsWith("TG"))
                {
                    if (int.TryParse(item.MaTacGia.Substring(2), out int so))
                    {
                        if (so > max) max = so;
                    }
                }
            }

            return "TG" + (max + 1).ToString("D3"); // VD: TG001, TG002,...
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            string maTG = txtMaTG.Text.Trim();
            string tenTG = txtTenTG.Text.Trim();

            if (string.IsNullOrEmpty(maTG) || string.IsNullOrEmpty(tenTG))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }

            TacGia tg = new TacGia
            {
                MaTacGia = maTG,
                TenTacGia = tenTG,
                QuocTich = txtQuocTich.Text.Trim(),
                TrangThai = rbtDangHoatDong.Checked ? true : (rbtTamNgung.Checked ? false : (bool?)null),
                NgayTao = dtpNgayTao.Value
            };

            string result = busTacGia.UpdateTacGia(tg);
            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Cập nhật tác giả thành công.");
                ClearForm();
                LoadDanhSachTacGia();
            }
            else
            {
                MessageBox.Show(result);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearForm();
            LoadDanhSachTacGia();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                LoadDanhSachTacGia();
                return;
            }

            var result = busTacGia.SearchTacGia(keyword);

            dgvDanhSachTG.DataSource = null;
            dgvDanhSachTG.DataSource = result;
            dgvDanhSachTG.Columns["MaTacGia"].HeaderText = "Mã Tác Giả";
            dgvDanhSachTG.Columns["TenTacGia"].HeaderText = "Tên Tác Giả";
            dgvDanhSachTG.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void ClearForm()
        {
            txtMaTG.Text = TaoMaTacGiaTuDong();
            txtTenTG.Clear();
            txtQuocTich.Clear();
            rbtDangHoatDong.Checked = false;
            rbtTamNgung.Checked = false;
            dtpNgayTao.Value = DateTime.Now;
            txtTimKiem.Clear();
            btnThem.Enabled = true;
            btnSua.Enabled = false;
        }
        private void dgvDanhSachTG_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDanhSachTG.Rows[e.RowIndex];
                txtMaTG.Text = row.Cells["MaTacGia"].Value.ToString();
                txtTenTG.Text = row.Cells["TenTacGia"].Value.ToString();
                txtQuocTich.Text = row.Cells["QuocTich"].Value?.ToString();

                bool? trangThai = row.Cells["TrangThai"].Value != DBNull.Value ? (bool?)Convert.ToBoolean(row.Cells["TrangThai"].Value) : null;
                if (trangThai.HasValue)
                {
                    rbtDangHoatDong.Checked = trangThai.Value;
                    rbtTamNgung.Checked = !trangThai.Value;
                }
                else
                {
                    rbtDangHoatDong.Checked = false;
                    rbtTamNgung.Checked = false;
                }

                dtpNgayTao.Value = Convert.ToDateTime(row.Cells["NgayTao"].Value);

                btnSua.Enabled = true;
                btnThem.Enabled = false;
            }
        }
    }
}
