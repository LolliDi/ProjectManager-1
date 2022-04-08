using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Models.Repositories
{
    class UsersRepository : Repository<Users>
    {
        public override IQueryable<Users> Items => context.Set<Users>().Include(item => item.Persons).Include(item => item.Roles);
    }
}
