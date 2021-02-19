using GraphQL.Types;
using ReadingIsGood.Model.Entities;

namespace ReadingIsGood.API.GraphQL.Types
{
    public class OrderType : ObjectGraphType<Order>
    {
        public OrderType()
        {
            Field(x => x.BookId, nullable: true);
            Field(x => x.CustomerId);
            Field(x => x.CreateDate);
        }
    }
}