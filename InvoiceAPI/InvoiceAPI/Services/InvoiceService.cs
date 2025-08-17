using InvoiceAPI.Data.Repository;
using InvoiceAPI.DTOs;
using InvoiceAPI.Infra;
using InvoiceAPI.Models.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceAPI.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IMessagePublisher _messagePublisher;

        public InvoiceService(IInvoiceRepository invoiceRepository, IMessagePublisher messagePublisher)
        {
            _invoiceRepository = invoiceRepository;
            _messagePublisher = messagePublisher;
        }

        public async Task<InvoiceReadDto> CreateInvoiceAsync(InvoiceCreateDto invoiceDto)
        {
            ValidateInvoice(invoiceDto);

            // Map DTO -> Entity
            var invoice = new Invoice
            {
                Description = invoiceDto.Description,
                Supplier = invoiceDto.Supplier,
                DueDate = invoiceDto.DueDate,
                Lines = invoiceDto.Lines.Select(l => new InvoiceLines
                {
                    Description = l.Description,
                    Price = l.Price,
                    Quantity = l.Quantity
                }).ToList()
            };

            // Save to DB
            await _invoiceRepository.AddAsync(invoice);
            await _invoiceRepository.SaveChangesAsync();

            // Publish event
            _messagePublisher.Publish("invoice_queue", invoice);

            // Map Entity -> ReadDto
            return new InvoiceReadDto
            {
                Id = invoice.Id,
                Description = invoice.Description,
                Supplier = invoice.Supplier,
                DueDate = invoice.DueDate,
                Lines = invoice.Lines.Select(l => new InvoiceLineReadDto
                {
                    Id = l.Id,
                    Description = l.Description,
                    Price = l.Price,
                    Quantity = l.Quantity
                }).ToList()
            };
        }

        // Private validation method
        private void ValidateInvoice(InvoiceCreateDto invoiceDto)
        {
            if (invoiceDto == null)
                throw new ArgumentException("Invoice cannot be null.");

            if (string.IsNullOrWhiteSpace(invoiceDto.Description))
                throw new ArgumentException("Invoice description is required.");

            if (string.IsNullOrWhiteSpace(invoiceDto.Supplier))
                throw new ArgumentException("Supplier is required.");

            if (invoiceDto.DueDate == default)
                throw new ArgumentException("Due date is required.");

            if (invoiceDto.Lines == null || !invoiceDto.Lines.Any())
                throw new ArgumentException("At least one invoice line is required.");

            foreach (var line in invoiceDto.Lines)
            {
                if (string.IsNullOrWhiteSpace(line.Description))
                    throw new ArgumentException("Each invoice line must have a description.");
                if (line.Price < 0)
                    throw new ArgumentException("Invoice line price cannot be negative.");
                if (line.Quantity <= 0)
                    throw new ArgumentException("Invoice line quantity must be a positive whole number.");
            }
        }
    }
}
