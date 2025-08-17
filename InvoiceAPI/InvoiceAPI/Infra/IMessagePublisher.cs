namespace InvoiceAPI.Infra
{
    public interface IMessagePublisher
    {
        void Publish<T>(string queueName, T message);
    }
}
