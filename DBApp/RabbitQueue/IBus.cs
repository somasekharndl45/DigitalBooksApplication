namespace CommonUtilities.RabbitQueue
{
    public interface IBus
    {
        Task SendSync<T>(string queue, T message); 
        Task ReceiveAsync<T>(string queue, Action<T> onMessage);

    }
}
