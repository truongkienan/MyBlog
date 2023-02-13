using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.EntityFrameworkCore;
namespace WebApp.Models
{
    public class TopicRepository:BaseRepository
    {
        public TopicRepository(MyContext context) : base(context)
        {
        }

        Dictionary<string, List<Topic>> GetAll()
        {
            IEnumerable<Topic> topics = context.Topics.FromSqlRaw<Topic>("SELECT CategoryName=null, Topic.* FROM Topic");

            Dictionary<string, List<Topic>> dict = new Dictionary<string, List<Topic>>();
            foreach (Topic item in topics)
            {
                string k = item.CategoryUrl;
                if (!dict.ContainsKey(k))
                {
                    dict[k] = new List<Topic>();    
                }
                dict[k].Add(item);
            }
            return dict;
        }
        Dictionary<string, List<Topic>[]> GetAllWithColumn()
        {
            string sql = "SELECT CategoryName=null, Topic.* FROM Category " +
                "JOIN Topic ON Category.CategoryUrl=Topic.CategoryUrl " +
                "WHERE Category.Active=1 AND Topic.Active=1 ORDER BY CategoryUrl, ColumnIndex, Position";

            IEnumerable<Topic> topics = context.Topics.FromSqlRaw<Topic>(sql);

            Dictionary<string, List<Topic>[]> dict = new Dictionary<string, List<Topic>[]>();
            foreach (Topic item in topics)
            {
                string k = item.CategoryUrl;
                if (!dict.ContainsKey(k))
                {
                    dict[k] = new List<Topic>[4];
                    for (int i = 0; i < 4; i++)
                    {
                        dict[k][i] = new List<Topic>();
                    }
                }
                dict[k][item.ColumnIndex].Add(item);
            }
            return dict;
        }
        public int Edit(Topic obj)
        {           
            SqlParameter[] sp =
            {
                new SqlParameter("@Id", obj.TopicId),
                new SqlParameter("@TopicName", obj.TopicName.Trim()),
                new SqlParameter("@TopicUrl", Helper.GenerateSlug(obj.TopicName)),
                new SqlParameter("@CategoryUrl", obj.CategoryUrl),
                new SqlParameter("@Content", obj.Content.Trim()),
                new SqlParameter("@ColumnIndex", obj.ColumnIndex),
                new SqlParameter("@Active", obj.Active),
            };

            return context.Database.ExecuteSqlRaw("EditTopic @Id, @TopicName, @TopicUrl, @CategoryUrl, @Content, @ColumnIndex, @Active", sp);
        }
        public int Add(Topic obj)
        {
            SqlParameter[] sp =
            {
                new SqlParameter("@TopicName", obj.TopicName.Trim()),
                new SqlParameter("@TopicUrl", Helper.GenerateSlug(obj.TopicName)),
                new SqlParameter("@CategoryUrl", obj.CategoryUrl),
                new SqlParameter("@Content", obj.Content.Trim()),
                new SqlParameter("@ColumnIndex", obj.ColumnIndex),
                new SqlParameter("@Active", obj.Active),
            };
            return context.Database.ExecuteSqlRaw("AddTopic @TopicName, @TopicUrl, @CategoryUrl, @Content, @ColumnIndex, @Active", sp);
        }
        public Topic GetTopicById(short id)
        {
            string sql = "SELECT Category.CategoryName, Topic.* FROM Category " +
                "JOIN Topic ON Category.CategoryUrl = Topic.CategoryUrl " +
                "WHERE Topic.TopicId=@Id";
          
            return context.Topics.FromSqlRaw<Topic>(sql, new SqlParameter("@Id", id)).SingleOrDefault();
        }
        public CategoryAndTopic GetTopicByUrl(string categoryUrl, string topicUrl)
        {
            var param = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@CategoryUrl",
                            Value = categoryUrl
                        },
                        new SqlParameter() {
                            ParameterName = "@TopicUrl",
                            Value = topicUrl
                        }};
            string sql = "SELECT Category.CategoryId, Category.CategoryName, Topic.* FROM Category " +
                "JOIN Topic ON Category.CategoryUrl = Topic.CategoryUrl " +
                "WHERE Category.CategoryUrl=@CategoryUrl AND Topic.TopicUrl=@TopicUrl";

            return context.CategoryAndTopics.FromSqlRaw<CategoryAndTopic>(sql, param).SingleOrDefault();
        }
        public Tuple<Category, IEnumerable<Topic>> GetTopicsByCategory(string url)
        {
            var param = new SqlParameter()
            {
                ParameterName = "@CategoryUrl",
                Value = url
            };

            Category category=new Category();
            List<Topic> topics = new List<Topic>();

            using (var cnn = context.Database.GetDbConnection())
            {
                using (IDbCommand cmm = cnn.CreateCommand())
                {
                    cmm.CommandType = System.Data.CommandType.StoredProcedure;
                    cmm.CommandText = "GetTopicsByCategory";
                    cmm.Parameters.Add(param);
                    cmm.Connection = cnn;
                    cnn.Open();
                    var reader = cmm.ExecuteReader();

                    while (reader.Read())
                    {
                        category = new Category
                        {
                            CategoryId = (byte)reader["CategoryId"],
                            CategoryName = (string)reader["CategoryName"],
                            CategoryUrl = (string)reader["CategoryUrl"]
                        };
                    }
                    reader.NextResult();
                    while (reader.Read())
                    {
                        topics.Add(new Topic
                        {
                            TopicId = (short)reader["TopicId"],
                            TopicName = (string)reader["TopicName"],
                            TopicUrl = (string)reader["TopicUrl"],
                            CategoryUrl = (string)reader["CategoryUrl"],
                            ColumnIndex = (byte)reader["ColumnIndex"],
                            Position = (short)reader["Position"],
                            Active = (bool)reader["Active"],
                        });
                    }
                }
            }
            return new Tuple<Category, IEnumerable<Topic>>(category, topics);
        }
        public Tuple<Category, List<Topic>[]> GetTopicsByCategoryWithColumn(string url)
        {
            Category item = context.Categories.SingleOrDefault(p => p.CategoryUrl == url);
            return GetTopics(item);
        }
        public Tuple<Category, List<Topic>[]> GetByActive() 
        {
            Category item = context.Categories.FirstOrDefault(p => p.ShowDefault == true);
            if(item==null)
            {
                item = context.Categories.First();
            }
            return GetTopics(item);
        }
        Tuple<Category, List<Topic>[]> GetTopics(Category ct)
        {
            if (GetAllWithColumn().ContainsKey(ct.CategoryUrl))
            {
                return new Tuple<Category, List<Topic>[]>(ct, GetAllWithColumn()[ct.CategoryUrl]);
            }
            return new Tuple<Category, List<Topic>[]>(ct, null);
        }
        public int UpdatePostion(short id, short postion)
        {
            string sql = "UPDATE Topic SET Position = @Position WHERE TopicId = @Id";
            SqlParameter[] sp =
            {
                new SqlParameter("@Id", id),
                new SqlParameter("@Position", postion)
            };
            return context.Database.ExecuteSqlRaw(sql, sp);
        }
    }
}
