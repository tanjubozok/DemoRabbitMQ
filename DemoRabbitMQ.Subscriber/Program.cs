using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory connectionFactory = new()
{
    Uri = new Uri("amqps://kdlvxpkh:Nx1oLT7msL9WqSA-L2TVoS_ry8wSbFlv@whale.rmq.cloudamqp.com/kdlvxpkh")
};

using var connection = connectionFactory.CreateConnection();
var channel = connection.CreateModel();
channel.QueueDeclare("hello-queue", true, false, false);

var consumer = new EventingBasicConsumer(channel);
channel.BasicConsume("hello-queue", true, consumer);

consumer.Received += Consumer_Received;

void Consumer_Received(object? sender, BasicDeliverEventArgs e)
{
    var message = Encoding.UTF8.GetString(e.Body.ToArray());
    Console.WriteLine($"Gelen message : {message}");
}

Console.WriteLine("Mesaj okuma bitti...");
Console.ReadLine();