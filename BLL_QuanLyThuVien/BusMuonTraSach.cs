using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_QuanLyThuVien;
using DTO_QuanLyThuVien;

namespace BLL_QuanLyThuVien
{
    public class BusMuonTraSach
    {
        private DALMuonTraSach dal = new DALMuonTraSach();

        public List<MuonTraSach> GetAll()
        {
            return dal.SelectAll();
        }

        public MuonTraSach? GetById(string ma)
        {
            return dal.GetById(ma);
        }

        public string Add(MuonTraSach m)
        {
            if (string.IsNullOrWhiteSpace(m.MaMuonTra))
                return "Mã mượn trả không được để trống.";

            return dal.Insert(m);
        }

        public string Update(MuonTraSach m)
        {
            if (string.IsNullOrWhiteSpace(m.MaMuonTra))
                return "Mã mượn trả không hợp lệ.";

            return dal.Update(m);
        }

        public string Delete(string ma)
        {
            return dal.Delete(ma);
        }

        public List<MuonTraSach> Search(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return GetAll();

            return dal.Search(keyword);
        }
        public string TaoMaMuonTraMoi()
        {
            var listMa = dal.GetAllMaMuonTra();

            if (listMa == null || listMa.Count == 0)
                return "MT001";

            int max = 0;
            foreach (var ma in listMa)
            {
                if (ma.StartsWith("MT") && int.TryParse(ma.Substring(2), out int num))
                {
                    if (num > max)
                        max = num;
                }
            }

            return "MT" + (max + 1).ToString("D3");
        }

    }
}

