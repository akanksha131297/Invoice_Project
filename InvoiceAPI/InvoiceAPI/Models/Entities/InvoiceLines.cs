using System.Text.Json.Serialization;

namespace InvoiceAPI.Models.Entities
{
    
    public class InvoiceLines
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public double Quantity { get; set; }
        /// the foreign key referencing the parent invoice.

        public int InvoiceId { get; set; }

        ///navigation key, parent invoice. Ignored during JSON serialization.
        [JsonIgnore]
        public Invoice? Invoice{ get; set; }
    }
}
