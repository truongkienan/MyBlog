namespace WebApp.Models
{
    public class SiteProvider: BaseProvider
    {
        StatisticRepository statistic;
        public StatisticRepository Statistic
        {
            get
            {
                if (statistic is null)
                {
                    statistic = new StatisticRepository(Context);
                }
                return statistic;
            }
        }

        TopicRepository topic;
        public TopicRepository Topic
        {
            get
            {
                if (topic is null)
                {
                    topic = new TopicRepository(Context);
                }
                return topic;
            }
        }
        CategoryRepository category;
        public CategoryRepository Category
        {
            get
            {
                if (category is null)
                {
                    category = new CategoryRepository(Context);
                }
                return category;
            }
        }
        MemberRepository member;
        public MemberRepository Member
        {
            get
            {
                if (member is null)
                {
                    member = new MemberRepository(Context);
                }
                return member;
            }
        }
        RoleRepository role;
        public RoleRepository Role
        {
            get
            {
                if (role is null)
                {
                    role = new RoleRepository(Context);
                }
                return role;
            }
        }
        MemberInRoleRepository memberInRole;
        public MemberInRoleRepository MemberInRole
        {
            get
            {
                if (memberInRole is null)
                {
                    memberInRole = new MemberInRoleRepository(Context);
                }
                return memberInRole;
            }
        }
    }
}
