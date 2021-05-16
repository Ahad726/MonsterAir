using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonsterAir.DataSeed
{
    public interface IDbInitializer
    {
        Task InitializeAsync();
    }
}
