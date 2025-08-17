using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InvoiceSubscriber.Models
{
    internal class InvoiceLines
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        public int InvoiceId { get; set; }

        [JsonIgnore]
        public Invoice? Invoice { get; set; }
    }
}
