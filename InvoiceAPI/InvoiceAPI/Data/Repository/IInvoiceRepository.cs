using InvoiceAPI.Models.Entities;

namespace InvoiceAPI.Data.Repository
{
    public interface IInvoiceRepository
    {
        Task AddAsync(Invoice invoice);

        Task SaveChangesAsync();
    }
}