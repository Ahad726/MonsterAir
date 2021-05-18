using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterAir.Data
{
    public interface IUnitOfWork<T> : IDisposable where T : DbContext
    {
        void Save();
    }
}
