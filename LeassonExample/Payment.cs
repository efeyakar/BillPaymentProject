using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeassonExample
{
    public interface IPayment 
    {
        List<Bill> Payment(List<Bill> bill); // Interface payment method oluşturduk.
    }
}
