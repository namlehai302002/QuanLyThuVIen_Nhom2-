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
    public class DALMuonTraSach
    {
        public List<MuonTraSach> SelectAll()
        {
            string sql = "SELECT * FROM MuonTraSach";
            return SelectBySql(sql, new List<object>());
        }

        public List<MuonTraSach> SelectBySql(string sql, List<object> args, CommandType cmdType = CommandType.Text)
        {
            List<MuonTraSach> list = new List<MuonTraSach>();
            using (SqlDataReader reader = DBUtil.Query(sql, args, cmdType))
            {
                while (reader.Read())
                {
                    list.Add(MapReaderToMuonTraSach(reader));
                }
            }
            return list;
        }

        public string Insert(MuonTraSach m)
        {
            string sql = @"INSERT INTO MuonTraSach (MaMuonTra, MaKhachHang, MaNhanVien, NgayMuon, NgayTra, MaTrangThai, NgayTao)
                           VALUES (@0, @1, @2, @3, @4, @5, @6)";
            List<object> args = new List<object>
            {
                m.MaMuonTra,
                m.MaKhachHang,
                m.MaNhanVien,
                m.NgayMuon.ToDateTime(TimeOnly.MinValue),
                m.NgayTra.ToDateTime(TimeOnly.MinValue),
                m.MaTrangThai,
                m.NgayTao
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

        public string Update(MuonTraSach m)
        {
            string sql = @"UPDATE MuonTraSach SET 
                            MaKhachHang = @0,
                            MaNhanVien = @1,
                            NgayMuon = @2,
                            NgayTra = @3,
                            MaTrangThai = @4,
                            NgayTao = @5
                           WHERE MaMuonTra = @6";
            List<object> args = new List<object>
            {
                m.MaKhachHang,
                m.MaNhanVien,
                m.NgayMuon.ToDateTime(TimeOnly.MinValue),
                m.NgayTra.ToDateTime(TimeOnly.MinValue),
                m.MaTrangThai,
                m.NgayTao,
                m.MaMuonTra
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

        public string Delete(string ma)
        {
            string sql = "DELETE FROM MuonTraSach WHERE MaMuonTra = @0";
            try
            {
                DBUtil.Update(sql, new List<object> { ma });
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }

        public MuonTraSach? GetById(string ma)
        {
            string sql = "SELECT * FROM MuonTraSach WHERE MaMuonTra = @0";
            var list = SelectBySql(sql, new List<object> { ma });
            return list.FirstOrDefault();
        }

        public List<MuonTraSach> Search(string keyword)
        {
            string sql = @"SELECT * FROM MuonTraSach
                           WHERE MaMuonTra LIKE @0 OR
                                 MaKhachHang LIKE @0 OR
                                 MaNhanVien LIKE @0";
            List<object> args = new List<object> { "%" + keyword + "%" };
            return SelectBySql(sql, args);
        }

        private MuonTraSach MapReaderToMuonTraSach(SqlDataReader reader)
        {
            return new MuonTraSach
            {
                MaMuonTra = reader["MaMuonTra"].ToString(),
                MaKhachHang = reader["MaKhachHang"].ToString(),
                MaNhanVien = reader["MaNhanVien"].ToString(),
                NgayMuon = DateOnly.FromDateTime(Convert.ToDateTime(reader["NgayMuon"])),
                NgayTra = DateOnly.FromDateTime(Convert.ToDateTime(reader["NgayTra"])),
                MaTrangThai = reader["MaTrangThai"].ToString(),
                NgayTao = Convert.ToDateTime(reader["NgayTao"])
            };
        }

        // Hàm lấy tất cả MaMuonTra dưới dạng List<string>
        public List<string> GetAllMaMuonTra()
        {
            List<string> listMa = new List<string>();
            string query = "SELECT MaMuonTra FROM MuonTraSach ORDER BY MaMuonTra";

            DataTable dt = DBUtil.GetDataTable(query, new List<object>());
            foreach (DataRow row in dt.Rows)
            {
                listMa.Add(row["MaMuonTra"].ToString());
            }
            return listMa;
        }
    }
}

