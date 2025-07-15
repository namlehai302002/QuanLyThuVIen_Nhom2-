using System;
using System.Linq;
using System.Windows.Forms;
using DTO_QuanLyThuVien;
using BLL_QuanLyThuVien;

namespace GUI_QuanLyThuVien
{
    public partial class frmChiTietXuatSach : Form
    {
        private readonly BUSChiTietXuatSach busCTX = new BUSChiTietXuatSach();
        private readonly BUSXuatSach busXuat = new BUSXuatSach(); // load cboMaXuat
        private readonly BUSSach busSach = new BUSSach(); // load cboMaSach

        private string maXuatDuocTruyen = "";

        public frmChiTietXuatSach() // Mở form độc lập
        {
            InitializeComponent();
            LoadData();
        }

        public frmChiTietXuatSach(string maXuat) // Mở từ frmXuatSach
        {
            InitializeComponent();
            maXuatDuocTruyen = maXuat;
            LoadDataTheoMaXuat();
            dgvCTXuatSach.ColumnHeadersHeight = 40;
        }

        private void LoadData()
        {
            dgvCTXuatSach.DataSource = busCTX.LayTatCa();

            cboMaXuat.DataSource = busXuat.LayTatCaXuatSach();
            cboMaXuat.DisplayMember = "MaXuat";
            cboMaXuat.ValueMember = "MaXuat";

            cboMaSach.DataSource = busSach.LayTatCaSach();
            cboMaSach.DisplayMember = "MaSach";
            cboMaSach.ValueMember = "MaSach";

            txtMaCTXuat.Text = busCTX.TaoMaTuDong();

            dgvCTXuatSach.ColumnHeadersHeight = 40;
        }

        private void LoadDataTheoMaXuat()
        {
            var ds = busCTX.LayTatCa()
                           .Where(ct => ct.MaXuat == maXuatDuocTruyen)
                           .ToList();

            dgvCTXuatSach.DataSource = ds;

            cboMaXuat.DataSource = busXuat.LayTatCaXuatSach();
            cboMaXuat.DisplayMember = "MaXuat";
            cboMaXuat.ValueMember = "MaXuat";

            cboMaSach.DataSource = busSach.LayTatCaSach();
            cboMaSach.DisplayMember = "MaSach";
            cboMaSach.ValueMember = "MaSach";

            txtMaCTXuat.Text = busCTX.TaoMaTuDong();

            if (!string.IsNullOrEmpty(maXuatDuocTruyen))
            {
                cboMaXuat.SelectedValue = maXuatDuocTruyen;
                cboMaXuat.Enabled = false;
            }
        }

        private ChiTietXuatSach LayDuLieuTuForm()
        {
            return new ChiTietXuatSach
            {
                MaChiTietXuat = txtMaCTXuat.Text.Trim(),
                MaXuat = cboMaXuat.SelectedValue.ToString(),
                MaSach = cboMaSach.SelectedValue.ToString(),
                SoLuong = int.TryParse(txtSoLuong.Text, out int sl) ? sl : 0
            };
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                var ct = LayDuLieuTuForm();
                busCTX.Them(ct);
                MessageBox.Show("Thêm thành công!");
                LoadDataTheoMaXuat();
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
                busCTX.CapNhat(ct);
                MessageBox.Show("Cập nhật thành công!");
                LoadDataTheoMaXuat();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi sửa: " + ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                string ma = txtMaCTXuat.Text.Trim();
                busCTX.Xoa(ma);
                MessageBox.Show("Xóa thành công!");
                LoadDataTheoMaXuat();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa: " + ex.Message);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaCTXuat.Text = busCTX.TaoMaTuDong();
            txtSoLuong.Text = "";
            cboMaSach.SelectedIndex = 0;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtTimKiem.Text.Trim().ToLower();
            var kq = busCTX.LayTatCa()
                           .Where(x => x.MaChiTietXuat.ToLower().Contains(tuKhoa) || x.MaXuat.ToLower().Contains(tuKhoa))
                           .ToList();
            dgvCTXuatSach.DataSource = kq;
        }

        private void dgvCTXuatSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtMaCTXuat.Text = dgvCTXuatSach.Rows[e.RowIndex].Cells["MaChiTietXuat"].Value.ToString();
                cboMaXuat.SelectedValue = dgvCTXuatSach.Rows[e.RowIndex].Cells["MaXuat"].Value.ToString();
                cboMaSach.SelectedValue = dgvCTXuatSach.Rows[e.RowIndex].Cells["MaSach"].Value.ToString();
                txtSoLuong.Text = dgvCTXuatSach.Rows[e.RowIndex].Cells["SoLuong"].Value.ToString();
            }
        }
    }
}
