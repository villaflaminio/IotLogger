using DataAccessEF.TypeRepository;
using Domain.Interfaces;

namespace DataAccessEF.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private IotContext context;
        public ISensorRepository Sensor { get; private set; }
        public IDataRepository Data { get; private set; }
        public UnitOfWork(IotContext context)
        {
            this.context = context;
            Sensor = new SensorRepository(this.context);
            Data = new DataRepository(this.context);
        }

       

        public void Dispose()
        {
            context.Dispose();
        }

        public int Save()
        {
            return context.SaveChanges();
        }
    }
}
