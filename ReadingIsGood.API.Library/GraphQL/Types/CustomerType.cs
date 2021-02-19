using GraphQL.Types;
using ReadingIsGood.Model.Entities;

namespace ReadingIsGood.API.GraphQL.Types
{
    public class CustomerType : ObjectGraphType<Customer>
    {
        public CustomerType()
        {
            Field(x => x.Id);
            Field(x => x.Name);
        }
    }
}