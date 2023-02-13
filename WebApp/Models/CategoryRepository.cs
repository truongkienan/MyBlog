using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Models
{
    public class CategoryRepository : BaseRepository
    {

        public CategoryRepository(MyContext context) : base(context)
        {
        }

        public List<Category> GetAll()
        {
            return context.Categories.OrderBy(p => p.Position).ToList();
        }
        public List<Category> GetAllActive()
        {
            return context.Categories.Where(p=>p.Active==true).OrderBy(p => p.Position).ToList();
        }
        public Category GetCategoryByUrl(string url) 
        {
               return context.Categories.SingleOrDefault(p=>p.CategoryUrl==url);
        }
        public int UpdatePostion(short id, short postion)
        {
            string sql = "UPDATE Category SET Position = @Position WHERE CategoryId = @Id";
            SqlParameter[] sp =
            {
                new SqlParameter("@Id", id),
                new SqlParameter("@Position", postion)
            };
            return context.Database.ExecuteSqlRaw(sql, sp);
        }
        public Category GetCategoryById(byte id)
        {
            return context.Categories.FromSqlRaw<Category>("SELECT * FROM Category WHERE Category.CategoryId=@Id", new SqlParameter("@Id", id)).SingleOrDefault();
        }
        public int Edit(Category obj)
        {
            SqlParameter[] sp =
            {
                new SqlParameter("@Id", obj.CategoryId),
                new SqlParameter("@CategoryName", obj.CategoryName.Trim()),
                new SqlParameter("@CategoryUrl", obj.CategoryUrl),
                new SqlParameter("@Active", obj.Active),
            };
            return context.Database.ExecuteSqlRaw("EditCategory @Id, @CategoryName, @CategoryUrl, @Active", sp);
        }
        public int Add(Category obj)
        {
            SqlParameter[] sp =
            {
                new SqlParameter("@CategoryName", obj.CategoryName.Trim()),
                new SqlParameter("@CategoryUrl", obj.CategoryUrl),
                new SqlParameter("@Active", obj.Active),
            };
            return context.Database.ExecuteSqlRaw("AddCategory @CategoryName, @CategoryUrl, @Active", sp);
        }
        public int ShowDefault(byte id)
        {
            string sql = "UPDATE Category SET ShowDefault=0" +
                "UPDATE Category SET ShowDefault=1 WHERE CategoryId=@Id";
            return context.Database.ExecuteSqlRaw(sql, new SqlParameter("@Id", id));
        }
    }
}
