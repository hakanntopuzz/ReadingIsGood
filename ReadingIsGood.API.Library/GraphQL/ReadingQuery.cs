using GraphQL.Types;
using ReadingIsGood.API.GraphQL.Types;
using ReadingIsGood.Business.Services.Interfaces;
using System.Collections.Generic;

namespace ReadingIsGood.API.GraphQL
{
    public class ReadingQuery : ObjectGraphType
    {
        public ReadingQuery(IOrderService orderService, ICustomerService customerService)
        {
            Field<ListGraphType<OrderType>>(
                "orders",
                arguments: new QueryArguments(new List<QueryArgument>
                {
                    new QueryArgument<IntGraphType> { Name = "bookId" },
                    new QueryArgument<IntGraphType> { Name = "customerId" }
                }),
                resolve: context =>
                {
                    var bookId = context.GetArgument<int?>("bookId");
                    var customerId = context.GetArgument<int?>("customerId");

                    return orderService.GetOrders(customerId, bookId);
                });

            Field<ListGraphType<CustomerType>>(
              "customers",
              arguments: new QueryArguments(new List<QueryArgument>
              {
                    new QueryArgument<IntGraphType> { Name = "id" },
                    new QueryArgument<StringGraphType> { Name = "name" }
              }),
              resolve: context =>
              {
                  var name = context.GetArgument<string?>("name");
                  var id = context.GetArgument<int?>("id");

                  return customerService.GetCustomers(id, name);
              });
        }
    }
}