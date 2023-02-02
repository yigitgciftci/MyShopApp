using Alisveris.Data.Entities;
using Alisveris.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alisveris.Data.Repository
{
    public class MemberRepository : Repository<Member>, IMemberRepository
    {
        private DatabaseContext _context;
        public MemberRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }
    }
}
