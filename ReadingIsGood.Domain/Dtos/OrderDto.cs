using System;

namespace ReadingIsGood.Model.Dtos
{
    public class OrderDto
    {
        public int CustomerId { get; set; }
        public int BookId { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
