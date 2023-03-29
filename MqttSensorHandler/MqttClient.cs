using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Domain.Model;
using log4net;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using MQTTnet.Diagnostics;

namespace Iot.Mqtt.Sensor.Handler
{
    public class MqttClient
    {
        // Getting logger.
        private static readonly ILog _logger = LogManager.GetLogger(typeof(MqttClient));
        private IMqttClient _mqttClientThread;

        public bool IsConnected => throw new NotImplementedException();

        public MqttClientOptions Options => throw new NotImplementedException();

        #region Constructor 
        public MqttClient()
        {
            try
            {
                _logger.Debug("Enter MqttClient constructor");

                MqttFactory mqttFactory = new MqttFactory();
                _mqttClientThread = mqttFactory.CreateMqttClient();
            }
            catch (Exception error)
            {
                _logger.Error(error);
                throw error;
            }
        }

        #endregion

        #region event of MqttClient


        #endregion

        #region method of MqttClient

        public async void StartConnectionAsync() //https://github-wiki-see.page/m/chkr1011/MQTTnet/wiki/Client
        {
            try
            {
                var options = new MqttClientOptionsBuilder()
                        .WithClientId(Guid.NewGuid().ToString())
                         .WithProtocolVersion(MQTTnet.Formatter.MqttProtocolVersion.V500)
                         .WithTcpServer("https://flespi.io/mqtt/", 1883)
                         .WithCleanSession(false)
                         .WithSessionExpiryInterval(0)
                         .WithWillDelayInterval(1)
                         .WithKeepAlivePeriod(TimeSpan.FromSeconds(20))
                         .WithCredentials("nviBGPvy8Oc53mkZA1TQlWw8nDNMECWQV4fpJAOGcOB5YJUmBD1MqnJBRkblMX1w", "")
                         //  .WithTls(tlsOptions)
                         .Build();



                _mqttClientThread.UseConnectedHandler(e =>
                {

                    Console.WriteLine("subscriber save connesso con mosquitto");
                    var topicFilter = new TopicFilterBuilder()
                        .WithTopic("#")
                        .Build();
                    _mqttClientThread.SubscribeAsync(topicFilter);
                });
                _mqttClientThread.UseDisconnectedHandler(e =>
                {
                    Console.WriteLine("Disconnect");
                });

                _mqttClientThread.UseApplicationMessageReceivedHandler(e =>
                {

                    MessageMqtt message = new MessageMqtt(Encoding.UTF8.GetString(e.ApplicationMessage.Payload), e.ApplicationMessage.Topic, DateTime.Now);
                    //message.Payload += " id Subscriber: " + id;
                    Console.WriteLine(message.Topic);

                });


                _mqttClientThread.ConnectAsync(options);
            }
            catch (Exception e)
            {
                _logger.Debug("Error starting MqttClient");
                _logger.Error(e);
            }
        }

        #endregion

    }
}