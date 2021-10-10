using System;
using System.Collections.Generic;
using System.Text;

namespace SuperBrandsTest.Entities.Models
{
    public class Size
    {
        public int Id { get; set; }
        public double RussianSize { get; set; }
        public string BrandSize { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }

    }
    public class SizeDto
    {
        public int BrandId { get; set; }
        public double RussianSize { get; set; }
        public string BrandSize { get; set; }
    }
}
