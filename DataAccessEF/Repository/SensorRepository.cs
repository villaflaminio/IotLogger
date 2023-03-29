using DataAccessEF.UnitOfWork;
using Domain;
using Domain.Interfaces;
using Domain.Model;
using Google.Protobuf.WellKnownTypes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessEF.TypeRepository
{
    class SensorRepository : GenericRepository<Sensor>, ISensorRepository
    {
        public SensorRepository(IotContext context) : base(context)
        {
        }

        public async Task<Sensor> Update(long id, SensorDto model)
        {
            Sensor? sensor = context.Sensor.FirstOrDefault(x => x.Id == id);

            if (sensor == null)
            {
                throw new NotFoundException("sensor is not found");
            }

            sensor.SensorId = model.SensorId;
            DbSet<Sensor> set = context.Set<Sensor>();
            set.Attach(sensor);
            context.Entry(sensor).State = EntityState.Modified;
                       
            await context.SaveChangesAsync();
            return sensor;
        }

        public async Task<Sensor> Delete(long id)
        {
            var sensor = context.Sensor.FirstOrDefault(u => u.Id == id);

            if (sensor == null)
            {
                throw new NotFoundException("sensor is not found");
            }

            context.Sensor.Remove(sensor);
            await context.SaveChangesAsync();
            return sensor;
        }
    }
}

