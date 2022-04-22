using System;
using System.Data.SqlTypes;
using System.Data.SqlClient;
namespace SQL_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder()
            {
                DataSource = @"(localdb)\MSSQLLocalDB",
                IntegratedSecurity = true,
            };
            Console.WriteLine(builder.ConnectionString);
            using (var DB_Connection = new SqlConnection(builder.ConnectionString))
            {
                DB_Connection.StateChange +=
                    (s, e) => { Console.WriteLine($"{nameof(DB_Connection)} в состоянии {(s as SqlConnection).State}"); };
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
    }
}
