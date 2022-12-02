using AutoMapper;
using GSC_FinalProject.Data;
using GSC_FinalProject.Dto;
using GSC_FinalProject.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace GSC_FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public PersonController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var persons = _uow.PersonsRepository.GetAll();
            return Ok(persons);
        }

        [HttpGet("{id}")]
        public IActionResult GetPerson(int id)
        {
            var person = _uow.PersonsRepository.GetById(id);
            if (person == null)
                return NotFound();

            var mappedPerson = _mapper.Map<PersonDTO>(person);
            return Ok(mappedPerson);
        }

        [HttpPost]
        public IActionResult Create([FromBody] PersonDTO personDTO)
        {
            var person = _mapper.Map<Person>(personDTO);
            _uow.PersonsRepository.Add(person);
            _uow.Complete();

            var mappedPerson = _mapper.Map<PersonDTO>(person);
            return Ok(mappedPerson);
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id, [FromBody] PersonDTO personDTO)
        {
            var person = _uow.PersonsRepository.GetById(id);
            if (personDTO is null)
                return NotFound();

            person.Name = personDTO.Name;
            person.Email = personDTO.Email;
            person.PhoneNumber = personDTO.PhoneNumber;

            _uow.PersonsRepository.Update(person);
            _uow.Complete();

            var mappedPerson = _mapper.Map<PersonDTO>(person);
            return Ok(mappedPerson);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var person = _uow.PersonsRepository.GetById(id);
            if (person is null)
                return NotFound();

            _uow.PersonsRepository.Delete(person.Id);
            _uow.Complete();
            return Ok();
        }
    }
}
