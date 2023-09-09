using ETrade.Application.Abstractions;
using ETrade.Domain.Entities;
using ETrade.Persistance.Contexts;
using ETrade.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.Persistance.Concretes
{
    public class OrderWriteRepository : WriteRepository<Order>, IOrderWriteRepository
    {
        public OrderWriteRepository(ETradeAPIDBContext context) : base(context)
        {
        }
    }
}
