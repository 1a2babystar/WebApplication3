using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models
{
    public static class Noticeacts
    {
        private static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Test;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public static List<Reducednotice> GetNotices()
        {
            string queryString = "select title, date, Id from dbo.Notices";
            List<Reducednotice> returnlist = new List<Reducednotice>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
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

        public static bool CreateNotice(Notice notice)
        {
            string queryString = "INSERT INTO dbo.Notices Values(@writer, @title, @date, @content)";
            bool ret = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                try
                {
                    command.Parameters.Add("@writer", System.Data.SqlDbType.VarChar, 50).Value = notice.writer;
                    command.Parameters.Add("@title", System.Data.SqlDbType.VarChar, 50).Value = notice.title;
                    command.Parameters.Add("@date", System.Data.SqlDbType.VarChar, 50).Value = notice.date;
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

        public static Notice ShowDetail(int id)
        {
            string queryString = "SELECT * FROM dbo.Notices WHERE Id = @id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                try
                {
                    command.Parameters.Add("@Id", System.Data.SqlDbType.VarChar, 50).Value = id;

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    Notice notice = new Notice(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3));
                    return notice;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return new Notice(null, null, null, null);
        }
    }
}
