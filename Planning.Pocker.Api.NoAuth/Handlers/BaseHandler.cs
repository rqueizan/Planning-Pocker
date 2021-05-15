using Planning.Pocker.Api.NoAuth.Data;
using System.Threading.Tasks;

namespace Planning.Pocker.Api.NoAuth.Handlers
{
    public abstract class BaseHandler
    {
        protected readonly ApiDbContext Context;

        protected BaseHandler(ApiDbContext context) => this.Context = context;

        protected async Task<bool> SaveChangesAsync() => (await Context.SaveChangesAsync()) > 0;
    }
}
