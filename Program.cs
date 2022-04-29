﻿using System;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.Common;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace SQL_Test
{
    public struct CustomerInfo
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public string E_Mail { get; set; }
        public string Telephone { get; set; }
        public string ToSQL_AdditionRq()
        {
            string result = null;
            if (FirstName != null && SecondName != null && LastName != null && E_Mail != null)
            {
                if (E_Mail.Contains('@'))
                {
                    result = $"INSERT INTO [dbo].[Shop] ([lastName], [firstName], [secondName], [telephone], [eMail]) VALUES (N'{this.LastName}',N'{this.FirstName}',N'{this.SecondName}', '{this.Telephone}', '{this.E_Mail}')\r\n";
                }
            }
            Console.WriteLine(result);
            return result;
        }
    }
    public struct OrderInfo
    {
        public string CustomerE_Mail { get; set; }
        public int? ProductCode { get; set; }
        public string ProductName { get; set; }
        public string MakeDatabaseRqst()
        {
            string result = null;
            if (ProductName != null && ProductCode != null && CustomerE_Mail != null)
            {
                if (CustomerE_Mail.Contains('@'))
                {
                    result = $"INSERT INTO [Orders] ([eMail], [productCode], [productName]) VALUES ('{this.CustomerE_Mail}',{this.ProductCode},'{this.ProductName}')\r\n";
                }
            }
            return result;
        }
    }

    class Program
    {
        static void AddCustomer(CustomerInfo customerInfo)
        {
            SqlConnectionStringBuilder SQL_Builder = new SqlConnectionStringBuilder()
            {
                DataSource = @"(localdb)\MSSQLLocalDB",
                IntegratedSecurity = false,
                InitialCatalog = @"D:\SQL\Database\MySqlTest.mdf",
                PersistSecurityInfo = false,
                UserID = "UserM",
                Password = "123456789Q",
                Pooling = false
            };
            Console.WriteLine(SQL_Builder.ConnectionString);
            using (SqlConnection DataBase = new SqlConnection(SQL_Builder.ConnectionString))
            {
                try
                {
                    Parallel.Invoke(DataBase.Open);
                    string cmd = customerInfo.ToSQL_AdditionRq();
                    SqlCommand command = new SqlCommand(cmd, DataBase);
                    command.ExecuteNonQuery();
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    DataBase.Close();
                }
            }
        }
        static void AddOrder(OrderInfo order)
        {            

            OleDbConnectionStringBuilder AccessBuilder = new OleDbConnectionStringBuilder()
            {
                Provider = @"Microsoft.ACE.OLEDB.12.0",
                DataSource = @"D:\SQL\Database\Database5.mdb"
            };

            using (OleDbConnection DataBase = new OleDbConnection(AccessBuilder.ConnectionString))
            {
                try
                {
                    Parallel.Invoke(DataBase.Open);
                    string cmd = order.MakeDatabaseRqst();
                    OleDbCommand command = new OleDbCommand(cmd, DataBase);
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{e.Message}");
                }
                finally
                {
                    DataBase.Close();
                }
            }    
        }
        static void Main(string[] args)
        {
            CustomerInfo customerInfo = new CustomerInfo();
            customerInfo.FirstName = "Иван";
            customerInfo.SecondName = "Иванович";
            customerInfo.LastName = "Иванов";
            customerInfo.Telephone = "+79995554466";
            customerInfo.E_Mail = "ivan@mail.ru";
            AddCustomer(customerInfo);
            Console.WriteLine("SQL ready!");

            OrderInfo order = new OrderInfo();
            order.CustomerE_Mail = "ivan@mail.ru";
            order.ProductCode = 1;
            order.ProductName = "osch плов";
            AddOrder(order);
            Console.WriteLine("Access ready!");
        }
    }
}
