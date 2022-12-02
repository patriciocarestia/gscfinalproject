namespace GSC_FinalProject.Data.User;

using GSC_FinalProject.Entities;
public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(DataContext context) : base(context) { }

    public User Login(string username, string password)
    {
        return _dbSet.FirstOrDefault(x => x.Username == username && x.Password == password)!;
    }

}
