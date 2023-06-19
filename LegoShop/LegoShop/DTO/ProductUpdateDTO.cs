﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace StoreAPI.DTO
{
    public class ProductUpdateDTO
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Range(0, 5)]
        public int? Rating { get; set; }
        public string? Review { get; set; }
        [Required]
        public string Status { get; set; } = null!;
        [ValidateNever]
        public int? CategoryId { get; set; }
        [Required]
        [Range(0, 200000000)]
        public decimal Price { get; set; }
        [Required]
        [Range(0, 200)]
        public int Quantity { get; set; }
        [Required]
        [Range(0, 200000)]
        public int Piece { get; set; }
        [Required]
        public string Age { get; set; } = null!;
    }
}
