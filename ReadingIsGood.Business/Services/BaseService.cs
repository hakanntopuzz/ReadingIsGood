using AutoMapper;

namespace ReadingIsGood.Business.Services
{
    public abstract class BaseService
    {
        protected readonly IMapper _mapper;
        protected readonly IUnitofWork _unitofWork;

        public BaseService(IMapper mapper,
            IUnitofWork unitofWork)
        {
            _mapper = mapper;
            _unitofWork = unitofWork;
        }
    }
}