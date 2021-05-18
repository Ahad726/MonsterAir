using Microsoft.EntityFrameworkCore;
using MonsterAir.Core.Contexts;
using MonsterAir.Core.Entities;
using MonsterAir.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterAir.Core.Repositories
{
    public class FlightRepository : Repository<Flight>, IFlightRepository
    {
        public FlightRepository(DbContext dbContext)
            :base(dbContext)
        {

        }
    }
}
