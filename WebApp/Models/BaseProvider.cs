namespace WebApp.Models
{
    public abstract class BaseProvider : IDisposable
    {
        MyContext context;
        protected MyContext Context
        {
            get
            {
                if (context is null)
                {
                    context = new MyContext();
                }
                return context;
            }
        }

        public void Dispose()
        {
            if (context != null)
            {
                context.Dispose();
            }
        }
    }
}
