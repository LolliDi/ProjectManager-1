using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ProjectManager.Models.Repositories
{
    class ProjectsRepository : Repository<Projects>
    {
        public override IQueryable<Projects> Items => context.Set<Projects>().Include(item => item.Users);
    }
}
