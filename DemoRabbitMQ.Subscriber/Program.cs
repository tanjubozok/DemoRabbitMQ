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

channel.BasicQos(0, 1, false);

var consumer = new EventingBasicConsumer(channel);
channel.BasicConsume("hello-queue", false, consumer);

consumer.Received += Consumer_Received;

void Consumer_Received(object? sender, BasicDeliverEventArgs e)
{
    var message = Encoding.UTF8.GetString(e.Body.ToArray());
    Console.WriteLine($"Gelen message : {message}");

    channel.BasicAck(e.DeliveryTag, false);
}

Console.WriteLine("Mesaj okuma bitti...");
Console.ReadLine();