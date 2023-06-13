using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Image
    {
        public int ImageId { get; set; }
        public int ProductId { get; set; }
        public string Img { get; set; } = null!;

        public virtual Product Product { get; set; } = null!;
    }
}
