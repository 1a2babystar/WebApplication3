using Notice_board.Data;
using Notice_board.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Data
{
    public class NoticeDAO : DB_CRUD<Notice, Newnotice, Pageinfo, List<Notice>>
    {
        public NoticeDAO()
        {
            LoadConnectionString("NoticeConnectionString");
        }

        private RET ErrorHanle<RET>(SqlConnection connection, SqlCommand command, Func<SqlConnection, SqlCommand, RET> action)
        {
            try
            {
                return action(connection, command);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                CloseDatabaseConnection(connection);
            }
            return action(connection, command);
        }

        public override List<Notice> GetList(Pageinfo pageinfo)
        {
            string filterstring = GetFilterString(pageinfo);
            List<Notice> returnlist = new List<Notice>();
            SqlConnection connection = GetDatabaseConnection();
            SqlCommand command = new SqlCommand("spGetNotices", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add("@filtercondition", System.Data.SqlDbType.VarChar, 1000).Value = filterstring;
            command.Parameters.Add("@sortdirection", System.Data.SqlDbType.VarChar, 5).Value = pageinfo.sort.dir;
            command.Parameters.Add("@sort", System.Data.SqlDbType.VarChar, 50).Value = pageinfo.sort.field;
            command.Parameters.Add("@limit", System.Data.SqlDbType.Int).Value = pageinfo.pageSize;
            command.Parameters.Add("@offset", System.Data.SqlDbType.Int).Value = pageinfo.skip;
            ErrorHanle<List<Notice>>(connection, command, (connection, command) =>
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Notice a = new Notice(Int32.Parse(reader["Id"].ToString()), reader["title"].ToString(), reader["writer"].ToString(), reader["content"].ToString(), reader["date"].ToString());
                        returnlist.Add(a);
                    }
                    reader.Close();
                }
                return returnlist;
            });
            return returnlist;
        }
        public override int Getcount(Pageinfo pageinfo)
        {
            string filterstring = GetFilterString(pageinfo);
            int rownumber = -1;
            SqlConnection connection = GetDatabaseConnection();
            SqlCommand command = new SqlCommand("spGetRowNum", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add("@filtercondition", System.Data.SqlDbType.VarChar, 1000).Value = filterstring;
            ErrorHanle<int>(connection, command, (connection, command) =>
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        rownumber = Int32.Parse(reader["rowcount"].ToString());
                    }
                    reader.Close();
                }
                return rownumber;
            });
            return rownumber;
        }
        public override int Create(Newnotice newnotice)
        {
            int CreateId = -1;
            var culture = new CultureInfo("en-GB");
            string time = DateTime.Now.ToString(culture);
            SqlConnection connection = GetDatabaseConnection();
            SqlCommand command = new SqlCommand("spCreateNotice", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add("@writer", System.Data.SqlDbType.VarChar, 50).Value = newnotice.writer;
            command.Parameters.Add("@title", System.Data.SqlDbType.VarChar, 50).Value = newnotice.title;
            command.Parameters.Add("@date", System.Data.SqlDbType.VarChar, 50).Value = time;
            command.Parameters.Add("@content", System.Data.SqlDbType.VarChar, 50).Value = newnotice.content;
            ErrorHanle<int>(connection, command, (connection, command) =>
            {
                CreateId = command.ExecuteNonQuery();
                return CreateId;
            });
            return CreateId;
        }
        public override int Update(Notice notice)
        {
            int UpdateId = -1;
            var culture = new CultureInfo("en-GB");
            string time = DateTime.Now.ToString(culture);
            SqlConnection connection = GetDatabaseConnection();
            SqlCommand command = new SqlCommand("spUpdateNotice", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add("@title", System.Data.SqlDbType.VarChar, 50).Value = notice.title;
            command.Parameters.Add("@date", System.Data.SqlDbType.VarChar, 50).Value = time;
            command.Parameters.Add("@content", System.Data.SqlDbType.VarChar, 50).Value = notice.content;
            command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = notice.id;
            command.Parameters.Add("@writer", System.Data.SqlDbType.VarChar, 50).Value = notice.writer;
            ErrorHanle<int>(connection, command, (connection, command) =>
            {
                UpdateId = command.ExecuteNonQuery();
                return UpdateId;
            });
            return UpdateId;
        }
        public override int Delete(int deleteId)
        {
            int DeleteId = -1;
            SqlConnection connection = GetDatabaseConnection();
            SqlCommand command = new SqlCommand("spDeleteNotice", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = deleteId;
            ErrorHanle<int>(connection, command, (connection, command) =>
            {
                DeleteId = command.ExecuteNonQuery();
                return DeleteId;
            });
            return DeleteId;
        }

        private string GetFilterString(Pageinfo pageinfo)
        {
            string condition = "";
            if (pageinfo.sort == null)
            {
                pageinfo.sort = new Sortinfo("Id", "asc");
            }
            if (pageinfo.filter != null)
            {
                Filterinfo filterinfo = pageinfo.filter;

                condition += "WHERE ";
                var logic = "";
                if (filterinfo.logic == "and")
                {
                    logic = "AND";
                }
                else
                {
                    logic = "OR";
                }

                var filters = filterinfo.filters;
                int i = 0;
                while (i != filters.Count)
                {
                    var filter = filters[i];
                    switch (filter.@operator)
                    {
                        case "eq":
                            condition += $"{filter.field} = '{filter.value}'";
                            break;
                        case "neq":
                            condition += $"{filter.field} <> '{filter.value}'";
                            break;
                        case "contains":
                            condition += $"{filter.field} LIKE '%{filter.value}%'";
                            break;
                        case "doesnotcontain":
                            condition += $"{filter.field} NOT LIKE '%{filter.value}%'";
                            break;
                        case "endswith":
                            condition += $"{filter.field} LIKE '%{filter.value}'";
                            break;
                        case "isnull":
                            condition += $"{filter.field} IS NULL";
                            break;
                        case "isnotnull":
                            condition += $"{filter.field} IS NOT NULL";
                            break;
                        case "isempty":
                            condition += $"{filter.field} = ''";
                            break;
                        case "isnotempty":
                            condition += $"{filter.field} <> ''";
                            break;
                        case "isnullorempty":
                            condition += $"{filter.field} IS NULL OR {filter.field} = ''";
                            break;
                        case "isnotnullorempty":
                            condition += $"{filter.field} IS NOT NULL AND {filter.field} <> ''";
                            break;
                    }
                    if (i != filters.Count - 1)
                    {
                        condition += $" {logic} ";
                    }
                    i++;
                }
            }
            return condition;
        }
    }
}