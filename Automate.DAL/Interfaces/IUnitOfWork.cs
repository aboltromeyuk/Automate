using Automate.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automate.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Drink> Drinks { get; }
        IRepository<Coin> Coins { get; }
        void Save();
    }
}
