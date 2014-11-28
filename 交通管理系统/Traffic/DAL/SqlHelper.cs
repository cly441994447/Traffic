using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace CLYLibrary
{
    public static class SqlHelper
    {
        /// <summary>
        /// 连接字符串，一个只读的字符串
        /// </summary>
        static readonly string connStr = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;

        /// <summary>
        /// 执行一个非查询语句，返回受影响行数，如果实行的是非增、删、改操作，返回-1
        /// </summary>
        /// <param name="cmdText">执行的SQL语句</param>
        /// <param name="paras">需要的参数</param>
        /// <returns>返回受影响行数</returns>
        public static int ExecuteNonQuery(string cmdText, params SqlParameter[] paras)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                {
                    if (paras != null)
                    {
                        cmd.Parameters.AddRange(paras);
                    }

                    conn.Open();

                    return cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// 执行一个查询的SQL语句，返回第一行第一列的结果
        /// </summary>
        /// <param name="cmdText">SQL语句</param>
        /// <param name="paras">参数</param>
        /// <returns>返回第一行第一列的数据</returns>
        public static object ExecuteScalar(string cmdText, params SqlParameter[] paras)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                {
                    if (paras != null)
                    {
                        cmd.Parameters.AddRange(paras);
                    }

                    conn.Open();

                    return cmd.ExecuteScalar();
                }
            }
        }

        public static SqlDataReader ExecuteRaeder(string cmdText, params SqlParameter[] paras)
        {
            SqlConnection conn = new SqlConnection(connStr);
            using (SqlCommand cmd = new SqlCommand(cmdText, conn))
            {
                if (paras != null)
                {
                    cmd.Parameters.AddRange(paras);
                }
                conn.Open();
                try
                {
                    return cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                }
                catch (Exception ex)
                {
                    conn.Dispose();
                    throw ex;
                }
            }
        }


        public static DataTable Adapter(string cmdText, params SqlParameter[] paras)
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter sda = new SqlDataAdapter(cmdText, connStr))
            {
                if (paras != null)
                {
                    sda.SelectCommand.Parameters.AddRange(paras);
                }
                sda.Fill(dt);
            }
            return dt;
        }
    }
}
