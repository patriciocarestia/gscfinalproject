namespace GSC_FinalProject.Data.User
{
    using GSC_FinalProject.Entities;
    public interface IUserRepository : IGenericRepository<User>
    {
        User Login(string username, string password);
    }
}
