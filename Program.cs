using System;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.Common;
using System.ComponentModel.DataAnnotations;

namespace SQL_Test
{
    class Program
    {
        static void PingDatabase<T>(T ConnectionStringBuilder)
            where T: DbConnectionStringBuilder
        {
            DbConnection DataBase;
            if (ConnectionStringBuilder is SqlConnectionStringBuilder)
            {
                DataBase = new SqlConnection((ConnectionStringBuilder as T).ConnectionString);
            }
            else
            {
                DataBase = new OleDbConnection((ConnectionStringBuilder as T).ConnectionString);
            }

            using (var DB_Connection = DataBase)
            {
                DataBase = null;
                DB_Connection.StateChange +=
                    (s, e) => { Console.WriteLine($"{nameof(DB_Connection)} в состоянии {DB_Connection.State}"); };
                try
                {
                    DB_Connection.Open();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{e.Message}");
                }
                finally
                {
                    DB_Connection.Close();
                }
            }            
        }
        static void Main(string[] args)
        {
            SqlConnectionStringBuilder SQL_Builder = new SqlConnectionStringBuilder()
            {
                DataSource = @"(localdb)\MSSQLLocalDB",
                IntegratedSecurity = true,                
            };

            OleDbConnectionStringBuilder AccessBuilder = new OleDbConnectionStringBuilder()
            {
                Provider = @"Microsoft.ACE.OLEDB.12.0",
                DataSource = @"C:\Users\Nikolay.DESKTOP-HRLQS2P\source\repos\SQL_Test\Database\Database5.mdb"
            };
            //Provider=;Data Source=C:\Users\Nikolay.DESKTOP-HRLQS2P\source\repos\SQL_Test\Database\Database5.mdb
            Console.WriteLine(SQL_Builder.ConnectionString);
            PingDatabase(SQL_Builder);
            PingDatabase(AccessBuilder);
            //using (var AccConnection = new OleDbConnection(AccessBuilder.ConnectionString))
            //{
            //    AccConnection.StateChange +=
            //        (s, e) => { Console.WriteLine($"{nameof(AccConnection)} в состоянии {(s as OleDbConnection).State}"); };
            //    try
            //    {
            //        AccConnection.Open();
            //    }
            //    catch(Exception e)
            //    {
            //        Console.WriteLine($"{e.Message}");
            //    }
            //    finally
            //    {
            //        AccConnection.Close();
            //    }
            //}
        }
    }
}
