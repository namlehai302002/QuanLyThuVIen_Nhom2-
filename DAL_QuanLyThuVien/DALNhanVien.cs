using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_QUANLYTHUVIEN;
using DTO_QuanLyThuVien;
using Microsoft.Data.SqlClient;

namespace DAL_QuanLyThuVien
{
    public class DALNhanVien
    {
        private string connectionString;
        private List<object> thamSo;

        public NhanVien? getNhanVien1(string email, string password)
        {
            string sql = "SELECT * FROM NhanVien WHERE Email = @0 AND MatKhau = @1";
            List<object> thamSo = new List<object> { email, password };

            using (SqlDataReader reader = DBUtil.Query(sql, thamSo))
            {
                if (reader.HasRows && reader.Read())
                {
                    return MapReaderToNhanVien(reader);
                }
            }
            return null;
        }

        public List<NhanVien> SelectBySql(string sql, List<object> args, CommandType cmdType = CommandType.Text)
        {
            List<NhanVien> list = new List<NhanVien>();
            using (SqlDataReader reader = DBUtil.Query(sql, args))
            {
                while (reader.Read())
                {
                    list.Add(MapReaderToNhanVien(reader));
                }
            }
            return list;
        }

        public List<NhanVien> SelectAll()
        {
            string sql = "SELECT * FROM NhanVien";
            return SelectBySql(sql, new List<object>());
        }

        public string UpdateNhanVien(NhanVien nv)
        {
            string sql = @"UPDATE NhanVien 
                   SET Ten = @0,
                       Email = @1,
                       MatKhau = @2,
                       SoDienThoai = @3,
                       VaiTro = @4,
                       TrangThai = @5,
                       NgayTao = @6
                   WHERE MaNhanVien = @7";

            List<object> parameters = new List<object>
    {
        nv.Ten,
        nv.Email,
        nv.MatKhau,
        nv.SoDienThoai,
        nv.VaiTro,
        nv.TrangThai,
        nv.NgayTao,
        nv.MaNhanVien
    };

            try
            {
                DBUtil.Update(sql, parameters);
                return ""; // Không lỗi
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }

        public string DeleteNhanVien(string maNV)
        {
            try
            {
                string query = "DELETE FROM NhanVien WHERE MaNhanVien = @0";
                var parameters = new List<object> { maNV };
                DBUtil.Update(query, parameters);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }

        public void InsertNhanVien(NhanVien nv)
        {
            string sql = @"INSERT INTO NhanVien (MaNhanVien, Ten, Email, MatKhau, SoDienThoai, VaiTro, TrangThai, NgayTao)
                           VALUES (@0, @1, @2, @3, @4, @5, @6, @7)";

            List<object> parameters = new List<object>
            {
                nv.MaNhanVien,
                nv.Ten,
                nv.Email,
                nv.MatKhau,
                nv.SoDienThoai,
                nv.VaiTro,
                nv.TrangThai,
                nv.NgayTao
            };

            DBUtil.Update(sql, parameters);
        }

        public NhanVien? GetNhanVienByEmail(string email)
        {
            string sql = "SELECT * FROM NhanVien WHERE Email = @0";
            List<object> args = new List<object> { email };

            using (SqlDataReader reader = DBUtil.Query(sql, args))
            {
                if (reader.HasRows && reader.Read())
                {
                    return MapReaderToNhanVien(reader);
                }
            }

            return null;
        }

        private NhanVien MapReaderToNhanVien(SqlDataReader reader)
        {
            return new NhanVien
            {
                MaNhanVien = reader["MaNhanVien"].ToString(),
                Ten = reader["Ten"].ToString(),
                Email = reader["Email"].ToString(),
                MatKhau = reader["MatKhau"].ToString(),
                SoDienThoai = reader["SoDienThoai"].ToString(),
                VaiTro = Convert.ToBoolean(reader["VaiTro"]),
                TrangThai = Convert.ToBoolean(reader["TrangThai"]),
                NgayTao = reader["NgayTao"] != DBNull.Value ? Convert.ToDateTime(reader["NgayTao"]) : DateTime.MinValue
            };
        }

        public List<NhanVien> SearchNhanVien(string keyword)
        {
            string sql = @"SELECT * FROM NhanVien 
                   WHERE MaNhanVien LIKE @0 
                      OR Ten LIKE @0 
                      OR Email LIKE @0";

            List<object> parameters = new List<object> { "%" + keyword + "%" };
            return SelectBySql(sql, parameters);
        }
        public NhanVien? GetNhanVienByMa(string maNhanVien)
        {
            string sql = "SELECT * FROM NhanVien WHERE MaNhanVien = @0";
            List<object> args = new List<object> { maNhanVien };

            using (SqlDataReader reader = DBUtil.Query(sql, args))
            {
                if (reader.HasRows && reader.Read())
                {
                    // Dùng luôn hàm MapReaderToNhanVien để tránh lặp code
                    return MapReaderToNhanVien(reader);
                }
            }
            return null;
        }

    }
}
