using GraphQL;
using GraphQL.Types;

namespace ReadingIsGood.API.GraphQL
{
    public class ReadingSchema : Schema
    {
        public ReadingSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<ReadingQuery>();
        }
    }
}
