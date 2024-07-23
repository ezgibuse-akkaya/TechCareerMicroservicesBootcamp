// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

Console.WriteLine("Mesajlar dinleniyor.");
var connectionFactory = new ConnectionFactory
{
    Uri = new Uri("amqps://qjyihuxv:BOhytX-Ss1xHgdrZaf29J6eZa5Q8irz7@moose.rmq.cloudamqp.com/qjyihuxv")
};

using var connection = connectionFactory.CreateConnection();
using var channel = connection.CreateModel();


channel.QueueDeclare("my-queue", true, true, false, null);


channel.QueueBind("my-queue", "my-fanout-exchange", "", null);

var consumer = new EventingBasicConsumer(channel);


consumer.Received += MessageReceived;


void MessageReceived(object? sender, BasicDeliverEventArgs args)
{
    var message = Encoding.UTF8.GetString(args.Body.ToArray());
    Console.WriteLine($"Gelen Mesaj: {message}");
    channel.BasicAck(args.DeliveryTag, false);
}


channel.BasicConsume("my-queue", false, consumer);


Console.ReadLine();