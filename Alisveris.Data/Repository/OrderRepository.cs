using Alisveris.Data.Entities;
using Alisveris.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alisveris.Data.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private DatabaseContext _context;
        public OrderRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }
    }
}
