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
    public class DALPhiSach
    {
        private string connectionString = DBUtil.connString;



        public List<PhiSach> GetAll()
        {
            List<PhiSach> list = new List<PhiSach>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM PhiSach";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new PhiSach
                    {
                        MaPhiSach = reader["MaPhiSach"].ToString(),
                        MaSach = reader["MaSach"].ToString(),
                        PhiMuon = Convert.ToDecimal(reader["PhiMuon"]),
                        PhiPhat = reader["PhiPhat"] as decimal?,
                        TrangThai = reader["TrangThai"] as bool?,
                        NgayTao = reader["NgayTao"] as DateTime?
                    });
                }
            }
            return list;
        }



        public bool Insert(PhiSach ps)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO PhiSach (MaPhiSach, MaSach, PhiMuon, PhiPhat, TrangThai, NgayTao)
                                 VALUES (@MaPhiSach, @MaSach, @PhiMuon, @PhiPhat, @TrangThai, @NgayTao)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaPhiSach", ps.MaPhiSach);
                cmd.Parameters.AddWithValue("@MaSach", ps.MaSach);
                cmd.Parameters.AddWithValue("@PhiMuon", ps.PhiMuon);
                cmd.Parameters.AddWithValue("@PhiPhat", (object)ps.PhiPhat ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@TrangThai", (object)ps.TrangThai ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@NgayTao", (object)ps.NgayTao ?? DBNull.Value);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }






        public bool Update(PhiSach ps)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"UPDATE PhiSach SET MaSach = @MaSach, PhiMuon = @PhiMuon,
                                 PhiPhat = @PhiPhat, TrangThai = @TrangThai, NgayTao = @NgayTao
                                 WHERE MaPhiSach = @MaPhiSach";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaPhiSach", ps.MaPhiSach);
                cmd.Parameters.AddWithValue("@MaSach", ps.MaSach);
                cmd.Parameters.AddWithValue("@PhiMuon", ps.PhiMuon);
                cmd.Parameters.AddWithValue("@PhiPhat", (object)ps.PhiPhat ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@TrangThai", (object)ps.TrangThai ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@NgayTao", (object)ps.NgayTao ?? DBNull.Value);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }







        public bool Delete(string maPhiSach)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM PhiSach WHERE MaPhiSach = @MaPhiSach";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaPhiSach", maPhiSach);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }






        public List<PhiSach> Search(string kw)
        {
            var list = new List<PhiSach>();
            using var connSql = new SqlConnection(connectionString);
            string sql = @"
            SELECT * FROM PhiSach
            WHERE 
                (@kw = '') OR
                (MaPhiSach LIKE @kw OR
                MaSach LIKE @kw OR
                CAST(PhiMuon AS NVARCHAR) LIKE @kw OR
                CAST(PhiPhat AS NVARCHAR) LIKE @kw OR
                CAST(TrangThai AS NVARCHAR) LIKE @kw OR
                CONVERT(NVARCHAR, NgayTao, 120) LIKE @kw)";
            using var cmd = new SqlCommand(sql, connSql);
            cmd.Parameters.AddWithValue("@kw", "%" + kw + "%");
            connSql.Open();
            using var r = cmd.ExecuteReader();
            while (r.Read())
            {
                list.Add(new PhiSach
                {
                    MaPhiSach = r["MaPhiSach"].ToString(),
                    MaSach = r["MaSach"].ToString(),
                    PhiMuon = Convert.ToDecimal(r["PhiMuon"]),
                    PhiPhat = r["PhiPhat"] == DBNull.Value ? null : (decimal?)Convert.ToDecimal(r["PhiPhat"]),
                    TrangThai = r["TrangThai"] == DBNull.Value ? null : (bool?)Convert.ToBoolean(r["TrangThai"]),
                    NgayTao = r["NgayTao"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(r["NgayTao"])
                });
            }
            return list;
        }
    }
}

