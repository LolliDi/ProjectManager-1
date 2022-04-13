using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Models.Repositories
{
    class ProjectResourcesRepository : Repository<ProjectResources>
    {
        public override IQueryable<ProjectResources> Items => context.Set<ProjectResources>().Include(item => item.Resources);
    }
}
