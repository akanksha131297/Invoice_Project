using InvoiceAPI.Controllers;
using InvoiceAPI.DTOs;
using InvoiceAPI.Models.Entities;
using InvoiceAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

public class InvoiceControllerTests
{
    [Fact]
    public async Task Create_Should_Return_Ok_With_Invoice()
    {
        // Arrange
        var mockService = new Mock<IInvoiceService>();
        var invoiceDto = new InvoiceCreateDto
        {
            Description = "Test Invoice",
            Supplier = "ABC Corp",
            DueDate = DateTime.UtcNow.AddDays(7),
            Lines = new List<InvoiceLineCreateDto>
            {
                new InvoiceLineCreateDto { Description = "Line1", Price = 10, Quantity = 2 }
            }
        };

        var invoiceReadDto = new InvoiceReadDto
        {
            Description = invoiceDto.Description,
            Supplier = invoiceDto.Supplier,
            DueDate = invoiceDto.DueDate,
            Lines = invoiceDto.Lines.Select(l => new InvoiceLineReadDto
            {
                Description = l.Description,
                Price = l.Price,
                Quantity = l.Quantity
            }).ToList()
        };

        mockService.Setup(s => s.CreateInvoiceAsync(It.IsAny<InvoiceCreateDto>()))
            .ReturnsAsync(invoiceReadDto);

        var controller = new InvoiceController(mockService.Object);

        // Act
        var result = await controller.Create(invoiceDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<InvoiceReadDto>(okResult.Value);
        Assert.Equal("Test Invoice", returnValue.Description);
    }
}
