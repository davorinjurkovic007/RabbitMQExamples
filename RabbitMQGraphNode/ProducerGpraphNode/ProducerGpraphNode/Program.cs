// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using System.Text;

Console.WriteLine("Hello, World!");


var factory = new ConnectionFactory
{
    HostName = "localhost",
    UserName = "guest",
    Password = "guest",
    //ContinuationTimeout = TimeSpan.MaxValue,
};

Console.WriteLine("Routing key: ");
var routingKey = Console.ReadLine();

Console.WriteLine("Message: "); 
var message = Console.ReadLine();

if (message == null)
    throw new Exception("Message is null");

using (var connection = factory.CreateConnection())
{
    using (var channel = connection.CreateModel())
    {
        channel.QueueDeclare(routingKey, true, false, false, null);

        var properties = channel.CreateBasicProperties();
        byte[] messageBytes = Encoding.UTF8.GetBytes(message);
        const string exchangeName = "test-exchange";

        channel.BasicPublish(exchangeName, routingKey, properties, messageBytes);
        Console.WriteLine($"Publish message: {message}");
    }

    Console.WriteLine("The message was published.");
}