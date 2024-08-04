using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    }
}
