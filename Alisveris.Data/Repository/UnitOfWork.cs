using Alisveris.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alisveris.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
        }

        public ICategoryRepository Category => new CategoryRepository(_context);
        public IProductRepository Product => new ProductRepository(_context);
        public IOrderRepository Order => new OrderRepository(_context);
        public IMemberRepository Member => new MemberRepository(_context);
        public IAdminRepository Admin => new AdminRepository(_context);


        public void Dispose()
        {
            _context.Dispose();
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
