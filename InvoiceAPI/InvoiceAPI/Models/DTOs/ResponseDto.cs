using System;
using System.Collections.Generic;

namespace InvoiceAPI.DTOs
{
    public class InvoiceReadDto
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public DateTime DueDate { get; set; }
        public string Supplier { get; set; } = null!;
        public List<InvoiceLineReadDto> Lines { get; set; } = new();
    }

    public class InvoiceLineReadDto
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public double Price { get; set; }
        public double Quantity { get; set; }
    }
}
