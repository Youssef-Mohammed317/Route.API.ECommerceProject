using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Shared.DTOs.BasketDTOs
{
    public class BasketItemDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        [Range(1, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }
        public string PictureUrl { get; set; } = string.Empty;
        public string PictureName { get; set; } = string.Empty;
        [Range(1, 100, ErrorMessage = "Quantity must be between 1 and 100.")]
        public int Quantity { get; set; }
    }
}
