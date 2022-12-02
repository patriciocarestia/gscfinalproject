namespace GSC_FinalProject.Data.Loan
{
    using GSC_FinalProject.Entities;
    public interface ILoanRepository : IGenericRepository<Loan>
    {
        bool SetReturnDate(int id);
    }
}