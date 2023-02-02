using Alisveris.Business.Common;
using Alisveris.Business.Models.Admin;
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
    public interface IAdminManager
    {
        Admin Authenticate(LoginAdminModel model);
    }
    public class AdminManager : IAdminManager
    {
        private DatabaseContext _context;

        public AdminManager(DatabaseContext context)
        {
            _context = context;
        }

        public Admin Authenticate(LoginAdminModel model)
        {
            Admin admin = _context.Admins.FirstOrDefault(x => x.Username == model.Username && x.Password == model.Password);

            return admin;
        }
    }
}
