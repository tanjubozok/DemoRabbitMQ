using RabbitMQ.Client;
using System.Text;

ConnectionFactory connectionFactory = new()
{
    Uri = new Uri("amqps://kdlvxpkh:Nx1oLT7msL9WqSA-L2TVoS_ry8wSbFlv@whale.rmq.cloudamqp.com/kdlvxpkh")
};

using var connection = connectionFactory.CreateConnection();
var channel = connection.CreateModel();
channel.QueueDeclare("hello-queue", true, false, false);

var list = Enumerable.Range(1, 100).ToList();
foreach (var item in list)
{
    string mesage = $"{item} : Lorem ipsum dolor";
    var messageBody = Encoding.UTF8.GetBytes(mesage);
    channel.BasicPublish(string.Empty, "hello-queue", null, messageBody);
    Console.WriteLine("Mesaj gönderildi");
};

Console.ReadLine();
