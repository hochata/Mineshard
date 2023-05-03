using System;
using System.Collections.Generic;
using System.Text;

using Mineshard.Analyzer.Controllers;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Mineshard.Analyzer.Broker;

public class Worker : IDisposable
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly IReportsController _controller;
    private readonly string _hostName;
    private readonly string _queueName;

    public Worker(IConfiguration config, IReportsController controller)
    {
        _hostName =
            config.GetValue<string>("RabbitMQ:HostName")
            ?? throw new ArgumentNullException(nameof(config), "Missing HostName configuration");

        _queueName =
            config.GetValue<string>("RabbitMQ:QueueName")
            ?? throw new ArgumentNullException(nameof(config), "Missing QueueName configuration");

        var factory = new ConnectionFactory() { HostName = _hostName };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(
            queue: _queueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );

        _controller = controller;
    }

    public void StartConsuming()
    {
        // Start consuming messages from the queue
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (model, ea) =>
        {
            // Get the message body as a string
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            // Print the message to the console
            Console.WriteLine(
                "Received message: {0}, from {1}",
                message,
                Environment.CurrentManagedThreadId
            );

            _controller.RunAnalysis(Guid.Parse(message));
        };
        _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);
    }

    public void Dispose()
    {
        _channel.Dispose();
        _connection.Dispose();
        GC.SuppressFinalize(this);
    }
}
