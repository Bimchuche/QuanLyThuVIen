using DTO_QuanLyThuVien;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_QuanLyThuVien
{
    public class DALNhanVien
    {
        public NhanVien? getNhanVien1(string email, string password)
        {
            string sql = "SELECT TOP 1 * FROM NhanVien WHERE Email = @0 AND MatKhau = @1";
            List<object> thamSo = new List<object>();
            thamSo.Add(email);
            thamSo.Add(password);
            SqlDataReader reader = DBUTIL.Query(sql, thamSo);
            if (reader.HasRows)
            {
                if (reader.Read())
                {
                    NhanVien nv = new NhanVien();
                    nv.MaNhanVien = reader["MaNhanVien"].ToString();
                    nv.Ten = reader["HoTen"].ToString();
                    nv.Email = reader["Email"].ToString();
                    nv.SoDienThoai = reader["SoDienThoai"].ToString();
                    nv.MatKhau = reader["MatKhau"].ToString();
                    nv.VaiTro = bool.Parse(reader["VaiTro"].ToString());
                    nv.TrangThai = bool.Parse(reader["TrangThai"].ToString());
                    nv.NgayTao = DateTime.Parse(reader["NgayTao"].ToString());
                    return nv;

                }
            }
            return null;
        }
        public void Update(NhanVien nv)
        {
            var sql = "UPDATE NhanVien SET MatKhau = @MatKhau WHERE Email = @Email";
            using (SqlConnection conn = new SqlConnection(DBUTIL.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MatKhau", nv.MatKhau);
                cmd.Parameters.AddWithValue("@Email", nv.Email);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public List<NhanVien> SelectBysql(string sql, List<object> args, CommandType cmdType = CommandType.Text)
        {
            List<NhanVien> list = new List<NhanVien>();
            try
            {
                SqlDataReader reader = DBUTIL.Query(sql, args, cmdType);
                while (reader.Read())
                {
                    NhanVien entity = new NhanVien();
                    entity.MaNhanVien = reader.GetString("MaNhanVien");
                    entity.Ten = reader.GetString("HoTen");
                    entity.Email = reader.GetString("Email");
                    entity.SoDienThoai = reader.GetString("SoDienThoai");
                    entity.MatKhau = reader.GetString("MatKhau");
                    entity.VaiTro = reader.GetBoolean("VaiTro");
                    entity.TrangThai = reader.GetBoolean("TrangThai");
                    entity.NgayTao = reader.GetDateTime("NgayTao");
                    list.Add(entity);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thực hiện câu lệnh SQL: " + ex.Message);
            }
            return list;


        }
        public List<NhanVien> selectAll()
        {
            string sql = "SELECT * FROM NhanVien";
            List<object> args = new List<object>();
            return SelectBysql(sql, new List<object>());
        }
        public void update(NhanVien nv)
        {
            try
            {
                string sql = @"UPDATE NhanVien 
                            SET HoTen = @1, Email = @2, MatKhau = @3, VaiTro = @4, TrangThai = @5
                            WHERE MaNhanVien = @0";
                List<object> thamSo = new List<object>();
                thamSo.Add(nv.MaNhanVien);
                thamSo.Add(nv.Ten);
                thamSo.Add(nv.Email);
                thamSo.Add(nv.SoDienThoai);
                thamSo.Add(nv.MatKhau);
                thamSo.Add(nv.VaiTro);
                thamSo.Add(nv.TrangThai);
                thamSo.Add(nv.NgayTao);
                DBUTIL.Update(sql, thamSo);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void insert(NhanVien nv)
        {
            try
            {
                string sql = @"INSERT INTO NhanVien (MaNhanVien, HoTen, Email, MatKhau, VaiTro, TrangThai) 
                            VALUES (@0, @1, @2, @3, @4, @5)";
                List<object> thamSo = new List<object>();
                thamSo.Add(nv.MaNhanVien);
                thamSo.Add(nv.Ten);
                thamSo.Add(nv.Email);
                thamSo.Add(nv.SoDienThoai);
                thamSo.Add(nv.MatKhau);
                thamSo.Add(nv.VaiTro);
                thamSo.Add(nv.TrangThai);
                thamSo.Add(nv.NgayTao);
                DBUTIL.Update(sql, thamSo);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void delete(string maNV)
        {
            try
            {
                string sql = "DELETE FROM NhanVien WHERE MaNhanVien = @0";
                List<object> thamSo = new List<object>();
                thamSo.Add(maNV);
                DBUTIL.Update(sql, thamSo);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<NhanVien> getNhanVienBy(string maNV)
        {
            string sql = "SELECT * FROM NhanVien WHERE MaNhanVien = @0";
            List<object> thamSo = new List<object>();
            thamSo.Add(maNV);
            return SelectBysql(sql, thamSo);


        }

    }
}
