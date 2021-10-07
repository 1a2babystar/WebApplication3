using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models;

namespace WebApplication3.Data
{
    public static class NoticeDAO
    {
        private static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Test;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public static List<Reducednotice> GetNotices()
        {
            List<Reducednotice> returnlist = new List<Reducednotice>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("GetAllNotices", connection);
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Reducednotice a = new Reducednotice(reader.GetString(0), reader.GetString(1), reader.GetInt32(2));
                            returnlist.Add(a);
                        }
                        reader.Close();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }

            return returnlist;
        }

        internal static int Delete(int id)
        {
            int newID = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("DeleteNotice", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                try
                {
                    command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;

                    connection.Open();
                    newID = command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return newID;
        }

        public static bool CreateNotice(Notice notice)
        {
            bool ret = false;

            var culture = new CultureInfo("en-GB");
            string time = DateTime.Now.ToString(culture);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("CreateNotice", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                try
                {
                    command.Parameters.Add("@writer", System.Data.SqlDbType.VarChar, 50).Value = notice.writer;
                    command.Parameters.Add("@title", System.Data.SqlDbType.VarChar, 50).Value = notice.title;
                    command.Parameters.Add("@date", System.Data.SqlDbType.VarChar, 50).Value = time;
                    command.Parameters.Add("@content", System.Data.SqlDbType.VarChar, 50).Value = notice.contents;

                    connection.Open();
                    int newID = command.ExecuteNonQuery();
                    ret = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
            return ret;
        }

        public static bool UpdateNotice(Notice notice)
        {
            bool ret = false;
            var culture = new CultureInfo("en-GB");
            string time = DateTime.Now.ToString(culture);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("UpdateNotice", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                try
                {
                    command.Parameters.Add("@title", System.Data.SqlDbType.VarChar, 50).Value = notice.title;
                    command.Parameters.Add("@date", System.Data.SqlDbType.VarChar, 50).Value = time;
                    command.Parameters.Add("@content", System.Data.SqlDbType.VarChar, 50).Value = notice.contents;
                    command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = notice.id;

                    connection.Open();
                    int newID = command.ExecuteNonQuery();
                    ret = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
            return ret;
        }

        public static Notice ShowDetail(int id)
        {
            Notice notice = new Notice();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("GetOneNotice", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                try
                {
                    command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            notice.id = id;
                            notice.writer = reader.GetString(1);
                            notice.title = reader.GetString(2);
                            notice.date = reader.GetString(3);
                            notice.contents = reader.GetString(4);
                        }
                        reader.Close();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                } 
            }
            return notice;
        }
    }
}
