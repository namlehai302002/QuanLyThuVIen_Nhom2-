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
    public class DALTheLoaiSach
    {
        public List<TheLoaiSach> SelectAll()
        {
            string sql = "SELECT * FROM TheLoaiSach";
            return SelectBySql(sql, new List<object>());
        }

        public TheLoaiSach? GetById(string maTheLoai)
        {
            string sql = "SELECT * FROM TheLoaiSach WHERE MaTheLoai = @0";
            var args = new List<object> { maTheLoai };

            using (SqlDataReader reader = DBUtil.Query(sql, args))
            {
                if (reader.HasRows && reader.Read())
                {
                    return MapReaderToTheLoai(reader);
                }
            }
            return null;
        }

        public List<TheLoaiSach> SelectBySql(string sql, List<object> args)
        {
            List<TheLoaiSach> list = new List<TheLoaiSach>();
            using (SqlDataReader reader = DBUtil.Query(sql, args))
            {
                while (reader.Read())
                {
                    list.Add(MapReaderToTheLoai(reader));
                }
            }
            return list;
        }

        public void Insert(TheLoaiSach tls)
        {
            string sql = @"INSERT INTO TheLoaiSach (MaTheLoai, TenTheLoai, TrangThai, NgayTao)
                           VALUES (@0, @1, @2, @3)";
            var args = new List<object>
            {
                tls.MaTheLoai,
                tls.TenTheLoai,
                tls.TrangThai,
                tls.NgayTao
            };
            DBUtil.Update(sql, args);
        }

        public string Update(TheLoaiSach tls)
        {
            string sql = @"UPDATE TheLoaiSach 
                           SET TenTheLoai = @0, 
                               TrangThai = @1, 
                               NgayTao = @2
                           WHERE MaTheLoai = @3";

            var args = new List<object>
            {
                tls.TenTheLoai,
                tls.TrangThai,
                tls.NgayTao,
                tls.MaTheLoai
            };

            try
            {
                DBUtil.Update(sql, args);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }

        public string Delete(string maTheLoai)
        {
            string sql = "DELETE FROM TheLoaiSach WHERE MaTheLoai = @0";
            var args = new List<object> { maTheLoai };

            try
            {
                DBUtil.Update(sql, args);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }

        private TheLoaiSach MapReaderToTheLoai(SqlDataReader reader)
        {
            return new TheLoaiSach
            {
                MaTheLoai = reader["MaTheLoai"].ToString(),
                TenTheLoai = reader["TenTheLoai"].ToString(),
                TrangThai = reader["TrangThai"] != DBNull.Value ? Convert.ToBoolean(reader["TrangThai"]) : (bool?)null,
                NgayTao = reader["NgayTao"] != DBNull.Value ? Convert.ToDateTime(reader["NgayTao"]) : (DateTime?)null
            };
        }
    }
}

