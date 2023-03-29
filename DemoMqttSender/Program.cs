using Domain.Model;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace DemoMqttSender
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Thread publisher = new Thread(Publisher.Run);
            publisher.Start();

        }

        public class Publisher
        {
            private static int[] requestPerSecond = { 100, 45, 20, 15, 1 };

            public static async void Run()

            {   ///https://github-wiki-see.page/m/chkr1011/MQTTnet/wiki/Client

                MqttFactory mqttFactory = new MqttFactory();


                IMqttClient client = mqttFactory.CreateMqttClient();


                var options = new MqttClientOptionsBuilder()
                               .WithClientId(Guid.NewGuid().ToString())
                               .WithProtocolVersion(MQTTnet.Formatter.MqttProtocolVersion.V310)
                               .WithTcpServer("localhost", 8883)
                               .WithCleanSession(false)
                               .WithSessionExpiryInterval(0)
                               .WithWillDelayInterval(1)
                               .WithKeepAlivePeriod(TimeSpan.FromSeconds(20))
                               .WithCredentials("root", "flaminio")
                               .Build();

                client.UseConnectedHandler(e =>
                {
                    Console.WriteLine("publisher connected to broker");
                });
                client.UseDisconnectedHandler(e =>
                {
                    //Console.WriteLine(mqttFactory.GetHashCode + "Disconnect");
                });

                await client.ConnectAsync(options);


                while (!client.IsConnected)
                {
                    Task.Delay(100).Wait();
                    //Console.WriteLine(mqttFactory.GetHashCode + "Disconnect");
                }

                ///
                /// Test invio dati verso i subscriber
                ///
                for (int i = 1; i < 1001; i++)
                {
                    PublishMessageAsync(client, i);
                    Task.Delay(100).Wait();
                }


            }

            private static async Task PublishMessageAsync(IMqttClient client, int i)
            {
                String[] typeOfInterface = { "IWorker", "ISpecialWorker" };
                Random random = new Random();

                Data data = new Data();
                IFormatter formatter = new BinaryFormatter();

                //string mex = "Hello " + i + " Time_Send: " + DateTime.Now;Random rn = new Random();
                string mex = i + "";

                var message = new MqttApplicationMessageBuilder()
                    .WithTopic("flaminio")
                    .WithPayload(SerializeToString(data))
                    .WithAtLeastOnceQoS()
                    .Build();

                if (client.IsConnected)
                {
                    await client.PublishAsync(message);
                }

            }

            private static string SerializeToString<TData>(TData settings)
            {
                using (var stream = new MemoryStream())
                {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(stream, settings);
                    stream.Flush();
                    stream.Position = 0;
                    return Convert.ToBase64String(stream.ToArray());
                }
            }

        }

    }
}
