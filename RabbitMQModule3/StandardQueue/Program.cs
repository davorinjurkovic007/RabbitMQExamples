using Common;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Runtime.InteropServices.ObjectiveC;
using System.Text;
using System.Text.Json;

ConnectionFactory _factory;
IConnection _connection;
IModel _model;
string QueueName = "StandardQueue_ExampleQueue";

Console.WriteLine("Hello, World!");

var payment1 = new Payment { AmountToPay = 25.0m, CardNumber = "1234123412341234", Name = "Mr S Haunts" };
var payment2 = new Payment { AmountToPay = 5.0m, CardNumber = "1234123412341234", Name = "Mr S Haunts" };
var payment3 = new Payment { AmountToPay = 2.0m, CardNumber = "1234123412341234", Name = "Mr S Haunts" };
var payment4 = new Payment { AmountToPay = 17.0m, CardNumber = "1234123412341234", Name = "Mr S Haunts" };
var payment5 = new Payment { AmountToPay = 300.0m, CardNumber = "1234123412341234", Name = "Mr S Haunts" };
var payment6 = new Payment { AmountToPay = 350.0m, CardNumber = "1234123412341234", Name = "Mr S Haunts" };
var payment7 = new Payment { AmountToPay = 295.0m, CardNumber = "1234123412341234", Name = "Mr S Haunts" };
var payment8 = new Payment { AmountToPay = 5625.0m, CardNumber = "1234123412341234", Name = "Mr S Haunts" };
var payment9 = new Payment { AmountToPay = 5.0m, CardNumber = "1234123412341234", Name = "Mr S Haunts" };
var payment10 = new Payment { AmountToPay = 12.0m, CardNumber = "1234123412341234", Name = "Mr S Haunts" };

CreateQueue();

SendMessage(payment1);
SendMessage(payment2);
SendMessage(payment3);
SendMessage(payment4);
SendMessage(payment5);
SendMessage(payment6);
SendMessage(payment7);
SendMessage(payment8);
SendMessage(payment9);
SendMessage(payment10);


void CreateQueue()
{
    _factory = new ConnectionFactory { HostName = "localhost", UserName = "guest", Password = "guest"};
    _connection = _factory.CreateConnection();
    _model = _connection.CreateModel();

    _model.QueueDeclare(QueueName, true, false, false, null);
}

void SendMessage(Payment message)
{
    _model.BasicPublish("", QueueName,   null, message.Serialize());
    Console.WriteLine(" [X] Payment Mesage Sent : {0} : {1} :{2}", message.CardNumber, message.AmountToPay, message.Name);
}

void Recieve()
{
    var consumer = new EventingBasicConsumer(_model);
    
    
}

uint GetMessageCount(IModel channel, string queueName)
{
    var results = channel.QueueDeclare(queueName, true, false, false, null);
    return results.MessageCount;
}

void OnMessagereceived(object sender, BasicDeliverEventArgs e)
{
    Console.WriteLine($"Message: {Encoding.UTF8.GetString(e.Body)}");
    Console.WriteLine("Press any key to stop consuming message.");
}