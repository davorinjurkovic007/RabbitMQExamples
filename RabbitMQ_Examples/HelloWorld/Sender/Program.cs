﻿// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using System.Text;

Console.WriteLine("Hello, World!");


var factory = new ConnectionFactory
{
    HostName = "localhost",
};

using var connection = factory.CreateConnection();

using var channel = connection.CreateModel();

channel.QueueDeclare(queue: "hello",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

const string mesage = "Hello World!";
var body = Encoding.UTF8.GetBytes(mesage);

channel.BasicPublish(exchange: string.Empty,
                    routingKey: "hello",
                    basicProperties: null,
                    body: body);
Console.WriteLine($" [x] Send {mesage}");

Console.WriteLine("Press [enter] to exit.");
Console.ReadLine();