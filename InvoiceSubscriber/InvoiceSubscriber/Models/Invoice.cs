using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSubscriber.Models
{
    internal class Invoice
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public string Supplier { get; set; }
        public List<InvoiceLines> Lines { get; set; }
    }
}
