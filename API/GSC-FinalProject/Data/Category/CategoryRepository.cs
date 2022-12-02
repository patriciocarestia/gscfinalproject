namespace GSC_FinalProject.Data.Category
{
    using GSC_FinalProject.Entities;
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(DataContext context) : base(context) { }
    }
}
