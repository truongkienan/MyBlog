namespace WebApp.Models
{
    public abstract class BaseRepository
    {
        protected MyContext context;
        public BaseRepository(MyContext context)
        {
            this.context = context;
        }
        public BaseRepository() { }
    }
}
