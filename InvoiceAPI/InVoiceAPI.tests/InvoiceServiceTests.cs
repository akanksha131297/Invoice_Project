using InvoiceAPI.Services;
using InvoiceAPI.Infra;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using InvoiceAPI.Models.Entities;
using InvoiceAPI.DTOs;

public class InvoiceServiceTests
{
    [Fact]
    public async Task CreateInvoice_Should_Save_And_Publish()
    {
        // Arrange: mock the repository
        var mockRepository = new Mock<InvoiceAPI.Data.Repository.IInvoiceRepository>();
        mockRepository.Setup(r => r.AddAsync(It.IsAny<Invoice>()))
                      .Returns(Task.CompletedTask);
        mockRepository.Setup(r => r.SaveChangesAsync())
                      .Returns(Task.CompletedTask);

        // Arrange: mock the publisher
        var mockPublisher = new Mock<IMessagePublisher>();

        // Arrange: create service instance using repository
        var service = new InvoiceService(mockRepository.Object, mockPublisher.Object);

        // Arrange: create invoice with lines
        var invoiceDto = new InvoiceCreateDto
        {
            Description = "Service Test Invoice",
            Supplier = "XYZ Ltd",
            DueDate = DateTime.UtcNow,
            Lines = new List<InvoiceLineCreateDto>
            {
                new InvoiceLineCreateDto
                {
                    Description = "Item1",
                    Price = 5,
                    Quantity = 1
                }
            }
        };

        // Act: call service method
        var result = await service.CreateInvoiceAsync(invoiceDto);

        // Assert: result is returned
        Assert.NotNull(result);
        Assert.Equal("Service Test Invoice", result.Description);
        Assert.Single(result.Lines);
        Assert.Equal("Item1", result.Lines[0].Description);

        // Assert: publisher called once
        mockPublisher.Verify(p => p.Publish("invoice_queue", It.IsAny<object>()), Times.Once);
    }
}
