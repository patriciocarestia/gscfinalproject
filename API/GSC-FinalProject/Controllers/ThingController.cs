using AutoMapper;
using GSC_FinalProject.Data;
using GSC_FinalProject.Entities;
using GSC_FinalProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GSC_FinalProject.Controllers
{
    public class ThingController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly List<Category> _categories;

        public ThingController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
            _categories = uow.CategoriesRepository.GetAll().ToList();
        }

        [HttpGet]
        public IActionResult Index()
        {
            var things = _uow.ThingsRepository.GetAll();
            var mappedThings = _mapper.Map<IEnumerable<ThingViewModel>>(things);
            return View(mappedThings);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewData["Categories"] = new SelectList(_categories, "Id", "Description");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(ThingViewModel thingViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Add", thingViewModel);
            }

            var category = _uow.CategoriesRepository.GetById(thingViewModel.CategoryId);
            if (category == null)
            {
                ModelState.AddModelError("CategoryId", "Category not found");
                return View(thingViewModel);
            }

            var thing = _mapper.Map<Thing>(thingViewModel);
            thing.CreationDate = DateTime.UtcNow;
            _uow.ThingsRepository.Add(thing);
            _uow.Complete();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
                return NotFound();

            var thing = _uow.ThingsRepository.GetById(id.Value);
            if (thing == null)
                return NotFound();

            var thingViewModel = _mapper.Map<ThingViewModel>(thing);
            return View(thingViewModel);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var thing = _uow.ThingsRepository.GetById(id.Value);
            if (thing == null)
                return NotFound();

            var thingViewModel = _mapper.Map<ThingViewModel>(thing);
            ViewData["Categories"] = new SelectList(_categories, "Id", "Description", thingViewModel.CategoryId);
            return View(thingViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ThingViewModel thingViewModel)
        {
            var thing = _uow.ThingsRepository.GetById(id);
            if (thing == null)
                return NotFound();

            if (!ModelState.IsValid)
            {
                ViewData["Categories"] = new SelectList(_categories, "Id", "Description", thingViewModel.CategoryId);
                return View(thingViewModel);
            }

            var updatedThing = _mapper.Map<ThingViewModel, Thing>(thingViewModel, thing);
            _uow.ThingsRepository.Update(updatedThing);
            _uow.Complete();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var thing = _uow.ThingsRepository.GetById(id.Value);
            if (thing == null)
                return NotFound();

            var thingViewModel = _mapper.Map<ThingViewModel>(thing);
            return View(thingViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var thing = _uow.ThingsRepository.GetById(id);
            if (thing == null)
                return NotFound();

            _uow.ThingsRepository.Delete(thing.Id);
            _uow.Complete();
            return RedirectToAction(nameof(Index));
        }
    }

}
