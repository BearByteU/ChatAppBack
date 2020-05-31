using ChatApp.Models;
using ChatApp.SRMDbContexts.IRepository;
using System.Threading.Tasks;

namespace ChatApp.SRMDbContexts.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ChatAppDbContext _dbcontext;

        public UnitOfWork(ChatAppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public IGenericRepository<User, int> UserRepository => new GenericRepository<User, int>(_dbcontext);

        public IGenericRepository<Message, int> MessageRepository => new GenericRepository<Message, int>(_dbcontext);

        public IGenericRepository<UniqueUserKey, int> UniqueUserRepository => new GenericRepository<UniqueUserKey, int>(_dbcontext);

        public IGenericRepository<Connection, int> ConnectionRepository => new GenericRepository<Connection, int>(_dbcontext);

        public IGenericRepository<ConversationRoom, int> UserRoomRepository => new GenericRepository<ConversationRoom, int>(_dbcontext);

        public void Dispose()
        {
            _dbcontext.Dispose();
        }

        public async Task Save()
        {
            await _dbcontext.SaveChangesAsync();
        }
        
    }
}
