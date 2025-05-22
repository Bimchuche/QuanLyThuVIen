using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DTO_QuanLyThuVien;
using Microsoft.Data.SqlClient;
namespace DAL_QuanLyThuVien
{
    public class DBUTIL
    {
        public static string ConnectionString = @"Data Source=Bminh\\SQLEXPRESS;Initial Catalog=Xuong_QuanLyThuVien;Integrated Security=True;";
        public static SqlCommand GetCommand(string sql, List<Object> args, CommandType cmdType)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = cmdType;
            for (int i = 0; i < args.Count; i++)
            {
                cmd.Parameters.AddWithValue($"@{i}", args[i]);
            }
            return cmd;
        }

        public static void Update(string sql, List<object> args, CommandType cmdType = CommandType.Text)
        {
            SqlCommand cmd = GetCommand(sql, args, cmdType);
            cmd.Connection.Open();
            cmd.Transaction = cmd.Connection.BeginTransaction();
            try
            {
                cmd.ExecuteNonQuery();
                cmd.Transaction.Commit();
            }
            catch (Exception)
            {
                cmd.Transaction.Rollback();
                throw;
            }
        }

        public static SqlDataReader Query(string sql, List<object> args, CommandType cmdType = CommandType.Text)
        {
            try
            {
                SqlCommand cmd = GetCommand(sql, args, cmdType);
                cmd.Connection.Open();
                return cmd.ExecuteReader();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static Object Value(string sql, List<object> args, CommandType cmdType = CommandType.Text)
        {
            try
            {
                SqlCommand cmd = GetCommand(sql, args, cmdType);
                cmd.Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                Object result = new object();
                if (reader.HasRows)
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        string columnName = reader.GetName(i);
                        PropertyInfo propertyInfo = result.GetType().GetProperty(columnName);

                        if (propertyInfo != null)
                        {
                            var value = reader.IsDBNull(i) ? null : reader[columnName];
                            propertyInfo.SetValue(result, value);
                        }
                    }
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
