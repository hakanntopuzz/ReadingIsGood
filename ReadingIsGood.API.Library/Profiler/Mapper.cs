using AutoMapper;
using ReadingIsGood.Domain.Requests;
using ReadingIsGood.Model.Dtos;
using ReadingIsGood.Model.Entities;

namespace ReadingIsGood.API.Profiler
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<CreateCustomerRequest, Customer>();
            CreateMap<CreateOrderRequest, Order>();
            CreateMap<Order, OrderDto>();
        }
    }
}