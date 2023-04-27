using System;
using System.Text;
using System.Collections.Generic;

using Mineshard.Analyzer.Controllers;
using Mineshard.Analyzer.Core;
using Mineshard.Persistence.Repos;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Mineshard.Analyzer;

public class Worker : IDisposable
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private string _hostName;
    private string _queueName;

    public Worker(IConfiguration config)
    {
        _hostName =
                config.GetValue<string>("RabbitMQ:HostName")
                ?? throw new ArgumentNullException(
                    nameof(config),
                    "Missing HostName configuration"
                );

        _queueName =
                config.GetValue<string>("RabbitMQ:QueueName")
                ?? throw new ArgumentNullException(
                    nameof(config),
                    "Missing QueueName configuration"
                );

        var factory = new ConnectionFactory() { HostName = _hostName };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: _queueName,
                             durable: true,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);
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
            // Execute here the actual code
            Console.WriteLine("Received message: {0}, from {1}", message, Environment.CurrentManagedThreadId);
            Thread.Sleep(10_000);
            Console.WriteLine("Finished message: {0}, from {1}", message, Environment.CurrentManagedThreadId);
        };
        _channel.BasicConsume(queue: _queueName,
                                autoAck: true,
                                consumer: consumer);
    }

    public void Dispose()
    {
        _channel.Dispose();
        _connection.Dispose();
        GC.SuppressFinalize(this);
    }
}
