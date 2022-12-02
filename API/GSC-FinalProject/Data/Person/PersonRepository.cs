namespace GSC_FinalProject.Data.Person
{
    using GSC_FinalProject.Entities;
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(DataContext context) : base(context) { }
    }
}
