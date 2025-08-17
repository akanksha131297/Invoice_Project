using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InvoiceAPI.DTOs
{
    public class InvoiceCreateDto
    {
        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public string Supplier { get; set; } 

        public List<InvoiceLineCreateDto> Lines { get; set; } 
    }

    public class InvoiceLineCreateDto
    {
        [Required]
        public string Description { get; set; } 

        [Required]
        public double Price { get; set; }

        public double Quantity { get; set; }
    }
}
