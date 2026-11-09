using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.IO;

namespace Inventory
{
    public class Database
    {
        private static readonly string DBpATH =
            Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Database.accdb"
            );

        private static readonly string connectionString =
            @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source="
            + DBpATH + ";Persist Security Info=False;";

        public static OleDbConnection GetConnection()
        {
            return new OleDbConnection(connectionString);
        }
    }
}