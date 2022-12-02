namespace GSC_FinalProject.Data.Loan
{
    using GSC_FinalProject.Entities;
    public class LoanRepository : GenericRepository<Loan>, ILoanRepository
    {
        public LoanRepository(DataContext context) : base(context) { }

        public bool SetReturnDate(int id)
        {
            var loan = GetById(id);
            loan.ReturnDate = DateTime.Now;
            return true;
        }
    }
}
