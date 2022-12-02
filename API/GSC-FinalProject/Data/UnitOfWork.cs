using GSC_FinalProject.Data.Category;
using GSC_FinalProject.Data.Loan;
using GSC_FinalProject.Data.Person;
using GSC_FinalProject.Data.Thing;
using GSC_FinalProject.Data.User;

namespace GSC_FinalProject.Data
{
    public class UnitOfWork : IUnitOfWork
    { 
        private readonly DataContext _context;
        public IPersonRepository PersonsRepository { get; }

        public ICategoryRepository CategoriesRepository { get; }

        public ILoanRepository LoansRepository { get; }

        public IThingRepository ThingsRepository { get; }

        public IUserRepository UsersRepository { get; }
        public UnitOfWork(DataContext context)
        {
            _context = context;
            PersonsRepository = new PersonRepository(_context);
            CategoriesRepository = new CategoryRepository(_context);
            LoansRepository = new LoanRepository(_context);
            ThingsRepository = new ThingRepository(_context);
            UsersRepository = new UserRepository(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }
    }
}
