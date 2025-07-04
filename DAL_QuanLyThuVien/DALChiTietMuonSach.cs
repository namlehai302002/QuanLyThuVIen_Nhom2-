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
    public class DALChiTietMuonSach
    {
        private string connectionString = DBUtil.connString;

        public List<ChiTietMuonSach> GetChiTietByMaMuonTra(string maMuonTra)
        {
            List<ChiTietMuonSach> list = new List<ChiTietMuonSach>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM ChiTietMuonSach WHERE MaMuonTra = @MaMuonTra";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaMuonTra", maMuonTra);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new ChiTietMuonSach
                    {
                        MaChiTiet = reader["MaChiTiet"].ToString(),
                        MaMuonTra = reader["MaMuonTra"].ToString(),
                        MaSach = reader["MaSach"].ToString(),
                        SoLuong = Convert.ToInt32(reader["SoLuong"]),
                        NgayTao = (reader["NgayTao"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(reader["NgayTao"])
                    });
                }
            }
            return list;
        }

        public bool Insert(ChiTietMuonSach ct)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO ChiTietMuonSach (MaChiTiet, MaMuonTra, MaSach, SoLuong, NgayTao)
                                 VALUES (@MaChiTiet, @MaMuonTra, @MaSach, @SoLuong, @NgayTao)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaChiTiet", ct.MaChiTiet);
                cmd.Parameters.AddWithValue("@MaMuonTra", ct.MaMuonTra);
                cmd.Parameters.AddWithValue("@MaSach", ct.MaSach);
                cmd.Parameters.AddWithValue("@SoLuong", ct.SoLuong);
                cmd.Parameters.AddWithValue("@NgayTao", ct.NgayTao);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Update(ChiTietMuonSach ct)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"UPDATE ChiTietMuonSach SET MaMuonTra = @MaMuonTra, MaSach = @MaSach,
                                 SoLuong = @SoLuong, NgayTao = @NgayTao
                                 WHERE MaChiTiet = @MaChiTiet";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaChiTiet", ct.MaChiTiet);
                cmd.Parameters.AddWithValue("@MaMuonTra", ct.MaMuonTra);
                cmd.Parameters.AddWithValue("@MaSach", ct.MaSach);
                cmd.Parameters.AddWithValue("@SoLuong", ct.SoLuong);
                cmd.Parameters.AddWithValue("@NgayTao", ct.NgayTao);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Delete(string maChiTiet)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM ChiTietMuonSach WHERE MaChiTiet = @MaChiTiet";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaChiTiet", maChiTiet);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}

