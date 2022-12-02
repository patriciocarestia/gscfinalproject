using AutoMapper;
using GSC_FinalProject.Data;
using GSC_FinalProject.Dto;
using GSC_FinalProject.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GSC_FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CategoryController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public IActionResult GetAll()
        {
            var categories = _uow.CategoriesRepository.GetAll();
            var mappedCategories = _mapper.Map<IEnumerable<CategoryDTO>>(categories);
            return Ok(mappedCategories);
        }

        [HttpGet("{id}")]
        public IActionResult GetCategory(int id)
        {
            var category = _uow.CategoriesRepository.GetById(id);
            if (category == null)
                return NotFound();

            var mappedCategory = _mapper.Map<CategoryDTO>(category);
            return Ok(mappedCategory);
        }

        [HttpPost]
        public IActionResult Add([FromBody] CategoryDTO categoryDTO)
        {
            var categoryExists = _uow.CategoriesRepository.Exists(c => c.Description == categoryDTO.Description);
            if (categoryExists)
                return BadRequest();

            var category = _mapper.Map<Category>(categoryDTO);
            category.CreationDate = DateTime.UtcNow;
            _uow.CategoriesRepository.Add(category);
            _uow.Complete();

            var mappedCategory = _mapper.Map<CategoryDTO>(category);
            return Ok(mappedCategory);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] CategoryDTO categoryDTO)
        {
            var category = _uow.CategoriesRepository.GetById(id);

            if (category == null)
                return NotFound();

            var categoryExists = _uow.CategoriesRepository.Exists(c => c.Description == categoryDTO.Description);
            if (categoryExists)
            {
                return BadRequest();
            }

            category.Description = categoryDTO.Description;

            _uow.CategoriesRepository.Update(category);
            _uow.Complete();

            var mappedCategory = _mapper.Map<CategoryDTO>(category);
            return Ok(mappedCategory);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var category = _uow.CategoriesRepository.GetById(id);
            if (category == null)
                return NotFound();

            _uow.CategoriesRepository.Delete(category.Id);
            _uow.Complete();
            return Ok();
        }
    }
}
