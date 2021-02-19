using System;

#nullable disable

namespace ReadingIsGood.Model.Entities
{
    public partial class Audit
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string TableName { get; set; }
        public string Action { get; set; }
        public string KeyValues { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
