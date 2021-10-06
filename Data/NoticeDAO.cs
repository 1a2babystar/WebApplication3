﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        internal static void Delete(int id)
        {
            string queryString = "DELETE FROM dbo.Notices WHERE Id = @id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                try
                {
                    command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;

                    connection.Open();
                    int newID = command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
        }

        public static bool CreateNotice(Notice notice)
        {
            string queryString = "INSERT INTO dbo.Notices Values(@writer, @title, @date, @content)";
            bool ret = false;
            string time = DateTime.Now.ToString("HH:mm:ss tt");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
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
            string queryString = "UPDATE dbo.Notices SET title = @title, content = @content, date = @date WHERE Id = @id";
            bool ret = false;
            string time = DateTime.Now.ToString("HH:mm:ss tt");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
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
            string queryString = "SELECT * FROM dbo.Notices WHERE Id = @id";
            Notice notice = new Notice();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
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