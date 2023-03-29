namespace Domain.Model
{
    /// <summary>
    /// Class used in business logic to represent user.
    /// </summary>
    public class Data
    {
        public long Id { get; set; }

        public double VoltageValue { get; set; }

        public double CurrentValue { get; set; }

        public double PowerValue { get; set; }

        public double EnergyValue { get; set; }

        public DateTime Date { get; set; }

        public DateTime Time { get; set; }

    }
}