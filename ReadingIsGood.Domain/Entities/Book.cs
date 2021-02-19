using System;
using System.Collections.Generic;

#nullable disable

namespace ReadingIsGood.Model.Entities
{
    public partial class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Stock { get; set; }
    }
}
