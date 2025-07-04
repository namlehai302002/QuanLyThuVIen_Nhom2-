using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_QUANLYTHUVIEN;
using DTO_QuanLyThuVien;
using Microsoft.Data.SqlClient;

namespace DAL_QuanLyThuVien
{
    public class DALSach
    {
        public List<Sach> GetAllSach()
        {
            string sql = "SELECT * FROM Sach";
            var list = new List<Sach>();
            using var reader = DBUtil.Query(sql, new List<object>());
            while (reader.Read())
            {
                var sach = new Sach
                {
                    MaSach = reader["MaSach"].ToString(),
                    TieuDe = reader["TieuDe"].ToString(),
                    MaTheLoai = reader["MaTheLoai"].ToString(),
                    MaTacGia = reader["MaTacGia"].ToString(),
                    NhaXuatBan = reader["NhaXuatBan"].ToString(),
                    SoLuongTon = reader["SoLuongTon"] != DBNull.Value ? Convert.ToInt32(reader["SoLuongTon"]) : (int?)null,
                    TrangThai = reader["TrangThai"] != DBNull.Value ? Convert.ToBoolean(reader["TrangThai"]) : (bool?)null,
                    NgayTao = reader["NgayTao"] != DBNull.Value ? Convert.ToDateTime(reader["NgayTao"]) : (DateTime?)null
                };
                list.Add(sach);
            }
            return list;
        }


        public void InsertSach(Sach s)
        {
            string sql = @"INSERT INTO SACH VALUES (@0, @1, @2, @3, @4, @5, @6, @7)";
            List<object> args = new List<object>
    {
        s.MaSach,
        s.TieuDe,
        s.MaTheLoai,
        s.MaTacGia,
        s.NhaXuatBan,
        s.SoLuongTon,
        s.TrangThai,
        s.NgayTao
    };
            DBUtil.Update(sql, args);
        }


        public void UpdateSach(Sach sach)
        {
            string sql = @"UPDATE Sach SET TieuDe = @0, MaTheLoai = @1, MaTacGia = @2,
                           NhaXuatBan = @3, SoLuongTon = @4, TrangThai = @5, NgayTao = @6
                           WHERE MaSach = @7";
            var args = new List<object>
            {
                sach.TieuDe,
                sach.MaTheLoai,
                sach.MaTacGia,
                sach.NhaXuatBan,
                sach.SoLuongTon,
                sach.TrangThai,
                sach.NgayTao,
                sach.MaSach
            };
            DBUtil.Update(sql, args);
        }







        public int DeleteSach(string maSach)
        {
            string sql = "DELETE FROM Sach WHERE MaSach = @0";
            return DBUtil.Update(sql, new List<object> { maSach });
        }


        public Sach GetSachByMa(string maSach)
        {
            string sql = "SELECT * FROM Sach WHERE MaSach = @0";
            List<object> args = new List<object> { maSach };

            using (SqlDataReader reader = DBUtil.Query(sql, args))
            {
                if (reader.Read())
                {
                    return new Sach
                    {
                        MaSach = reader["MaSach"].ToString(),
                        TieuDe = reader["TieuDe"].ToString(),
                        MaTheLoai = reader["MaTheLoai"].ToString(),
                        MaTacGia = reader["MaTacGia"].ToString(),
                        NhaXuatBan = reader["NhaXuatBan"].ToString(),
                        SoLuongTon = reader["SoLuongTon"] != DBNull.Value ? Convert.ToInt32(reader["SoLuongTon"]) : (int?)null,
                        TrangThai = reader["TrangThai"] != DBNull.Value ? Convert.ToBoolean(reader["TrangThai"]) : (bool?)null,
                        NgayTao = reader["NgayTao"] != DBNull.Value ? Convert.ToDateTime(reader["NgayTao"]) : (DateTime?)null
                    };
                }
            }

            return null;
        }










        public List<Sach> TimKiemSach(string tuKhoa)
        {
            string sql = @"SELECT * FROM SACH 
                   WHERE MaSach LIKE @0 OR TieuDe LIKE @1";

            List<object> args = new List<object> { "%" + tuKhoa + "%", "%" + tuKhoa + "%" };
            SqlDataReader reader = DBUtil.Query(sql, args);

            List<Sach> list = new List<Sach>();
            while (reader.Read())
            {
                list.Add(new Sach
                {
                    MaSach = reader["MaSach"].ToString(),
                    TieuDe = reader["TieuDe"].ToString(),
                    MaTheLoai = reader["MaTheLoai"].ToString(),
                    MaTacGia = reader["MaTacGia"].ToString(),
                    NhaXuatBan = reader["NhaXuatBan"].ToString(),
                    SoLuongTon = reader["SoLuongTon"] as int?,
                    TrangThai = reader["TrangThai"] as bool?,
                    NgayTao = reader["NgayTao"] as DateTime?
                });
            }
            reader.Close();
            return list;
        }


    }
}
