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
    public class OrderReadRepository : ReadRepository<Order>, IOrderReadRepository
    {
        public OrderReadRepository(ETradeAPIDBContext context) : base(context)
        {
        }
    }
}
