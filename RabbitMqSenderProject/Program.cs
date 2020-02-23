using Newtonsoft.Json;
using RabbitMqSenderProject.Control;
using RabbitMqSenderProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMqSenderProject
{
    class Program
    {
        static void Main(string[] args)
        {
            RabbitMqConnection rmqConnection = new RabbitMqConnection();
            Person person = new Person() { Id = 304011601, Name = "Mehmed Fatih", SurName = "Temiz", BirthDate = new DateTime(1988, 10, 25), Message = "Nesne Takip Projesi Tam Gaz devam Ediyor", MessageSendTime = DateTime.Now };
            rmqConnection.SendMessageToQuery(JsonConvert.SerializeObject(person) , "fatihinAnahtari" , "fatihinAnahtari");
        }
    }
}
