using System.Text;

using RabbitMQ.Client;

namespace Mineshard.Api.Broker;

public class Producer
{
    private readonly ConnectionFactory _factory;
    private readonly string _queueName;
    private readonly string _routingKey;
    private readonly string _exchangeName;

    public Producer(IConfiguration config)
    {
        _exchangeName =
            config.GetValue<string>("RabbitMQ:ExchangeName")
            ?? throw new ArgumentNullException(
                nameof(config),
                "Missing ExchangeName configuration"
            );
        _queueName =
            config.GetValue<string>("RabbitMQ:QueueName")
            ?? throw new ArgumentNullException(nameof(config), "Missing QueueName configuration");
        _routingKey =
            config.GetValue<string>("RabbitMQ:RoutingKey")
            ?? throw new ArgumentNullException(nameof(config), "Missing RoutingKey configuration");

        var hostName =
            config.GetValue<string>("RabbitMQ:HostName")
            ?? throw new ArgumentNullException(nameof(config), "Missing HostName configuration");
        _factory = new ConnectionFactory() { HostName = hostName };
    }

    public void Send(Guid repoId)
    {
        using (var connection = _factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            // Declare the Exchange

            channel.ExchangeDeclare(exchange: _exchangeName, type: ExchangeType.Direct);

            // Declare the Queue
            channel.QueueDeclare(
                queue: _queueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            // Bind the queue to the exchange with the routing key
            channel.QueueBind(queue: _queueName, exchange: _exchangeName, routingKey: _routingKey);

            var body = Encoding.UTF8.GetBytes(repoId.ToString());

            // Publish the message
            channel.BasicPublish(
                exchange: _exchangeName,
                routingKey: _routingKey,
                basicProperties: null,
                body: body
            );
        }
    }
}
