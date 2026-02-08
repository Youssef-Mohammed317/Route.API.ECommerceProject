using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domian.Entites.ProductModule
{
    public class Product : BaseEntity<int>
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public string PictureUrl { get; set; } = null!;
        public string PictureName { get; set; } = null!;

        #region Relations
        [ForeignKey(nameof(ProductBrand))]
        public int ProductBrandId { get; set; }

        public virtual ProductBrand ProductBrand { get; set; } = null!;


        [ForeignKey(nameof(ProductType))]
        public int ProductTypeId { get; set; }

        public virtual ProductType ProductType { get; set; } = null!;
        #endregion
    }
}
