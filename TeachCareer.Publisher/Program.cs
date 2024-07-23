// See https://aka.ms/new-console-template for more information

using System.Text;
using RabbitMQ.Client;

Console.WriteLine("Publisher");


var connectionFactory = new ConnectionFactory
{
    Uri = new Uri("amqps://qjyihuxv:BOhytX-Ss1xHgdrZaf29J6eZa5Q8irz7@moose.rmq.cloudamqp.com/qjyihuxv")
};

using var connection = connectionFactory.CreateConnection();
using var channel = connection.CreateModel();
channel.ConfirmSelect();
channel.ExchangeDeclare("my-fanout-exchange", ExchangeType.Fanout, true, false, null);

var message = "Hello World!";

var body = Encoding.UTF8.GetBytes(message);

try
{
    channel.BasicPublish("my-fanout-exchange", "", null, body);
    channel.WaitForConfirms(TimeSpan.FromSeconds(2));
}
catch (Exception e)
{
    Console.WriteLine(e);
    throw;
}


Console.WriteLine("Mesaj gönderildi.");