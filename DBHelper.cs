using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cagy_okex
{
    public class DBHelper
    {

        string connString = "Host=st.funenc.com;Port=5432;Username=postgres;Password=1;Database=bit";

        public int Exec(string sql)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    int ct = 0;
                    try
                    {
                        ct = cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("插入数据库的异常:" + ex.Message + "sql:" + sql);
                        conn.Close();
                    }
                    return ct;
                }
            }
        }


        public int Exist(string dt)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                string sql = "select count(1) from bh where tm='" + dt + "'";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    int ct = 0;
                    try
                    {
                        ct = int.Parse(cmd.ExecuteScalar().ToString());
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("插入数据库的异常:" + ex.Message + "sql:" + sql);
                        conn.Close();
                    }
                    return ct;
                }
            }
        }
    }
}
