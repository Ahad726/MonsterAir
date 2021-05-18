using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MonsterAir.Core.Entities;

namespace MonsterAir.Core.Contexts
{
    public interface IAirContext
    {
        DbSet<Flight> Flights { get; set; }
    }
}
