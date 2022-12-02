namespace GSC_FinalProject.Data.Thing
{
    using GSC_FinalProject.Data.Person;
    using GSC_FinalProject.Entities;
    public class ThingRepository : GenericRepository<Thing>, IThingRepository
    {
        public ThingRepository(DataContext context) : base(context) { }
    }
}
