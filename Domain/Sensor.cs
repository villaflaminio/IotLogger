using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain
{
    /// <summary>
    /// Class used in business logic to represent user.
    /// </summary>
    public class Sensor
    {
        public long Id { get; set; }
        public string SensorId { get; set; }
        public List<Data> IotData { get; set; }
    }
}