using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Models
{
    public class MemberInRoleRepository : BaseRepository
    {
        public MemberInRoleRepository(MyContext context) : base(context) { }
        public int Save(MemberInRole obj)
        {
            SqlParameter[] sp =
            {
                new SqlParameter("@MemberId", obj.MemberId),
                new SqlParameter("@RoleId", obj.RoleId)
            };
         
            return context.Database.ExecuteSqlRaw("SaveMemberInRole @MemberId, @RoleId", sp);
        }
    }
}
