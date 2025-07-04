using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_QuanLyThuVien;
using DTO_QuanLyThuVien;

namespace BLL_QuanLyThuVien
{
    public class BUSPhiSach
    {
        private DALPhiSach dal = new DALPhiSach();

        public List<PhiSach> GetAll() => dal.GetAll();

        public bool Add(PhiSach ps) => dal.Insert(ps);

        public bool Update(PhiSach ps) => dal.Update(ps);

        public bool Delete(string maPhiSach) => dal.Delete(maPhiSach);

        public List<PhiSach> Search(string kw) => dal.Search(kw);
    }
}

