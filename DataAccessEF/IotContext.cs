using Domain;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net;

namespace DataAccessEF
{
    public class IotContext  : DbContext
    {
        public IotContext(DbContextOptions options) : base(options) { }
        public DbSet<Sensor> Sensor { get; set; }
        public DbSet<Data> Data { get; set; }

    }
}
