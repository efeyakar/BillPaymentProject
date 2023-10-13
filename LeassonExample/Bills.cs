using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeassonExample
{
    public class Bill
    { 

        // Field tanımlarımız burada

        private string customerName;
        private double idNumber;
        private string invoiceType;
        private double debtAmount;
        private string status;


        private readonly string[] invoice = { "su", "elektrik", "doğalgaz", "telefon", "internet" }; // Burada fatura türlerini ekledim düzenlenemez olması readonly keywordu kullandım.

        

        #region Encapsulation

        public string CustomerName
        {
            get
            {
                return customerName;
            }
            set
            {
                customerName = value;
            }
        }


        public double IdNumber
        {
            get
            {
                return idNumber;
            }
            set
            {
                // Burada Tc kimlik numarasının 11 haneli olup olmadığını kontrol ediyorum. Eğer 11 haneli değilde kullanıcının tekrardan girmesini istiyorum.

                if (value.ToString().Length == 11)
                {
                    idNumber = value;
                }
                else
                {
                    Console.WriteLine("TC Kimlik numarası 11 haneli olmalıdır. Lütfen tekrar giriniz: ");
                    idNumber = Convert.ToInt32(Console.ReadLine());
                }
            }
        }

        public string InvoiceType
        {
            get
            {
                return invoiceType;
            }
            set
            {

                // Burada kullanıcının girmiş olduğu fatura adında bir tür olup olmadığını kontrol ediyoruz. Eğer yoksa kullanıcının tekrardan fatura türü girmesini sağlıyoruz.

                if (invoice.Contains(value.ToLower()))
                {
                    invoiceType = value;
                }
                else
                {
                    Console.WriteLine("Geçersiz fatura türü. Lütfen tekrar giriniz.(su,elektrik,doğalgaz,telefon,internet): ");
                    invoiceType = Console.ReadLine();
                }
                
            }
        }
        public double DebtAmount 
        { 
            get
            { 
                return debtAmount; 
            }
            set
            {
               debtAmount = value;
                

                
            }
        }

        public string Status 
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
            }
        }


        #endregion


        #region Consractor

        public Bill(string customerName, double idNumber, string invoiceType, double debtAmount,string status)
        {
            this.CustomerName = customerName;
            this.IdNumber = idNumber;
            this.InvoiceType = invoiceType;
            this.DebtAmount = debtAmount;
            this.Status = status;
        }

        #endregion
    }
}
