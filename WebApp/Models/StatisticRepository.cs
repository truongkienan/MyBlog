using Microsoft.EntityFrameworkCore;

namespace WebApp.Models
{
    public class StatisticRepository : BaseRepository
    {
        public StatisticRepository(MyContext context) : base(context)
        {
        }
        public List<StatisticTopic> GetStatisticTopics()
        {
            string sql = "SELECT Category.CategoryName , COUNT(*) AS TopicCount FROM Category " +
                "JOIN Topic ON Category.CategoryUrl = Topic.CategoryUrl " +
                "GROUP BY Category.CategoryName";
            return context.StatisticTopics.FromSqlRaw<StatisticTopic>(sql).ToList();
        }
    }
}
