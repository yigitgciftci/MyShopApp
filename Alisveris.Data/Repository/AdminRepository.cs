using Alisveris.Data.Entities;
using Alisveris.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alisveris.Data.Repository
{
    public class AdminRepository : Repository<Admin>, IAdminRepository
    {
        private DatabaseContext _context;
        public AdminRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }
    }
}
