using SuperBrandsTest.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperBrandsTest.API.Product.Models
{
    public class ProductModel
    {
        public ProductDto ProductDto { get; set; }
        public int BrandId { get; set; }
    }
}
