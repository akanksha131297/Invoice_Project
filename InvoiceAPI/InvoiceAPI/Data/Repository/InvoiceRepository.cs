using InvoiceAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
namespace InvoiceAPI.Data.Repository
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly AppDBContext _context;

        public InvoiceRepository(AppDBContext context) => _context = context;

      
        public async Task AddAsync(Invoice invoice)
        {
            if (invoice == null)
                throw new ArgumentNullException(nameof(invoice));

            try
            {
                // Add the invoice entity to the context.
                await _context.Invoices.AddAsync(invoice);
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("An error occurred while adding the invoice.", ex);
            }
        }

        public async Task SaveChangesAsync()
        {
            try
            {
                // Save all changes to the database.
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("An error occurred while saving changes to the database.", ex);
            }
        }
    }
}
