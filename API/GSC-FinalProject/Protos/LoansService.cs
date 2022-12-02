using AutoMapper;
using Grpc.Core;
using GSC_FinalProject.Data;

namespace LoansAPI.Protos
{
    public class LoansService : LoanService.LoanServiceBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public LoansService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public override Task<LoanReply> SetReturnDate(LoanRequest request, ServerCallContext context)
        {
            var loan = _uow.LoansRepository.GetById(request.Id);

            _uow.LoansRepository.SetReturnDate(request.Id);
            _uow.Complete();

            return Task.FromResult(new LoanReply
            {
                Message = "Return date has updated successfully",
                Success = true
            });
        }
    }
}