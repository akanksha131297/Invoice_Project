using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using InvoiceSubscriber.Models;
using System.Text.Json.Serialization;



class Program
{
    static void Main(string[] args)
    {
        var factory = new ConnectionFactory
        {
            Uri = new Uri("amqps://ahrucdmc:w9aootQWT5JZi2wTehyRDdLl5wUktZMs@ostrich.lmq.cloudamqp.com/ahrucdmc"),
            DispatchConsumersAsync = true
        };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        string queueName = "invoice_queue";

        channel.QueueDeclare(
            queue: queueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        Console.WriteLine(" [*] Waiting for messages in queue '{0}'. To exit press CTRL+C", queueName);

        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            try
            {
                var invoice = JsonSerializer.Deserialize<Invoice>(message);

                Console.WriteLine(" [x] Received Invoice:");
                Console.WriteLine($"     Id: {invoice.Id}");
                Console.WriteLine($"     Description: {invoice.Description}");
                Console.WriteLine($"     DueDate: {invoice.DueDate}");
                Console.WriteLine($"     Supplier: {invoice.Supplier}");

                if (invoice.Lines != null)
                {
                    foreach (var line in invoice.Lines)
                    {
                        Console.WriteLine($"    +------->    Line {line.Id}: {line.Description} - {line.Price} x {line.Quantity}");
                    }
                }

                Console.WriteLine("--------------------------------------------------");
            }
            catch (Exception ex)
            {
                Console.WriteLine(" [ERROR] Failed to deserialize message: " + ex.Message);
            }

            await System.Threading.Tasks.Task.Yield();
        };

        channel.BasicConsume(
            queue: queueName,
            autoAck: true,
            consumer: consumer);

        Console.WriteLine(" Press [enter] to exit.");
        Console.ReadLine();
    }
}
