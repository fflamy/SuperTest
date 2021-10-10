using System;
using System.Collections.Generic;
using System.Text;

namespace SuperBrandsTest.Entities.Models
{
    public class Product : Interfaces.IValidateModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public int SizeId { get; set; }
        public Size Size { get; set; }
        public bool Validate()
        {
            return Size !=null && !(string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Size.BrandSize) || Size.RussianSize <= 0 || BrandId <= 0);
        }
    }
    public class ProductDto
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public double RussianSize { get; set; }

    }
}
