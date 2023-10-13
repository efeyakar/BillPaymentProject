using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace LeassonExample
{
    internal class Program
    {
        static void Main()
        {

            Console.WriteLine("Fatura Ödeme sistemine hoşgeldiniz."); // Açılış yazısı

            #region  Costumer girişlerini aldığımız fonksiyonu çağırıyoruz

            var costumerList = CustomerInput();

            #endregion

            #region Boçları yazdırdığımız fonksiyonu çağırıyoruz

            PrintDebt(costumerList);

            #endregion
            
            
            Console.ReadKey(); // Program bittikten sonra kapanmaması için
        }


        #region User Data Input Functions

            /*  
             *  1.Kısım input al
             *  
             *  1- ++ Kullanıcıdan AD_SOYAD,TC,FATURA_TÜRÜ,BORÇ TUTARI(ondalık olacak) şekilde faturaları konsoldan al. (Bill CLASS’ı yaratılacak)
             *  2- ++ Ekrandan n tane kullanıcı girişi yapılabilir. “Başka kullanıcı var mı?” sorusu HAYIR ise başka kullanıcı alma.
             *  3-  Aynı TC’ye sahip birden fazla fatura olabilir. Eğer aynı TC’ye ait birden fazla fatura varsa bunları ayrı bir listede tut
             */


        static List<Bill> CustomerInput()
        {
            //Değişkenlerimizi tanımladık.

            string customerName;
            string invoiceType;
            string anyOrderCustomer;
            double idNumber;
            double debtAmount;
            double totalAmount = 0.0;
            List<Bill> userPaymentInfo = new List<Bill>();

            
            // Do-While ile kullanıcıdan bilgileri aldık. Daha sonra Bill sınıfından obje oluşturup bunları list içinde sakladık.

            do
            {
                Console.Write("Adınızı giriniz: ");
                customerName = Console.ReadLine();

                Console.Write("TC Kimlik numarazını giriniz: ");
                idNumber = Convert.ToDouble(Console.ReadLine());

                Console.Write("Fatura türü giriniz(): ");
                invoiceType = Console.ReadLine();

                Console.Write("Borç tutarı giriniz: ");
                debtAmount = Convert.ToDouble(Console.ReadLine().Replace(".", ",").Replace(",", ",")); // Bu kısımda terminalden nokta da girilse virgül de girilse double formatına uygun hale getirecek.

                totalAmount += debtAmount; // Toplam fatura borcu için yaptığımız işlem

                

                Bill PaymentInfo = new Bill(customerName, idNumber, invoiceType, debtAmount, "--");
                userPaymentInfo.Add(PaymentInfo);


                Console.Write("Başka kullanıcı var mı: ");
                anyOrderCustomer = Console.ReadLine();


            } while (anyOrderCustomer.ToUpper() != "HAYIR");

            Console.WriteLine($"\n\r\n\rToplam fatura borcu {totalAmount} liradır. Fatura ayrıntıları aşağıda mevcuttur.\n\r");
            return userPaymentInfo;  

        }

        #endregion


        #region Borçların yazdırıldığı kısım


            /*  2.kısım Borçları yazdır.
            *  
            *  1- Toplam fatura borcunu ekrana yazdır.
            *  2- Kişi’lerin toplam fatura borçlarını isimleriyle birlikte yazdır.
            */

        static void PrintDebt(List<Bill> userPaymentInfo)
        {

            // Burada lambda kullanarak kullanıcıdan aldığımız datayı TC no ya göre filtreledik. Daha sonra bu filtreye göre yeni bir tane nesne oluşturduk.

            var totalAmaount = userPaymentInfo.GroupBy(item => item.IdNumber).Select(Group => new { Idnumber = Group.Key, Name = Group.First().CustomerName, Amaount = Group.Sum(item => item.DebtAmount) });

            Console.WriteLine("-------------------------------------------------------------------");
            Console.WriteLine("Tc No \t\t Ad Soyad \t Borç Tutarı");

            foreach (var item in totalAmaount)
            {

                Console.WriteLine($"{item.Idnumber}\t{item.Name}\t\t{item.Amaount} ");

            }
            #endregion

            #region Ödeme işlemini burada yapıyoruz

            /*  3.Kısım ödeme işlemi
            *  
            *  Bir ödeme Interfacesi yaz. Bu interface içinde ödeme yapılacak metodu yaz.
            *  Bir İşlem classı oluştur ve ödeme interfacesini implement ederek döngü içerisinde sadece borcu olanlar için ödeme fonksiyonunu 
            *  çalışmasını sağla. Ödedikten sonra kişilerin borçlarını sıfırla.
            */


            Progress progress = new Progress();

            userPaymentInfo = progress.Payment(userPaymentInfo);

            #endregion


            #region Ödeme işleminden sonra listedeki son durum


            /*  4.Kısım 
            *  
            *  Borçlar ödendikten sonra ekrana borcu ödenenler için BORÇ ÖDENDİ bilgisini liste olarak yazdır.(Borcu olmayanlar için boş kalacak

           */

            Console.WriteLine("\n\r-------------------------------------------------------------------");
            Console.WriteLine("Tc No \t\t Ad Soyad \t Fatura\t Borç Tutarı\t Ödeme Durumu");

            foreach (var item in userPaymentInfo)
            {

                Console.WriteLine($"{item.IdNumber}\t{item.CustomerName}\t\t {item.InvoiceType}\t {item.DebtAmount}\t{item.Status}");
            }


        }
        #endregion


    }
}
