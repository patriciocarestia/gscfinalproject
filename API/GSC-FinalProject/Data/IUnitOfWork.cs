using GSC_FinalProject.Data.Person;
using GSC_FinalProject.Data.Category;
using GSC_FinalProject.Data.Loan;
using GSC_FinalProject.Data.Thing;
using GSC_FinalProject.Data.User;

namespace GSC_FinalProject.Data
{
    public interface IUnitOfWork
    {
        public IPersonRepository PersonsRepository { get; }
        public ICategoryRepository CategoriesRepository { get; }
        public ILoanRepository LoansRepository { get; }
        public IThingRepository ThingsRepository { get; }
        public IUserRepository UsersRepository { get; }

        public int Complete();
    }
}
