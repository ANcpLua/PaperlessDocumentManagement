namespace PaperlessDocumentManagement.BusinessServices.Interfaces
{
    public interface IQueueService
    {
        Task PublishAsync<T>(string queueName, T message);
        Task<T> ConsumeAsync<T>(string queueName) where T : class;
        Task AcknowledgeAsync(string queueName, ulong deliveryTag);
        Task RejectAsync(string queueName, ulong deliveryTag, bool requeue = true);
    }
}
