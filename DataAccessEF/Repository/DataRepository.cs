using Domain;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessEF.TypeRepository
{
    class DataRepository : GenericRepository<Data>, IDataRepository
    {
        public DataRepository(IotContext context) : base(context)
        {

        }
    }
}

