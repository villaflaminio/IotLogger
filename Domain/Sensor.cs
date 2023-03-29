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

        public Sensor()
        {
            IotData = new List<Data>();
        }

        public Sensor(string sensorId)
        {
            SensorId = sensorId;
            IotData = new List<Data>();
        }
    }
}