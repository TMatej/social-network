using DataAccessLayer;
using Infrastructure.UnitOfWork;

namespace Infrastructure.EFCore.UnitOfWork
{
    public class EFUnitOfWork : IUnitOfWork
    {
        public SocialNetworkDBContext Context { get; }

        public EFUnitOfWork(SocialNetworkDBContext dbContext)
        {
            Context = dbContext;
        }

        public void Commit()
        {
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
