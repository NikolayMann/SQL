using System;

namespace SQL_Test
{
    class Program
    {

        static void Main(string[] args)
        {
            Shop shop = new Shop();
            CustomerInfo customerInfo = new CustomerInfo();
            customerInfo.FirstName = "Иван";
            customerInfo.SecondName = "Иванович";
            customerInfo.LastName = "Иванов";
            customerInfo.Telephone = "+79995554466";
            customerInfo.E_Mail = "ivan@mail.ru";          

            OrderInfo order = new OrderInfo();
            order.CustomerE_Mail = "ivan@mail.ru";
            order.ProductCode = 1;
            order.ProductName = "osch плов";
            shop.AddOrder(customerInfo,order);
        }
    }
}
