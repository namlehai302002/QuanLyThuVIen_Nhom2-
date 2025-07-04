using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_QuanLyThuVien;
using DTO_QuanLyThuVien;

namespace BLL_QuanLyThuVien
{
    public class BusTacGia
    {
        DALTacGia dalTacGia = new DALTacGia();

        public List<TacGia> GetAllTacGia()
        {
            return dalTacGia.SelectAll();
        }

        public string AddTacGia(TacGia tg)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(tg.MaTacGia))
                    return "Mã tác giả không được để trống.";

                dalTacGia.InsertTacGia(tg);
                return "";
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }

        public string UpdateTacGia(TacGia tg)
        {
            try
            {
                return dalTacGia.UpdateTacGia(tg);
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }

        public string DeleteTacGia(string maTG)
        {
            try
            {
                return dalTacGia.DeleteTacGia(maTG);
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }

        public List<TacGia> SearchTacGia(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword)) return GetAllTacGia();

            var danhSach = GetAllTacGia();

            return danhSach.Where(tg =>
            (tg.MaTacGia ?? "").Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
            (tg.TenTacGia ?? "").Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
            (tg.QuocTich ?? "").Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
            tg.TrangThai.ToString().Contains(keyword, StringComparison.OrdinalIgnoreCase)
            ).ToList();
        }
    }
}

