using InvoiceAPI.Infra;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;


namespace InvoiceApi.Messaging
{
    /// Publishes messages to a RabbitMQ queue.
    public class RabbitMqPublisher : IMessagePublisher
    {
        private readonly string _connectionString;
        public RabbitMqPublisher(IConfiguration config)
        {
            _connectionString = config["RabbitMQ:ConnectionString"]
                ?? throw new ArgumentNullException("RabbitMQ connection string is missing ");
        }
       
        public void Publish<T>(string queueName, T message)
        {
            try
            {
                // Create a connection factory using the provided connection string.
                var factory = new RabbitMQ.Client.ConnectionFactory
                {
                    Uri = new Uri(_connectionString)
                };

                // Establish a connection and create a channel.
                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();

                // Declare the queue to ensure it exists.
                channel.QueueDeclare(
                    queue: queueName,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                // Serialize the message to JSON and encode it as a byte array.
                var json = JsonSerializer.Serialize(message);
                var body = Encoding.UTF8.GetBytes(json);

                // Publish the message to the queue.
                channel.BasicPublish(
                    exchange: "",
                    routingKey: queueName,
                    basicProperties: null,
                    body: body);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"RabbitMQ publish failed: {ex.Message}");
                throw;
            }
        }
    }
}