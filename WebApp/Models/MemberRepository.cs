using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace WebApp.Models
{
    public class MemberRepository: BaseRepository
    {
        public MemberRepository(MyContext context) : base(context)
        {
        }
        public ResponseLogin Login(LoginModel obj)
        {
            SqlParameter[] sp = {
                new SqlParameter("@Username", obj.Username.Trim()),
                new SqlParameter("@Password", Helper.Hash(obj.Username + "!@#$%" + obj.Password))
            };
            var list= context.ResponseLogins.FromSqlRaw<ResponseLogin>("LoginMember @Username, @Password", sp).ToList();
            return list.SingleOrDefault();
        }

        public IEnumerable<Member> GetMembers()
        {
            return context.Members.Select(p => new Member
            {
                MemberId = p.MemberId,
                Username = p.Username,
                Email = p.Email,
                Fullname = p.Fullname
            });
        }

        public int Add(Member obj)
        {
            SqlParameter[] sp =
            {
                new SqlParameter("@Username", obj.Username.Trim()),
                new SqlParameter("@Password", Helper.Hash(obj.Username + "!@#$%" + obj.Password)),
                new SqlParameter("@Email", obj.Email),
                new SqlParameter("@Fullname", obj.Fullname)
            };
            return context.Database.ExecuteSqlRaw("AddMember @Username, @Password, @Email, @Fullname", sp);
        }
        public Member GetMemberById(Guid id)
        {
            return context.Members.Select(p=> new Member
            {
                MemberId=p.MemberId,
                Username=p.Username
            }).Where(p=>p.MemberId==id).SingleOrDefault();
        }
        public int Change(ChangeModel obj)
        {
            SqlParameter[] sp = {
                new SqlParameter("@Username", obj.Username.Trim()),
                new SqlParameter("@OldPassword", Helper.Hash(obj.Username + "!@#$%" + obj.OldPassword)),
                new SqlParameter("@NewPassword", Helper.Hash(obj.Username + "!@#$%" + obj.NewPassword))
            };
            return context.Database.ExecuteSqlRaw("UPDATE Member SET Password=@NewPassword WHERE Username=@Username AND Password=@OldPassword", sp);
        }
    }
}
