using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMqSenderProject.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMqSenderProject.Control
{
    public class RabbitMqConnection
    {
        private ConnectionFactory connectionFactory;
        private ConnectionModel conModel;
        private readonly string connectionPath = "C://Users/MONSTER/source/repos/RabbitMqSenderProject/RabbitMqSenderProject/Data/connection.json";


        public RabbitMqConnection()
        {

            StreamReader streamReader = new StreamReader(connectionPath, Encoding.Default, false);

            string connectionJson = streamReader.ReadToEnd();

            conModel = JsonConvert.DeserializeObject<ConnectionModel>(connectionJson);
            connectionFactory = new ConnectionFactory() { HostName = conModel.HostName, UserName = conModel.UserName, Password = conModel.Password };
        }

        public IConnection OpenRabbitConnection()
        {

            IConnection connection = connectionFactory.CreateConnection();
            return connection;
        }

        public void SendMessageToQuery(string message, string queue, string routingKey , bool durable = false, bool exclusive = false, IDictionary<string, object> arguments = null, string exchange = "", IBasicProperties basicProperties = null)
        {
            var connection = OpenRabbitConnection();
            IModel channel = connection.CreateModel();
            channel.QueueDeclare(queue: queue, durable: durable, exclusive: exclusive, arguments: arguments);

            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: exchange, routingKey: routingKey, basicProperties: basicProperties, body: body);

            Console.WriteLine(message + " gonderildi");

            Console.ReadLine();

        }
    }
}
