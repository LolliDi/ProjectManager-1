using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Models.Repositories
{
    class RolesRepository : Repository<Roles>
    {
        public RolesRepository(ProjectManagerContext context) : base(context) { }

        public override IQueryable<Roles> Items => context.Set<Roles>();
    }
}
