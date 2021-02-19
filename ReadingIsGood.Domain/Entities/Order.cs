using System;
using System.Collections.Generic;

#nullable disable

namespace ReadingIsGood.Model.Entities
{
    public partial class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int? BookId { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
