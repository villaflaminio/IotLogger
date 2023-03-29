using System;

namespace Domain.Model
{
    /// <summary>
    /// Class used in business logic to represent user.
    /// </summary>
    /// 
    [Serializable]
    public class Data
    {
        public long Id { get; set; }

        public double VoltageValue { get; set; }

        public double CurrentValue { get; set; }

        public double PowerValue { get; set; }

        public double EnergyValue { get; set; }

        public DateTime Date { get; set; }

        public DateTime Time { get; set; }


        public Data()
        {
            this.Id = 0;
            this.VoltageValue = 0;
            this.CurrentValue = 0;
            this.PowerValue = 0;
            this.EnergyValue = 0;
            this.Date = DateTime.Now;
            this.Time = DateTime.Now;
        }


        public Data(long id, double voltageValue, double currentValue, double powerValue, double energyValue, DateTime date, DateTime time)
        {
            this.Id = id;
            this.VoltageValue = voltageValue;
            this.CurrentValue = currentValue;
            this.PowerValue = powerValue;
            this.EnergyValue = energyValue;
            this.Date = date;
            this.Time = time;
        }

        public static Data GenerateRandomData()
        {
            Data data = new Data();
            {
                Random random = new Random();
                data.VoltageValue = random.NextDouble();
                data.CurrentValue = random.NextDouble();
                data.PowerValue = random.NextDouble();
                data.EnergyValue = random.NextDouble();
                data.Date = DateTime.Now;
                data.Time = DateTime.Now;
            }

            return data;
        }

    }
}