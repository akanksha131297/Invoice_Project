using InvoiceAPI.DTOs;
using InvoiceAPI.Models.Entities;
using System.Threading.Tasks;

namespace InvoiceAPI.Services
{
    public interface IInvoiceService
    {
        Task<InvoiceReadDto> CreateInvoiceAsync(InvoiceCreateDto invoiceDto);
    }
}
