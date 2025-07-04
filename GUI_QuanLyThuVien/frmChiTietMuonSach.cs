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
    public partial class frmChiTietMuonSach : Form
    {
        private string maMuonTra;
        private BindingList<ChiTietMuonSach> dsChiTiet = new BindingList<ChiTietMuonSach>();

        private BusChiTietMuonSach busChiTietMuonSach = new BusChiTietMuonSach();
        private BUSSach busSach = new BUSSach();

        public frmChiTietMuonSach(string maMuonTra)
        {
            InitializeComponent();
            this.maMuonTra = maMuonTra;
            this.Load += frmChiTietMuonTraSach_Load;
        }
        private void frmChiTietMuonTraSach_Load(object sender, EventArgs e)
        {
            txtMaMuonTra.Text = maMuonTra;
            LoadChiTietMuonTra();
            LoadSach();
            dgrChiTiet.DataSource = dsChiTiet;
        }
        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void LoadChiTietMuonTra()
        {
            var chiTietList = busChiTietMuonSach.GetChiTietByMaMuonTra(maMuonTra);
            dsChiTiet.Clear();
            if (chiTietList != null)
            {
                foreach (var ct in chiTietList)
                {
                    dsChiTiet.Add(ct);
                }
            }
        }

        private void LoadSach()
        {
            var sachList = busSach.LayTatCaSach();
            dgrSach.DataSource = sachList;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (dgrSach.CurrentRow != null)
            {
                string maSach = dgrSach.CurrentRow.Cells["MaSach"].Value.ToString();

                // Hộp thoại nhập số lượng
                string input = Microsoft.VisualBasic.Interaction.InputBox("Nhập số lượng sách muốn mượn:", "Nhập số lượng", "1");
                if (!int.TryParse(input, out int soLuong) || soLuong <= 0)
                {
                    MessageBox.Show("Số lượng không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Kiểm tra nếu sách đã có trong danh sách mượn
                var chiTietDaCo = dsChiTiet.FirstOrDefault(c => c.MaSach == maSach);
                if (chiTietDaCo != null)
                {
                    // Tăng số lượng
                    chiTietDaCo.SoLuong += soLuong;
                }
                else
                {
                    // Thêm mới nếu chưa có
                    dsChiTiet.Add(new ChiTietMuonSach
                    {
                        MaChiTiet = "CT" + (dsChiTiet.Count + 1).ToString("000"),
                        MaMuonTra = maMuonTra,
                        MaSach = maSach,
                        SoLuong = soLuong,
                        NgayTao = DateTime.Now,
                        MatKhau = "" // Nếu không dùng thì bỏ
                    });
                }

                dgrChiTiet.Refresh(); // Cập nhật lại hiển thị
                TinhTien();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgrChiTiet.CurrentRow != null)
            {
                ChiTietMuonSach item = (ChiTietMuonSach)dgrChiTiet.CurrentRow.DataBoundItem;

                DialogResult result = MessageBox.Show($"Bạn có chắc muốn xóa sách [{item.MaSach}] khỏi danh sách mượn không?",
                                                      "Xác nhận xóa",
                                                      MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    dsChiTiet.Remove(item);
                    TinhTien();
                }
            }
        }
        private void TinhTien()
        {
            decimal tongTien = 10000 * dsChiTiet.Count;
            decimal dichVu = string.IsNullOrWhiteSpace(txtDichVu.Text) ? 0 : decimal.Parse(txtDichVu.Text);
            decimal phanTramGiam = string.IsNullOrWhiteSpace(txtPhanTram.Text) ? 0 : decimal.Parse(txtPhanTram.Text);

            decimal giamGia = (tongTien * phanTramGiam) / 100;
            decimal thanhTien = tongTien + dichVu - giamGia;

            txtTong.Text = tongTien.ToString("N0");
            txtGiamGia.Text = giamGia.ToString("N0");
            txtThanhTien.Text = thanhTien.ToString("N0");
        }
    }
}
