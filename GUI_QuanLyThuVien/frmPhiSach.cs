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
    public partial class frmPhiSach : Form
    {
        BUSPhiSach busPhiSach = new BUSPhiSach();
        public frmPhiSach()
        {
            InitializeComponent();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            var ps = new PhiSach
            {
                MaPhiSach = GenerateNextMaPhiSach(),
                MaSach = cboMaSach.Text,
                PhiMuon = decimal.Parse(txtPhiMuon.Text),
                PhiPhat = string.IsNullOrWhiteSpace(txtPhiPhat.Text) ? null : (decimal?)decimal.Parse(txtPhiPhat.Text),
                TrangThai = GetTrangThai(),
                NgayTao = dtpNgayTao.Value
            };

            if (busPhiSach.Add(ps))
            {
                MessageBox.Show("Thêm thành công!");
                LoadData();
                ResetForm();
            }
            else
            {
                MessageBox.Show("Thêm thất bại!");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (rbtDaThanhToan.Checked)
            {
                MessageBox.Show("Phiếu đã thanh toán không được phép chỉnh sửa!");
                return;
            }

            var ps = new PhiSach
            {
                MaPhiSach = txtMaPhiSach.Text,
                MaSach = cboMaSach.Text,
                PhiMuon = decimal.Parse(txtPhiMuon.Text),
                PhiPhat = string.IsNullOrWhiteSpace(txtPhiPhat.Text) ? null : (decimal?)decimal.Parse(txtPhiPhat.Text),
                TrangThai = GetTrangThai(),
                NgayTao = dtpNgayTao.Value
            };

            if (busPhiSach.Update(ps))
            {
                MessageBox.Show("Cập nhật thành công!");
                LoadData();
                ResetForm();
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại!");
            }
        }
        private string GenerateNextMaPhiSach()
        {
            var list = busPhiSach.GetAll();
            if (list.Count == 0)
                return "PS001";

            var maxMa = list.Select(p => p.MaPhiSach)
                           .Where(m => m.StartsWith("PS"))
                           .OrderByDescending(m => m)
                           .FirstOrDefault();

            int number = 0;
            if (maxMa != null)
            {
                int.TryParse(maxMa.Substring(2), out number);
            }

            number++;
            return "PS" + number.ToString("D3");
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            string ma = txtMaPhiSach.Text;
            if (busPhiSach.Delete(ma))
            {
                MessageBox.Show("Xóa thành công!");
                LoadData();
                ResetForm();
            }
            else
            {
                MessageBox.Show("Xóa thất bại!");
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ResetForm();
            LoadData();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string kw = txtTimKiem.Text.Trim();
            dgvDanhSachPhiSach.DataSource = busPhiSach.Search(kw);
        }

        private void dgvDanhSachPhiSach_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDanhSachPhiSach.Rows[e.RowIndex];
                txtMaPhiSach.Text = row.Cells["MaPhiSach"].Value.ToString();
                cboMaSach.Text = row.Cells["MaSach"].Value.ToString();
                txtPhiMuon.Text = row.Cells["PhiMuon"].Value.ToString();
                txtPhiPhat.Text = row.Cells["PhiPhat"].Value?.ToString();
                dtpNgayTao.Value = Convert.ToDateTime(row.Cells["NgayTao"].Value);

                bool trangThai = Convert.ToBoolean(row.Cells["TrangThai"].Value);
                rbtDaThanhToan.Checked = trangThai;
                rbtChuaThanhToan.Checked = !trangThai;
                if (trangThai)
                {
                    btnSua.Enabled = false;

                }
                else // Chưa thanh toán thì mở khóa bình thường
                {
                    btnSua.Enabled = true;
                }
            }
        }
        private void LoadData()
        {
            dgvDanhSachPhiSach.DataSource = busPhiSach.GetAll();
        }
        private void LoadComboBoxMaSach()
        {
            cboMaSach.Items.Clear();
            cboMaSach.Items.Add("S001");
            cboMaSach.Items.Add("S002");
            cboMaSach.Items.Add("S003");
            cboMaSach.Items.Add("S004");
            cboMaSach.Items.Add("S005");
        }

        private void ResetForm()
        {
            txtMaPhiSach.Text = GenerateNextMaPhiSach();
            txtMaPhiSach.ReadOnly = true;
            cboMaSach.SelectedIndex = -1;
            txtPhiMuon.Clear();
            txtPhiPhat.Clear();
            rbtDaThanhToan.Checked = false;
            rbtChuaThanhToan.Checked = false;
            dtpNgayTao.Value = DateTime.Now;
            txtTimKiem.Clear();



            btnSua.Enabled = false;
        }
        private bool GetTrangThai()
        {
            if (rbtDaThanhToan.Checked) return true;
            if (rbtChuaThanhToan.Checked) return false;
            return false;
        }
        private void frmPhiSach_Load(object sender, EventArgs e)
        {
            txtMaPhiSach.ReadOnly = true;
            LoadData();
            LoadComboBoxMaSach();
            ResetForm();
        }

        private void rbtDaThanhToan_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtDaThanhToan.Checked) rbtChuaThanhToan.Checked = false;
        }

        private void rbtChuaThanhToan_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtChuaThanhToan.Checked) rbtDaThanhToan.Checked = false;
        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
