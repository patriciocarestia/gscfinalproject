using AutoMapper;
using GSC_FinalProject.Data;
using GSC_FinalProject.Dto;
using GSC_FinalProject.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;

namespace GSC_FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public LoanController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var loans = _uow.LoansRepository.GetAll();
            var mappedLoans = _mapper.Map<IEnumerable<LoanDTO>>(loans);
            return Ok(mappedLoans);
        }

        [HttpGet("{id}")]
        public IActionResult GetLoan(int id)
        {
            var loan = _uow.LoansRepository.GetById(id);
            if (loan == null)
                return NotFound();

            loan.Person = _uow.PersonsRepository.GetById(loan.Id);
            var mappedLoan = _mapper.Map<LoanDTO>(loan);
            return Ok(mappedLoan);
        }

        [HttpPost]
        public IActionResult Create([FromBody] LoanDTO loanDTO)
        {
            var loan = _mapper.Map<Loan>(loanDTO);
            loan.CreateDate = DateTime.UtcNow;
            loan.ReturnDate = null;

            _uow.LoansRepository.Add(loan);
            _uow.Complete();

            var mappedLoan = _mapper.Map<LoanDTO>(loan);
            return Ok(mappedLoan);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _uow.LoansRepository.Delete(id);
            _uow.Complete();
            return Ok();
        }
    }
}
