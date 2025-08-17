using System.Text.Json.Serialization;

namespace InvoiceAPI.Models.Entities
{
    public class Invoice
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public DateTime DueDate { get; set; }

        public string Supplier { get; set; }

        public List<InvoiceLines> Lines { get; set; }
    }
}
