using System;
using System.Collections.Generic;

namespace SuperBrandsTest.Entities.Models
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Size> Sizes { get; set; }
    }
    public class BrandDto:Interfaces.IValidateModel
    {
        public string Name { get; set; }
        public List<SizeDto> Sizes { get; set; }

        public bool Validate()
        {
            return !string.IsNullOrWhiteSpace(Name);
        }
    }
}
