using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace WebApp.Models
{
    public class RoleRepository: BaseRepository
    {
        public RoleRepository(MyContext context) : base(context)
        {
        }
        public IEnumerable<Role> GetRoles()
        {
            return context.Roles.OrderByDescending(p=>p.RoleId);
        }

        public int Add(Role obj)
        {
            return context.Database.ExecuteSqlRaw("AddRole @Name", new SqlParameter("@Name", obj.RoleName));
        }
        public List<RoleChecked> GetRolesCheckedByMember(Guid id)
        {
            return context.RoleCheckeds.FromSqlRaw<RoleChecked>("GetRolesCheckedByMember @Id", new SqlParameter("@Id", id)).ToList();
        }
        public IEnumerable<Role> GetRolesByMember(Guid id)
        {
            return context.Roles.FromSqlRaw<Role>("GetRolesByMember @MemberId", new SqlParameter("@MemberId", id)).ToList();
        }
    }
}
