using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model;

namespace Domain.Interfaces
{
    public interface ISensorRepository : IGenericRepository<Sensor>
    {
        Task<Sensor> Update(long id, SensorDto model);
        Task<Sensor> Delete(long id);

    }
}
