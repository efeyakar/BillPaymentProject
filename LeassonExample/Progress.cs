using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeassonExample
{
    public class Progress : IPayment
    {
        public List<Bill> Payment(List<Bill> bill) // Burada oluştruğumuz Interface i implamant ettik. Ve Methodu kullanım amacına göre düzenledik.
        {
            int answer;

            foreach (Bill b in bill)
            {
                if (b.DebtAmount !=0.0)
                {
                    Console.WriteLine($"{b.IdNumber} Tc kimlik numaralı {b.CustomerName}, {b.InvoiceType} faturanızın {b.DebtAmount} TL borcunu ödemek için 1'e basınız.");
                    answer = Convert.ToInt32(Console.ReadLine());
                    if (answer == 1)
                    {
                        b.DebtAmount = 0.0;
                        b.Status = "Ödendi";

                    }
                    else
                    {
                        Console.WriteLine($"{b.InvoiceType} faturanızın {b.DebtAmount} TL tutarındaki borcunuz ödenmedi.");
                        b.Status = "ödenmedi";
                    }
                }
            }
            return bill;
        }
    }
}
