using Alisveris.Business.Models.Admin;
using Alisveris.Business.Models.Member;
using Alisveris.Data;
using Alisveris.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alisveris.Business
{
    public interface IMemberManager
    {
        Member Authenticate(LoginMemberModel model);
    }
    public class MemberManager : IMemberManager
    {
        private DatabaseContext _context;

        public MemberManager(DatabaseContext context)
        {
            _context = context;
        }

        public Member Authenticate(LoginMemberModel model)
        {
            Member member = _context.Members.SingleOrDefault(x => x.Email == model.Email && x.Password == model.Password);

            return member;
        }
    }
}
