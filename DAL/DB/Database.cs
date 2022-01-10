using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DB
{
    public class Database
    {
        private NpgsqlConnection Conn { get; set; }

        public Database(string connstr)
        {
                Conn = new NpgsqlConnection(connstr);
                OpenCon();
            
        }
        public bool OpenCon()
        {
            lock (Conn)
            {
                if (Conn.State == System.Data.ConnectionState.Open)
                    return true;

                Conn.Open();

                if (Conn.State == System.Data.ConnectionState.Open)
                    return true;

                return false;
            }
        }
        

        public bool CloseCon()
        {
            lock (Conn)
            {
                if (Conn.State == System.Data.ConnectionState.Closed)
                    return true;

                Conn.Close();

                if (Conn.State == System.Data.ConnectionState.Closed)
                    return true;

                return false;
            }
        }

        public bool ExecuteNonQuery(NpgsqlCommand cmd)
        {
            lock (Conn)
            {
                cmd.Connection = Conn;
                if (cmd.ExecuteNonQuery() == -1)
                    return false;

                return true;
            }
        }

        public NpgsqlDataReader ExecuteQuery(NpgsqlCommand cmd)
        {
            lock (Conn)
            {
                cmd.Connection = Conn;
                return cmd.ExecuteReader();
            }
        }
    }
}
