
using InvoiceAPI.Data;
using InvoiceAPI.Data.Repository;
using InvoiceAPI.Models;
using InvoiceAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class InvoiceRepositoryTests
{
    [Fact]
    public async Task AddAsync_Should_Add_Invoice_To_Db()
    {
        var options = new DbContextOptionsBuilder<AppDBContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

         var context = new AppDBContext(options);
        var repo = new InvoiceRepository(context);

        var invoice = new Invoice
        {
            Description = "Test Invoice",
            Supplier = "XYZ Corp",
            DueDate = DateTime.UtcNow
        };

        await repo.AddAsync(invoice);
        await repo.SaveChangesAsync();

        var saved = context.Invoices.FirstOrDefault();
        Assert.NotNull(saved);
        Assert.Equal("Test Invoice", saved.Description);
    }
}
