using Microsoft.EntityFrameworkCore;
using MonsterAir.Core.Entities;
using MonsterAir.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterAir.Core.Repositories
{
    public class JourneyRepository : Repository<JourneyHistory> , IJourneyRepository
    {
        public JourneyRepository(DbContext dbContext)
            :base(dbContext)
        {

        }

    }
}
