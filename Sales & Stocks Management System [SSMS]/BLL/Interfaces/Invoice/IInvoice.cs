using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.Invoice
{
    public interface IInvoice<T> where T : class
    {
        double GetTotal(T Invoice);
    }
}
