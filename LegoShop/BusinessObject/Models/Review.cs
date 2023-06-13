using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Review
    {
        public int ReviewId { get; set; }
        public int AccountId { get; set; }
        public int ProductId { get; set; }
        public string Content { get; set; } = null!;
        public int? Rating { get; set; }

        public virtual Account Account { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
