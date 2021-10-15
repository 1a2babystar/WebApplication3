using Notice_board.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Notice_board.Data
{
    public abstract class DB_CRUD<ITEM, NEWITEM, PAGEINFO, ITEMLIST>
    {
        protected string connectionString;

        protected void LoadConnectionString(string db_string)
        {
            this.connectionString = ConfigurationManager.ConnectionStrings[db_string].ConnectionString;
        }

        protected SqlConnection GetDatabaseConnection()
        {
            SqlConnection dbConn = new SqlConnection(connectionString);

            if (dbConn.State != System.Data.ConnectionState.Open)
            {
                dbConn.Open();
                return dbConn;
            }
            else
            {
                //connection already opened;
                return dbConn;
            }
        }
        protected bool CloseDatabaseConnection(SqlConnection dbConn)
        {
            bool ret = false;
            try
            {
                dbConn.Close();
                ret = true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return ret;
        }
        public abstract ITEMLIST GetList(PAGEINFO obj);
        public abstract int Getcount(PAGEINFO obj);
        public abstract int Create(NEWITEM obj);
        public abstract int Update(ITEM obj);
        public abstract int Delete(int deleteId);
    }
}
